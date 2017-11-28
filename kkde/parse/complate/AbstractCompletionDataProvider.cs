using System;
using System.Collections.Generic;
using System.Text;
using kkde.global;
using ICSharpCode.TextEditor.Gui.CompletionWindow;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor;

namespace kkde.parse.complate
{
	/// <summary>
	/// 入力補完プロバイダの共通部分抽象クラス
	/// </summary>
	public abstract class AbstractCompletionDataProvider : ICompletionDataProvider
	{
		#region フィールド
		internal string m_preSelection = null;
		internal int m_defaultIndex = -1;
		#endregion

		#region プロパティ
		/// <summary>
		/// リストに表示するアイコンイメージリスト
		/// </summary>
		public System.Windows.Forms.ImageList ImageList
		{
			get { return GlobalStatus.EditorManager.CompImageList.ImageList; }
		}

		/// <summary>
		/// 入力補完時に選択状態にして置換する文字列
		/// </summary>
		public string PreSelection
		{
			get { return m_preSelection; }
		}

		/// <summary>
		/// デフォルトで選択する番号
		/// </summary>
		public int DefaultIndex
		{
			get { return m_defaultIndex; }
		}
		#endregion

		#region メソッド
		/// <summary>
		/// キーが押されたとき
		/// </summary>
		/// <param name="key">押されたキー</param>
		/// <returns></returns>
		public CompletionDataProviderKeyResult ProcessKey(char key)
		{
			return CompletionDataProviderKeyResult.NormalKey;
		}

		/// <summary>
		/// 文字列を挿入する
		/// </summary>
		/// <param name="data"></param>
		/// <param name="textArea"></param>
		/// <param name="insertionOffset"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool InsertAction(ICompletionData data, ICSharpCode.TextEditor.TextArea textArea, int insertionOffset, char key)
		{
			textArea.Caret.Position = textArea.Document.OffsetToPosition(insertionOffset);
			return data.InsertAction(textArea, key);
		}

		/// <summary>
		/// 現在のカーソル位置にあるワードを取得する
		/// </summary>
		/// <param name="textArea"></param>
		/// <returns></returns>
		public string GetWordBeforeCaret(TextArea textArea)
		{
			int start = TextUtilities.FindPrevWordStart(textArea.Document, textArea.Caret.Offset);
			return textArea.Document.GetText(start, textArea.Caret.Offset - start);
		}

		/// <summary>
		/// 入力補完リストに表示するデータリストを生成する
		/// 継承クラス側で上書きすること
		/// </summary>
		/// <param name="fileName">ファイル名</param>
		/// <param name="textArea">アクティブなテキストエディタ</param>
		/// <param name="charTyped">入力された文字</param>
		/// <returns>リストに表示するデータリスト</returns>
		public abstract ICompletionData[] GenerateCompletionData(string fileName, ICSharpCode.TextEditor.TextArea textArea, char charTyped);
		#endregion
	}
}
