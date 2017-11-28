using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace kkde.kagex
{
	/// <summary>
	/// 落下運動のアクションハンドラ情報
	/// </summary>
	public class FallActionInfo : BaseActionInfo
	{
		string m_distance = "";

		[Category("時間")]
		[Description("落下量")]
		public string Distance
		{
			get { return m_distance; }
			set { m_distance = value; }
		}

		/// <summary>
		/// アクションハンドラ名を返す
		/// </summary>
		/// <returns>アクションハンドラ名</returns>
		public override string GetHandlerName()
		{
			return ActionHandlerManager.HANDLER_NAME_FALL;
		}

		/// <summary>
		/// envinit構文のコードを返す
		/// </summary>
		/// <returns>コード</returns>
		public override string GetCode()
		{
			string code = "";
			code += String.Format("%[ handler:\"{0}\"", this.GetHandlerName());
			if (m_distance != "")
			{
				code += getParamCode("distance", m_distance);
			}
			if (Time != "")
			{
				code += getParamCode("time", Time);
			}
			if (Delay != "")
			{
				code += getParamCode("delay", Delay);
			}

			code += " ]";
			return code;
		}
	}
}
