using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using kkde.screen.control;
using System.Diagnostics;

namespace kkde.screen
{
	/// <summary>
	/// スクリーンエディタ上のコントロールプロパティを表すクラス
	/// </summary>
	public partial class ScreenPropertyForm : WeifenLuo.WinFormsUI.DockContent
	{
		#region フィールド
		private IScreenControl m_activeControl = null;
		#endregion

		#region プロパティ
		/// <summary>
		/// プロパティグリッドに表示するプロパティを設定する
		/// </summary>
		/// <param name="control"></param>
		public void SetProperty(IScreenControl control)
		{
			m_activeControl = control;
			typePropetyGrid.SelectedObject = m_activeControl.GetPropertyParam();
		}
		#endregion

		#region メソッド
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ScreenPropertyForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// プロパティ値が変更されたとき
		/// </summary>
		private void typePropetyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			if (m_activeControl == null)
			{
				//変更を反映する相手がないため何もしない
				return;
			}

			if (e.ChangedItem.Label == "Location")
			{
				m_activeControl.MoveControl((Point)e.ChangedItem.Value);
			}
			else if (e.ChangedItem.Label == "X" || e.ChangedItem.Label == "Y" )
			{
				if (e.ChangedItem.Parent != null && e.ChangedItem.Parent.Label == "Location")
				{
					m_activeControl.MoveControl((Point)e.ChangedItem.Parent.Value);
				}
			}
			else if (e.ChangedItem.Label == "ImagePath")
			{
				m_activeControl.SetImagePath((string)e.ChangedItem.Value);
			}
		}
		#endregion



	}
}
