using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace kkde.help
{
	public partial class VersionForm : Form
	{
		public VersionForm()
		{
			InitializeComponent();

			systemOSLabel.Text = KkdeEnvInfo.OSVersion;
			systemNetLabel.Text = KkdeEnvInfo.DotnetVersion;
			//systemCPULabel.Text = KkdeEnvInfo.CPUVersion;
			//totalMemoryLabel.Text = KkdeEnvInfo.MemorySize;
			//useMemoryLabel.Text = KkdeEnvInfo.UseMemorySize;
			//gcMemoryLabel.Text = KkdeEnvInfo.GcMemorySize;
		}

		/// <summary>
		/// OKボタンを押したとき
		/// </summary>
		private void okButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// WEBサイト
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void webLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start("http://www.poringsoft.net/");
			}
			catch (Win32Exception err)
			{
				util.Msgbox.Error(String.Format("{0}\nURLの関連付けが正常に設定されていないため、ブラウザの起動に失敗しました。", err.Message));
			}
		}

		private void mailLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				System.Diagnostics.Process.Start("mailto:mry@poringsoft.net");
			}
			catch (Win32Exception err)
			{
				util.Msgbox.Error(String.Format("{0}\nメールアドレスの関連付けが正常に行われていないため、メールソフトの起動に失敗しました。", err.Message));
			}
		}
	}
}
