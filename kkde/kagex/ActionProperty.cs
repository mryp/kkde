using System;
using System.Collections.Generic;
using System.Text;

namespace kkde.kagex
{
	public class ActionProperty
	{
		string m_name;
		IActionInfo m_handler;

		/// <summary>
		/// アクションプロパティ名
		/// </summary>
		public string Name
		{
			get { return m_name; }
			set { m_name = value; }
		}
		
		/// <summary>
		/// プロパティに関連づけるハンドラオブジェクト
		/// </summary>
		public IActionInfo Handler
		{
			get { return m_handler; }
			set { m_handler = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name"></param>
		public ActionProperty(string name)
		{
			m_name = name;
			m_handler = new NullActionInfo();
		}
	}
}
