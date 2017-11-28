using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using kkde.global;
using kkde.project;

namespace kkde.kag.sound
{
	/// <summary>
	/// サウンドプレイヤーを表すフォーム
	/// </summary>
	public partial class SoundPlayerForm : WeifenLuo.WinFormsUI.DockContent
	{
		#region 再生実行スレッドに渡す引数クラス
		class SoundPlayerArgment
		{
			Yanesdk.Sound.Sound m_sound;
			string m_filePath;

			/// <summary>
			/// 再生オブジェクト
			/// </summary>
			public Yanesdk.Sound.Sound Sound
			{
				get { return m_sound; }
				set { m_sound = value; }
			}
			
			/// <summary>
			/// ファイルパス
			/// </summary>
			public string FilePath
			{
				get { return m_filePath; }
				set { m_filePath = value; }
			}

			/// <summary>
			/// コンストラクタ
			/// </summary>
			/// <param name="sound"></param>
			public SoundPlayerArgment(Yanesdk.Sound.Sound sound)
				: this(sound, "")
			{
			}

			/// <summary>
			/// コンストラクタ
			/// </summary>
			/// <param name="sound"></param>
			/// <param name="filePath"></param>
			public SoundPlayerArgment(Yanesdk.Sound.Sound sound, string filePath)
			{
				m_sound = sound;
				m_filePath = filePath;
			}
		}
		#endregion

		#region フィールド
		/// <summary>
		/// 再生オブジェクト
		/// </summary>
		Yanesdk.Sound.Sound m_sound = new Yanesdk.Sound.Sound();

		/// <summary>
		/// 次・前を探すときのファイルリスト
		/// 表示時に更新すること
		/// </summary>
		string[] m_searchFileList = null;
		#endregion

		#region 初期化・終了処理
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SoundPlayerForm()
		{
			InitializeComponent();
		}
		#endregion

		#region 再生関連メソッド
		/// <summary>
		/// 再生する
		/// </summary>
		/// <param name="filePath">再生するファイル名</param>
		/// <param name="show">表示するときtrue</param>
		public void ShowPlay(string filePath, bool show)
		{
			if (File.Exists(filePath) == false)
			{
				return;	//再生するファイルが無い
			}
			Play(filePath);
			if (show)
			{
				this.Show(GlobalStatus.FormManager.MainForm.MainDockPanel);
				m_searchFileList = util.FileUtil.GetDirectoryFile(Path.GetDirectoryName(filePath)		//検索リストを更新する
					, FileType.GetKrkrFileExtForSearch(FileType.KrkrType.Sound), SearchOption.TopDirectoryOnly);
			}
		}

		/// <summary>
		/// 現在開いているファイルを再生する
		/// </summary>
		public void Play()
		{
			Play("");
		}

		public void Play(string filePath)
		{
			SoundPlayerArgment arg = new SoundPlayerArgment(m_sound, filePath);
			if (soundBgWorker.IsBusy == false)
			{
				soundBgWorker.RunWorkerAsync(arg);
			}
		}

		/// <summary>
		/// 現在再生しているファイルを停止する
		/// </summary>
		public void Stop()
		{
			if (m_sound.IsPlaying())
			{
				m_sound.Stop();
			}
		}

		/// <summary>
		/// 再生ファイルの情報をセットする
		/// </summary>
		private void SetInfo()
		{
			if (m_sound.Loaded == false)
			{
				return;
			}

			fileInfoBox.Text = Path.GetFileName(m_sound.FileName);
		}
		#endregion

		#region イベント
		/// <summary>
		/// 前のファイルを再生する
		/// </summary>
		private void toolItemSoundPrev_Click(object sender, EventArgs e)
		{
			if (m_sound.Loaded == false)
			{
				return;
			}
			string prevFilePath = util.FileUtil.GetPrevFilePath(m_searchFileList, m_sound.FileName, FileType.KrkrType.Sound);
			if (prevFilePath == "")
			{
				return;	//取得できなかった
			}

			ShowPlay(prevFilePath, false);
		}

		/// <summary>
		/// 次のファイルを再生する
		/// </summary>
		private void toolItemSoundNext_Click(object sender, EventArgs e)
		{
			if (m_sound.Loaded == false)
			{
				return;
			}
			string nextFilePath = util.FileUtil.GetNextFilePath(m_searchFileList, m_sound.FileName, FileType.KrkrType.Sound);
			if (nextFilePath == "")
			{
				return;	//取得できず
			}

			ShowPlay(nextFilePath, false);	//表示せずに再生
		}

		/// <summary>
		/// 再生開始
		/// </summary>
		private void menuItemSoundPlay_Click(object sender, EventArgs e)
		{
			Play();
		}

		/// <summary>
		/// 再生停止
		/// </summary>
		private void menuItemSoundStop_Click(object sender, EventArgs e)
		{
			Stop();
		}

		/// <summary>
		/// ループ再生を行う
		/// </summary>
		private void toolItemSoundLoop_Click(object sender, EventArgs e)
		{
			Stop();
			if (toolItemSoundLoop.Checked)
			{
				m_sound.Loop = Int32.MaxValue;	//最大数でセットする
			}
			else
			{
				m_sound.Loop = 0;
			}
		}

		/// <summary>
		/// 名前をクリップボードにコピーする
		/// </summary>
		private void menuItemSoundCopyName_Click(object sender, EventArgs e)
		{
			if (m_sound.Loaded == false)
			{
				util.Msgbox.Error("コピーするファイルが見つかりません");
				return;
			}

			Clipboard.SetText(Path.GetFileNameWithoutExtension(m_sound.FileName));
		}
		#endregion

		#region スレッドイベント
		/// <summary>
		/// ファイルの読み込みと再生を実行する
		/// </summary>
		private void soundBgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			SoundPlayerArgment arg = e.Argument as SoundPlayerArgment;
			if (arg == null)
			{
				Debug.WriteLine("ERROR: サウンドオブジェクトを指定してください");
				return;
			}

			if (arg.Sound.Loaded == false || arg.FilePath != "")
			{
				if (File.Exists(arg.FilePath) == false)
				{
					return;	//再生するファイルが指定されていない
				}

				//読み込む
				Yanesdk.Ytl.YanesdkResult ret;
				switch (FileType.GetSoundType(arg.FilePath))
				{
					case FileType.SoundType.Ogg:
					case FileType.SoundType.Wave:
					case FileType.SoundType.Midi:
						ret = arg.Sound.Load(arg.FilePath);
						break;
					default:
						ret = Yanesdk.Ytl.YanesdkResult.FileNotFound;
						return;
				}
				if (ret != Yanesdk.Ytl.YanesdkResult.NoError)
				{
					Debug.WriteLine("再生できません: ret=" + ret.ToString());
					return;	//エラーが発生
				}
			}

			if (arg.Sound.IsPlaying())
			{
				arg.Sound.Stop();	//再生中の時は一旦停止する
			}

			arg.Sound.Play();
		}

		/// <summary>
		/// 再生実行が完了したとき
		/// </summary>
		private void soundBgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			SetInfo();
		}
		#endregion
	}
}
