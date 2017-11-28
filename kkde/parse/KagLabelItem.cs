using System;
using System.Collections.Generic;
using System.Text;

namespace kkde.parse
{
	/// <summary>
	/// KAGラベルを保持するクラス
	/// </summary>
	public class KagLabelItem
	{
		string m_filePath;
		int m_lineNumber;
		string m_label;

		/// <summary>
		/// ファイルパス
		/// </summary>
		public string FilePath
		{
			get { return m_filePath; }
		}

		/// <summary>
		/// 行番号
		/// </summary>
		public int LineNumber
		{
			get { return m_lineNumber; }
		}
		
		/// <summary>
		/// ラベル文字列
		/// </summary>
		public string Label
		{
			get { return m_label; }
		}

		/// <summary>
		/// ラベル名（*label|aaa の場合、*label を返す）
		/// </summary>
		public string LabelName
		{
			get
			{
				int pos = m_label.IndexOf('|');
				if (pos == -1)
				{
					return m_label;
				}

				return m_label.Substring(0, pos);
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="label">ラベル</param>
		/// <param name="lineNumber">行番号</param>
		public KagLabelItem(string label, string filePath, int lineNumber)
		{
			m_label = label;
			m_filePath = filePath;
			m_lineNumber = lineNumber;
		}

		/// <summary>
		/// ラベル情報を文字列として返す
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return String.Format("label={0}, line={1}, file={2}", m_label, m_lineNumber, m_filePath);
		}
	}
}
