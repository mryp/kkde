using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using kkde.screen.control;
using kkde.global;
using System.IO;
using System.Xml;

namespace kkde.screen
{
	public class ScreenPanel : Panel, IScreenControl
	{
		#region フィールド
		private List<ScreenControl> m_controlList = new List<ScreenControl>();
		private ScreenControl m_activeControl = null;
		private Point m_moveControlPoint = Point.Empty;
		private Point m_bgImageLocation = Point.Empty;
		private Bitmap m_bgImage = null;
		private BackgroundType m_param;
		#endregion

		#region プロパティ
		/// <summary>
		/// パラメーター
		/// </summary>
		public BackgroundType Param
		{
			get { return m_param; }
			set { m_param = value; }
		}
		#endregion

		#region 初期化・終了処理メソッド
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ScreenPanel()
		{
			InitializeComponent();

			//ダブルバッファリング有効化（ちらつき防止）
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

			m_param = new BackgroundType();
			m_controlList = new List<ScreenControl>();
		}

		/// <summary>
		/// コントロール初期化
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// ScreenPanel
			// 
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ScreenPanel_Paint);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ScreenPanel_MouseMove);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ScreenPanel_MouseDown);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ScreenPanel_MouseUp);
			this.ResumeLayout(false);
		}

		#endregion

		#region コントロール処理メソッド
		/// <summary>
		/// コントロール一覧を取得する
		/// </summary>
		/// <returns></returns>
		public List<ScreenControl> GetControlList()
		{
			return m_controlList;
		}

		/// <summary>
		/// コントロールを追加する
		/// </summary>
		/// <param name="control"></param>
		public void AddControl(ScreenControl control)
		{
			m_controlList.Add(control);
			setForcusToControl(control);
			this.Invalidate();
		}

		/// <summary>
		/// コントロールを削除する
		/// </summary>
		/// <param name="control"></param>
		public void RemoveControl(ScreenControl control)
		{
			m_controlList.Remove(control);
		}
		#endregion

		#region ファイル保存・読み込み関連
		public void WriteXml(XmlTextWriter xw)
		{
			//背景を描き出す
			writeXmlBackground(xw);

			for (int i = 0; i < m_controlList.Count; i++)	//先頭（一番後ろにあるモノ）から順番に
			{
				ScreenControl sc = m_controlList[i];
				sc.WriteXml(xw);
			}
		}

		private void writeXmlBackground(XmlTextWriter xw)
		{
			xw.WriteStartElement("screenpanel");
			writeElement(xw, "name", this.Name);
			xw.WriteEndElement();
		}
		
		private static void writeElement(XmlTextWriter xw, string elementName, object value)
		{
			xw.WriteStartElement(elementName);
			xw.WriteValue(value);
			xw.WriteEndElement();
		}

		public void ReadXml(XmlTextReader xr)
		{
			readXmlBackground(xr);
		}

		private void readXmlBackground(XmlTextReader xr)
		{

		}
		#endregion

		#region マウスイベント関連メソッド
		/// <summary>
		/// マウスボタンを押したとき
		/// </summary>
		private void ScreenPanel_MouseDown(object sender, MouseEventArgs e)
		{
			Debug.WriteLine("ScreenPanel_MouseDown");
			if (e.Button == MouseButtons.Left)
			{
				//ボタンを押した位置のノードを選択状態にする
				ScreenControl sc = getControlFormPos(e.Location);
				if (sc == null)
				{
					m_moveControlPoint = Point.Empty;
				}
				else
				{
					m_moveControlPoint = new Point(e.Location.X - sc.Location.X, e.Location.Y - sc.Location.Y);
				}
				setForcusToControl(sc);
			}
			else
			{
				m_moveControlPoint = Point.Empty;
			}
		}

		/// <summary>
		/// マウスカーソルを移動したとき
		/// </summary>
		private void ScreenPanel_MouseMove(object sender, MouseEventArgs e)
		{
			if (m_moveControlPoint == Point.Empty)
			{
				//開始位置が設定されていない
				return;
			}
			if (m_activeControl == null)
			{
				return;
			}

			setControlPosFromCursor();
		}

		/// <summary>
		/// マウスボタンをあげたとき
		/// </summary>
		private void ScreenPanel_MouseUp(object sender, MouseEventArgs e)
		{
			if (m_moveControlPoint != Point.Empty)
			{
				m_moveControlPoint = Point.Empty;
				m_activeControl.UpdateParam();
			}
		}

		/// <summary>
		/// 指定した位置にあるコントロールを取得する
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		private ScreenControl getControlFormPos(Point p)
		{
			//一番上（最後に追加したモノ）が先に見つかるように後ろから検索を行う
			for (int i = m_controlList.Count - 1; i >= 0; i--)
			{
				ScreenControl sc = m_controlList[i];
				Rectangle rec = new Rectangle(sc.Location, sc.Size);
				if (rec.Contains(p))
				{
					return sc;
				}
			}

			return null;
		}

		/// <summary>
		/// コントロールにフォーカスを設定する
		/// </summary>
		/// <param name="control">フォーカスを設定するコントロール</param>
		private void setForcusToControl(ScreenControl control)
		{
			foreach (ScreenControl sc in m_controlList)
			{
				//全コントロールをフォーカス無しにする
				sc.IsFocus = false;
			}
			if (control != null)
			{
				Debug.WriteLine("set focus!");
				m_activeControl = (ScreenControl)control;
				m_activeControl.IsFocus = true;
			}
			else
			{
				m_activeControl = null;
				GlobalStatus.FormManager.ScreenProperty.SetProperty(this);
			}
		}

		/// <summary>
		/// コントロールの位置をカーソルから決定する
		/// </summary>
		private void setControlPosFromCursor()
		{
			//コントロールをマウスの移動に追随させる
			Point panelPoint = this.PointToClient(Cursor.Position);
			Debug.WriteLine("panelX=" + panelPoint.X.ToString() + " panelY=" + panelPoint.Y.ToString());
			Debug.WriteLine("controlX=" + m_moveControlPoint.X.ToString() + " controlY=" + m_moveControlPoint.Y.ToString());
			m_activeControl.MoveControl(new Point(panelPoint.X - m_moveControlPoint.X
				, panelPoint.Y - m_moveControlPoint.Y));

			this.Invalidate();
		}
		#endregion

		#region 画面描画関連メソッド
		/// <summary>
		/// 画面更新
		/// </summary>
		private void ScreenPanel_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			if (m_bgImage != null)
			{
				g.DrawImage(m_bgImage, m_bgImageLocation);
			}

			foreach (ScreenControl sc in m_controlList)
			{
				sc.OnDraw(g);
			}
		}
		#endregion

		#region IScreenControl メンバ
		BaseType IScreenControl.GetPropertyParam()
		{
			return m_param;
		}

		void IScreenControl.SetImagePath(string filePath)
		{
			if (File.Exists(filePath))
			{
				m_bgImage = new Bitmap(filePath);
			}
			else
			{
				m_bgImage = null;
			}
			m_param.ImagePath = filePath;
			this.Invalidate();
		}

		void IScreenControl.MoveControl(Point pos)
		{
			m_param.Location = pos;
			m_bgImageLocation = pos;
			this.Invalidate();
		}
		#endregion
	}

}
