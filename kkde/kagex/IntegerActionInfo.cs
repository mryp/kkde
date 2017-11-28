using System;
using System.Collections.Generic;
using System.Text;

namespace kkde.kagex
{
	/// <summary>
	/// 整数情報のみを保持するアクションハンドラ
	/// </summary>
	public class IntegerActionInfo : IActionInfo
	{
		int m_value = 0;

		public int Value
		{
			get { return m_value; }
			set { m_value = value; }
		}

		/// <summary>
		/// アクションハンドラ名を返す
		/// </summary>
		/// <returns>アクションハンドラ名</returns>
		public string GetHandlerName()
		{
			return ActionHandlerManager.HANDLER_NAME_INT;
		}

		/// <summary>
		/// envinit構文のコードを返す
		/// </summary>
		/// <returns>コード</returns>
		public string GetCode()
		{
			return m_value.ToString();
		}
	}
}
