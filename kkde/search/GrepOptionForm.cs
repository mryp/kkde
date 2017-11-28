using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace kkde.search
{
	/// <summary>
	/// Grep検索の設定値をセットするフォーム
	/// </summary>
	public partial class GrepOptionForm : Form
	{
		bool m_searchResult = false;
		EditorSearchOption m_option;

		/// <summary>
		/// 検索結果
		/// 検索を行うときはtrue
		/// </summary>
		public bool SearchResult
		{
			get { return m_searchResult; }
			set { m_searchResult = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="option">検索オプション</param>
		/// <param name="selectText">現在選択している文字列</param>
		public GrepOptionForm(EditorSearchOption option, string selectText)
		{
			InitializeComponent();

			m_option = option;
			if (selectText != "")
			{
				//指定した項目を追加する
				option.SearchKeywordTable.Add(selectText);
			}

			//検索履歴をセットする
			foreach (string keyword in m_option.SearchKeywordTable.List)
			{
				searchKeywordBox.Items.Add(keyword);
			}
			if (searchKeywordBox.Items.Count > 0)
			{
				//一番先頭を選択状態にする
				searchKeywordBox.SelectedIndex = 0;
			}

			//オプション情報の反映
			ignoreCaseCheckBox.Checked = m_option.IgnoreCase;
			wordUnitCheckBox.Checked = m_option.WordUnit;
			regexCheckBox.Checked = m_option.Regex;

			//ファイル種類をセットする
			foreach (string keyword in m_option.GrepOption.FileExtTable.List)
			{
				fileExtBox.Items.Add(keyword);
			}
			if (fileExtBox.Items.Count > 0)
			{
				//一番先頭を選択状態にする
				fileExtBox.SelectedIndex = 0;
			}
			else
			{
				fileExtBox.Text = "*.ks;*.tjs";
			}

			if (m_option.GrepOption.Pos == GrepPotision.Project)
			{
				grepPosProjectRadioButton.Checked = true;
			}
			else if (m_option.GrepOption.Pos == GrepPotision.Folder)
			{
				grepPosFolderRadioButton.Checked = true;
			}
			grepPosFolderBox.Text = m_option.GrepOption.FolderPath;
			grepSubFolderCheckBox.Checked = m_option.GrepOption.UseSubFolder;

			m_searchResult = false;
		}

		/// <summary>
		/// ファイル拡張子のテンプレート一覧を表示する
		/// </summary>
		private void fileExtRefButton_Click(object sender, EventArgs e)
		{
			fileExtPopMenu.Show(Cursor.Position);	//コンテキストメニューを表示する
		}

		/// <summary>
		/// フォルダ選択ダイアログを表示する
		/// </summary>
		private void grepPosFolderRefButton_Click(object sender, EventArgs e)
		{
			if (grepFolderDialog.SelectedPath == "")
			{
				grepFolderDialog.SelectedPath = grepPosFolderBox.Text;
			}
			if (grepFolderDialog.ShowDialog() == DialogResult.OK)
			{
				grepPosFolderBox.Text = grepFolderDialog.SelectedPath;
			}
		}

		/// <summary>
		/// 検索オプションをセットする
		/// </summary>
		private bool setSearchOption()
		{
			//オプションチェック
			if (searchKeywordBox.Text == "")
			{
				util.Msgbox.Warning("検索キーワードが入力されていません");
				return false;
			}
			if (fileExtBox.Text == "")
			{
				util.Msgbox.Warning("検索種類が入力されていません");
				return false;
			}
			if (grepPosFolderRadioButton.Checked)
			{
				if (Directory.Exists(grepPosFolderBox.Text) == false)
				{
					util.Msgbox.Warning("指定フォルダは存在しません");
					return false;
				}
			}

			m_option.SearchKeywordTable.Add(searchKeywordBox.Text);
			m_option.IgnoreCase = ignoreCaseCheckBox.Checked;
			m_option.WordUnit = wordUnitCheckBox.Checked;
			m_option.Regex = regexCheckBox.Checked;
			m_option.Type = SearchType.Grep;
			m_option.Direction = SearchDirection.Down;

			m_option.GrepOption.FileExtTable.Add(fileExtBox.Text);
			if (grepPosProjectRadioButton.Checked)
			{
				m_option.GrepOption.Pos = GrepPotision.Project;
			}
			else if (grepPosFolderRadioButton.Checked)
			{
				m_option.GrepOption.Pos = GrepPotision.Folder;
			}
			m_option.GrepOption.FolderPath = grepPosFolderBox.Text;
			m_option.GrepOption.UseSubFolder = grepSubFolderCheckBox.Checked;

			return true;
		}

		/// <summary>
		/// オプション情報をセットして閉じる
		/// </summary>
		private void searchButton_Click(object sender, EventArgs e)
		{
			if (setSearchOption() == false)
			{
				return;	//オプションセットに失敗したとき
			}

			m_searchResult = true;
			this.Close();
		}

		/// <summary>
		/// 何もせずに閉じる
		/// </summary>
		private void closeButton_Click(object sender, EventArgs e)
		{
			m_searchResult = false;
			this.Close();
		}

		/// <summary>
		/// 吉里吉里ファイルの検索拡張子をセットする
		/// </summary>
		private void fileExtMenuItemKrkr_Click(object sender, EventArgs e)
		{
			setFileExtOption("*.ks;*.tjs");
		}

		/// <summary>
		/// 吉里吉里ファイルの検索拡張子をセットする
		/// </summary>
		private void fileExtMenuItemKag_Click(object sender, EventArgs e)
		{
			setFileExtOption("*.ks");
		}

		/// <summary>
		/// 吉里吉里ファイルの検索拡張子をセットする
		/// </summary>
		private void fileExtMenuItemTjs_Click(object sender, EventArgs e)
		{
			setFileExtOption("*.tjs");
		}

		/// <summary>
		/// 吉里吉里ファイルの検索拡張子をセットする
		/// </summary>
		private void fileExtMenuItemTxt_Click(object sender, EventArgs e)
		{
			setFileExtOption("*.txt");
		}

		/// <summary>
		/// 吉里吉里ファイルの検索拡張子をセットする
		/// </summary>
		private void fileExtMenuItemAll_Click(object sender, EventArgs e)
		{
			setFileExtOption("*.*");
		}

		/// <summary>
		/// 指定拡張子文字列をオプションにセットする
		/// </summary>
		/// <param name="extName">拡張子文字列（例：*.tjs）</param>
		public void setFileExtOption(string extName)
		{
			fileExtBox.Text = extName;
		}
	}
}
