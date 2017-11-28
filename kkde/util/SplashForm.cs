using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace kkde.util
{
	public partial class SplashForm : Form
	{
		//Splashフォーム
		private static SplashForm _form = null;
		//メインフォーム
		private static Form _mainForm = null;
		//Splashを表示するスレッド
		private static System.Threading.Thread _thread = null;
		//lock用のオブジェクト
		private static readonly object syncObject = new object();

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SplashForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Splashフォーム
		/// </summary>
		public static SplashForm Form
		{
			get { return _form; }
		}

		/// <summary>
		/// Splashフォームを表示する
		/// </summary>
		/// <param name="mainForm">メインフォーム</param>
		public static void ShowSplash(Form mainForm)
		{
			if (_form != null || _thread != null)
				return;

			_mainForm = mainForm;
			//メインフォームのActivatedイベントでSplashフォームを消す
			if (_mainForm != null)
			{
				_mainForm.Activated += new EventHandler(_mainForm_Activated);
			}

			//スレッドの作成
			_thread = new System.Threading.Thread(
				new System.Threading.ThreadStart(StartThread));
			_thread.Name = "SplashForm";
			_thread.IsBackground = true;
			_thread.SetApartmentState(System.Threading.ApartmentState.STA);
			//スレッドの開始
			_thread.Start();
		}

		/// <summary>
		/// Splashフォームを表示する
		/// </summary>
		public static void ShowSplash()
		{
			ShowSplash(null);
		}

		/// <summary>
		/// Splashフォームを消す
		/// </summary>
		public static void CloseSplash()
		{
			lock (syncObject)
			{
				if (_form != null && _form.IsDisposed == false)
				{
					//Splashフォームを閉じる
					//Invokeが必要か調べる
					if (_form.InvokeRequired)
						_form.Invoke(new MethodInvoker(_form.Close));
					else
						_form.Close();
				}

				if (_mainForm != null)
				{
					_mainForm.Activated -= new EventHandler(_mainForm_Activated);
					//メインフォームをアクティブにする
					_mainForm.Activate();
				}

				_form = null;
				_thread = null;
				_mainForm = null;
			}
		}

		//スレッドで開始するメソッド
		private static void StartThread()
		{
			//Splashフォームを作成
			_form = new SplashForm();
			//Splashフォームをクリックして閉じられるようにする
			_form.Click += new EventHandler(_form_Click);
			//Splashフォームを表示する
			Application.Run(_form);
		}

		//Splashフォームがクリックされた時
		private static void _form_Click(object sender, EventArgs e)
		{
			//Splashフォームを閉じる
			CloseSplash();
		}

		//メインフォームがアクティブになった時
		private static void _mainForm_Activated(object sender, EventArgs e)
		{
			//Splashフォームを閉じる
			CloseSplash();
		}
	}
}
