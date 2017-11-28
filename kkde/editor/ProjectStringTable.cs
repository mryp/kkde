using System;
using System.Collections.Generic;
using System.Text;

namespace kkde.editor
{
	/// <summary>
	/// プロジェクトをキーとする情報を保持するテーブル
	/// </summary>
	public class ProjectStringTable
	{
		string m_projectPath = "";
		List<string> m_list = new List<string>();

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
		public List<string> List
		{
			get { return m_list; }
			set { m_list = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ProjectStringTable()
		{
		}

	}
}
