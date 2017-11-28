namespace kkde.kag.label
{
	partial class KagLabelForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KagLabelForm));
			this.labelTreeView = new System.Windows.Forms.TreeView();
			this.treePopMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.popMenuItemMove = new System.Windows.Forms.ToolStripMenuItem();
			this.treeImageList = new System.Windows.Forms.ImageList(this.components);
			this.treePopMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// labelTreeView
			// 
			this.labelTreeView.ContextMenuStrip = this.treePopMenu;
			this.labelTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelTreeView.HideSelection = false;
			this.labelTreeView.ImageIndex = 0;
			this.labelTreeView.ImageList = this.treeImageList;
			this.labelTreeView.Indent = 20;
			this.labelTreeView.ItemHeight = 18;
			this.labelTreeView.Location = new System.Drawing.Point(0, 0);
			this.labelTreeView.Name = "labelTreeView";
			this.labelTreeView.SelectedImageIndex = 0;
			this.labelTreeView.Size = new System.Drawing.Size(289, 427);
			this.labelTreeView.TabIndex = 2;
			this.labelTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.labelTreeView_BeforeExpand);
			this.labelTreeView.DoubleClick += new System.EventHandler(this.labelTreeView_DoubleClick);
			this.labelTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelTreeView_MouseDown);
			this.labelTreeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.labelTreeView_AfterExpand);
			// 
			// treePopMenu
			// 
			this.treePopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.popMenuItemMove});
			this.treePopMenu.Name = "treePopMenu";
			this.treePopMenu.Size = new System.Drawing.Size(121, 26);
			// 
			// popMenuItemMove
			// 
			this.popMenuItemMove.Name = "popMenuItemMove";
			this.popMenuItemMove.Size = new System.Drawing.Size(120, 22);
			this.popMenuItemMove.Text = "移動(&M)";
			this.popMenuItemMove.Click += new System.EventHandler(this.popMenuItemMove_Click);
			// 
			// treeImageList
			// 
			this.treeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeImageList.ImageStream")));
			this.treeImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.treeImageList.Images.SetKeyName(0, "file_kag.png");
			this.treeImageList.Images.SetKeyName(1, "Flag_greenHS.png");
			// 
			// KagLabelForm
			// 
			this.ClientSize = new System.Drawing.Size(289, 427);
			this.Controls.Add(this.labelTreeView);
			this.DockableAreas = ((WeifenLuo.WinFormsUI.DockAreas)(((((WeifenLuo.WinFormsUI.DockAreas.Float | WeifenLuo.WinFormsUI.DockAreas.DockLeft)
						| WeifenLuo.WinFormsUI.DockAreas.DockRight)
						| WeifenLuo.WinFormsUI.DockAreas.DockTop)
						| WeifenLuo.WinFormsUI.DockAreas.DockBottom)));
			this.HideOnClose = true;
			this.Name = "KagLabelForm";
			this.ShowHint = WeifenLuo.WinFormsUI.DockState.DockLeft;
			this.TabText = "ラベル";
			this.Text = "ラベル";
			this.treePopMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView labelTreeView;
		private System.Windows.Forms.ImageList treeImageList;
		private System.Windows.Forms.ContextMenuStrip treePopMenu;
		private System.Windows.Forms.ToolStripMenuItem popMenuItemMove;
	}
}
