using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace kkde.kag.sound
{
	/// <summary>
	/// MCIで再生を行う事が可能なファイル管理クラス
	/// </summary>
	public class MciSoundFile : NativeWindow, ISoundFile
	{
		#region フィールド
		/// <summary>
		/// ファイルパス
		/// </summary>
		internal string m_filePath;

		/// <summary>
		/// 親フォーム
		/// </summary>
		internal Form m_parentForm;

		/// <summary>
		/// 再生しているかどうか
		/// trueのとき再生中
		/// </summary>
		internal bool m_isPlay = false;

		/// <summary>
		/// オープンしているかどうか
		/// trueのときオープン中
		/// </summary>
		internal bool m_isOpen = false;
		#endregion

		#region プロパティ
		/// <summary>
		/// ファイルパスを取得する
		/// </summary>
		public string FilePath
		{
			get { return m_filePath; }
		}
		#endregion

		#region メソッド
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="filePath">再生するファイルパス</param>
		public MciSoundFile(string filePath, Form parentForm)
		{
			m_filePath = filePath;
			parentForm.HandleDestroyed += new EventHandler(parentForm_HandleDestroyed);
			m_parentForm = parentForm;
			this.AssignHandle(m_parentForm.Handle);
		}

		void parentForm_HandleDestroyed(object sender, EventArgs e)
		{
			this.ReleaseHandle();
		}

		/// <summary>
		/// 
		/// </summary>
		public unsafe void Open()
		{
			if (m_isOpen == false)
			{
				string cmd = String.Format("open \"{0}\" alias {1}", m_filePath, Win32MciApi.MCI_ALIAS_FILE_NAME);
				int ret = Win32MciApi.mciSendString(cmd, null, 0, IntPtr.Zero);
				if (ret != 0)
				{
					util.Msgbox.Error("再生ファイルのオープンに失敗しました\nERROR No: " + ret.ToString());
					return;	//エラー
				}

				m_isOpen = true;
			}
		}

		/// <summary>
		/// 再生する
		/// </summary>
		public void Play()
		{
			if (File.Exists(m_filePath) == false)
			{
				util.Msgbox.Error("再生するファイルが見つかりません\n" + m_filePath);
				return;
			}

			Stop();	//止める
			Open();	//開く

			//再生開始
			string cmd = String.Format("play {0} notify", Win32MciApi.MCI_ALIAS_FILE_NAME);
			Win32MciApi.mciSendString(cmd, null, 0, this.Handle);
			m_isPlay = true;
		}

		/// <summary>
		/// 再生を止める
		/// </summary>
		public void Stop()
		{
			if (m_isPlay)
			{
				//再生中なら止める
				m_isPlay = false;
				string cmd = String.Format("stop {0}", Win32MciApi.MCI_ALIAS_FILE_NAME);
				Win32MciApi.mciSendString(cmd, null, 0, IntPtr.Zero);
			}
			if (m_isOpen)
			{
				m_isOpen = false;
				string cmd = String.Format("close {0}", Win32MciApi.MCI_ALIAS_FILE_NAME);
				Win32MciApi.mciSendString(cmd, null, 0, IntPtr.Zero);
			}
		}

		/// <summary>
		/// 解放
		/// </summary>
		public void Dispose()
		{
			Stop();
			this.ReleaseHandle();
		}

		/// <summary>
		/// ファイル情報を取得する
		/// </summary>
		/// <returns>ファイル情報</returns>
		public virtual string GetInfo()
		{
			string info = "";
			info += Path.GetFileName(m_filePath) + "\r\n";
			info += "type: WAVE" + "\r\n";

			return info;
		}

		/// <summary>
		/// メッセージを補足する
		/// </summary>
		/// <param name="m">メッセージ</param>
		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case Win32MciApi.MM_MCINOTIFY:
					if ((int)m.WParam == Win32MciApi.MCI_NOTIFY_SUCCESSFUL)
					{
						this.Stop();
					}
					break;
			}
			base.WndProc(ref m);
		}
		#endregion
	}
}
