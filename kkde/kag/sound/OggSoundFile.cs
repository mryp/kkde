/*
//Tsukikage.Audioは凍結
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
//using Tsukikage.Audio;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace kkde.kag.sound
{
	/// <summary>
	/// OGGファイル再生管理クラス
	/// 未完成（なんかエラーが・・・orz）
	/// </summary>
	public unsafe class OggSoundFile : NativeWindow, ISoundFile
	{
		#region フィールド
		/// <summary>
		/// サウンドバッファ数
		/// </summary>
		const int MAX_BUFF_COUNT = 2;

		/// <summary>
		/// サウンドバッファサイズを決めるための秒数
		/// </summary>
		const int BUFFER_SIZE_TIME = 3;

		/// <summary>
		/// ファイルパス
		/// </summary>
		string m_filePath;

		/// <summary>
		/// 親フォーム
		/// </summary>
		Form m_parentForm;

		/// <summary>
		/// OGGファイルのデコードストリーム
		/// </summary>
		OggDecodeStream m_oggStream = null;

		/// <summary>
		/// waveOutのハンドル
		/// </summary>
		IntPtr m_hwo = IntPtr.Zero;

		/// <summary>
		/// 再生ファイル情報
		/// </summary>
		Win32PcmApi.WAVEFORMATEX m_wf;

		/// <summary>
		/// 再生データ情報
		/// </summary>
		Win32PcmApi.WAVEHDR_TAG[] m_whTag;

		/// <summary>
		/// 再生終了時呼び出し関数
		/// </summary>
		Win32PcmApi.waveOutProc m_waveOutproc;

		/// <summary>
		/// ダブルバッファリング用現在の再生データ情報バッファ番号
		/// </summary>
		int m_selectBuffNo = 0;

		/// <summary>
		/// OGGデコードデータを保持するバッファ（ダブルバッファ対応
		/// </summary>
		byte[][] m_soundBuffer;

		/// <summary>
		/// 再生中かどうか
		/// </summary>
		bool m_isPlay = false;
		#endregion

		#region プロパティ
		/// <summary>
		/// ファイルパスを取得する
		/// </summary>
		public string FilePath
		{
			get { return m_filePath; }
		}

		/// <summary>
		/// 再生中かどうか
		/// 再生中の時はtrue
		/// </summary>
		public bool IsPlay
		{
			get { return m_isPlay; }
		}
		#endregion

		#region メソッド
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="filePath">ファイルパス</param>
		public OggSoundFile(string filePath, Form parentForm)
		{
			m_filePath = filePath;
			m_parentForm = parentForm;
			parentForm.HandleDestroyed += new EventHandler(parentForm_HandleDestroyed);
			this.AssignHandle(m_parentForm.Handle);
		}

		void parentForm_HandleDestroyed(object sender, EventArgs e)
		{
			this.ReleaseHandle();
		}

		/// <summary>
		/// 再生する
		/// 
		/// 参考：
		/// http://d.hatena.ne.jp/HRM-7/20070311/1173619851
		/// http://wisdom.sakura.ne.jp/system/winapi/media/mm5.html
		/// http://sewig.jp/diary/?date=20060624
		/// </summary>
		public void Play()
		{
			if (File.Exists(m_filePath) == false)
			{
				util.Msgbox.Error("再生するファイルが見つかりません\n" + m_filePath);
				return;
			}

			if (m_isPlay)
			{
				//再生中の時は強制的に停止する
				Stop();
			}
			m_oggStream = getOggStream(FilePath);
			if (m_oggStream == null)
			{
				return;
			}

			m_wf = new Win32PcmApi.WAVEFORMATEX();
			m_wf.wFormatTag = 0x0001;
			m_wf.nChannels = (ushort)m_oggStream.Channels;
			m_wf.nSamplesPerSec = (uint)m_oggStream.SamplesPerSecond;
			m_wf.wBitsPerSample = (ushort)m_oggStream.BitsPerSample;
			m_wf.nBlockAlign = (ushort)(m_wf.nChannels * m_wf.wBitsPerSample / 8);
			m_wf.nAvgBytesPerSec = m_wf.nSamplesPerSec * m_wf.nBlockAlign;
			m_wf.cbSize = 0;

			Debug.WriteLine("waveOutOpen");
			m_hwo = new IntPtr();
			m_waveOutproc = new Win32PcmApi.waveOutProc(waveOutProc);
			//uint ret = Win32PcmApi.waveOutOpen(ref m_hwo, 0x0000, ref m_wf, m_waveOutproc, 0, Win32PcmApi.CALLBACK_FUNCTION);
			uint ret = Win32PcmApi.waveOutOpen(ref m_hwo, 0x0000, ref m_wf, (uint)this.Handle, 0, Win32PcmApi.CALLBACK_WINDOW);
			if (ret != 0)
			{
				util.Msgbox.Error("デバイスのオープンに失敗しました\nERROR NO:" + ret.ToString());
				return;
			}

			playFirst(m_hwo);
		}

		/// <summary>
		/// 再生時のコールバック関数
		/// </summary>
		/// <param name="hwo">デバイスハンドル</param>
		/// <param name="uMsg">送信メッセージ</param>
		/// <param name="dwInstance">ユーザー定義追加情報</param>
		/// <param name="dwParam1">メッセージの追加情報その１</param>
		/// <param name="dwParam2">メッセージの追加情報その２</param>
		private void waveOutProc(IntPtr hwo, uint uMsg, uint dwInstance, uint dwParam1, uint dwParam2)
		{
			switch (uMsg)
			{
				case Win32PcmApi.WOM_OPEN:
					break;
				case Win32PcmApi.WOM_CLOSE:
					break;
				case Win32PcmApi.WOM_DONE:
					Debug.WriteLine(String.Format("WOM_DONE dwParam1={0}, dwParam2={1}", dwParam1, dwParam2));
					playNext(hwo);
					break;
			}
		}

		/// <summary>
		/// 初期化し再生開始
		/// </summary>
		/// <param name="hwo"></param>
		private void playFirst(IntPtr hwo)
		{
			Debug.WriteLine("playFirst");
			m_soundBuffer = new byte[MAX_BUFF_COUNT][];
			m_whTag = new Win32PcmApi.WAVEHDR_TAG[MAX_BUFF_COUNT];
			for (int i = 0; i < m_whTag.Length; i++)
			{
				m_whTag[i] = new Win32PcmApi.WAVEHDR_TAG();
				if (setWaveStream(i) == true)
				{
					Win32PcmApi.waveOutPrepareHeader(hwo, ref m_whTag[i], (uint)sizeof(Win32PcmApi.WAVEHDR_TAG));
				}
			}

			for (int i = 0; i < m_whTag.Length; i++)
			{
				Win32PcmApi.waveOutWrite(hwo, ref m_whTag[i], (uint)sizeof(Win32PcmApi.WAVEHDR_TAG));
			}
			m_selectBuffNo = 0;// getNextSelectBuffNo(0);
			m_isPlay = true;
		}

		/// <summary>
		/// 次のバッファを取得し再生する
		/// </summary>
		/// <param name="hwo"></param>
		private void playNext(IntPtr hwo)
		{
			if (m_isPlay == false)
			{
				Debug.WriteLine("再生開始していないので次を再生できません");
				return;
			}

			bool ret = setWaveStream(m_selectBuffNo);
			if (ret == false)
			{
				Debug.WriteLine("再生失敗");
				return;	//再生する物がないので何もしない
			}

			uint phret = Win32PcmApi.waveOutPrepareHeader(hwo, ref m_whTag[m_selectBuffNo], (uint)sizeof(Win32PcmApi.WAVEHDR_TAG));
			uint outret = Win32PcmApi.waveOutWrite(hwo, ref m_whTag[m_selectBuffNo], (uint)sizeof(Win32PcmApi.WAVEHDR_TAG));
			Debug.WriteLine(String.Format("waveOutPrepareHeader={0}, waveOutWrite={1}", phret, outret));

			m_selectBuffNo = getNextBuffNo(m_selectBuffNo);	//バッファ番号を更新
		}

		/// <summary>
		/// 次にセットするバッファ番号を取得する
		/// </summary>
		/// <param name="selectBuffNo">現在のバッファ番号</param>
		/// <returns></returns>
		private int getNextBuffNo(int selectBuffNo)
		{
			selectBuffNo++;
			if (selectBuffNo > (MAX_BUFF_COUNT - 1))
			{
				selectBuffNo = 0;	//最大値をオーバーしたので最初にセット
			}

			return selectBuffNo;
		}

		/// <summary>
		/// WAVEストリームをセットする
		/// </summary>
		/// <param name="buffNo">バッファ番号</param>
		/// <returns>セットに成功したときtrue</returns>
		private bool setWaveStream(int buffNo)
		{
			if (buffNo > m_whTag.Length - 1)
			{
				Debug.WriteLine("バッファ番号が不正 buffNo=" + buffNo.ToString());
				return false;	//バッファ番号が不正
			}

			//データ読み込み
			uint buffSize = m_wf.nAvgBytesPerSec * BUFFER_SIZE_TIME;		//BUFFER_SIZE_TIME秒分のデータ
			m_soundBuffer[buffNo] = new byte[buffSize];
			int readSize = m_oggStream.Read(m_soundBuffer[buffNo], 0, m_soundBuffer[buffNo].Length);
			if (readSize <= 0)
			{
				Debug.WriteLine("m_oggStream読み取り失敗or終了");
				return false;	//読み取れなかったとき
			}

			if (m_whTag[buffNo].lpData == IntPtr.Zero)
			{
				Debug.WriteLine("lpDataメモリ確保");
				m_whTag[buffNo].lpData = Win32Memory.LocalAlloc(Win32Memory.LMEM_FIXED, buffSize);
				if (m_whTag[buffNo].lpData == IntPtr.Zero)
				{
					Debug.WriteLine("lpDataメモリ確保失敗");
					return false;	//メモリ確保できなかった
				}
			}
			Marshal.Copy(m_soundBuffer[buffNo], 0, m_whTag[buffNo].lpData, readSize);
			m_whTag[buffNo].dwBufferLength = (uint)readSize;
			m_whTag[buffNo].dwFlags = 0;				//バッファの追加情報
			m_whTag[buffNo].dwLoops = 1;				//１回再生
			m_whTag[buffNo].dwBytesRecorded = 0;		//録音用ではない
			m_whTag[buffNo].dwUser = 0;					//オプションのユーザ領域
			m_whTag[buffNo].lpNext = IntPtr.Zero;		//使用しない
			m_whTag[buffNo].reserved = 0;				//使用しない

			return true;	//セット完了
		}

		/// <summary>
		/// OGGファイルストリームを取得する
		/// 新たに読み直したいときはいったん閉じてから開く必要がある
		/// </summary>
		/// <param name="filePath">ファイルパス</param>
		/// <returns>ストリーム</returns>
		private OggDecodeStream getOggStream(string filePath)
		{
			if (m_oggStream == null)
			{
				//まだ取得していないときだけ取得する
				OggDecodeStream ogg = null;
				try
				{
					Stream stream = new FileStream(m_filePath, FileMode.Open);
					ogg = new OggDecodeStream(stream);
				}
				catch (Exception err)
				{
					util.Msgbox.Error("OGGファイルストリームの取得に失敗しました\n" + err.Message);
					ogg = null;
				}

				m_oggStream = ogg;
			}

			return m_oggStream;
		}

		/// <summary>
		/// 再生を止める
		/// </summary>
		public void Stop()
		{
			if (m_hwo != IntPtr.Zero)
			{
				m_isPlay = false;
				Win32PcmApi.waveOutReset(m_hwo);
				for (int i = 0; i < m_whTag.Length; i++)
				{
					if (m_whTag[i].lpData != IntPtr.Zero)
					{
						Win32Memory.LocalFree(m_whTag[i].lpData);
					}
					Win32PcmApi.waveOutUnprepareHeader(m_hwo, ref m_whTag[i], (uint)sizeof(Win32PcmApi.WAVEHDR_TAG));
				}
				Win32PcmApi.waveOutClose(m_hwo);

				m_selectBuffNo = 0;
				m_hwo = IntPtr.Zero;
			}

			if (m_oggStream != null)
			{
				m_oggStream.Close();
				m_oggStream.Dispose();
				m_oggStream = null;
			}
		}

		/// <summary>
		/// 解放する
		/// </summary>
		public void Dispose()
		{
			Stop();
			this.ReleaseHandle();
		}

		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case Win32PcmApi.MM_WOM_OPEN:
					break;
				case Win32PcmApi.MM_WOM_CLOSE:
					break;
				case Win32PcmApi.MM_WOM_DONE:
					Debug.WriteLine(String.Format("MM_WOM_DONE LParam={0}, WParam={1}", m.LParam, m.WParam));
					playNext((IntPtr)m.WParam);
					break;
			}
			base.WndProc(ref m);
		}

		/// <summary>
		/// ファイル情報を取得する
		/// </summary>
		/// <returns>ファイル情報</returns>
		public string GetInfo()
		{
			string info = "";
			info += Path.GetFileName(m_filePath) + "\r\n";
			info += "ファイルタイプ: Ogg Vorbis" + "\r\n";

			m_oggStream = getOggStream(FilePath);
			if (m_oggStream == null)
			{
				info += "FILE INFO ERROR\r\n";
			}
			else
			{
				info += String.Format("量子化ビット数: {0}\r\n", m_oggStream.BitsPerSample);
				info += String.Format("チャンネル数: {0}\r\n", m_oggStream.Channels);
				info += String.Format("再生周波数: {0}\r\n", m_oggStream.SamplesPerSecond);
				//info += String.Format("デコードバイトサイズ: {0}\r\n", m_oggStream.Length);
			}

			return info;
		}
		#endregion
	}
}
*/