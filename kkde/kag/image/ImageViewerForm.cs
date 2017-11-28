using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using kkde.global;
using System.IO;
using kkde.project;
using System.Diagnostics;

namespace kkde.kag.image
{
	/// <summary>
	/// イメージビューワーを表示するフォーム
	/// </summary>
	public partial class ImageViewerForm : WeifenLuo.WinFormsUI.DockContent
	{
		#region 定数・フィールド
		/// <summary>
		/// 表示モード
		/// </summary>
		private enum Mode
		{
			/// <summary>
			/// ウィンドウサイズに合わせる
			/// </summary>
			Window,
			/// <summary>
			/// 画像サイズに合わせる
			/// </summary>
			FullScale,
		}

		/// <summary>
		/// 縮尺テーブル
		/// </summary>
		private readonly float[] SCALE_TABLE = {0.01F, 0.015F, 0.02F, 0.03F, 0.04F, 0.05F, 0.0625F, 0.0833F, 0.125F, 0.1667F, 0.25F, 0.3333F
							  ,0.50F, 0.6667F, 1.0F, 1.5F, 2.0F, 2.5F, 3.0F, 3.5F, 4.0F, 4.5F, 5.0F, 6.0F, 7.0F, 8.0F, 12.0F, 16.0F};
		/// <summary>
		/// 原寸大表示時のテーブル位置
		/// </summary>
		private const int SCALE_POS_FULL = 14;

		/// <summary>
		/// 表示している画像のファイルパス
		/// </summary>
		private string m_filePath = "";

		/// <summary>
		/// 表示する画像オブジェクト
		/// </summary>
		private Bitmap m_bitmap = null;

		/// <summary>
		/// 表示モード
		/// </summary>
		private Mode m_mode = Mode.FullScale;

		/// <summary>
		/// 現在表示しているテーブル位置
		/// </summary>
		private int m_scalePos = SCALE_POS_FULL;

		/// <summary>
		/// マウスボタンを押しているかどうか
		/// </summary>
		bool m_isMouseDown = false;

		/// <summary>
		/// マウスボタン押下位置
		/// </summary>
		Point m_mouseDownPos = new Point(0, 0);

		/// <summary>
		/// 次・前を探すときのファイルリスト
		/// 表示時に更新すること
		/// </summary>
		string[] m_searchFileList = null;
		#endregion

		#region 初期化
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ImageViewerForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 画像を表示する
		/// </summary>
		/// <param name="filePath">表示するファイルパス</param>
		/// <param name="show">ウィンドウを表示するかどうか（表示するときtrueを指定する）</param>
		public void ShowImage(string filePath, bool show)
		{
			if (m_bitmap != null)
			{
				//前回表示している画像の解放
				m_bitmap.Dispose();
				m_bitmap = null;
			}
			if (File.Exists(filePath) == false)
			{
				return;	//ファイルパスが不正
			}

			m_filePath = filePath;
			m_isMouseDown = false;
			m_bitmap = new Bitmap(m_filePath);
			if (m_mode == Mode.Window)
			{
				setSizeImageMatchWindow();		//ウィンドウに合わせる
			}
			else
			{
				setSizeImageScale(m_scalePos);	//スケール変更なしで表示
			}

			//表示するかどうか
			if (show)
			{
				this.Show(GlobalStatus.FormManager.MainForm.MainDockPanel);
				m_searchFileList = util.FileUtil.GetDirectoryFile(Path.GetDirectoryName(m_filePath)		//検索リストを更新する
					, FileType.GetKrkrFileExtForSearch(FileType.KrkrType.Image), SearchOption.TopDirectoryOnly);
			}
		}
		#endregion

		#region 画像操作メソッド
		/// <summary>
		/// 画像を拡大する
		/// </summary>
		private void setSizeImageExpand()
		{
			if (m_scalePos < SCALE_TABLE.Length - 1)
			{
				setSizeImageScale(m_scalePos + 1);
			}
		}

		/// <summary>
		/// 画像を縮小する
		/// </summary>
		private void setSizeImageReduce()
		{
			if (m_scalePos > 0)
			{
				setSizeImageScale(m_scalePos - 1);
			}
		}

		/// <summary>
		/// ウィンドウに合わせる
		/// </summary>
		private void setSizeImageMatchWindow()
		{
			m_mode = Mode.Window;
			imageViewBox.Dock = DockStyle.Fill;
			imageViewBox.Invalidate();
		}

		/// <summary>
		/// 原寸大の大きさにする
		/// </summary>
		private void setSizeImageFullScale()
		{
			setSizeImageScale(SCALE_POS_FULL);
		}

