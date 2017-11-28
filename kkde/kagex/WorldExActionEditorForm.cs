using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using kkde.global;

namespace kkde.kagex
{
	/// <summary>
	/// ワールド拡張アクションエディタフォーム
	/// </summary>
	public partial class WorldExActionEditorForm : Form
	{
		#region フィールド
		/// <summary>
		/// アクション情報を保持する変数
		/// key=アクションプロパティ名
		/// value=アクションハンドラ
		/// </summary>
		Dictionary<string, IActionInfo> m_infoTable;
		#endregion

		#region 初期化・更新・終了
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public WorldExActionEditorForm()
		{
			InitializeComponent();

			m_infoTable = new Dictionary<string, IActionInfo>();
			initActionComboBox();
			initPreviewComboBox();
		}

		/// <summary>
		/// アクションハンドラコンボボックス初期化
		/// </summary>
		public void initActionComboBox()
		{
			//アクションハンドラをすべて登録する
			actionHandlerComboBox.Items.Clear();
			foreach (string name in ActionHandlerManager.HandlerNameList)
			{
				actionHandlerComboBox.Items.Add(name);
			}
		}

		/// <summary>
		/// プレビューオブジェクトコンボボックスを初期化する
		/// </summary>
		public void initPreviewComboBox()
		{
			setTargetPreviewComboBox(0);
		}

		/// <summary>
		/// 現在選択しているオブジェクトコンボボックスを変更する
		/// </summary>
		/// <param name="selectIndex">選択状態にするオブジェクトのインデックス</param>
		public void setTargetPreviewComboBox(int selectIndex)
		{
			toolItemPreviewTargetComboBox.Items.Clear();
			toolItemPreviewTargetComboBox.Items.Add(GlobalStatus.WorldExPreview.CharObject);
			toolItemPreviewTargetComboBox.Items.Add(GlobalStatus.WorldExPreview.EventObject);
			toolItemPreviewTargetComboBox.Items.Add(GlobalStatus.WorldExPreview.StageObject);
			toolItemPreviewTargetComboBox.SelectedIndex = selectIndex;
		}

		/// <summary>
		/// 終了しようとしたとき
		/// </summary>
		private void WorldExActionEditorForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			//ウィンドウはクローズせずに非表示にする
			this.Hide();
			e.Cancel = true;
		}
		#endregion

		#region ツールバーイベント
		/// <summary>
		/// アクションを生成しクリップボードにコピーする
		/// </summary>
		private void toolItemCopy_Click(object sender, EventArgs e)
		{
			string code = createActionCode();
			if (code == "")
			{
				return;	//取得できなかったので何もしない
			}

			Clipboard.SetText(code);
		}

		/// <summary>
		/// プレビュー
		/// </summary>
		private void toolItemPreview_Click(object sender, EventArgs e)
		{
			string code = createPreviewActionCode();
			if (code == "")
			{
				return;	//取得できず
			}

			mainToolBar.Focus();						//プロパティの値を確定させるために強制的にフォーカスを移動させる
			setGlobalActionName(actionNameBox.Text);	//アクション名をグローバルにセット
			GlobalStatus.WorldExPreview.Run(code);
		}

		/// <summary>
		/// アクション名を現在選択しているオブジェクトに対してセットする
		/// </summary>
		/// <param name="actionName"></param>
		private void setGlobalActionName(string actionName)
		{
			//現在選択しているオブジェクトにアクション名をセットする
			BaseWorldExAttrType attrType = (BaseWorldExAttrType)toolItemPreviewTargetComboBox.SelectedItem;
			attrType.Action = actionName;

			//グローバルにセットし直す
			if (attrType is CharWorldExAttrType)
			{
				GlobalStatus.WorldExPreview.CharObject = (CharWorldExAttrType)attrType;
			}
			else if (attrType is EventWorldExAttrType)
			{
				GlobalStatus.WorldExPreview.EventObject = (EventWorldExAttrType)attrType;
			}
			else if (attrType is StageWorldExAttrType)
			{
				GlobalStatus.WorldExPreview.StageObject = (StageWorldExAttrType)attrType;
			}
		}

