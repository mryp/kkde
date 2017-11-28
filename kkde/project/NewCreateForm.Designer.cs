namespace kkde.project
{
	partial class NewCreateForm
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.projectKagexppRadioButton = new System.Windows.Forms.RadioButton();
			this.projectKag3RadioButton = new System.Windows.Forms.RadioButton();
			this.projectEmptyRadioButton = new System.Windows.Forms.RadioButton();
			this.okButton = new System.Windows.Forms.Button();
			this.saveDirectoryTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.projectNameTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.cancelButton = new System.Windows.Forms.Button();
			this.refSavePathButton = new System.Windows.Forms.Button();
			this.saveFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.projectKagexppRadioButton);
			this.groupBox1.Controls.Add(this.projectKag3RadioButton);
			this.groupBox1.Controls.Add(this.projectEmptyRadioButton);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(320, 88);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "プロジェクトの種類";
			// 
			// projectKagexppRadioButton
			// 
			this.projectKagexppRadioButton.AutoSize = true;
			this.projectKagexppRadioButton.Checked = true;
			this.projectKagexppRadioButton.Location = new System.Drawing.Point(16, 60);
			this.projectKagexppRadioButton.Name = "projectKagexppRadioButton";
			this.projectKagexppRadioButton.Size = new System.Drawing.Size(127, 16);
			this.projectKagexppRadioButton.TabIndex = 2;
			this.projectKagexppRadioButton.TabStop = true;
			this.projectKagexppRadioButton.Text = "KAGEX++ プロジェクト";
			this.projectKagexppRadioButton.UseVisualStyleBackColor = true;
			// 
			// projectKag3RadioButton
			// 
			this.projectKag3RadioButton.AutoSize = true;
			this.projectKag3RadioButton.Location = new System.Drawing.Point(16, 40);
			this.projectKag3RadioButton.Name = "projectKag3RadioButton";
			this.projectKag3RadioButton.Size = new System.Drawing.Size(107, 16);
			this.projectKag3RadioButton.TabIndex = 1;
			this.projectKag3RadioButton.TabStop = true;
			this.projectKag3RadioButton.Text = "KAG3 プロジェクト";
			this.projectKag3RadioButton.UseVisualStyleBackColor = true;
			// 
			// projectEmptyRadioButton
			// 
			this.projectEmptyRadioButton.AutoSize = true;
			this.projectEmptyRadioButton.Location = new System.Drawing.Point(16, 20);
			this.projectEmptyRadioButton.Name = "projectEmptyRadioButton";
			this.projectEmptyRadioButton.Size = new System.Drawing.Size(96, 16);
			this.projectEmptyRadioButton.TabIndex = 0;
			this.projectEmptyRadioButton.TabStop = true;
			this.projectEmptyRadioButton.Text = "空のプロジェクト";
			this.projectEmptyRadioButton.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(168, 160);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 6;
			this.okButton.Text = "作成";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// saveDirectoryTextBox
			// 
			this.saveDirectoryTextBox.Location = new System.Drawing.Point(48, 128);
			this.saveDirectoryTextBox.Name = "saveDirectoryTextBox";
			this.saveDirectoryTextBox.Size = new System.Drawing.Size(256, 19);
			this.saveDirectoryTextBox.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 132);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 12);
			this.label1.TabIndex = 3;
			this.label1.Text = "場所：";
			// 
			// projectNameTextBox
			// 
			this.projectNameTextBox.Location = new System.Drawing.Point(88, 104);
			this.projectNameTextBox.Name = "projectNameTextBox";
			this.projectNameTextBox.Size = new System.Drawing.Size(240, 19);
			this.projectNameTextBox.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 108);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(74, 12);
			this.label2.TabIndex = 1;
			this.label2.Text = "プロジェクト名：";
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(248, 160);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 7;
			this.cancelButton.Text = "キャンセル";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// refSavePathButton
			// 
			this.refSavePathButton.Location = new System.Drawing.Point(304, 128);
			this.refSavePathButton.Name = "refSavePathButton";
			this.refSavePathButton.Size = new System.Drawing.Size(24, 19);
			this.refSavePathButton.TabIndex = 5;
			this.refSavePathButton.Text = "...";
			this.refSavePathButton.UseVisualStyleBackColor = true;
			this.refSavePathButton.Click += new System.EventHandler(this.refSavePathButton_Click);
			// 
			// saveFolderDialog
			// 
			this.saveFolderDialog.Description = "保存先パス選択";
			// 
			// NewCreateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(336, 188);
			this.Controls.Add(this.refSavePathButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.projectNameTextBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.saveDirectoryTextBox);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NewCreateForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "新規作成";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton projectKagexppRadioButton;
		private System.Windows.Forms.RadioButton projectKag3RadioButton;
		private System.Windows.Forms.RadioButton projectEmptyRadioButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.TextBox saveDirectoryTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox projectNameTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button refSavePathButton;
		private System.Windows.Forms.FolderBrowserDialog saveFolderDialog;
	}
}