using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace kkde.help
{
	public partial class HelpReferenceDialog : Form
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public HelpReferenceDialog()
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

		/// <summary>
		/// 閉じようとしたとき
		/// </summary>
		private void HelpReferenceDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			//ウィンドウはクローズせずに非表示にする
			this.Hide();
			e.Cancel = true;
		}
	}
}
