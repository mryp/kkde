using System;
using System.Collections.Generic;
using System.Text;

namespace kkde.parse
{
	/// <summary>
	/// 字句解析インターフェイス
	/// </summary>
	public interface ILexer
	{
		/// <summary>
		/// 現在のトークンを取得する
		/// </summary>
		/// <returns>現在のトークン</returns>
		Token GetToken();

		/// <summary>
		/// 次のトークンを取得する
		/// </summary>
		/// <returns>次のトークン</returns>
		Token NextToken();
	}
}
