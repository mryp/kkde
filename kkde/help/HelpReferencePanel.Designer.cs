namespace kkde.help
{
	partial class HelpReferencePanel
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

		#region コンポーネント デザイナで生成されたコード

		/// <summary> 
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.naviToolBar = new System.Windows.Forms.ToolStrip();
			this.toolItemBackPage = new System.Windows.Forms.ToolStripButton();
			this.toolItemFrontPage = new System.Windows.Forms.ToolStripButton();
			this.toolItemStop = new System.Windows.Forms.ToolStripButton();
			this.toolItemRefresh = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.urlComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.toolItemEnter = new System.Windows.Forms.ToolStripButton();
			this.helpBrowser = new System.Windows.Forms.WebBrowser();
			this.naviToolBar.SuspendLayout();
			this.SuspendLayout();
			// 
			// naviToolBar
			// 
			this.naviToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolItemBackPage,
            this.toolItemFrontPage,
            this.toolItemStop,
            this.toolItemRefresh,
            this.toolStripSeparator1,
            this.urlComboBox,
            this.toolItemEnter});
			this.naviToolBar.Location = new System.Drawing.Point(0, 0);
			this.naviToolBar.Name = "naviToolBar";
			this.naviToolBar.Size = new System.Drawing.Size(281, 26);
			this.naviToolBar.TabIndex = 1;
			this.naviToolBar.Text = "toolStrip1";
			this.naviToolBar.Resize += new System.EventHandler(this.naviToolBar_Resize);
			// 
			// toolItemBackPage
			// 
			this.toolItemBackPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemBackPage.Enabled = false;
			this.toolItemBackPage.Image = global::kkde.Properties.Resources.NavBack;
			this.toolItemBackPage.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemBackPage.Name = "toolItemBackPage";
			this.toolItemBackPage.Size = new System.Drawing.Size(23, 23);
			this.toolItemBackPage.Text = "戻る";
			this.toolItemBackPage.Click += new System.EventHandler(this.toolItemBackPage_Click);
			// 
			// toolItemFrontPage
			// 
			this.toolItemFrontPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemFrontPage.Enabled = false;
			this.toolItemFrontPage.Image = global::kkde.Properties.Resources.NavForward;
			this.toolItemFrontPage.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemFrontPage.Name = "toolItemFrontPage";
			this.toolItemFrontPage.Size = new System.Drawing.Size(23, 23);
			this.toolItemFrontPage.Text = "進む";
			this.toolItemFrontPage.Click += new System.EventHandler(this.toolItemFrontPage_Click);
			// 
			// toolItemStop
			// 
			this.toolItemStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemStop.Image = global::kkde.Properties.Resources.Icons_16x16_BrowserCancel;
			this.toolItemStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemStop.Name = "toolItemStop";
			this.toolItemStop.Size = new System.Drawing.Size(23, 23);
			this.toolItemStop.Text = "中止";
			this.toolItemStop.Click += new System.EventHandler(this.toolItemStop_Click);
			// 
			// toolItemRefresh
			// 
			this.toolItemRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemRefresh.Image = global::kkde.Properties.Resources.Icons_16x16_BrowserRefresh;
			this.toolItemRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemRefresh.Name = "toolItemRefresh";
			this.toolItemRefresh.Size = new System.Drawing.Size(23, 23);
			this.toolItemRefresh.Text = "更新";
			this.toolItemRefresh.Click += new System.EventHandler(this.toolItemRefresh_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
			// 
			// urlComboBox
			// 
			this.urlComboBox.Name = "urlComboBox";
			this.urlComboBox.Size = new System.Drawing.Size(121, 26);
			this.urlComboBox.SelectedIndexChanged += new System.EventHandler(this.urlComboBox_SelectedIndexChanged);
			this.urlComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.urlComboBox_KeyPress);
			// 
			// toolItemEnter
			// 
			this.toolItemEnter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemEnter.Image = global::kkde.Properties.Resources.Icons_16x16_ArrowDown;
			this.toolItemEnter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemEnter.Name = "toolItemEnter";
			this.toolItemEnter.Size = new System.Drawing.Size(23, 23);
			this.toolItemEnter.Text = "移動";
			this.toolItemEnter.Click += new System.EventHandler(this.toolItemEnter_Click);
			// 
			// helpBrowser
			// 
			this.helpBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.helpBrowser.Location = new System.Drawing.Point(0, 26);
			this.helpBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.helpBrowser.Name = "helpBrowser";
			this.helpBrowser.Size = new System.Drawing.Size(281, 133);
			this.helpBrowser.TabIndex = 2;
			this.helpBrowser.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.helpBrowser_Navigated);
			// 
			// HelpReferencePanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.helpBrowser);
			this.Controls.Add(this.naviToolBar);
			this.Name = "HelpReferencePanel";
			this.Size = new System.Drawing.Size(281, 159);
			this.naviToolBar.ResumeLayout(false);
			this.naviToolBar.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip naviToolBar;
		private System.Windows.Forms.ToolStripButton toolItemBackPage;
		private System.Windows.Forms.ToolStripButton toolItemFrontPage;
		private System.Windows.Forms.ToolStripButton toolItemStop;
		private System.Windows.Forms.ToolStripButton toolItemRefresh;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripComboBox urlComboBox;
		private System.Windows.Forms.ToolStripButton toolItemEnter;
		private System.Windows.Forms.WebBrowser helpBrowser;
	}
}
