using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using kkde.screen.control;
using kkde.global;
using System.IO;
using System.Xml;

namespace kkde.screen
{
	/// <summary>
	/// スクリーンエディタ上で表示するコントロールを表すクラス
	/// </summary>
	public class ScreenControl : IScreenControl
	{
		#region 定数
		private const int FOCUS_PEN_WIDTH = 3;
		#endregion

		#region フィールド
		private Panel m_parentPanel = null;
		private BaseType m_param = null;
		private bool m_isFocus = false;
		private Pen m_focusPen = null;
		private Point m_location = Point.Empty;
		private Size m_size = Size.Empty;
		private Bitmap m_image = null;
		#endregion

		#region プロパティ
		/// <summary>
		/// パラメーター
		/// </summary>
		public BaseType Param
		{
			get { return m_param; }
			set { m_param = value; }
		}

		public BaseType GetPropertyParam()
		{
			return this.Param;
		}

		/// <summary>
		/// フォーカスを持っているかどうか
		/// </summary>
		public bool IsFocus
		{
			get { return m_isFocus; }
			set 
			{
				if (m_isFocus != value)
				{
					m_isFocus = value;
					UpdateParam();
					m_parentPanel.Invalidate();
				}
			}
		}

		/// <summary>
		/// 位置
		/// </summary>
		public Point Location
		{
			get { return m_location; }
			set { m_location = value; }
		}

		/// <summary>
		/// サイズ
		/// </summary>
		public Size Size
		{
			get { return m_size; }
			set { m_size = value; }
		}

		/// <summary>
		/// 表示してる画像
		/// </summary>
		public Bitmap Image
		{
			get { return m_image; }
			set { m_image = value; }
		}
		#endregion

		#region メソッド
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ScreenControl(Panel parentPanel)
		{
			m_parentPanel = parentPanel;
			m_focusPen = new Pen(Brushes.Blue, FOCUS_PEN_WIDTH);
		}

		/// <summary>
		/// 位置を移動する
		/// </summary>
		/// <param name="pos">移動する場所（左上原点）</param>
		public void MoveControl(Point pos)
		{
			this.Location = pos;
			m_param.Location = pos;
			m_parentPanel.Invalidate();
		}

		/// <summary>
		/// パラメータを更新する
		/// </summary>
		public void UpdateParam()
		{
			GlobalStatus.FormManager.ScreenProperty.SetProperty(this);
		}

		/// <summary>
		/// 画像を設定する
		/// </summary>
		/// <param name="filePath"></param>
		public void SetImagePath(string filePath)
		{
			if (File.Exists(filePath))
			{
				m_image = new Bitmap(filePath);
			}
			else
			{
				m_image = global::kkde.Properties.Resources.ImgViewBG;
			}
			m_size = m_image.Size;
			m_param.ImagePath = filePath;
			m_parentPanel.Invalidate();
		}

		/// <summary>
		/// 描画する
		/// </summary>
		/// <param name="g"></param>
		public void OnDraw(Graphics g)
		{
			g.DrawImage(m_image, m_location);
			if (m_isFocus)
			{
				//フォーカスが当たっているときは枠をつける
				if (m_size.Width > FOCUS_PEN_WIDTH && m_size.Height > FOCUS_PEN_WIDTH)
				{
					g.DrawRectangle(m_focusPen, m_location.X, m_location.Y, m_size.Width, m_size.Height);
				}
			}
		}
		#endregion

		#region ファイルの読み込み・書き込み
		public void WriteXml(XmlTextWriter xw)
		{
		}

		public void ReadXml(XmlTextReader xr)
		{
		}
		#endregion
	}
}
