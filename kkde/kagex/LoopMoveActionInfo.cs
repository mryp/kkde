using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace kkde.kagex
{
	/// <summary>
	/// ループ動作を伴う移動アクション情報ハンドラ
	/// </summary>
	public class LoopMoveActionInfo : BaseActionInfo
	{
		WorldExRelativePotision m_min = new WorldExRelativePotision();
		WorldExRelativePotision m_max = new WorldExRelativePotision();
		string m_loop = "";

		[Category("位置")]
		[Description("最小値\n相対指定有効")]
		[TypeConverter(typeof(WorldExRelativePotisionObjectConverter))]
		public WorldExRelativePotision Min
		{
			get { return m_min; }
			set { m_min = value; }
		}

		[Category("位置")]
		[Description("最大値\n相対指定有効")]
		[TypeConverter(typeof(WorldExRelativePotisionObjectConverter))]
		public WorldExRelativePotision Max
		{
			get { return m_max; }
			set { m_max = value; }
		}

		[Category("時間")]
		[Description("ループ時間")]
		public string Loop
		{
			get { return m_loop; }
			set { m_loop = value; }
		}

		/// <summary>
		/// アクションハンドラ名を返す
		/// </summary>
		/// <returns>アクションハンドラ名</returns>
		public override string GetHandlerName()
		{
			return ActionHandlerManager.HANDLER_NAME_LOOP;
		}

		/// <summary>
		/// envinit構文のコードを返す
		/// </summary>
		/// <returns>コード</returns>
		public override string GetCode()
		{
			string code = "";
			code += String.Format("%[ handler:\"{0}\"", this.GetHandlerName());
			if (m_min.ToString() != "")
			{
				code += getParamCode("min", m_min);
			}
			if (m_max.ToString() != "")
			{
				code += getParamCode("max", m_max);
			}
			if (m_loop != "")
			{
				code += getParamCode("loop", m_loop);
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
