using System;
using System.Collections.Generic;
using System.Text;
using kkde.project;

namespace kkde.parse
{
	/// <summary>
	/// 構文解析結果を管理するクラス
	/// </summary>
	public class CompletionUnitManager
	{
		/// <summary>
		/// KAG解析結果を保持するテーブル
		/// </summary>
		Dictionary<string, KagCompletionUnit> m_kagTable = new Dictionary<string, KagCompletionUnit>();

		/// <summary>
		/// TJS解析結果を保持するテーブル
		/// </summary>
		Dictionary<string, TjsCompletionUnit> m_tjsTable = new Dictionary<string, TjsCompletionUnit>();

		kagex.KagexCompletionUnit m_kagexEnvinitInfo = null;

		/// <summary>
		/// KAG構文解析結果テーブル（読み取り専用）
		/// </summary>
		public Dictionary<string, KagCompletionUnit> KagTable
		{
			get { return m_kagTable; }
		}

		/// <summary>
		/// TJS構文解析結果テーブル（読み取り専用）
		/// </summary>
		public Dictionary<string, TjsCompletionUnit> TjsTable
		{
			get { return m_tjsTable; }
		}

		/// <summary>
		/// KAGEXのEnvinit.tjsファイルを解析した結果
		/// </summary>
		public kagex.KagexCompletionUnit KagexEnvinitInfo
		{
			get { return m_kagexEnvinitInfo; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CompletionUnitManager()
		{
		}

		/// <summary>
		/// テーブルをすべてクリアーする
		/// </summary>
		public void Clear()
		{
			m_kagTable.Clear();
		}

		/// <summary>
		/// 構文解析結果を追加する
		/// </summary>
		/// <param name="filePath"></param>
		/// <param name="cu"></param>
		public void Add(string filePath, CompletionUnit cu)
		{
			if (filePath == "")
			{
				return;	//空の時は追加しない
			}
			if (FileType.GetKrkrType(filePath) == FileType.KrkrType.Kag)
			{
				if (m_kagTable.ContainsKey(filePath) == false)
				{
					m_kagTable.Add(filePath, null);
				}

				m_kagTable[filePath] = (KagCompletionUnit)cu;
			}
			else if (FileType.GetKrkrType(filePath) == FileType.KrkrType.Tjs)
			{
				if (FileType.IsKagexEnvinitFileName(filePath))
				{
					m_kagexEnvinitInfo = (kagex.KagexCompletionUnit)cu;
				}
			}
		}
	}
}
