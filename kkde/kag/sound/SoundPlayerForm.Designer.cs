namespace kkde.kag.sound
{
	partial class SoundPlayerForm
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
			this.soundToolBar = new System.Windows.Forms.ToolStrip();
			this.toolItemSoundPrev = new System.Windows.Forms.ToolStripButton();
			this.toolItemSoundNext = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemSoundPlay = new System.Windows.Forms.ToolStripButton();
			this.toolItemSoundStop = new System.Windows.Forms.ToolStripButton();
			this.toolItemSoundLoop = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolItemSoundCopyName = new System.Windows.Forms.ToolStripButton();
			this.fileInfoBox = new System.Windows.Forms.TextBox();
			this.soundPopMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuItemSoundPlay = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSoundStop = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.menuItemSoundCopyName = new System.Windows.Forms.ToolStripMenuItem();
			this.soundBgWorker = new System.ComponentModel.BackgroundWorker();
			this.soundToolBar.SuspendLayout();
			this.soundPopMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// soundToolBar
			// 
			this.soundToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolItemSoundPrev,
            this.toolItemSoundNext,
            this.toolStripSeparator2,
            this.toolItemSoundPlay,
            this.toolItemSoundStop,
            this.toolItemSoundLoop,
            this.toolStripSeparator1,
            this.toolItemSoundCopyName});
			this.soundToolBar.Location = new System.Drawing.Point(0, 0);
			this.soundToolBar.Name = "soundToolBar";
			this.soundToolBar.Size = new System.Drawing.Size(284, 25);
			this.soundToolBar.TabIndex = 0;
			this.soundToolBar.Text = "toolStrip1";
			// 
			// toolItemSoundPrev
			// 
			this.toolItemSoundPrev.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemSoundPrev.Image = global::kkde.Properties.Resources.NavBack;
			this.toolItemSoundPrev.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemSoundPrev.Name = "toolItemSoundPrev";
			this.toolItemSoundPrev.Size = new System.Drawing.Size(23, 22);
			this.toolItemSoundPrev.Text = "前の音楽を再生";
			this.toolItemSoundPrev.Click += new System.EventHandler(this.toolItemSoundPrev_Click);
			// 
			// toolItemSoundNext
			// 
			this.toolItemSoundNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemSoundNext.Image = global::kkde.Properties.Resources.NavForward;
			this.toolItemSoundNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemSoundNext.Name = "toolItemSoundNext";
			this.toolItemSoundNext.Size = new System.Drawing.Size(23, 22);
			this.toolItemSoundNext.Text = "次の音楽を再生";
			this.toolItemSoundNext.Click += new System.EventHandler(this.toolItemSoundNext_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// toolItemSoundPlay
			// 
			this.toolItemSoundPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemSoundPlay.Image = global::kkde.Properties.Resources.PlayHS;
			this.toolItemSoundPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemSoundPlay.Name = "toolItemSoundPlay";
			this.toolItemSoundPlay.Size = new System.Drawing.Size(23, 22);
			this.toolItemSoundPlay.Text = "再生開始";
			this.toolItemSoundPlay.Click += new System.EventHandler(this.menuItemSoundPlay_Click);
			// 
			// toolItemSoundStop
			// 
			this.toolItemSoundStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemSoundStop.Image = global::kkde.Properties.Resources.StopHS;
			this.toolItemSoundStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemSoundStop.Name = "toolItemSoundStop";
			this.toolItemSoundStop.Size = new System.Drawing.Size(23, 22);
			this.toolItemSoundStop.Text = "再生停止";
			this.toolItemSoundStop.Click += new System.EventHandler(this.menuItemSoundStop_Click);
			// 
			// toolItemSoundLoop
			// 
			this.toolItemSoundLoop.CheckOnClick = true;
			this.toolItemSoundLoop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemSoundLoop.Image = global::kkde.Properties.Resources.RepeatHS;
			this.toolItemSoundLoop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemSoundLoop.Name = "toolItemSoundLoop";
			this.toolItemSoundLoop.Size = new System.Drawing.Size(23, 22);
			this.toolItemSoundLoop.Text = "ループ再生";
			this.toolItemSoundLoop.Click += new System.EventHandler(this.toolItemSoundLoop_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolItemSoundCopyName
			// 
			this.toolItemSoundCopyName.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolItemSoundCopyName.Image = global::kkde.Properties.Resources.Icons_16x16_CopyIcon;
			this.toolItemSoundCopyName.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolItemSoundCopyName.Name = "toolItemSoundCopyName";
			this.toolItemSoundCopyName.Size = new System.Drawing.Size(23, 22);
			this.toolItemSoundCopyName.Text = "ファイル名をコピーする";
			this.toolItemSoundCopyName.Click += new System.EventHandler(this.menuItemSoundCopyName_Click);
			// 
			// fileInfoBox
			// 
			this.fileInfoBox.ContextMenuStrip = this.soundPopMenu;
			this.fileInfoBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fileInfoBox.Location = new System.Drawing.Point(0, 25);
			this.fileInfoBox.Multiline = true;
			this.fileInfoBox.Name = "fileInfoBox";
			this.fileInfoBox.ReadOnly = true;
			this.fileInfoBox.Size = new System.Drawing.Size(284, 148);
			this.fileInfoBox.TabIndex = 1;
			// 
			// soundPopMenu
			// 
			this.soundPopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSoundPlay,
            this.menuItemSoundStop,
            this.toolStripMenuItem1,
            this.menuItemSoundCopyName});
			this.soundPopMenu.Name = "contextMenuStrip1";
			this.soundPopMenu.Size = new System.Drawing.Size(227, 76);
			// 
			// menuItemSoundPlay
			// 
			this.menuItemSoundPlay.Image = global::kkde.Properties.Resources.PlayHS;
			this.menuItemSoundPlay.Name = "menuItemSoundPlay";
			this.menuItemSoundPlay.Size = new System.Drawing.Size(226, 22);
			this.menuItemSoundPlay.Text = "再生開始(&P)";
			this.menuItemSoundPlay.Click += new System.EventHandler(this.menuItemSoundPlay_Click);
			// 
			// menuItemSoundStop
			// 
			this.menuItemSoundStop.Image = global::kkde.Properties.Resources.StopHS;
			this.menuItemSoundStop.Name = "menuItemSoundStop";
			this.menuItemSoundStop.Size = new System.Drawing.Size(226, 22);
			this.menuItemSoundStop.Text = "再生停止(&S)";
			this.menuItemSoundStop.Click += new System.EventHandler(this.menuItemSoundStop_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(223, 6);
			// 
			// menuItemSoundCopyName
			// 
			this.menuItemSoundCopyName.Image = global::kkde.Properties.Resources.Icons_16x16_CopyIcon;
			this.menuItemSoundCopyName.Name = "menuItemSoundCopyName";
			this.menuItemSoundCopyName.Size = new System.Drawing.Size(226, 22);
			this.menuItemSoundCopyName.Text = "ファイル名をコピーする(&C)";
			this.menuItemSoundCopyName.Click += new System.EventHandler(this.menuItemSoundCopyName_Click);
			// 
			// soundBgWorker
			// 
			this.soundBgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.soundBgWorker_DoWork);
			this.soundBgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.soundBgWorker_RunWorkerCompleted);
			// 
			// SoundPlayerForm
			// 
			this.ClientSize = new System.Drawing.Size(284, 173);
			this.Controls.Add(this.fileInfoBox);
			this.Controls.Add(this.soundToolBar);
			this.DockableAreas = ((WeifenLuo.WinFormsUI.DockAreas)(((((WeifenLuo.WinFormsUI.DockAreas.Float | WeifenLuo.WinFormsUI.DockAreas.DockLeft)
						| WeifenLuo.WinFormsUI.DockAreas.DockRight)
						| WeifenLuo.WinFormsUI.DockAreas.DockTop)
						| WeifenLuo.WinFormsUI.DockAreas.DockBottom)));
			this.HideOnClose = true;
			this.Name = "SoundPlayerForm";
			this.ShowHint = WeifenLuo.WinFormsUI.DockState.DockLeft;
			this.TabText = "サウンド";
			this.soundToolBar.ResumeLayout(false);
			this.soundToolBar.PerformLayout();
			this.soundPopMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip soundToolBar;
		private System.Windows.Forms.ToolStripButton toolItemSoundPlay;
		private System.Windows.Forms.ToolStripButton toolItemSoundStop;
		private System.Windows.Forms.ToolStripButton toolItemSoundCopyName;
		private System.Windows.Forms.TextBox fileInfoBox;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ContextMenuStrip soundPopMenu;
		private System.Windows.Forms.ToolStripMenuItem menuItemSoundPlay;
		private System.Windows.Forms.ToolStripMenuItem menuItemSoundStop;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem menuItemSoundCopyName;
		private System.Windows.Forms.ToolStripButton toolItemSoundPrev;
		private System.Windows.Forms.ToolStripButton toolItemSoundNext;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.ComponentModel.BackgroundWorker soundBgWorker;
		private System.Windows.Forms.ToolStripButton toolItemSoundLoop;
	}
}
