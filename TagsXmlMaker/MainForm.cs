using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace TagsXmlMaker
{
	public partial class MainForm : Form
	{
		TreeNode m_preSelectedNode = null;
		KagTagsFile m_tagFile = new KagTagsFile();

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// フォーム読み込み時
		/// </summary>
		private void MainForm_Load(object sender, EventArgs e)
		{
			showDefaultPanel();
		}

		/// <summary>
		/// デフォルトの何もしないパネルを表示する
		/// </summary>
		private void showDefaultPanel()
		{
			defaultPanel.Visible = true;
			defaultPanel.Dock = DockStyle.Fill;
			
			tagPanel.Visible = false;
			attrPanel.Visible = false;
		}

		/// <summary>
		/// タグ入力パネルを表示する
		/// </summary>
		/// <param name="tag"></param>
		private void showTagPanel(KagTag tag)
		{
			tagPanel.Visible = true;
			tagPanel.Dock = DockStyle.Fill;
			if (tag != null)
			{
				tagNameBox.Text = tag.Name;
				tagGropuBox.Text = tag.Group;
				tagShortInfoBox.Text = tag.ShortInfo;
				tagLongInfoBox.Text = tag.Remarks;
			}

			defaultPanel.Visible = false;
			attrPanel.Visible = false;
		}

		/// <summary>
		/// 属性入力パネルを表示する
		/// </summary>
		/// <param name="attr"></param>
		private void showAttrPanel(KagTagAttr attr)
		{
			attrPanel.Visible = true;
			attrPanel.Dock = DockStyle.Fill;
			if (attr != null)
			{
				attrNameBox.Text = attr.Name;
				attrRequiredBox.Text = attr.Required;
				attrFormatBox.Text = attr.Format;
				attrShortInfoBox.Text = attr.ShortInfo;
				attrLongInfoBox.Text = attr.Info;
			}

			defaultPanel.Visible = false;
			tagPanel.Visible = false;
		}

		/// <summary>
		/// 現在選択しているノードの中身を表示する
		/// </summary>
		private void showSelectedNode()
		{
			if (tagTreeView.SelectedNode == null)
			{
				return;
			}

			m_preSelectedNode = tagTreeView.SelectedNode;
			if (tagTreeView.SelectedNode.Tag is KagTag)
			{
				showTagPanel((KagTag)tagTreeView.SelectedNode.Tag);
			}
			else if (tagTreeView.SelectedNode.Tag is KagTagAttr)
			{
				showAttrPanel((KagTagAttr)tagTreeView.SelectedNode.Tag);
			}
			else
			{
				showDefaultPanel();
			}
		}

		/// <summary>
		/// 指定したノードを選択状態にして表示する
		/// </summary>
		/// <param name="selectNode"></param>
		private void showNode(TreeNode selectNode)
		{
			saveNode(m_preSelectedNode);
			tagTreeView.SelectedNode = selectNode;
			showSelectedNode();
		}

		/// <summary>
		/// 現在の編集内容をノードに保存する
		/// </summary>
		/// <param name="node"></param>
		private void saveNode(TreeNode node)
		{
			if (node == null)
			{
				return;
			}

			if (tagPanel.Visible)
			{
				KagTag tag = new KagTag();
				tag.Name = tagNameBox.Text;
				tag.Group = tagGropuBox.Text;
				tag.ShortInfo = tagShortInfoBox.Text;
				tag.Remarks = tagLongInfoBox.Text;
				node.Tag = tag;
				node.Text = tag.Name;
			}
			else if (attrPanel.Visible)
			{
				KagTagAttr attr = new KagTagAttr();
				attr.Name = attrNameBox.Text;
				attr.Required = attrRequiredBox.Text;
				attr.Format = attrFormatBox.Text;
				attr.ShortInfo = attrShortInfoBox.Text;
				attr.Info = attrLongInfoBox.Text;
				node.Tag = attr;
				node.Text = attr.Name;
			}
			else
			{
				//何もしない
			}
		}

		/// <summary>
		/// 新規作成
		/// </summary>
		private void menuItemFileNew_Click(object sender, EventArgs e)
		{
			if (tagTreeView.Nodes.Count > 0)
			{
				if (MessageBox.Show("新規作成を行うと現在のノードを破棄します。\n本当に新規作成しますか？", "新規作成", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
				{
					return;	//何もしない
				}
			}

			tagTreeView.Nodes.Clear();
			tagTreeView.Nodes.Add("tdb");
		}

		/// <summary>
		/// ファイルオープン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemFileOpen_Click(object sender, EventArgs e)
		{
			tagTreeView.Nodes.Clear();

			openXmlFileDialog.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
			if (openXmlFileDialog.ShowDialog() == DialogResult.OK)
			{
				m_tagFile.LoadFromXmlFile(openXmlFileDialog.FileName);
				tagTreeView.Nodes.Add(m_tagFile.RootNode);
			}
		}

		/// <summary>
		/// 上書き保存
		/// </summary>
		private void menuItemFileSave_Click(object sender, EventArgs e)
		{
			if (tagTreeView.Nodes.Count == 0)
			{
				return;	//ノードがない
			}

			if (File.Exists(m_tagFile.FilePath))
			{
				//ファイルがあるとき
				m_tagFile.SaveFromTreeNode(tagTreeView.Nodes[0], m_tagFile.FilePath);
			}
			else
			{
				menuItemFileSaveAs_Click(sender, e);
			}
		}

		/// <summary>
		/// 名前を付けて保存
		/// </summary>
		private void menuItemFileSaveAs_Click(object sender, EventArgs e)
		{
			if (tagTreeView.Nodes.Count == 0)
			{
				return;	//ノードがない
			}

			saveXmlFileDialog.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
			if (saveXmlFileDialog.ShowDialog() == DialogResult.OK)
			{
				m_tagFile.SaveFromTreeNode(tagTreeView.Nodes[0], saveXmlFileDialog.FileName);
			}
		}

		/// <summary>
		/// 閉じる
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemFileClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// ツリーの内容をクリックしたとき
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tagTreeView_Click(object sender, EventArgs e)
		{
			saveNode(m_preSelectedNode);
			showSelectedNode();
		}

		/// <summary>
		/// ツリーのアイテムに対してマウスボタンを押したとき
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tagTreeView_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Left)
			{
				tagTreeView.SelectedNode = tagTreeView.GetNodeAt(e.X, e.Y);
			}
		}

		/// <summary>
		/// 新規にタグ追加
		/// </summary>
		private void menuItemPopAddTag_Click(object sender, EventArgs e)
		{
			if (tagTreeView.Nodes.Count == 0)
			{
				return;	//まだ何も開いていないときは何もしない
			}

			string name = "NewTag";
			TreeNode node = tagTreeView.Nodes[0].Nodes.Add(name);	//tdbノードの下に追加する
			KagTag tag = new KagTag();
			tag.Name = name;
			node.Tag = tag;

			showNode(node);
		}

		/// <summary>
		/// 現在のタグに属性追加
		/// </summary>
		private void menuItemPopAddAttr_Click(object sender, EventArgs e)
		{
			string name = "NewAttr";
			TreeNode node = tagTreeView.SelectedNode.Nodes.Add(name);
			KagTagAttr attr = new KagTagAttr();
			attr.Name = name;
			node.Tag = attr;

			showNode(node);
		}

		/// <summary>
		/// 現在選択しているノードを削除
		/// </summary>
		private void menuItemPopDelete_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("本当に削除しますか？", "削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				tagTreeView.SelectedNode.Remove();
			}
		}

		/// <summary>
		/// ポップアップメニューを開こうとしたとき
		/// </summary>
		private void popMenu_Opening(object sender, CancelEventArgs e)
		{
			if (tagPanel.Visible)
			{
				menuItemPopAddTag.Enabled = true;
				menuItemPopAddAttr.Enabled = true;
				menuItemPopDelete.Enabled = true;
			}
			else if (attrPanel.Visible)
			{
				menuItemPopAddTag.Enabled = false;
				menuItemPopAddAttr.Enabled = false;
				menuItemPopDelete.Enabled = true;
			}
			else
			{
				menuItemPopAddTag.Enabled = true;
				menuItemPopAddAttr.Enabled = false;
				menuItemPopDelete.Enabled = false;
			}
		}

		private void tagTreeView_KeyDown(object sender, KeyEventArgs e)
		{

		}

		private void tagTreeView_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Return)
			{
				if (tagTreeView.SelectedNode != null)
				{
					showNode(tagTreeView.SelectedNode);
				}
			}
		}

		private void menuItemDebugOutputList_Click(object sender, EventArgs e)
		{
			if (tagTreeView.Nodes.Count == 0)
			{
				Debug.WriteLine("アイテム=0");
				return;
			}

			Debug.WriteLine("------------------------------------------------------------");
			foreach (TreeNode tagNode in tagTreeView.Nodes[0].Nodes)
			{
				KagTag tag = tagNode.Tag as KagTag;
				if (tag == null)
				{
					continue;
				}

				foreach (TreeNode attrNode in tagNode.Nodes)
				{
					KagTagAttr attr = attrNode.Tag as KagTagAttr;
					if (attr == null)
					{
						continue;
					}

					Debug.WriteLine(String.Format("{0, -20}{1, -15}{2}", tag.Name, attr.Name, attr.Format));
				}
			}
		}
	}
}
