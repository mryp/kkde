using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace kkde.kagex
{
	public partial class ActionPropertySelectDialog : Form
	{
		private ActionProperty m_selectedProperty = null;

		/// <summary>
		/// 選択しているプロパティを取得する
		/// </summary>
		public ActionProperty SelectedProperty
		{
			get { return m_selectedProperty; }
		}

		ListViewItem leftListItem = new ListViewItem(new string[] {
            "left",
            "左端（X方向座標）の位置"}, -1);
		ListViewItem topListItem = new ListViewItem(new string[] {
            "top",
            "上端（Y方向座標）の位置"}, -1);
		ListViewItem zoomListItem = new ListViewItem(new string[] {
            "zoom",
            "拡大率（％）"}, -1);
		ListViewItem zoomxListItem = new ListViewItem(new string[] {
            "zoomx",
            "X方向の拡大率（％）"}, -1);
		ListViewItem zoomyListItem = new ListViewItem(new string[] {
            "zoomy",
            "Y方向の拡大率（％）"}, -1);
		ListViewItem opacityListItem = new ListViewItem(new string[] {
            "opacity",
            "透明度（0 - 255）"}, -1);
		ListViewItem rotateListItem = new ListViewItem(new string[] {
            "rotate",
            "回転角度（度）"}, -1);
		ListViewItem afxListItem = new ListViewItem(new string[] {
            "afx",
            "回転・拡大の原点X座標"}, -1);
		ListViewItem afyListItem = new ListViewItem(new string[] {
            "afy",
            "回転・拡大の原点Y座標"}, -1);
		ListViewItem timeListItem = new ListViewItem(new string[] {
            "time",
            "アクション動作時間"}, -1);
		ListViewItem nowaitListItem = new ListViewItem(new string[] {
            "nowait",
            "アクション強制同期の回避"}, -1);
		ListViewItem tilexListItem = new ListViewItem(new string[] {
            "tilex",
            "タイル表示時のX方向座標の位置（イベント絵、環境レイヤのみ）"}, -1);
		ListViewItem tileyListItem = new ListViewItem(new string[] {
            "tiley",
            "タイル表示時のY方向座標の位置（イベント絵、環境レイヤのみ）"}, -1);

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ActionPropertySelectDialog()
		{
			InitializeComponent();

			initPropertyList();
		}

		/// <summary>
		/// リストの初期化
		/// </summary>
		private void initPropertyList()
		{
			propertyListView.Items.Add(leftListItem);
			propertyListView.Items.Add(topListItem);
			propertyListView.Items.Add(zoomListItem);
			propertyListView.Items.Add(zoomxListItem);
			propertyListView.Items.Add(zoomyListItem);
			propertyListView.Items.Add(opacityListItem);
			propertyListView.Items.Add(rotateListItem);
			propertyListView.Items.Add(afxListItem);
			propertyListView.Items.Add(afyListItem);
			propertyListView.Items.Add(timeListItem);
			propertyListView.Items.Add(nowaitListItem);
			propertyListView.Items.Add(tilexListItem);
			propertyListView.Items.Add(tileyListItem);
		}

		/// <summary>
		/// OKボタンを押したとき
		/// </summary>
		private void okButton_Click(object sender, EventArgs e)
		{
			if (propertyListView.SelectedItems == null)
			{
				return;	//選択していない
			}

			string propertyName = propertyListView.SelectedItems[0].Text;
			m_selectedProperty = new ActionProperty(propertyName);
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
		private void propertyListView_DoubleClick(object sender, EventArgs e)
		{
			//OKボタン動作を行う
			okButton_Click(sender, e);
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
