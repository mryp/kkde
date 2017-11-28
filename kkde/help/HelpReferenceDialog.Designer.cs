namespace kkde.help
{
	partial class HelpReferenceDialog
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
			this.helpPanel = new kkde.help.HelpReferencePanel();
			this.SuspendLayout();
			// 
			// helpPanel
			// 
			this.helpPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.helpPanel.Location = new System.Drawing.Point(0, 0);
			this.helpPanel.Name = "helpPanel";
			this.helpPanel.Size = new System.Drawing.Size(642, 643);
			this.helpPanel.TabIndex = 0;
			// 
			// HelpReferenceDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(642, 643);
			this.Controls.Add(this.helpPanel);
			this.Name = "HelpReferenceDialog";
			this.Text = "HelpReferenceDialog";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HelpReferenceDialog_FormClosing);
			this.ResumeLayout(false);

		}

		#endregion

		private HelpReferencePanel helpPanel;
	}
}