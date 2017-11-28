using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace kkde.option
{
	/// <summary>
	/// KAG入力補完リストの入力アイテムを作成するフォーム
	/// </summary>
	public partial class KagCompletionListInputBox : Form
	{
		/// <summary>
		/// アイテムを区切る記号
		/// </summary>
		private const char LIST_SEP_MARK = ';';

		/// <summary>
		/// リスト入力文字列を取得する
		/// </summary>
		public string InputText
		{
			get { return getInputText(); }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="input"></param>
		public KagCompletionListInputBox(string input)
		{
			InitializeComponent();

			initInput(input);
		}

		/// <summary>
		/// 入力をセットする
		/// </summary>
		/// <param name="input"></param>
		private void initInput(string input)
		{
			string[] itemList = input.Split(LIST_SEP_MARK);
			foreach (string item in itemList)
			{
				listInputBox.Text += item + "\r\n";
			}
			listInputBox.Select(0, 0);
		}

		/// <summary>
		/// 入力文字列を取得する
		/// </summary>
		/// <returns></returns>
		private string getInputText()
		{
			string input = listInputBox.Text.Trim();
			return input.Replace("\r\n", ";");
		}

		/// <summary>
		/// OKボタン
		/// </summary>
		private void okButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// キャンセルボタン
		/// </summary>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
