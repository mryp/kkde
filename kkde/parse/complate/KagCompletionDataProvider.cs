using System;
using System.Collections.Generic;
using System.Text;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Gui.CompletionWindow;
using kkde.global;
using ICSharpCode.TextEditor.Document;
using System.Diagnostics;

namespace kkde.parse.complate
{
	/// <summary>
	/// KAG入力補完プロバイダクラス
	/// </summary>
	public class KagCompletionDataProvider : AbstractCompletionDataProvider
	{
		#region 定数
		/// <summary>
		/// ループ限界回数（主に再帰チェックに使用する）
		/// </summary>
		const int MAX_LOOP_COUNT = 20;
		#endregion

		#region フィールド
		/// <summary>
		/// 循環参照（＊とか）でチェックしカウントする変数
		/// </summary>
		int m_reverseCount = 0;
		#endregion

		#region 入力補完文字列関連
		/// <summary>
		/// KAG入力補完リストに表示するアイテムリストを返す
		/// </summary>
		/// <param name="fileName">ファイル名</param>
		/// <param name="textArea">アクティブなテキストエディタ</param>
		/// <param name="charTyped">入力された文字列</param>
		/// <returns>入力補完アイテムリスト</returns>
		public override ICompletionData[] GenerateCompletionData(string fileName, TextArea textArea, char charTyped)
		{
			KagMacro[] macroList = GlobalStatus.ParserSrv.GetKagMacroList();
			if (macroList.Length == 0)
			{
				return null;	//一つもないときはリストを表示しない
			}

			int lineNum = textArea.Caret.Line;
			int colNum = textArea.Caret.Column;
			LineSegment lineSeg = textArea.Document.GetLineSegment(lineNum);
			string lineText = textArea.Document.GetText(lineSeg.Offset, colNum);
			if (KagUtility.ExistLineHead(lineText, ';'))
			{
				return null;	//コメント行のとき
			}
			if (KagUtility.ExistLineHead(lineText, '*'))
			{
				return null;	//ラベル行のとき
			}

			m_preSelection = null;
			ICompletionData[] list = null;
			KagTagKindInfo info = KagUtility.GetTagKind(lineText);
			if (charTyped == '[')		//タグ名？
			{
				if (KagUtility.ExistLineHead(lineText, '@'))
				{
					return null;	//@タグがある
				}
				if (info.StrMode)
				{
					return null;	//文字列表示中
				}

				//タグ入力
				list = getMacroNameList(info, macroList);
			}
			else if (charTyped == '@')	//タグ名？
			{
				if (KagUtility.IsLineHead(lineText, colNum) == false)
				{
					return null;	//行頭ではない
				}
				if (info.StrMode)
				{
					return null;
				}

				//タグ入力
				list = getMacroNameList(info, macroList);
			}
			else if (charTyped == ' ')	//属性名？
			{
				if ((info.Kind == KagCompletionData.Kind.Unknown)
				|| (info.StrMode == true))
				{
					return null;	//属性名ではないので何もしない
				}

				//属性名
				list = getAttrNameList(info, macroList);
			}
			else if (charTyped == '=')	//属性値？
			{
				if (info.Kind != KagCompletionData.Kind.AttrName)
				{
					return null;	//属性値ではないので何もしない
				}

				//属性値
				list = getAttrValueList(info, macroList);
			}
			else if (charTyped == '\0')	//その他（Ctrl+Space）
			{
				switch (info.Kind)
				{
					case KagCompletionData.Kind.KagTagName:
					case KagCompletionData.Kind.Unknown:
						if (info.StrMode)
						{
							return null;
						}
						//タグ入力
						list = getMacroNameList(info, macroList);
						break;
					case KagCompletionData.Kind.AttrName:
						if (info.StrMode)
						{
							return null;
						}
						//属性入力
						list = getAttrNameList(info, macroList);
						break;
					case KagCompletionData.Kind.AttrValue:
						//属性値入力
						list = getAttrValueList(info, macroList);
						break;
					default:
						return null;
				}

				//Debug.WriteLine("info=" + info.ToString());
				m_preSelection = getCompReplaceText(textArea, info);
			}
			else
			{
				return null;	//何も表示しない
			}

			if (list == null || list.Length == 0)
			{
				return null;	//一つもないとき
			}
			return list;
		}
		#endregion

