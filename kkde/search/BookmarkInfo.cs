using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.TextEditor.Document;


namespace kkde.search
{
	/// <summary>
	/// ブックマークリストに表示するブックマーク情報を保持するクラス
	/// </summary>
	public class BookmarkInfo
	{
		int m_lineNumber = -1;
		string m_filePath = "";
		string m_name = "";

		#region プロパティ
		/// <summary>
		/// 行番号
		/// </summary>
		public int LineNumber
		{
			get { return m_lineNumber; }
			set { m_lineNumber = value; }
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
		/// ブックマークの名前
		/// </summary>
		public string Name
		{
			get { return m_name; }
			set { m_name = value; }
		}
		#endregion

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BookmarkInfo()
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="lineNumber">行番号</param>
		/// <param name="filePath">ファイル名</param>
		/// <param name="name">ブックマーク名</param>
		public BookmarkInfo(int lineNumber, string filePath, string name)
		{
			m_lineNumber = lineNumber;
			m_filePath = filePath;
			m_name = name;
		}
	}
}
