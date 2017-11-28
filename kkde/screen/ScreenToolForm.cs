using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using kkde.screen.control;

namespace kkde.screen
{
	/// <summary>
	/// スクリーンツールボックスを表示するフォーム
	/// </summary>
	public partial class ScreenToolForm : WeifenLuo.WinFormsUI.DockContent
	{
		#region フィールド
		private Point m_mousePoint = Point.Empty;
		#endregion

		#region 初期化・終了処理メソッド
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ScreenToolForm()
		{
			InitializeComponent();

			initTree();
		}

		/// <summary>
		/// ツリーの表示をすべてクリアーする
		/// </summary>
		private void initTree()
		{
			toolTreeView.Nodes.Clear();

			TreeNode generalNode = toolTreeView.Nodes.Add("全般");
			generalNode.Nodes.Add("画像コントロール").Tag = typeof(ImageType).ToString();
			generalNode.Nodes.Add("ジャンプボタン").Tag = typeof(BaseType).ToString();

			TreeNode saveloadNode = toolTreeView.Nodes.Add("セーブ・ロード");
			saveloadNode.Nodes.Add("セーブコントロール").Tag = typeof(BaseType).ToString();
			saveloadNode.Nodes.Add("ロードコントロール").Tag = typeof(BaseType).ToString();
			saveloadNode.Nodes.Add("ページボタン").Tag = typeof(BaseType).ToString();
			saveloadNode.Nodes.Add("セーブボタン").Tag = typeof(BaseType).ToString();
			saveloadNode.Nodes.Add("ロードボタン").Tag = typeof(BaseType).ToString();
		}
		#endregion

		#region ドラッグ・ドロップ関連
		/// <summary>
		/// マウスボタンを押したとき
		/// </summary>
		private void toolTreeView_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				//ボタンを押した位置のノードを選択状態にする
				m_mousePoint = Point.Empty;
				TreeNode dragNode = toolTreeView.GetNodeAt(e.X, e.Y);
				if (dragNode == null)
				{
					return;
				}
				toolTreeView.SelectedNode = dragNode;

				if (dragNode.Parent == null)
				{
					return;	//カテゴリツリーなので何もしない
				}

				m_mousePoint = new Point(e.X, e.Y);
			}
			else
			{
				m_mousePoint = Point.Empty;
			}
		}

		/// <summary>
		/// マウスボタンをあげたとき
		/// </summary>
		private void toolTreeView_MouseUp(object sender, MouseEventArgs e)
		{
			m_mousePoint = Point.Empty;
		}

		/// <summary>
		/// マウスを移動したとき
		/// </summary>
		private void toolTreeView_MouseMove(object sender, MouseEventArgs e)
		{
			if (m_mousePoint == Point.Empty)
			{
				//開始位置が設定されていない
				return;
			}

			Rectangle moveRect = new Rectangle(
				m_mousePoint.X - SystemInformation.DragSize.Width / 2,
				m_mousePoint.Y - SystemInformation.DragSize.Height / 2,
				SystemInformation.DragSize.Width,
				SystemInformation.DragSize.Height);
			if (moveRect.Contains(e.X, e.Y))
			{
				//移動量が少ないのでドラッグを開始しない
				return;
			}

			//ドラッグを開始する
			TreeNode dragNode = toolTreeView.GetNodeAt(m_mousePoint);
			String text = dragNode.Tag.ToString();
			toolTreeView.DoDragDrop(text, DragDropEffects.Copy);
			m_mousePoint = Point.Empty;	//ドラッグを開始したので位置をクリアする
			Debug.WriteLine("DragStart! text=" + text);
		}
		#endregion
	}
}
