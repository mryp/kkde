using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using kkde.global;
using kkde.editor;

namespace kkde.project
{
	/// <summary>
	/// プロジェクトツリーを表示するフォーム
	/// </summary>
	public partial class ProjectForm : WeifenLuo.WinFormsUI.DockContent
	{
		#region 定数
		/// <summary>
		/// ツリーノード追加時のダミーノードテキスト
		/// </summary>
		private const string DUMMY_TEXT = "dummy";

		/// <summary>
		/// ツリーノードに追加されるタグ
		/// </summary>
		private enum TreeItemTag
		{
			/// <summary>
			/// プロジェクト名ノード
			/// </summary>
			Project,

			/// <summary>
			/// ファイルノード
			/// </summary>
			File,

			/// <summary>
			/// ディレクトリノード
			/// </summary>
			Directory,
		}

		#region ツリーアイコンのインデックス値
		/// <summary>
		/// フォルダの閉じているときの画像インデックス
		/// </summary>
		private const int IMAGE_INDEX_DIR_CLOSE = 0;

		/// <summary>
		/// フォルダの開いているときの画像インデックス
		/// </summary>
		private const int IMAGE_INDEX_DIR_OPEN = 1;

		/// <summary>
		/// その他ファイルの画像インデックス
		/// </summary>
		private const int IMAGE_INDEX_MISC = 2;

		/// <summary>
		/// 画像ファイルの画像インデックス
		/// </summary>
		private const int IMAGE_INDEX_IMAGE = 3;

		/// <summary>
		/// KAGファイルの画像インデックス
		/// </summary>
		private const int IMAGE_INDEX_KAG = 4;

		/// <summary>
		/// TJSファイルの画像インデックス
		/// </summary>
		private const int IMAGE_INDEX_TJS = 5;

		/// <summary>
		/// テキストファイルの画像インデックス
		/// </summary>
		private const int IMAGE_INDEX_TEXT = 6;

		/// <summary>
		/// プロジェクトの画像インデックス
		/// </summary>
		private const int IMAGE_INDEX_PROJECT = 7;
		#endregion
		#endregion

		#region
		#endregion

		#region プロパティ
		#endregion

		#region 初期化・終了メソッド
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ProjectForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// プロジェクトを読み込む（プロジェクトファイルのパスを指定する）
		/// </summary>
		/// <param name="path">プロジェクトファイルのパス</param>
		public void LoadProject(string path)
		{
			try
			{
				CloseProject();	//いったん閉じる
				GlobalStatus.Project.Load(path);
				loadProject();
			}
			catch (Exception err)
			{
				util.Msgbox.Error("プロジェクトの読み込みに失敗しました\n" + err.Message);
			}
		}

		/// <summary>
		/// プロジェクトを読み込む（プロジェクトのオブジェクトを指定する）
		/// </summary>
		/// <param name="project">プロジェクトのオブジェクト</param>
		public void LoadProject(ProjectFile project)
		{
			CloseProject();	//いったん閉じる

			GlobalStatus.Project = project;
			loadProject();
		}

		/// <summary>
		/// プロジェクトを読み込む（現在設定されているプロジェクトから読み込む）
		/// </summary>
		private void loadProject()
		{
			GlobalStatus.EnvOption.LastProjectPath = GlobalStatus.Project.FilePath;	//設定に保存する
			GlobalStatus.EnvOption.HistoryProject.Add(GlobalStatus.Project.FilePath);
			GlobalStatus.ParserSrv.ClearCompetionUnit();	//いったんすべてクリアーする
			GlobalStatus.ParserSrv.ParseDirectory(GlobalStatus.Project.DataFullPath, GlobalStatus.Project.Type);
			GlobalStatus.FormManager.MainForm.UpdateStatusBarProjectType();

			ChangeOption();
			loadOpenedProjectInfo();
			initTree();
		}

