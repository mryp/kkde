using System;
using System.Collections.Generic;
using System.Text;

namespace kkde.parse
{
	/// <summary>
	/// TJS構文解析結果を保持するクラス
	/// </summary>
	public class TjsCompletionUnit : CompletionUnit
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public TjsCompletionUnit(string filePath)
			: base(filePath)
		{
		}
	}
}
