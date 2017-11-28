using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace kkde.kagex
{
	/// <summary>
	/// ワールド拡張属性の立ち絵用
	/// </summary>
	public class CharWorldExAttrType : BaseWorldExAttrType
	{
		#region フィールド
		//立ち絵専用属性
		private string m_pos;
		private string m_level;
		private string m_stime;
		#endregion

		#region 操作メソッド
		/// <summary>
		/// 属性をすべて初期化する
		/// </summary>
		public override void ResetAttr()
		{
			base.ResetAttr();

			m_pos = "";
			m_level = "";
			m_stime = "";
		}

		/// <summary>
		/// オブジェクト内容を全て初期化する
		/// </summary>
		public override void ResetAll()
		{
			base.ResetAll();

			ResetAttr();
		}

		/// <summary>
		/// オブジェクトの表示名
		/// </summary>
		/// <returns>オブジェクト名（表示用）</returns>
		public override string ToString()
		{
			return "立ち絵: " + Name;
		}

		/// <summary>
		/// KAG属性文字列を返す
		/// </summary>
		/// <returns>KAG属性文字列</returns>
		public override string ToKagTagAttr()
		{
			string attr = "";
			if (m_pos != "")
			{
				attr += String.Format("pos=\"{0}\" ", m_pos);
			}
			if (m_stime != "")
			{
				attr += String.Format("stime=\"{0}\" ", m_stime);
			}
			if (m_level != "")
			{
				attr += String.Format("level=\"{0}\" ", m_level);
			}

			return base.ToKagTagAttr() + attr;
		}
		#endregion

		[Category("位置")]
		[Description("立ち絵を表示する位置を指定します")]
		[TypeConverter(typeof(PropertyGridWorldExPosConverter))]
		public string Pos
		{
			get { return m_pos; }
			set { m_pos = value; }
		}

		[Category("時間")]
		[Description("舞台の時間名を指定します")]
		[TypeConverter(typeof(PropertyGridWorldExStimeConverter))]
		public string Stime
		{
			get { return m_stime; }
			set { m_stime = value; }
		}

		[Category("位置")]
		[Description("立ち絵を表示するレベル（前後）を指定します")]
		[TypeConverter(typeof(PropertyGridWorldExLevelConverter))]
		public string Level
		{
			get { return m_level; }
			set { m_level = value; }
		}

	}
}
