namespace kkde.help
{
	partial class HelpReferenceForm
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
			this.helpPanel = new kkde.help.HelpReferencePanel();
			this.SuspendLayout();
			// 
			// helpPanel
			// 
			this.helpPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.helpPanel.Location = new System.Drawing.Point(0, 0);
			this.helpPanel.Name = "helpPanel";
			this.helpPanel.Size = new System.Drawing.Size(605, 496);
			this.helpPanel.TabIndex = 0;
			// 
			// HelpReferenceForm
			// 
			this.ClientSize = new System.Drawing.Size(605, 496);
			this.Controls.Add(this.helpPanel);
			this.DockableAreas = ((WeifenLuo.WinFormsUI.DockAreas)(((((WeifenLuo.WinFormsUI.DockAreas.Float | WeifenLuo.WinFormsUI.DockAreas.DockLeft)
						| WeifenLuo.WinFormsUI.DockAreas.DockRight)
						| WeifenLuo.WinFormsUI.DockAreas.DockTop)
						| WeifenLuo.WinFormsUI.DockAreas.DockBottom)));
			this.HideOnClose = true;
			this.Name = "HelpReferenceForm";
			this.ShowHint = WeifenLuo.WinFormsUI.DockState.Float;
			this.TabText = "ヘルプリファレンス";
			this.Text = "ヘルプリファレンス";
			this.ResumeLayout(false);

		}

		#endregion

		private HelpReferencePanel helpPanel;




	}
}
