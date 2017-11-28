using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using kkde.global;
using System.Diagnostics;

namespace kkde.kagex
{
	/// <summary>
	/// ワールド拡張ビューワーのタグ用属性エディタ
	/// </summary>
	public partial class WorldExPreviewAttr : Form
	{
		#region 初期化・更新
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public WorldExPreviewAttr()
		{
			InitializeComponent();

			changeControlSize();
			setTargetBox();
			attrPropertyGrid.SelectedObject = toolItemTargetSelectBox.SelectedItem;
		}

		/// <summary>
		/// 表示・非表示が切り替わったとき
		/// </summary>
		private void WorldExPreviewAttr_VisibleChanged(object sender, EventArgs e)
		{
			if (this.Visible)	//表示になったとき
			{
				UpdatePropertyGridConverter();
			}
		}

		/// <summary>
		/// 現在選択しているオブジェクトコンボボックスを変更する
		/// </summary>
		/// <param name="selectIndex">選択状態にするオブジェクトのインデックス</param>
		public void setTargetBox(int selectIndex)
		{
			toolItemTargetSelectBox.Items.Clear();
			toolItemTargetSelectBox.Items.Add(GlobalStatus.WorldExPreview.CharObject);
			toolItemTargetSelectBox.Items.Add(GlobalStatus.WorldExPreview.EventObject);
			toolItemTargetSelectBox.Items.Add(GlobalStatus.WorldExPreview.StageObject);
			toolItemTargetSelectBox.SelectedIndex = selectIndex;
		}

		/// <summary>
		/// 現在選択しているオブジェクトコンボボックスを変更する
		/// </summary>
		private void setTargetBox()
		{
			setTargetBox(0);
		}

		/// <summary>
		/// ツールバーのサイズを変更したとき
		/// </summary>
		private void targetToolBar_Resize(object sender, EventArgs e)
		{
			changeControlSize();
		}

		/// <summary>
		/// コントロールサイズ変更時の処理
		/// </summary>
		private void changeControlSize()
		{
			int width = targetToolBar.Width - toolItemTargetRefButton.Width - 16;
			toolItemTargetSelectBox.Size = new Size(width, 26);
		}

		/// <summary>
		/// 対象オブジェクトを選択したとき
		/// </summary>
		private void toolItemTargetSelectBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			attrPropertyGrid.SelectedObject = toolItemTargetSelectBox.SelectedItem;
		}

		/// <summary>
		/// 閉じようとしたとき
		/// </summary>
		private void WorldExPreviewAttr_FormClosing(object sender, FormClosingEventArgs e)
		{
			//ウィンドウはクローズせずに非表示にする
			this.Hide();
			e.Cancel = true;
		}

		/// <summary>
		/// 表示を更新する
		/// </summary>
		/// <param name="target">更新するオブジェクト</param>
		public void UpdateObject(KagexPreview.TargetObject target)
		{
			setTargetBox(0);
			int index = 0;
			for (int i = 0; i < toolItemTargetSelectBox.Items.Count; i++)
			{
				if (target == KagexPreview.TargetObject.Char && toolItemTargetSelectBox.Items[i] is CharWorldExAttrType)
				{
					index = i;
					break;
				}
				else if (target == KagexPreview.TargetObject.Event && toolItemTargetSelectBox.Items[i] is EventWorldExAttrType)
				{
					index = i;
					break;
				}
				else if (target == KagexPreview.TargetObject.Stage && toolItemTargetSelectBox.Items[i] is StageWorldExAttrType)
				{
					index = i;
					break;
				}
			}
			toolItemTargetSelectBox.SelectedIndex = index;
		}

