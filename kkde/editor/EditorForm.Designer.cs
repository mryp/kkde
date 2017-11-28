namespace kkde.editor
{
	partial class EditorForm
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
			this.tabPopMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tabMenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.tabMenuItemClose = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.tabMenuItemCloseOther = new System.Windows.Forms.ToolStripMenuItem();
			this.tabMenuItemCloseLeft = new System.Windows.Forms.ToolStripMenuItem();
			this.tabMenuItemCloseRight = new System.Windows.Forms.ToolStripMenuItem();
			this.editorPopMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.editorMenuItemCut = new System.Windows.Forms.ToolStripMenuItem();
			this.editorMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.editorMenuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.editorMenuItemComment = new System.Windows.Forms.ToolStripMenuItem();
			this.editorMenuItemUnComment = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemJumpSplit = new System.Windows.Forms.ToolStripSeparator();
			this.menuItemJumpDefine = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemJumpGrepDefine = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemJumpLabel = new System.Windows.Forms.ToolStripMenuItem();
			this.mainTextEditor = new kkde.editor.TextEditorEx();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.menuItemRegionMsgonoff = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemRegionTrans = new System.Windows.Forms.ToolStripMenuItem();
			this.tabPopMenu.SuspendLayout();
			this.editorPopMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabPopMenu
			// 
			this.tabPopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabMenuItemSave,
            this.toolStripMenuItem2,
            this.tabMenuItemClose,
            this.toolStripMenuItem3,
            this.tabMenuItemCloseOther,
            this.tabMenuItemCloseLeft,
            this.tabMenuItemCloseRight});
			this.tabPopMenu.Name = "contextMenuStrip1";
			this.tabPopMenu.Size = new System.Drawing.Size(215, 126);
			// 
			// tabMenuItemSave
			// 
			this.tabMenuItemSave.Name = "tabMenuItemSave";
			this.tabMenuItemSave.Size = new System.Drawing.Size(214, 22);
			this.tabMenuItemSave.Text = "上書き保存(&S)";
			this.tabMenuItemSave.Click += new System.EventHandler(this.tabMenuItemSave_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(211, 6);
			// 
			// tabMenuItemClose
			// 
			this.tabMenuItemClose.Name = "tabMenuItemClose";
			this.tabMenuItemClose.Size = new System.Drawing.Size(214, 22);
			this.tabMenuItemClose.Text = "このタブを閉じる(&C)";
			this.tabMenuItemClose.Click += new System.EventHandler(this.tabMenuItemClose_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(211, 6);
			// 
			// tabMenuItemCloseOther
			// 
			this.tabMenuItemCloseOther.Name = "tabMenuItemCloseOther";
			this.tabMenuItemCloseOther.Size = new System.Drawing.Size(214, 22);
			this.tabMenuItemCloseOther.Text = "これ以外を閉じる(&O)";
			this.tabMenuItemCloseOther.Click += new System.EventHandler(this.tabMenuItemCloseOther_Click);
			// 
			// tabMenuItemCloseLeft
			// 
			this.tabMenuItemCloseLeft.Name = "tabMenuItemCloseLeft";
			this.tabMenuItemCloseLeft.Size = new System.Drawing.Size(214, 22);
			this.tabMenuItemCloseLeft.Text = "左のタブを全て閉じる(&L)";
			this.tabMenuItemCloseLeft.Visible = false;
			this.tabMenuItemCloseLeft.Click += new System.EventHandler(this.tabMenuItemCloseLeft_Click);
			// 
			// tabMenuItemCloseRight
			// 
			this.tabMenuItemCloseRight.Name = "tabMenuItemCloseRight";
			this.tabMenuItemCloseRight.Size = new System.Drawing.Size(214, 22);
			this.tabMenuItemCloseRight.Text = "右のタブを全て閉じる(&R)";
			this.tabMenuItemCloseRight.Visible = false;
			this.tabMenuItemCloseRight.Click += new System.EventHandler(this.tabMenuItemCloseRight_Click);
			// 
			// editorPopMenu
			// 
			this.editorPopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editorMenuItemCut,
            this.editorMenuItemCopy,
            this.editorMenuItemPaste,
            this.toolStripMenuItem1,
            this.editorMenuItemComment,
            this.editorMenuItemUnComment,
            this.menuItemJumpSplit,
            this.menuItemJumpDefine,
            this.menuItemJumpGrepDefine,
            this.menuItemJumpLabel,
            this.toolStripMenuItem4,
            this.menuItemRegionMsgonoff,
            this.menuItemRegionTrans});
			this.editorPopMenu.Name = "editorPopMenu";
			this.editorPopMenu.Size = new System.Drawing.Size(309, 264);
			this.editorPopMenu.Opening += new System.ComponentModel.CancelEventHandler(this.editorPopMenu_Opening);
			// 
			// editorMenuItemCut
			// 
			this.editorMenuItemCut.Name = "editorMenuItemCut";
			this.editorMenuItemCut.ShortcutKeyDisplayString = "Ctrl+X";
			this.editorMenuItemCut.Size = new System.Drawing.Size(308, 22);
			this.editorMenuItemCut.Text = "切り取り(&T)";
			this.editorMenuItemCut.Click += new System.EventHandler(this.editorMenuItemCut_Click);
			// 
			// editorMenuItemCopy
			// 
			this.editorMenuItemCopy.Name = "editorMenuItemCopy";
			this.editorMenuItemCopy.ShortcutKeyDisplayString = "Ctrl+C";
			this.editorMenuItemCopy.Size = new System.Drawing.Size(308, 22);
			this.editorMenuItemCopy.Text = "コピー(&C)";
			this.editorMenuItemCopy.Click += new System.EventHandler(this.editorMenuItemCopy_Click);
			// 
			// editorMenuItemPaste
			// 
			this.editorMenuItemPaste.Name = "editorMenuItemPaste";
			this.editorMenuItemPaste.ShortcutKeyDisplayString = "Ctrl+V";
			this.editorMenuItemPaste.Size = new System.Drawing.Size(308, 22);
			this.editorMenuItemPaste.Text = "貼り付け(&P)";
			this.editorMenuItemPaste.Click += new System.EventHandler(this.editorMenuItemPaste_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(305, 6);
			// 
			// editorMenuItemComment
			// 
			this.editorMenuItemComment.Name = "editorMenuItemComment";
			this.editorMenuItemComment.ShortcutKeyDisplayString = "Ctrl+P";
			this.editorMenuItemComment.Size = new System.Drawing.Size(308, 22);
			this.editorMenuItemComment.Text = "選択範囲をコメント化(&M)";
			this.editorMenuItemComment.Click += new System.EventHandler(this.editorMenuItemComment_Click);
			// 
			// editorMenuItemUnComment
			// 
			this.editorMenuItemUnComment.Name = "editorMenuItemUnComment";
			this.editorMenuItemUnComment.ShortcutKeyDisplayString = "Ctrl+Shift+P";
			this.editorMenuItemUnComment.Size = new System.Drawing.Size(308, 22);
			this.editorMenuItemUnComment.Text = "選択範囲のコメント解除(&E)";
			this.editorMenuItemUnComment.Click += new System.EventHandler(this.editorMenuItemUnComment_Click);
			// 
			// menuItemJumpSplit
			// 
			this.menuItemJumpSplit.Name = "menuItemJumpSplit";
			this.menuItemJumpSplit.Size = new System.Drawing.Size(305, 6);
			// 
			// menuItemJumpDefine
			// 
			this.menuItemJumpDefine.Name = "menuItemJumpDefine";
			this.menuItemJumpDefine.Size = new System.Drawing.Size(308, 22);
			this.menuItemJumpDefine.Text = "定義へ移動(&G)";
			this.menuItemJumpDefine.Click += new System.EventHandler(this.menuItemJumpDefine_Click);
			// 
			// menuItemJumpGrepDefine
			// 
			this.menuItemJumpGrepDefine.Name = "menuItemJumpGrepDefine";
			this.menuItemJumpGrepDefine.Size = new System.Drawing.Size(308, 22);
			this.menuItemJumpGrepDefine.Text = "全ての参照を検索(&A)";
			this.menuItemJumpGrepDefine.Click += new System.EventHandler(this.menuItemJumpGrepDefine_Click);
			// 
			// menuItemJumpLabel
			// 
			this.menuItemJumpLabel.Name = "menuItemJumpLabel";
			this.menuItemJumpLabel.Size = new System.Drawing.Size(308, 22);
			this.menuItemJumpLabel.Text = "シナリオ・ラベルへ移動(&L)";
			this.menuItemJumpLabel.Click += new System.EventHandler(this.menuItemJumpLabel_Click);
			// 
			// mainTextEditor
			// 
			this.mainTextEditor.ContextMenuStrip = this.editorPopMenu;
			this.mainTextEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainTextEditor.Location = new System.Drawing.Point(0, 0);
			this.mainTextEditor.Name = "mainTextEditor";
			this.mainTextEditor.ParseActionSaveFile = false;
			this.mainTextEditor.ShowEOLMarkers = true;
			this.mainTextEditor.ShowHRuler = true;
			this.mainTextEditor.ShowInvalidLines = false;
			this.mainTextEditor.ShowSpaces = true;
			this.mainTextEditor.ShowTabs = true;
			this.mainTextEditor.ShowVRuler = true;
			this.mainTextEditor.Size = new System.Drawing.Size(500, 457);
			this.mainTextEditor.TabIndex = 0;
			this.mainTextEditor.UseCodeCompletion = false;
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(305, 6);
			// 
			// menuItemRegionMsgonoff
			// 
			this.menuItemRegionMsgonoff.Name = "menuItemRegionMsgonoff";
			this.menuItemRegionMsgonoff.Size = new System.Drawing.Size(308, 22);
			this.menuItemRegionMsgonoff.Text = "@msgoff-@msgon";
			this.menuItemRegionMsgonoff.Click += new System.EventHandler(this.menuItemRegionMsgonoff_Click);
			// 
			// menuItemRegionTrans
			// 
			this.menuItemRegionTrans.Name = "menuItemRegionTrans";
			this.menuItemRegionTrans.Size = new System.Drawing.Size(308, 22);
			this.menuItemRegionTrans.Text = "@begintrans-@endtrans";
			this.menuItemRegionTrans.Click += new System.EventHandler(this.menuItemRegionTrans_Click);
			// 
			// EditorForm
			// 
			this.ClientSize = new System.Drawing.Size(500, 457);
			this.Controls.Add(this.mainTextEditor);
			this.DockableAreas = WeifenLuo.WinFormsUI.DockAreas.Document;
			this.Name = "EditorForm";
			this.ShowHint = WeifenLuo.WinFormsUI.DockState.Document;
			this.TabPageContextMenuStrip = this.tabPopMenu;
			this.TabText = "TEXT";
			this.DoubleClick += new System.EventHandler(this.EditorForm_DoubleClick);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EditorForm_FormClosed);
			this.tabPopMenu.ResumeLayout(false);
			this.editorPopMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private TextEditorEx mainTextEditor;
		private System.Windows.Forms.ContextMenuStrip tabPopMenu;
		private System.Windows.Forms.ToolStripMenuItem tabMenuItemClose;
		private System.Windows.Forms.ToolStripMenuItem tabMenuItemSave;
		private System.Windows.Forms.ContextMenuStrip editorPopMenu;
		private System.Windows.Forms.ToolStripMenuItem editorMenuItemCut;
		private System.Windows.Forms.ToolStripMenuItem editorMenuItemCopy;
		private System.Windows.Forms.ToolStripMenuItem editorMenuItemPaste;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem editorMenuItemComment;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem tabMenuItemCloseOther;
		private System.Windows.Forms.ToolStripMenuItem tabMenuItemCloseLeft;
		private System.Windows.Forms.ToolStripMenuItem tabMenuItemCloseRight;
		private System.Windows.Forms.ToolStripMenuItem editorMenuItemUnComment;
		private System.Windows.Forms.ToolStripSeparator menuItemJumpSplit;
		private System.Windows.Forms.ToolStripMenuItem menuItemJumpDefine;
		private System.Windows.Forms.ToolStripMenuItem menuItemJumpGrepDefine;
		private System.Windows.Forms.ToolStripMenuItem menuItemJumpLabel;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem menuItemRegionMsgonoff;
		private System.Windows.Forms.ToolStripMenuItem menuItemRegionTrans;

	}
}
