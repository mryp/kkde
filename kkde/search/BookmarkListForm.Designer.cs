namespace kkde.search
{
	partial class BookmarkListForm
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
			this.bookmarkListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// bookmarkListView
			// 
			this.bookmarkListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.bookmarkListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
			this.bookmarkListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bookmarkListView.FullRowSelect = true;
			this.bookmarkListView.LabelEdit = true;
			this.bookmarkListView.Location = new System.Drawing.Point(0, 0);
			this.bookmarkListView.Name = "bookmarkListView";
			this.bookmarkListView.Size = new System.Drawing.Size(458, 182);
			this.bookmarkListView.TabIndex = 0;
			this.bookmarkListView.UseCompatibleStateImageBehavior = false;
			this.bookmarkListView.View = System.Windows.Forms.View.Details;
			this.bookmarkListView.DoubleClick += new System.EventHandler(this.bookmarkListView_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "ブックマーク名";
			this.columnHeader1.Width = 134;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "行番号";
			this.columnHeader2.Width = 69;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "ファイル名";
			this.columnHeader3.Width = 297;
			// 
			// BookmarkListForm
			// 
			this.ClientSize = new System.Drawing.Size(458, 182);
			this.Controls.Add(this.bookmarkListView);
			this.DockableAreas = ((WeifenLuo.WinFormsUI.DockAreas)(((((WeifenLuo.WinFormsUI.DockAreas.Float | WeifenLuo.WinFormsUI.DockAreas.DockLeft)
						| WeifenLuo.WinFormsUI.DockAreas.DockRight)
						| WeifenLuo.WinFormsUI.DockAreas.DockTop)
						| WeifenLuo.WinFormsUI.DockAreas.DockBottom)));
			this.HideOnClose = true;
			this.Name = "BookmarkListForm";
			this.ShowHint = WeifenLuo.WinFormsUI.DockState.DockBottom;
			this.TabText = "ブックマーク";
			this.Text = "ブックマーク";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView bookmarkListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
	}
}
