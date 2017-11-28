namespace TagsConverter
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
			this.inputOpenDialog = new System.Windows.Forms.OpenFileDialog();
			this.outputRefButton = new System.Windows.Forms.Button();
			this.inputRefButton = new System.Windows.Forms.Button();
			this.helpButton = new System.Windows.Forms.Button();
			this.closeButton = new System.Windows.Forms.Button();
			this.convertButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.inputBox = new System.Windows.Forms.TextBox();
			this.outputBox = new System.Windows.Forms.TextBox();
			this.outputSaveDialog = new System.Windows.Forms.SaveFileDialog();
			this.SuspendLayout();
			// 
			// inputOpenDialog
			// 
			this.inputOpenDialog.DefaultExt = "*.xml";
			this.inputOpenDialog.FileName = "tags.xml";
			this.inputOpenDialog.Filter = "tags.xml (*.xml)|*.xml";
			this.inputOpenDialog.Title = "入力ファイル";
			// 
			// outputRefButton
			// 
			this.outputRefButton.Location = new System.Drawing.Point(320, 36);
			this.outputRefButton.Name = "outputRefButton";
			this.outputRefButton.Size = new System.Drawing.Size(32, 19);
			this.outputRefButton.TabIndex = 17;
			this.outputRefButton.Text = "...";
			this.outputRefButton.UseVisualStyleBackColor = true;
			this.outputRefButton.Click += new System.EventHandler(this.outputRefButton_Click);
			// 
			// inputRefButton
			// 
			this.inputRefButton.Location = new System.Drawing.Point(320, 12);
			this.inputRefButton.Name = "inputRefButton";
			this.inputRefButton.Size = new System.Drawing.Size(32, 19);
			this.inputRefButton.TabIndex = 16;
			this.inputRefButton.Text = "...";
			this.inputRefButton.UseVisualStyleBackColor = true;
			this.inputRefButton.Click += new System.EventHandler(this.inputRefButton_Click);
			// 
			// helpButton
			// 
			this.helpButton.Location = new System.Drawing.Point(8, 68);
			this.helpButton.Name = "helpButton";
			this.helpButton.Size = new System.Drawing.Size(75, 23);
			this.helpButton.TabIndex = 15;
			this.helpButton.Text = "ヘルプ";
			this.helpButton.UseVisualStyleBackColor = true;
			this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
			// 
			// closeButton
			// 
			this.closeButton.Location = new System.Drawing.Point(272, 68);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(75, 23);
			this.closeButton.TabIndex = 14;
			this.closeButton.Text = "閉じる";
			this.closeButton.UseVisualStyleBackColor = true;
			this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
			// 
			// convertButton
			// 
			this.convertButton.Location = new System.Drawing.Point(192, 68);
			this.convertButton.Name = "convertButton";
			this.convertButton.Size = new System.Drawing.Size(75, 23);
			this.convertButton.TabIndex = 13;
			this.convertButton.Text = "変換";
			this.convertButton.UseVisualStyleBackColor = true;
			this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(69, 12);
			this.label2.TabIndex = 12;
			this.label2.Text = "出力ファイル：";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(69, 12);
			this.label1.TabIndex = 11;
			this.label1.Text = "入力ファイル：";
			// 
			// inputBox
			// 
			this.inputBox.Location = new System.Drawing.Point(80, 12);
			this.inputBox.Name = "inputBox";
			this.inputBox.Size = new System.Drawing.Size(240, 19);
			this.inputBox.TabIndex = 9;
			// 
			// outputBox
			// 
			this.outputBox.Location = new System.Drawing.Point(80, 36);
			this.outputBox.Name = "outputBox";
			this.outputBox.Size = new System.Drawing.Size(240, 19);
			this.outputBox.TabIndex = 10;
			// 
			// outputSaveDialog
			// 
			this.outputSaveDialog.Filter = "kag3tag.ks (*.ks)|*.ks";
			this.outputSaveDialog.Title = "出力ファイル";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(357, 97);
			this.Controls.Add(this.outputRefButton);
			this.Controls.Add(this.inputRefButton);
			this.Controls.Add(this.helpButton);
			this.Controls.Add(this.closeButton);
			this.Controls.Add(this.convertButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.inputBox);
			this.Controls.Add(this.outputBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.Text = "tags.xml->kag3tag.ks";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.OpenFileDialog inputOpenDialog;
		private System.Windows.Forms.Button outputRefButton;
		private System.Windows.Forms.Button inputRefButton;
		private System.Windows.Forms.Button helpButton;
		private System.Windows.Forms.Button closeButton;
		private System.Windows.Forms.Button convertButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox inputBox;
		private System.Windows.Forms.TextBox outputBox;
		private System.Windows.Forms.SaveFileDialog outputSaveDialog;

	}
}

