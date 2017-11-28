using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Windows.Forms;

using kkde.debug;
using kkde.editor;
using kkde.global;
using kkde.help;
using kkde.option;
using kkde.project;
using kkde.search;
using WeifenLuo.WinFormsUI;

namespace kkde
{
	/// <summary>
	/// メインフォームクラス
	/// </summary>
    public partial class MainForm : Form
	{
		#region フィールド
		/// <summary>
		/// 新規作成ダイアログ
		/// </summary>
		NewCreateForm m_newCreateForm = null;

		/// <summary>
		/// バージョン情報フォーム
		/// </summary>
		VersionForm m_versionForm = null;
		#endregion

		#region プロパティ
		/// <summary>
		/// メインのドックパネルを返す
		/// </summary>
		public DockPanel MainDockPanel
		{
			get
			{
				return mainDockPanel;
			}
		}

		/// <summary>
		/// 新規作成ダイアログアクセス用プロパティ
		/// </summary>
		private NewCreateForm newCreateForm
		{
			get
			{
				if (m_newCreateForm == null)
				{
					m_newCreateForm = new NewCreateForm();
				}

				return m_newCreateForm;
			}
		}

		/// <summary>
		/// バージョン情報ダイアログアクセス用フォーム
		/// </summary>
		private VersionForm versionForm
		{
			get 
			{
				if (m_versionForm == null)
				{
					m_versionForm = new VersionForm();
				}

				return m_versionForm; 
			}
		}		
		#endregion

		#region 初期化
		/// <summary>
		/// コンストラクタ
		/// </summary>
        public MainForm()
        {
            InitializeComponent();
#if !DEBUG
			//デバッグ用の項目をすべて非表示にする
			menuItemDebug.Visible = false;
#endif
			//設定の初期化
			initOptionSetting();

			//オブジェクト初期化
			GlobalStatus.FormManager.MainForm = this;
			GlobalStatus.EditorManager = new EditorManager(mainDockPanel);
			GlobalStatus.EditorManager.CompImageList = new CompletionImageList(editorImageList);
			GlobalStatus.KrkrProc = new KrkrProcess();

			string projectPath = getProjectPathFromCommandArgs();
			if (projectPath != null)
			{
				//コマンドで渡されたファイルを開く
				GlobalStatus.FormManager.ProjectForm.LoadProject(projectPath);
			}
			else
			{
				//最後に読み込んだプロジェクトファイルが存在するときはプロジェクトを開く
				if ((GlobalStatus.EnvOption.ProjectOpenedLastProject)
				&& File.Exists(GlobalStatus.EnvOption.LastProjectPath))
				{
					GlobalStatus.FormManager.ProjectForm.LoadProject(GlobalStatus.EnvOption.LastProjectPath);
				}
			}

			//表示の更新
			UpdateWindowState();
			UpdateStatusBar();
			UpdateToolBar();
		}

		/// <summary>
		/// 設定ファイル関連を初期化する
		/// </summary>
		public void initOptionSetting()
		{
			//それぞれデフォルトオプションファイルを作成する
			if (File.Exists(ConstEnvOption.KagModeFilePath) == false)
			{
				KagColorType colorType = (KagColorType)TypeColorFile.GetDefault(FileType.KrkrType.Kag, TypeColorFile.DefaultColorType.White);
				TypeColorFile.SaveFile(FileType.KrkrType.Kag, colorType);
			}
			if (File.Exists(ConstEnvOption.TjsModeFilePath) == false)
			{
				TjsColorType colorType = (TjsColorType)TypeColorFile.GetDefault(FileType.KrkrType.Tjs, TypeColorFile.DefaultColorType.White);
				TypeColorFile.SaveFile(FileType.KrkrType.Tjs, colorType);
			}
			if (File.Exists(ConstEnvOption.DefModeFilePath) == false)
			{
				BaseColorType colorType = (BaseColorType)TypeColorFile.GetDefault(FileType.KrkrType.Unknown, TypeColorFile.DefaultColorType.White);
				TypeColorFile.SaveFile(FileType.KrkrType.Unknown, colorType);
			}
			if (File.Exists(ConstEnvOption.KagOptionFilePath) == false)
			{
				//KAGエディタオプションファイルが存在しないので作成する
				EditorOption option = EditorOption.GetDefault(FileType.KrkrType.Kag);
				EditorOption.SaveFile(ConstEnvOption.KagOptionFilePath, option);
			}
			if (File.Exists(ConstEnvOption.TjsOptionFilePath) == false)
			{
				//TJSエディタオプションファイルが存在しないので作成する
				EditorOption option = EditorOption.GetDefault(FileType.KrkrType.Tjs);
				EditorOption.SaveFile(ConstEnvOption.TjsOptionFilePath, option);
			}
			if (File.Exists(ConstEnvOption.DefOptionFilePath) == false)
			{
				//その他のファイルエディタオプションファイルが存在しないので作成する
				EditorOption option = EditorOption.GetDefault(FileType.KrkrType.Unknown);
				EditorOption.SaveFile(ConstEnvOption.DefOptionFilePath, option);
			}
			if (File.Exists(ConstEnvOption.SyntaxModesFilePath) == false)
			{
				SyntaxModesFile.SaveFile(ConstEnvOption.SyntaxModesFilePath);	//ファイルを作成する
			}

			//設定ファイルを読み込む
			if (File.Exists(EnvOption.FilePath))
			{
				//環境設定を読み込む
				GlobalStatus.EnvOption = EnvOption.LoadFile(EnvOption.FilePath);
			}
			if (File.Exists(ConstEnvOption.SyntaxModesFilePath))
			{
				//色強調情報を読み込む
				ICSharpCode.TextEditor.Document.FileSyntaxModeProvider fileSysntax =
					new ICSharpCode.TextEditor.Document.FileSyntaxModeProvider(Path.GetDirectoryName(ConstEnvOption.SyntaxModesFilePath));
				ICSharpCode.TextEditor.Document.HighlightingManager.Manager.AddSyntaxModeFileProvider(fileSysntax);
			}
			if (File.Exists(ConstEnvOption.DockPanelFilePath))
			{
				//ドッキングパネル情報を読み込む
				loadFileDockPanel();
			}
		}

