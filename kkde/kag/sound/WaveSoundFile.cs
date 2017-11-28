using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace kkde.kag.sound
{
	/// <summary>
	/// WAVEファイル再生管理クラス
	/// </summary>
	public class WaveSoundFile : MciSoundFile
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="filePath">ファイルパス</param>
		/// <param name="parentForm">親フォーム</param>
		public WaveSoundFile(string filePath, Form parentForm)
			: base(filePath, parentForm)
		{
		}

		/// <summary>
		/// 情報を取得する
		/// </summary>
		/// <returns></returns>
		public override string GetInfo()
		{
			string info = "";
			info += Path.GetFileName(this.FilePath) + "\r\n";
			info += "type: WAVE" + "\r\n";

			return info;
		}
	}
}
