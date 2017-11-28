using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace kkde.kagex
{
	/// <summary>
	/// Cos波振動アクションハンドラ情報
	/// </summary>
	public class CosActionInfo : BaseActionInfo
	{
		WorldExRelativePotision m_vibration = new WorldExRelativePotision();
		WorldExRelativePotision m_offset = new WorldExRelativePotision();
		string m_cycle = "";
		string m_angvel = "";

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

		[Category("動作")]
		[Description("周期（ms）")]
		public string Cycle
		{
			get { return m_cycle; }
			set { m_cycle = value; }
		}

		[Category("動作")]
		[Description("角速度（度/sec）\ncycleを指定しているときは無効")]
		public string Angvel
		{
			get { return m_angvel; }
			set { m_angvel = value; }
		}

		/// <summary>
		/// アクションハンドラ名を返す
		/// </summary>
		/// <returns>アクションハンドラ名</returns>
		public override string GetHandlerName()
		{
			return ActionHandlerManager.HANDLER_NAME_COS;
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
			if (m_cycle != "")
			{
				code += getParamCode("cycle", m_cycle);
			}
			if (m_angvel != "")
			{
				code += getParamCode("angvel", m_angvel);
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
