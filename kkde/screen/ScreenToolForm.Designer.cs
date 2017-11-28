namespace kkde.screen
{
	partial class ScreenToolForm
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
			this.toolTreeView = new System.Windows.Forms.TreeView();
			this.SuspendLayout();
			// 
			// toolTreeView
			// 
			this.toolTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolTreeView.Indent = 20;
			this.toolTreeView.ItemHeight = 18;
			this.toolTreeView.Location = new System.Drawing.Point(0, 0);
			this.toolTreeView.Name = "toolTreeView";
			this.toolTreeView.Size = new System.Drawing.Size(377, 516);
			this.toolTreeView.TabIndex = 0;
			this.toolTreeView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.toolTreeView_MouseUp);
			this.toolTreeView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.toolTreeView_MouseMove);
			this.toolTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolTreeView_MouseDown);
			// 
			// ScreenToolForm
			// 
			this.ClientSize = new System.Drawing.Size(377, 516);
			this.Controls.Add(this.toolTreeView);
			this.DockableAreas = ((WeifenLuo.WinFormsUI.DockAreas)(((((WeifenLuo.WinFormsUI.DockAreas.Float | WeifenLuo.WinFormsUI.DockAreas.DockLeft)
						| WeifenLuo.WinFormsUI.DockAreas.DockRight)
						| WeifenLuo.WinFormsUI.DockAreas.DockTop)
						| WeifenLuo.WinFormsUI.DockAreas.DockBottom)));
			this.HideOnClose = true;
			this.Name = "ScreenToolForm";
			this.TabText = "スクリーンツールボックス";
			this.Text = "スクリーンツールボックス";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView toolTreeView;

	}
}
