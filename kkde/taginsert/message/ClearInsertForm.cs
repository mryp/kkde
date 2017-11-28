using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace kkde.taginsert.message
{
	/// <summary>
	/// メッセージクリアータグ挿入フォーム
	/// </summary>
	public partial class ClearInsertForm : kkde.taginsert.BaseInsertForm
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ClearInsertForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 挿入するコードを返す
		/// </summary>
		/// <returns>挿入コード</returns>
		public override string GetCode()
		{
			string text = "";
			if (addLabelCheckBox.Checked)
			{
				text += "[p]" + this.GetNewLineCode() +this.GetNewLineCode();
			}
			if (addNewPageWaitCheckButton.Checked)
			{
				text += "*" + addLabelNameTextBox.Text + this.GetNewLineCode();
			}

			if (allClearRadioButton.Checked)
			{
				text += "[cm]";
			}
			else if (allResetReadioButton.Checked)
			{
				text += "[ct]";
			}
			else if (currentClearRadioButton.Checked)
			{
				text += "[er]";
			}

			return text;
		}
	}
}
