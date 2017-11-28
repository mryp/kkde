namespace kkde.search
{
	partial class SearchOptionForm
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
			this.searchKeywordBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.upSearchButton = new System.Windows.Forms.Button();
			this.downSearchButton = new System.Windows.Forms.Button();
			this.ignoreCaseCheckBox = new System.Windows.Forms.CheckBox();
			this.wordUnitCheckBox = new System.Windows.Forms.CheckBox();
			this.regexCheckBox = new System.Windows.Forms.CheckBox();
			this.closeButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// searchKeywordBox
			// 
			this.searchKeywordBox.FormattingEnabled = true;
			this.searchKeywordBox.Location = new System.Drawing.Point(48, 12);
			this.searchKeywordBox.Name = "searchKeywordBox";
			this.searchKeywordBox.Size = new System.Drawing.Size(264, 20);
			this.searchKeywordBox.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 12);
			this.label1.TabIndex = 7;
			this.label1.Text = "検索：";
			// 
			// upSearchButton
			// 
			this.upSearchButton.Location = new System.Drawing.Point(320, 12);
			this.upSearchButton.Name = "upSearchButton";
			this.upSearchButton.Size = new System.Drawing.Size(88, 23);
			this.upSearchButton.TabIndex = 1;
			this.upSearchButton.Text = "上方向検索";
			this.upSearchButton.UseVisualStyleBackColor = true;
			this.upSearchButton.Click += new System.EventHandler(this.upSearchButton_Click);
			// 
			// downSearchButton
			// 
			this.downSearchButton.Location = new System.Drawing.Point(320, 40);
			this.downSearchButton.Name = "downSearchButton";
			this.downSearchButton.Size = new System.Drawing.Size(88, 23);
			this.downSearchButton.TabIndex = 2;
			this.downSearchButton.Text = "下方向検索";
			this.downSearchButton.UseVisualStyleBackColor = true;
			this.downSearchButton.Click += new System.EventHandler(this.downSearchButton_Click);
			// 
			// ignoreCaseCheckBox
			// 
			this.ignoreCaseCheckBox.AutoSize = true;
			this.ignoreCaseCheckBox.Location = new System.Drawing.Point(48, 48);
			this.ignoreCaseCheckBox.Name = "ignoreCaseCheckBox";
			this.ignoreCaseCheckBox.Size = new System.Drawing.Size(148, 16);
			this.ignoreCaseCheckBox.TabIndex = 3;
			this.ignoreCaseCheckBox.Text = "大文字小文字を区別する";
			this.ignoreCaseCheckBox.UseVisualStyleBackColor = true;
			// 
			// wordUnitCheckBox
			// 
			this.wordUnitCheckBox.AutoSize = true;
			this.wordUnitCheckBox.Location = new System.Drawing.Point(48, 68);
			this.wordUnitCheckBox.Name = "wordUnitCheckBox";
			this.wordUnitCheckBox.Size = new System.Drawing.Size(135, 16);
			this.wordUnitCheckBox.TabIndex = 4;
			this.wordUnitCheckBox.Text = "単語単位での検索する";
			this.wordUnitCheckBox.UseVisualStyleBackColor = true;
			// 
			// regexCheckBox
			// 
			this.regexCheckBox.AutoSize = true;
			this.regexCheckBox.Location = new System.Drawing.Point(48, 88);
			this.regexCheckBox.Name = "regexCheckBox";
			this.regexCheckBox.Size = new System.Drawing.Size(124, 16);
			this.regexCheckBox.TabIndex = 5;
			this.regexCheckBox.Text = "正規表現を使用する";
			this.regexCheckBox.UseVisualStyleBackColor = true;
			// 
			// closeButton
			// 
			this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.closeButton.Location = new System.Drawing.Point(320, 80);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(88, 23);
			this.closeButton.TabIndex = 6;
			this.closeButton.Text = "閉じる";
			this.closeButton.UseVisualStyleBackColor = true;
			this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
			// 
			// SearchForm
			// 
			this.AcceptButton = this.downSearchButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.closeButton;
			this.ClientSize = new System.Drawing.Size(417, 111);
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
			this.Name = "SearchForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "検索ダイアログ";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox searchKeywordBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button upSearchButton;
		private System.Windows.Forms.Button downSearchButton;
		private System.Windows.Forms.CheckBox ignoreCaseCheckBox;
		private System.Windows.Forms.CheckBox wordUnitCheckBox;
		private System.Windows.Forms.CheckBox regexCheckBox;
		private System.Windows.Forms.Button closeButton;
	}
}