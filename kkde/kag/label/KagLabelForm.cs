using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using kkde.global;
using kkde.parse;
using System.IO;
using System.Threading;

namespace kkde.kag.label
{
	/// <summary>
	/// KAGラベルツリーを表示するフォーム
	/// </summary>
	public partial class KagLabelForm : WeifenLuo.WinFormsUI.DockContent
	{
		#region 定数
		/// <summary>
		/// フォルダの閉じているときの画像インデックス
		/// </summary>
		private const int IMAGE_INDEX_FILE_KAG = 0;

		/// <summary>
		/// フォルダの開いているときの画像インデックス
		/// </summary>
		private const int IMAGE_INDEX_KAG_LABEL = 1;

		/// <summary>
		/// KAGラベルを記述しているファイルの拡張子
		/// </summary>
		private const string FILE_EXT = "*.ks";

		/// <summary>
		/// ラベルが無いときに表示する項目
		/// </summary>
		private const string NOT_LABEL_TEXT = "NO LABEL";
		#endregion

		#region 初期化
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public KagLabelForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// ツリーを初期化する
		/// </summary>
		public void InitTree()
		{
			string[] files = util.FileUtil.GetDirectoryFile(GlobalStatus.Project.DataFullPath
				, FILE_EXT, System.IO.SearchOption.AllDirectories);
			if (files == null || files.Length == 0)
			{
				return;	//構築する物がない
			}

			foreach (string file in files)
			{
				//ファイルごとのラベルリストを取得する
				InitTreeFileItem(file);
			}
		}

		/// <summary>
		/// ファイルツリーを初期化する
		/// </summary>
		/// <param name="filePath"></param>
		public void InitTreeFileItem(string filePath)
		{
			//ファイルごとのラベルリストを取得する
			KagLabelItem[] items = GlobalStatus.ParserSrv.GetLabelList(filePath);
			if (items == null || items.Length == 0)
			{
				return;
			}

			initTreeFileItem(filePath, items);
		}

		/// <summary>
		/// ファイルツリーを初期化する
		/// </summary>
		/// <param name="filePath">ファイルパス</param>
		/// <param name="itemList">追加するラベルリスト</param>
		private void initTreeFileItem(string filePath, KagLabelItem[] itemList)
		{
			TreeNode fileNode = getFileTreeNode(filePath);
			if (fileNode.IsExpanded == false)
			{
				return;
			}

			//選択アイテムを保存しておく
			KagLabelItem selectedItem = getSelectedLabelItem(filePath);
			List<TreeNode> treeNodeList = new List<TreeNode>();
			TreeNode selectNode = null;
			if (itemList != null)
			{
				foreach (KagLabelItem item in itemList)
				{
					TreeNode labelNode = new TreeNode(item.Label, IMAGE_INDEX_KAG_LABEL, IMAGE_INDEX_KAG_LABEL);
					labelNode.Tag = item;
					treeNodeList.Add(labelNode);

					if (selectedItem != null && selectedItem.LabelName == item.LabelName)
					{
						selectNode = labelNode;	//選択ノードを覚えておく
					}
				}
			}

			//ファイルノードを取得する
			if (haveLabelNode(fileNode) == true && fileNode.Nodes.Count == treeNodeList.Count)
			{
				//ノードの変化がないときは変更があったところだけセットする
				for (int i = 0; i < treeNodeList.Count; i++)
				{
					if (((KagLabelItem)fileNode.Nodes[i].Tag).LineNumber != ((KagLabelItem)treeNodeList[i].Tag).LineNumber)
					{
						fileNode.Nodes[i].Tag = treeNodeList[i].Tag;
					}
					if (((KagLabelItem)fileNode.Nodes[i].Tag).Label != ((KagLabelItem)treeNodeList[i].Tag).Label)
					{
						fileNode.Nodes[i].Text = treeNodeList[i].Text;
					}
				}
			}
			else
			{
				//ノード数が変化したときは位置から作り直す
				fileNode.Nodes.Clear();
				if (treeNodeList.Count == 0)
				{
					fileNode.Nodes.Add(new TreeNode(NOT_LABEL_TEXT, IMAGE_INDEX_KAG_LABEL, IMAGE_INDEX_KAG_LABEL));
				}
				else
				{
					fileNode.Nodes.AddRange(treeNodeList.ToArray());
				}
			}

			if (selectNode != null)
			{
				labelTreeView.SelectedNode = selectNode;
			}
		}

		/// <summary>
		/// 指定したノードがラベルノードを持っているかどうか
		/// ラベルノードを持っているときはtrueを返す
		/// </summary>
		/// <param name="node">ファイルノード</param>
		/// <returns></returns>
		private bool haveLabelNode(TreeNode node)
		{
			if (node == null)
			{
				return false;
			}
			if (node.Nodes.Count > 0)
			{
				if (node.Nodes[0].Tag is KagLabelItem)
				{
					return true;	//KAGラベルノード発見
				}
				else
				{
					return false;
				}
			}

			return false;
		}

