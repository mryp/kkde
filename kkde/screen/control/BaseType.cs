using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Diagnostics;

namespace kkde.screen.control
{
	/// <summary>
	/// スクリーンコントロールのベースとなる部品
	/// </summary>
	public class BaseType
	{
		#region フィールド
		private Point m_location;
		private String m_name;
		private String m_imagePath;
		#endregion

		#region プロパティ
		[Category("配置")]
		[Description("コントロールの左上原点位置")]
		public Point Location
		{
			get { return m_location; }
			set { m_location = value; }
		}

		[Category("基本")]
		[Description("コントロール名")]
		public String Name
		{
			get { return m_name; }
			set { m_name = value; }
		}

		[Category("表示")]
		[Description("背景画像")]
		[Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
		public String ImagePath
		{
			get { return m_imagePath; }
			set 
			{
//				Debug.WriteLine("set path=" + value);
				m_imagePath = value; 
			}
		}
		#endregion
	}
}
