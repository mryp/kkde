using System;
using System.Collections.Generic;
using System.Text;

using ICSharpCode.TextEditor.Gui.CompletionWindow;
using System.Diagnostics;

namespace kkde.parse.complate
{
	/// <summary>
	/// TJSの入力補完表示用データ
	/// </summary>
	public class TjsCompletionData : ICompletionData
	{
		#region フィールド
		string m_text = "";
		string m_description = "";
		#endregion

		#region プロパティ
		/// <summary>
		/// 表示するアイコン番号
		/// </summary>
		public int ImageIndex
		{
			get { return 0; }
		}

		/// <summary>
		/// 入力補完リストに表示する文字列
		/// </summary>
		public string Text
		{
			get
			{
				return m_text;
			}
			set
			{
				m_text = value;
			}
		}

		/// <summary>
		/// 項目選択時に表示される詳細説明
		/// </summary>
		public string Description
		{
			get { return m_description; }
		}

		/// <summary>
		/// 優先度（次回表示時の重み付け？）
		/// </summary>
		public double Priority
		{
			get { return 0; }
		}
		#endregion

		#region メソッド
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="text">リストに表示する文字列</param>
		/// <param name="description">項目選択のTips</param>
		public TjsCompletionData(string text, string description)
		{
			m_text = text;
			m_description = description;
		}

		/// <summary>
		/// 項目選択決定時にエディタへのアクション
		/// </summary>
		/// <param name="textArea">操作するエディタ</param>
		/// <param name="ch">入力された文字</param>
		/// <returns>成功したときtrue</returns>
		public bool InsertAction(ICSharpCode.TextEditor.TextArea textArea, char ch)
		{
			textArea.InsertString(m_text);
			return false;
		}

		/// <summary>
		/// 並び替え用比較関数
		/// </summary>
		/// <param name="obj">TjsCompletionDataのオブジェクト</param>
		/// <returns>比較結果</returns>
		public int CompareTo(object obj)
		{
			if (obj == null || !(obj is TjsCompletionData))
			{
				return -1;
			}
			return m_text.CompareTo(((TjsCompletionData)obj).Text);	//文字列で比較する
		}
		#endregion
	}
}