		/// <summary>
		/// フォルダツリーを構築する
		/// </summary>
		private void initTree()
		{
			//ルートフォルダの取得
			string path = GlobalStatus.Project.DataFullPath;
			if (Directory.Exists(path) == false)
			{
				return;
			}

			//ツリーの初期化
			fileTreeView.Nodes.Clear();

			//プロジェクト名をルートノードとして作成する
			TreeNode rootNode = fileTreeView.Nodes.Add(GlobalStatus.Project.Name
				, GlobalStatus.Project.Name, IMAGE_INDEX_PROJECT, IMAGE_INDEX_PROJECT);
			rootNode.Tag = TreeItemTag.Project;
			rootNode.Nodes.Add(DUMMY_TEXT);
			rootNode.Expand();	//一階層だけ開く
		}

		/// <summary>
		/// プロジェクトを閉じる
		/// </summary>
		public void CloseProject()
		{
			saveOpenedProjectInfo();
		}

		/// <summary>
		/// 現在開いているプロジェクト関連の情報を保存する
		/// </summary>
		private void saveOpenedProjectInfo()
		{
			if (GlobalStatus.Project.IsOpened == false)	
			{
				return;	//プロジェクトを開いていないときは何もしない
			}

			//ブックマークリスト
			ProjectBookmarkTable bookmarkTable = GlobalStatus.EnvOption.GetProjectBookmarkTable(GlobalStatus.Project.FilePath);
			bookmarkTable.List.Clear();
			bookmarkTable.List.AddRange(GlobalStatus.FormManager.BookmarkListForm.GetBookmarkList());

			//開いているファイルリスト
			ProjectStringTable table = GlobalStatus.EnvOption.GetProjectOpenedTable(GlobalStatus.Project.FilePath);
			table.List.Clear();
			table.List.AddRange(GlobalStatus.EditorManager.GetOpenedFileList());
		}

		/// <summary>
		/// 現在開いているプロジェクト関連の情報を読み取りそれぞれにセットする
		/// </summary>
		private void loadOpenedProjectInfo()
		{
			if (GlobalStatus.Project.IsOpened == false)
			{
				return;	//プロジェクトが開いていないと読み取れないので何もしない
			}

			//ブックマークリストを取得
			ProjectBookmarkTable bookmarkTable = GlobalStatus.EnvOption.GetProjectBookmarkTable(GlobalStatus.Project.FilePath);
			GlobalStatus.FormManager.BookmarkListForm.SetBookmarkList(bookmarkTable.List.ToArray());

			//開いているファイルリストを取得
			if (GlobalStatus.EnvOption.ProjectOpenedLastFile)
			{
				ProjectStringTable table = GlobalStatus.EnvOption.GetProjectOpenedTable(GlobalStatus.Project.FilePath);
				foreach (string path in table.List)
				{
					if (File.Exists(path))	//ファイルがあるときだけファイルを開く
					{
						GlobalStatus.EditorManager.LoadFile(path);	//エディタを開く
					}
				}
			}
		}

		/// <summary>
		/// オプション状況を反映する
		/// </summary>
		public void ChangeOption()
		{
			if (GlobalStatus.EnvOption.ProjectWatchProjectPath == false
			||  Directory.Exists(GlobalStatus.Project.DataFullPath) == false)
			{
				projectDirWatcher.EnableRaisingEvents = false;
			}
			else
			{
				//プロジェクトパスの監視を開始
				projectDirWatcher.Path = GlobalStatus.Project.DataFullPath;
				projectDirWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName;
				projectDirWatcher.EnableRaisingEvents = true;
			}
		}
		#endregion

