using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace kkde.kagex
{
	/// <summary>
	/// アクション情報を表すインターフェイス
	/// </summary>
	public class BaseActionInfo : IActionInfo
	{
		string m_time = "";
		string m_delay = "";

		[Category("時間")]
		[Description("アクションが駆動する時間を指定します")]
		public string Time
		{
			get { return m_time; }
			set { m_time = value; }
		}

		[Category("時間")]
		[Description("アクションが動作開始するまでの遅れる時間を指定します")]
		public string Delay
		{
			get { return m_delay; }
			set { m_delay = value; }
		}

		/// <summary>
		/// アクションハンドラ名を返す
		/// </summary>
		/// <returns>アクションハンドラ名</returns>
		public virtual string GetHandlerName()
		{
			return ActionHandlerManager.HANDLER_NAME_NULL;
		}

		/// <summary>
		/// envinit構文のコードを返す
		/// </summary>
		/// <returns>コード</returns>
		public virtual string GetCode()
		{
			return "";
		}

		/// <summary>
		/// 入力パラメータの途中を返す
		/// 例：, accel:"-1"
		/// </summary>
		/// <param name="param">キーとなる値</param>
		/// <param name="data">セットする値</param>
		/// <returns>パラメーター</returns>
		internal string getParamCode(string param, object data)
		{
			return String.Format(", {0}:\"{1}\"", param, data.ToString());
		}
	}
}
