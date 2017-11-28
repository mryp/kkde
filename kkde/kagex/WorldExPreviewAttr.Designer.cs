namespace kkde.kagex
{
	partial class WorldExPreviewAttr
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorldExPreviewAttr));
			this.mainToolBar = new System.Windows.Forms.ToolStrip();
			this.toolItemCopyTag = new System.Windows.Forms.ToolStripButton();
			this.toolItemPreview = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemResetAttr = new System.Windows.Forms.ToolStripButton();
			this.toolItemResetAll = new System.Windows.Forms.ToolStripButton();
			this.fontDialog1 = new System.Windows.Forms.FontDialog();
			this.attrPropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.attrEditorPopMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuItemTagCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemPreview = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.menuItemSelectChar = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSelectEvent = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSelectStage = new System.Windows.Forms.ToolStripMenuItem();
			this.targetToolBar = new System.Windows.Forms.ToolStrip();
			this.toolItemTargetSelectBox = new System.Windows.Forms.ToolStripComboBox();
			this.toolItemTargetRefButton = new System.Windows.Forms.ToolStripButton();
			this.toolItemTopMost = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.mainToolBar.SuspendLayout();
			this.attrEditorPopMenu.SuspendLayout();
			this.targetToolBar.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainToolBar
			// 
			this.mainToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolItemCopyTag,
            this.toolItemPreview,
            this.toolStripSeparator1,
            this.toolItemTopMost,
            this.toolStripSeparator2,
            this.toolItemResetAttr,
            this.toolItemResetAll});
			this.mainToolBar.Location = new System.Drawing.Point(0, 0);
			this.mainToolBar.Name = "mainToolBar";
			this.mainToolBar.Size = new System.Drawing.Size(305, 25);
			this.mainToolBar.TabIndex = 1;
			this.mainToolBar.Text = "toolStrip1";
			// 
			// toolItemCopyTag
			// 
			this.toolItemCopyTag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemCopyTag.Image = global::kkde.Properties.Resources.CopyHS;
			this.toolItemCopyTag.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemCopyTag.Name = "toolItemCopyTag";
			this.toolItemCopyTag.Size = new System.Drawing.Size(23, 22);
			this.toolItemCopyTag.Text = "タグコピー";
			this.toolItemCopyTag.Click += new System.EventHandler(this.toolItemCopyTag_Click);
			// 
			// toolItemPreview
			// 
			this.toolItemPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemPreview.Image = global::kkde.Properties.Resources.Icons_16x16_ResourceEditor;
			this.toolItemPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemPreview.Name = "toolItemPreview";
			this.toolItemPreview.Size = new System.Drawing.Size(23, 22);
			this.toolItemPreview.Text = "プレビュー";
			this.toolItemPreview.Click += new System.EventHandler(this.toolItemPreview_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolItemResetAttr
			// 
			this.toolItemResetAttr.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemResetAttr.Image = global::kkde.Properties.Resources.Icons_16x16_CancelIcon;
			this.toolItemResetAttr.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemResetAttr.Name = "toolItemResetAttr";
			this.toolItemResetAttr.Size = new System.Drawing.Size(23, 22);
			this.toolItemResetAttr.Text = "属性をすべてクリアーする";
			this.toolItemResetAttr.ToolTipText = "属性をすべてクリアする";
			this.toolItemResetAttr.Click += new System.EventHandler(this.toolItemResetAttr_Click);
			// 
			// toolItemResetAll
			// 
			this.toolItemResetAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemResetAll.Image = global::kkde.Properties.Resources.Critical1;
			this.toolItemResetAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemResetAll.Name = "toolItemResetAll";
			this.toolItemResetAll.Size = new System.Drawing.Size(23, 22);
			this.toolItemResetAll.Text = "オブジェクトをすべてクリアーする";
			this.toolItemResetAll.ToolTipText = "オブジェクトをすべてクリアする";
			this.toolItemResetAll.Click += new System.EventHandler(this.toolItemResetAll_Click);
			// 
			// attrPropertyGrid
			// 
			this.attrPropertyGrid.ContextMenuStrip = this.attrEditorPopMenu;
			this.attrPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.attrPropertyGrid.Location = new System.Drawing.Point(0, 51);
			this.attrPropertyGrid.Name = "attrPropertyGrid";
			this.attrPropertyGrid.Size = new System.Drawing.Size(305, 392);
			this.attrPropertyGrid.TabIndex = 0;
			this.attrPropertyGrid.ToolbarVisible = false;
			// 
			// attrEditorPopMenu
			// 
			this.attrEditorPopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemTagCopy,
            this.menuItemPreview,
            this.toolStripMenuItem1,
            this.menuItemSelectChar,
            this.menuItemSelectEvent,
            this.menuItemSelectStage});
			this.attrEditorPopMenu.Name = "attrEditorPopMenu";
			this.attrEditorPopMenu.Size = new System.Drawing.Size(228, 120);
			// 
			// menuItemTagCopy
			// 
			this.menuItemTagCopy.Name = "menuItemTagCopy";
			this.menuItemTagCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.menuItemTagCopy.Size = new System.Drawing.Size(227, 22);
			this.menuItemTagCopy.Text = "タグコピー(&C)";
			this.menuItemTagCopy.Click += new System.EventHandler(this.toolItemCopyTag_Click);
			// 
			// menuItemPreview
			// 
			this.menuItemPreview.Name = "menuItemPreview";
			this.menuItemPreview.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
			this.menuItemPreview.Size = new System.Drawing.Size(227, 22);
			this.menuItemPreview.Text = "プレビュー(&P)";
			this.menuItemPreview.Click += new System.EventHandler(this.toolItemPreview_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(224, 6);
			// 
			// menuItemSelectChar
			// 
			this.menuItemSelectChar.Name = "menuItemSelectChar";
			this.menuItemSelectChar.Size = new System.Drawing.Size(227, 22);
			this.menuItemSelectChar.Text = "立ち絵選択";
			this.menuItemSelectChar.Click += new System.EventHandler(this.menuItemSelectChar_Click);
			// 
			// menuItemSelectEvent
			// 
			this.menuItemSelectEvent.Name = "menuItemSelectEvent";
			this.menuItemSelectEvent.Size = new System.Drawing.Size(227, 22);
			this.menuItemSelectEvent.Text = "イベント絵選択";
			this.menuItemSelectEvent.Click += new System.EventHandler(this.menuItemSelectEvent_Click);
			// 
			// menuItemSelectStage
			// 
			this.menuItemSelectStage.Name = "menuItemSelectStage";
			this.menuItemSelectStage.Size = new System.Drawing.Size(227, 22);
			this.menuItemSelectStage.Text = "背景絵選択";
			this.menuItemSelectStage.Click += new System.EventHandler(this.menuItemSelectStage_Click);
			// 
			// targetToolBar
			// 
			this.targetToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolItemTargetSelectBox,
            this.toolItemTargetRefButton});
			this.targetToolBar.Location = new System.Drawing.Point(0, 25);
			this.targetToolBar.Name = "targetToolBar";
			this.targetToolBar.Size = new System.Drawing.Size(305, 26);
			this.targetToolBar.TabIndex = 2;
			this.targetToolBar.Text = "toolStrip1";
			this.targetToolBar.Resize += new System.EventHandler(this.targetToolBar_Resize);
			// 
			// toolItemTargetSelectBox
			// 
			this.toolItemTargetSelectBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.toolItemTargetSelectBox.Name = "toolItemTargetSelectBox";
			this.toolItemTargetSelectBox.Size = new System.Drawing.Size(121, 26);
			this.toolItemTargetSelectBox.SelectedIndexChanged += new System.EventHandler(this.toolItemTargetSelectBox_SelectedIndexChanged);
			// 
			// toolItemTargetRefButton
			// 
			this.toolItemTargetRefButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolItemTargetRefButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolItemTargetRefButton.Image = ((System.Drawing.Image)(resources.GetObject("toolItemTargetRefButton.Image")));
			this.toolItemTargetRefButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemTargetRefButton.Name = "toolItemTargetRefButton";
			this.toolItemTargetRefButton.Size = new System.Drawing.Size(24, 23);
			this.toolItemTargetRefButton.Text = "...";
			this.toolItemTargetRefButton.ToolTipText = "オブジェクト参照";
			this.toolItemTargetRefButton.Click += new System.EventHandler(this.toolItemTargetRefButton_Click);
			// 
			// toolItemTopMost
			// 
			this.toolItemTopMost.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemTopMost.Image = global::kkde.Properties.Resources.BringToFrontHS;
			this.toolItemTopMost.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemTopMost.Name = "toolItemTopMost";
			this.toolItemTopMost.Size = new System.Drawing.Size(23, 22);
			this.toolItemTopMost.Text = "toolStripButton1";
			this.toolItemTopMost.Click += new System.EventHandler(this.toolItemTopMost_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// WorldExPreviewAttr
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(305, 443);
			this.Controls.Add(this.attrPropertyGrid);
			this.Controls.Add(this.targetToolBar);
			this.Controls.Add(this.mainToolBar);
			this.MaximizeBox = false;
			this.Name = "WorldExPreviewAttr";
			this.Text = "ワールド拡張プレビューエディタ";
			this.VisibleChanged += new System.EventHandler(this.WorldExPreviewAttr_VisibleChanged);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WorldExPreviewAttr_FormClosing);
			this.mainToolBar.ResumeLayout(false);
			this.mainToolBar.PerformLayout();
			this.attrEditorPopMenu.ResumeLayout(false);
			this.targetToolBar.ResumeLayout(false);
			this.targetToolBar.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip mainToolBar;
		private System.Windows.Forms.ToolStripButton toolItemPreview;
		private System.Windows.Forms.FontDialog fontDialog1;
		private System.Windows.Forms.PropertyGrid attrPropertyGrid;
		private System.Windows.Forms.ToolStrip targetToolBar;
		private System.Windows.Forms.ToolStripComboBox toolItemTargetSelectBox;
		private System.Windows.Forms.ToolStripButton toolItemTargetRefButton;
		private System.Windows.Forms.ToolStripButton toolItemCopyTag;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton toolItemResetAttr;
		private System.Windows.Forms.ToolStripButton toolItemResetAll;
		private System.Windows.Forms.ContextMenuStrip attrEditorPopMenu;
		private System.Windows.Forms.ToolStripMenuItem menuItemTagCopy;
		private System.Windows.Forms.ToolStripMenuItem menuItemPreview;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem menuItemSelectChar;
		private System.Windows.Forms.ToolStripMenuItem menuItemSelectEvent;
		private System.Windows.Forms.ToolStripMenuItem menuItemSelectStage;
		private System.Windows.Forms.ToolStripButton toolItemTopMost;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
	}
}