		/// <summary>
		/// 指定した縮尺で表示する
		/// </summary>
		/// <param name="scalePos">縮尺テーブルの位置</param>
		private void setSizeImageScale(int scalePos)
		{
			if (m_bitmap == null)
			{
				return;
			}
			if (scalePos < 0 || scalePos > SCALE_TABLE.Length)
			{
				return;
			}

			m_scalePos = scalePos;
			m_mode = Mode.FullScale;
			imageViewBox.Dock = DockStyle.None;
			imageViewBox.Size = new Size((int)((float)m_bitmap.Width * SCALE_TABLE[m_scalePos])
				, (int)((float)m_bitmap.Height * SCALE_TABLE[m_scalePos]));
			imageViewBox.Invalidate();
		}

		/// <summary>
		/// 次の画像を表示する
		/// </summary>
		private void showNextImage()
		{
			string nextFilePath = util.FileUtil.GetNextFilePath(m_searchFileList, m_filePath, FileType.KrkrType.Image);
			if (nextFilePath == "")
			{
				return;	//取得できず
			}
			ShowImage(nextFilePath, false);
		}

		/// <summary>
		/// 前の画像を表示する
		/// </summary>
		private void showPrevImage()
		{
			string prevFilePath = util.FileUtil.GetPrevFilePath(m_searchFileList, m_filePath, FileType.KrkrType.Image);
			if (prevFilePath == "")
			{
				return;	//取得できなかった
			}
			ShowImage(prevFilePath, false);
		}
		#endregion

		#region イベント
		/// <summary>
		/// イメージを描画する
		/// </summary>
		private void imageViewBox_Paint(object sender, PaintEventArgs e)
		{
			if (m_bitmap == null)
			{
				return;	//描画する物がないときは何もしない
			}

			Graphics g = e.Graphics;
			RectangleF rect;
			if (m_mode == Mode.Window)
			{
				float w = (float)imageViewBox.Width / (float)m_bitmap.Width;
				float h = (float)imageViewBox.Height / (float)m_bitmap.Height;
				if (h < w)
				{
					//縦の縮尺で表示する
					rect = new RectangleF(0, 0, m_bitmap.Width * h, m_bitmap.Height * h);
				}
				else
				{
					//横の縮尺で表示する
					rect = new RectangleF(0, 0, m_bitmap.Width * w, m_bitmap.Height * w);
				}
			}
			else
			{
				rect = new RectangleF(0, 0, m_bitmap.Width * SCALE_TABLE[m_scalePos], m_bitmap.Height * SCALE_TABLE[m_scalePos]);
			}
			g.DrawImage(m_bitmap, rect);
		}

		/// <summary>
		/// ウィンドウに合わせる
		/// </summary>
		private void menuItemSetSizeWindow_Click(object sender, EventArgs e)
		{
			setSizeImageMatchWindow();
		}

		/// <summary>
		/// 実際のサイズ
		/// </summary>
		private void menuItemSetSizeFullScale_Click(object sender, EventArgs e)
		{
			setSizeImageFullScale();
		}

		/// <summary>
		/// 拡大する
		/// </summary>
		private void menuItemSetSizeExpand_Click(object sender, EventArgs e)
		{
			setSizeImageExpand();
		}

		/// <summary>
		/// 縮小する
		/// </summary>
		private void menuItemSetSizeReduce_Click(object sender, EventArgs e)
		{
			setSizeImageReduce();
		}

		/// <summary>
		/// 次の画像を表示する
		/// </summary>
		private void menuItemShowNext_Click(object sender, EventArgs e)
		{
			showNextImage();
		}

		/// <summary>
		/// 前の画像を表示する
		/// </summary>
		private void menuItemShowPrev_Click(object sender, EventArgs e)
		{
			showPrevImage();
		}

		/// <summary>
		/// ファイル名をコピーする
		/// </summary>
		private void menuItemCopyFileName_Click(object sender, EventArgs e)
		{
			if (File.Exists(m_filePath) == false)
			{
				util.Msgbox.Error("コピーするファイルが見つかりません");
				return;
			}
			Clipboard.SetText(Path.GetFileNameWithoutExtension(m_filePath));
		}

		/// <summary>
		/// マウスボタンを押したとき
		/// </summary>
		private void imagePanel_MouseDown(object sender, MouseEventArgs e)
		{
			m_isMouseDown = true;
			m_mouseDownPos = e.Location;
		}

		/// <summary>
		/// マウスを移動したとき
		/// </summary>
		private void imagePanel_MouseMove(object sender, MouseEventArgs e)
		{
			if (m_isMouseDown)
			{
				imagePanel.AutoScrollPosition = new Point(
					-(e.X - m_mouseDownPos.X + imagePanel.AutoScrollPosition.X),
					-(e.Y - m_mouseDownPos.Y + imagePanel.AutoScrollPosition.Y));
			}
		}

		/// <summary>
		/// マウスボタンを上げたとき
		/// </summary>
		private void imagePanel_MouseUp(object sender, MouseEventArgs e)
		{
			m_isMouseDown = false;
			m_mouseDownPos = new Point(0, 0);
		}
		#endregion
	}
}
