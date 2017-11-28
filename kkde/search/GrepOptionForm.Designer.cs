namespace kkde.search
{
	partial class GrepOptionForm
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
			this.regexCheckBox = new System.Windows.Forms.CheckBox();
			this.wordUnitCheckBox = new System.Windows.Forms.CheckBox();
			this.ignoreCaseCheckBox = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.searchKeywordBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.fileExtBox = new System.Windows.Forms.ComboBox();
			this.grepPosFolderBox = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.grepSubFolderCheckBox = new System.Windows.Forms.CheckBox();
			this.grepPosFolderRefButton = new System.Windows.Forms.Button();
			this.grepPosFolderRadioButton = new System.Windows.Forms.RadioButton();
			this.grepPosProjectRadioButton = new System.Windows.Forms.RadioButton();
			this.fileExtRefButton = new System.Windows.Forms.Button();
			this.fileExtPopMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.fileExtMenuItemKrkr = new System.Windows.Forms.ToolStripMenuItem();
			this.fileExtMenuItemKag = new System.Windows.Forms.ToolStripMenuItem();
			this.fileExtMenuItemTjs = new System.Windows.Forms.ToolStripMenuItem();
			this.fileExtMenuItemTxt = new System.Windows.Forms.ToolStripMenuItem();
			this.fileExtMenuItemAll = new System.Windows.Forms.ToolStripMenuItem();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.searchButton = new System.Windows.Forms.Button();
			this.closeButton = new System.Windows.Forms.Button();
			this.grepFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.groupBox1.SuspendLayout();
			this.fileExtPopMenu.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// regexCheckBox
			// 
			this.regexCheckBox.AutoSize = true;
			this.regexCheckBox.Location = new System.Drawing.Point(16, 64);
			this.regexCheckBox.Name = "regexCheckBox";
			this.regexCheckBox.Size = new System.Drawing.Size(124, 16);
			this.regexCheckBox.TabIndex = 11;
			this.regexCheckBox.Text = "正規表現を使用する";
			this.regexCheckBox.UseVisualStyleBackColor = true;
			// 
			// wordUnitCheckBox
			// 
			this.wordUnitCheckBox.AutoSize = true;
			this.wordUnitCheckBox.Location = new System.Drawing.Point(16, 44);
			this.wordUnitCheckBox.Name = "wordUnitCheckBox";
			this.wordUnitCheckBox.Size = new System.Drawing.Size(135, 16);
			this.wordUnitCheckBox.TabIndex = 10;
			this.wordUnitCheckBox.Text = "単語単位での検索する";
			this.wordUnitCheckBox.UseVisualStyleBackColor = true;
			// 
			// ignoreCaseCheckBox
			// 
			this.ignoreCaseCheckBox.AutoSize = true;
			this.ignoreCaseCheckBox.Location = new System.Drawing.Point(16, 24);
			this.ignoreCaseCheckBox.Name = "ignoreCaseCheckBox";
			this.ignoreCaseCheckBox.Size = new System.Drawing.Size(148, 16);
			this.ignoreCaseCheckBox.TabIndex = 9;
			this.ignoreCaseCheckBox.Text = "大文字小文字を区別する";
			this.ignoreCaseCheckBox.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 12);
			this.label1.TabIndex = 12;
			this.label1.Text = "検索：";
			// 
			// searchKeywordBox
			// 
			this.searchKeywordBox.FormattingEnabled = true;
			this.searchKeywordBox.Location = new System.Drawing.Point(48, 8);
			this.searchKeywordBox.Name = "searchKeywordBox";
			this.searchKeywordBox.Size = new System.Drawing.Size(256, 20);
			this.searchKeywordBox.TabIndex = 8;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 12);
			this.label2.TabIndex = 13;
			this.label2.Text = "種類：";
			// 
			// fileExtBox
			// 
			this.fileExtBox.FormattingEnabled = true;
			this.fileExtBox.Location = new System.Drawing.Point(48, 32);
			this.fileExtBox.Name = "fileExtBox";
			this.fileExtBox.Size = new System.Drawing.Size(256, 20);
			this.fileExtBox.TabIndex = 14;
			// 
			// grepPosFolderBox
			// 
			this.grepPosFolderBox.FormattingEnabled = true;
			this.grepPosFolderBox.Location = new System.Drawing.Point(32, 64);
			this.grepPosFolderBox.Name = "grepPosFolderBox";
			this.grepPosFolderBox.Size = new System.Drawing.Size(264, 20);
			this.grepPosFolderBox.TabIndex = 15;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(0, 12);
			this.label3.TabIndex = 16;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.grepSubFolderCheckBox);
			this.groupBox1.Controls.Add(this.grepPosFolderRefButton);
			this.groupBox1.Controls.Add(this.grepPosFolderRadioButton);
			this.groupBox1.Controls.Add(this.grepPosProjectRadioButton);
			this.groupBox1.Controls.Add(this.grepPosFolderBox);
			this.groupBox1.Location = new System.Drawing.Point(8, 64);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(336, 112);
			this.groupBox1.TabIndex = 17;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "検索場所";
			// 
			// grepSubFolderCheckBox
			// 
			this.grepSubFolderCheckBox.AutoSize = true;
			this.grepSubFolderCheckBox.Location = new System.Drawing.Point(32, 88);
			this.grepSubFolderCheckBox.Name = "grepSubFolderCheckBox";
			this.grepSubFolderCheckBox.Size = new System.Drawing.Size(111, 16);
			this.grepSubFolderCheckBox.TabIndex = 17;
			this.grepSubFolderCheckBox.Text = "サブフォルダも検索";
			this.grepSubFolderCheckBox.UseVisualStyleBackColor = true;
			// 
			// grepPosFolderRefButton
			// 
			this.grepPosFolderRefButton.Location = new System.Drawing.Point(296, 64);
			this.grepPosFolderRefButton.Name = "grepPosFolderRefButton";
			this.grepPosFolderRefButton.Size = new System.Drawing.Size(32, 19);
			this.grepPosFolderRefButton.TabIndex = 16;
			this.grepPosFolderRefButton.Text = "...";
			this.grepPosFolderRefButton.UseVisualStyleBackColor = true;
			this.grepPosFolderRefButton.Click += new System.EventHandler(this.grepPosFolderRefButton_Click);
			// 
			// grepPosFolderRadioButton
			// 
			this.grepPosFolderRadioButton.AutoSize = true;
			this.grepPosFolderRadioButton.Location = new System.Drawing.Point(16, 40);
			this.grepPosFolderRadioButton.Name = "grepPosFolderRadioButton";
			this.grepPosFolderRadioButton.Size = new System.Drawing.Size(82, 16);
			this.grepPosFolderRadioButton.TabIndex = 1;
			this.grepPosFolderRadioButton.TabStop = true;
			this.grepPosFolderRadioButton.Text = "指定フォルダ";
			this.grepPosFolderRadioButton.UseVisualStyleBackColor = true;
			// 
			// grepPosProjectRadioButton
			// 
			this.grepPosProjectRadioButton.AutoSize = true;
			this.grepPosProjectRadioButton.Location = new System.Drawing.Point(16, 20);
			this.grepPosProjectRadioButton.Name = "grepPosProjectRadioButton";
			this.grepPosProjectRadioButton.Size = new System.Drawing.Size(74, 16);
			this.grepPosProjectRadioButton.TabIndex = 0;
			this.grepPosProjectRadioButton.TabStop = true;
			this.grepPosProjectRadioButton.Text = "プロジェクト";
			this.grepPosProjectRadioButton.UseVisualStyleBackColor = true;
			// 
			// fileExtRefButton
			// 
			this.fileExtRefButton.ContextMenuStrip = this.fileExtPopMenu;
			this.fileExtRefButton.Location = new System.Drawing.Point(304, 32);
			this.fileExtRefButton.Name = "fileExtRefButton";
			this.fileExtRefButton.Size = new System.Drawing.Size(32, 19);
			this.fileExtRefButton.TabIndex = 18;
			this.fileExtRefButton.Text = ">";
			this.fileExtRefButton.UseVisualStyleBackColor = true;
			this.fileExtRefButton.Click += new System.EventHandler(this.fileExtRefButton_Click);
			// 
			// fileExtPopMenu
			// 
			this.fileExtPopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileExtMenuItemKrkr,
            this.fileExtMenuItemKag,
            this.fileExtMenuItemTjs,
            this.fileExtMenuItemTxt,
            this.fileExtMenuItemAll});
			this.fileExtPopMenu.Name = "fileExtPopMenu";
			this.fileExtPopMenu.Size = new System.Drawing.Size(246, 114);
			// 
			// fileExtMenuItemKrkr
			// 
			this.fileExtMenuItemKrkr.Name = "fileExtMenuItemKrkr";
			this.fileExtMenuItemKrkr.Size = new System.Drawing.Size(245, 22);
			this.fileExtMenuItemKrkr.Text = "吉里吉里ファイル (*.ks;*.tjs)";
			this.fileExtMenuItemKrkr.Click += new System.EventHandler(this.fileExtMenuItemKrkr_Click);
			// 
			// fileExtMenuItemKag
			// 
			this.fileExtMenuItemKag.Name = "fileExtMenuItemKag";
			this.fileExtMenuItemKag.Size = new System.Drawing.Size(245, 22);
			this.fileExtMenuItemKag.Text = "KAGシナリオファイル (*.ks)";
			this.fileExtMenuItemKag.Click += new System.EventHandler(this.fileExtMenuItemKag_Click);
			// 
			// fileExtMenuItemTjs
			// 
			this.fileExtMenuItemTjs.Name = "fileExtMenuItemTjs";
			this.fileExtMenuItemTjs.Size = new System.Drawing.Size(245, 22);
			this.fileExtMenuItemTjs.Text = "TJSスクリプトファイル (*.tjs)";
			this.fileExtMenuItemTjs.Click += new System.EventHandler(this.fileExtMenuItemTjs_Click);
			// 
			// fileExtMenuItemTxt
			// 
			this.fileExtMenuItemTxt.Name = "fileExtMenuItemTxt";
			this.fileExtMenuItemTxt.Size = new System.Drawing.Size(245, 22);
			this.fileExtMenuItemTxt.Text = "テキストファイル (*.txt)";
			this.fileExtMenuItemTxt.Click += new System.EventHandler(this.fileExtMenuItemTxt_Click);
			// 
			// fileExtMenuItemAll
			// 
			this.fileExtMenuItemAll.Name = "fileExtMenuItemAll";
			this.fileExtMenuItemAll.Size = new System.Drawing.Size(245, 22);
			this.fileExtMenuItemAll.Text = "すべてのファイル (*.*)";
			this.fileExtMenuItemAll.Click += new System.EventHandler(this.fileExtMenuItemAll_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.ignoreCaseCheckBox);
			this.groupBox2.Controls.Add(this.wordUnitCheckBox);
			this.groupBox2.Controls.Add(this.regexCheckBox);
			this.groupBox2.Location = new System.Drawing.Point(8, 184);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(336, 88);
			this.groupBox2.TabIndex = 19;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "検索オプション";
			// 
			// searchButton
			// 
			this.searchButton.Location = new System.Drawing.Point(184, 280);
			this.searchButton.Name = "searchButton";
			this.searchButton.Size = new System.Drawing.Size(75, 23);
			this.searchButton.TabIndex = 20;
			this.searchButton.Text = "検索";
			this.searchButton.UseVisualStyleBackColor = true;
			this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
			// 
			// closeButton
			// 
			this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.closeButton.Location = new System.Drawing.Point(264, 280);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(75, 23);
			this.closeButton.TabIndex = 21;
			this.closeButton.Text = "閉じる";
			this.closeButton.UseVisualStyleBackColor = true;
			this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
			// 
			// grepFolderDialog
			// 
			this.grepFolderDialog.Description = "検索フォルダ";
			this.grepFolderDialog.ShowNewFolderButton = false;
			// 
			// GrepOptionForm
			// 
			this.AcceptButton = this.searchButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.closeButton;
			this.ClientSize = new System.Drawing.Size(351, 309);
			this.Controls.Add(this.closeButton);
			this.Controls.Add(this.searchButton);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.fileExtRefButton);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.fileExtBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.searchKeywordBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "GrepOptionForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "フォルダ検索ダイアログ";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.fileExtPopMenu.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox regexCheckBox;
		private System.Windows.Forms.CheckBox wordUnitCheckBox;
		private System.Windows.Forms.CheckBox ignoreCaseCheckBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox searchKeywordBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox fileExtBox;
		private System.Windows.Forms.ComboBox grepPosFolderBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton grepPosProjectRadioButton;
		private System.Windows.Forms.CheckBox grepSubFolderCheckBox;
		private System.Windows.Forms.Button grepPosFolderRefButton;
		private System.Windows.Forms.RadioButton grepPosFolderRadioButton;
		private System.Windows.Forms.Button fileExtRefButton;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button searchButton;
		private System.Windows.Forms.Button closeButton;
		private System.Windows.Forms.FolderBrowserDialog grepFolderDialog;
		private System.Windows.Forms.ContextMenuStrip fileExtPopMenu;
		private System.Windows.Forms.ToolStripMenuItem fileExtMenuItemKrkr;
		private System.Windows.Forms.ToolStripMenuItem fileExtMenuItemKag;
		private System.Windows.Forms.ToolStripMenuItem fileExtMenuItemTjs;
		private System.Windows.Forms.ToolStripMenuItem fileExtMenuItemTxt;
		private System.Windows.Forms.ToolStripMenuItem fileExtMenuItemAll;
	}
}