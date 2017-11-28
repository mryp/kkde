namespace kkde.search
{
	partial class ReplaceOptionForm
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
			this.closeButton = new System.Windows.Forms.Button();
			this.regexCheckBox = new System.Windows.Forms.CheckBox();
			this.wordUnitCheckBox = new System.Windows.Forms.CheckBox();
			this.ignoreCaseCheckBox = new System.Windows.Forms.CheckBox();
			this.downSearchButton = new System.Windows.Forms.Button();
			this.upSearchButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.searchKeywordBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.replaceKeywordBox = new System.Windows.Forms.ComboBox();
			this.allReplaceButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// closeButton
			// 
			this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.closeButton.Location = new System.Drawing.Point(320, 96);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(88, 23);
			this.closeButton.TabIndex = 7;
			this.closeButton.Text = "閉じる";
			this.closeButton.UseVisualStyleBackColor = true;
			this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
			// 
			// regexCheckBox
			// 
			this.regexCheckBox.AutoSize = true;
			this.regexCheckBox.Location = new System.Drawing.Point(48, 104);
			this.regexCheckBox.Name = "regexCheckBox";
			this.regexCheckBox.Size = new System.Drawing.Size(124, 16);
			this.regexCheckBox.TabIndex = 6;
			this.regexCheckBox.Text = "正規表現を使用する";
			this.regexCheckBox.UseVisualStyleBackColor = true;
			// 
			// wordUnitCheckBox
			// 
			this.wordUnitCheckBox.AutoSize = true;
			this.wordUnitCheckBox.Location = new System.Drawing.Point(48, 84);
			this.wordUnitCheckBox.Name = "wordUnitCheckBox";
			this.wordUnitCheckBox.Size = new System.Drawing.Size(135, 16);
			this.wordUnitCheckBox.TabIndex = 5;
			this.wordUnitCheckBox.Text = "単語単位での検索する";
			this.wordUnitCheckBox.UseVisualStyleBackColor = true;
			// 
			// ignoreCaseCheckBox
			// 
			this.ignoreCaseCheckBox.AutoSize = true;
			this.ignoreCaseCheckBox.Location = new System.Drawing.Point(48, 64);
			this.ignoreCaseCheckBox.Name = "ignoreCaseCheckBox";
			this.ignoreCaseCheckBox.Size = new System.Drawing.Size(148, 16);
			this.ignoreCaseCheckBox.TabIndex = 4;
			this.ignoreCaseCheckBox.Text = "大文字小文字を区別する";
			this.ignoreCaseCheckBox.UseVisualStyleBackColor = true;
			// 
			// downSearchButton
			// 
			this.downSearchButton.Location = new System.Drawing.Point(320, 36);
			this.downSearchButton.Name = "downSearchButton";
			this.downSearchButton.Size = new System.Drawing.Size(88, 23);
			this.downSearchButton.TabIndex = 3;
			this.downSearchButton.Text = "下方向置換";
			this.downSearchButton.UseVisualStyleBackColor = true;
			this.downSearchButton.Click += new System.EventHandler(this.downSearchButton_Click);
			// 
			// upSearchButton
			// 
			this.upSearchButton.Location = new System.Drawing.Point(320, 8);
			this.upSearchButton.Name = "upSearchButton";
			this.upSearchButton.Size = new System.Drawing.Size(88, 23);
			this.upSearchButton.TabIndex = 2;
			this.upSearchButton.Text = "上方向置換";
			this.upSearchButton.UseVisualStyleBackColor = true;
			this.upSearchButton.Click += new System.EventHandler(this.upSearchButton_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 12);
			this.label1.TabIndex = 8;
			this.label1.Text = "検索：";
			// 
			// searchKeywordBox
			// 
			this.searchKeywordBox.FormattingEnabled = true;
			this.searchKeywordBox.Location = new System.Drawing.Point(48, 8);
			this.searchKeywordBox.Name = "searchKeywordBox";
			this.searchKeywordBox.Size = new System.Drawing.Size(264, 20);
			this.searchKeywordBox.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 12);
			this.label2.TabIndex = 9;
			this.label2.Text = "置換：";
			// 
			// replaceKeywordBox
			// 
			this.replaceKeywordBox.FormattingEnabled = true;
			this.replaceKeywordBox.Location = new System.Drawing.Point(48, 36);
			this.replaceKeywordBox.Name = "replaceKeywordBox";
			this.replaceKeywordBox.Size = new System.Drawing.Size(264, 20);
			this.replaceKeywordBox.TabIndex = 1;
			// 
			// allReplaceButton
			// 
			this.allReplaceButton.Location = new System.Drawing.Point(320, 64);
			this.allReplaceButton.Name = "allReplaceButton";
			this.allReplaceButton.Size = new System.Drawing.Size(88, 23);
			this.allReplaceButton.TabIndex = 10;
			this.allReplaceButton.Text = "全置換";
			this.allReplaceButton.UseVisualStyleBackColor = true;
			this.allReplaceButton.Click += new System.EventHandler(this.allReplaceButton_Click);
			// 
			// ReplaceForm
			// 
			this.AcceptButton = this.downSearchButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.closeButton;
			this.ClientSize = new System.Drawing.Size(414, 127);
			this.Controls.Add(this.allReplaceButton);
			this.Controls.Add(this.replaceKeywordBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.closeButton);
			this.Controls.Add(this.regexCheckBox);
			this.Controls.Add(this.wordUnitCheckBox);
			this.Controls.Add(this.ignoreCaseCheckBox);
			this.Controls.Add(this.downSearchButton);
			this.Controls.Add(this.upSearchButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.searchKeywordBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ReplaceForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "置換ダイアログ";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button closeButton;
		private System.Windows.Forms.CheckBox regexCheckBox;
		private System.Windows.Forms.CheckBox wordUnitCheckBox;
		private System.Windows.Forms.CheckBox ignoreCaseCheckBox;
		private System.Windows.Forms.Button downSearchButton;
		private System.Windows.Forms.Button upSearchButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox searchKeywordBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox replaceKeywordBox;
		private System.Windows.Forms.Button allReplaceButton;
	}
}