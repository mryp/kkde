using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using kkde.global;
using kkde.project;
using System.IO;
using System.Text.RegularExpressions;
using kkde.option;

namespace kkde.search
{
	/// <summary>
	/// 検索結果を表示するクラス
	/// </summary>
	public partial class SearchResultForm : WeifenLuo.WinFormsUI.DockContent
	{
		/// <summary>
		/// 検索結果を保持するクラス
		/// </summary>
		private class ResultState
		{
			string m_text;
			int m_progress;
			string m_fileName;

			/// <summary>
			/// 検索結果一覧
			/// </summary>
			public string Text
			{
				get { return m_text; }
				set { m_text = value; }
			}
			
			/// <summary>
			/// 進捗（100％）
			/// </summary>
			public int Progress
			{
				get { return m_progress; }
				set { m_progress = value; }
			}
			
			/// <summary>
			/// ファイル名
			/// </summary>
			public string FileName
			{
				get { return m_fileName; }
				set { m_fileName = value; }
			}

			/// <summary>
			/// コンストラクタ
			/// </summary>
			/// <param name="fileName">ファイル名</param>
			/// <param name="progress">進捗</param>
			/// <param name="text">内容文字列</param>
			public ResultState(string fileName, int progress, string text)
			{
				m_text = text;
				m_progress = progress;
				m_fileName = fileName;
			}
		}

		EditorSearchOption m_option = null;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SearchResultForm()
		{
			InitializeComponent();

			resultEditor.Text = "";
			resultEditor.Document.ReadOnly = true;
			resultEditor.ActiveTextArea.MouseDoubleClick += new MouseEventHandler(ActiveTextArea_MouseDoubleClick);
			SetEditorOption();
			infoStatusBar.Visible = false;	//非表示
		}

		/// <summary>
		/// エディタウィンドウを初期化する
		/// </summary>
		public void SetEditorOption()
		{
			EditorOption option = EditorOption.LoadFile(ConstEnvOption.DefOptionFilePath);

			resultEditor.Encoding = option.Encoding;
			resultEditor.Font = option.Font;
			resultEditor.Document.TextEditorProperties.AllowCaretBeyondEOL = option.AllowCaretBeyondEOL;
			resultEditor.Document.TextEditorProperties.TabIndent = option.TabIndent;
			resultEditor.Document.TextEditorProperties.IndentationSize = option.IndentationSize;
			resultEditor.Document.TextEditorProperties.IndentStyle = option.IndentStyle;
			resultEditor.Document.TextEditorProperties.DocumentSelectionMode = option.DocumentSelectionMode;
			resultEditor.Document.TextEditorProperties.BracketMatchingStyle = option.BracketMatchingStyle;
			resultEditor.Document.TextEditorProperties.ShowMatchingBracket = option.ShowMatchingBracket;
			resultEditor.Document.TextEditorProperties.ShowLineNumbers = option.ShowLineNumbers;
			resultEditor.Document.TextEditorProperties.ShowSpaces = option.ShowSpaces;
			resultEditor.Document.TextEditorProperties.ShowWideSpaces = option.ShowWideSpaces;
			resultEditor.Document.TextEditorProperties.ShowTabs = option.ShowTabs;
			resultEditor.Document.TextEditorProperties.ShowEOLMarker = option.ShowEOLMarker;
			resultEditor.Document.TextEditorProperties.ShowInvalidLines = option.ShowInvalidLines;
			resultEditor.Document.TextEditorProperties.IsIconBarVisible = option.IsIconBarVisible;
			resultEditor.Document.TextEditorProperties.EnableFolding = option.EnableFolding;
			resultEditor.Document.TextEditorProperties.ShowHorizontalRuler = false;
			resultEditor.Document.TextEditorProperties.ShowVerticalRuler = false;
			resultEditor.Document.TextEditorProperties.ConvertTabsToSpaces = option.ConvertTabsToSpaces;
			resultEditor.Document.TextEditorProperties.UseAntiAliasedFont = option.UseAntiAliasedFont;
			resultEditor.Document.TextEditorProperties.CreateBackupCopy = option.CreateBackupCopy;
			resultEditor.Document.TextEditorProperties.MouseWheelScrollDown = option.MouseWheelScrollDown;
			resultEditor.Document.TextEditorProperties.MouseWheelTextZoom = option.MouseWheelTextZoom;
			resultEditor.Document.TextEditorProperties.HideMouseCursor = option.HideMouseCursor;
			resultEditor.Document.TextEditorProperties.CutCopyWholeLine = option.CutCopyWholeLine;
			resultEditor.Document.TextEditorProperties.VerticalRulerRow = option.VerticalRulerRow;
			resultEditor.Document.TextEditorProperties.LineViewerStyle = option.LineViewerStyle;
			resultEditor.Document.TextEditorProperties.LineTerminator = option.LineTerminator;
			resultEditor.Document.TextEditorProperties.AutoInsertCurlyBracket = option.AutoInsertCurlyBracket;
			resultEditor.Document.TextEditorProperties.UseCustomLine = option.UseCustomLine;

			resultEditor.OptionsChanged();
		}

