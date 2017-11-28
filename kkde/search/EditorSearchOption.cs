using System;
using System.Collections.Generic;
using System.Text;
using kkde.editor;

namespace kkde.search
{
	/// <summary>
	/// 検索方向
	/// </summary>
	public enum SearchDirection
	{
		/// <summary>
		/// 上方向
		/// </summary>
		Up,
		/// <summary>
		/// 下方向
		/// </summary>
		Down,
	}

	/// <summary>
	/// 検索の種類
	/// </summary>
	public enum SearchType
	{
		/// <summary>
		/// 検索
		/// </summary>
		Search,
		/// <summary>
		/// 置換
		/// </summary>
		Replace,
		/// <summary>
		/// 全置換
		/// </summary>
		ReplaceAll,
		/// <summary>
		/// Grep検索
		/// </summary>
		Grep,
	}

	/// <summary>
	/// 検索時の設定情報を保持するクラス
	/// </summary>
	public class EditorSearchOption
	{
		#region フィールド
		const int MAX_HISTORY_COUNT = 20;
		HistoryTable m_searchKeywordList = new HistoryTable();
		HistoryTable m_replaceKeywordList = new HistoryTable();
		SearchDirection m_direction = SearchDirection.Down;
		bool m_ignorecase = false;
		bool m_wordunit = false;
		bool m_regex = false;
		SearchType m_type = SearchType.Search;
		GrepOption m_grepOption = new GrepOption();
		bool m_headStart = false;
		#endregion

		#region プロパティ
		/// <summary>
		/// 検索キーワードリスト
		/// </summary>
		public HistoryTable SearchKeywordTable
		{
			get { return m_searchKeywordList; }
			set { m_searchKeywordList = value; }
		}

		/// <summary>
		/// 置換キーワードリスト
		/// </summary>
		public HistoryTable ReplaceKeywordTable
		{
			get { return m_replaceKeywordList; }
			set { m_replaceKeywordList = value; }
		}

		/// <summary>
		/// 検索方向
		/// </summary>
		public SearchDirection Direction
		{
			get { return m_direction; }
			set { m_direction = value; }
		}

		/// <summary>
		/// 大文字小文字を区別しない
		/// </summary>
		public bool IgnoreCase
		{
			get { return m_ignorecase; }
			set { m_ignorecase = value; }
		}

		/// <summary>
		/// 単語単位の検索を行う
		/// </summary>
		public bool WordUnit
		{
			get { return m_wordunit; }
			set { m_wordunit = value; }
		}

		/// <summary>
		/// 正規表現を使用する
		/// </summary>
		public bool Regex
		{
			get { return m_regex; }
			set { m_regex = value; }
		}

		/// <summary>
		/// 検索タイプ
		/// </summary>
		public SearchType Type
		{
			get { return m_type; }
			set { m_type = value; }
		}

		/// <summary>
		/// Grep検索オプション
		/// </summary>
		public GrepOption GrepOption
		{
			get { return m_grepOption; }
			set { m_grepOption = value; }
		}

		/// <summary>
		/// 先頭から検索するかどうか
		/// trueのとき先頭から検索を開始する
		/// </summary>
		public bool HeadStart
		{
			get { return m_headStart; }
			set { m_headStart = value; }
		}
		#endregion

		#region プロパティ（読み取り専用）
		/// <summary>
		/// 検索キーワードを取得する（読み取り専用）
		/// </summary>
		public string SearchKeyword
		{
			get
			{
				if (m_searchKeywordList.List.Count == 0)
				{
					return "";
				}

				return m_searchKeywordList.List[0];
			}
		}

		/// <summary>
		/// 置換キーワードを取得する（読み取り専用）
		/// </summary>
		public string ReplaceKeyword
		{
			get
			{
				if (m_replaceKeywordList.List.Count == 0)
				{
					return "";
				}

				return m_replaceKeywordList.List[0];
			}
		}
		#endregion

		#region メソッド
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public EditorSearchOption()
		{
			m_searchKeywordList.MaxCount = MAX_HISTORY_COUNT;
			m_replaceKeywordList.MaxCount = MAX_HISTORY_COUNT;
		}
		#endregion
	}
}
