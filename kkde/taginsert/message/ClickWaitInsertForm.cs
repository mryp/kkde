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
	/// クリック待ち挿入フォーム
	/// </summary>
	public partial class ClickWaitInsertForm : kkde.taginsert.BaseInsertForm
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ClickWaitInsertForm()
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
			if (lwaitRadioButton.Checked)
			{
				text += "[l]";
				if (lwaitAddNewLineCheckBox.Checked)
				{
					text += "[r]";
				}
			}
			else if (pwaitRadioButton.Checked)
			{
				text += "[p]";
				if (pwaitAddClearLayerCheckBox.Checked)
				{
					text += GetNewLineCode() + GetNewLineCode() + "[cm]" + GetNewLineCode();
				}
			}

			return text;
		}
	}
}
