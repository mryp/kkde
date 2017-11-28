namespace kkde.kagex
{
	partial class WorldExActionEditorForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorldExActionEditorForm));
			this.mainToolBar = new System.Windows.Forms.ToolStrip();
			this.toolItemCopy = new System.Windows.Forms.ToolStripButton();
			this.toolItemPreview = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemTopMost = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemReset = new System.Windows.Forms.ToolStripButton();
			this.removeObjectPropertyButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.objectPropertyListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.addObjectPropertyButton = new System.Windows.Forms.Button();
			this.actionNameBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.actionHandlerPropetyGrid = new System.Windows.Forms.PropertyGrid();
			this.label4 = new System.Windows.Forms.Label();
			this.actionHandlerComboBox = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemPreviewTargetComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.toolItemPreviewTargetRefButton = new System.Windows.Forms.ToolStripButton();
			this.mainToolBar.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainToolBar
			// 
			this.mainToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolItemCopy,
            this.toolItemPreview,
            this.toolStripSeparator1,
            this.toolItemTopMost,
            this.toolStripSeparator2,
            this.toolItemReset,
            this.toolStripSeparator3,
            this.toolItemPreviewTargetComboBox,
            this.toolItemPreviewTargetRefButton});
			this.mainToolBar.Location = new System.Drawing.Point(0, 0);
			this.mainToolBar.Name = "mainToolBar";
			this.mainToolBar.Size = new System.Drawing.Size(400, 26);
			this.mainToolBar.TabIndex = 0;
			this.mainToolBar.Text = "toolStrip1";
			// 
			// toolItemCopy
			// 
			this.toolItemCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemCopy.Image = global::kkde.Properties.Resources.CopyHS;
			this.toolItemCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemCopy.Name = "toolItemCopy";
			this.toolItemCopy.Size = new System.Drawing.Size(23, 23);
			this.toolItemCopy.Text = "クリップボードにコピー";
			this.toolItemCopy.Click += new System.EventHandler(this.toolItemCopy_Click);
			// 
			// toolItemPreview
			// 
			this.toolItemPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemPreview.Image = global::kkde.Properties.Resources.Icons_16x16_ResourceEditor;
			this.toolItemPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemPreview.Name = "toolItemPreview";
			this.toolItemPreview.Size = new System.Drawing.Size(23, 23);
			this.toolItemPreview.Text = "プレビュー";
			this.toolItemPreview.Click += new System.EventHandler(this.toolItemPreview_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
			// 
			// toolItemTopMost
			// 
			this.toolItemTopMost.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemTopMost.Image = global::kkde.Properties.Resources.BringToFrontHS;
			this.toolItemTopMost.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemTopMost.Name = "toolItemTopMost";
			this.toolItemTopMost.Size = new System.Drawing.Size(23, 23);
			this.toolItemTopMost.Text = "最前面で常に表示する";
			this.toolItemTopMost.Click += new System.EventHandler(this.toolItemTopMost_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 26);
			// 
			// toolItemReset
			// 
			this.toolItemReset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemReset.Image = global::kkde.Properties.Resources.Icons_16x16_CancelIcon;
			this.toolItemReset.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemReset.Name = "toolItemReset";
			this.toolItemReset.Size = new System.Drawing.Size(23, 23);
			this.toolItemReset.Text = "内容をすべてクリア";
			this.toolItemReset.Click += new System.EventHandler(this.toolItemReset_Click);
			// 
			// removeObjectPropertyButton
			// 
			this.removeObjectPropertyButton.Location = new System.Drawing.Point(88, 392);
			this.removeObjectPropertyButton.Name = "removeObjectPropertyButton";
			this.removeObjectPropertyButton.Size = new System.Drawing.Size(75, 23);
			this.removeObjectPropertyButton.TabIndex = 11;
			this.removeObjectPropertyButton.Text = "削除";
			this.removeObjectPropertyButton.UseVisualStyleBackColor = true;
			this.removeObjectPropertyButton.Click += new System.EventHandler(this.removeObjectPropertyButton_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(97, 12);
			this.label2.TabIndex = 10;
			this.label2.Text = "アクションプロパティ：";
			// 
			// objectPropertyListView
			// 
			this.objectPropertyListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.objectPropertyListView.FullRowSelect = true;
			this.objectPropertyListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.objectPropertyListView.HideSelection = false;
			this.objectPropertyListView.Location = new System.Drawing.Point(8, 72);
			this.objectPropertyListView.MultiSelect = false;
			this.objectPropertyListView.Name = "objectPropertyListView";
			this.objectPropertyListView.Size = new System.Drawing.Size(152, 320);
			this.objectPropertyListView.TabIndex = 9;
			this.objectPropertyListView.UseCompatibleStateImageBehavior = false;
			this.objectPropertyListView.View = System.Windows.Forms.View.Details;
			this.objectPropertyListView.SelectedIndexChanged += new System.EventHandler(this.objectPropertyListView_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "プロパティ名";
			this.columnHeader1.Width = 145;
			// 
			// addObjectPropertyButton
			// 
			this.addObjectPropertyButton.Location = new System.Drawing.Point(8, 392);
			this.addObjectPropertyButton.Name = "addObjectPropertyButton";
			this.addObjectPropertyButton.Size = new System.Drawing.Size(75, 23);
			this.addObjectPropertyButton.TabIndex = 8;
			this.addObjectPropertyButton.Text = "追加";
			this.addObjectPropertyButton.UseVisualStyleBackColor = true;
			this.addObjectPropertyButton.Click += new System.EventHandler(this.addObjectPropertyButton_Click);
			// 
			// actionNameBox
			// 
			this.actionNameBox.Location = new System.Drawing.Point(48, 32);
			this.actionNameBox.Name = "actionNameBox";
			this.actionNameBox.Size = new System.Drawing.Size(112, 19);
			this.actionNameBox.TabIndex = 7;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 12);
			this.label1.TabIndex = 6;
			this.label1.Text = "名前：";
			// 
			// actionHandlerPropetyGrid
			// 
			this.actionHandlerPropetyGrid.Location = new System.Drawing.Point(176, 72);
			this.actionHandlerPropetyGrid.Name = "actionHandlerPropetyGrid";
			this.actionHandlerPropetyGrid.Size = new System.Drawing.Size(216, 344);
			this.actionHandlerPropetyGrid.TabIndex = 13;
			this.actionHandlerPropetyGrid.ToolbarVisible = false;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(176, 56);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(91, 12);
			this.label4.TabIndex = 15;
			this.label4.Text = "ハンドラプロパティ：";
			// 
			// actionHandlerComboBox
			// 
			this.actionHandlerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.actionHandlerComboBox.FormattingEnabled = true;
			this.actionHandlerComboBox.Location = new System.Drawing.Point(224, 32);
			this.actionHandlerComboBox.Name = "actionHandlerComboBox";
			this.actionHandlerComboBox.Size = new System.Drawing.Size(168, 20);
			this.actionHandlerComboBox.TabIndex = 14;
			this.actionHandlerComboBox.SelectedIndexChanged += new System.EventHandler(this.actionHandlerComboBox_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(176, 36);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(47, 12);
			this.label3.TabIndex = 12;
			this.label3.Text = "ハンドラ：";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 26);
			// 
			// toolItemPreviewTargetComboBox
			// 
			this.toolItemPreviewTargetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.toolItemPreviewTargetComboBox.Name = "toolItemPreviewTargetComboBox";
			this.toolItemPreviewTargetComboBox.Size = new System.Drawing.Size(240, 26);
			// 
			// toolItemPreviewTargetRefButton
			// 
			this.toolItemPreviewTargetRefButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolItemPreviewTargetRefButton.Image = ((System.Drawing.Image)(resources.GetObject("toolItemPreviewTargetRefButton.Image")));
			this.toolItemPreviewTargetRefButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemPreviewTargetRefButton.Name = "toolItemPreviewTargetRefButton";
			this.toolItemPreviewTargetRefButton.Size = new System.Drawing.Size(24, 22);
			this.toolItemPreviewTargetRefButton.Text = "...";
			this.toolItemPreviewTargetRefButton.ToolTipText = "プレビューオブジェクト参照";
			this.toolItemPreviewTargetRefButton.Click += new System.EventHandler(this.toolItemPreviewTargetRefButton_Click);
			// 
			// WorldExActionEditorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(400, 424);
			this.Controls.Add(this.actionHandlerPropetyGrid);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.actionHandlerComboBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.removeObjectPropertyButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.objectPropertyListView);
			this.Controls.Add(this.addObjectPropertyButton);
			this.Controls.Add(this.actionNameBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.mainToolBar);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "WorldExActionEditorForm";
			this.Text = "ワールド拡張アクションエディタ";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WorldExActionEditorForm_FormClosing);
			this.mainToolBar.ResumeLayout(false);
			this.mainToolBar.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip mainToolBar;
		private System.Windows.Forms.ToolStripButton toolItemCopy;
		private System.Windows.Forms.Button removeObjectPropertyButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListView objectPropertyListView;
		private System.Windows.Forms.Button addObjectPropertyButton;
		private System.Windows.Forms.TextBox actionNameBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PropertyGrid actionHandlerPropetyGrid;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox actionHandlerComboBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton toolItemPreview;
		private System.Windows.Forms.ToolStripButton toolItemTopMost;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton toolItemReset;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripComboBox toolItemPreviewTargetComboBox;
		private System.Windows.Forms.ToolStripButton toolItemPreviewTargetRefButton;

	}
}