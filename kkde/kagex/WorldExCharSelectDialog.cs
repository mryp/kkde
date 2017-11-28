using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace kkde.kagex
{
	public partial class WorldExCharSelectDialog : Form
	{
		WorldExCharTreeNode m_charTreeNode = null;
		string m_selectedText = "";

		/// <summary>
		/// 選択した文字列
		/// </summary>
		public string SelectedText
		{
			get { return m_selectedText; }
			set { m_selectedText = value; }
		}
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public WorldExCharSelectDialog()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 表示されるとき
		/// </summary>
		private void WorldExCharSelectDialog_Shown(object sender, EventArgs e)
		{
			UpdateNode();
			if (m_selectedText != "")
			{
				selectNodeFromText(m_selectedText);
			}
			charTreeView.Focus();
		}

		/// <summary>
		/// ノードを最新に更新する
		/// </summary>
		public void UpdateNode()
		{
			charTreeView.Nodes.Clear();
			m_charTreeNode = new WorldExCharTreeNode();
			if (m_charTreeNode.RootNode == null)
			{
				return;	//ノードが取得できない
			}

			//ツリービューに追加
			foreach (TreeNode node in m_charTreeNode.RootNode.Nodes)
			{
				charTreeView.Nodes.Add(node);
			}
		}

		/// <summary>
		/// 文字列からノードを選択する
		/// </summary>
		/// <param name="m_selectedText"></param>
		private void selectNodeFromText(string m_selectedText)
		{
			string[] charParts = m_selectedText.Split(' ');
			if (charParts == null)
			{
				return;	//何かエラーが出たので何もしない
			}

			TreeNode selectNode = null;
			for (int i=0; i<charParts.Length; i++)
			{
				switch (i)
				{
					case 0: //名前
						selectNode = searchNode(charTreeView.Nodes, charParts[i]);
						break;
					case 1:	//姿勢
						selectNode = searchNode(selectNode.Nodes, charParts[i]);
						break;
					case 2: //服装
						selectNode = searchNode(selectNode.Nodes, charParts[i]);
						break;
					case 3: //表情
						selectNode = searchNode(selectNode.Nodes, charParts[i]);
						break;
				}

				if (selectNode == null)
				{
					break;	//見つからなかったら終了
				}
			}

			charTreeView.SelectedNode = selectNode;
		}

		/// <summary>
		/// 指定したノードリストから文字列にヒットするノードを検索し返す
		/// </summary>
		/// <param name="nodes">検索するノードリスト</param>
		/// <param name="searchText">検索文字列</param>
		/// <returns>見つかったノード（見つからなかったときはnull）</returns>
		private TreeNode searchNode(TreeNodeCollection nodes, string searchText)
		{
			foreach (TreeNode node in nodes)
			{
				if (searchText == node.Text)
				{
					return node;
				}
			}

			return null;	//見つからなかった
		}

		/// <summary>
		/// 選択内容をセットする
		/// </summary>
		private void okButton_Click(object sender, EventArgs e)
		{
			if (charTreeView.SelectedNode == null)
			{
				m_selectedText = "";
				return;
			}

			m_selectedText = m_charTreeNode.GetCharText(charTreeView.SelectedNode);
		}

		/// <summary>
		/// キャンセルしたとき
		/// </summary>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			//何もしない
		}

		/// <summary>
		/// ダブルクリックしたとき
		/// </summary>
		private void charTreeView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				charTreeView.SelectedNode = charTreeView.GetNodeAt(e.X, e.Y);
			}
			TreeNode selectNode = charTreeView.SelectedNode;
			if (selectNode == null)	//選択されていないとき
			{
				return;
			}
			if (selectNode.Nodes.Count > 0)	//子ノードが一つ以上存在するとき
			{
				return;	//子ノードが存在するときはコピーしない（ツリーを開く動作と紛らわしいため最後のノードのみ選択可とする）
			}

			//OKボタン動作を行う
			okButton_Click(sender, e);
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

	}
}