		#region マクロ・タグ名リスト取得関連
		/// <summary>
		/// マクロ名リストを取得する
		/// </summary>
		/// <param name="info">現在位置の入力情報</param>
		/// <param name="macroList">マクロ情報リスト</param>
		/// <returns>マクロ名リスト</returns>
		ICompletionData[] getMacroNameList(KagTagKindInfo info, KagMacro[] macroList)
		{
			List<ICompletionData> list = new List<ICompletionData>();
			foreach (KagMacro macro in macroList)
			{
				list.Add(new KagCompletionData(macro.Name, macro.Comment, getTagMacroKind(macro)));
			}

			return list.ToArray();
		}

		/// <summary>
		/// KAGマクロの情報から入力補完用マクロの種類を返す
		/// </summary>
		/// <param name="macro"></param>
		/// <returns></returns>
		KagCompletionData.Kind getTagMacroKind(KagMacro macro)
		{
			KagCompletionData.Kind kind = KagCompletionData.Kind.KagTagName;	//デフォルトをセット
			switch (macro.DefType)
			{
				case KagMacro.DefineType.Kag:
					kind =  KagCompletionData.Kind.KagTagName;
					break;
				case KagMacro.DefineType.Kagex:
					kind = KagCompletionData.Kind.KagexTagName;
					break;
				case KagMacro.DefineType.User:
					kind = KagCompletionData.Kind.UserTagName;
					break;
			}

			return kind;
		}
		#endregion

		#region 属性名リスト取得関連
		/// <summary>
		/// 属性名リストを取得する
		/// </summary>
		/// <param name="info">現在位置の入力情報</param>
		/// <param name="macroList">マクロ情報リスト</param>
		/// <returns>マクロ名リスト</returns>
		ICompletionData[] getAttrNameList(KagTagKindInfo info, KagMacro[] macroList)
		{
			KagMacro macro = KagUtility.GetKagMacro(info.TagName, macroList);
			if (macro == null)
			{
				return null;	//マクロが見つからない
			}

			//属性を取得
			resetReverseCount();
			Dictionary<string, ICompletionData> table = new Dictionary<string, ICompletionData>();
			table = getAttrNameListFromMacro(macro, macroList, table);

			//すでに書いているものは削除する
			foreach (string deleteAttrName in info.AttrTable.Keys)
			{
				table.Remove(deleteAttrName);
			}

			//出力用にリストを変換
			ICompletionData[] list = new ICompletionData[table.Count];
			int i = 0;
			foreach (ICompletionData data in table.Values)
			{
				list[i++] = data;
			}
			return list;
		}

		/// <summary>
		/// マクロオブジェクトから属性名リストを取得する
		/// ＊再帰する
		/// </summary>
		/// <param name="macro">取得するマクロオブジェクト</param>
		/// <param name="macroList">マクロ情報リスト</param>
		/// <param name="table">取得した属性名を格納するテーブル</param>
		/// <returns>取得した属性名テーブル（tableをそのまま返す）</returns>
		private Dictionary<string, ICompletionData> getAttrNameListFromMacro(KagMacro macro
			, KagMacro[] macroList, Dictionary<string, ICompletionData> table)
		{
			if (overcheckReverseCount())
			{
				//再帰回数がオーバーしているときは何もせずに終了する
				return table;
			}

			//通常のマクロ属性を追加
			foreach (KagMacroAttr attr in macro.AttrTable.Values)
			{
				if (table.ContainsKey(attr.Name) == false)
				{
					//存在しないときだけ追加する
					table.Add(attr.Name, new KagCompletionData(attr.Name, attr.Comment, KagCompletionData.Kind.AttrName));
				}
			}

			//全省略マクロ属性を追加
			KagMacro asterMacro = null;
			foreach (string macroName in macro.AsteriskTagList)
			{
				asterMacro = KagUtility.GetKagMacro(macroName, macroList);
				if (asterMacro == null)
				{
					continue;	//このマクロは飛ばす
				}

				//自分自身を呼び出し
				table = getAttrNameListFromMacro(asterMacro, macroList, table);
			}

			return table;
		}
		#endregion

