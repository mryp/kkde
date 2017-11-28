namespace kkde.project
{
	partial class AddFileForm
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
			this.addListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.pathBox = new System.Windows.Forms.TextBox();
			this.nameBox = new System.Windows.Forms.TextBox();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.pathRefButton = new System.Windows.Forms.Button();
			this.dirPathDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.SuspendLayout();
			// 
			// addListView
			// 
			this.addListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.addListView.Dock = System.Windows.Forms.DockStyle.Top;
			this.addListView.FullRowSelect = true;
			this.addListView.GridLines = true;
			this.addListView.HideSelection = false;
			this.addListView.Location = new System.Drawing.Point(0, 0);
			this.addListView.MultiSelect = false;
			this.addListView.Name = "addListView";
			this.addListView.Size = new System.Drawing.Size(390, 176);
			this.addListView.TabIndex = 0;
			this.addListView.UseCompatibleStateImageBehavior = false;
			this.addListView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "種類";
			this.columnHeader1.Width = 153;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "説明";
			this.columnHeader2.Width = 285;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 188);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "場所";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 212);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 12);
			this.label2.TabIndex = 4;
			this.label2.Text = "名前";
			// 
			// pathBox
			// 
			this.pathBox.Location = new System.Drawing.Point(48, 184);
			this.pathBox.Name = "pathBox";
			this.pathBox.ReadOnly = true;
			this.pathBox.Size = new System.Drawing.Size(296, 19);
			this.pathBox.TabIndex = 2;
			// 
			// nameBox
			// 
			this.nameBox.Location = new System.Drawing.Point(48, 208);
			this.nameBox.Name = "nameBox";
			this.nameBox.Size = new System.Drawing.Size(328, 19);
			this.nameBox.TabIndex = 5;
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(224, 232);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 6;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(304, 232);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 7;
			this.cancelButton.Text = "キャンセル";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// pathRefButton
			// 
			this.pathRefButton.Location = new System.Drawing.Point(344, 184);
			this.pathRefButton.Name = "pathRefButton";
			this.pathRefButton.Size = new System.Drawing.Size(32, 19);
			this.pathRefButton.TabIndex = 3;
			this.pathRefButton.Text = "...";
			this.pathRefButton.UseVisualStyleBackColor = true;
			this.pathRefButton.Click += new System.EventHandler(this.pathRefButton_Click);
			// 
			// dirPathDialog
			// 
			this.dirPathDialog.Description = "保存フォルダ選択";
			// 
			// AddFileForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(390, 261);
			this.Controls.Add(this.pathRefButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.nameBox);
			this.Controls.Add(this.pathBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.addListView);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddFileForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ファイルの追加";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView addListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox pathBox;
		private System.Windows.Forms.TextBox nameBox;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button pathRefButton;
		private System.Windows.Forms.FolderBrowserDialog dirPathDialog;
	}
}