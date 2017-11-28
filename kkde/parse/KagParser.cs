using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace kkde.parse
{
	/// <summary>
	/// KAG構文解析クラス
	/// </summary>
	class KagParser : IParser
	{
		KagLexer m_lexer;
		KagCompletionUnit m_cu;

		#region プロパティ
		/// <summary>
		/// 解析結果
		/// </summary>
		public CompletionUnit CompletionUnit
		{
			get { return m_cu; }
		}
		#endregion

		#region メソッド
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="filePath">ファイルパス</param>
		/// <param name="lexer">字句解析オブジェクト</param>
		public KagParser(string filePath, KagLexer lexer)
		{
			m_lexer = lexer;
			m_cu = new KagCompletionUnit(filePath);
		}

		/// <summary>
		/// 構文解析開始
		/// </summary>
		public void Parse()
		{
			Token token;
			while ((token = m_lexer.NextToken()) != null)
			{
				switch (token.Kind)
				{
					case KagTokens.Label:		//ラベルを追加
						m_cu.AddLabel(token.Val, token.Line);
						break;
					case KagTokens.Macro:		//マクロを追加
						m_cu.AddMacro(paserMacro(token));
						break;
					case KagTokens.TjsScript:
						//当分未実装
						break;
					case KagTokens.StartRegion:
						m_cu.AddRegion(RegionItem.Pos.Start, token.Val, token.Line);
						break;
					case KagTokens.EndRegion:
						m_cu.AddRegion(RegionItem.Pos.End, token.Val, token.Line);
						break;
				}
			}
		}

		/// <summary>
		/// マクロを構文解析しオブジェクトを生成する
		/// </summary>
		/// <param name="token">マクロ内容すべてを含むトークン</param>
		private KagMacro paserMacro(Token macroToken)
		{
			KagMacro macro = new KagMacro();
			macro.FilePath = m_cu.FilePath;
			macro.LineNumber = macroToken.Line;

			//マクロ内を解析し、マクロ属性を取得する
			TextReader reader;
			using (reader = new StringReader(macroToken.Val))
			{
				KagMacroLexer macroLexer = new KagMacroLexer(reader);
				Token token;
				while ((token = macroLexer.NextToken()) != null)
				{
					switch (token.Kind)
					{
						case KagTokens.MacroAttr:
							object obj = token.Obj;
							if (obj is Dictionary<string, string>)
							{
								setMacro(ref macro, token.Val, (Dictionary<string, string>)obj);
							}
							break;
						case KagTokens.MacroName:
							break;
						case KagTokens.AsteriskTag:
							break;
					}
				}
			}

			//マクロコメントを解析し対応する属性にセットする
			using (reader = new StringReader(macroToken.Comment))
			{
				while (reader.Peek() > -1)
				{
					parseMAcroCommentLine(ref macro, reader.ReadLine());
				}
			}

			return macro;
		}

		/// <summary>
		/// マクロ属性リストを解析し、マクロオブジェクトに格納する
		/// </summary>
		/// <param name="macro">格納先マクロオブジェクト</param>
		/// <param name="tagName">タグ名</param>
		/// <param name="attrList">属性リスト</param>
		private void setMacro(ref KagMacro macro, string tagName, Dictionary<string, string> attrList)
		{
			if (tagName == "macro")
			{
				//マクロタグの時はname属性値を取得する
				foreach (string key in attrList.Keys)
				{
					if (key == "name")
					{
						if (attrList[key] != "")
						{
							macro.Name = attrList[key];
							//Debug.WriteLine("macro.Name=" + macro.Name);
						}
						return;
					}
				}
			}
			else
			{
				//そのほかのタグ名の時は属性を取得する
				foreach (string key in attrList.Keys)
				{
					if (key == "*")
					{
						//省略属性を持つタグ名を追加
						macro.AddAsteriskTagName(tagName);
					}
					else
					{
						string attrName = getMacroAttrName(attrList[key]);
						if (attrName != "")
						{
							//属性値があるのでセットする
							KagMacroAttr attr = new KagMacroAttr();
							attr.Name = attrName;
							//Debug.WriteLine("attr.Name=" + attr.Name);

							string attrDef = getMacroAttrDef(attrList[key]);
							if (attrDef != "")
							{
								attr.DefaultValue = attrDef;	//属性値があるのでセットする
								//Debug.WriteLine("attr.DefaultValue=" + attr.DefaultValue);
							}

							//属性を追加
							macro.AddAttr(attr);
						}
					}
				}
			}
		}

		/// <summary>
		/// マクロ属性名を取得する
		/// （例：%color=|0x111111 のとき colorを返す）
		/// </summary>
		/// <param name="val">マクロ定義内のタグ属性値（[font color=%color|0xff0000]の場合%color|0xff0000の部分</param>
		/// <returns></returns>
		private string getMacroAttrName(string val)
		{
			if (val.Length < 2)
			{
				return "";	//%と属性名分の文字列サイズもない
			}
			if (val[0] == '%')
			{
				int pos = val.IndexOf('|');
				if (pos == -1)
				{
					return val.Remove(0, 1);
				}
				else
				{
					if (pos - 1 < 1)	//取得できる文字列が一つもないとき（例：%|）
					{
						return "";
					}
					return val.Substring(1, pos - 1);
				}
			}

			return "";
		}

		/// <summary>
		/// マクロ属性のデフォルト値を返す
		/// 例：%color|0x111111 のとき 0x111111を返す
		/// </summary>
		/// <param name="val">マクロ定義内のタグ属性値（[font color=%color|0xff0000]の場合%color|0xff0000の部分</param>
		/// <returns></returns>
		private string getMacroAttrDef(string val)
		{
			int pos = val.IndexOf('|');
			if (pos == -1)
			{
				return "";	//デフォルト値がない
			}

			int len = (val.Length-1) - (pos + 1);
			if ((len < 1) || ((pos + 1 + len) >= val.Length))	//|だけのとき
			{
				return "";	//デフォルト値がない
			}
			return val.Substring(pos + 1, len);
		}

		/// <summary>
		/// マクロコメントを解析し該当するマクロにセットする
		/// </summary>
		/// <param name="macro">セットするマクロ</param>
		/// <param name="line">マクロコメント行文字列</param>
		private void parseMAcroCommentLine(ref KagMacro macro, string line)
		{
			if (line == null || line.StartsWith(";;") == false)
			{
				return;	//マクロコメントではない
			}
			line = line.Remove(0, 2);	//";;"を削除する

			int pos = line.IndexOf('=');
			if (pos != -1)	//見つかったとき
			{
				KagMacroAttr attr = macro.GetMacroAttr(line.Substring(0, pos));
				if (attr != null)	//属性が見つかったとき
				{
					if (pos + 1 < line.Length)	//=の後ろに文字があるとき
					{
						int commaPos = line.IndexOf(',', pos + 1);
						if (commaPos != -1)
						{
							attr.Comment = line.Substring(pos + 1, commaPos - (pos + 1)).Replace("\\n", "\n");	//属性コメントセット
							if (commaPos + 1 < line.Length)	//コンマの後ろに文字があるとき
							{
								attr.ValueType = line.Substring(commaPos + 1).Trim();			//属性値タイプセット
							}
							//Debug.WriteLine("属性コメント=" + attr.Comment);
							//Debug.WriteLine("属性値タイプ=" + attr.ValueType);
						}
						else
						{
							attr.Comment = line.Substring(pos + 1).Trim().Replace("\\n", "\n");		//属性コメントセット
							//Debug.WriteLine("属性コメント=" + attr.Comment);
						}

						return;		//属性コメントが取得できたので終了
					}
				}
			}

			//属性コメントではないときはそのままタグコメントとして格納する
			macro.Comment += line.Replace("\\n", "\n") +"\r\n";
			//Debug.WriteLine("マクロコメント=" + line);
			return;
		}
		#endregion
	}
}
