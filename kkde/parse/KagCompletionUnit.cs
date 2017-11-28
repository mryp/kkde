using System;
using System.Collections.Generic;
using System.Text;

namespace kkde.parse
{
	/// <summary>
	/// KAG構文解析結果を保持するクラス
	/// </summary>
	public class KagCompletionUnit : CompletionUnit
	{
		List<KagLabelItem> m_labelList = new List<KagLabelItem>();
		Dictionary<string, KagMacro> m_macroTable = new Dictionary<string, KagMacro>();
		List<RegionItem> m_regionList = new List<RegionItem>();

		/// <summary>
		/// ラベルリストを取得する
		/// </summary>
		public List<KagLabelItem> LabelList
		{
			get { return m_labelList; }
		}

		/// <summary>
		/// マクロリストを取得する
		/// </summary>
		public Dictionary<string, KagMacro> MacroTable
		{
			get { return m_macroTable; }
		}

		/// <summary>
		/// 折りたたみ記号リスト
		/// </summary>
		public List<RegionItem> RegionList
		{
			get { return m_regionList; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public KagCompletionUnit(string filePath)
			: base(filePath)
		{
		}

		/// <summary>
		/// ラベルを追加する
		/// </summary>
		/// <param name="label">ラベル</param>
		/// <param name="lineNumber">行番号</param>
		public void AddLabel(string label, int lineNumber)
		{
			m_labelList.Add(new KagLabelItem(label, this.FilePath, lineNumber));
		}

		/// <summary>
		/// マクロを追加する
		/// </summary>
		/// <param name="macro">追加するマクロオブジェクト</param>
		public void AddMacro(KagMacro macro)
		{
			if (macro == null || macro.Name == null || macro.Name == "")
			{
				return;	//何もしない
			}

			if (m_macroTable.ContainsKey(macro.Name))
			{
				m_macroTable.Remove(macro.Name);	//すでに存在するときは一端削除する
			}
			m_macroTable.Add(macro.Name, macro);
		}

		/// <summary>
		/// 折りたたみを追加する
		/// </summary>
		/// <param name="pos">位置</param>
		/// <param name="regionText">折りたたみの文字列</param>
		/// <param name="lineNumber">行番号</param>
		public void AddRegion(RegionItem.Pos pos, string regionText, int lineNumber)
		{
			if (regionText == "")
			{
				return;
			}
			else if (pos == RegionItem.Pos.Start && regionText.StartsWith(RegionItem.KAG_REGION_START) == false)
			{
				return;	//start記号と合致しないとき
			}
			else if (pos == RegionItem.Pos.End && regionText.StartsWith(RegionItem.KAG_REGION_END) == false)
			{
				return;	//end記号と合致しない
			}

			m_regionList.Add(new RegionItem(pos, lineNumber, regionText, this.FilePath));
		}
	}
}
