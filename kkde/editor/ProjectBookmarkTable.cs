using System;
using System.Collections.Generic;
using System.Text;

using kkde.search;

namespace kkde.editor
{
	/// <summary>
	/// プロジェクトのブックマークテーブル
	/// </summary>
	public class ProjectBookmarkTable
	{
		string m_projectPath = "";
		List<BookmarkInfo> m_list = new List<BookmarkInfo>();

		/// <summary>
		/// プロジェクトパス
		/// </summary>
		public string ProjectPath
		{
			get { return m_projectPath; }
			set { m_projectPath = value; }
		}
		
		/// <summary>
		/// 文字列リスト
		/// </summary>
		public List<BookmarkInfo> List
		{
			get { return m_list; }
			set { m_list = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ProjectBookmarkTable()
		{
		}
	}
}
