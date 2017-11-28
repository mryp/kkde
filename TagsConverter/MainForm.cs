using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace TagsConverter
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();

#if DEBUG
			inputBox.Text = "tags.xml";
			outputBox.Text = "kag3tag.ks";
#endif
		}

		/// <summary>
		/// 入力テキストボックスにパスを入力する
		/// </summary>
		private void inputRefButton_Click(object sender, EventArgs e)
		{
			if (inputOpenDialog.ShowDialog() == DialogResult.OK)
			{
				inputBox.Text = inputOpenDialog.FileName;
			}
		}

		/// <summary>
		/// 出力テキストボックスにパスを入力する
		/// </summary>
		private void outputRefButton_Click(object sender, EventArgs e)
		{
			if (outputSaveDialog.ShowDialog() == DialogResult.OK)
			{
				outputBox.Text = outputSaveDialog.FileName;
			}
		}

		/// <summary>
		/// ヘルプ
		/// </summary>
		private void helpButton_Click(object sender, EventArgs e)
		{
			string text = "";
			text += "TagsConverter version 1.0.0\n";
			text += "copyright (C) 2008 PORING SOFT\n";
			text += "\n";
			text += "tags.xmlファイルをKKDE2用tag3tag.ksファイルに変換します\n";
			text += "パラメータータイプなど完全には変換できないので変換後のファイルを修正する必要があります";

			MessageBox.Show(text, "ヘルプ");
		}

		/// <summary>
		/// 変換
		/// </summary>
		private void convertButton_Click(object sender, EventArgs e)
		{
			//パラメーターチェック
			if (File.Exists(inputBox.Text) == false)
			{
				MessageBox.Show("入力ファイルが見つかりません");
				return;
			}
			if (outputBox.Text == "")
			{
				MessageBox.Show("出力ファイルが指定されていません");
				return;
			}

			KagTagsFile tagsFile = new KagTagsFile(inputBox.Text);
			bool ret = tagsFile.SaveFileToKag(outputBox.Text);
			if (ret == false)
			{
				MessageBox.Show("ファイルの作成に失敗しました");
				return;
			}

			MessageBox.Show("変換が完了しました", "完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}



		/// <summary>
		/// 閉じる
		/// </summary>
		private void closeButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
