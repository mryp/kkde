namespace kkde.project
{
	partial class ProjectPropertyForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.nameBox = new System.Windows.Forms.TextBox();
			this.dataPathBox = new System.Windows.Forms.TextBox();
			this.exePathBox = new System.Windows.Forms.TextBox();
			this.kindComboBox = new System.Windows.Forms.ComboBox();
			this.dataPathRefButton = new System.Windows.Forms.Button();
			this.exePathRefButton = new System.Windows.Forms.Button();
			this.saveButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.dataFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.exeFileOpenDialog = new System.Windows.Forms.OpenFileDialog();
			this.exeArgvBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.filePathBox = new System.Windows.Forms.TextBox();
			this.filePathRefButton = new System.Windows.Forms.Button();
			this.projectFileSaveDialog = new System.Windows.Forms.SaveFileDialog();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(74, 12);
			this.label1.TabIndex = 12;
			this.label1.Text = "プロジェクト名：";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(93, 12);
			this.label2.TabIndex = 13;
			this.label2.Text = "データフォルダパス：";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 88);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 12);
			this.label3.TabIndex = 14;
			this.label3.Text = "実行ファイルパス：";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(8, 136);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(96, 12);
			this.label4.TabIndex = 16;
			this.label4.Text = "プロジェクトの種類：";
			// 
			// nameBox
			// 
			this.nameBox.Location = new System.Drawing.Point(104, 36);
			this.nameBox.Name = "nameBox";
			this.nameBox.Size = new System.Drawing.Size(304, 19);
			this.nameBox.TabIndex = 2;
			// 
			// dataPathBox
			// 
			this.dataPathBox.Location = new System.Drawing.Point(104, 60);
			this.dataPathBox.Name = "dataPathBox";
			this.dataPathBox.Size = new System.Drawing.Size(304, 19);
			this.dataPathBox.TabIndex = 3;
			// 
			// exePathBox
			// 
			this.exePathBox.Location = new System.Drawing.Point(104, 84);
			this.exePathBox.Name = "exePathBox";
			this.exePathBox.Size = new System.Drawing.Size(304, 19);
			this.exePathBox.TabIndex = 5;
			// 
			// kindComboBox
			// 
			this.kindComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.kindComboBox.FormattingEnabled = true;
			this.kindComboBox.Items.AddRange(new object[] {
            "空のプロジェクト",
            "KAG3プロジェクト",
            "KAGEX++プロジェクト"});
			this.kindComboBox.Location = new System.Drawing.Point(104, 132);
			this.kindComboBox.Name = "kindComboBox";
			this.kindComboBox.Size = new System.Drawing.Size(168, 20);
			this.kindComboBox.TabIndex = 8;
			// 
			// dataPathRefButton
			// 
			this.dataPathRefButton.Location = new System.Drawing.Point(408, 60);
			this.dataPathRefButton.Name = "dataPathRefButton";
			this.dataPathRefButton.Size = new System.Drawing.Size(32, 19);
			this.dataPathRefButton.TabIndex = 4;
			this.dataPathRefButton.Text = "...";
			this.dataPathRefButton.UseVisualStyleBackColor = true;
			this.dataPathRefButton.Click += new System.EventHandler(this.dataPathRefButton_Click);
			// 
			// exePathRefButton
			// 
			this.exePathRefButton.Location = new System.Drawing.Point(408, 84);
			this.exePathRefButton.Name = "exePathRefButton";
			this.exePathRefButton.Size = new System.Drawing.Size(32, 19);
			this.exePathRefButton.TabIndex = 6;
			this.exePathRefButton.Text = "...";
			this.exePathRefButton.UseVisualStyleBackColor = true;
			this.exePathRefButton.Click += new System.EventHandler(this.exePathRefButton_Click);
			// 
			// saveButton
			// 
			this.saveButton.Location = new System.Drawing.Point(280, 164);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(75, 23);
			this.saveButton.TabIndex = 9;
			this.saveButton.Text = "保存";
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(360, 164);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 10;
			this.cancelButton.Text = "キャンセル";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// dataFolderDialog
			// 
			this.dataFolderDialog.Description = "データフォルダパスを選択";
			this.dataFolderDialog.ShowNewFolderButton = false;
			// 
			// exeFileOpenDialog
			// 
			this.exeFileOpenDialog.DefaultExt = "exe";
			this.exeFileOpenDialog.FileName = "krkr.exe";
			this.exeFileOpenDialog.Filter = "実行ファイル (*.exe)|*.exe";
			this.exeFileOpenDialog.Tag = "実行ファイルパスを選択";
			// 
			// exeArgvBox
			// 
			this.exeArgvBox.Location = new System.Drawing.Point(104, 108);
			this.exeArgvBox.Name = "exeArgvBox";
			this.exeArgvBox.Size = new System.Drawing.Size(304, 19);
			this.exeArgvBox.TabIndex = 7;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(8, 112);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(89, 12);
			this.label5.TabIndex = 15;
			this.label5.Text = "実行パラメーター：";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(8, 16);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(64, 12);
			this.label6.TabIndex = 11;
			this.label6.Text = "ファイルパス：";
			// 
			// filePathBox
			// 
			this.filePathBox.Location = new System.Drawing.Point(104, 12);
			this.filePathBox.Name = "filePathBox";
			this.filePathBox.Size = new System.Drawing.Size(304, 19);
			this.filePathBox.TabIndex = 0;
			// 
			// filePathRefButton
			// 
			this.filePathRefButton.Location = new System.Drawing.Point(408, 12);
			this.filePathRefButton.Name = "filePathRefButton";
			this.filePathRefButton.Size = new System.Drawing.Size(32, 19);
			this.filePathRefButton.TabIndex = 1;
			this.filePathRefButton.Text = "...";
			this.filePathRefButton.UseVisualStyleBackColor = true;
			this.filePathRefButton.Click += new System.EventHandler(this.filePathRefButton_Click);
			// 
			// projectFileSaveDialog
			// 
			this.projectFileSaveDialog.Title = "プロジェクトファイルパス選択";
			// 
			// ProjectPropertyForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(448, 193);
			this.Controls.Add(this.filePathRefButton);
			this.Controls.Add(this.filePathBox);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.exeArgvBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.saveButton);
			this.Controls.Add(this.exePathRefButton);
			this.Controls.Add(this.dataPathRefButton);
			this.Controls.Add(this.kindComboBox);
			this.Controls.Add(this.exePathBox);
			this.Controls.Add(this.dataPathBox);
			this.Controls.Add(this.nameBox);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProjectPropertyForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "プロパティ";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox nameBox;
		private System.Windows.Forms.TextBox dataPathBox;
		private System.Windows.Forms.TextBox exePathBox;
		private System.Windows.Forms.ComboBox kindComboBox;
		private System.Windows.Forms.Button dataPathRefButton;
		private System.Windows.Forms.Button exePathRefButton;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.FolderBrowserDialog dataFolderDialog;
		private System.Windows.Forms.OpenFileDialog exeFileOpenDialog;
		private System.Windows.Forms.TextBox exeArgvBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox filePathBox;
		private System.Windows.Forms.Button filePathRefButton;
		private System.Windows.Forms.SaveFileDialog projectFileSaveDialog;
	}
}