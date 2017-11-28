using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace kkde.kagex
{
	/// <summary>
	/// ワールド拡張で使用する相対位置を表すクラス（相対指定可）
	/// </summary>
	public class WorldExRelativePotision
	{
		string m_pos = "";
		bool m_isRelative = false;

		/// <summary>
		/// 位置
		/// </summary>
		[Description("位置")]
		public string Pos
		{
			get { return m_pos; }
			set { m_pos = value; }
		}

		/// <summary>
		/// 相対位置かどうか
		/// </summary>
		[Description("相対指定かどうか")]
		public bool IsRelative
		{
			get { return m_isRelative; }
			set { m_isRelative = value; }
		}

		/// <summary>
		/// 文字列表現を返す
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string relative = "";
			if (m_isRelative)
			{
				relative = "@";
			}

			return relative + m_pos;
		}
	}
}
