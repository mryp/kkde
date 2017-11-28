using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using kkde.global;
using System.IO;

namespace kkde.search
{
	/// <summary>
	/// ブックマークリストを表示するクラス
	/// </summary>
	public partial class BookmarkListForm : WeifenLuo.WinFormsUI.DockContent
	{
		private const int COLUMN_INDEX_NAME = 0;
		private const int COLUMN_INDEX_LINE_NUMBER = 1;
		private const int COLUMN_INDEX_FILE_PATH = 2;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BookmarkListForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// ブックマークを新規に追加する
		/// </summary>
		public void AddBookmark(int lineNumber, string filePath)
		{
			addListItem("ブックマーク", lineNumber, filePath);
		}

		/// <summary>
		/// ブックマークを削除する
		/// </summary>
		/// <param name="lineNumber"></param>
		/// <param name="filePath"></param>
		public void RemoveBookmark(int lineNumber, string filePath)
		{
			foreach (ListViewItem item in bookmarkListView.Items)
			{
				if (Convert.ToInt32(item.SubItems[COLUMN_INDEX_LINE_NUMBER].Text) == lineNumber
				&& item.SubItems[COLUMN_INDEX_FILE_PATH].Text == filePath)
				{
					//削除する
					bookmarkListView.Items.Remove(item);
					return;
				}
			}
		}

		/// <summary>
		/// ブックマークをすべて削除する
		/// </summary>
		public void RemoveBookmarkAll()
		{
			bookmarkListView.Items.Clear();
		}

		/// <summary>
		/// ブックマークをリストに追加する
		/// </summary>
		/// <param name="name"></param>
		/// <param name="lineNumber"></param>
		/// <param name="filePath"></param>
		private void addListItem(string name, int lineNumber, string filePath)
		{
			foreach (ListViewItem item in bookmarkListView.Items)
			{
				if (Convert.ToInt32(item.SubItems[COLUMN_INDEX_LINE_NUMBER].Text) == lineNumber
				&& item.SubItems[COLUMN_INDEX_FILE_PATH].Text == filePath)
				{
					//すでに追加されているので何もしない
					return;
				}
			}

			//重複していないので追加する
			ListViewItem newItem = new ListViewItem(new string[] { name, lineNumber.ToString(), filePath });
			bookmarkListView.Items.Add(newItem);
		}

		/// <summary>
		/// ブックマークリストを返す
		/// </summary>
		/// <returns>ブックマーク情報の配列</returns>
		public BookmarkInfo[] GetBookmarkList()
		{
			List<BookmarkInfo> list = new List<BookmarkInfo>();

			foreach (ListViewItem item in bookmarkListView.Items)
			{
				list.Add(new BookmarkInfo(Convert.ToInt32(item.SubItems[COLUMN_INDEX_LINE_NUMBER].Text)
					, item.SubItems[COLUMN_INDEX_FILE_PATH].Text, item.Text));
			}

			return list.ToArray();
		}

		/// <summary>
		/// ブックマークをリストに反映する
		/// </summary>
		/// <param name="bookmarkList"></param>
		public void SetBookmarkList(BookmarkInfo[] bookmarkList)
		{
			bookmarkListView.Items.Clear();

			foreach (BookmarkInfo bookmark in bookmarkList)
			{
				if (File.Exists(bookmark.FilePath))	//ファイルが存在するときだけ追加する
				{
					addListItem(bookmark.Name, bookmark.LineNumber, bookmark.FilePath);
				}
			}
		}

		/// <summary>
		/// アイテムをダブルクリックしたとき
		/// </summary>
		private void bookmarkListView_DoubleClick(object sender, EventArgs e)
		{
			if (bookmarkListView.SelectedItems == null)
			{
				return;
			}

			ListViewItem item = bookmarkListView.SelectedItems[0];	//最初に選択のみ有効とする
			if (item == null)
			{
				return;	//選択が無効なので何もしない
			}

			//指定した位置へエディタを開き移動する
			int lineNumber = Convert.ToInt32(item.SubItems[COLUMN_INDEX_LINE_NUMBER].Text);
			string filePath = item.SubItems[COLUMN_INDEX_FILE_PATH].Text;
			GlobalStatus.EditorManager.LoadFile(filePath, lineNumber - 1);
		}
	}
}
