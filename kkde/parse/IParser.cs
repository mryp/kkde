using System;
using System.Collections.Generic;
using System.Text;

namespace kkde.parse
{
	/// <summary>
	/// 構文解析インターフェイス
	/// </summary>
	public interface IParser
	{
		CompletionUnit CompletionUnit
		{
			get;
		}

		/// <summary>
		/// 構文解析を実行する
		/// </summary>
		void Parse();
	}
}
