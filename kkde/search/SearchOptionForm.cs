using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using kkde.global;

namespace kkde.search
{
	public partial class SearchOptionForm : Form
	{
		EditorSearchOption m_option;
		bool m_searchResult = false;

		/// <summary>
		/// 検索を行うかどうか
		/// 検索を行うときはtrueを返す
		/// </summary>
		public bool SearchResult
		{
			get { return m_searchResult; }
			set { m_searchResult = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SearchOptionForm(EditorSearchOption option, string selectText)
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

			m_searchResult = false;
		}

		/// <summary>
		/// 上方向検索を行う
		/// </summary>
		private void upSearchButton_Click(object sender, EventArgs e)
		{
			m_option.Direction = SearchDirection.Up;
			if (setSearchOption() == false)
			{
				return;	//オプションセットに失敗したとき
			}

			m_searchResult = true;
			this.Close();
		}

		/// <summary>
		/// 下方向検索を行う
		/// </summary>
		private void downSearchButton_Click(object sender, EventArgs e)
		{
			m_option.Direction = SearchDirection.Down;
			if (setSearchOption() == false)
			{
				return;	//オプションセットに失敗したとき
			}

			m_searchResult = true;
			this.Close();
		}

		/// <summary>
		/// 検索オプションをセットする
		/// </summary>
		private bool setSearchOption()
		{
			if (searchKeywordBox.Text == "")
			{
				util.Msgbox.Warning("検索キーワードが入力されていません");
				return false;
			}

			m_option.SearchKeywordTable.Add(searchKeywordBox.Text);
			m_option.IgnoreCase = ignoreCaseCheckBox.Checked;
			m_option.WordUnit = wordUnitCheckBox.Checked;
			m_option.Regex = regexCheckBox.Checked;
			m_option.Type = SearchType.Search;

			return true;
		}

		/// <summary>
		/// 何もせずにダイアログを閉じる
		/// </summary>
		private void closeButton_Click(object sender, EventArgs e)
		{
			m_searchResult = false;
			this.Close();
		}
	}
}
