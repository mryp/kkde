using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace kkde.parse.kagex
{
	/// <summary>
	/// envinit.tjsの字句解析クラス
	/// </summary>
	public class KagexEnvinitLexer : TjsBaseLexer, yyParser.yyInput
	{
		#region フィールド
		int m_token;
		object m_value;
		bool m_regexFlag;
		private Dictionary<string, int> m_reservedTable = new Dictionary<string, int>();
		#endregion

		#region メソッド
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="reader"></param>
		public KagexEnvinitLexer(String text)
			: base(text)
		{
			m_token = 0;
			m_value = null;
			m_regexFlag = false;
			initReservedTable();
		}

		/// <summary>
		/// 正規表現フラグを立てる（パーサーからセットする）
		/// </summary>
		public void SetOnRegexFlag()
		{
			m_regexFlag = true;
		}

		/// <summary>
		/// 予約後テーブルを初期化する
		/// </summary>
		private void initReservedTable()
		{
			m_reservedTable.Add("new", kagex.Token.T_NEW);
			m_reservedTable.Add("delete", kagex.Token.T_DELETE);
			m_reservedTable.Add("typeof", kagex.Token.T_TYPEOF);
			m_reservedTable.Add("isvalid", kagex.Token.T_ISVALID);
			m_reservedTable.Add("invalidate", kagex.Token.T_INVALIDATE);
			m_reservedTable.Add("instanceof", kagex.Token.T_INSTANCEOF);
			m_reservedTable.Add("this", kagex.Token.T_THIS);
			m_reservedTable.Add("super", kagex.Token.T_SUPER);
			m_reservedTable.Add("global", kagex.Token.T_GLOBAL);
			m_reservedTable.Add("class", kagex.Token.T_CLASS);
			m_reservedTable.Add("continue", kagex.Token.T_CONTINUE);
			m_reservedTable.Add("function", kagex.Token.T_FUNCTION);
			m_reservedTable.Add("debugger", kagex.Token.T_DEBUGGER);
			m_reservedTable.Add("default", kagex.Token.T_DEFAULT);
			m_reservedTable.Add("case", kagex.Token.T_CASE);
			m_reservedTable.Add("extends", kagex.Token.T_EXTENDS);
			m_reservedTable.Add("finally", kagex.Token.T_FINALLY);
			m_reservedTable.Add("property", kagex.Token.T_PROPERTY);
			m_reservedTable.Add("private", kagex.Token.T_PRIVATE);
			m_reservedTable.Add("public", kagex.Token.T_PUBLIC);
			m_reservedTable.Add("protected", kagex.Token.T_PROTECTED);
			m_reservedTable.Add("static", kagex.Token.T_STATIC);
			m_reservedTable.Add("return", kagex.Token.T_RETURN);
			m_reservedTable.Add("break", kagex.Token.T_BREAK);
			m_reservedTable.Add("export", kagex.Token.T_EXPORT);
			m_reservedTable.Add("import", kagex.Token.T_IMPORT);
			m_reservedTable.Add("switch", kagex.Token.T_SWITCH);
			m_reservedTable.Add("in", kagex.Token.T_IN);
			m_reservedTable.Add("incontextof", kagex.Token.T_INCONTEXTOF);
			m_reservedTable.Add("for", kagex.Token.T_FOR);
			m_reservedTable.Add("while", kagex.Token.T_WHILE);
			m_reservedTable.Add("do", kagex.Token.T_DO);
			m_reservedTable.Add("if", kagex.Token.T_IF);
			m_reservedTable.Add("var", kagex.Token.T_VAR);
			m_reservedTable.Add("const", kagex.Token.T_CONST);
			m_reservedTable.Add("enum", kagex.Token.T_ENUM);
			m_reservedTable.Add("goto", kagex.Token.T_GOTO);
			m_reservedTable.Add("throw", kagex.Token.T_THROW);
			m_reservedTable.Add("try", kagex.Token.T_TRY);
			m_reservedTable.Add("setter", kagex.Token.T_SETTER);
			m_reservedTable.Add("getter", kagex.Token.T_GETTER);
			m_reservedTable.Add("else", kagex.Token.T_ELSE);
			m_reservedTable.Add("catch", kagex.Token.T_CATCH);
			m_reservedTable.Add("synchronized", kagex.Token.T_SYNCHRONIZED);
			m_reservedTable.Add("with", kagex.Token.T_WITH);
			m_reservedTable.Add("int", kagex.Token.T_INT);
			m_reservedTable.Add("real", kagex.Token.T_REAL);
			m_reservedTable.Add("string", kagex.Token.T_STRING);
			m_reservedTable.Add("octet", kagex.Token.T_OCTET);
			m_reservedTable.Add("false", kagex.Token.T_FALSE);
			m_reservedTable.Add("null", kagex.Token.T_NULL);
			m_reservedTable.Add("true", kagex.Token.T_TRUE);
			m_reservedTable.Add("void", kagex.Token.T_VOID);
			m_reservedTable.Add("NaN", kagex.Token.T_NAN);
			m_reservedTable.Add("Infinity", kagex.Token.T_INFINITY);
		}
		#endregion

		#region yyInput メンバ
		/// <summary>
		/// トークンを次に進める
		/// 進めたらtrueを返す
		/// </summary>
		/// <returns>進めるときはtrue</returns>
		public bool advance()
		{
			char c;
			while (!this.Eos())
			{
				c = this.GetNext();
				if (m_regexFlag)
				{
					//正規表現モードの時
					m_regexFlag = false;
					return true;
				}
				if (isSpace(c))
				{
					continue;
				}

				//Debug.WriteLine(String.Format("c=\"{0}\", line={1}, col={2}", c, Line, Col));
				switch (c)
				{
					//記号関連
					case '>':
						if (isMatchString(">>>="))
						{
							m_token = kagex.Token.T_RBITSHIFTEQUAL;
						}
						else if (isMatchString(">>>"))
						{
							m_token = kagex.Token.T_RBITSHIFT;
						}
						else if (isMatchString(">>="))
						{
							m_token = kagex.Token.T_RARITHSHIFTEQUAL;
						}
						else if (isMatchString(">>"))
						{
							m_token = kagex.Token.T_RARITHSHIFT;
						}
						else if (isMatchString(">="))
						{
							m_token = kagex.Token.T_GTOREQUAL;
						}
						else
						{
							m_token = kagex.Token.T_GT;
						}
						return true;
					case '<':
						if (isMatchString("<<="))
						{
							m_token = kagex.Token.T_LARITHSHIFTEQUAL;
						}
						else if (isMatchString("<->"))
						{
							m_token = kagex.Token.T_SWAP;
						}
						else if (isMatchString("<="))
						{
							m_token = kagex.Token.T_LTOREQUAL;
						}
						else if (isMatchString("<<"))
						{
							m_token = kagex.Token.T_LARITHSHIFT;
						}
						else
						{
							if (this.Peek() == '%')
							{
								ReadOctet(c);
								m_token = kagex.Token.T_CONSTVAL;	//オクテットの定義
								return true;
							}

							m_token = kagex.Token.T_LT;	//"<"
						}
						return true;
					case '=':
						if (isMatchString("==="))
						{
							m_token = kagex.Token.T_DISCEQUAL;
						}
						else if (isMatchString("=="))
						{
							m_token = kagex.Token.T_EQUALEQUAL;
						}
						else if (isMatchString("=>"))
						{
							m_token = kagex.Token.T_COMMA;	//"=>"は","と同等とする
						}
						else
						{
							m_token = kagex.Token.T_EQUAL;
						}
						return true;
					case '!':
						if (isMatchString("!=="))
						{
							m_token = kagex.Token.T_DISCNOTEQUAL;
						}
						else if (isMatchString("!="))
						{
							m_token = kagex.Token.T_NOTEQUAL;
						}
						else
						{
							m_token = kagex.Token.T_EXCRAMATION;
						}
						return true;
					case '&':
						if (isMatchString("&&="))
						{
							m_token = kagex.Token.T_LOGICALANDEQUAL;
						}
						else if (isMatchString("&&"))
						{
							m_token = kagex.Token.T_LOGICALAND;
						}
						else if (isMatchString("&="))
						{
							m_token = kagex.Token.T_AMPERSANDEQUAL;
						}
						else
						{
							m_token = kagex.Token.T_AMPERSAND;
						}
						return true;
					case '|':
						if (isMatchString("||="))
						{
							m_token = kagex.Token.T_LOGICALOREQUAL;
						}
						else if (isMatchString("||"))
						{
							m_token = kagex.Token.T_LOGICALOR;
						}
						else if (isMatchString("|="))
						{
							m_token = kagex.Token.T_VERTLINEEQUAL;
						}
						else
						{
							m_token = kagex.Token.T_VERTLINE;
						}
						return true;
					case '.':
						if (!this.Eos())
						{
							switch ((char)this.Peek())
							{
								case '0':
								case '1':
								case '2':
								case '3':
								case '4':
								case '5':
								case '6':
								case '7':
								case '8':
								case '9':
									//数字
									m_value = ReadNumber(c);
									m_token = kagex.Token.T_CONSTVAL;
									return true;
								default:
									//ここでは何もしない
									break;
							}
						}
						if (isMatchString("..."))
						{
							m_token = kagex.Token.T_OMIT;
						}
						else
						{
							m_token = kagex.Token.T_DOT;
						}
						return true;
					case '+':
						if (isMatchString("++"))
						{
							m_token = kagex.Token.T_INCREMENT;
						}
						else if (isMatchString("+="))
						{
							m_token = kagex.Token.T_PLUSEQUAL;
						}
						else
						{
							m_token = kagex.Token.T_PLUS;
						}
						return true;
					case '-':
						if (isMatchString("-="))
						{
							m_token = kagex.Token.T_MINUSEQUAL;
						}
						else if (isMatchString("--"))
						{
							m_token = kagex.Token.T_DECREMENT;
						}
						else
						{
							m_token = kagex.Token.T_MINUS;
						}
						return true;
					case '*':
						if (isMatchString("*="))
						{
							m_token = kagex.Token.T_ASTERISKEQUAL;
						}
						else
						{
							m_token = kagex.Token.T_ASTERISK;
						}
						return true;
					case '/':
						if (skipComment(c))
						{
							continue;	//コメントだったときはスキップする
						}
						
						if (isMatchString("/="))
						{
							m_token = kagex.Token.T_SLASHEQUAL;
						}
						else
						{
							m_token = kagex.Token.T_SLASH;
						}
						return true;
					case '\\':
						if (isMatchString("\\="))
						{
							m_token = kagex.Token.T_BACKSLASHEQUAL;
						}
						else
						{
							m_token = kagex.Token.T_BACKSLASH;
						}
						return true;
					case '%':
						if (isMatchString("%="))
						{
							m_token = kagex.Token.T_PERCENTEQUAL;
						}
						else
						{
							m_token = kagex.Token.T_PERCENT;
						}
						return true;
					case '^':
						if (isMatchString("^="))
						{
							m_token = kagex.Token.T_CHEVRONEQUAL;
						}
						else
						{
							m_token = kagex.Token.T_CHEVRON;
						}
						return true;
					case '[':
						m_token = kagex.Token.T_LBRACKET;
						return true;
					case ']':
						m_token = kagex.Token.T_RBRACKET;
						return true;
					case '(':
						m_token = kagex.Token.T_LPARENTHESIS;
						return true;
					case ')':
						m_token = kagex.Token.T_RPARENTHESIS;
						return true;
					case '~':
						m_token = kagex.Token.T_TILDE;
						return true;
					case '?':
						m_token = kagex.Token.T_QUESTION;
						return true;
					case ':':
						m_token = kagex.Token.T_COLON;
						return true;
					case ',':
						m_token = kagex.Token.T_COMMA;
						return true;
					case ';':
						m_token = kagex.Token.T_SEMICOLON;
						return true;
					case '{':
						m_token = kagex.Token.T_LBRACE;
						return true;
					case '}':
						m_token = kagex.Token.T_RBRACE;
						return true;
					case '#':
						m_token = kagex.Token.T_SHARP;
						return true;
					case '$':
						m_token = kagex.Token.T_DOLLAR;
						return true;
					case '\'':
					case '\"':
						//文字列処理
						m_value = ReadString(c);
						m_token = kagex.Token.T_CONSTVAL;
						//Debug.WriteLine(String.Format("ReadString=\"{0}\", line={1}, col={2}", m_value, Line, Col));
						return true;
					case '@':
						//プリプロセッサor文字列
						if (this.Eos() == true)
						{
							continue;	//終了
						}

						c = this.Peek();
						if (c == '\"' || c == '\'')
						{
							//文字列として処理する
							//本当は評価式として処理するがKKDEでは中身をみないため
							this.GetNext();
							m_value = ReadString(c);
							m_token = kagex.Token.T_CONSTVAL;
							return true;
						}

						//プリプロセッサ部分はコメント扱いで読み飛ばす
						SkipPreprosessor();
						continue;
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
						//数字
						m_value = ReadNumber(c);
						m_token = kagex.Token.T_CONSTVAL;
						return true;
					default:		//そのほかの文字
						//ここでは何もしない
						break;
				}

				//
				string symbol = readSymbol(c);
				int token;
				if (m_token == kagex.Token.T_DOT)
				{
					//前回取得したトークンが"."のときはかならずシンボルとする
					token = kagex.Token.T_SYMBOL;
				}
				else
				{
					token = getReservedToken(symbol);
				}
				switch (token)
				{
					case kagex.Token.T_FALSE:
					case kagex.Token.T_TRUE:
					case kagex.Token.T_NULL:
					case kagex.Token.T_NAN:
					case kagex.Token.T_INFINITY:
						//これらは定数としてセットする
						token = kagex.Token.T_CONSTVAL;
						break;
					default:
						break;
				}

				m_token = token;
				m_value = symbol;
				return true;
			}

			return false;
		}

		/// <summary>
		/// シンボル名から対応する予約語トークンを取得し返す
		/// </summary>
		/// <param name="symbol"></param>
		/// <returns></returns>
		private int getReservedToken(string symbol)
		{
			if (m_reservedTable.ContainsKey(symbol))
			{
				return m_reservedTable[symbol];
			}

			return kagex.Token.T_SYMBOL;	//予約後リストに見つからないときはシンボルを返す
		}

		/// <summary>
		/// シンボルとして読み込む
		/// </summary>
		/// <param name="ch"></param>
		/// <returns></returns>
		private string readSymbol(char ch)
		{
			StringBuilder s = new StringBuilder(ch.ToString());
			System.Globalization.UnicodeCategory uc; 
			while (!this.Eos())
			{
				ch = this.GetNext();
				uc = Char.GetUnicodeCategory(ch);
				if (System.Globalization.UnicodeCategory.OtherLetter == uc
					|| Char.IsLetterOrDigit(ch) || ch == '_')
				{
					s.Append(ch.ToString());
				}
				else
				{
					break;
				}
			}
			if (!this.Eos())
			{
				this.UnGet();
			}

			return s.ToString();
		}

		/// <summary>
		/// コメントをスキップする
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		private bool skipComment(char c)
		{
			if (c != '/')
			{
				return false;
			}

			if (!this.Eos())
			{
				c = this.GetNext();
				if (c == '/')
				{
					this.SkipToEndOfLine();	//行末まで読み捨て
					return true;
				}
				else if (c == '*')
				{
					skipToMultiComment();
					return true;
				}
			}

			return false;
		}

		private void skipToMultiComment()
		{
			char c;
			while (!this.Eos())
			{
				c = this.GetNext();
				if (c == '*')
				{
					if (this.Eos())
					{
						break;	//読み取るものがない
					}

					c = this.Peek();
					if (c == '/')
					{
						//コメント終了
						this.GetNext();	//読み捨てる
						break;
					}
				}
			}
		}

		/// <summary>
		/// プリプロセッサ部分を読み飛ばす
		/// </summary>
		private void SkipPreprosessor()
		{
			if (isMatchString("set"))
			{
				skipParenthesis();	//読み飛ばす
			}
			else if (isMatchString("if"))
			{
				skipParenthesis();	//読み飛ばす
			}
			else if (isMatchString("endif"))
			{
				//何もしない
			}

			return;
		}

		/// <summary>
		/// 括弧をスキップする
		/// </summary>
		void skipParenthesis()
		{
			char ch;
			int level = 0;
			while (!this.Eos())
			{
				ch = this.GetNext();
				
				if (ch == '(')
				{
					level++;
				}
				else if (ch == ')')
				{
					level--;
				}
				else if (isSpace(ch))
				{
					continue;
				}

				if (level == 0)
				{
					return;
				}
			}

			return;
		}

		/// <summary>
		/// 文字列を読み込み返す
		/// </summary>
		/// <param name="delim">" or '</param>
		/// <returns></returns>
		private string ReadString(char delim)
		{
			StringBuilder sb = new StringBuilder();

			char c;
			while (!this.Eos())
			{
				c = this.GetNext();
				if (c == '\\')
				{
					sb.Append(c);
					if (this.Eos() == true)
					{
						break;	//終わりがきているので終了
					}

					c = this.GetNext();
					sb.Append(c);	//エスケープ文字を取り込む
				}
				else if (c == delim)	//" or '
				{
					if (this.Eos() == true)
					{
						break;	//終了
					}

					skipSpace();
					if (this.Peek() == delim)
					{
						//"A" "B" となっていたときは"AB"とするため続ける
						c = this.GetNext();
						sb.Append(c);
					}
					else
					{
						break;	//文字列の終わりが指定されたので終了する
					}
				}
				else
				{
					sb.Append(c);	//文字列として追加
				}
			}

			return sb.ToString();
		}

		/// <summary>
		/// 数字を読み込む
		/// </summary>
		/// <param name="c"></param>
		private string ReadNumber(char c)
		{
			StringBuilder sb = new StringBuilder();

			if (c == '.')
			{
				//小数点
				sb.Append("0");
			}
			sb.Append(c);
			
			int cardinalNumber = 10;
			if (c == '0')
			{
				char mark = this.Peek();
				if (mark == '\0')
				{
					return sb.ToString();	//"0"だけ
				}
				else if (mark == 'X' || mark == 'x')	//16進数
				{
					cardinalNumber = 16;
					c = this.GetNext();
				}
				else if (mark == 'B' || mark == 'b')	//2進数
				{
					cardinalNumber = 2;
					c = this.GetNext();
				}
				else if (mark == 'E' || mark == 'e')	//exp
				{
					c = this.GetNext();
				}
				else if (mark == 'P' || mark == 'p')	//2^n exp
				{
					c = this.GetNext();
				}
				else if (mark == '.')	//小数点
				{
					c = this.GetNext();
				}
				else if (mark == '0' || mark == '1' || mark == '2' || mark == '3' || mark == '4' || mark == '5' || mark == '6' || mark == '7')
				{
					cardinalNumber = 8;
					c = this.GetNext();
				}
				else
				{
					return sb.ToString();	//"0"だけ
				}
				sb.Append(c);
			}

			bool end = false;
			while (!this.Eos())
			{
				c = this.GetNext();
				switch (c)
				{
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
						end = false;
						break;
					case 'A':
					case 'B':
					case 'C':
					case 'D':
					case 'E':
					case 'F':
					case 'a':
					case 'b':
					case 'c':
					case 'd':
					case 'e':
					case 'f':
						if (cardinalNumber != 16)
						{
							end = true;	//16進数ではないときは終了
						}
						break;
					default:
						end = true;
						break;
				}

				if (end)
				{
					//終了
					this.UnGet();
					break;
				}
				sb.Append(c);
			}
			return sb.ToString();
		}

		/// <summary>
		/// オクテットを読み込む
		/// </summary>
		/// <param name="c"></param>
		private void ReadOctet(char c)
		{
			//閉じ括弧が現れるまでを読み捨てる
			while (!this.Eos())
			{
				c = this.GetNext();
				if (isSpace(c))
				{
					continue;
				}
				else if (c == '%')
				{
					if (this.Peek() == '>')
					{
						this.GetNext();	//読み捨てる
						break;
					}
				}
			}
		}

		/// <summary>
		/// 空白部分を読み捨てる
		/// </summary>
		private void skipSpace()
		{
			while (!this.Eos())
			{
				if (isSpace(this.Peek()) == true)
				{
					this.GetNext();	//読み捨てる
				}
				else
				{
					break;//空白終了
				}
			}
		}

		/// <summary>
		/// 指定した文字が空白かどうか
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		private bool isSpace(char c)
		{
			switch (c)
			{
				//空白関連
				case ' ':		//空白
					return true;
				case '\t':
					return true;
				case '\r':		//改行
				case '\n':
					this.HandleLineEnd(c);
					return true;
				default:
					/* そのほかの文字の時 */
					break;
			}

			return false;	//空白ではない
		}

		/// <summary>
		/// 現在の位置から指定した文字と等しいかどうか
		/// </summary>
		/// <param name="str">比較したい文字列</param>
		/// <returns>等しいときtrue</returns>
		private bool isMatchString(string str)
		{
			int pos = 0;
			char c = this.ReaderCurrent();
			while ((pos < str.Length) && !this.Eos())
			{
				if (c != str[pos])
				{
					break;	//同じじゃないのを発見したので比較終了
				}
				c = this.GetNext();
				pos++;
			}
			if (pos != str.Length)
			{
				//同じではなかったみたいなので、進めた分を元に戻す
				this.UnGet(pos);
				return false;
			}
			else
			{
				this.UnGet();	//一個だけ戻る
			}

			return true;
		}

		/// <summary>
		/// 現在のトークンの種類を返す
		/// </summary>
		/// <returns>現在のトークンID</returns>
		public int token()
		{
			return m_token;
		}

		/// <summary>
		/// 現在のトークンの値を返す
		/// </summary>
		/// <returns>現在のトークンの値</returns>
		public object value()
		{
			return m_value;
		}
		#endregion
	}
}
