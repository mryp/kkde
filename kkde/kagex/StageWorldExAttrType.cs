using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace kkde.kagex
{
	/// <summary>
	/// ワールド拡張属性の背景絵用
	/// </summary>
	public class StageWorldExAttrType : BaseWorldExAttrType
	{
		#region フィールド
		private string m_stime;
		#endregion

		#region
		[Category("時間")]
		[Description("舞台の時間名を指定します")]
		[TypeConverter(typeof(PropertyGridWorldExStimeConverter))]
		public string Stime
		{
			get { return m_stime; }
			set { m_stime = value; }
		}
		#endregion

		#region 操作メソッド
		/// <summary>
		/// 属性をすべて初期化する
		/// </summary>
		public override void ResetAttr()
		{
			base.ResetAttr();

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
		/// オブジェクトの表示名を返す
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return "背景絵: " + Name;
		}

		/// <summary>
		/// KAG属性文字列を返す
		/// </summary>
		/// <returns></returns>
		public override string ToKagTagAttr()
		{
			string attr = "";
			if (m_stime != "")
			{
				attr += String.Format("stime=\"{0}\" ", m_stime);
			}

			return base.ToKagTagAttr() + attr;
		}
		#endregion
	}
}
