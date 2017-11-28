using System;
using System.Collections.Generic;
using System.Text;

namespace kkde.kagex
{
	public interface IActionInfo
	{
		/// <summary>
		/// 情報を保持するハンドラ名
		/// </summary>
		/// <returns></returns>
		string GetHandlerName();

		/// <summary>
		/// 辞書配列コードとしてオブジェクト内容を返す
		/// </summary>
		/// <returns></returns>
		string GetCode();
	}
}
