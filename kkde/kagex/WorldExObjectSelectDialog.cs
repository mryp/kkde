using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using kkde.global;

namespace kkde.kagex
{
	public partial class WorldExObjectSelectDialog : Form
	{
		public enum Mode
		{
			/// <summary>
			/// イベント絵リストモード
			/// </summary>
			Event,
			/// <summary>
			/// 背景絵リストモード
			/// </summary>
			Stage,
		}

		/// <summary>
		/// KAGEX解析結果を保持する変数
		/// </summary>
		kkde.parse.kagex.KagexCompletionUnit m_cu = null;
		string m_selectedText = "";
		Mode m_mode = Mode.Event;

		/// <summary>
		/// 選択した文字列
		/// </summary>
		public string SelectedText
		{
			get { return m_selectedText; }
			set { m_selectedText = value; }
		}

		/// <summary>
		/// 表示するモードをセットする
		/// </summary>
		/// <param name="m"></param>
		public void SetMode(Mode m)
		{
			m_mode = m;
			if (m_mode == Mode.Event)
			{
				this.Text = "イベント絵オブジェクト選択";
			}
			else if (m_mode == Mode.Stage)
			{
				this.Text = "背景絵オブジェクト選択";
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public WorldExObjectSelectDialog()
		{
			InitializeComponent();
		}

		/// <summary>
		/// ダイアログを表示したとき
		/// </summary>
		private void WorldExObjectSelectDialog_Shown(object sender, EventArgs e)
		{
			m_cu = GlobalStatus.ParserSrv.GetKagexEnvinitInfo();
			if (m_cu == null)
			{
				return;	//初期化できるデータがない
			}

			if (m_mode == Mode.Event)
			{
				initListViewItem(m_cu.GetEventList());
			}
			else if (m_mode == Mode.Stage)
			{
				initListViewItem(m_cu.GetStageList());
			}
			
			if (m_selectedText != "")
			{
				foreach (ListViewItem item in objectListView.Items)
				{
					if (item.Text == m_selectedText)
					{
						item.Selected = true;						//選択状態にする
						objectListView.EnsureVisible(item.Index);	//見える位置までスクロールする
						break;
					}
				}
			}
			objectListView.Focus();
		}

		/// <summary>
		/// リストビューを指定されたリストで初期化する
		/// </summary>
		/// <param name="listView">初期化するビュー</param>
		/// <param name="initList">初期化するリスト</param>
		private void initListViewItem(List<string> initList)
		{
			if (initList == null)
			{
				return;
			}

			objectListView.Items.Clear();
			foreach (string text in initList)
			{
				objectListView.Items.Add(text);
			}
		}

		/// <summary>
		/// OKボタンを押したとき
		/// </summary>
		private void okButton_Click(object sender, EventArgs e)
		{
			if (objectListView.SelectedItems == null)
			{
				m_selectedText = "";
				return;
			}

			m_selectedText = objectListView.SelectedItems[0].Text;
		}

		/// <summary>
		/// キャンセルボタンを押したとき
		/// </summary>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			//何もしない
		}

		/// <summary>
		/// アイテムをダブルクリックしたとき
		/// </summary>
		private void objectListView_DoubleClick(object sender, EventArgs e)
		{
			//OKボタン動作を行う
			okButton_Click(sender, e);
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
