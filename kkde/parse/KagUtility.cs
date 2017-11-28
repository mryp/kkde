using System;
using System.Collections.Generic;
using System.Text;
using kkde.parse.complate;
using kkde.global;
using ICSharpCode.TextEditor.Document;

namespace kkde.parse
{
	/// <summary>
	/// KAG関連処理のユーティリティクラス
	/// </summary>
	public static class KagUtility
	{
		/// <summary>
		/// 再帰を行う限界数
		/// </summary>
		private const int MAX_REVERSE_COUNT = 20;

		/// <summary>
		/// KAGタグの情報をライン行文字列から取得し返す
		/// </summary>
		/// <param name="line">情報を取得するタグ情報</param>
		/// <returns>タグ情報</returns>
		public static KagTagKindInfo GetTagKind(string line)
		{
			KagTagKindInfo info = new KagTagKindInfo();

			bool startFlag = true;	//先頭の時true
			bool stringModeFlag = false;	//文字列中（""）のときtrue
			for (int i = 0; i < line.Length; i++)
			{
				if (line[i] == '[')
				{
					startFlag = false;
					if (stringModeFlag == false && info.Kind == KagCompletionData.Kind.Unknown)
					{
						info.Kind = KagCompletionData.Kind.KagTagName;
						info.TagName = "";
						info.AttrName = "";
						info.AttrValue = "";
						info.AttrTable.Clear();
					}
				}
				else if (line[i] == '@')
				{
					if (startFlag)
					{
						startFlag = false;
						info.Kind = KagCompletionData.Kind.KagTagName;
						info.TagName = "";
						info.AttrName = "";
						info.AttrValue = "";
						info.AttrTable.Clear();
					}
				}
				else if (line[i] == ']')
				{
					startFlag = false;
					if (stringModeFlag == false)
					{
						info.Kind = KagCompletionData.Kind.Unknown;
						info.TagName = "";
						info.AttrValue = "";
						info.AttrValue = "";
					}
				}
				else if (line[i] == ' ')
				{
					startFlag = false;
					switch (info.Kind)
					{
						case KagCompletionData.Kind.KagTagName:
						case KagCompletionData.Kind.AttrName:
						case KagCompletionData.Kind.AttrValue:
							if (stringModeFlag == false)
							{
								info.Kind = KagCompletionData.Kind.AttrName;
								info.AddAttrTable(info.AttrName, info.AttrValue);
								info.AttrName = "";
								info.AttrValue = "";
							}
							break;
						default:
							break;
					}
				}
				else if (line[i] == '=')
				{
					startFlag = false;
					switch (info.Kind)
					{
						case KagCompletionData.Kind.AttrName:
							info.Kind = KagCompletionData.Kind.AttrValue;
							info.AddAttrTable(info.AttrName, info.AttrValue);
							info.AttrValue = "";
							break;
						default:
							break;
					}
				}
				else if (line[i] == '"')
				{
					startFlag = false;
					if (i != 0 && line[i - 1] == '\\')	//"がエスケープされているとき
					{
						continue;
					}

					stringModeFlag = !stringModeFlag;	//フラグを反転させる
				}
				else if (line[i] == '\t')
				{
					//何もしない
				}
				else
				{
					startFlag = false;
					switch (info.Kind)
					{
						case KagCompletionData.Kind.KagTagName:
							info.TagName += line[i];
							break;
						case KagCompletionData.Kind.AttrName:
							info.AttrName += line[i];
							break;
						case KagCompletionData.Kind.AttrValue:
							info.AttrValue += line[i];
							break;
					}
				}
			}

			if (info.Kind == KagCompletionData.Kind.AttrName
			|| info.Kind == KagCompletionData.Kind.AttrValue)
			{
				info.AddAttrTable(info.AttrName, info.AttrValue);
			}
			info.StrMode = stringModeFlag;
			return info;
		}

		/// <summary>
		/// KAGタグの情報をエディタドキュメント情報から取得し返す
		/// </summary>
		/// <param name="document">エディタのドキュメント情報</param>
		/// <param name="lineNumber">行番号</param>
		/// <param name="colNumber">カーソル位置を表す桁番号</param>
		/// <returns>KAGタグ情報（見つからなかったときはnullを返す）</returns>
		public static KagTagKindInfo GetTagKind(IDocument document, int lineNumber, int colNumber)
		{
			if (document == null)
			{
				return null;	//ドキュメントがないとき
			}
			LineSegment lineSeg = document.GetLineSegment(lineNumber);
			if (lineSeg == null)
			{
				return null;	//情報が無いとき
			}

			string lineText = document.GetText(lineSeg);
			if (colNumber < lineText.Length)
			{
				lineText = lineText.Substring(0, colNumber);
			}
			if (KagUtility.ExistLineHead(lineText, ';'))
			{
				return null;	//コメント行のとき
			}
			if (KagUtility.ExistLineHead(lineText, '*'))
			{
				return null;	//ラベル行のとき
			}

			KagTagKindInfo info = KagUtility.GetTagKind(lineText);
			return info;
		}

