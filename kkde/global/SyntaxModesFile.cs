using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace kkde.global
{
	/// <summary>
	/// 強調定義ファイル管理ファイル作成クラス
	/// </summary>
	public class SyntaxModesFile
	{
		/// <summary>
		/// モード
		/// </summary>
		private class Mode
		{
			string m_fileName = "";
			string m_name = "";
			string m_extensions = "";

			/// <summary>
			/// ファイル名
			/// </summary>
			public string FileName
			{
				get { return m_fileName; }
				set { m_fileName = value; }
			}
			
			/// <summary>
			/// 定義名
			/// </summary>
			public string Name
			{
				get { return m_name; }
				set { m_name = value; }
			}
			
			/// <summary>
			/// 拡張子リスト
			/// </summary>
			public string Extensions
			{
				get { return m_extensions; }
				set { m_extensions = value; }
			}

			/// <summary>
			/// コンストラクタ
			/// </summary>
			/// <param name="name">定義名</param>
			/// <param name="fileName">ファイル名</param>
			/// <param name="extensions">拡張子リスト</param>
			public Mode(string name, string fileName, string extensions)
			{
				m_name = name;
				m_fileName = fileName;
				m_extensions = extensions;
			}
		}

		/// <summary>
		/// 作成するモードリスト
		/// </summary>
		private static Mode[] m_modeList = new Mode[] {
			new Mode("TJS2", "tjs2_mode.xshd", ".tjs"),	
			new Mode("KAG", "kag_mode.xshd", ".ks;.asd"),	
			new Mode("Default", "def_mode.xshd", ".*"),	
		};

		/// <summary>
		/// コンストラクタ
		/// </summary>
		private SyntaxModesFile()
		{
		}

		/// <summary>
		/// SyntaxModesファイルを作成する
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static bool SaveFile(string filePath)
		{
			//書き込む内容作成
			string text = "";
			text += String.Format("<SyntaxModes version=\"1.0\">\n");
			foreach (Mode mode in m_modeList)
			{
				text += String.Format("    <Mode file       = \"{0}\"\n", mode.FileName);
				text += String.Format("          name       = \"{0}\"\n", mode.Name);
				text += String.Format("          extensions = \"{0}\"/>\n", mode.Extensions);
			}
			text += String.Format("</SyntaxModes>\n");

			//ファイルに書き込み
			StreamWriter sw;
			bool ret = true;
			using (sw = new StreamWriter(filePath))
			{
				try
				{
					sw.Write(text);
				}
				catch (Exception err)
				{
					System.Diagnostics.Debug.WriteLine("SyntaxModesファイル作成エラー: " + err.ToString());
					ret = false;
				}
			}

			return ret;
		}
	}
}
