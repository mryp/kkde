namespace kkde.help
{
	partial class VersionForm
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
			this.okButton = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.mailLinkLabel = new System.Windows.Forms.LinkLabel();
			this.webLinkLabel = new System.Windows.Forms.LinkLabel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.systemNetLabel = new System.Windows.Forms.Label();
			this.systemOSLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(48, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(265, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "KiriKiri Development Environment version 2.0.0 α5";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(48, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(211, 12);
			this.label2.TabIndex = 1;
			this.label2.Text = "Copyright (C) 2005-2010 PORING SOFT";
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.okButton.Location = new System.Drawing.Point(232, 208);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 2;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::kkde.Properties.Resources.icon_32;
			this.pictureBox1.InitialImage = null;
			this.pictureBox1.Location = new System.Drawing.Point(8, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			this.pictureBox1.TabIndex = 3;
			this.pictureBox1.TabStop = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.mailLinkLabel);
			this.groupBox1.Controls.Add(this.webLinkLabel);
			this.groupBox1.Location = new System.Drawing.Point(8, 48);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(304, 72);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "サポート";
			// 
			// mailLinkLabel
			// 
			this.mailLinkLabel.AutoSize = true;
			this.mailLinkLabel.Location = new System.Drawing.Point(16, 44);
			this.mailLinkLabel.Name = "mailLinkLabel";
			this.mailLinkLabel.Size = new System.Drawing.Size(175, 12);
			this.mailLinkLabel.TabIndex = 1;
			this.mailLinkLabel.TabStop = true;
			this.mailLinkLabel.Text = "メールアドレス： mry@poringsoft.net";
			this.mailLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.mailLinkLabel_LinkClicked);
			// 
			// webLinkLabel
			// 
			this.webLinkLabel.AutoSize = true;
			this.webLinkLabel.Location = new System.Drawing.Point(16, 24);
			this.webLinkLabel.Name = "webLinkLabel";
			this.webLinkLabel.Size = new System.Drawing.Size(201, 12);
			this.webLinkLabel.TabIndex = 0;
			this.webLinkLabel.TabStop = true;
			this.webLinkLabel.Text = "WEBサイト： http://www.poringsoft.net/";
			this.webLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.webLinkLabel_LinkClicked);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.systemNetLabel);
			this.groupBox2.Controls.Add(this.systemOSLabel);
			this.groupBox2.Location = new System.Drawing.Point(8, 128);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(304, 72);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "システム情報";
			// 
			// systemNetLabel
			// 
			this.systemNetLabel.AutoSize = true;
			this.systemNetLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.systemNetLabel.Location = new System.Drawing.Point(16, 44);
			this.systemNetLabel.Name = "systemNetLabel";
			this.systemNetLabel.Size = new System.Drawing.Size(89, 12);
			this.systemNetLabel.TabIndex = 1;
			this.systemNetLabel.Text = ".NET Framework";
			// 
			// systemOSLabel
			// 
			this.systemOSLabel.AutoSize = true;
			this.systemOSLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.systemOSLabel.Location = new System.Drawing.Point(16, 24);
			this.systemOSLabel.Name = "systemOSLabel";
			this.systemOSLabel.Size = new System.Drawing.Size(17, 12);
			this.systemOSLabel.TabIndex = 0;
			this.systemOSLabel.Text = "OS";
			// 
			// VersionForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.okButton;
			this.ClientSize = new System.Drawing.Size(318, 236);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "VersionForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "バージョン情報";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.LinkLabel mailLinkLabel;
		private System.Windows.Forms.LinkLabel webLinkLabel;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label systemNetLabel;
		private System.Windows.Forms.Label systemOSLabel;
	}
}