using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace kkde.kag.sound
{
	public class MidiSoundFile : MciSoundFile
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="filePath">ファイルパス</param>
		/// <param name="parentForm">親フォーム</param>
		public MidiSoundFile(string filePath, Form parentForm)
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
			info += "type: MIDI" + "\r\n";

			return info;
		}
	}
}
