using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TagsXmlMaker
{
	class KagTag
	{
		string m_name = "";
		string m_shortInfo = "";
		string m_group = "";
		string m_remarks = "";

		/// <summary>
		/// タグ名
		/// </summary>
		public string Name
		{
			get { return m_name; }
			set { m_name = value; }
		}

		/// <summary>
		/// タグの短い説明
		/// </summary>
		public string ShortInfo
		{
			get { return m_shortInfo; }
			set { m_shortInfo = value; }
		}

		/// <summary>
		/// タグのグループ
		/// </summary>
		public string Group
		{
			get { return m_group; }
			set { m_group = value; }
		}

		/// <summary>
		/// タグの長い説明
		/// </summary>
		public string Remarks
		{
			get { return m_remarks; }
			set { m_remarks = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public KagTag()
		{
		}
	}
}