		#region 属性値リスト取得関連
		/// <summary>
		/// 属性値リストを取得する
		/// </summary>
		/// <param name="macro">取得するマクロオブジェクト</param>
		/// <param name="macroList">マクロ情報リスト</param>
		/// <returns>属性値リスト</returns>
		private ICompletionData[] getAttrValueList(KagTagKindInfo info, KagMacro[] macroList)
		{
			KagMacro macro = KagUtility.GetKagMacro(info.TagName, macroList);
			if (macro == null)
			{
				return null;	//マクロが見つからない
			}
			resetReverseCount();
			KagMacroAttr attr = getMacroAttr(info.AttrName, macro, macroList);
			if (attr == null)
			{
				return null;	//属性が見つからない
			}

			return KagCompletionAttrValue.GetCompletionDataList(attr, info);
		}

		/// <summary>
		/// マクロ属性オブジェクトを取得する
		/// （再帰する）
		/// </summary>
		/// <param name="attrName">取得したい属性名</param>
		/// <param name="macro">属性の所属するマクロオブジェクト</param>
		/// <param name="macroList">マクロ情報リスト</param>
		/// <returns>属性オブジェクト</returns>
		private KagMacroAttr getMacroAttr(string attrName, KagMacro macro, KagMacro[] macroList)
		{
			if (overcheckReverseCount())
			{
				//再帰回数がオーバーしているときは何もせずに終了する
				return null;
			}

			//属性リストから検索する
			foreach (KagMacroAttr attr in macro.AttrTable.Values)
			{
				if (attrName == attr.Name)
				{
					return attr;	//属性を見つけた
				}
			}

			//省略属性リストから検索する
			KagMacro asterMacro = null;
			foreach (string tagName in macro.AsteriskTagList)
			{
				asterMacro = KagUtility.GetKagMacro(tagName, macroList);
				if (asterMacro == null)
				{
					continue;	//このマクロは見つからない
				}
				KagMacroAttr attr = getMacroAttr(attrName, asterMacro, macroList);
				if (attr != null)
				{
					return attr;	//属性を見つけた
				}
			}

			return null;	//見つからなかった
		}
		#endregion

		#region ループチェックメソッド
		/// <summary>
		/// 再帰回数をリセットする
		/// </summary>
		private void resetReverseCount()
		{
			m_reverseCount = 0;
		}

		/// <summary>
		/// 再帰回数をチェックしカウンタを増やす
		/// </summary>
		/// <returns>回数がオーバーしているときはtrue</returns>
		private bool overcheckReverseCount()
		{
			m_reverseCount++;
			if (m_reverseCount > MAX_LOOP_COUNT)
			{
				Debug.WriteLine("■overcheckReverseCount 最大再帰回数を超えました");
				return true;	//オーバーしている
			}
			else
			{
				return false;
			}
		}
		#endregion

		#region メソッド
		/// <summary>
		/// 入力補完時に置換する選択文字列をセットする
		/// </summary>
		/// <param name="textArea"></param>
		/// <param name="info"></param>
		/// <returns></returns>
		private string getCompReplaceText(TextArea textArea, KagTagKindInfo info)
		{
			string select = base.GetWordBeforeCaret(textArea);
			if (select == "\"" && info.Kind == KagCompletionData.Kind.AttrValue)
			{
				if (info.AttrValue != "")
				{
					select = "\"" + info.AttrValue + "\"";
				}
			}
			else if (select == "=" && info.Kind == KagCompletionData.Kind.AttrValue)
			{
				if (info.AttrValue == "")
				{
					select = null;	//選択しない
				}
			}
			else if (select == "=\"" && info.Kind == KagCompletionData.Kind.AttrValue)
			{
				if (info.AttrValue == "")
				{
					select = "\"";
				}
			}
			else if ((select == "[" || select == "@") && info.Kind == KagCompletionData.Kind.KagTagName)
			{
				select = null;	//選択しない
			}
			else if ((select.EndsWith(" ") == true) && info.Kind == KagCompletionData.Kind.AttrName)
			{
				select = null;	//選択しない
			}

			return select;
		}
		#endregion
	}
}