		/// <summary>
		/// アクションコードを生成し返す
		/// </summary>
		/// <returns>アクションコード（envinit.tjsに登録する辞書形式）</returns>
		private string createActionCode()
		{
			//エラーチェック
			if (actionNameBox.Text == "")
			{
				util.Msgbox.Error("アクション名が入力されていません");
				return "";	//アクション名が無い
			}
			if (objectPropertyListView.Items.Count == 0)
			{
				util.Msgbox.Error("アクションプロパティがセットされていません");
				return "";	//オブジェクトが一つもセットされていない
			}

			//コード生成
			string code = "";
			code += String.Format("\"{0}\" => %[\n", actionNameBox.Text);		//アクション名
			foreach (ListViewItem item in objectPropertyListView.Items)
			{
				code += String.Format("\t\"{0}\" => {1},\n", item.Text, m_infoTable[item.Text].GetCode());	//アクションプロパティ
			}
			code += "],\n";

			return code;
		}

		/// <summary>
		/// プレビュー用のアクションコードを生成し返す
		/// </summary>
		/// <returns>アクションコード（プレビュー用）</returns>
		private string createPreviewActionCode()
		{
			//エラーチェック
			if (actionNameBox.Text == "")
			{
				util.Msgbox.Error("アクション名が入力されていません");
				return "";	//アクション名が無い
			}
			if (objectPropertyListView.Items.Count == 0)
			{
				util.Msgbox.Error("アクションプロパティがセットされていません");
				return "";	//オブジェクトが一つもセットされていない
			}

			//コード生成
			string code = "";
			code += "@iscript\n";
			code += String.Format("global.world_object.env.actions[\"{0}\"] = %[\n", actionNameBox.Text);		//アクション名
			foreach (ListViewItem item in objectPropertyListView.Items)
			{
				code += String.Format("\t\"{0}\" => {1},\n", item.Text, m_infoTable[item.Text].GetCode());	//アクションプロパティ
			}
			code += "];\n";
			code += "@endscript\n";

			return code;
		}

		/// <summary>
		/// 最前面で常に表示する
		/// </summary>
		private void toolItemTopMost_Click(object sender, EventArgs e)
		{
			if (this.TopMost)	//現在最前面の時
			{
				//最前面表示を解除する
				toolItemTopMost.Checked = false;
				this.TopMost = false;
			}
			else
			{
				//最前面表示にするを解除する
				toolItemTopMost.Checked = true;
				this.TopMost = true;
			}
		}

		/// <summary>
		/// 内容をすべてリセットする
		/// </summary>
		private void toolItemReset_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("現在の内容を全てリセットします\nよろしいですか？", "消去"
				, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
			{
				//何もしない
				return;
			}

			actionHandlerPropetyGrid.SelectedObject = null;
			objectPropertyListView.Items.Clear();
			initActionComboBox();

			//グローバルを全て初期化する
			GlobalStatus.WorldExPreview.CharObject.ResetAll();
			GlobalStatus.WorldExPreview.EventObject.ResetAll();
			GlobalStatus.WorldExPreview.StageObject.ResetAll();
			initPreviewComboBox();
		}

		/// <summary>
		/// プレビューするターゲットオブジェクトを選択する
		/// </summary>
		private void toolItemPreviewTargetRefButton_Click(object sender, EventArgs e)
		{
			BaseWorldExAttrType attrType = (BaseWorldExAttrType)toolItemPreviewTargetComboBox.SelectedItem;
			if (attrType is CharWorldExAttrType)
			{
				if (GlobalStatus.FormManager.WorldExCharSelectDialog.ShowDialog() == DialogResult.OK)
				{
					attrType.Name = GlobalStatus.FormManager.WorldExCharSelectDialog.SelectedText;
					setGlobalFromSelectedProperty();
					setTargetPreviewComboBox(toolItemPreviewTargetComboBox.SelectedIndex);
				}
			}
			else
			{
				if (attrType is EventWorldExAttrType)
				{
					GlobalStatus.FormManager.WorldExObjectSelectDialog.SetMode(WorldExObjectSelectDialog.Mode.Event);
				}
				else if (attrType is StageWorldExAttrType)
				{
					GlobalStatus.FormManager.WorldExObjectSelectDialog.SetMode(WorldExObjectSelectDialog.Mode.Stage);
				}
				else
				{
					return;	//何もできない
				}

				if (GlobalStatus.FormManager.WorldExObjectSelectDialog.ShowDialog() == DialogResult.OK)
				{
					attrType.Name = GlobalStatus.FormManager.WorldExObjectSelectDialog.SelectedText;
					setGlobalFromSelectedProperty();
					setTargetPreviewComboBox(toolItemPreviewTargetComboBox.SelectedIndex);
				}
			} 
		}

