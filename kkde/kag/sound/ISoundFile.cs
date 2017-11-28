using System;
using System.Collections.Generic;
using System.Text;

namespace kkde.kag.sound
{
	/// <summary>
	/// サウンドファイルのインターフェイス
	/// </summary>
	public interface ISoundFile
	{
		/// <summary>
		/// 開いているファイルパスを取得する
		/// </summary>
		string FilePath { get; }

		/// <summary>
		/// ファイルを再生する
		/// </summary>
		void Play();

		/// <summary>
		/// 再生を止める
		/// </summary>
		void Stop();

		/// <summary>
		/// 強制的に解放
		/// </summary>
		void Dispose();

		/// <summary>
		/// ファイル情報
		/// </summary>
		/// <returns>ファイル情報</returns>
		string GetInfo();
	}
}
