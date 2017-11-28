using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using ICSharpCode.TextEditor.Gui.CompletionWindow;

using kkde.global;

namespace kkde.parse.complate
{
	/// <summary>
	/// TJS入力補完用データ生成クラス
	/// </summary>
	public class TjsCompletionDataProvider : AbstractCompletionDataProvider
	{		
		/// <summary>
		/// 入力補完リストに表示するデータリストを生成する
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="textArea"></param>
		/// <param name="charTyped"></param>
		/// <returns></returns>
		public override ICompletionData[] GenerateCompletionData(string fileName, ICSharpCode.TextEditor.TextArea textArea, char charTyped)
		{
			List<ICompletionData> list = new List<ICompletionData>();

			list.Add(new TjsCompletionData("TJS入力補完テスト", "テストその0"));
			list.Add(new TjsCompletionData("あああ", "テストその1"));
			list.Add(new TjsCompletionData("いいい", "テストその2"));
			list.Add(new TjsCompletionData("ううう", "テストその3"));

			return list.ToArray();
		}
	}
}
