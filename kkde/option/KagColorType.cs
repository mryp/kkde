using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace kkde.option
{
	/// <summary>
	/// KAG強調表示カラー設定を保持するクラス
	/// </summary>
	public class KagColorType : BaseColorType
	{
		#region フィールド
		private Color m_comment;
		private Color m_label;
		private Color m_scriptTagName;
		private Color m_tagName;
		private Color m_attributeName;
		private Color m_attributeValue;
		#endregion

		[Category("KAG3")]
		[Description("コメント行の文字色")]
		public Color Comment
		{
			get { return m_comment; }
			set { m_comment = value; }
		}

		[Category("KAG3")]
		[Description("ラベル名の文字色")]
		public Color Label
		{
			get { return m_label; }
			set { m_label = value; }
		}

		[Category("KAG3")]
		[Description("TJSスクリプトを呼び出すタグの文字色")]
		public Color TjsScript
		{
			get { return m_scriptTagName; }
			set { m_scriptTagName = value; }
		}

		[Category("KAG3")]
		[Description("タグ名の文字色")]
		public Color TagName
		{
			get { return m_tagName; }
			set { m_tagName = value; }
		}

		[Category("KAG3")]
		[Description("属性名の文字色")]
		public Color AttributeName
		{
			get { return m_attributeName; }
			set { m_attributeName = value; }
		}

		[Category("KAG3")]
		[Description("属性値の文字色")]
		public Color AttributeValue
		{
			get { return m_attributeValue; }
			set { m_attributeValue = value; }
		}

	}
}
