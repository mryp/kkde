using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using kkde.editor;
using kkde.screen.control;
using kkde.global;
using System.Xml;

namespace kkde.screen
{
	/// <summary>
	/// スクリーンエディタフォーム
	/// </summary>
	public partial class ScreenMakerForm : WeifenLuo.WinFormsUI.DockContent, IEditorDocContent
	{
		#region フィールド
		/// <summary>
		/// エディタ内容が保存済みかどうかを保持する変数
		/// trueの時は保存されていない。falseのときは保存されている
		/// </summary>
		private bool m_isTextChanged = false;

		/// <summary>
		/// エディタを閉じたかどうか
		/// </summary>
		private bool m_IsClosed = false;

		/// <summary>
		/// ファイルパス
		/// </summary>
		private String m_filePath = "";

		/// <summary>
		/// 背景用キャンパスとなるパネル
		/// </summary>
		private ScreenPanel m_bgPanel;
		#endregion

		#region プロパティ
		/// <summary>
		/// ファイル名
		/// </summary>
		public String FileName
		{
			get
			{
				return m_filePath;
			}
		}

		/// <summary>
		/// エディタ内容が変更されているかどうか
		/// 変更されている（未保存）ときはtrueを返す
		/// </summary>
		public bool IsTextChanged
		{
			get { return m_isTextChanged; }
		}

		/// <summary>
		/// エディタを閉じた後かどうか
		/// trueのときはすでに閉じている
		/// </summary>
		public bool IsClosed
		{
			get { return m_IsClosed; }
		}
		#endregion

		#region 初期化メソッド
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ScreenMakerForm()
		{
			InitializeComponent();

			inttPanel();
			initTab();
		}

		/// <summary>
		/// 背景パネルを初期化する
		/// </summary>
		private void inttPanel()
		{
			m_bgPanel = new ScreenPanel();
			m_bgPanel.AllowDrop = true;
			m_bgPanel.BackColor = System.Drawing.Color.Black;
			m_bgPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			m_bgPanel.Location = new System.Drawing.Point(8, 32);
			m_bgPanel.Name = "bgPanel";
			m_bgPanel.Size = new System.Drawing.Size(800, 600);
			m_bgPanel.TabIndex = 0;
			m_bgPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.bgPanel_DragDrop);
			m_bgPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.bgPanel_DragEnter);
			this.Controls.Add(m_bgPanel);
		}

		/// <summary>
		/// タブの状態を初期化する
		/// </summary>
		private void initTab()
		{
			setTabText(false, "無題");
		}
		#endregion

		#region ファイル関連メソッド
		/// <summary>
		/// ファイルを読み込む
		/// </summary>
		/// <param name="fileName"></param>
		public void LoadFile(string fileName)
		{
			m_filePath = fileName;
			setTabText(false);

			//未実装
		}

		/// <summary>
		/// ファイルに保存する
		/// </summary>
		/// <param name="fileName"></param>
		public void SaveFile(string fileName)
		{
			setTabText(false);

			using (FileStream fs = new FileStream(fileName, FileMode.Create))
			{
				using (XmlTextWriter xw = new XmlTextWriter(fs, Encoding.UTF8))
				{
					xw.Formatting = Formatting.Indented;

					xw.WriteStartDocument();
					xw.WriteStartElement("krkrui");

					m_bgPanel.WriteXml(xw);

					xw.WriteEndElement();
					xw.WriteEndDocument();
				}
			}
		}

		#endregion

		#region タブ・ウィンドウ関連メソッド
		/// <summary>
		/// タブテキストをセットする
		/// </summary>
		/// <param name="isChanged">変更があるかどうか（変更ありの時はtrueをしていする）</param>
		private void setTabText(bool isChanged)
		{
			if (File.Exists(m_filePath) == false)
			{
				return;
			}
			setTabText(isChanged, Path.GetFileName(m_filePath));
		}

		/// <summary>
		/// タブテキストをセットする
		/// </summary>
		/// <param name="isChanged">変更があるかどうか</param>
		/// <param name="text">タブに表示するテキスト</param>
		private void setTabText(bool isChanged, string text)
		{
			if (isChanged == true)
			{
				//変更ありの時は米印をつける
				text = text + " *";
			}

			m_isTextChanged = isChanged;
			this.TabText = text;
		}

		/// <summary>
		/// フォームをアクティブにする
		/// </summary>
		public void ActivateForm()
		{
			this.Activate();
		}

		/// <summary>
		/// フォームを閉じる
		/// </summary>
		public void CloseForm()
		{
			this.Close();
		}

		/// <summary>
		/// フォームが閉じられたとき
		/// </summary>
		private void ScreenMakerForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			m_IsClosed = true;
		}
		#endregion

		#region ドロップ関連
		/// <summary>
		/// 領域に入ってきたとき
		/// </summary>
		private void bgPanel_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(string)))
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		/// <summary>
		/// ドロップしたとき
		/// </summary>
		private void bgPanel_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(string)))
			{
				string classText = (string)e.Data.GetData(typeof(string));
				if (classText.StartsWith("kkde") == false)
				{
					//コントロールクラスではないので何もしない
					return;
				}

				Debug.WriteLine("drop classText=" + classText);
				Type type = Type.GetType(classText);
				BaseType instance = (BaseType)Activator.CreateInstance(type);

				ScreenControl control = createScreenControl(instance);
				m_bgPanel.AddControl(control);
			}
		}

		/// <summary>
		/// スクリーンコントロールを新しく作成する
		/// </summary>
		/// <param name="param">パラメーター</param>
		/// <returns>コントロール</returns>
		private ScreenControl createScreenControl(BaseType param)
		{
			Point controlPos = m_bgPanel.PointToClient(Cursor.Position);
			param.Location = controlPos;

			ScreenControl control = new ScreenControl(m_bgPanel);
			control.Param = param;
			control.Image = global::kkde.Properties.Resources.ImgViewBG;
			control.Location = param.Location;
			control.Size = control.Image.Size;

			return control;
		}
		#endregion
	}
}
