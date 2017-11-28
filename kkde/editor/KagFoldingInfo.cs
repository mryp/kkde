using System;
using System.Collections.Generic;
using System.Text;
using kkde.parse;

namespace kkde.editor
{
	/// <summary>
	/// KAGの折りたたみ情報
	/// </summary>
	public class KagFoldingInfo
	{
		RegionItem[] m_regionList;
		KagLabelItem[] m_labelList;

		/// <summary>
		/// 範囲指定折りたたみ情報リストを取得する
		/// </summary>
		public RegionItem[] RgionList
		{
			get { return m_regionList; }
		}
		
		/// <summary>
		/// ラベルリストを取得する
		/// </summary>
		public KagLabelItem[] LabelList
		{
			get { return m_labelList; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="regionList"></param>
		/// <param name="labelList"></param>
		public KagFoldingInfo(RegionItem[] regionList, KagLabelItem[] labelList)
		{
			m_regionList = regionList;
			m_labelList = labelList;
		}

	}
}
