using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace kkde.parse
{
	public class KagLexer : AbstractKagLexer
	{
		Token m_token = null;
		StringBuilder m_endText = new StringBuilder(1024);
		StringBuilder m_comment = new StringBuilder(512);

		#region メソッド
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="reader"></param>
		public KagLexer(TextReader reader)
			: base(reader)
		{
		}
		
		/// <summary>
		/// 現在のトークンを返す
		/// </summary>
		/// <returns></returns>
		public Token GetToken()
		{
			return m_token;
		}

		/// <summary>
		/// 次のトークンを読み込み返す
		/// </summary>
		/// <returns></returns>
		public Token NextToken()
		{
			int nextChar;
			char c;
			int tempLine, tempCol;
			int preCommentLine = -1;
			string tagName = "";
			string comment = "";
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
					case ';':		//コメント
						if (IsKagLineHead(c) == false)
						{
							break;	//コメントではない
						}
						if (this.ReaderPeek() !=  ';')
						{
							break;	//マクロコメントではない
						}

						if (Line != preCommentLine + 1)
						{
							m_comment.Length = 0;	//次の行ではないので初期化する
						}
						preCommentLine = Line;
						comment = String.Format("{0}{1}", c, this.ReadToEndOfLine());
						if (comment.StartsWith(";;region"))
						{
							//折りたたみ範囲開始記号
							return new Token(KagTokens.StartRegion, Line, Col, comment, "");
						}
						else if (comment.EndsWith(";;endregion"))
						{
							//折りたたみ範囲終了記号
							return new Token(KagTokens.EndRegion, Line, Col, comment, "");
						}
						else
						{
							m_comment.AppendFormat("{0}\n", comment);
						}
						break;
					case '*':		//ラベル
						if (IsKagLineHead(c) == false)
						{
							break;
						}

						//-1は'*'の分
						return new Token(KagTokens.Label, Line, Col - 1, c.ToString() + this.ReadToEndOfLine(), "");
					case '@':		//タグ
					case '[':
						if (IsKagLineHead(c) == false && c == '@')
						{
							break;	//'@'の時だけ行頭必須なので処理を飛ばす
						}
						tempLine = this.Line;
						tempCol = this.Col - 1;

						tagName = ReadKagTagName();
						if (tagName == "macro")
						{
							string text = readEndTagString(String.Format("{0}{1}{2}", c, tagName, (char)this.ReaderCurrent()), "endmacro");
							if (text == "")
							{
								break;	//閉じられていないので何もしない
							}

							comment = "";
							if (tempLine == preCommentLine + 1)
							{
								//このマクロ用のマクロコメントがあるときはセットする
								comment = m_comment.ToString();
							}
							return new Token(KagTokens.Macro, tempLine, tempCol, text, comment);
						}
						else if (tagName == "iscript")
						{
							//TJSスクリプトは未実装
						}
						break;
					default:		//そのほかの文字
						break;
				}
			}

			return null;
		}

		/// <summary>
		/// endTagNameまでを検索し文字列として返す
		/// endTagNameが見つからなかったときは空文字を返す
		/// </summary>
		/// <param name="preText">返す文字列に含めるプレフィクス</param>
		/// <param name="endTagName">検索するタグ名</param>
		/// <returns>現在位置からのendTagNameを含む文字列</returns>
		private string readEndTagString(string preText, string endTagName)
		{
			//前処理
			m_endText.Length = 0;
			m_endText.Append(preText);

			char c;
			int nextChar;
			bool end = false;
			string tagName = "";
			while ((nextChar = this.ReaderRead()) != -1)
			{
				c = (char)nextChar;
				switch (c)
				{
					case '@':		//タグ
					case '[':
						if (IsKagLineHead(c) == false && c == '@')
						{
							break;	//'@'の時だけ行頭必須なので処理を飛ばす
						}

						tagName = ReadKagTagName();
						m_endText.AppendFormat("{0}{1}{2}", c, tagName, (char)this.ReaderCurrent());
						if (tagName != endTagName)
						{
							continue;	//次へ
						}

						end = true;		//正常終了
						break;
					case '\n':
					case '\r':
						//改行してタグ終了
						HandleLineEnd(c);
						break;
				}

				if (end)
				{
					break;	//ループ終了
				}
				m_endText.Append(c);
			}

			if (end == false)
			{
				return "";	//タグが見つからなかったときはエラー
			}
			return m_endText.ToString();
		}
		#endregion
	}
}