		/// <summary>
		/// フォームを表示したとき
		/// </summary>
		private void mainForm_Load(object sender, EventArgs e)
		{
		}

		/// <summary>
		/// ウィンドウの状態を反映する
		/// </summary>
		private void UpdateWindowState()
		{
			this.StartPosition = GlobalStatus.EnvOption.WindowStartPos;
			this.WindowState = GlobalStatus.EnvOption.WindowState;
			this.Size = GlobalStatus.EnvOption.WindowSize;
		}

		/// <summary>
		/// ツールバーの状態を反映する
		/// </summary>
		private void UpdateToolBar()
		{
			if (GlobalStatus.EnvOption.ToolBarFilePos.Location.X == -1)
			{
				//設定が読み込めていない時はデフォルト値をセットする
				initSetToolBar();
			}

			mainToolStripContainer.TopToolStripPanel.Controls.Clear();
			List<ToolBarPos> posList = new List<ToolBarPos>();
			posList.Add(GlobalStatus.EnvOption.ToolBarFilePos);
			posList.Add(GlobalStatus.EnvOption.ToolBarEditPos);
			posList.Add(GlobalStatus.EnvOption.ToolBarSearchPos);
			posList.Add(GlobalStatus.EnvOption.ToolBarBookmarkPos);
			posList.Add(GlobalStatus.EnvOption.ToolBarExecPos);
			posList.Add(GlobalStatus.EnvOption.ToolBarViewPos);
			posList.Sort();

			for (int i = 0; i < posList.Count; i++)
			{
				setToolBarPos(getToolBarObject(posList[i]), posList[i]);
			}

			//setToolBarPos(fileToolStrip, GlobalStatus.EnvOption.ToolBarFilePos);
			//setToolBarPos(editToolStrip, GlobalStatus.EnvOption.ToolBarEditPos);
			//setToolBarPos(searchToolStrip, GlobalStatus.EnvOption.ToolBarSearchPos);
			//setToolBarPos(bookmarkToolStrip, GlobalStatus.EnvOption.ToolBarBookmarkPos);
			//setToolBarPos(execToolStrip, GlobalStatus.EnvOption.ToolBarExecPos);
			//setToolBarPos(viewToolStrip, GlobalStatus.EnvOption.ToolBarViewPos);
		}

		private void initSetToolBar()
		{
			//作成時に初期値をセットする用に変更
		}

		/// <summary>
		/// ツールバーの位置設定オブジェクトからツールバーオブジェクトを取得する
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		private ToolStrip getToolBarObject(ToolBarPos pos)
		{
			if (pos == GlobalStatus.EnvOption.ToolBarFilePos)
			{
				return fileToolStrip;
			}
			else if (pos == GlobalStatus.EnvOption.ToolBarEditPos)
			{
				return editToolStrip;
			}
			else if (pos == GlobalStatus.EnvOption.ToolBarSearchPos)
			{
				return searchToolStrip;
			}
			else if (pos == GlobalStatus.EnvOption.ToolBarBookmarkPos)
			{
				return bookmarkToolStrip;
			}
			else if (pos == GlobalStatus.EnvOption.ToolBarExecPos)
			{
				return execToolStrip;
			}
			else if (pos == GlobalStatus.EnvOption.ToolBarViewPos)
			{
				return viewToolStrip;
			}

			return fileToolStrip;
		}

		/// <summary>
		/// 表示状態をセットする
		/// </summary>
		private void setToolBarPos(ToolStrip toolBar, ToolBarPos pos)
		{
			toolBar.Location = new Point(pos.Location.X, pos.Location.Y);
			switch (pos.Direction)
			{
				case ToolBarDirection.Top:
					this.mainToolStripContainer.TopToolStripPanel.Controls.Add(toolBar);
					break;
				case ToolBarDirection.Bottom:
					this.mainToolStripContainer.BottomToolStripPanel.Controls.Add(toolBar);
					break;
				case ToolBarDirection.Left:
					this.mainToolStripContainer.LeftToolStripPanel.Controls.Add(toolBar);
					break;
				case ToolBarDirection.Right:
					this.mainToolStripContainer.RightToolStripPanel.Controls.Add(toolBar);
					break;
				default:
					this.mainToolStripContainer.TopToolStripPanel.Controls.Add(toolBar);
					break;
			}
		}

		/// <summary>
		/// 引数からプロジェクトパスを取得する
		/// </summary>
		/// <returns></returns>
		private string getProjectPathFromCommandArgs()
		{
			string[] cmds = System.Environment.GetCommandLineArgs();
			if (cmds.Length > 1)
			{
				string path = cmds[1];
				if (File.Exists(path))
				{
					string ext = Path.GetExtension(path);
					if (ext == ("." + ProjectFile.PROJECT_EXT_NAME))
					{
						return path;
					}
				}
			}

			return null;
		}
		#endregion

