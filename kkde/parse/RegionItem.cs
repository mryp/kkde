using System;
using System.Collections.Generic;
using System.Text;

namespace kkde.parse
{
	/// <summary>
	/// 折りたたみ用記号用アイテム
	/// </summary>
	public class RegionItem
	{
		public const string KAG_REGION_START = ";;region";
		public const string KAG_REGION_END = ";;endregion";

		public const string TJS_REGION_START = "//region";
		public const string TJS_REGION_END = "//endregion";

		/// <summary>
		/// 位置
		/// </summary>
		public enum Pos
		{
			/// <summary>
			/// 開始記号
			/// </summary>
			Start,
			/// <summary>
			/// 終了記号
			/// </summary>
			End,
		}

		string m_filePath = "";
		int m_lineNumber = -1;
		string m_text = "";
		Pos m_pos = Pos.Start;

		/// <summary>
		/// ファイルパス
		/// </summary>
		public string FilePath
		{
			get { return m_filePath; }
			set { m_filePath = value; }
		}
		
		/// <summary>
		/// 行番号
		/// </summary>
		public int LineNumber
		{
			get { return m_lineNumber; }
			set { m_lineNumber = value; }
		}
		
		/// <summary>
		/// 表示文字列（PosがStartのときのみ）
		/// </summary>
		public string Text
		{
			get { return m_text; }
			set { m_text = value; }
		}

		/// <summary>
		/// 位置（開始か終了か）
		/// </summary>
		public Pos Position
		{
			get { return m_pos; }
			set { m_pos = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public RegionItem(Pos pos, int lineNumber, string text, string filePath)
		{
			m_pos = pos;
			m_lineNumber = lineNumber;
			m_text = removeRegionSymbol(text);
			m_filePath = filePath;
		}

		/// <summary>
		/// 先頭記号を削除する
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public string removeRegionSymbol(string text)
		{
			text = text.Trim();
			if (text == KAG_REGION_START)
			{
				return text;
			}
			else if (text == TJS_REGION_START)
			{
				return text;
			}
			if (text.StartsWith(KAG_REGION_START))
			{
				return text.Remove(0, KAG_REGION_START.Length);	//記号部分を削除
			}
			else if (text.StartsWith(TJS_REGION_START))
			{
				return text.Remove(0, TJS_REGION_START.Length);	//記号部分を削除
			}

			return text;
		}
	}
}
