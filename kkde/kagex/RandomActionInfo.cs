using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace kkde.kagex
{
	/// <summary>
	/// ランダム振動アクションハンドラ情報
	/// </summary>
	public class RandomActionInfo : BaseActionInfo
	{
		WorldExRelativePotision m_vibration = new WorldExRelativePotision();
		WorldExRelativePotision m_offset = new WorldExRelativePotision();
		string m_waittime = "";

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
		[Description("振動間の待ち時間")]
		public string WaitTime
		{
			get { return m_waittime; }
			set { m_waittime = value; }
		}

		/// <summary>
		/// アクションハンドラ名を返す
		/// </summary>
		/// <returns>アクションハンドラ名</returns>
		public override string GetHandlerName()
		{
			return ActionHandlerManager.HANDLER_NAME_RANDOM;
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
			if (m_waittime != "")
			{
				code += getParamCode("waittime", m_waittime);
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