		#region ツリーイベント
		/// <summary>
		/// マウスボタンを押したとき
		/// </summary>
		private void fileTreeView_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Left)
			{
				fileTreeView.SelectedNode = fileTreeView.GetNodeAt(e.X, e.Y);
			}
		}

		/// <summary>
		/// ツリーを開こうとしたとき
		/// </summary>
		private void fileTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			//ダミーを削除する
			TreeNode node = e.Node;
			node.Nodes.Clear();

			//ディレクトリが存在するときはそのディレクトリ内を読み取りツリーに追加する
			string path = getNodeFullPath(node);
			if (Directory.Exists(path))
			{
				fileTreeView.BeginUpdate();

				//フォルダの追加
				DirectoryInfo dirInfo = new DirectoryInfo(path);
				DirectoryInfo[] dirs = dirInfo.GetDirectories();

				//フォルダ名順に並ぶようにソート
				List<string> dirNameList = new List<string>();
				foreach (DirectoryInfo info in dirs)
				{
					//隠し属性ではないときだけ追加する
					if ((info.Attributes & FileAttributes.Hidden) == 0)
					{
						dirNameList.Add(Path.GetFileName(info.FullName));
					}
				}
				dirNameList.Sort();
				
				//フォルダをツリーに追加する
				foreach (string name in dirNameList)
				{
					TreeNode n = new TreeNode(name, IMAGE_INDEX_DIR_CLOSE, IMAGE_INDEX_DIR_OPEN);
					n.Nodes.Add(DUMMY_TEXT);
					n.Tag = TreeItemTag.Directory;
					node.Nodes.Add(n);
				}

				//ファイルの追加
				FileInfo[] files = dirInfo.GetFiles();

				//ファイル名順で並ぶようにソート
				List<string> fileNameList = new List<string>();
				foreach (FileInfo info in files)
				{
					if ((info.Attributes & FileAttributes.Hidden) == 0)
					{
						fileNameList.Add(Path.GetFileName(info.FullName));
					}
				}
				fileNameList.Sort();
				
				//ファイルをツリーに追加する
				foreach (string name in fileNameList)
				{
					addNodeFile(node, name);
				}

				fileTreeView.EndUpdate();
			}
		}

		/// <summary>
		/// ファイルノードを追加する
		/// </summary>
		/// <param name="parentNode">追加される親ノード</param>
		/// <param name="fileName">追加するファイル名</param>
		private void addNodeFile(TreeNode parentNode, string fileName)
		{
			int imageIndex;
			switch (FileType.GetKrkrType(Path.Combine(getNodeFullPath(parentNode), fileName)))
			{
				case FileType.KrkrType.Kag:
					imageIndex = IMAGE_INDEX_KAG;
					break;
				case FileType.KrkrType.Tjs:
					imageIndex = IMAGE_INDEX_TJS;
					break;
				case FileType.KrkrType.Text:
					imageIndex = IMAGE_INDEX_TEXT;
					break;
				case FileType.KrkrType.Image:
					imageIndex = IMAGE_INDEX_IMAGE;
					break;
				default:
					imageIndex = IMAGE_INDEX_MISC;
					break;
			}

			TreeNode n = new TreeNode(fileName, imageIndex, imageIndex);
			n.Tag = TreeItemTag.File;
			parentNode.Nodes.Add(n);
		}

		/// <summary>
		/// ツリーのフルパスから実ファイル・フォルダのフルパスを生成し返す
		/// </summary>
		/// <param name="nodeFullPath">フルパスを取得したいノード</param>
		/// <returns>実ファイル・フォルダのフルパス</returns>
		private string getNodeFullPath(TreeNode node)
		{
			if (node == null)
			{
				return "";
			}

			//ノードのフルパスのトップにはプロジェクト名が存在するため、その部分を削除する
			string nodeFullPath = node.FullPath;
			string dirPath = GlobalStatus.Project.DataFullPath;
			int sepPos = nodeFullPath.IndexOf(fileTreeView.PathSeparator);
			if (sepPos == -1)
			{
				return dirPath;
			}

			//プロジェクト名以降のノードパスとプロジェクトデータパスをくっつける
			string nodePath = nodeFullPath.Substring(sepPos + 1);
			return Path.Combine(dirPath, nodePath);
		}

		/// <summary>
		/// アイテムをダブルクリックしたとき
		/// </summary>
		private void fileTreeView_DoubleClick(object sender, EventArgs e)
		{
			TreeNode node = fileTreeView.SelectedNode;

			//ダブルクリックではファイルのみ開く動作を行う
			if ((TreeItemTag)node.Tag == TreeItemTag.File)
			{
				//ファイルを開く
				openFile(node);
			}
		}
		#endregion

		#region ポップアップメニュー
		/// <summary>
		/// ポップアップメニューを開こうとしたとき
		/// ・選択項目により表示・非表示を設定する
		/// </summary>
		private void treePopMenu_Opening(object sender, CancelEventArgs e)
		{
			TreeNode node = fileTreeView.SelectedNode;

			if (node == null)	//何も選択していないとき
			{
				treeMenuItemOpen.Visible = false;
				treeMenuItemAdd.Visible = false;
				treeMenuItemDelete.Visible = false;
				treeMenuItemChangeName.Visible = false;
				treeMenuItemSep1.Visible = false;
				treeMenuItemUpdate.Visible = true;
				treeMenuItemProperty.Visible = false;
			}
			else if ((TreeItemTag)node.Tag == TreeItemTag.Project)
			{
				treeMenuItemOpen.Visible = true;
				treeMenuItemAdd.Visible = true;
				treeMenuItemDelete.Visible = false;
				treeMenuItemChangeName.Visible = false;
				treeMenuItemSep1.Visible = true;
				treeMenuItemUpdate.Visible = true;
				treeMenuItemProperty.Visible = true;
			}
			else if ((TreeItemTag)node.Tag == TreeItemTag.File)
			{
				treeMenuItemOpen.Visible = true;
				treeMenuItemAdd.Visible = false;
				treeMenuItemDelete.Visible = true;
				treeMenuItemChangeName.Visible = true;
				treeMenuItemSep1.Visible = true;
				treeMenuItemUpdate.Visible = true;
				treeMenuItemProperty.Visible = true;
			}
			else if ((TreeItemTag)node.Tag == TreeItemTag.Directory)
			{
				treeMenuItemOpen.Visible = true;
				treeMenuItemAdd.Visible = true;
				treeMenuItemDelete.Visible = true;
				treeMenuItemChangeName.Visible = true;
				treeMenuItemSep1.Visible = true;
				treeMenuItemUpdate.Visible = true;
				treeMenuItemProperty.Visible = true;
			}
		}

		/// <summary>
		/// 開く
		/// </summary>
		private void treeMenuItemOpen_Click(object sender, EventArgs e)
		{
			TreeNode node = fileTreeView.SelectedNode;

			string fullPath = getNodeFullPath(node);
			if ((TreeItemTag)node.Tag == TreeItemTag.Project)
			{
				Process.Start(fullPath);	//エクスプローラーで開く
			}
			else if ((TreeItemTag)node.Tag == TreeItemTag.File)
			{
				openFile(fullPath);
			}
			else if ((TreeItemTag)node.Tag == TreeItemTag.Directory)
			{
				Process.Start(fullPath);	//エクスプローラーで開く
			}
		}

		/// <summary>
		/// ファイルを開く
		/// </summary>
		/// <param name="node">開きたいアイテムを表しているノードオブジェクト</param>
		private void openFile(TreeNode node)
		{
			string fullPath = getNodeFullPath(node);
			openFile(fullPath);
		}

		/// <summary>
		/// ファイルを開く
		/// </summary>
		/// <param name="fullPath">開くファイルのフルパス</param>
		private void openFile(string fullPath)
		{
			switch (FileType.GetKrkrType(fullPath))
			{
				case FileType.KrkrType.Kag:
				case FileType.KrkrType.Tjs:
				case FileType.KrkrType.Text:
					//エディタで開く
					GlobalStatus.EditorManager.LoadFile(fullPath);
					break;
				case FileType.KrkrType.Image:
					//イメージビューワーで開く
					GlobalStatus.FormManager.ImageViewerForm.ShowImage(fullPath, true);
					break;
				case FileType.KrkrType.Sound:
					//サウンドプレイヤーで開く
					GlobalStatus.FormManager.SoundPlayerForm.ShowPlay(fullPath, true);
					break;
				default:
					//エディタで開く
					GlobalStatus.EditorManager.LoadFile(fullPath);
					break;
			}
		}

		/// <summary>
		/// 追加
		/// </summary>
		private void treeMenuItemAdd_Click(object sender, EventArgs e)
		{
			TreeNode node = fileTreeView.SelectedNode;

			switch ((TreeItemTag)node.Tag)
			{
				case TreeItemTag.Project:
				case TreeItemTag.Directory:
					AddFileForm addForm = new AddFileForm(getNodeFullPath(node), true);
					addForm.ShowDialog();
					if (addForm.AddPath != "")	//作成に成功したとき
					{
						addNodeFile(fileTreeView.SelectedNode, addForm.AddFileName);
						if (node.IsExpanded == true)
						{
							node.Collapse();	//開いているときはいったん閉じる
							node.Expand();
						}
					}
					break;
				default:
					//何もしない
					break;
			}

		}

		/// <summary>
		/// 削除
		/// </summary>
		private void treeMenuItemDelete_Click(object sender, EventArgs e)
		{
			TreeNode node = fileTreeView.SelectedNode;

			try
			{
				string fullPath = getNodeFullPath(node);
				if ((TreeItemTag)node.Tag == TreeItemTag.File)
				{
					if (MessageBox.Show("完全に削除されます。\nファイルを本当に削除しますか？", "警告"
						, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
					{
						File.Delete(fullPath);
					}
				}
				else if ((TreeItemTag)node.Tag == TreeItemTag.Directory)
				{
					if (MessageBox.Show("フォルダ内にあるファイルを含めすべて完全に削除されます。\nディレクトリを本当に削除しますか？", "警告"
						, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
					{
						Directory.Delete(fullPath, true);
					}
				}
				else
				{
					return;
				}

				//更新
				if (node.Parent.IsExpanded == true)
				{
					node.Parent.Collapse();	//開いているときはいったん閉じる
					node.Parent.Expand();
				}
			}
			catch (IOException ioerr)
			{
				util.Msgbox.Error("削除できませんでした。\n" + ioerr.Message);
			}
			catch (Exception err)
			{
				util.Msgbox.Error("削除に失敗しました。\n" + err.Message);
			}
		}

		/// <summary>
		/// 名前を変更
		/// </summary>
		private void treeMenuItemChangeName_Click(object sender, EventArgs e)
		{
			TreeNode node = fileTreeView.SelectedNode;

			if (((TreeItemTag)node.Tag == TreeItemTag.File)
			|| (TreeItemTag)node.Tag == TreeItemTag.Directory)
			{
				//ツリーラベル変更開始
				fileTreeView.LabelEdit = true;
				node.BeginEdit();
			}
		}

		/// <summary>
		/// ラベル変更が完了したとき
		/// </summary>
		private void fileTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			fileTreeView.LabelEdit = false;
			e.Node.EndEdit(false);
			if (e.Label == null)
			{
				return;
			}
			if (e.Label == "")
			{
				util.Msgbox.Error("変更する名前を入力してください");
				return;
			}

			try
			{
				//変更を実際のファイル名変更に反映する
				string fullPath = getNodeFullPath(e.Node);
				if (File.Exists(fullPath))
				{
					File.Move(fullPath, Path.Combine(Path.GetDirectoryName(fullPath), e.Label));
				}
				else if (Directory.Exists(fullPath))
				{
					Directory.Move(fullPath, Path.Combine(Path.GetDirectoryName(fullPath), e.Label));
				}
			}
			catch (Exception err)
			{
				e.CancelEdit = true;
				util.Msgbox.Error("名前を変更できません\n" + err.Message);
			}
		}

		/// <summary>
		/// 最新の情報に更新
		/// </summary>
		private void treeMenuItemUpdate_Click(object sender, EventArgs e)
		{
			TreeNode node = fileTreeView.SelectedNode;

			if (node == null)	//何も選択していないとき
			{
				initTree();
			}
			else if ((TreeItemTag)node.Tag == TreeItemTag.Project)
			{
				if (node.IsExpanded == true)
				{
					node.Collapse();	//開いているときはいったん閉じる
					node.Expand();
				}
			}
			else if ((TreeItemTag)node.Tag == TreeItemTag.File)
			{
				if (node.Parent != null)
				{
					//上のフォルダを更新する
					node.Parent.Collapse();	//開いているときはいったん閉じる
					node.Parent.Expand();
				}
			}
			else if ((TreeItemTag)node.Tag == TreeItemTag.Directory)
			{
				if (node.IsExpanded == true)
				{
					node.Collapse();	//開いているときはいったん閉じる
					node.Expand();
				}
			}
		}

		/// <summary>
		/// プロパティ
		/// </summary>
		private void treeMenuItemProperty_Click(object sender, EventArgs e)
		{
			TreeNode node = fileTreeView.SelectedNode;

			string fullPath = getNodeFullPath(node);
			if (((TreeItemTag)node.Tag == TreeItemTag.File)
			|| ((TreeItemTag)node.Tag == TreeItemTag.Directory))
			{
				//プロパティフォームを表示する
				FilePropertyForm propertyForm = new FilePropertyForm(fullPath);
				propertyForm.ShowDialog();
			}
			else if ((TreeItemTag)node.Tag == TreeItemTag.Project)
			{
				//プロジェクト変更フォームを表示する
				ProjectPropertyForm propertyForm = new ProjectPropertyForm(GlobalStatus.Project);
				propertyForm.ShowDialog();
				if (propertyForm.IsUpdate)
				{
					//更新があったときは再度ツリーを構築し直す
					initTree();
					GlobalStatus.FormManager.MainForm.UpdateStatusBarProjectType();
				}
			}
		}

		private void treeMenuItemCopyName_Click(object sender, EventArgs e)
		{
			TreeNode node = fileTreeView.SelectedNode;
			if (node == null)	//何も選択していないとき
			{
				return;	//何もしない
			}

			string filePath = getNodeFullPath(node);
			string copyText = "";
			switch (FileType.GetKrkrType(filePath))
			{
				case FileType.KrkrType.Image:
				case FileType.KrkrType.Sound:
					copyText = Path.GetFileNameWithoutExtension(filePath);	//拡張子を省く
					break;
				default:
					copyText = Path.GetFileName(filePath);	//拡張子を付ける
					break;
			}

			//クリップボードに文字列をコピー
			Clipboard.SetText(copyText);
		}
		#endregion

		#region ファイルの変更監視
		/// <summary>
		/// ファイルが追加作成されたとき
		/// </summary>
		private void projectDirWatcher_Created(object sender, FileSystemEventArgs e)
		{
			updateNode(e.FullPath);
		}

		/// <summary>
		/// ファイルが削除されたとき
		/// </summary>
		private void projectDirWatcher_Deleted(object sender, FileSystemEventArgs e)
		{
			updateNode(e.FullPath);
		}

		/// <summary>
		/// ファイル名が変更されたとき
		/// </summary>
		private void projectDirWatcher_Renamed(object sender, RenamedEventArgs e)
		{
			updateNode(e.FullPath);
		}

		/// <summary>
		/// 指定したファイルのノードを更新する
		/// </summary>
		/// <param name="filePath">ファイルパス</param>
		private void updateNode(string filePath)
		{
			if (filePath == "")
			{
				return;	//パスが不正
			}
			string dirPath = Path.GetDirectoryName(filePath);
			TreeNode node = getExpandTreeNodeFromFullPath(fileTreeView.Nodes[0], dirPath);
			if (node == null)
			{
				//見つからなかったもしくは開いていないとき
				return;
			}

			//閉じて開く
			node.Collapse();
			node.Expand();
		}

		/// <summary>
		/// 指定したフルパスを表すノードが開かれていたときはそのノードを返す
		/// 見つからなかったときや開いていないときはnullを返す
		/// </summary>
		/// <param name="rootNode">検索開始するトップノード</param>
		/// <param name="fullPath">検索するフルパス</param>
		/// <returns>見つかったノード</returns>
		private TreeNode getExpandTreeNodeFromFullPath(TreeNode rootNode, string fullPath)
		{
			if (rootNode.IsExpanded)
			{
				//ルートをチェックする
				string rootPath = getNodeFullPath(rootNode);
				if (rootPath == fullPath)
				{
					return rootNode;
				}
			}

			foreach (TreeNode node in rootNode.Nodes)
			{
				if (node.IsExpanded)	//開いているときだけチェックする
				{
					string path = getNodeFullPath(node);
					if (path == fullPath)
					{
						return node;	//見つかった
					}

					TreeNode n = getExpandTreeNodeFromFullPath(node, fullPath);
					if (n != null)
					{
						return n;	//見つかった
					}
				}
			}

			return null;
		}
		#endregion

	}
}
