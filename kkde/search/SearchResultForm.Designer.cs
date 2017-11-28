namespace kkde.search
{
	partial class SearchResultForm
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
			this.infoStatusBar = new System.Windows.Forms.StatusStrip();
			this.searchStopButton = new System.Windows.Forms.ToolStripSplitButton();
			this.menuItemStop = new System.Windows.Forms.ToolStripMenuItem();
			this.searchResultStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.searchResultProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.grepBgWarker = new System.ComponentModel.BackgroundWorker();
			this.editorPopMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.editorMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.editorMenuItemClear = new System.Windows.Forms.ToolStripMenuItem();
			this.resultEditor = new kkde.editor.TextEditorEx();
			this.infoStatusBar.SuspendLayout();
			this.editorPopMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// infoStatusBar
			// 
			this.infoStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchStopButton,
            this.searchResultStatusLabel,
            this.searchResultProgressBar});
			this.infoStatusBar.Location = new System.Drawing.Point(0, 172);
			this.infoStatusBar.Name = "infoStatusBar";
			this.infoStatusBar.Size = new System.Drawing.Size(545, 23);
			this.infoStatusBar.TabIndex = 1;
			this.infoStatusBar.Text = "statusStrip1";
			// 
			// searchStopButton
			// 
			this.searchStopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.searchStopButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemStop});
			this.searchStopButton.Image = global::kkde.Properties.Resources.Critical;
			this.searchStopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.searchStopButton.Name = "searchStopButton";
			this.searchStopButton.Size = new System.Drawing.Size(32, 21);
			this.searchStopButton.Text = "検索中止";
			this.searchStopButton.ButtonClick += new System.EventHandler(this.searchStopButton_ButtonClick);
			// 
			// menuItemStop
			// 
			this.menuItemStop.Name = "menuItemStop";
			this.menuItemStop.Size = new System.Drawing.Size(124, 22);
			this.menuItemStop.Text = "検索中止";
			this.menuItemStop.Click += new System.EventHandler(this.menuItemStop_Click);
			// 
			// searchResultStatusLabel
			// 
			this.searchResultStatusLabel.Name = "searchResultStatusLabel";
			this.searchResultStatusLabel.Size = new System.Drawing.Size(396, 18);
			this.searchResultStatusLabel.Spring = true;
			this.searchResultStatusLabel.Text = "検索中...";
			this.searchResultStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// searchResultProgressBar
			// 
			this.searchResultProgressBar.Name = "searchResultProgressBar";
			this.searchResultProgressBar.Size = new System.Drawing.Size(100, 17);
			// 
			// grepBgWarker
			// 
			this.grepBgWarker.WorkerReportsProgress = true;
			this.grepBgWarker.WorkerSupportsCancellation = true;
			this.grepBgWarker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.grepBgWarker_DoWork);
			this.grepBgWarker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.grepBgWarker_RunWorkerCompleted);
			this.grepBgWarker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.grepBgWarker_ProgressChanged);
			// 
			// editorPopMenu
			// 
			this.editorPopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editorMenuItemCopy,
            this.toolStripMenuItem1,
            this.editorMenuItemClear});
			this.editorPopMenu.Name = "editorPopMenu";
			this.editorPopMenu.Size = new System.Drawing.Size(166, 54);
			// 
			// editorMenuItemCopy
			// 
			this.editorMenuItemCopy.Name = "editorMenuItemCopy";
			this.editorMenuItemCopy.Size = new System.Drawing.Size(165, 22);
			this.editorMenuItemCopy.Text = "コピー(&C)";
			this.editorMenuItemCopy.Click += new System.EventHandler(this.editorMenuItemCopy_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(162, 6);
			// 
			// editorMenuItemClear
			// 
			this.editorMenuItemClear.Name = "editorMenuItemClear";
			this.editorMenuItemClear.Size = new System.Drawing.Size(165, 22);
			this.editorMenuItemClear.Text = "すべてクリア(&L)";
			this.editorMenuItemClear.Click += new System.EventHandler(this.editorMenuItemClear_Click);
			// 
			// resultEditor
			// 
			this.resultEditor.ContextMenuStrip = this.editorPopMenu;
			this.resultEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.resultEditor.Location = new System.Drawing.Point(0, 0);
			this.resultEditor.Name = "resultEditor";
			this.resultEditor.ShowEOLMarkers = true;
			this.resultEditor.ShowSpaces = true;
			this.resultEditor.ShowTabs = true;
			this.resultEditor.ShowVRuler = true;
			this.resultEditor.Size = new System.Drawing.Size(545, 172);
			this.resultEditor.TabIndex = 0;
			// 
			// SearchResultForm
			// 
			this.ClientSize = new System.Drawing.Size(545, 195);
			this.Controls.Add(this.resultEditor);
			this.Controls.Add(this.infoStatusBar);
			this.DockableAreas = ((WeifenLuo.WinFormsUI.DockAreas)(((((WeifenLuo.WinFormsUI.DockAreas.Float | WeifenLuo.WinFormsUI.DockAreas.DockLeft)
						| WeifenLuo.WinFormsUI.DockAreas.DockRight)
						| WeifenLuo.WinFormsUI.DockAreas.DockTop)
						| WeifenLuo.WinFormsUI.DockAreas.DockBottom)));
			this.HideOnClose = true;
			this.Name = "SearchResultForm";
			this.ShowHint = WeifenLuo.WinFormsUI.DockState.DockBottom;
			this.TabText = "検索結果";
			this.Text = "検索結果";
			this.infoStatusBar.ResumeLayout(false);
			this.infoStatusBar.PerformLayout();
			this.editorPopMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private kkde.editor.TextEditorEx resultEditor;
		private System.Windows.Forms.StatusStrip infoStatusBar;
		private System.Windows.Forms.ToolStripSplitButton searchStopButton;
		private System.Windows.Forms.ToolStripMenuItem menuItemStop;
		private System.Windows.Forms.ToolStripStatusLabel searchResultStatusLabel;
		private System.ComponentModel.BackgroundWorker grepBgWarker;
		private System.Windows.Forms.ToolStripProgressBar searchResultProgressBar;
		private System.Windows.Forms.ContextMenuStrip editorPopMenu;
		private System.Windows.Forms.ToolStripMenuItem editorMenuItemCopy;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem editorMenuItemClear;
	}
}
