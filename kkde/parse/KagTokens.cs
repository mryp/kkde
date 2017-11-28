using System;
using System.Collections.Generic;
using System.Text;

namespace kkde.parse
{
	/// <summary>
	/// KAGトークンの種類定義クラス
	/// </summary>
	public static class KagTokens
	{
		//KagLexer用
		public const int EOF = 0;
		public const int Label = 1;
		public const int Macro = 2;
		public const int TjsScript = 3;
		public const int StartRegion = 4;
		public const int EndRegion = 5;

		//KagMacroLexer用
		public const int MacroName = 10;
		public const int MacroAttr = 11;
		public const int AsteriskTag = 12;
	}
}