		/// <summary>
		/// プロパティ情報のコンバーターを更新する
		/// </summary>
		public void UpdatePropertyGridConverter()
		{
			//リスト情報初期化
			kkde.parse.kagex.KagexCompletionUnit cu = GlobalStatus.ParserSrv.GetKagexEnvinitInfo();
			if (cu == null)
			{
				return;
			}

			PropertyGridWorldExActionConverter.SetList(cu.GetActionList());
			PropertyGridWorldExStimeConverter.SetList(cu.GetTimeList());
			PropertyGridWorldExTransConverter.SetList(cu.GetTransList());
			PropertyGridWorldExPosConverter.SetList(cu.GetPosList());
			PropertyGridWorldExLevelConverter.SetList(cu.GetLevelList());
		}
		#endregion

		#region ツールバーイベント
		/// <summary>
		/// 吉里吉里でプレビューする
		/// </summary>
		private void toolItemPreview_Click(object sender, EventArgs e)
		{
			mainToolBar.Focus();				//プロパティの値を確定させるために強制的にフォーカスを移動させる
			setGlobalFromSelectedProperty();	//プロパティ内容をグローバルにセットする
			GlobalStatus.WorldExPreview.Run();
		}

		/// <summary>
		/// 現在の内容をコピーする
		/// </summary>
		private void toolItemCopyTag_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(GlobalStatus.WorldExPreview.GetKagTagAttr((BaseWorldExAttrType)toolItemTargetSelectBox.SelectedItem));
		}

		/// <summary>
		/// 現在の属性をリセットする
		/// </summary>
		private void toolItemResetAttr_Click(object sender, EventArgs e)
		{
			BaseWorldExAttrType attrType = (BaseWorldExAttrType)attrPropertyGrid.SelectedObject;
			attrType.ResetAttr();
			attrPropertyGrid.Refresh();
		}

		/// <summary>
		/// 現在のオブジェクト情報をすべてリセットする
		/// </summary>
		private void toolItemResetAll_Click(object sender, EventArgs e)
		{
			GlobalStatus.KrkrProc.Kill();
			BaseWorldExAttrType attrType = (BaseWorldExAttrType)attrPropertyGrid.SelectedObject;
			attrType.ResetAll();
			setTargetBox(toolItemTargetSelectBox.SelectedIndex);
			attrPropertyGrid.Refresh();
		}

		/// <summary>
		/// オブジェクトを選択する
		/// </summary>
		private void toolItemTargetRefButton_Click(object sender, EventArgs e)
		{
			BaseWorldExAttrType attrType = (BaseWorldExAttrType)attrPropertyGrid.SelectedObject;
			if (attrType is CharWorldExAttrType)
			{
				if (GlobalStatus.FormManager.WorldExCharSelectDialog.ShowDialog() == DialogResult.OK)
				{
					attrType.Name = GlobalStatus.FormManager.WorldExCharSelectDialog.SelectedText;
					setGlobalFromSelectedProperty();
					setTargetBox(toolItemTargetSelectBox.SelectedIndex);
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
					setTargetBox(toolItemTargetSelectBox.SelectedIndex);
				}
			} 
		}

		/// <summary>
		/// 現在選択しているプロパティをグローバルにセットし直す
		/// </summary>
		private void setGlobalFromSelectedProperty()
		{
			BaseWorldExAttrType attrType = (BaseWorldExAttrType)attrPropertyGrid.SelectedObject;
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
		/// 最前面で表示する
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
		#endregion

		#region メニューイベント
		/// <summary>
		/// 立ち絵オブジェクトを選択する
		/// </summary>
		private void menuItemSelectChar_Click(object sender, EventArgs e)
		{
			UpdateObject(KagexPreview.TargetObject.Char);
			toolItemTargetRefButton_Click(sender, e);
		}

		/// <summary>
		/// イベント絵を選択する
		/// </summary>
		private void menuItemSelectEvent_Click(object sender, EventArgs e)
		{
			UpdateObject(KagexPreview.TargetObject.Event);
			toolItemTargetRefButton_Click(sender, e);
		}

		/// <summary>
		/// 背景絵を選択する
		/// </summary>
		private void menuItemSelectStage_Click(object sender, EventArgs e)
		{
			UpdateObject(KagexPreview.TargetObject.Stage);
			toolItemTargetRefButton_Click(sender, e);
		}
		#endregion

	}
}