		/// <summary>
		/// 指定したファイルパスのノードを取得する
		/// 存在しないときは新しく作成する
		/// </summary>
		/// <param name="filePath">ファイルパス</param>
		/// <returns>ファイルノード</returns>
		private TreeNode getFileTreeNode(string filePath)
		{
			foreach (TreeNode node in labelTreeView.Nodes)
			{
				if (filePath == (string)node.Tag)
				{
					//見つかったときはそのノードを返す
					return node;
				}
			}

			if (filePath == "")
			{
				return null;	//作成する物がない
			}
			//無いときは新しく作成する
			TreeNode newNode = new TreeNode(Path.GetFileName(filePath), IMAGE_INDEX_FILE_KAG, IMAGE_INDEX_FILE_KAG);
			newNode.Tag = filePath;
			newNode.Nodes.Add(new TreeNode(NOT_LABEL_TEXT, IMAGE_INDEX_KAG_LABEL, IMAGE_INDEX_KAG_LABEL));	//ダミーラベル挿入
			labelTreeView.Nodes.Add(newNode);
			return newNode;
		}

		/// <summary>
		/// 現在選択しているラベルのアイテムを返す
		/// 選択しているのがラベルではないときはnullを返す
		/// 指定したファイルのラベルではないときはnullを返す
		/// </summary>
		/// <param name="filePath">取得したいラベルの所属しているファイルパス</param>
		/// <returns>ラベルのアイテム</returns>
		private KagLabelItem getSelectedLabelItem(string filePath)
		{
			KagLabelItem selectedItem = getLabelItem(labelTreeView.SelectedNode);
			if (selectedItem != null)
			{
				if (selectedItem.FilePath == filePath)
				{
					//指定したファイルのラベルを選択していたときそのラベルを返す
					return selectedItem;
				}
			}

			//選択しているのはラベルではないまたは指定したファイルのラベルではないとき
			return null;
		}
		#endregion

		#region 全体イベント
		/// <summary>
		/// マウスボタンを押したとき
		/// </summary>
		private void labelTreeView_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Left)
			{
				labelTreeView.SelectedNode = labelTreeView.GetNodeAt(e.X, e.Y);
			}
		}

		/// <summary>
		/// ツリーアイテムをダブルクリックしたとき
		/// </summary>
		private void labelTreeView_DoubleClick(object sender, EventArgs e)
		{
			TreeNode selectedNode = labelTreeView.SelectedNode;
			if (selectedNode == null)
			{
				return;
			}
			if (selectedNode.Tag is string)
			{
				return;	//ファイルノードの時は何もしない
			}
			jumpLbel(labelTreeView.SelectedNode);
		}

		/// <summary>
		/// ポップアップメニュー→移動
		/// </summary>
		private void popMenuItemMove_Click(object sender, EventArgs e)
		{
			jumpLbel(labelTreeView.SelectedNode);
		}

		/// <summary>
		/// 指定したラベル位置へエディタを開き遷移する
		/// </summary>
		/// <param name="label"></param>
		private void jumpLbel(TreeNode node)
		{
			KagLabelItem label = getLabelItem(node);
			if (label == null)
			{
				return;	//取得できなかった
			}

			GlobalStatus.EditorManager.LoadFile(label.FilePath, label.LineNumber);
		}

		/// <summary>
		/// ノードからラベルオブジェクトを取得する
		/// ラベルノードではないときはnullを返す
		/// </summary>
		/// <param name="node">ラベルノード</param>
		/// <returns>ラベルオブジェクト</returns>
		private KagLabelItem getLabelItem(TreeNode node)
		{
			if (node == null)
			{
				return null;
			}

			if (node.Tag is KagLabelItem)	//ラベルノードの時
			{
				if (node.Tag != null)
				{
					return (KagLabelItem)node.Tag;
				}
			}
			else if (node.Tag is string)	//ファイル名ノードの時
			{
				if (File.Exists((string)node.Tag))
				{
					return new KagLabelItem("", (string)node.Tag, 0);
				}
			}

			return null;
		}

		/// <summary>
		/// ノードを展開しようとしているとき
		/// </summary>
		private void labelTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
		}

		/// <summary>
		/// ノードを展開したとき
		/// </summary>
		private void labelTreeView_AfterExpand(object sender, TreeViewEventArgs e)
		{
			TreeNode selectedNode = labelTreeView.SelectedNode;
			if (selectedNode == null)
			{
				return;
			}

			InitTreeFileItem((string)selectedNode.Tag);
		}
		#endregion
	}
}
