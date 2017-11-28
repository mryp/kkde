using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace kkde.project
{
	/// <summary>
	/// ファイル・フォルダのプロパティを表示するフォーム
	/// </summary>
	public partial class FilePropertyForm : Form
	{
		public FilePropertyForm(string filePath)
		{
			InitializeComponent();

			init(filePath);
		}

		/// <summary>
		/// 変数初期化を行う
		/// </summary>
		/// <param name="filePath">ファイルパス</param>
		private void init(string filePath)
		{
			if (File.Exists(filePath))
			{
				FileType.KrkrType krkrType = FileType.GetKrkrType(filePath);
				nameLabel.Text = Path.GetFileName(filePath);
				dirPathLabel.Text = Path.GetDirectoryName(filePath);
				switch (krkrType)
				{
					case FileType.KrkrType.Kag:
						kindLabel.Text = "KAGシナリオファイル";
						break;
					case FileType.KrkrType.Tjs:
						kindLabel.Text = "TJSスクリプトファイル";
						break;
					case FileType.KrkrType.Text:
						kindLabel.Text = "テキストファイル";
						break;
					case FileType.KrkrType.Image:
						kindLabel.Text = "画像ファイル";
						break;
					case FileType.KrkrType.Sound:
						kindLabel.Text = "サウンドファイル";
						break;
					default:
						kindLabel.Text = "その他のファイル（KKDEで扱えないファイル）";
						break;
				}
				
			}
			else if (Directory.Exists(filePath))
			{
				nameLabel.Text = Path.GetFileName(filePath);
				dirPathLabel.Text = Path.GetDirectoryName(filePath);
				kindLabel.Text = "フォルダ";
			}
			else
			{
				nameLabel.Text = "不明";
				dirPathLabel.Text = "不明";
				kindLabel.Text = "不明";
			}
		}

		/// <summary>
		/// 閉じるボタンを押したとき
		/// </summary>
		private void closeButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