		/// <summary>
		/// Grep検索を実行する
		/// </summary>
		/// <param name="option">検索オプション</param>
		public void Grep(EditorSearchOption option)
		{
			m_option = option;
			infoStatusBar.Visible = true;	//ステータスバーを表示する
			resultEditor.Text = "";
			searchResultProgressBar.Value = 0;
			searchResultStatusLabel.Text = "検索中...";
			grepBgWarker.RunWorkerAsync(option);
		}

		/// <summary>
		/// Grep検索を実行する
		/// </summary>
		private void grepBgWarker_DoWork(object sender, DoWorkEventArgs e)
		{
			EditorSearchOption option = (EditorSearchOption)e.Argument;
			Grep grep = new Grep(option);
			if (grep.Option == null)
			{
				return;	//オプションが不正なときは強制終了
			}

			//検索ファイルリストを取得する
			string[] fileList = grep.GetSearchFileList();
			if (fileList == null || fileList.Length == 0)
			{
				return;
			}
			string basePath = option.GrepOption.SearchStartPath;

			//ファイルの数だけ検索を行う
			for (int i = 0; i < fileList.Length; i++)
			{
				if (grepBgWarker.CancellationPending)
				{
					return;	//強制中断
				}
				List<search.Grep.SearchResult> result = grep.Search(fileList[i]);	//検索する

				//結果作成し送信する
				int progress = (int)((double)((double)i / (double)fileList.Length) * 100);
				string text = "";
				foreach (search.Grep.SearchResult res in result)
				{
					text += String.Format("{0}({1}):{2}\n"
						, util.FileUtil.ConvertRelativaPath(basePath, res.FilePath), res.LineNumber, res.LineText);
				}
				ResultState state = new ResultState(fileList[i], progress, text);
				grepBgWarker.ReportProgress(progress, state);
			}
		}

		/// <summary>
		/// Grep検索の実行状況報告を受けたとき
		/// </summary>
		private void grepBgWarker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			//情報を更新する
			ResultState state = (ResultState)e.UserState;
			searchResultProgressBar.Value = state.Progress;
			searchResultStatusLabel.Text = Path.GetFileName(state.FileName);

			if (state.Text == "")
			{
				return;	//結果が入っていないときは何もしない
			}
			resultEditor.Text += state.Text;
			resultEditor.MoveCaretLastLine();
		}

		/// <summary>
		/// Grep検索が完了したとき
		/// </summary>
		private void grepBgWarker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			infoStatusBar.Visible = false;	//完了したので非表示にする
		}

		/// <summary>
		/// 検索を中止する
		/// </summary>
		private void menuItemStop_Click(object sender, EventArgs e)
		{
			if (grepBgWarker.IsBusy)
			{
				//Grep検索を行っているときは中止する
				grepBgWarker.CancelAsync();
			}
		}

		/// <summary>
		/// 検索を中止する
		/// </summary>
		private void searchStopButton_ButtonClick(object sender, EventArgs e)
		{
			if (grepBgWarker.IsBusy)
			{
				//Grep検索を行っているときは中止する
				grepBgWarker.CancelAsync();
			}
		}

		/// <summary>
		/// 検索結果リストをダブルクリックしたとき
		/// </summary>
		void ActiveTextArea_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (m_option == null)
			{
				return;	//オプションがないときは何もしない
			}
			//行文字列を入手する
			string line = resultEditor.GetSelectLineText();

			//行を解析しファイルパスと行番号を取得する
			Regex regex = new Regex(@"(?<1>.+)\((?<2>[0-9]+)\):.*");
			Match match = regex.Match(line);
			if (match.Success)
			{
				//指定したファイルパスと行番号を開く
				string fullPath = util.FileUtil.ConvertFullPath(m_option.GrepOption.SearchStartPath, match.Groups[1].Value);
				int num = Convert.ToInt32(match.Groups[2].Value);
				GlobalStatus.EditorManager.LoadFile(fullPath, num - 1);
			}
		}

		private void editorMenuItemCopy_Click(object sender, EventArgs e)
		{
			resultEditor.CopyText();
		}

		private void editorMenuItemClear_Click(object sender, EventArgs e)
		{
			resultEditor.Text = "";
			resultEditor.Refresh();
		}

	}
}
