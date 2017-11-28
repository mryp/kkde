using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace kkde.search
{
	public partial class JumpOptionForm : Form
	{
		int m_lineNumber = -1;

		/// <summary>
		/// 指定された行番号
		/// </summary>
		public int LineNumber
		{
			get { return m_lineNumber; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public JumpOptionForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 移動するとき
		/// </summary>
		private void moveButton_Click(object sender, EventArgs e)
		{
			if (lineNumberBox.Text == "")
			{
				m_lineNumber = -1;
				util.Msgbox.Error("行番号を入力してください");
				return;
			}

			//変換する
			if (Int32.TryParse(lineNumberBox.Text, out m_lineNumber) == false)
			{
				m_lineNumber = -1;
				util.Msgbox.Error("整数値を入力してください");
				return;
			}

			this.Close();
		}

		/// <summary>
		/// キャンセルするとき
		/// </summary>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			m_lineNumber = -1;
			this.Close();
		}
	}
}
