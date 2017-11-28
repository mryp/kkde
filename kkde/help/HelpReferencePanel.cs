using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace kkde.help
{
	/// <summary>
	/// ヘルプリファレンスを表示するパネル
	/// </summary>
	public partial class HelpReferencePanel : UserControl
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public HelpReferencePanel()
		{
			InitializeComponent();

			helpBrowser.CanGoBackChanged += new EventHandler(helpBrowser_CanGoBackChanged);
			helpBrowser.CanGoForwardChanged += new EventHandler(helpBrowser_CanGoForwardChanged);
			toolboxItemChangeSize();
		}

		void helpBrowser_CanGoForwardChanged(object sender, EventArgs e)
		{
			toolItemFrontPage.Enabled = helpBrowser.CanGoForward;
		}

		void helpBrowser_CanGoBackChanged(object sender, EventArgs e)
		{
			toolItemBackPage.Enabled = helpBrowser.CanGoBack;
		}

		/// <summary>
		/// ツールボックスのサイズを変更する
		/// </summary>
		private void toolboxItemChangeSize()
		{
			int width = naviToolBar.Width - (23 * 6);	//サイズはボタン数によって変更すること
			urlComboBox.Size = new Size(width, 26);
		}

		/// <summary>
		/// ツールボックスのサイズが変更されたとき
		/// </summary>
		private void naviToolBar_Resize(object sender, EventArgs e)
		{
			toolboxItemChangeSize();
		}

		/// <summary>
		/// 新しいページへ遷移する
		/// </summary>
		/// <param name="url">遷移するページ</param>
		public void Navigate(string url)
		{
			try
			{
				helpBrowser.Navigate(url);
			}
			catch (Exception err)
			{
				Debug.WriteLine("ERROR: " + err.ToString());
				util.Msgbox.Error("ページを表示できません\n" + err.Message);
			}
		}

		/// <summary>
		/// 元に戻る
		/// </summary>
		private void toolItemBackPage_Click(object sender, EventArgs e)
		{
			helpBrowser.GoBack();
		}

		/// <summary>
		/// やり直し
		/// </summary>
		private void toolItemFrontPage_Click(object sender, EventArgs e)
		{
			helpBrowser.GoForward();
		}

		/// <summary>
		/// 更新を中止する
		/// </summary>
		private void toolItemStop_Click(object sender, EventArgs e)
		{
			helpBrowser.Stop();
		}

		/// <summary>
		/// 現在の表示を再更新する
		/// </summary>
		private void toolItemRefresh_Click(object sender, EventArgs e)
		{
			helpBrowser.Refresh();
		}

		/// <summary>
		/// URL決定ボタンを押したとき
		/// </summary>
		private void toolItemEnter_Click(object sender, EventArgs e)
		{
			this.Navigate(urlComboBox.Text);
		}

		/// <summary>
		/// コンボボックスの選択状態を変更したとき
		/// </summary>
		private void urlComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.Navigate(urlComboBox.Text);
		}

		/// <summary>
		/// URL入力欄でアドレスを入力後エンターを押したとき
		/// </summary>
		private void urlComboBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Return)
			{
				this.Navigate(urlComboBox.Text);
			}
		}

		/// <summary>
		/// ブラウザの表示切り替えを行ったとき
		/// </summary>
		private void helpBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
		{
			if (urlComboBox.ComboBox.Items.Contains(e.Url.ToString()) == false)
			{
				urlComboBox.Items.Add(e.Url.ToString());	//存在しないときは追加する
			}
			urlComboBox.Text = e.Url.ToString();
		}
	}
}
