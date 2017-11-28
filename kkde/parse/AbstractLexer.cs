using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace kkde.parse
{
	/// <summary>
	/// 字句解析抽象クラス
	/// </summary>
	public abstract class AbstractLexer
	{
		TextReader m_reader;
		int m_current = -1;
		int m_col = 0;
		int m_line = 0;
		protected StringBuilder m_sb = new StringBuilder(1024);
		protected int m_lineHeadTabNum = 0;

		/// <summary>
		/// 現在の列番号（０～）
		/// </summary>
		public int Col
		{
			get { return m_col; }
		}

		/// <summary>
		/// 現在の行番号（０～）
		/// </summary>
		public int Line
		{
			get { return m_line; }
		}

		/// <summary>
		/// 現在の位置を読み込み次へ移動する
		/// </summary>
		/// <returns></returns>
		protected int ReaderRead()
		{
			++m_col;
			m_current = m_reader.Read();
			return m_current;
		}

		/// <summary>
		/// 位置を移動せずに次の値を返す
		/// </summary>
		/// <returns></returns>
		protected int ReaderPeek()
		{
			return m_reader.Peek();
		}

		/// <summary>
		/// 位置を移動せずに現在の値を返す
		/// </summary>
		/// <returns></returns>
		protected int ReaderCurrent()
		{
			return m_current;
		}

		#region メソッド
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="reader"></param>
		protected AbstractLexer(TextReader reader)
		{
			m_reader = reader;
		}

		/// <summary>
		/// 行末かどうかを返す
		/// </summary>
		/// <param name="ch"></param>
		/// <returns></returns>
		protected bool WasLineEnd(char ch)
		{
			// Handle MS-DOS or MacOS line ends.
			if (ch == '\r')
			{
				if (m_reader.Peek() == '\n')
				{ // MS-DOS line end '\r\n'
					ch = (char)ReaderRead();
				}
				else
				{ // assume MacOS line end which is '\r'
					ch = '\n';
				}
			}

			return ch == '\n';
		}

		/// <summary>
		/// 行末かどうかチェックして返す
		/// 行末だったときは進める
		/// </summary>
		/// <param name="ch"></param>
		/// <returns></returns>
		protected bool HandleLineEnd(char ch)
		{
			if (WasLineEnd(ch))
			{
				++m_line;
				m_col = 0;
				m_lineHeadTabNum = 0;
				return true;
			}
			return false;
		}

		/// <summary>
		/// 行末までスキップする
		/// </summary>
		protected void SkipToEndOfLine()
		{
			int nextChar;
			while ((nextChar = m_reader.Read()) != -1)
			{
				if (HandleLineEnd((char)nextChar))
				{
					break;
				}
			}
		}

		/// <summary>
		/// 行末まで読み込んで返す
		/// </summary>
		/// <returns></returns>
		protected string ReadToEndOfLine()
		{
			m_sb.Length = 0;
			int nextChar;
			while ((nextChar = m_reader.Read()) != -1)
			{
				char ch = (char)nextChar;
				m_current = ch;

				// Return read string, if EOL is reached
				if (HandleLineEnd(ch))
				{
					return m_sb.ToString();
				}

				m_sb.Append(ch);
			}

			// Got EOF before EOL
			string retStr = m_sb.ToString();
			m_col += retStr.Length;
			return retStr;
		}

		/// <summary>
		/// 現在の位置が行頭かどうか
		/// </summary>
		/// <param name="c"></param>
		/// <returns>現在位置が行頭の時true</returns>
		protected bool IsKagLineHead(char c)
		{
			if (Col == 1)
			{
				return true;	//行頭
			}
			if (Col == m_lineHeadTabNum + 1)
			{
				return true;	//先頭にはタブしかない
			}

			return false;	//行頭ではない
		}
		#endregion
	}
}
