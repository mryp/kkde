using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TagsXmlMaker
{
	/// <summary>
	/// KAGタグ属性の説明を保持するクラス
	/// </summary>
	public class KagTagAttr
	{
		string m_name = "";
		string m_shortInfo = "";
		string m_required = "";
		string m_format = "";
		string m_info = "";

		/// <summary>
		/// 属性名
		/// </summary>
		public string Name
		{
			get { return m_name; }
			set { m_name = value; }
		}

		/// <summary>
		/// 属性の短い説明
		/// </summary>
		public string ShortInfo
		{
			get { return m_shortInfo; }
			set { m_shortInfo = value; }
		}

		/// <summary>
		/// 属性が必須かどうか
		/// 必須の時true
		/// </summary>
		public string Required
		{
			get { return m_required; }
			set { m_required = value; }
		}

		/// <summary>
		/// 属性の入力内容フォーマット
		/// </summary>
		public string Format
		{
			get { return m_format; }
			set { m_format = value; }
		}

		/// <summary>
		/// 属性の長い説明
		/// </summary>
		public string Info
		{
			get { return m_info; }
			set { m_info = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public KagTagAttr()
		{
		}
	}
}
