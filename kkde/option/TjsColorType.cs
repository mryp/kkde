using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace kkde.option
{
	/// <summary>
	/// TJSファイルのカラー設定保持クラス
	/// </summary>
	public class TjsColorType : BaseColorType
	{
		#region フィールド
		private Color m_comment;
		private Color m_string;
		private Color m_regexp;
		private Color m_octet;
		private Color m_preProKeyWord;
		private Color m_keyWord;
		#endregion

		[Category("TJS2")]
		[Description("コメントの文字色")]
		public Color Comment
		{
			get { return m_comment; }
			set { m_comment = value; }
		}

		[Category("TJS2")]
		[Description("文字列の文字色")]
		public Color String
		{
			get { return m_string; }
			set { m_string = value; }
		}

		[Category("TJS2")]
		[Description("正規表現の文字色")]
		[Browsable(false)]	//これは非対応に変更
		public Color Regexp
		{
			get { return m_regexp; }
			set { m_regexp = value; }
		}

		[Category("TJS2")]
		[Description("オクテット列の文字色")]
		public Color Octet
		{
			get { return m_octet; }
			set { m_octet = value; }
		}

		[Category("TJS2")]
		[Description("プリプロセッサ指令の文字色")]
		public Color PreProcessorKeyWord
		{
			get { return m_preProKeyWord; }
			set { m_preProKeyWord = value; }
		}

		[Category("TJS2")]
		[Description("予約後の文字色")]
		public Color KeyWord
		{
			get { return m_keyWord; }
			set { m_keyWord = value; }
		}

	}
}
