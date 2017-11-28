using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace kkde.kagex
{
	/// <summary>
	/// 基本移動アクションハンドラ情報
	/// </summary>
	public class MoveActionInfo : BaseActionInfo
	{
		WorldExRelativePotision m_start = new WorldExRelativePotision();
		WorldExRelativePotision m_value = new WorldExRelativePotision();
		string m_accel = "";

		[Category("位置")]
		[Description("初期値\n相対指定有効")]
		[TypeConverter(typeof(WorldExRelativePotisionObjectConverter))]
		public WorldExRelativePotision Start
		{
			get { return m_start; }
			set { m_start = value; }
		}

		[Category("位置")]
		[Description("終了値\n相対指定有効")]
		[TypeConverter(typeof(WorldExRelativePotisionObjectConverter))]
		public WorldExRelativePotision Value
		{
			get { return m_value; }
			set { m_value = value; }
		}

		[Category("動作")]
		[Description("アクション時の加減速を指定します（-1, 0, 1）\n")]
		[TypeConverter(typeof(PropertyGridAccelConverter))]
		public string Accel
		{
			get { return m_accel; }
			set { m_accel = value; }
		}

		/// <summary>
		/// アクションハンドラ名を返す
		/// </summary>
		/// <returns>アクションハンドラ名</returns>
		public override string GetHandlerName()
		{
			return ActionHandlerManager.HANDLER_NAME_MOVE;
		}

		/// <summary>
		/// envinit構文のコードを返す
		/// </summary>
		/// <returns>コード</returns>
		public override string GetCode()
		{
			string code = "";
			code += String.Format("%[ handler:\"{0}\"", this.GetHandlerName());
			if (m_start.ToString() != "")
			{
				code += getParamCode("start", m_start);
			}
			if (m_value.ToString() != "")
			{
				code += getParamCode("value", m_value);
			}
			if (m_accel != "")
			{
				code += getParamCode("accel", m_accel);
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