		/// <summary>
		/// 現在選択しているプロパティをグローバルにセットし直す
		/// </summary>
		private void setGlobalFromSelectedProperty()
		{
			BaseWorldExAttrType attrType = (BaseWorldExAttrType)toolItemPreviewTargetComboBox.SelectedItem;
			if (attrType is CharWorldExAttrType)
			{
				GlobalStatus.WorldExPreview.CharObject = (CharWorldExAttrType)attrType;
			}
			else if (attrType is EventWorldExAttrType)
			{
				GlobalStatus.WorldExPreview.EventObject = (EventWorldExAttrType)attrType;
			}
			else if (attrType is StageWorldExAttrType)
			{
				GlobalStatus.WorldExPreview.StageObject = (StageWorldExAttrType)attrType;
			}
		}
		#endregion

		#region アクションプロパティ関連
		/// <summary>
		/// アクションプロパティを追加する
		/// </summary>
		private void addObjectPropertyButton_Click(object sender, EventArgs e)
		{
			ActionPropertySelectDialog form = new ActionPropertySelectDialog();
			if (form.ShowDialog() == DialogResult.OK)
			{
				if (m_infoTable.ContainsKey(form.SelectedProperty.Name))
				{
					util.Msgbox.Info(form.SelectedProperty.Name + "プロパティはすでに登録されています");
					return;	//同じのが見つかったので追加しない
				}

				//追加する
				ListViewItem item = new ListViewItem(form.SelectedProperty.Name);
				objectPropertyListView.Items.Add(item);
				m_infoTable.Add(form.SelectedProperty.Name, form.SelectedProperty.Handler);	
			}
		}

		/// <summary>
		/// 現在選択しているアクションプロパティを削除する
		/// </summary>
		private void removeObjectPropertyButton_Click(object sender, EventArgs e)
		{
			if (objectPropertyListView.SelectedItems == null || objectPropertyListView.SelectedItems.Count == 0)
			{
				return;
			}

			objectPropertyListView.Items.Remove(objectPropertyListView.SelectedItems[0]);
		}

		/// <summary>
		/// アクションプロパティの選択アイテムが変更されたとき
		/// </summary>
		private void objectPropertyListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (objectPropertyListView.SelectedItems == null || objectPropertyListView.SelectedItems.Count == 0)
			{
				return;
			}

			//アクションプロパティの内容をハンドラにセットする
			Debug.WriteLine("アクションプロパティ選択項目変更: " + objectPropertyListView.SelectedItems[0].Text);
			IActionInfo actionInfo = m_infoTable[objectPropertyListView.SelectedItems[0].Text];
			actionHandlerPropetyGrid.SelectedObject = actionInfo;
			actionHandlerComboBox.SelectedItem = actionInfo.GetHandlerName();
		}
		#endregion

		#region ハンドラプロパティ関連
		/// <summary>
		/// アクションハンドラの選択アイテムが変更されたとき
		/// </summary>
		private void actionHandlerComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (objectPropertyListView.SelectedItems == null || objectPropertyListView.SelectedItems.Count == 0)
			{
				Debug.WriteLine("アクションプロパティが選択されていません");
				return;
			}

			string handlerText = actionHandlerComboBox.SelectedItem.ToString();
			string propertyText = objectPropertyListView.SelectedItems[0].Text;
			if (handlerText == m_infoTable[propertyText].GetHandlerName())
			{
				return;	//変更が無いので何もしない
			}
			if (m_infoTable[propertyText].GetHandlerName() != ActionHandlerManager.HANDLER_NAME_NULL)	//変更前がNULLではないとき
			{
				if (MessageBox.Show("ハンドラを変更すると現在のハンドラプロパティ内容は削除されます\nよろしいですか？", "質問", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
				{
					actionHandlerComboBox.SelectedItem = m_infoTable[propertyText].GetHandlerName();	//コンボボックスの内容を変更する
					return;
				}
			}
			
			//変更する
			m_infoTable[propertyText] = ActionHandlerManager.CreateActionInfoFromName(handlerText);
			actionHandlerPropetyGrid.SelectedObject = m_infoTable[propertyText];
			Debug.WriteLine("アクションハンドラ選択項目変更：property=" + propertyText + " handler=" + handlerText);
		}
		#endregion
	}
}
