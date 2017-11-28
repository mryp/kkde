using System;
using System.Collections.Generic;
using System.Text;

namespace kkde.editor
{
	/// <summary>
	/// 履歴管理テーブルクラス
	/// </summary>
	public class HistoryTable
	{
		/// <summary>
		/// 履歴を保持するデフォルトの最大数
		/// </summary>
		const int DEF_MAX_COUNT = 8;

		/// <summary>
		/// 履歴リスト
		/// </summary>
		List<string> m_list = new List<string>();

		/// <summary>
		/// 履歴を保持する最大数
		/// </summary>
		int m_maxCount = DEF_MAX_COUNT;

		/// <summary>
		/// 履歴リスト
		/// </summary>
		public List<string> List
		{
			get { return m_list; }
			set { m_list = value; }
		}

		/// <summary>
		/// 最大保持数
		/// </summary>
		public int MaxCount
		{
			get { return m_maxCount; }
			set { m_maxCount = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public HistoryTable()
		{
		}

		/// <summary>
		/// アイテムを追加する
		/// </summary>
		/// <param name="item">追加するアイテム</param>
		public void Add(string item)
		{
			if (m_list.Contains(item) == true)
			{
				//すでに登録済みの場合はいったん削除する
				m_list.Remove(item);
			}
			if (m_list.Count >= m_maxCount)
			{
				//最大数を超えているときは一番最後の物を削除する
				m_list.RemoveAt(m_maxCount - 1);
			}

			m_list.Insert(0, item);	//先頭に追加する
		}


	}
}
