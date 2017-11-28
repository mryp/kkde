namespace kkde.project
{
	partial class FilePropertyForm
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
			this.label4 = new System.Windows.Forms.Label();
			this.nameLabel = new System.Windows.Forms.Label();
			this.dirPathLabel = new System.Windows.Forms.Label();
			this.kindLabel = new System.Windows.Forms.Label();
			this.closeButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "名前：";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(8, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 12);
			this.label2.TabIndex = 1;
			this.label2.Text = "場所：";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(8, 64);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(35, 12);
			this.label4.TabIndex = 3;
			this.label4.Text = "種類：";
			// 
			// nameLabel
			// 
			this.nameLabel.AutoSize = true;
			this.nameLabel.Location = new System.Drawing.Point(56, 8);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(35, 12);
			this.nameLabel.TabIndex = 4;
			this.nameLabel.Text = "label3";
			// 
			// dirPathLabel
			// 
			this.dirPathLabel.AutoSize = true;
			this.dirPathLabel.Location = new System.Drawing.Point(56, 32);
			this.dirPathLabel.Name = "dirPathLabel";
			this.dirPathLabel.Size = new System.Drawing.Size(35, 12);
			this.dirPathLabel.TabIndex = 5;
			this.dirPathLabel.Text = "label5";
			// 
			// kindLabel
			// 
			this.kindLabel.AutoSize = true;
			this.kindLabel.Location = new System.Drawing.Point(56, 64);
			this.kindLabel.Name = "kindLabel";
			this.kindLabel.Size = new System.Drawing.Size(35, 12);
			this.kindLabel.TabIndex = 6;
			this.kindLabel.Text = "label6";
			// 
			// closeButton
			// 
			this.closeButton.Location = new System.Drawing.Point(336, 80);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(75, 23);
			this.closeButton.TabIndex = 7;
			this.closeButton.Text = "閉じる";
			this.closeButton.UseVisualStyleBackColor = true;
			this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
			// 
			// FilePropertyForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(419, 110);
			this.Controls.Add(this.closeButton);
			this.Controls.Add(this.kindLabel);
			this.Controls.Add(this.dirPathLabel);
			this.Controls.Add(this.nameLabel);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FilePropertyForm";
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
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.Label dirPathLabel;
		private System.Windows.Forms.Label kindLabel;
		private System.Windows.Forms.Button closeButton;
	}
}