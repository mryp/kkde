using System;
using System.Collections.Generic;
using System.Text;

namespace kkde.parse
{
	/// <summary>
	/// 構文解析結果格納クラス
	/// </summary>
	public class CompletionUnit
	{
		/// <summary>
		/// 構文解析したファイルパス
		/// </summary>
		string m_filePath = "";

		/// <summary>
		/// 構文解析したファイルパス
		/// </summary>
		public string FilePath
		{
			get { return m_filePath; }
			set { m_filePath = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CompletionUnit(string filePath)
		{
			m_filePath = filePath;
		}
	}
}
