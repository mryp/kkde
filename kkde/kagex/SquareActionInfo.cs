using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace kkde.kagex
{
	/// <summary>
	/// 矩形波振動アクションハンドラ情報
	/// </summary>
	public class SquareActionInfo : BaseActionInfo
	{
		WorldExRelativePotision m_vibration = new WorldExRelativePotision();
		WorldExRelativePotision m_offset = new WorldExRelativePotision();
		string m_ontime = "";
		string m_offtime = "";

		[Category("位置")]
		[Description("振動量\n相対指定有効")]
		[TypeConverter(typeof(WorldExRelativePotisionObjectConverter))]
		public WorldExRelativePotision Vibration
		{
			get { return m_vibration; }
			set { m_vibration = value; }
		}

		[Category("位置")]
		[Description("補正値\n相対指定有効")]
		[TypeConverter(typeof(WorldExRelativePotisionObjectConverter))]
		public WorldExRelativePotision Offset
		{
			get { return m_offset; }
			set { m_offset = value; }
		}

		[Category("時間")]
		[Description("ON状態になっている時間")]
		public string Ontime
		{
			get { return m_ontime; }
			set { m_ontime = value; }
		}

		[Category("時間")]
		[Description("OFF状態になっている時間")]
		public string Offtime
		{
			get { return m_offtime; }
			set { m_offtime = value; }
		}

		/// <summary>
		/// アクションハンドラ名を返す
		/// </summary>
		/// <returns>アクションハンドラ名</returns>
		public override string GetHandlerName()
		{
			return ActionHandlerManager.HANDLER_NAME_SQUARE;
		}

		/// <summary>
		/// envinit構文のコードを返す
		/// </summary>
		/// <returns>コード</returns>
		public override string GetCode()
		{
			string code = "";
			code += String.Format("%[ handler:\"{0}\"", this.GetHandlerName());
			if (m_vibration.ToString() != "")
			{
				code += getParamCode("vibration", m_vibration);
			}
			if (m_offset.ToString() != "")
			{
				code += getParamCode("offset", m_offset);
			}
			if (m_ontime != "")
			{
				code += getParamCode("ontime", m_ontime);
			}
			if (m_offtime != "")
			{
				code += getParamCode("offtime", m_offtime);
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
