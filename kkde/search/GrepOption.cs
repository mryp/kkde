using System;
using System.Collections.Generic;
using System.Text;
using kkde.editor;
using kkde.global;

namespace kkde.search
{
	/// <summary>
	/// Grep検索開始場所
	/// </summary>
	public enum GrepPotision
	{
		/// <summary>
		/// プロジェクトフォルダ
		/// </summary>
		Project,
		/// <summary>
		/// 任意のフォルダ
		/// </summary>
		Folder,
	}

	/// <summary>
	/// Grep検索時のオプション情報を保持する
	/// </summary>
	public class GrepOption
	{
		#region フィールド
		HistoryTable m_fileExtTable = new HistoryTable();
		GrepPotision m_pos = GrepPotision.Project;
		string m_folderPath = "";
		bool m_useSubFolder = true;
		#endregion

		#region プロパティ
		/// <summary>
		/// 検索するファイルの拡張子のリスト
		/// 例：*.ks;*.tjs
		/// </summary>
		public HistoryTable FileExtTable
		{
			get { return m_fileExtTable; }
			set { m_fileExtTable = value; }
		}

		/// <summary>
		/// 検索開始位置
		/// </summary>
		public GrepPotision Pos
		{
			get { return m_pos; }
			set { m_pos = value; }
		}

		/// <summary>
		/// 検索開始フォルダパス（任意フォルダ検索時使用）
		/// </summary>
		public string FolderPath
		{
			get { return m_folderPath; }
			set { m_folderPath = value; }
		}

		/// <summary>
		/// サブフォルダも検索するかどうか
		/// </summary>
		public bool UseSubFolder
		{
			get { return m_useSubFolder; }
			set { m_useSubFolder = value; }
		}

		#endregion

		#region プロパティ（読み取り専用）
		/// <summary>
		/// ファイルの拡張子を取得する（読み取り専用）
		/// </summary>
		public string FileExt
		{
			get
			{
				if (m_fileExtTable.List.Count == 0)
				{
					return "";
				}

				return m_fileExtTable.List[0];
			}
		}

		/// <summary>
		/// Grep検索の開始ディレクトリパスを返す
		/// </summary>
		/// <returns>ディレクトリパス</returns>
		public string SearchStartPath
		{
			get
			{
				if (GlobalStatus.Project.IsOpened == false)
				{
					return "";
				}

				string path = "";
				if (this.Pos == GrepPotision.Project)
				{
					path = GlobalStatus.Project.DataFullPath;
				}
				else if (this.Pos == GrepPotision.Folder)
				{
					path = this.FolderPath;
				}

				return path;
			}
		}
		#endregion

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public GrepOption()
		{
		}
	}
}
