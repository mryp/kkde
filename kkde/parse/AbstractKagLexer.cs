using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace kkde.parse
{
	/// <summary>
	/// 字句解析抽象クラス
	/// </summary>
	public abstract class AbstractKagLexer : AbstractLexer
	{
		#region メソッド
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="reader"></param>
		protected AbstractKagLexer(TextReader reader)
			: base(reader)
		{
		}

		/// <summary>
		/// タグ名を取得する
		/// </summary>
		/// <returns>タグ名</returns>
		protected string ReadKagTagName()
		{
			char c;
			int nextChar;
			bool end = false;
			m_sb.Length = 0;
			while ((nextChar = this.ReaderRead()) != -1)
			{
				c = (char)nextChar;
				switch (c)
				{
					case '\t':
					case ' ':
					case '[':
					case ']':
						//タグ終了
						end = true;
						break;
					case '\n':
					case '\r':
						//改行してタグ終了
						HandleLineEnd(c);
						end = true;
						break;
					default:
						end = false;
						break;
				}

				if (end)
				{
					break;	//終了
				}
				m_sb.Append(c);
			}

			//Debug.WriteLine("readTagName tagName=" + m_tagName.ToString() + " cur=" + ((char)this.ReaderCurrent()).ToString());
			return m_sb.ToString();
		}
		#endregion
	}
}
