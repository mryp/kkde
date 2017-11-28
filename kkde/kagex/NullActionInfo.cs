using System;
using System.Collections.Generic;
using System.Text;

namespace kkde.kagex
{
	/// <summary>
	/// NULLを表すアクションハンドラ
	/// </summary>
	public class NullActionInfo : IActionInfo
	{
		string m_value = "null";

		public string Value
		{
			get { return m_value; }
			set { m_value = value; }
		}

		public string GetCode()
		{
			return "null";
		}

		public string GetHandlerName()
		{
			return ActionHandlerManager.HANDLER_NAME_NULL;
		}
	}
}
