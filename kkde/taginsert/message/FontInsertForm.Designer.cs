namespace kkde.taginsert.message
{
	partial class FontInsertForm
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.fontNameComboBox = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.italicCheckBox = new System.Windows.Forms.CheckBox();
			this.fontColorTextBox = new System.Windows.Forms.TextBox();
			this.boldCheckBox = new System.Windows.Forms.CheckBox();
			this.shadowCheckBox = new System.Windows.Forms.CheckBox();
			this.edgeCheckBox = new System.Windows.Forms.CheckBox();
			this.shadowColorTextBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.edgeColorTextBox = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.fontColorRefButton = new System.Windows.Forms.Button();
			this.shadowColorRefButton = new System.Windows.Forms.Button();
			this.edgeColorRefButton = new System.Windows.Forms.Button();
			this.fontSizeTextBox = new System.Windows.Forms.TextBox();
			this.colorSelectDialog = new System.Windows.Forms.ColorDialog();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(112, 248);
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(192, 248);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.fontSizeTextBox);
			this.groupBox1.Controls.Add(this.edgeColorRefButton);
			this.groupBox1.Controls.Add(this.shadowColorRefButton);
			this.groupBox1.Controls.Add(this.fontColorRefButton);
			this.groupBox1.Controls.Add(this.edgeColorTextBox);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.shadowColorTextBox);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.edgeCheckBox);
			this.groupBox1.Controls.Add(this.shadowCheckBox);
			this.groupBox1.Controls.Add(this.boldCheckBox);
			this.groupBox1.Controls.Add(this.fontColorTextBox);
			this.groupBox1.Controls.Add(this.italicCheckBox);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.fontNameComboBox);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(264, 232);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "フォント設定";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "フォント名：";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "サイズ：";
			// 
			// fontNameComboBox
			// 
			this.fontNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.fontNameComboBox.FormattingEnabled = true;
			this.fontNameComboBox.Location = new System.Drawing.Point(80, 24);
			this.fontNameComboBox.Name = "fontNameComboBox";
			this.fontNameComboBox.Size = new System.Drawing.Size(121, 20);
			this.fontNameComboBox.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(16, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(23, 12);
			this.label3.TabIndex = 6;
			this.label3.Text = "色：";
			// 
			// italicCheckBox
			// 
			this.italicCheckBox.AutoSize = true;
			this.italicCheckBox.Location = new System.Drawing.Point(16, 96);
			this.italicCheckBox.Name = "italicCheckBox";
			this.italicCheckBox.Size = new System.Drawing.Size(101, 16);
			this.italicCheckBox.TabIndex = 7;
			this.italicCheckBox.Text = "斜体で表示する";
			this.italicCheckBox.UseVisualStyleBackColor = true;
			// 
			// fontColorTextBox
			// 
			this.fontColorTextBox.Location = new System.Drawing.Point(80, 72);
			this.fontColorTextBox.Name = "fontColorTextBox";
			this.fontColorTextBox.Size = new System.Drawing.Size(120, 19);
			this.fontColorTextBox.TabIndex = 8;
			// 
			// boldCheckBox
			// 
			this.boldCheckBox.AutoSize = true;
			this.boldCheckBox.Location = new System.Drawing.Point(16, 112);
			this.boldCheckBox.Name = "boldCheckBox";
			this.boldCheckBox.Size = new System.Drawing.Size(101, 16);
			this.boldCheckBox.TabIndex = 9;
			this.boldCheckBox.Text = "太字で表示する";
			this.boldCheckBox.UseVisualStyleBackColor = true;
			// 
			// shadowCheckBox
			// 
			this.shadowCheckBox.AutoSize = true;
			this.shadowCheckBox.Location = new System.Drawing.Point(16, 128);
			this.shadowCheckBox.Name = "shadowCheckBox";
			this.shadowCheckBox.Size = new System.Drawing.Size(119, 16);
			this.shadowCheckBox.TabIndex = 10;
			this.shadowCheckBox.Text = "影を付けて表示する";
			this.shadowCheckBox.UseVisualStyleBackColor = true;
			// 
			// edgeCheckBox
			// 
			this.edgeCheckBox.AutoSize = true;
			this.edgeCheckBox.Location = new System.Drawing.Point(16, 176);
			this.edgeCheckBox.Name = "edgeCheckBox";
			this.edgeCheckBox.Size = new System.Drawing.Size(139, 16);
			this.edgeCheckBox.TabIndex = 11;
			this.edgeCheckBox.Text = "縁取りを付けて表示する";
			this.edgeCheckBox.UseVisualStyleBackColor = true;
			// 
			// shadowColorTextBox
			// 
			this.shadowColorTextBox.Location = new System.Drawing.Point(96, 152);
			this.shadowColorTextBox.Name = "shadowColorTextBox";
			this.shadowColorTextBox.Size = new System.Drawing.Size(120, 19);
			this.shadowColorTextBox.TabIndex = 13;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(32, 152);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(45, 12);
			this.label4.TabIndex = 12;
			this.label4.Text = "影の色：";
			// 
			// edgeColorTextBox
			// 
			this.edgeColorTextBox.Location = new System.Drawing.Point(96, 200);
			this.edgeColorTextBox.Name = "edgeColorTextBox";
			this.edgeColorTextBox.Size = new System.Drawing.Size(120, 19);
			this.edgeColorTextBox.TabIndex = 15;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(32, 200);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(65, 12);
			this.label5.TabIndex = 14;
			this.label5.Text = "縁取りの色：";
			// 
			// fontColorRefButton
			// 
			this.fontColorRefButton.Location = new System.Drawing.Point(200, 72);
			this.fontColorRefButton.Name = "fontColorRefButton";
			this.fontColorRefButton.Size = new System.Drawing.Size(32, 19);
			this.fontColorRefButton.TabIndex = 16;
			this.fontColorRefButton.Text = "...";
			this.fontColorRefButton.UseVisualStyleBackColor = true;
			this.fontColorRefButton.Click += new System.EventHandler(this.fontColorRefButton_Click);
			// 
			// shadowColorRefButton
			// 
			this.shadowColorRefButton.Location = new System.Drawing.Point(216, 152);
			this.shadowColorRefButton.Name = "shadowColorRefButton";
			this.shadowColorRefButton.Size = new System.Drawing.Size(32, 19);
			this.shadowColorRefButton.TabIndex = 17;
			this.shadowColorRefButton.Text = "...";
			this.shadowColorRefButton.UseVisualStyleBackColor = true;
			this.shadowColorRefButton.Click += new System.EventHandler(this.shadowColorRefButton_Click);
			// 
			// edgeColorRefButton
			// 
			this.edgeColorRefButton.Location = new System.Drawing.Point(216, 200);
			this.edgeColorRefButton.Name = "edgeColorRefButton";
			this.edgeColorRefButton.Size = new System.Drawing.Size(32, 19);
			this.edgeColorRefButton.TabIndex = 18;
			this.edgeColorRefButton.Text = "...";
			this.edgeColorRefButton.UseVisualStyleBackColor = true;
			this.edgeColorRefButton.Click += new System.EventHandler(this.edgeColorRefButton_Click);
			// 
			// fontSizeTextBox
			// 
			this.fontSizeTextBox.Location = new System.Drawing.Point(80, 48);
			this.fontSizeTextBox.Name = "fontSizeTextBox";
			this.fontSizeTextBox.Size = new System.Drawing.Size(120, 19);
			this.fontSizeTextBox.TabIndex = 19;
			// 
			// colorSelectDialog
			// 
			this.colorSelectDialog.FullOpen = true;
			// 
			// FontInsertForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.ClientSize = new System.Drawing.Size(279, 276);
			this.Controls.Add(this.groupBox1);
			this.Name = "FontInsertForm";
			this.Controls.SetChildIndex(this.cancelButton, 0);
			this.Controls.SetChildIndex(this.okButton, 0);
			this.Controls.SetChildIndex(this.groupBox1, 0);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox edgeCheckBox;
		private System.Windows.Forms.CheckBox shadowCheckBox;
		private System.Windows.Forms.CheckBox boldCheckBox;
		private System.Windows.Forms.TextBox fontColorTextBox;
		private System.Windows.Forms.CheckBox italicCheckBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox fontNameComboBox;
		private System.Windows.Forms.TextBox edgeColorTextBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox shadowColorTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button edgeColorRefButton;
		private System.Windows.Forms.Button shadowColorRefButton;
		private System.Windows.Forms.Button fontColorRefButton;
		private System.Windows.Forms.TextBox fontSizeTextBox;
		private System.Windows.Forms.ColorDialog colorSelectDialog;
	}
}
