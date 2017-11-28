using System;
using System.Collections.Generic;
using System.Text;

namespace kkde.parse.complate
{
	/// <summary>
	/// KAGのタグ種別を保持するクラス
	/// </summary>
	public class KagTagKindInfo
	{
		KagCompletionData.Kind m_kind = KagCompletionData.Kind.Unknown;
		string m_tagName = "";
		string m_attrName = "";
		string m_attrValue = "";
		bool m_strMode = false;
		Dictionary<string, string> m_attrTable = new Dictionary<string, string>();

		/// <summary>
		/// 入力補完を行う種類
		/// </summary>
		public KagCompletionData.Kind Kind
		{
			get { return m_kind; }
			set { m_kind = value; }
		}

		/// <summary>
		/// 入力補完を行っているタグ名
		/// </summary>
		public string TagName
		{
			get { return m_tagName; }
			set { m_tagName = value; }
		}

		/// <summary>
		/// 入力補完を行っている属性名
		/// </summary>
		public string AttrName
		{
			get { return m_attrName; }
			set { m_attrName = value; }
		}

		/// <summary>
		/// 入力補完を行っている属性値
		/// </summary>
		public string AttrValue
		{
			get { return m_attrValue; }
			set { m_attrValue = value; }
		}

		/// <summary>
		/// 文字列入力中かどうか（"の後ろにあるかどうか）
		/// </summary>
		public bool StrMode
		{
			get { return m_strMode; }
			set { m_strMode = value; }
		}

		/// <summary>
		/// 属性テーブル
		/// key=属性名
		/// value=属性値
		/// </summary>
		public Dictionary<string, string> AttrTable
		{
			get { return m_attrTable; }
			set { m_attrTable = value; }
		}

		/// <summary>
		/// 文字列を返す
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string text = String.Format("kind={0}, tagName={1}, attrName={2}, attrValue={3}, strMode={4}\n"
				, m_kind, m_tagName, m_attrName, m_attrValue, m_strMode);
			foreach (string key in m_attrTable.Keys)
			{
				text += String.Format("attrTable[{0}]={1}\n", key, m_attrTable[key]);
			}

			return text;
		}

		/// <summary>
		/// 属性値テーブルに属性情報を追加する
		/// </summary>
		/// <param name="attrName">属性名</param>
		/// <param name="attrValue">属性値</param>
		public void AddAttrTable(string attrName, string attrValue)
		{
			if (attrName == "")
			{
				return;	//属性名が無いときは追加しない
			}
			if (m_attrTable.ContainsKey(attrName) == false)
			{
				m_attrTable.Add(attrName, "");
			}

			m_attrTable[attrName] = attrValue;
		}
	}
}
