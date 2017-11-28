using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using kkde.editor;

namespace kkde.search
{
	public partial class ReplaceAllForm : Form
	{
		bool m_cancel = false;

		/// <summary>
		/// キャンセルしたかどうか
		/// キャンセルしたときtrueを返す
		/// </summary>
		public bool Cancel
		{
			get { return m_cancel; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ReplaceAllForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// フォームを表示したとき
		/// </summary>
		private void ReplaceAllForm_Load(object sender, EventArgs e)
		{
			m_cancel = false;
		}

		/// <summary>
		/// ダイアログを閉じる
		/// </summary>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Close();	//閉じる
		}

		/// <summary>
		/// ダイアログを閉じようとしているとき
		/// </summary>
		private void ReplaceAllForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			m_cancel = true;
		}

		/// <summary>
		/// 置換個数をセットする
		/// </summary>
		/// <param name="count">個数</param>
		public void SetReplaceCount(int count)
		{
			countLabel.Text = count.ToString();
		}
	}
}
