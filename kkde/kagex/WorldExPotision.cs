using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace kkde.kagex
{
	/// <summary>
	/// ワールド拡張で使用する位置を表すクラス（初期化指定・相対指定を行えるクラス）
	/// </summary>
	public class WorldExPotision
	{
		string m_toPos = "";
		string m_fromPos = "";
		bool m_isPercent = false;
		bool m_isRelative = false;

		/// <summary>
		/// 位置
		/// </summary>
		[Description("位置")]
		public string ToPos
		{
			get { return m_toPos; }
			set { m_toPos = value; }
		}

		/// <summary>
		/// 初期位置
		/// </summary>
		[Description("初期位置")]
		public string FromPos
		{
			get { return m_fromPos; }
			set { m_fromPos = value; }
		}

		/// <summary>
		/// パーセント値かどうか
		/// </summary>
		[Description("パーセントかどうか")]
		public bool IsPercent
		{
			get { return m_isPercent; }
			set { m_isPercent = value; }
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
			string percent = "";
			if (m_isPercent)
			{
				percent = "%";
			}
			string relative = "";
			if (m_isRelative)
			{
				relative = "@";
			}

			string str = "";
			if (m_fromPos == "")
			{
				str = relative + m_toPos + percent;
			}
			else
			{
				str = relative + m_toPos + percent + ":" + relative + m_fromPos + percent;
			}

			return str;
		}
	}
}
