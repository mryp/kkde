namespace kkde.project
{
	partial class ProjectForm
	{
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナで生成されたコード

		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectForm));
			this.fileTreeView = new System.Windows.Forms.TreeView();
			this.treePopMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.treeMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.treeMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.treeMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.treeMenuItemChangeName = new System.Windows.Forms.ToolStripMenuItem();
			this.treeMenuItemSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.treeMenuItemUpdate = new System.Windows.Forms.ToolStripMenuItem();
			this.treeMenuItemProperty = new System.Windows.Forms.ToolStripMenuItem();
			this.treeImageList = new System.Windows.Forms.ImageList(this.components);
			this.projectDirWatcher = new System.IO.FileSystemWatcher();
			this.treeMenuItemCopyName = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.treePopMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.projectDirWatcher)).BeginInit();
			this.SuspendLayout();
			// 
			// fileTreeView
			// 
			this.fileTreeView.ContextMenuStrip = this.treePopMenu;
			this.fileTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fileTreeView.HideSelection = false;
			this.fileTreeView.ImageIndex = 0;
			this.fileTreeView.ImageList = this.treeImageList;
			this.fileTreeView.Indent = 20;
			this.fileTreeView.ItemHeight = 18;
			this.fileTreeView.Location = new System.Drawing.Point(0, 0);
			this.fileTreeView.Name = "fileTreeView";
			this.fileTreeView.SelectedImageIndex = 0;
			this.fileTreeView.Size = new System.Drawing.Size(358, 472);
			this.fileTreeView.TabIndex = 1;
			this.fileTreeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.fileTreeView_AfterLabelEdit);
			this.fileTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.fileTreeView_BeforeExpand);
			this.fileTreeView.DoubleClick += new System.EventHandler(this.fileTreeView_DoubleClick);
			this.fileTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fileTreeView_MouseDown);
			// 
			// treePopMenu
			// 
			this.treePopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.treeMenuItemOpen,
            this.treeMenuItemAdd,
            this.treeMenuItemDelete,
            this.treeMenuItemChangeName,
            this.toolStripMenuItem1,
            this.treeMenuItemCopyName,
            this.treeMenuItemSep1,
            this.treeMenuItemUpdate,
            this.treeMenuItemProperty});
			this.treePopMenu.Name = "treePopMenu";
			this.treePopMenu.Size = new System.Drawing.Size(196, 192);
			this.treePopMenu.Opening += new System.ComponentModel.CancelEventHandler(this.treePopMenu_Opening);
			// 
			// treeMenuItemOpen
			// 
			this.treeMenuItemOpen.Name = "treeMenuItemOpen";
			this.treeMenuItemOpen.Size = new System.Drawing.Size(195, 22);
			this.treeMenuItemOpen.Text = "開く (&O)";
			this.treeMenuItemOpen.Click += new System.EventHandler(this.treeMenuItemOpen_Click);
			// 
			// treeMenuItemAdd
			// 
			this.treeMenuItemAdd.Name = "treeMenuItemAdd";
			this.treeMenuItemAdd.Size = new System.Drawing.Size(195, 22);
			this.treeMenuItemAdd.Text = "追加 (&A)...";
			this.treeMenuItemAdd.Click += new System.EventHandler(this.treeMenuItemAdd_Click);
			// 
			// treeMenuItemDelete
			// 
			this.treeMenuItemDelete.Name = "treeMenuItemDelete";
			this.treeMenuItemDelete.Size = new System.Drawing.Size(195, 22);
			this.treeMenuItemDelete.Text = "削除 (&D)";
			this.treeMenuItemDelete.Click += new System.EventHandler(this.treeMenuItemDelete_Click);
			// 
			// treeMenuItemChangeName
			// 
			this.treeMenuItemChangeName.Name = "treeMenuItemChangeName";
			this.treeMenuItemChangeName.Size = new System.Drawing.Size(195, 22);
			this.treeMenuItemChangeName.Text = "名前の変更 (&R)";
			this.treeMenuItemChangeName.Click += new System.EventHandler(this.treeMenuItemChangeName_Click);
			// 
			// treeMenuItemSep1
			// 
			this.treeMenuItemSep1.Name = "treeMenuItemSep1";
			this.treeMenuItemSep1.Size = new System.Drawing.Size(192, 6);
			// 
			// treeMenuItemUpdate
			// 
			this.treeMenuItemUpdate.Name = "treeMenuItemUpdate";
			this.treeMenuItemUpdate.Size = new System.Drawing.Size(195, 22);
			this.treeMenuItemUpdate.Text = "最新の情報に更新 (&U)";
			this.treeMenuItemUpdate.Click += new System.EventHandler(this.treeMenuItemUpdate_Click);
			// 
			// treeMenuItemProperty
			// 
			this.treeMenuItemProperty.Name = "treeMenuItemProperty";
			this.treeMenuItemProperty.Size = new System.Drawing.Size(195, 22);
			this.treeMenuItemProperty.Text = "プロパティ (&P)";
			this.treeMenuItemProperty.Click += new System.EventHandler(this.treeMenuItemProperty_Click);
			// 
			// treeImageList
			// 
			this.treeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeImageList.ImageStream")));
			this.treeImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.treeImageList.Images.SetKeyName(0, "Icons.16x16.ClosedFolderBitmap.png");
			this.treeImageList.Images.SetKeyName(1, "Icons.16x16.OpenFolderBitmap.png");
			this.treeImageList.Images.SetKeyName(2, "Icons.16x16.MiscFiles.png");
			this.treeImageList.Images.SetKeyName(3, "file_image.png");
			this.treeImageList.Images.SetKeyName(4, "file_kag.png");
			this.treeImageList.Images.SetKeyName(5, "file_tjs.png");
			this.treeImageList.Images.SetKeyName(6, "file_txt.png");
			this.treeImageList.Images.SetKeyName(7, "file_app.png");
			// 
			// projectDirWatcher
			// 
			this.projectDirWatcher.EnableRaisingEvents = true;
			this.projectDirWatcher.IncludeSubdirectories = true;
			this.projectDirWatcher.NotifyFilter = System.IO.NotifyFilters.DirectoryName;
			this.projectDirWatcher.SynchronizingObject = this;
			this.projectDirWatcher.Renamed += new System.IO.RenamedEventHandler(this.projectDirWatcher_Renamed);
			this.projectDirWatcher.Deleted += new System.IO.FileSystemEventHandler(this.projectDirWatcher_Deleted);
			this.projectDirWatcher.Created += new System.IO.FileSystemEventHandler(this.projectDirWatcher_Created);
			// 
			// treeMenuItemCopyName
			// 
			this.treeMenuItemCopyName.Name = "treeMenuItemCopyName";
			this.treeMenuItemCopyName.Size = new System.Drawing.Size(195, 22);
			this.treeMenuItemCopyName.Text = "ファイル名コピー(&C)";
			this.treeMenuItemCopyName.Click += new System.EventHandler(this.treeMenuItemCopyName_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(192, 6);
			// 
			// ProjectForm
			// 
			this.ClientSize = new System.Drawing.Size(358, 472);
			this.Controls.Add(this.fileTreeView);
			this.DockableAreas = ((WeifenLuo.WinFormsUI.DockAreas)(((((WeifenLuo.WinFormsUI.DockAreas.Float | WeifenLuo.WinFormsUI.DockAreas.DockLeft)
						| WeifenLuo.WinFormsUI.DockAreas.DockRight)
						| WeifenLuo.WinFormsUI.DockAreas.DockTop)
						| WeifenLuo.WinFormsUI.DockAreas.DockBottom)));
			this.HideOnClose = true;
			this.Name = "ProjectForm";
			this.ShowHint = WeifenLuo.WinFormsUI.DockState.DockLeft;
			this.TabText = "プロジェクト";
			this.Text = "プロジェクト";
			this.treePopMenu.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.projectDirWatcher)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView fileTreeView;
		private System.Windows.Forms.ImageList treeImageList;
		private System.Windows.Forms.ContextMenuStrip treePopMenu;
		private System.Windows.Forms.ToolStripMenuItem treeMenuItemOpen;
		private System.Windows.Forms.ToolStripMenuItem treeMenuItemAdd;
		private System.Windows.Forms.ToolStripMenuItem treeMenuItemDelete;
		private System.Windows.Forms.ToolStripMenuItem treeMenuItemChangeName;
		private System.Windows.Forms.ToolStripSeparator treeMenuItemSep1;
		private System.Windows.Forms.ToolStripMenuItem treeMenuItemUpdate;
		private System.Windows.Forms.ToolStripMenuItem treeMenuItemProperty;
		private System.IO.FileSystemWatcher projectDirWatcher;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem treeMenuItemCopyName;
	}
}
