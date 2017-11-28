using System;
using System.Collections.Generic;
using System.Text;

namespace kkde.parse
{
	/// <summary>
	/// KAGマクロの属性を表すクラス
	/// </summary>
	public class KagMacroAttr
	{
		string m_name = "";
		string m_comment = "";
		string m_defValue = "";
		string m_valueType = "";

		/// <summary>
		/// 属性名
		/// </summary>
		public string Name
		{
			get { return m_name; }
			set { m_name = value; }
		}
		
		/// <summary>
		/// 属性の説明
		/// </summary>
		public string Comment
		{
			get { return m_comment; }
			set { m_comment = value; }
		}
		
		/// <summary>
		/// 属性のデフォルト値
		/// </summary>
		public string DefaultValue
		{
			get { return m_defValue; }
			set { m_defValue = value; }
		}
		
		/// <summary>
		/// 属性値のタイプ名
		/// </summary>
		public string ValueType
		{
			get { return m_valueType; }
			set { m_valueType = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public KagMacroAttr()
		{
		}
	}
}
