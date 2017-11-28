using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace kkde.parse
{
	class KagMacroLexer : AbstractKagLexer, ILexer
	{
		Token m_token = null;
		Dictionary<string, string> m_attrList = new Dictionary<string, string>();

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="reader"></param>
		public KagMacroLexer(TextReader reader)
			: base(reader)
		{
		}

		public Token GetToken()
		{
			return m_token;
		}

		public Token NextToken()
		{
			int nextChar;
			char c;
			string tagName = "";
			while ((nextChar = this.ReaderRead()) != -1)
			{
				c = (char)nextChar;
				switch (c)
				{
					case ' ':		//空白
						break;
					case '\t':
						m_lineHeadTabNum++;	//タブの数を増やす
						break;
					case '\r':		//改行
					case '\n':
						this.HandleLineEnd(c);
						break;
					case '*':		//ラベル
					case ';':		//コメント
						if (IsKagLineHead(c) == false)
						{
							break;	//行頭ではないので処理を続ける
						}

						SkipToEndOfLine();	//この行は無視する
						break;
					case '@':		//タグ
					case '[':
						if (IsKagLineHead(c) == false && c == '@')
						{
							break;	//'@'の時だけ行頭必須なので処理を飛ばす
						}

						tagName = ReadKagTagName();
						c = (char)ReaderCurrent();
						if (c == ']' || c == '\n' || c == '\r')
						{
							break;	//属性が無いので終了
						}

						return new Token(KagTokens.MacroAttr, -1, -1, tagName, "", readKagAttr());
					default:		//そのほかの文字
						break;
				}
			}

			return null;
		}

		private Dictionary<string, string> readKagAttr()
		{
			m_sb.Length = 0;
			m_attrList.Clear();

			bool strMode = false;		//trueのとき"文字列中、falseのとき"文字列ではない
			bool attrNameMode = true;	//trueのとき属性名読み込み中、falseのとき属性値読み込み中
			char c;
			int nextChar;
			bool end = false;
			string attrName = "";
			while ((nextChar = this.ReaderRead()) != -1)
			{
				c = (char)nextChar;
				switch (c)
				{
					case ']':
						end = true;	//タグ終了
						break;	
					case '\n':
					case '\r':
						HandleLineEnd(c);
						end = true;	//タグ終了
						break;
					case ' ':
						if (strMode)
						{
							//文字列モードの時は空白も含める
							m_sb.Append(c);
						}
						else
						{
							if (attrNameMode == true)	//属性名の時
							{
								if (m_sb.Length != 0)
								{
									//属性名をセット
									attrName = m_sb.ToString();
									m_attrList[attrName] = "";
									m_sb.Length = 0;
								}
							}
							else                        //属性値の時
							{
								if (attrName != "")
								{
									//属性値をセット
									m_attrList[attrName] = m_sb.ToString();
								}
								//初期化
								attrNameMode = true;
								m_sb.Length = 0;
								attrName = "";
							}
						}
						break;
					case '*':
						if (strMode == false)
						{
							m_attrList[c.ToString()] = "";
						}
						break;
					case '"':
						strMode = !strMode;
						break;
					case '\\':
						m_sb.Append(c);
						if (strMode == true && this.ReaderPeek() == '"')
						{
							//\" とあるときは"文字としてエスケープ
							m_sb.Append((char)this.ReaderRead());
						}
						break;
					case '=':
						if (m_sb.Length != 0)
						{
							//属性名をセット
							attrName = m_sb.ToString();
							m_attrList[attrName] = "";
							m_sb.Length = 0;
						}
						attrNameMode = false;
						break;
					default:
						m_sb.Append(c);
						break;
				}

				if (end)
				{
					//最後に残っている物を格納する
					if (attrNameMode == true)	//属性名の時
					{
						if (m_sb.Length != 0)
						{
							//属性名をセット
							attrName = m_sb.ToString();
							m_attrList[attrName] = "";
						}
					}
					else                        //属性値の時
					{
						if (attrName != "")
						{
							//属性値をセット
							m_attrList[attrName] = m_sb.ToString();
						}
					}
					break;	//ループ終了
				}
			}

			return m_attrList;
		}
	}
}
