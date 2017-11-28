namespace TagsXmlMaker
{
	partial class MainForm
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
			this.mainMenu = new System.Windows.Forms.MenuStrip();
			this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemFileNew = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemFileOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.menuItemFileSave = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.menuItemFileClose = new System.Windows.Forms.ToolStripMenuItem();
			this.tagTreeView = new System.Windows.Forms.TreeView();
			this.popMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuItemPopAddTag = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemPopAddAttr = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.menuItemPopDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.tagPanel = new System.Windows.Forms.Panel();
			this.tagLongInfoBox = new System.Windows.Forms.TextBox();
			this.tagShortInfoBox = new System.Windows.Forms.TextBox();
			this.tagGropuBox = new System.Windows.Forms.TextBox();
			this.tagNameBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.attrPanel = new System.Windows.Forms.Panel();
			this.attrFormatBox = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.attrLongInfoBox = new System.Windows.Forms.TextBox();
			this.attrShortInfoBox = new System.Windows.Forms.TextBox();
			this.attrRequiredBox = new System.Windows.Forms.TextBox();
			this.attrNameBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.defaultPanel = new System.Windows.Forms.Panel();
			this.label10 = new System.Windows.Forms.Label();
			this.openXmlFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveXmlFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.menuItemDebug = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemDebugOutputList = new System.Windows.Forms.ToolStripMenuItem();
			this.mainMenu.SuspendLayout();
			this.popMenu.SuspendLayout();
			this.tagPanel.SuspendLayout();
			this.attrPanel.SuspendLayout();
			this.defaultPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.menuItemDebug});
			this.mainMenu.Location = new System.Drawing.Point(0, 0);
			this.mainMenu.Name = "mainMenu";
			this.mainMenu.Size = new System.Drawing.Size(571, 26);
			this.mainMenu.TabIndex = 0;
			this.mainMenu.Text = "menuStrip1";
			// 
			// menuItemFile
			// 
			this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFileNew,
            this.menuItemFileOpen,
            this.toolStripMenuItem1,
            this.menuItemFileSave,
            this.menuItemFileSaveAs,
            this.toolStripMenuItem2,
            this.menuItemFileClose});
			this.menuItemFile.Name = "menuItemFile";
			this.menuItemFile.Size = new System.Drawing.Size(85, 22);
			this.menuItemFile.Text = "ファイル(&F)";
			// 
			// menuItemFileNew
			// 
			this.menuItemFileNew.Name = "menuItemFileNew";
			this.menuItemFileNew.Size = new System.Drawing.Size(190, 22);
			this.menuItemFileNew.Text = "新規作成(&N)";
			this.menuItemFileNew.Click += new System.EventHandler(this.menuItemFileNew_Click);
			// 
			// menuItemFileOpen
			// 
			this.menuItemFileOpen.Name = "menuItemFileOpen";
			this.menuItemFileOpen.Size = new System.Drawing.Size(190, 22);
			this.menuItemFileOpen.Text = "開く(&O)";
			this.menuItemFileOpen.Click += new System.EventHandler(this.menuItemFileOpen_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(187, 6);
			// 
			// menuItemFileSave
			// 
			this.menuItemFileSave.Name = "menuItemFileSave";
			this.menuItemFileSave.Size = new System.Drawing.Size(190, 22);
			this.menuItemFileSave.Text = "上書き保存(&S)";
			this.menuItemFileSave.Click += new System.EventHandler(this.menuItemFileSave_Click);
			// 
			// menuItemFileSaveAs
			// 
			this.menuItemFileSaveAs.Name = "menuItemFileSaveAs";
			this.menuItemFileSaveAs.Size = new System.Drawing.Size(190, 22);
			this.menuItemFileSaveAs.Text = "名前を付けて保存(&A)";
			this.menuItemFileSaveAs.Click += new System.EventHandler(this.menuItemFileSaveAs_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(187, 6);
			// 
			// menuItemFileClose
			// 
			this.menuItemFileClose.Name = "menuItemFileClose";
			this.menuItemFileClose.Size = new System.Drawing.Size(190, 22);
			this.menuItemFileClose.Text = "閉じる(&C)";
			this.menuItemFileClose.Click += new System.EventHandler(this.menuItemFileClose_Click);
			// 
			// tagTreeView
			// 
			this.tagTreeView.ContextMenuStrip = this.popMenu;
			this.tagTreeView.Dock = System.Windows.Forms.DockStyle.Left;
			this.tagTreeView.FullRowSelect = true;
			this.tagTreeView.HideSelection = false;
			this.tagTreeView.Location = new System.Drawing.Point(0, 26);
			this.tagTreeView.Name = "tagTreeView";
			this.tagTreeView.Size = new System.Drawing.Size(176, 531);
			this.tagTreeView.TabIndex = 1;
			this.tagTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tagTreeView_MouseDown);
			this.tagTreeView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tagTreeView_KeyPress);
			this.tagTreeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tagTreeView_KeyDown);
			this.tagTreeView.Click += new System.EventHandler(this.tagTreeView_Click);
			// 
			// popMenu
			// 
			this.popMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemPopAddTag,
            this.menuItemPopAddAttr,
            this.toolStripMenuItem3,
            this.menuItemPopDelete});
			this.popMenu.Name = "popMenu";
			this.popMenu.Size = new System.Drawing.Size(143, 76);
			this.popMenu.Opening += new System.ComponentModel.CancelEventHandler(this.popMenu_Opening);
			// 
			// menuItemPopAddTag
			// 
			this.menuItemPopAddTag.Name = "menuItemPopAddTag";
			this.menuItemPopAddTag.Size = new System.Drawing.Size(142, 22);
			this.menuItemPopAddTag.Text = "タグ追加(&T)";
			this.menuItemPopAddTag.Click += new System.EventHandler(this.menuItemPopAddTag_Click);
			// 
			// menuItemPopAddAttr
			// 
			this.menuItemPopAddAttr.Name = "menuItemPopAddAttr";
			this.menuItemPopAddAttr.Size = new System.Drawing.Size(142, 22);
			this.menuItemPopAddAttr.Text = "属性追加(&A)";
			this.menuItemPopAddAttr.Click += new System.EventHandler(this.menuItemPopAddAttr_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(139, 6);
			// 
			// menuItemPopDelete
			// 
			this.menuItemPopDelete.Name = "menuItemPopDelete";
			this.menuItemPopDelete.Size = new System.Drawing.Size(142, 22);
			this.menuItemPopDelete.Text = "削除(&D)";
			this.menuItemPopDelete.Click += new System.EventHandler(this.menuItemPopDelete_Click);
			// 
			// tagPanel
			// 
			this.tagPanel.Controls.Add(this.tagLongInfoBox);
			this.tagPanel.Controls.Add(this.tagShortInfoBox);
			this.tagPanel.Controls.Add(this.tagGropuBox);
			this.tagPanel.Controls.Add(this.tagNameBox);
			this.tagPanel.Controls.Add(this.label4);
			this.tagPanel.Controls.Add(this.label3);
			this.tagPanel.Controls.Add(this.label2);
			this.tagPanel.Controls.Add(this.label1);
			this.tagPanel.Location = new System.Drawing.Point(176, 24);
			this.tagPanel.Name = "tagPanel";
			this.tagPanel.Size = new System.Drawing.Size(392, 288);
			this.tagPanel.TabIndex = 2;
			// 
			// tagLongInfoBox
			// 
			this.tagLongInfoBox.Location = new System.Drawing.Point(72, 80);
			this.tagLongInfoBox.Multiline = true;
			this.tagLongInfoBox.Name = "tagLongInfoBox";
			this.tagLongInfoBox.Size = new System.Drawing.Size(312, 192);
			this.tagLongInfoBox.TabIndex = 3;
			// 
			// tagShortInfoBox
			// 
			this.tagShortInfoBox.Location = new System.Drawing.Point(72, 56);
			this.tagShortInfoBox.Name = "tagShortInfoBox";
			this.tagShortInfoBox.Size = new System.Drawing.Size(312, 19);
			this.tagShortInfoBox.TabIndex = 2;
			// 
			// tagGropuBox
			// 
			this.tagGropuBox.Location = new System.Drawing.Point(72, 32);
			this.tagGropuBox.Name = "tagGropuBox";
			this.tagGropuBox.Size = new System.Drawing.Size(152, 19);
			this.tagGropuBox.TabIndex = 1;
			// 
			// tagNameBox
			// 
			this.tagNameBox.Location = new System.Drawing.Point(72, 8);
			this.tagNameBox.Name = "tagNameBox";
			this.tagNameBox.Size = new System.Drawing.Size(152, 19);
			this.tagNameBox.TabIndex = 0;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(8, 84);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(57, 12);
			this.label4.TabIndex = 7;
			this.label4.Text = "長い説明：";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 60);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(57, 12);
			this.label3.TabIndex = 6;
			this.label3.Text = "短い説明：";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(49, 12);
			this.label2.TabIndex = 5;
			this.label2.Text = "グループ：";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 12);
			this.label1.TabIndex = 4;
			this.label1.Text = "タグ名：";
			// 
			// attrPanel
			// 
			this.attrPanel.Controls.Add(this.attrFormatBox);
			this.attrPanel.Controls.Add(this.label9);
			this.attrPanel.Controls.Add(this.attrLongInfoBox);
			this.attrPanel.Controls.Add(this.attrShortInfoBox);
			this.attrPanel.Controls.Add(this.attrRequiredBox);
			this.attrPanel.Controls.Add(this.attrNameBox);
			this.attrPanel.Controls.Add(this.label5);
			this.attrPanel.Controls.Add(this.label6);
			this.attrPanel.Controls.Add(this.label7);
			this.attrPanel.Controls.Add(this.label8);
			this.attrPanel.Location = new System.Drawing.Point(416, 328);
			this.attrPanel.Name = "attrPanel";
			this.attrPanel.Size = new System.Drawing.Size(392, 304);
			this.attrPanel.TabIndex = 4;
			// 
			// attrFormatBox
			// 
			this.attrFormatBox.Location = new System.Drawing.Point(72, 56);
			this.attrFormatBox.Name = "attrFormatBox";
			this.attrFormatBox.Size = new System.Drawing.Size(152, 19);
			this.attrFormatBox.TabIndex = 2;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(8, 60);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(61, 12);
			this.label9.TabIndex = 7;
			this.label9.Text = "フォーマット：";
			// 
			// attrLongInfoBox
			// 
			this.attrLongInfoBox.Location = new System.Drawing.Point(72, 104);
			this.attrLongInfoBox.Multiline = true;
			this.attrLongInfoBox.Name = "attrLongInfoBox";
			this.attrLongInfoBox.Size = new System.Drawing.Size(312, 192);
			this.attrLongInfoBox.TabIndex = 4;
			// 
			// attrShortInfoBox
			// 
			this.attrShortInfoBox.Location = new System.Drawing.Point(72, 80);
			this.attrShortInfoBox.Name = "attrShortInfoBox";
			this.attrShortInfoBox.Size = new System.Drawing.Size(312, 19);
			this.attrShortInfoBox.TabIndex = 3;
			// 
			// attrRequiredBox
			// 
			this.attrRequiredBox.Location = new System.Drawing.Point(72, 32);
			this.attrRequiredBox.Name = "attrRequiredBox";
			this.attrRequiredBox.Size = new System.Drawing.Size(152, 19);
			this.attrRequiredBox.TabIndex = 1;
			// 
			// attrNameBox
			// 
			this.attrNameBox.Location = new System.Drawing.Point(72, 8);
			this.attrNameBox.Name = "attrNameBox";
			this.attrNameBox.Size = new System.Drawing.Size(152, 19);
			this.attrNameBox.TabIndex = 0;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(8, 108);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(57, 12);
			this.label5.TabIndex = 9;
			this.label5.Text = "長い説明：";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(8, 84);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(57, 12);
			this.label6.TabIndex = 8;
			this.label6.Text = "短い説明：";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(8, 36);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(47, 12);
			this.label7.TabIndex = 6;
			this.label7.Text = "必須？：";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(8, 12);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(47, 12);
			this.label8.TabIndex = 5;
			this.label8.Text = "属性名：";
			// 
			// defaultPanel
			// 
			this.defaultPanel.Controls.Add(this.label10);
			this.defaultPanel.Location = new System.Drawing.Point(192, 328);
			this.defaultPanel.Name = "defaultPanel";
			this.defaultPanel.Size = new System.Drawing.Size(200, 100);
			this.defaultPanel.TabIndex = 3;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(8, 16);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(116, 12);
			this.label10.TabIndex = 0;
			this.label10.Text = "ノードを選択してください";
			// 
			// openXmlFileDialog
			// 
			this.openXmlFileDialog.FileName = "openFileDialog1";
			this.openXmlFileDialog.Filter = "TagsXmlファイル(*.xml)|*.xml";
			// 
			// saveXmlFileDialog
			// 
			this.saveXmlFileDialog.DefaultExt = "xml";
			this.saveXmlFileDialog.Filter = "TagsXmlファイル(*.xml)|*.xml";
			// 
			// menuItemDebug
			// 
			this.menuItemDebug.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDebugOutputList});
			this.menuItemDebug.Name = "menuItemDebug";
			this.menuItemDebug.Size = new System.Drawing.Size(85, 22);
			this.menuItemDebug.Text = "デバッグ(&E)";
			// 
			// menuItemDebugOutputList
			// 
			this.menuItemDebugOutputList.Name = "menuItemDebugOutputList";
			this.menuItemDebugOutputList.Size = new System.Drawing.Size(153, 22);
			this.menuItemDebugOutputList.Text = "リスト表示(&L)";
			this.menuItemDebugOutputList.Click += new System.EventHandler(this.menuItemDebugOutputList_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(571, 557);
			this.Controls.Add(this.defaultPanel);
			this.Controls.Add(this.attrPanel);
			this.Controls.Add(this.tagPanel);
			this.Controls.Add(this.tagTreeView);
			this.Controls.Add(this.mainMenu);
			this.MainMenuStrip = this.mainMenu;
			this.Name = "MainForm";
			this.Text = "TagsXmlMaker";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.mainMenu.ResumeLayout(false);
			this.mainMenu.PerformLayout();
			this.popMenu.ResumeLayout(false);
			this.tagPanel.ResumeLayout(false);
			this.tagPanel.PerformLayout();
			this.attrPanel.ResumeLayout(false);
			this.attrPanel.PerformLayout();
			this.defaultPanel.ResumeLayout(false);
			this.defaultPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip mainMenu;
		private System.Windows.Forms.ToolStripMenuItem menuItemFile;
		private System.Windows.Forms.ToolStripMenuItem menuItemFileNew;
		private System.Windows.Forms.ToolStripMenuItem menuItemFileOpen;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem menuItemFileSave;
		private System.Windows.Forms.ToolStripMenuItem menuItemFileSaveAs;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem menuItemFileClose;
		private System.Windows.Forms.TreeView tagTreeView;
		private System.Windows.Forms.Panel tagPanel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tagLongInfoBox;
		private System.Windows.Forms.TextBox tagShortInfoBox;
		private System.Windows.Forms.TextBox tagGropuBox;
		private System.Windows.Forms.TextBox tagNameBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel attrPanel;
		private System.Windows.Forms.TextBox attrLongInfoBox;
		private System.Windows.Forms.TextBox attrShortInfoBox;
		private System.Windows.Forms.TextBox attrRequiredBox;
		private System.Windows.Forms.TextBox attrNameBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox attrFormatBox;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Panel defaultPanel;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.OpenFileDialog openXmlFileDialog;
		private System.Windows.Forms.SaveFileDialog saveXmlFileDialog;
		private System.Windows.Forms.ContextMenuStrip popMenu;
		private System.Windows.Forms.ToolStripMenuItem menuItemPopAddTag;
		private System.Windows.Forms.ToolStripMenuItem menuItemPopAddAttr;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem menuItemPopDelete;
		private System.Windows.Forms.ToolStripMenuItem menuItemDebug;
		private System.Windows.Forms.ToolStripMenuItem menuItemDebugOutputList;
	}
}

