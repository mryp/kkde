using System;
using System.Collections.Generic;
using System.Text;

namespace kkde.search
{
	/// <summary>
	/// 検索結果を保持するクラス
	/// </summary>
	public class EditorSearchResult
	{
		int m_offset;
		int m_length;

		/// <summary>
		/// 検索結果があるかどうか
		/// trueのとき見つかった、falseの時は見つからなかった
		/// </summary>
		public bool IsHit
		{
			get
			{
				if (m_offset == -1)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		}

		/// <summary>
		/// 検索結果の位置
		/// </summary>
		public int Offset
		{
			get { return m_offset; }
			set { m_offset = value; }
		}
		
		/// <summary>
		/// 検索結果見つかったサイズ
		/// </summary>
		public int Length
		{
			get { return m_length; }
			set { m_length = value; }
		}

		public EditorSearchResult()
		{
			m_offset = -1;
			m_length = 0;
		}
	}
}