		#region 終了処理
		/// <summary>
		/// 終了処理
		/// </summary>
		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				//設定をファイルに保存する
				saveFileDockPanel();
				saveWindowState();
				saveToolBar();
				EnvOption.SaveFile(EnvOption.FilePath, GlobalStatus.EnvOption);
			}
			catch (Exception err)
			{
				util.Msgbox.Error("設定情報の保存ができませんでした\n" + err.ToString());
			}
		}

		/// <summary>
		/// 終了しようとしたときの処理
		/// </summary>
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (GlobalStatus.Project.IsOpened)
			{
				GlobalStatus.FormManager.ProjectForm.CloseProject();
			}

			//すべてのタブを閉じる（保存していないときは保存する）
			if (GlobalStatus.EditorManager.CloseFileAll() == false)
			{
				//すべて閉じれなかった（キャンセルを押した）時は閉じるのをやめる
				e.Cancel = true;
				return;
			}
		}

		/// <summary>
		/// ウィンドウの状態を設定情報に保存する
		/// </summary>
		private void saveWindowState()
		{
			GlobalStatus.EnvOption.WindowStartPos = this.StartPosition;
			GlobalStatus.EnvOption.WindowState = this.WindowState;
			GlobalStatus.EnvOption.WindowSize = this.Size;
		}

		/// <summary>
		/// ツールバーの状態を設定に保存する
		/// </summary>
		private void saveToolBar()
		{
			GlobalStatus.EnvOption.ToolBarFilePos = getToolBarPos(fileToolStrip);
			GlobalStatus.EnvOption.ToolBarEditPos = getToolBarPos(editToolStrip);
			GlobalStatus.EnvOption.ToolBarSearchPos = getToolBarPos(searchToolStrip);
			GlobalStatus.EnvOption.ToolBarBookmarkPos = getToolBarPos(bookmarkToolStrip);
			GlobalStatus.EnvOption.ToolBarExecPos = getToolBarPos(execToolStrip);
			GlobalStatus.EnvOption.ToolBarViewPos = getToolBarPos(viewToolStrip);
		}

		/// <summary>
		/// ツールバーオブジェクトからツールバー位置情報を取得する
		/// </summary>
		/// <param name="toolbar"></param>
		/// <returns></returns>
		private ToolBarPos getToolBarPos(ToolStrip toolbar)
		{
			ToolBarPos pos = new ToolBarPos();
			if (this.mainToolStripContainer.TopToolStripPanel.Controls.Contains(toolbar))
			{
				pos.Direction = ToolBarDirection.Top;
			}
			else if (this.mainToolStripContainer.BottomToolStripPanel.Controls.Contains(toolbar))
			{
				pos.Direction = ToolBarDirection.Bottom;
			}
			else if (this.mainToolStripContainer.LeftToolStripPanel.Controls.Contains(toolbar))
			{
				pos.Direction = ToolBarDirection.Left;
			}
			else if (this.mainToolStripContainer.RightToolStripPanel.Controls.Contains(toolbar))
			{
				pos.Direction = ToolBarDirection.Right;
			}
			else
			{
				pos.Direction = ToolBarDirection.Top;
			}
			pos.Location = toolbar.Location;

			return pos;
		}
		#endregion

		#region ドッキングパネル
		/// <summary>
		/// ドッキングパネルの状態をファイルに保存する
		/// </summary>
		private void saveFileDockPanel()
		{
			mainDockPanel.SaveAsXml(ConstEnvOption.DockPanelFilePath);
		}

		/// <summary>
		/// ドッキングパネルの状態をファイルから読み込む
		/// </summary>
		private void loadFileDockPanel()
		{
			mainDockPanel.LoadFromXml(ConstEnvOption.DockPanelFilePath, new WeifenLuo.WinFormsUI.DeserializeDockContent(GetDockPanelContent));
		}

		/// <summary>
		/// ドッキングパネルの状態をクラスタイプからオブジェクトを返す
		/// </summary>
		/// <param name="typeName">取得したいクラス名</param>
		/// <returns>取得したクラスオブジェクト</returns>
		WeifenLuo.WinFormsUI.DockContent GetDockPanelContent(string typeName)
		{
			Debug.WriteLine("typeName=" + typeName);
			if (typeName == typeof(ProjectForm).ToString())
			{
				return GlobalStatus.FormManager.ProjectForm;
			}
			else if (typeName == typeof(SearchResultForm).ToString())
			{
				return GlobalStatus.FormManager.SearchResultForm;
			}
			else if (typeName == typeof(ExecLogForm).ToString())
			{
				return GlobalStatus.FormManager.ExecLogForm;
			}
			else if (typeName == typeof(BookmarkListForm).ToString())
			{
				return GlobalStatus.FormManager.BookmarkListForm;
			}
			else if (typeName == typeof(kag.label.KagLabelForm).ToString())
			{
				return GlobalStatus.FormManager.KagLabelForm;
			}
			else if (typeName == typeof(kkde.kag.image.ImageViewerForm).ToString())
			{
				return GlobalStatus.FormManager.ImageViewerForm;
			}
			else if (typeName == typeof(kkde.kag.sound.SoundPlayerForm).ToString())
			{
				return GlobalStatus.FormManager.SoundPlayerForm;
			}
			else if (typeName == typeof(kkde.kagex.WorldExViewForm).ToString())
			{
				return GlobalStatus.FormManager.WorldExViewForm;
			}
			else if (typeName == typeof(kkde.help.HelpReferenceForm).ToString())
			{
				return GlobalStatus.FormManager.HelpReferenceForm;
			}
			//else if (typeName == typeof(kkde.screen.ScreenMakerForm).ToString())
			//{
			//	return GlobalStatus.FormManager.ScreenEditor;
			//}
			else if (typeName == typeof(kkde.screen.ScreenPropertyForm).ToString())
			{
				return GlobalStatus.FormManager.ScreenProperty;
			}
			else if (typeName == typeof(kkde.screen.ScreenToolForm).ToString())
			{
				return GlobalStatus.FormManager.ScreenToolBox;
			}

			return null;
		}

		/// <summary>
		/// エディタのタブ選択を変更したとき
		/// </summary>
		private void mainDockPanel_ActiveDocumentChanged(object sender, EventArgs e)
		{
			UpdateStatusBar();
		}

		/// <summary>
		/// ドロップできるかどうか判定
		/// </summary>
		private void mainDockPanel_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))	//ドロップされたのがファイルの時
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		/// <summary>
		/// ドックパネルにファイルをドロップしたとき
		/// </summary>
		private void mainDockPanel_DragDrop(object sender, DragEventArgs e)
		{
			//ドロップされたファイル名リストを取得する
			string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			foreach (string fileName in fileList)
			{
				GlobalStatus.EditorManager.LoadFile(fileName);	//エディタを開く
			}
		}
		#endregion

		#region ステータスバー
		/// <summary>
		/// ステータスバー情報のすべてを更新する
		/// </summary>
		public void UpdateStatusBar()
		{
			UpdateStatusBarEncoding(GlobalStatus.EditorManager.ActiveEditor);
			UpdateStatusBarLineTerm(GlobalStatus.EditorManager.ActiveEditor);
			UpdateStatusBarCaretPos(GlobalStatus.EditorManager.ActiveEditor);
			UpdateStatusBarProjectType();
		}

		/// <summary>
		/// ステータスバー情報のすべてを更新する
		/// </summary>
		/// <param name="editor">更新するエディタ</param>
		public void UpdateStatusBar(TextEditorEx editor)
		{
			UpdateStatusBarEncoding(editor);
			UpdateStatusBarLineTerm(editor);
			UpdateStatusBarCaretPos(editor);
			UpdateStatusBarProjectType();
		}

		/// <summary>
		/// ステータスバーの文字コード表示を更新する
		/// </summary>
		public void UpdateStatusBarEncoding(TextEditorEx editor)
		{
			if (editor != null)
			{
				encodingStatusLabel.Text = editor.Encoding.EncodingName;
			}
			else
			{
				encodingStatusLabel.Text = "　　　　　";
			}
		}

		/// <summary>
		/// ステータスバーの改行コード表示を更新する
		/// </summary>
		public void UpdateStatusBarLineTerm(TextEditorEx editor)
		{
			if (editor != null)
			{
				string lineTerm = editor.TextEditorProperties.LineTerminator;
				linetermStatusLabel.Text = EditorOption.GetLineTermName(lineTerm);
			}
			else
			{
				linetermStatusLabel.Text = "　　　　　";
			}
		}

		/// <summary>
		/// ステータスバーのカーソル位置表示を更新する
		/// </summary>
		public void UpdateStatusBarCaretPos(TextEditorEx editor)
		{
			int line, col;
			if (editor != null)
			{
				line = editor.ActiveTextArea.Caret.Line;
				col = editor.ActiveTextArea.Caret.Column;
			}
			else
			{
				line = 0;
				col = 0;
			}

			UpdateStatusBarCaretPos(line + 1, col);	//行は1から開始
		}

		/// <summary>
		/// ステータスバーのカーソル位置を指定して更新する
		/// </summary>
		/// <param name="line">行</param>
		/// <param name="col">桁</param>
		public void UpdateStatusBarCaretPos(int line, int col)
		{
			caretPosStatusLabel.Text = String.Format(" {0, 7}行 {1, 7}桁", line, col);
		}

		/// <summary>
		/// プロジェクトタイプ表示を更新する
		/// </summary>
		public void UpdateStatusBarProjectType()
		{
			string text = "";
			if (GlobalStatus.Project.IsOpened)
			{
				switch (GlobalStatus.Project.Type)
				{
					case ProjectType.Empty:
						text = "Empty";
						break;
					case ProjectType.Kag3:
						text = "KAG3";
						break;
					case ProjectType.Kagexpp:
						text = "KAGEX++";
						break;
				}
			}
			else
			{
				text = "Empty";
			}

			projectTypeStatusLabel.Text = text;
		}
		#endregion

		#region ファイルメニュー
		/// <summary>
		/// ファイル→新規作成→プロジェクト新規作成
		/// </summary>
		private void menuItemFileNewProject_Click(object sender, EventArgs e)
		{
			newCreateForm.ShowDialog();
			if (newCreateForm.CreateProject != null)
			{
				GlobalStatus.FormManager.ProjectForm.LoadProject(newCreateForm.CreateProject);
				GlobalStatus.FormManager.ProjectForm.Show(mainDockPanel);
			}
		}

		/// <summary>
		/// ファイル→新規作成→プロジェクトをインポート
		/// </summary>
		private void menuItemFileNewProjectImport_Click(object sender, EventArgs e)
		{
			ProjectPropertyForm importForm = new ProjectPropertyForm(null);
			importForm.ShowDialog();
			if (importForm.IsUpdate)
			{
				GlobalStatus.FormManager.ProjectForm.LoadProject(importForm.Project);
				GlobalStatus.FormManager.ProjectForm.Show(mainDockPanel);
			}
		}

		/// <summary>
		/// ファイル→新規作成→ファイル新規作成
		/// </summary>
		private void menuItemFileNewFile_Click(object sender, EventArgs e)
		{
			//GlobalStatus.EditorManager.CreateNew();
			AddFileForm addForm = new AddFileForm(false);
			addForm.ShowDialog();
			if (addForm.AddPath != "")	//作成に成功したとき
			{
				GlobalStatus.EditorManager.LoadFile(addForm.AddPath);
			}
		}

		/// <summary>
		/// ファイル→開く→プロジェクトを開く
		/// </summary>
		private void menuItemFileOpenProject_Click(object sender, EventArgs e)
		{
			//プロジェクトを開くためにファイルオープンダイアログを表示する
			OpenFileDialog openDialog = new OpenFileDialog();
			openDialog.Title = "プロジェクトを開く";
			openDialog.Filter = String.Format("プロジェクトファイル (*.{0})|*.{0}", ProjectFile.PROJECT_EXT_NAME);
			openDialog.DefaultExt = ProjectFile.PROJECT_EXT_NAME;
			if (openDialog.ShowDialog() == DialogResult.OK)
			{
				//プロジェクトを読み込み、ツリーを表示する
				GlobalStatus.FormManager.ProjectForm.LoadProject(openDialog.FileName);
				GlobalStatus.FormManager.ProjectForm.Show(mainDockPanel);
			}
		}

		/// <summary>
		/// ファイル→開く→ファイルを開く
		/// </summary>
		private void menuItemFileOpenFile_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.OpenFile();
		}

		/// <summary>
		/// ファイル→上書き保存
		/// </summary>
		private void menuItemFileSave_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.SaveFile();
		}

		/// <summary>
		/// ファイル→名前を付けて保存
		/// </summary>
		private void menuItemFileSaveAs_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.SaveFileAs();
		}

		/// <summary>
		/// ファイル→すべてのファイルを保存
		/// </summary>
		private void menuItemFileSaveAll_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.SaveFileAll();
		}

		/// <summary>
		/// ファイル→閉じる
		/// </summary>
		private void menuItemFileClose_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.CloseFile();
		}

		/// <summary>
		/// ファイル→すべてを閉じる
		/// </summary>
		private void menuItemFileCloseAll_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.CloseFileAll();
		}

		/// <summary>
		/// ファイル→終了
		/// </summary>
		private void menuItemFileExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// ファイルメニューを開こうとしたとき
		/// </summary>
		private void menuItemFile_DropDownOpening(object sender, EventArgs e)
		{
			//現在の履歴を全削除
			menuItemFileHistoryProject.DropDownItems.Clear();
			menuItemFileHistoryFile.DropDownItems.Clear();

			if (GlobalStatus.EnvOption.HistoryFile.List.Count > 0)
			{
				//ファイルの履歴があるときは追加する
				foreach (string path in GlobalStatus.EnvOption.HistoryFile.List)
				{
					menuItemFileHistoryFile.DropDownItems.Add(path, null, menuItemFileHistoryFile_Click);
				}
			}

			if (GlobalStatus.EnvOption.HistoryProject.List.Count > 0)
			{
				//プロジェクトの履歴があるときは追加する
				foreach (string path in GlobalStatus.EnvOption.HistoryProject.List)
				{
					menuItemFileHistoryProject.DropDownItems.Add(path, null, menuItemFileHistoryProject_Click);
				}
			}
		}

		/// <summary>
		/// プロジェクトの履歴が選択されたときに呼ばれるイベント
		/// </summary>
		private void menuItemFileHistoryProject_Click(object sender, EventArgs e)
		{
			string path = sender.ToString();

			//プロジェクトを読み込み、ツリーを表示する
			GlobalStatus.FormManager.ProjectForm.LoadProject(path);
			GlobalStatus.FormManager.ProjectForm.Show(mainDockPanel);
		}

		/// <summary>
		/// ファイルの履歴が選択されたときに呼ばれるイベント
		/// </summary>
		private void menuItemFileHistoryFile_Click(object sender, EventArgs e)
		{
			string path = sender.ToString();

			//ファイルを開く
			GlobalStatus.EditorManager.LoadFile(path);
		}
		#endregion

		#region 編集メニュー
		/// <summary>
		/// 編集→元に戻る
		/// </summary>
		private void menuItemEditUndo_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.Undo();
		}

		/// <summary>
		/// 編集→やり直し
		/// </summary>
		private void menuItemEditRedo_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.Redo();
		}

		/// <summary>
		/// 編集→切り取り
		/// </summary>
		private void menuItemEditCut_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.CutText();
		}

		/// <summary>
		/// 編集→コピー
		/// </summary>
		private void menuItemEditCopy_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.CopyText();
		}

		/// <summary>
		/// 編集→貼り付け
		/// </summary>
		private void menuItemEditPaste_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.PasteText();
		}

		/// <summary>
		/// 編集→削除
		/// </summary>
		private void menuItemEditDelete_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.DeleteText();
		}

		/// <summary>
		/// 編集→すべて選択
		/// </summary>
		private void menuItemEditAllSelect_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.SelectAllText();
		}

		/// <summary>
		/// 編集→コメント化
		/// </summary>
		private void menuItemEditComment_Click(object sender, EventArgs e)
		{
			//GlobalStatus.EditorManager.ToggleComment();
			GlobalStatus.EditorManager.CommentOut();
		}

		/// <summary>
		/// 編集→コメント解除
		/// </summary>
		private void menuItemEditUnComment_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.UnCommentOut();
		}

		/// <summary>
		/// 編集→折りたたむ/展開
		/// </summary>
		private void menuItemEditFoldToggle_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.ToggleFolding();
		}

		/// <summary>
		/// 編集→すべてを折りたたむ/展開
		/// </summary>
		private void menuItemEditFoldTogleAll_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.ToggleFoldingAll();
		}

		/// <summary>
		/// 編集→定義をすべて折りたたむ
		/// </summary>
		private void menuItemEditFoldDefAll_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.FoldingDefAll();
		}
		#endregion

		#region 挿入メニュー
		/// <summary>
		/// タグ挿入画面を表示し、結果をエディタに挿入する
		/// </summary>
		/// <param name="tagInsertForm">表示するタグ挿入画面</param>
		public void TagInsertFormShowDialog(kkde.taginsert.ITagInsertAction tagInsertForm)
		{
			if (tagInsertForm.ShowDialog(global.GlobalStatus.EditorManager.GetSelectedText()) == DialogResult.OK)
			{
				string text = tagInsertForm.GetCode();
				if (text == "")
				{
					return;	//挿入するモノがないので何もしない
				}

				//挿入開始
				GlobalStatus.EditorManager.InsertText(text, tagInsertForm.IsSelectedClear());
			}
		}

		/// <summary>
		/// メッセージ→改行
		/// </summary>
		private void menuItemInsertMessR_Click(object sender, EventArgs e)
		{
			TagInsertFormShowDialog(new kkde.taginsert.message.NewLineInsertForm());
		}

		/// <summary>
		/// メッセージ→クリック待ち
		/// </summary
		private void menuItemInsertMessClickwait_Click(object sender, EventArgs e)
		{
			TagInsertFormShowDialog(new kkde.taginsert.message.ClickWaitInsertForm());
		}

		/// <summary>
		/// メッセージ→メッセージクリア
		/// </summary>
		private void menuItemInsertMessClear_Click(object sender, EventArgs e)
		{
			TagInsertFormShowDialog(new kkde.taginsert.message.ClearInsertForm());
		}

		/// <summary>
		/// メッセージ→フォント
		/// </summary>
		private void menuItemInsertMessFont_Click(object sender, EventArgs e)
		{
			TagInsertFormShowDialog(new kkde.taginsert.message.FontInsertForm());
		}

		/// <summary>
		/// メッセージ→行スタイル
		/// </summary>
		private void menuItemInsertMessStyle_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// メッセージ→表示速度
		/// </summary>
		private void menuItemInsertMessDelay_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// メッセージ→インデント
		/// </summary>
		private void menuItemInsertMessIndent_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// メッセージ→ルビ
		/// </summary>
		private void menuItemInsertMessRuby_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// メッセージ→インライン画像
		/// </summary>
		private void menuItemInsertMessGraph_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// メッセージ→文字位置
		/// </summary>
		private void menuItemInsertMessLocate_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// メッセージ→メッセージレイヤ切り替え
		/// </summary>
		private void menuItemInsertMessCurrent_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// メッセージ→メッセージレイヤ属性
		/// </summary>
		private void menuItemInsertMessOpt_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// レイヤ/画像→画像表示
		/// </summary>
		private void menuItemInsertLayerImage_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// レイヤ/画像→レイヤ属性
		/// </summary>
		private void menuItemInsertLayerOpt_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// レイヤ/画像→レイヤ移動
		/// </summary>
		private void menuItemInsertLayerMove_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// レイヤ/画像→画面揺らし
		/// </summary>
		private void menuItemInsertLayerQuake_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// レイヤ/画像→トランジション
		/// </summary>
		private void menuItemInsertLayerTrans_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// BGM→BGM再生
		/// </summary>
		private void menuItemInsertBgmPlay_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// BGM→BGM停止
		/// </summary>
		private void menuItemInsertBgmStop_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// BGM→BGM停止待ち
		/// </summary>
		private void menuItemInsertBgmWait_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// BGM→BGM音量
		/// </summary>
		private void menuItemInsertBgmOpt_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// BGM→BGM入れ替え
		/// </summary>
		private void menuItemInsertBgmChange_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// BGM→BGMフェード
		/// </summary>
		private void menuItemInsertBgmFade_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// 効果音→効果音再生
		/// </summary>
		private void menuItemInsertSePlay_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// 効果音→効果音停止
		/// </summary>
		private void menuItemInsertSeStop_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// 効果音→効果音停止待ち
		/// </summary>
		private void menuItemInsertSeWait_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// 効果音→効果音音量
		/// </summary>
		private void menuItemInsertSeOpt_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// 効果音→効果音フェード
		/// </summary>
		private void menuItemInsertSeFade_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// ラベル/ジャンプ→ラベル
		/// </summary>
		private void menuItemInsertJumpLabel_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// ラベル/ジャンプ→リンク
		/// </summary>
		private void menuItemInsertJumpLink_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// ラベル/ジャンプ→ボタン
		/// </summary>
		private void menuItemInsertJumpButton_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// ラベル/ジャンプ→サブルーチン呼び出し
		/// </summary>
		private void menuItemInsertJumpCall_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// ラベル/ジャンプ→サブルーチン作成
		/// </summary>
		private void menuItemInsertJumpCreate_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// ラベル/ジャンプ→シナリオジャンプ
		/// </summary>
		private void menuItemInsertJumpScenario_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// ラベル/ジャンプ→マクロ
		/// </summary>
		private void menuItemInsertJumpMacro_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// システム設定→クリックスキップ
		/// </summary>
		private void menuItemInsertSystemClick_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// システム設定→プラグイン読み込み
		/// </summary>
		private void menuItemInsertSystemPlugin_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// システム設定→シナリオ停止
		/// </summary>
		private void menuItemInsertSystemStop_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// システム設定→ウェイト
		/// </summary>
		private void menuItemInsertSystemWait_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// 変数/TJS→TJS式評価
		/// </summary>
		private void menuItemInsertTjsEval_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// 変数/TJS→条件文
		/// </summary>
		private void menuItemInsertTjsIf_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// 変数/TJS→TJSスクリプト
		/// </summary>
		private void menuItemInsertTjsScript_Click(object sender, EventArgs e)
		{

		}
		#endregion

		#region 検索メニュー
		/// <summary>
		/// 検索→検索
		/// </summary>
		private void menuItemSearchFind_Click(object sender, EventArgs e)
		{
			SearchOptionForm form = new SearchOptionForm(GlobalStatus.EnvOption.SearchOption, GlobalStatus.EditorManager.GetSelectedText());
			form.ShowDialog();
			if (form.SearchResult == true)
			{
				GlobalStatus.EditorManager.Search(GlobalStatus.EnvOption.SearchOption);
			}
		}

		/// <summary>
		/// 検索→下方向検索
		/// </summary>
		private void menuItemSearchDown_Click(object sender, EventArgs e)
		{
			editorSearchNext(SearchDirection.Down);
		}
		
		/// <summary>
		/// 検索→上方向検索
		/// </summary>
		private void menuItemSearchUp_Click(object sender, EventArgs e)
		{
			editorSearchNext(SearchDirection.Up);
		}

		/// <summary>
		/// 現在のオプション状態から次を検索する
		/// </summary>
		/// <param name="direction">検索する方向</param>
		private void editorSearchNext(SearchDirection direction)
		{
			GlobalStatus.EnvOption.SearchOption.Direction = direction;
			switch (GlobalStatus.EnvOption.SearchOption.Type)
			{
				case SearchType.Search:
					GlobalStatus.EditorManager.Search(GlobalStatus.EnvOption.SearchOption);		//検索する
					break;
				case SearchType.Replace:
					GlobalStatus.EditorManager.Replace(GlobalStatus.EnvOption.SearchOption);	//置換する
					break;
			}
		}

		/// <summary>
		/// 検索→置換
		/// </summary>
		private void menuItemSearchReplace_Click(object sender, EventArgs e)
		{
			ReplaceOptionForm form = new ReplaceOptionForm(GlobalStatus.EnvOption.SearchOption, GlobalStatus.EditorManager.GetSelectedText());
			form.ShowDialog();
			if (form.SearchResult == true)
			{
				switch (GlobalStatus.EnvOption.SearchOption.Type)
				{
					case SearchType.ReplaceAll:
						GlobalStatus.EditorManager.ReplaceAll(GlobalStatus.EnvOption.SearchOption);
						break;
					case SearchType.Replace:
						GlobalStatus.EditorManager.Replace(GlobalStatus.EnvOption.SearchOption);
						break;
				}
			}
		}

		/// <summary>
		/// 検索→GREP検索
		/// </summary>
		private void menuItemSearchGrep_Click(object sender, EventArgs e)
		{
			GrepOptionForm form = new GrepOptionForm(GlobalStatus.EnvOption.SearchOption, GlobalStatus.EditorManager.GetSelectedText());
			form.ShowDialog();
			if (form.SearchResult == true)
			{
				GlobalStatus.FormManager.SearchResultForm.Show(mainDockPanel);
				GlobalStatus.FormManager.SearchResultForm.Grep(GlobalStatus.EnvOption.SearchOption);	//検索開始
			}
		}

		/// <summary>
		/// 検索→ジャンプ
		/// </summary>
		private void menuItemSearchJump_Click(object sender, EventArgs e)
		{
			JumpOptionForm form = new JumpOptionForm();
			form.ShowDialog();
			if (form.LineNumber != -1)
			{
				GlobalStatus.EditorManager.MoveCaretLine(form.LineNumber - 1);
			}
		}

		/// <summary>
		/// 検索→ブックマーク登録/解除
		/// </summary>
		private void menuItemSearchBookmarkEnt_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.ToggleBookmarkSelectedLine();
		}

		/// <summary>
		/// 検索→前のブックマーク
		/// </summary>
		private void menuItemSearchBookmarkPrev_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.MovePrevBookmark();
		}

		/// <summary>
		/// 検索次のブックマーク
		/// </summary>
		private void menuItemSearchBookmarkNext_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.MoveNextBookmark();
		}

		/// <summary>
		/// 検索→ブックマークをすべて解除
		/// </summary>
		private void menuItemSearchBookmarkClear_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("現在登録しているブックマークをすべて削除します。\n本当に削除しますか？"
				, "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
				 == DialogResult.Yes)
			{
				GlobalStatus.EditorManager.ClearAllBookmark();
			}
		}

		/// <summary>
		/// 検索→対応する括弧へ移動
		/// </summary>
		private void menuItemSearchKakko_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.MoveMatchingKakko();
		}
		#endregion

		#region 実行メニュー
		/// <summary>
		/// 実行→デバッグ開始
		/// </summary>
		private void menuItemExecStart_Click(object sender, EventArgs e)
		{
			GlobalStatus.FormManager.ExecLogForm.Init();
			GlobalStatus.FormManager.ExecLogForm.Show(mainDockPanel);
			if (GlobalStatus.EnvOption.ExecSaveAll)
			{
				GlobalStatus.EditorManager.SaveFileAll();	//全保存を行う
			}
			GlobalStatus.KrkrProc.Start(GlobalStatus.Project, KrkrProcess.StartMode.Debug);
		}

		/// <summary>
		/// 実行→ラベルからデバッグ実行
		/// </summary>
		private void menuItemExecStartLabel_Click(object sender, EventArgs e)
		{
			GlobalStatus.FormManager.ExecLogForm.Init();
			GlobalStatus.FormManager.ExecLogForm.Show(mainDockPanel);
			if (GlobalStatus.EnvOption.ExecSaveAll)
			{
				GlobalStatus.EditorManager.SaveFileAll();	//全保存を行う
			}
			GlobalStatus.LabelExecMgr.ExecLabelJump();
		}

		/// <summary>
		/// ログウィンドウを表示する
		/// </summary>
		private void execKrkrLogView()
		{
			if (GlobalStatus.EnvOption.ExecOpenLog)
			{
				GlobalStatus.FormManager.ExecLogForm.Show(mainDockPanel);
				GlobalStatus.FormManager.ExecLogForm.LoadFile();
			}
		}

		/// <summary>
		/// 実行→デバッグ中止
		/// </summary>
		private void menuItemExecStop_Click(object sender, EventArgs e)
		{
			GlobalStatus.KrkrProc.Kill();
		}

		/// <summary>
		/// 実行→リリース
		/// </summary>
		private void menuItemExecRelease_Click(object sender, EventArgs e)
		{
			ReleaseProcess proc = new ReleaseProcess();
			proc.Start(GlobalStatus.Project);
		}

		/// <summary>
		/// 実行メニューを開こうとしたとき
		/// </summary>
		private void menuItemExec_DropDownOpening(object sender, EventArgs e)
		{
			if (GlobalStatus.KrkrProc.HasExited)
			{
				menuItemExecStop.Enabled = false;	//吉里吉里を実行していないときは強制終了ボタンを押せないようにする
			}
			else
			{
				menuItemExecStop.Enabled = true;
			}
		}
		#endregion

		#region マクロメニュー
		/// <summary>
		/// マクロ→マクロ開始
		/// </summary>
		private void menuItemMacroStart_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// マクロ→マクロ登録
		/// </summary>
		private void menuItemMacroAdd_Click(object sender, EventArgs e)
		{

		}
		#endregion

		#region 設定メニュー
		/// <summary>
		/// 設定→環境設定
		/// </summary>
		private void menuItemOptionEnv_Click(object sender, EventArgs e)
		{
			option.EnvOptionForm form = new EnvOptionForm();
			form.ShowDialog();
		}

		/// <summary>
		/// 設定→ファイルタイプ別設定→KAGシナリオ
		/// </summary>
		private void menuItemOptionColorKag_Click(object sender, EventArgs e)
		{
			option.TypeOptionForm form = new option.TypeOptionForm(FileType.KrkrType.Kag);
			form.ShowDialog();
		}

		/// <summary>
		/// 設定→ファイルタイプ別設定→TJSスクリプト
		/// </summary>
		private void menuItemOptionColorTjs_Click(object sender, EventArgs e)
		{
			option.TypeOptionForm form = new option.TypeOptionForm(FileType.KrkrType.Tjs);
			form.ShowDialog();
		}

		/// <summary>
		/// 設定→ファイルタイプ別設定→その他のファイル
		/// </summary>
		private void menuItemOptionColorOther_Click(object sender, EventArgs e)
		{
			option.TypeOptionForm form = new option.TypeOptionForm(FileType.KrkrType.Unknown);
			form.ShowDialog();
		}
		#endregion

		#region 表示メニュー
		/// <summary>
		/// 表示→テキストエディタ
		/// </summary>
		private void menuItemViewEditor_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.Show();
		}

		/// <summary>
		/// 表示→次のエディタ
		/// </summary>
		private void menuItemViewEditorNext_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.ShowNextEditor();
		}

		/// <summary>
		/// 表意→前のエディタ
		/// </summary>
		private void menuItemViewEditorPrev_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.ShowPrevEditor();
		}

		/// <summary>
		/// 表示→エディタを分割
		/// </summary>
		private void menuItemViewEditorSplit_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.ShowSplit();
		}

		/// <summary>
		/// 表示→プロジェクト
		/// </summary>
		private void menuItemViewProject_Click(object sender, EventArgs e)
		{
			GlobalStatus.FormManager.ProjectForm.Show(mainDockPanel);
		}

		/// <summary>
		/// 表示→検索結果ウィンドウ
		/// </summary>
		private void menuItemViewSearchResult_Click(object sender, EventArgs e)
		{
			GlobalStatus.FormManager.SearchResultForm.Show(mainDockPanel);
		}

		/// <summary>
		/// 表示→実行ログウィンドウ
		/// </summary>
		private void menuItemViewLog_Click(object sender, EventArgs e)
		{
			GlobalStatus.FormManager.ExecLogForm.Show(mainDockPanel);
		}

		/// <summary>
		/// 表示→ブックマークウィンドウ
		/// </summary>
		private void menuItemViewBookmark_Click(object sender, EventArgs e)
		{
			GlobalStatus.FormManager.BookmarkListForm.Show(mainDockPanel);
		}

		/// <summary>
		/// 表示→ラベルツリーウィンドウ
		/// </summary>
		private void menuItemViewLabel_Click(object sender, EventArgs e)
		{
			GlobalStatus.FormManager.KagLabelForm.Show(mainDockPanel);
		}

		/// <summary>
		/// イメージビューワー
		/// </summary>
		private void menuItemViewImage_Click(object sender, EventArgs e)
		{
			GlobalStatus.FormManager.ImageViewerForm.Show(mainDockPanel);
		}

		/// <summary>
		/// サウンドプレイヤー
		/// </summary>
		private void menuItemViewSound_Click(object sender, EventArgs e)
		{
			GlobalStatus.FormManager.SoundPlayerForm.Show(mainDockPanel);
		}

		/// <summary>
		/// KAGEXワールド拡張ビューワー
		/// </summary>
		private void menuItemViewKagexView_Click(object sender, EventArgs e)
		{
			GlobalStatus.FormManager.WorldExViewForm.InitView();
			GlobalStatus.FormManager.WorldExViewForm.Show(mainDockPanel);
		}

		/// <summary>
		/// KAGEXワールド拡張プレビューエディタ
		/// </summary>
		private void menuItemViewKagexPreview_Click(object sender, EventArgs e)
		{
			GlobalStatus.FormManager.WorldExPReviewAttrFromShow();
		}

		/// <summary>
		/// KAGEXワールド拡張エディタ
		/// </summary>
		private void menuItemViewKagexEditor_Click(object sender, EventArgs e)
		{
			//GlobalStatus.FormManager.WorldExEditorForm.Init();
			//GlobalStatus.FormManager.WorldExEditorForm.Show(mainDockPanel);
		}

		/// <summary>
		/// KAGEXアクションエディタ
		/// </summary>
		private void menuItemViewKagexAction_Click(object sender, EventArgs e)
		{
			GlobalStatus.FormManager.WorldExActionEditorFormShow();
		}

		/// <summary>
		/// KAGEX++スクリーンエディタ
		/// </summary>
		private void menuItemViewKagexScreen_Click(object sender, EventArgs e)
		{
		//	GlobalStatus.FormManager.ScreenEditor.Show(mainDockPanel);
		}

		/// <summary>
		/// KAGEX++スクリーンプロパティ
		/// </summary>
		private void menuItemViewKagexScreenProperty_Click(object sender, EventArgs e)
		{
			GlobalStatus.FormManager.ScreenProperty.Show(mainDockPanel);
		}

		/// <summary>
		/// KAGEX++スクリーンツールボックス
		/// </summary>
		private void menuItemViewKagexScreenToolbox_Click(object sender, EventArgs e)
		{
			GlobalStatus.FormManager.ScreenToolBox.Show(mainDockPanel);
		}
		#endregion

		#region ヘルプメニュー
		/// <summary>
		/// ヘルプ→マニュアル
		/// </summary>
		private void menuItemHelpManual_Click(object sender, EventArgs e)
		{
			util.Msgbox.Info("マニュアルは未実装です\nサポートサイトにいくつか資料があるかもしれません");
		}

		/// <summary>
		/// ヘルプ→KAGリファレンス
		/// </summary>
		private void menuItemHelpKag_Click(object sender, EventArgs e)
		{
			Process.Start(ConstEnvOption.Kag3ManualPath);
		}

		/// <summary>
		/// ヘルプ→TJSリファレンス
		/// </summary>
		private void menuItemHelpTjs_Click(object sender, EventArgs e)
		{
			Process.Start(ConstEnvOption.Tjs2ManualPath);
		}

		/// <summary>
		/// ヘルプ→吉里吉里リファレンス
		/// </summary>
		private void menuItemHelpKrkr_Click(object sender, EventArgs e)
		{
			Process.Start(ConstEnvOption.Krkr2ManualPath);
		}

		/// <summary>
		/// ヘルプ→バージョン情報
		/// </summary>
		private void menuItemHelpVersion_Click(object sender, EventArgs e)
		{
			versionForm.StartPosition = FormStartPosition.CenterParent;
			versionForm.ShowDialog();
		}
		#endregion

		#region デバッグメニュー
		/// <summary>
		/// 設定ファイル格納ディレクトリを開く
		/// </summary>
		private void menuItemDebugHideOpenOptionDir_Click(object sender, EventArgs e)
		{
			Process.Start(EnvOption.AppDataPath);
		}

		/// <summary>
		/// デバッグ1
		/// </summary>
		private void menuItemDebugTest1_Click(object sender, EventArgs e)
		{
			if (GlobalStatus.EditorManager.ActiveEditor == null)
			{
				//画面がないので何もしない
				return;
			}

			string filePath = GlobalStatus.EditorManager.ActiveEditor.FileName;
			if (FileType.GetKrkrType(filePath) != FileType.KrkrType.Kag)
			{
				//KAGシナリオじゃないのでラベルがない
				return;
			}

			int line = GlobalStatus.EditorManager.ActiveEditor.ActiveTextArea.Caret.Line;

			//ラベルリストから直近のラベルを取得する
			kkde.parse.KagLabelItem[] labelList = GlobalStatus.ParserSrv.GetLabelList(filePath);
			kkde.parse.KagLabelItem jumpLabel = null;
			if (labelList != null)
			{
				foreach (kkde.parse.KagLabelItem label in labelList)
				{
					if (label.LineNumber > line)
					{
						break;
					}
					jumpLabel = label;
				}
			}

			//ラベルが取得できたときはセットする（無いときは空文字とする）
			string jumpLabelText = "";
			if (jumpLabel != null)
			{
				jumpLabelText = jumpLabel.LabelName;
			}

			Debug.WriteLine(String.Format("FileName={0}, label={1}", Path.GetFileName(filePath), jumpLabelText));
		}

		/// <summary>
		/// デバッグ2
		/// </summary>
		private void menuItemDebugTest2_Click(object sender, EventArgs e)
		{
			GlobalStatus.FormManager.HelpReferenceForm.Show(mainDockPanel);
			GlobalStatus.FormManager.HelpReferenceForm.Navigate("http://www.google.co.jp/ig?hl=ja");
		}
		#endregion
	}
}
