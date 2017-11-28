using System;
using System.Collections.Generic;
using System.Text;

namespace kkde.parse
{
	public class TjsBaseLexer
	{
		int m_pos = 0;
		int m_col = 0;
		int m_line = 0;
		string m_text;
		char m_pre;

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
		/// コンストラクタ
		/// </summary>
		/// <param name="text"></param>
		public TjsBaseLexer(string text)
		{
			m_text = text;
		}

		/// <summary>
		/// 現在の位置を読み込み次へ移動する
		/// </summary>
		/// <returns></returns>
		protected char GetNext()
		{
			if (Eos())
			{
				return '\0';
			}
			++m_col;

			m_pre = m_text[m_pos];
			return m_text[m_pos++];
		}

		/// <summary>
		/// 位置を移動せずに次の値を返す
		/// </summary>
		/// <returns></returns>
		protected char Peek()
		{
			if (Eos())
			{
				return '\0';
			}
			return m_text[m_pos];
		}

		/// <summary>
		/// 位置を移動せずに現在の値を返す
		/// </summary>
		/// <returns></returns>
		protected char ReaderCurrent()
		{
			return m_pre;
		}

		/// <summary>
		/// End Of String かどうか
		/// </summary>
		/// <returns></returns>
		public bool Eos()
		{
			return m_pos >= m_text.Length;
		}

		/// <summary>
		/// 一つ前に戻る
		/// </summary>
		public void UnGet()
		{
			UnGet(1);
		}

		/// <summary>
		/// 指定個数分だけ戻る
		/// </summary>
		/// <param name="num">戻る個数</param>
		public void UnGet(int num)
		{
			if ((m_col - num) < 0)
			{
				m_line--;
				m_col = 0;
			}
			else
			{
				m_col = m_col - num;
			}
			m_pos = Math.Max(0, m_pos - num);
			int prePos = Math.Max(0, m_pos - num-1);
			m_pre = m_text[prePos];
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
				if (Peek() == '\n')
				{ // MS-DOS line end '\r\n'
					ch = GetNext();
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
				return true;
			}
			return false;
		}

		/// <summary>
		/// 行末までスキップする
		/// </summary>
		protected void SkipToEndOfLine()
		{
			char c;
			while (!Eos())
			{
				c = GetNext();
				if (HandleLineEnd(c))
				{
					break;
				}
			}
		}
	}
}
