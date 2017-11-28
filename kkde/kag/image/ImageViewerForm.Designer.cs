namespace kkde.kag.image
{
	partial class ImageViewerForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageViewerForm));
			this.imageToolBar = new System.Windows.Forms.ToolStrip();
			this.toolItemShowPrev = new System.Windows.Forms.ToolStripButton();
			this.toolItemShowNext = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemSetSizeWindow = new System.Windows.Forms.ToolStripButton();
			this.toolItemSetSizeFullScale = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemSetSizeExpand = new System.Windows.Forms.ToolStripButton();
			this.toolItemSetSizeReduce = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemCopyFileName = new System.Windows.Forms.ToolStripButton();
			this.imagePanel = new System.Windows.Forms.Panel();
			this.imagePopMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuItemShowPrev = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemShowNext = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.menuItemSetSizeWindow = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSetSizeFullScale = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.menuItemSetSizeExpand = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSetSizeReduce = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.menuItemCopyFileName = new System.Windows.Forms.ToolStripMenuItem();
			this.imageViewBox = new System.Windows.Forms.PictureBox();
			this.imageToolBar.SuspendLayout();
			this.imagePanel.SuspendLayout();
			this.imagePopMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.imageViewBox)).BeginInit();
			this.SuspendLayout();
			// 
			// imageToolBar
			// 
			this.imageToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolItemShowPrev,
            this.toolItemShowNext,
            this.toolStripSeparator1,
            this.toolItemSetSizeWindow,
            this.toolItemSetSizeFullScale,
            this.toolStripSeparator2,
            this.toolItemSetSizeExpand,
            this.toolItemSetSizeReduce,
            this.toolStripSeparator3,
            this.toolItemCopyFileName});
			this.imageToolBar.Location = new System.Drawing.Point(0, 0);
			this.imageToolBar.Name = "imageToolBar";
			this.imageToolBar.Size = new System.Drawing.Size(266, 25);
			this.imageToolBar.TabIndex = 0;
			this.imageToolBar.Text = "toolStrip1";
			// 
			// toolItemShowPrev
			// 
			this.toolItemShowPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemShowPrev.Image = global::kkde.Properties.Resources.NavBack;
			this.toolItemShowPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemShowPrev.Name = "toolItemShowPrev";
			this.toolItemShowPrev.Size = new System.Drawing.Size(23, 22);
			this.toolItemShowPrev.Text = "前の画像を表示";
			this.toolItemShowPrev.Click += new System.EventHandler(this.menuItemShowPrev_Click);
			// 
			// toolItemShowNext
			// 
			this.toolItemShowNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemShowNext.Image = global::kkde.Properties.Resources.NavForward;
			this.toolItemShowNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemShowNext.Name = "toolItemShowNext";
			this.toolItemShowNext.Size = new System.Drawing.Size(23, 22);
			this.toolItemShowNext.Text = "次の画像を表示";
			this.toolItemShowNext.Click += new System.EventHandler(this.menuItemShowNext_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolItemSetSizeWindow
			// 
			this.toolItemSetSizeWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemSetSizeWindow.Image = global::kkde.Properties.Resources.imagewindow;
			this.toolItemSetSizeWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemSetSizeWindow.Name = "toolItemSetSizeWindow";
			this.toolItemSetSizeWindow.Size = new System.Drawing.Size(23, 22);
			this.toolItemSetSizeWindow.Text = "ウィンドウに合わせる";
			this.toolItemSetSizeWindow.Click += new System.EventHandler(this.menuItemSetSizeWindow_Click);
			// 
			// toolItemSetSizeFullScale
			// 
			this.toolItemSetSizeFullScale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemSetSizeFullScale.Image = global::kkde.Properties.Resources.imageauto;
			this.toolItemSetSizeFullScale.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemSetSizeFullScale.Name = "toolItemSetSizeFullScale";
			this.toolItemSetSizeFullScale.Size = new System.Drawing.Size(23, 22);
			this.toolItemSetSizeFullScale.Text = "実際のサイズ";
			this.toolItemSetSizeFullScale.Click += new System.EventHandler(this.menuItemSetSizeFullScale_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolItemSetSizeExpand
			// 
			this.toolItemSetSizeExpand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemSetSizeExpand.Image = global::kkde.Properties.Resources.magnify_b;
			this.toolItemSetSizeExpand.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemSetSizeExpand.Name = "toolItemSetSizeExpand";
			this.toolItemSetSizeExpand.Size = new System.Drawing.Size(23, 22);
			this.toolItemSetSizeExpand.Text = "拡大する";
			this.toolItemSetSizeExpand.Click += new System.EventHandler(this.menuItemSetSizeExpand_Click);
			// 
			// toolItemSetSizeReduce
			// 
			this.toolItemSetSizeReduce.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemSetSizeReduce.Image = global::kkde.Properties.Resources.reduce_b;
			this.toolItemSetSizeReduce.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemSetSizeReduce.Name = "toolItemSetSizeReduce";
			this.toolItemSetSizeReduce.Size = new System.Drawing.Size(23, 22);
			this.toolItemSetSizeReduce.Text = "縮小する";
			this.toolItemSetSizeReduce.Click += new System.EventHandler(this.menuItemSetSizeReduce_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// toolItemCopyFileName
			// 
			this.toolItemCopyFileName.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemCopyFileName.Image = ((System.Drawing.Image)(resources.GetObject("toolItemCopyFileName.Image")));
			this.toolItemCopyFileName.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemCopyFileName.Name = "toolItemCopyFileName";
			this.toolItemCopyFileName.Size = new System.Drawing.Size(23, 22);
			this.toolItemCopyFileName.Text = "ファイル名をコピーする";
			this.toolItemCopyFileName.Click += new System.EventHandler(this.menuItemCopyFileName_Click);
			// 
			// imagePanel
			// 
			this.imagePanel.AutoScroll = true;
			this.imagePanel.ContextMenuStrip = this.imagePopMenu;
			this.imagePanel.Controls.Add(this.imageViewBox);
			this.imagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.imagePanel.Location = new System.Drawing.Point(0, 25);
			this.imagePanel.Name = "imagePanel";
			this.imagePanel.Size = new System.Drawing.Size(266, 270);
			this.imagePanel.TabIndex = 1;
			this.imagePanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imagePanel_MouseMove);
			this.imagePanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imagePanel_MouseDown);
			this.imagePanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imagePanel_MouseUp);
			// 
			// imagePopMenu
			// 
			this.imagePopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemShowPrev,
            this.menuItemShowNext,
            this.toolStripMenuItem2,
            this.menuItemSetSizeWindow,
            this.menuItemSetSizeFullScale,
            this.toolStripMenuItem1,
            this.menuItemSetSizeExpand,
            this.menuItemSetSizeReduce,
            this.toolStripMenuItem3,
            this.menuItemCopyFileName});
			this.imagePopMenu.Name = "imagePopMenu";
			this.imagePopMenu.Size = new System.Drawing.Size(227, 176);
			// 
			// menuItemShowPrev
			// 
			this.menuItemShowPrev.Image = global::kkde.Properties.Resources.NavBack;
			this.menuItemShowPrev.Name = "menuItemShowPrev";
			this.menuItemShowPrev.Size = new System.Drawing.Size(226, 22);
			this.menuItemShowPrev.Text = "前の画像を表示(&P)";
			this.menuItemShowPrev.Click += new System.EventHandler(this.menuItemShowPrev_Click);
			// 
			// menuItemShowNext
			// 
			this.menuItemShowNext.Image = global::kkde.Properties.Resources.NavForward;
			this.menuItemShowNext.Name = "menuItemShowNext";
			this.menuItemShowNext.Size = new System.Drawing.Size(226, 22);
			this.menuItemShowNext.Text = "次の画像を表示(&N)";
			this.menuItemShowNext.Click += new System.EventHandler(this.menuItemShowNext_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(223, 6);
			// 
			// menuItemSetSizeWindow
			// 
			this.menuItemSetSizeWindow.Image = global::kkde.Properties.Resources.imagewindow;
			this.menuItemSetSizeWindow.Name = "menuItemSetSizeWindow";
			this.menuItemSetSizeWindow.Size = new System.Drawing.Size(226, 22);
			this.menuItemSetSizeWindow.Text = "ウィンドウに合わせる(&W)";
			this.menuItemSetSizeWindow.Click += new System.EventHandler(this.menuItemSetSizeWindow_Click);
			// 
			// menuItemSetSizeFullScale
			// 
			this.menuItemSetSizeFullScale.Image = global::kkde.Properties.Resources.imageauto;
			this.menuItemSetSizeFullScale.Name = "menuItemSetSizeFullScale";
			this.menuItemSetSizeFullScale.Size = new System.Drawing.Size(226, 22);
			this.menuItemSetSizeFullScale.Text = "実際のサイズ(&O)";
			this.menuItemSetSizeFullScale.Click += new System.EventHandler(this.menuItemSetSizeFullScale_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(223, 6);
			// 
			// menuItemSetSizeExpand
			// 
			this.menuItemSetSizeExpand.Image = global::kkde.Properties.Resources.magnify_b;
			this.menuItemSetSizeExpand.Name = "menuItemSetSizeExpand";
			this.menuItemSetSizeExpand.Size = new System.Drawing.Size(226, 22);
			this.menuItemSetSizeExpand.Text = "拡大する(&E)";
			this.menuItemSetSizeExpand.Click += new System.EventHandler(this.menuItemSetSizeExpand_Click);
			// 
			// menuItemSetSizeReduce
			// 
			this.menuItemSetSizeReduce.Image = global::kkde.Properties.Resources.reduce_b;
			this.menuItemSetSizeReduce.Name = "menuItemSetSizeReduce";
			this.menuItemSetSizeReduce.Size = new System.Drawing.Size(226, 22);
			this.menuItemSetSizeReduce.Text = "縮小する(&R)";
			this.menuItemSetSizeReduce.Click += new System.EventHandler(this.menuItemSetSizeReduce_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(223, 6);
			// 
			// menuItemCopyFileName
			// 
			this.menuItemCopyFileName.Image = global::kkde.Properties.Resources.Icons_16x16_CopyIcon;
			this.menuItemCopyFileName.Name = "menuItemCopyFileName";
			this.menuItemCopyFileName.Size = new System.Drawing.Size(226, 22);
			this.menuItemCopyFileName.Text = "ファイル名をコピーする(&C)";
			this.menuItemCopyFileName.Click += new System.EventHandler(this.menuItemCopyFileName_Click);
			// 
			// imageViewBox
			// 
			this.imageViewBox.BackgroundImage = global::kkde.Properties.Resources.ImgViewBG;
			this.imageViewBox.ContextMenuStrip = this.imagePopMenu;
			this.imageViewBox.Location = new System.Drawing.Point(0, 0);
			this.imageViewBox.Name = "imageViewBox";
			this.imageViewBox.Size = new System.Drawing.Size(288, 304);
			this.imageViewBox.TabIndex = 0;
			this.imageViewBox.TabStop = false;
			this.imageViewBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imagePanel_MouseMove);
			this.imageViewBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imagePanel_MouseDown);
			this.imageViewBox.Paint += new System.Windows.Forms.PaintEventHandler(this.imageViewBox_Paint);
			this.imageViewBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imagePanel_MouseUp);
			// 
			// ImageViewerForm
			// 
			this.ClientSize = new System.Drawing.Size(266, 295);
			this.Controls.Add(this.imagePanel);
			this.Controls.Add(this.imageToolBar);
			this.DockableAreas = ((WeifenLuo.WinFormsUI.DockAreas)(((((WeifenLuo.WinFormsUI.DockAreas.Float | WeifenLuo.WinFormsUI.DockAreas.DockLeft)
						| WeifenLuo.WinFormsUI.DockAreas.DockRight)
						| WeifenLuo.WinFormsUI.DockAreas.DockTop)
						| WeifenLuo.WinFormsUI.DockAreas.DockBottom)));
			this.HideOnClose = true;
			this.Name = "ImageViewerForm";
			this.ShowHint = WeifenLuo.WinFormsUI.DockState.DockLeft;
			this.TabText = "イメージ";
			this.imageToolBar.ResumeLayout(false);
			this.imageToolBar.PerformLayout();
			this.imagePanel.ResumeLayout(false);
			this.imagePopMenu.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.imageViewBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip imageToolBar;
		private System.Windows.Forms.ToolStripButton toolItemShowNext;
		private System.Windows.Forms.Panel imagePanel;
		private System.Windows.Forms.PictureBox imageViewBox;
		private System.Windows.Forms.ContextMenuStrip imagePopMenu;
		private System.Windows.Forms.ToolStripMenuItem menuItemSetSizeWindow;
		private System.Windows.Forms.ToolStripMenuItem menuItemSetSizeFullScale;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem menuItemSetSizeExpand;
		private System.Windows.Forms.ToolStripMenuItem menuItemSetSizeReduce;
		private System.Windows.Forms.ToolStripMenuItem menuItemShowNext;
		private System.Windows.Forms.ToolStripMenuItem menuItemShowPrev;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem menuItemCopyFileName;
		private System.Windows.Forms.ToolStripButton toolItemShowPrev;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton toolItemSetSizeWindow;
		private System.Windows.Forms.ToolStripButton toolItemSetSizeFullScale;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton toolItemSetSizeExpand;
		private System.Windows.Forms.ToolStripButton toolItemSetSizeReduce;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton toolItemCopyFileName;
	}
}