		/// <summary>
		/// 指定した位置が行頭かどうか
		/// （ここでの行頭はKAGの行頭を意味する）
		/// </summary>
		/// <param name="line">チェックする行文字列</param>
		/// <param name="col">指定する位置</param>
		/// <returns>行頭の時はtrue</returns>
		public static bool IsLineHead(string line, int col)
		{
			if (col == 0)
			{
				return true;	//一番先頭の場合は問答無用で行頭
			}
			if (col > line.Length)
			{
				return false;	//範囲オーバー
			}

			for (int i = 0; i < col; i++)	//一番先頭から現在位置まで検索
			{
				if (line[i] != '\t')		//タブ以外
				{
					return false;	//行頭ではない
				}
			}

			return true;	//行頭
		}

		/// <summary>
		/// 行頭に指定した文字が存在するかどうかが存在するかどうか
		/// （ここでの行頭はKAGの行頭を意味する）
		/// </summary>
		/// <param name="line">行文字列</param>
		/// <param name="ch">チェックする文字</param>
		/// <returns>行頭にチェックする文字があったときはtrue</returns>
		public static bool ExistLineHead(string line, char ch)
		{
			if (line == null || line == "")
			{
				return false;	//行が存在しない
			}

			bool look = false;
			for (int i = 0; i < line.Length; i++)
			{
				if (line[i] == ch)
				{
					look = true;
					break;	//発見
				}
				else if (line[i] == '\t')
				{
					//無視する
				}
				else
				{
					look = false;
					break;	//指定文字以外が見つかった
				}
			}

			return look;
		}

		/// <summary>
		/// マクロオブジェクトを取得する
		/// マクロリストはグローバルから取得する
		/// </summary>
		/// <param name="macroName">取得するマクロ名</param>
		/// <returns>マクロオブジェクト（見つからなかったらnull）</returns>
		public static KagMacro GetKagMacro(string macroName)
		{
			KagMacro[] macroList = GlobalStatus.ParserSrv.GetKagMacroList();
			return GetKagMacro(macroName, macroList);
		}

		/// <summary>
		/// マクロオブジェクトを取得する
		/// 存在しないときはnullを返す
		/// </summary>
		/// <param name="macroName">取得するマクロ名</param>
		/// <param name="macroList">マクロ情報リスト</param>
		/// <returns>マクロオブジェクト</returns>
		public static KagMacro GetKagMacro(string macroName, KagMacro[] macroList)
		{
			if (macroName == "" || macroList == null)
			{
				return null;
			}

			KagMacro macro = null;
			foreach (KagMacro m in macroList)
			{
				if (macroName == m.Name)
				{
					macro = m;
					break;
				}
			}

			return macro;
		}

		/// <summary>
		/// 属性名リストを取得する
		/// </summary>
		/// <param name="info">現在位置の入力情報</param>
		/// <param name="macroList">マクロ情報リスト</param>
		/// <returns>マクロ名リスト</returns>
		public static KagMacroAttr GetKagMacroAttr(string attrName, KagTagKindInfo info, KagMacro[] macroList)
		{
			//マクロを取得
			KagMacro macro = KagUtility.GetKagMacro(info.TagName, macroList);
			if (macro == null)
			{
				return null;	//マクロが見つからない
			}

			//属性を取得
			Dictionary<string, KagMacroAttr> table = new Dictionary<string, KagMacroAttr>();
			table = getAttrNameListFromMacro(macro, macroList, table, 0);

			//属性を検索する
			KagMacroAttr attr = null;
			if (table.ContainsKey(attrName))
			{
				attr = table[attrName];
			}

			return attr;
		}

		/// <summary>
		/// マクロオブジェクトから属性名リストを取得する
		/// ＊再帰する
		/// </summary>
		/// <param name="macro">取得するマクロオブジェクト</param>
		/// <param name="macroList">マクロ情報リスト</param>
		/// <param name="table">取得した属性名を格納するテーブル</param>
		/// <returns>取得した属性名テーブル（tableをそのまま返す）</returns>
		private static Dictionary<string, KagMacroAttr> getAttrNameListFromMacro(KagMacro macro
			, KagMacro[] macroList, Dictionary<string, KagMacroAttr> table, int reverseCount)
		{
			if (reverseCount > MAX_REVERSE_COUNT)
			{
				//再帰回数がオーバーしているときは何もせずに終了する
				return table;
			}
			reverseCount++;

			//通常のマクロ属性を追加
			foreach (KagMacroAttr attr in macro.AttrTable.Values)
			{
				if (table.ContainsKey(attr.Name) == false)
				{
					//存在しないときだけ追加する
					table.Add(attr.Name, attr);
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
				table = getAttrNameListFromMacro(asterMacro, macroList, table, reverseCount);
			}

			return table;
		}
	}
}
