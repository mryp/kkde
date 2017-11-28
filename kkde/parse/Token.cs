using System;
using System.Collections.Generic;
using System.Text;

namespace kkde.parse
{
	/// <summary>
	/// 字句解析結果のトークン
	/// </summary>
	public class Token
	{
		int m_kind;
		int m_line;
		int m_col;
		string m_val;
		string m_comment;
		object m_obj;

		/// <summary>
		/// トークンの種類
		/// </summary>
		public int Kind
		{
			get { return m_kind; }
			set { m_kind = value; }
		}

		/// <summary>
		/// 行番号
		/// </summary>
		public int Line
		{
			get { return m_line; }
			set { m_line = value; }
		}

		/// <summary>
		/// 列番号
		/// </summary>
		public int Col
		{
			get { return m_col; }
			set { m_col = value; }
		}

		/// <summary>
		/// トークンの値
		/// </summary>
		public string Val
		{
			get { return m_val; }
			set { m_val = value; }
		}

		/// <summary>
		/// トークンのコメント
		/// </summary>
		public string Comment
		{
			get { return m_comment; }
			set { m_comment = value; }
		}

		public object Obj
		{
			get { return m_obj; }
			set { m_obj = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="kind"></param>
		/// <param name="line"></param>
		/// <param name="col"></param>
		public Token(int kind, int line, int col)
			: this(kind, line, col, "", "")
		{
		}

		public Token(int kind, int line, int col, string val, string comment)
			: this(kind, line, col, val, comment, null)
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="kind">種類</param>
		/// <param name="line">行番号</param>
		/// <param name="col">列番号</param>
		/// <param name="val">値</param>
		/// <param name="comment">コメント</param>
		public Token(int kind, int line, int col, string val, string comment, object obj)
		{
			m_kind = kind;
			m_line = line;
			m_col = col;
			m_val = val;
			m_comment = comment;
			m_obj = obj;
		}

		/// <summary>
		/// 文字列として返す
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return String.Format("kind={0}, line={1}, col={2}, val={3}, comment={4}", m_kind, m_line, m_col, m_val, m_comment);
		}
	}
}
