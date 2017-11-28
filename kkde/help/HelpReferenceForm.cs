using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace kkde.help
{
	/// <summary>
	/// ヘルプリファレンスを表示するフォーム
	/// </summary>
	public partial class HelpReferenceForm : WeifenLuo.WinFormsUI.DockContent
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public HelpReferenceForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 新しいページへ遷移する
		/// </summary>
		/// <param name="url">遷移するページ</param>
		public void Navigate(string url)
		{
			helpPanel.Navigate(url);
		}
	}
}
