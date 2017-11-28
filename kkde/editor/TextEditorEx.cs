using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor.Actions;
using ICSharpCode.TextEditor.Gui.CompletionWindow;

using kkde.search;
using kkde.global;
using kkde.project;
using kkde.parse;
using kkde.parse.complate;
using System.IO;

namespace kkde.editor
{
	/// <summary>
	/// テキストエディタのメインクラス
	/// </summary>
	public partial class TextEditorEx : TextEditorControl
	{
		#region フィールド
		internal CodeCompletionWindow m_codeCompletionWindow = null;
		private TjsCompletionDataProvider m_tjsCompDataPrv = null;
		internal KagCompletionDataProvider m_kagCompDataPrv = null;
		private bool m_useCodeComp = false;
		private bool m_parseActionSaveFile = false;
		private int m_totalLength = -1;
		#endregion

		#region プロパティ
		/// <summary>
		/// 現在アクティブなTextAreaクラスのインスタンス
		/// </summary>
		public TextArea ActiveTextArea
		{
			get
			{
				return this.ActiveTextAreaControl.TextArea;
			}
		}

		/// <summary>
		/// KAG入力補完プロバイダを取得する
		/// </summary>
		internal KagCompletionDataProvider KagCompDataPrv
		{
			get
			{
				if (m_kagCompDataPrv == null)
				{
					m_kagCompDataPrv = new KagCompletionDataProvider();
				}
				return m_kagCompDataPrv;
			}
		}

		/// <summary>
		/// TJS入力補完プロバイダを取得する
		/// </summary>
		internal TjsCompletionDataProvider TjsCompDataPrv
		{
			get 
			{
				if (m_tjsCompDataPrv == null)
				{
					m_tjsCompDataPrv = new TjsCompletionDataProvider();
				}
				return m_tjsCompDataPrv; 
			}
		}

		/// <summary>
		/// 入力補完を行うかどうか
		/// </summary>
		public bool UseCodeCompletion
		{
			get { return m_useCodeComp; }
			set { m_useCodeComp = value; }
		}

		/// <summary>
		/// 構文解析をファイル保存時に行うかどうか
		/// trueのときファイル保存時
		/// falseのときキー入力時
		/// </summary>
		public bool ParseActionSaveFile
		{
			get { return m_parseActionSaveFile; }
			set { m_parseActionSaveFile = value; }
		}
		#endregion

		#region 初期化・終了メソッド
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public TextEditorEx()
		{
			InitializeComponent();

			//イベントの設定
			this.Document.BookmarkManager.Added += new BookmarkEventHandler(BookmarkManager_Added);
			this.Document.BookmarkManager.Removed += new BookmarkEventHandler(BookmarkManager_Removed);
			this.ActiveTextArea.KeyEventHandler += new ICSharpCode.TextEditor.KeyEventHandler(ActiveTextArea_KeyEventHandler);
			this.ActiveTextArea.KeyUp += new System.Windows.Forms.KeyEventHandler(ActiveTextArea_KeyUp);
			this.ActiveTextArea.Caret.PositionChanged += new EventHandler(Caret_PositionChanged);
			this.Document.FoldingManager.FoldingStrategy = new ParserFoldingStrategy();
			this.ActiveTextArea.ToolTipRequest += new ToolTipRequestEventHandler(ActiveTextArea_ToolTipRequest);
			this.ActiveTextArea.DragDrop += new DragEventHandler(ActiveTextArea_DragDrop);
			this.ActiveTextArea.DragEnter += new DragEventHandler(ActiveTextArea_DragEnter);

			editactions[Keys.Space | Keys.Control] = new CompletionAction();
			editactions[Keys.F1] = new HelpReferenceAction();
		}
		#endregion

		#region 編集関連メソッド
		/// <summary>
		/// 切り取り
		/// </summary>
		public void CutText()
		{
			IEditAction action = new Cut();
			action.Execute(ActiveTextArea);
		}

		/// <summary>
		/// コピー
		/// </summary>
		public void CopyText()
		{
			IEditAction action = new Copy();
			action.Execute(ActiveTextArea);
		}

		/// <summary>
		/// 貼り付け
		/// </summary>
		public void PasteText()
		{
			IEditAction action = new Paste();
			action.Execute(ActiveTextArea);
		}

		/// <summary>
		/// 削除
		/// </summary>
		public void DeleteText()
		{
			IEditAction action = new Delete();
			action.Execute(ActiveTextArea);
		}

		/// <summary>
		/// すべて選択
		/// </summary>
		public void SelectAllText()
		{
			IEditAction action = new SelectWholeDocument();
			action.Execute(ActiveTextArea);
		}

		/// <summary>
		/// 選択範囲のコメント状態を切り変える
		/// （コメント中はコメント解除、コメントではないときはコメント化する）
		/// </summary>
		public void ToggleComment()
		{
			IEditAction action = new ToggleComment();
			action.Execute(ActiveTextArea);
		}

		/// <summary>
		/// 折りたたみを切り替える（折りたたみ状態を反転させる）
		/// </summary>
		public void ToggleFolding()
		{
			IEditAction action = new ToggleFolding();
			action.Execute(ActiveTextArea);
		}

		/// <summary>
		/// すべての折り畳みを切り替える
		/// </summary>
		public void ToggleAllFoldings()
		{
			IEditAction action = new ToggleAllFoldings();
			action.Execute(ActiveTextArea);
		}

		/// <summary>
		/// 定義だけ表示する（折りたたむ）
		/// FoldTypeがMemberBodyまたは、Regionのもの
		/// </summary>
		public void ShowDefinitionsOnly()
		{
			IEditAction action = new ShowDefinitionsOnly();
			action.Execute(ActiveTextArea);
		}

		/// <summary>
		/// FoldTypeがRegionのものの折り畳む
		/// </summary>
		public void CloseRegionFlodings()
		{
//			IEditAction action = new CloseRegionFlodAction();
//			action.Execute(ActiveTextArea);
		}

		/// <summary>
		/// 選択開始の文字列位置から選択終了の文字列位置までを選択状態にする
		/// </summary>
		/// <param name="startOffset">選択開始の文字列位置</param>
		/// <param name="endOffset">選択終了の文字列位置</param>
		public void SelectText(int startOffset, int endOffset)
		{
			SelectText(ActiveTextArea.Document.OffsetToPosition(startOffset)
				, ActiveTextArea.Document.OffsetToPosition(endOffset));
		}

		/// <summary>
		/// 選択開始のCaret位置から選択終了のCaret位置までを選択状態にする
		/// </summary>
		/// <param name="startPos">選択開始のCaret位置</param>
		/// <param name="endPos">選択終了のCaret位置</param>
		public void SelectText(Point startPos, Point endPos)
		{
			ActiveTextArea.SelectionManager.ClearSelection();
			ActiveTextArea.AutoClearSelection = false;
			ActiveTextArea.SelectionManager.ExtendSelection(startPos, endPos);

			ActiveTextArea.Caret.Position = endPos;
		}

		/// <summary>
		/// 指定された行全体を選択状態にする
		/// </summary>
		/// <param name="lineNumber"></param>
		public void SelectText(int lineNumber)
		{
			LineSegment lineSeg = ActiveTextArea.Document.GetLineSegment(lineNumber);
			SelectText(lineSeg.Offset, lineSeg.Offset + lineSeg.Length);
		}

		/// <summary>
		/// lineNumberの位置へキャレットを移動させる
		/// </summary>
		/// <param name="lineNumber">行番号</param>
		public void MoveCaretLine(int lineNumber)
		{
			if (lineNumber < 0)
			{
				lineNumber = 0;	//最小値より小さいとき
			}
			else if (lineNumber > this.Document.LineSegmentCollection.Count)
			{
				lineNumber = this.Document.LineSegmentCollection.Count;	//最大値より大きいとき
			}
			ActiveTextArea.Caret.Position = new Point(0, lineNumber);
		}

		/// <summary>
		/// 指定行へ移動しその行のテキストを選択状態にする
		/// </summary>
		/// <param name="lineNumber">行番号</param>
		public void MoveCaretLineSelectText(int lineNumber)
		{
			MoveCaretLine(lineNumber);
			SelectText(lineNumber);
		}

		/// <summary>
		/// 指定した文字列を検索しその行のテキストを選択状態にする
		/// </summary>
		/// <param name="searchText">検索する文字列</param>
		public void MoveCaretLineSelectText(EditorSearchOption searchOption)
		{
			EditorSearchResult result = this.Search(searchOption);
			if (result != null && result.IsHit)
			{
				Point p = this.Document.OffsetToPosition(result.Offset);
				MoveCaretLine(p.Y);
				SelectText(p.Y);
			}
		}

		/// <summary>
		/// 現在選択しているテキストを取得する
		/// </summary>
		/// <returns></returns>
		public string GetSelectedText()
		{
			return this.ActiveTextArea.SelectionManager.SelectedText;
		}

		/// <summary>
		/// 一番最後の行へカーソルを移動する
		/// </summary>
		public void MoveCaretLastLine()
		{
			MoveCaretLine(this.Document.TotalNumberOfLines);
		}

		/// <summary>
		/// 指定した行の文字列を取得する
		/// </summary>
		/// <param name="lineNumber">指定する文字列の行番号</param>
		/// <returns>文字列</returns>
		public string GetLineText(int lineNumber)
		{
			LineSegment lineSeg = ActiveTextArea.Document.GetLineSegment(lineNumber);
			return this.Document.GetText(lineSeg);
		}

		/// <summary>
		/// 現在のカーソル位置の行文字列を取得する
		/// </summary>
		/// <returns></returns>
		public string GetSelectLineText()
		{
			return GetLineText(this.ActiveTextArea.Caret.Line);
		}

		/// <summary>
		/// 現在のカーソル位置にテキストを挿入する
		/// </summary>
		/// <param name="text"></param>
		public void InsertText(string text)
		{
			this.ActiveTextArea.InsertString(text);
		}

		/// <summary>
		/// 現在の選択状態を解除する
		/// </summary>
		public void ClearSelectedText()
		{
			if (this.ActiveTextArea.SelectionManager.IsSelected(this.ActiveTextArea.Caret.Offset))
			{
				ISelection sel = this.ActiveTextArea.SelectionManager.GetSelectionAt(this.ActiveTextArea.Caret.Offset);
				this.ActiveTextArea.SelectionManager.ClearSelection();
				if (sel.EndPosition != this.ActiveTextArea.Caret.Position)
				{
					//選択領域の後ろにカーソルを移動する
					this.ActiveTextArea.Caret.Position = sel.EndPosition;
				}
			}
		}

		/// <summary>
		/// 選択している範囲の上下に選択した文字列を挿入し囲む
		/// </summary>
		/// <param name="firstText"></param>
		/// <param name="lastText"></param>
		public void RegionSelectText(string firstText, string lastText)
		{
			string lf = Document.TextEditorProperties.LineTerminator;
			string insertText = firstText + lf + GetSelectedText() + lf + lastText;
			InsertText(insertText);
		}
		#endregion

		#region コメント化関連
		/// <summary>
		/// コメント化する
		/// </summary>
		public void DoCommentOut()
		{
			if (ActiveTextArea.SelectionManager.SelectedText == String.Empty)
			{
				//現在のカーソル位置で何も選択していないときは行全体を選択状態にする
				MoveCaretLineSelectText(this.ActiveTextArea.Caret.Line);
			}

			if (ActiveTextArea.SelectionManager.SelectionCollection.Count > 0)
			{
				ISelection selection = ActiveTextArea.SelectionManager.SelectionCollection[0];
				switch (FileType.GetKrkrType(FileName))
				{
					case FileType.KrkrType.Kag:
						doCommentOutKagText(selection);
						break;
					case FileType.KrkrType.Tjs:
						doCommentOutTjsText(selection);
						break;
				}
			}
		}

		/// <summary>
		/// KAGの文字列をコメント化する（先頭行に";"を挿入する）
		/// </summary>
		/// <param name="selection">選択状態</param>
		private void doCommentOutKagText(ISelection selection)
		{
			doCommentOutInsertPrefix(selection, ";");
		}

		/// <summary>
		/// TJSの文字列をコメント化する（先頭行に"//"を挿入する）
		/// </summary>
		/// <param name="selection">選択状態</param>
		private void doCommentOutTjsText(ISelection selection)
		{
			doCommentOutInsertPrefix(selection, "//");
		}

		/// <summary>
		/// 文字列の先頭に接頭語をつける
		/// </summary>
		/// <param name="selection">選択状態</param>
		/// <param name="prefix">接頭語（行頭につける文字列）</param>
		private void doCommentOutInsertPrefix(ISelection selection, string prefix)
		{
			for (int i = selection.StartPosition.Y; i <= selection.EndPosition.Y; i++)
			{
				LineSegment lineSeg = Document.GetLineSegment(i);
				Document.Insert(lineSeg.Offset, prefix);
			}
		}

		/// <summary>
		/// コメントを解除する
		/// </summary>
		public void DoUncommentOut()
		{
			if (ActiveTextArea.SelectionManager.SelectedText == String.Empty)
			{
				//現在のカーソル位置で何も選択していないときは行全体を選択状態にする
				MoveCaretLineSelectText(this.ActiveTextArea.Caret.Line);
			}

			if (ActiveTextArea.SelectionManager.SelectionCollection.Count > 0)
			{
				ISelection selection = ActiveTextArea.SelectionManager.SelectionCollection[0];
				switch (FileType.GetKrkrType(FileName))
				{
					case FileType.KrkrType.Kag:
						doUncommentOutKagText(selection);
						break;
					case FileType.KrkrType.Tjs:
						doUncommentOutTjsText(selection);
						break;
				}
			}
		}

		/// <summary>
		/// KAG文字列のコメントを解除する（先頭行の";"を削除する）
		/// </summary>
		/// <param name="selection">選択状態</param>
		private void doUncommentOutKagText(ISelection selection)
		{
			doUncommentOutDeletePrefix(selection, ";");
		}

		/// <summary>
		/// TJS文字列のコメントを解除する（先頭行の"//"を削除する）
		/// </summary>
		/// <param name="selection">選択状態</param>
		private void doUncommentOutTjsText(ISelection selection)
		{
			doUncommentOutDeletePrefix(selection, "//");
		}

		/// <summary>
		/// 文字列先頭の接頭語を削除する
		/// </summary>
		/// <param name="selection">選択状態</param>
		/// <param name="prefix">接頭語</param>
		private void doUncommentOutDeletePrefix(ISelection selection, string prefix)
		{
			for (int i = selection.StartPosition.Y; i <= selection.EndPosition.Y; i++)
			{
				LineSegment lineSeg = Document.GetLineSegment(i);
				
				if (Document.GetText(lineSeg).StartsWith(prefix))
				{
					Document.Remove(lineSeg.Offset, prefix.Length);
				}
			}
		}
		#endregion

		#region 検索関連
		/// <summary>
		/// 指定した検索を行い、見つかった文字列を選択状態にする
		/// </summary>
		/// <param name="option">検索するオプション</param>
		/// <returns>見つかった位置</returns>
		public EditorSearchResult Search(EditorSearchOption option)
		{
			return Search(option, true);
		}

		/// <summary>
		/// 指定した検索を行い、見つかった文字列を選択状態にする
		/// </summary>
		/// <param name="option">検索するオプション</param>
		/// <param name="showErrorMsg">エラーメッセージを表示するかどうか（trueかつオプションでもtrueのとき表示する）</param>
		/// <returns>見つかった位置</returns>
		public EditorSearchResult Search(EditorSearchOption option, bool showErrorMsg)
		{
			Point caretBackup = this.ActiveTextArea.Caret.Position;
			if (option.HeadStart)
			{
				this.ActiveTextArea.Caret.Position = new Point(0, 0);	//先頭へ移動
			}
			int offset = this.ActiveTextArea.Caret.Offset;
			if (this.ActiveTextArea.SelectionManager.IsSelected(offset))
			{
				ISelection sel = this.ActiveTextArea.SelectionManager.GetSelectionAt(offset);

				//選択しているときはカーソル位置をそれぞれ次を探せる位置へカーソルを移動する
				switch (option.Direction)
				{
					case SearchDirection.Up:
						this.ActiveTextArea.Caret.Position = sel.StartPosition;
						break;
					case SearchDirection.Down:
						this.ActiveTextArea.Caret.Position = sel.EndPosition;
						break;
				}

				this.ActiveTextArea.SelectionManager.ClearSelection();	//選択を解除する
				offset = this.ActiveTextArea.Caret.Offset;	//新しいオフセット位置をセットする
			}

			//検索開始
			EditorSearchResult result = null;
			try
			{
				result = util.TextUtil.SearchNext(option, offset, this.Document.TextBufferStrategy);
			}
			catch (Exception err)
			{
				util.Msgbox.Error("検索キーワードが正しくありません\n" + err.Message);
			}

			//見つからなかったときの処理
			if (result == null || result.IsHit == false)
			{
				//見つからなかったときは何もしない
				if (showErrorMsg && global.GlobalStatus.EnvOption.SearchResultShowDiaglog)
				{
					String text = "";
					if (option.Direction == SearchDirection.Up)
					{
						text = "上方向";
					}
					else
					{
						text = "下方向";
					}
					text += "に検索文字列が見つかりませんでした";
					util.Msgbox.Info(text);
				}

				if (option.HeadStart)
				{
					this.ActiveTextArea.Caret.Position = caretBackup;	//見つからなかったときは選択位置を元に戻す
				}
				return null;
			}

			//ヒットした文字列を選択状態にする
			this.SelectText(result.Offset, result.Offset + result.Length);
			return result;
		}

		public void MarkSearchString(EditorSearchOption option, ITextBufferStrategy buffer)
		{
			int offset = 0;
			EditorSearchResult result = null;
			while (true)
			{
				result = util.TextUtil.SearchNext(option, offset, buffer);
				if (result == null || result.IsHit == false)
				{
					//ループを抜ける
					break;
				}

				this.Document.MarkerStrategy.AddMarker(new TextMarker(result.Offset, result.Length, TextMarkerType.SolidBlock, Color.Red));
				offset = result.Offset + result.Length;
			}

			//更新
			this.Refresh();
		}

		public void HightrightSearchString(EditorSearchOption option)
		{
		}

		/// <summary>
		/// 現在選択している文字列を置換する
		/// </summary>
		/// <param name="option">置換するオプション情報</param>
		/// <returns>置換したときはtrue</returns>
		public bool ReplaceSelectedText(EditorSearchOption option)
		{
			int offset = this.ActiveTextArea.Caret.Offset;
			if (this.ActiveTextArea.SelectionManager.IsSelected(offset) == false)
			{
				//選択されていないときは何もしない
				return false;
			}

			ISelection sel = this.ActiveTextArea.SelectionManager.GetSelectionAt(offset);
			this.Document.Replace(sel.Offset, sel.Length, option.ReplaceKeyword);	//置換する
			this.ActiveTextArea.Caret.Position = this.Document.OffsetToPosition(sel.Offset + option.ReplaceKeyword.Length);	//カーソル位置をセットする
			this.ActiveTextArea.SelectionManager.ClearSelection();					//選択解除
			return true;
		}
		#endregion

		#region ブックマーク関連
		/// <summary>
		/// ブックマークを追加したときのイベント
		/// </summary>
		void BookmarkManager_Added(object sender, BookmarkEventArgs e)
		{
			//リストにブックマークを追加する
			GlobalStatus.FormManager.BookmarkListForm.AddBookmark(e.Bookmark.LineNumber + 1, this.FileName);
		}

		/// <summary>
		/// ブックマークを削除したときのイベント
		/// </summary>
		void BookmarkManager_Removed(object sender, BookmarkEventArgs e)
		{
			//リストからブックマークを削除する
			GlobalStatus.FormManager.BookmarkListForm.RemoveBookmark(e.Bookmark.LineNumber + 1, this.FileName);
		}

		/// <summary>
		/// 指定された行をブックマーク登録する
		/// すでに登録されているときは解除する
		/// </summary>
		/// <param name="lineNumber">行番号</param>
		public void ToggleBookmark(int lineNumber)
		{
			//ブックマークを登録（すでに登録済みの場合は解除する
			this.Document.BookmarkManager.ToggleMarkAt(lineNumber);
			this.Refresh();
		}

		/// <summary>
		/// 現在のカーソル行をブックマーク登録する
		/// </summary>
		public void ToggleBookmarkSelectedLine()
		{
			ToggleBookmark(this.ActiveTextArea.Caret.Line);
		}

		/// <summary>
		/// ブックマークをすべてクリアーする
		/// </summary>
		public void ClearAllBookmark()
		{
			this.Document.BookmarkManager.Clear();
			this.Refresh();
		}

		/// <summary>
		/// ブックマークをリストから取得する
		/// </summary>
		public void LoadBookmarkFormList()
		{
			//リストからブックマークを削除する
			BookmarkInfo[] bookmarkList = GlobalStatus.FormManager.BookmarkListForm.GetBookmarkList();
			foreach (BookmarkInfo bookmark in bookmarkList)
			{
				if (this.FileName == bookmark.FilePath)
				{
					ToggleBookmark(bookmark.LineNumber - 1);
				}
			}
		}

		/// <summary>
		/// 次のブックマークへカーソルを移動する
		/// </summary>
		public void MoveNextBookmark()
		{
			Bookmark bookmark = this.Document.BookmarkManager.GetNextMark(this.ActiveTextArea.Caret.Line);
			if (bookmark == null)
			{
				return;	//ブックマークが取得できなかった
			}
			MoveCaretLine(bookmark.LineNumber);
		}

		/// <summary>
		/// 前のブックマークへカーソルを移動する
		/// </summary>
		public void MovePrevBookmark()
		{
			Bookmark bookmark = this.Document.BookmarkManager.GetPrevMark(this.ActiveTextArea.Caret.Line);
			if (bookmark == null)
			{
				return;	//ブックマークが取得できなかった
			}
			MoveCaretLine(bookmark.LineNumber);
		}

		/// <summary>
		/// 対応する括弧へ移動
		/// </summary>
		public void MoveMatchingKakko()
		{
			IEditAction action = new GotoMatchingBrace();
			action.Execute(ActiveTextArea);
		}
		#endregion

		#region 入力補完関連
		/// <summary>
		/// キー入力があったとき
		/// </summary>
		/// <param name="ch">入力された文字</param>
		/// <returns>入力をキャンセルするときはtrue</returns>
		bool ActiveTextArea_KeyEventHandler(char ch)
		{
			if (m_useCodeComp)	//入力補完を行うとき
			{
				FileType.KrkrType type = FileType.GetKrkrType(this.FileName);
				if (m_codeCompletionWindow != null && !m_codeCompletionWindow.IsDisposed)
				{
					if (type == FileType.KrkrType.Kag && (ch == ' ' || ch == '='))
					{
						//次の入力補完へ移行させるためここでは何もしない
					}
					else if (type == FileType.KrkrType.Kag && ch == ']')
					{
						//タグ入力が終了したので入力補完を終了させる
						m_codeCompletionWindow.Close();
						return false;
					}
					else
					{
						//入力補完リストにイベントを投げる
						return m_codeCompletionWindow.ProcessKeyEvent(ch);
					}
				}

				switch (type)
				{
					case FileType.KrkrType.Kag:
						kagKeyEventHandler(ch);
						break;
					case FileType.KrkrType.Tjs:
						tjsKeyEventHandler(ch);
						break;
				}
			}

			return false;
		}

		/// <summary>
		/// TJSファイルでキー入力イベントが発生したとき
		/// </summary>
		/// <param name="ch"></param>
		private void tjsKeyEventHandler(char ch)
		{
			switch (ch)
			{
				case '.':
					//メソッド呼び出しの時
					//m_codeCompletionWindow = CodeCompletionWindow.ShowCompletionWindow(GlobalStatus.FormManager.MainForm
					//	, this, this.FileName, TjsCompDataPrv, ch);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// KAGファイルでキー入力イベントが発生したとき
		/// </summary>
		/// <param name="ch"></param>
		private void kagKeyEventHandler(char ch)
		{
			switch (ch)
			{
				case '[':	//タグ呼び出しの時
				case '@':	//タグ呼び出しの時
				case ' ':	//属性名呼び出しの時
				case '=':	//属性値呼び出しの時
					m_codeCompletionWindow = CodeCompletionWindow.ShowCompletionWindow(GlobalStatus.FormManager.MainForm
						, this, this.FileName, KagCompDataPrv, ch);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// 折りたたみ状態を更新する
		/// </summary>
		public void UpdateFolding()
		{
			if (this.TextEditorProperties.EnableFolding == false)
			{
				return;	//折りたたみが無効なので何もしない
			}

			switch (FileType.GetKrkrType(this.FileName))
			{
				case FileType.KrkrType.Kag:
					//Debug.WriteLine("折りたたみ開始");
					this.Document.FoldingManager.UpdateFoldings(this.FileName
						, new KagFoldingInfo(GlobalStatus.ParserSrv.getRegionList(this.FileName)
						, GlobalStatus.ParserSrv.GetLabelList(this.FileName)));
					break;
				case FileType.KrkrType.Tjs:
					break;
			}
		}

		/// <summary>
		/// キーを押し上げたとき
		/// </summary>
		void ActiveTextArea_KeyUp(object sender, KeyEventArgs e)
		{
		}

		/// <summary>
		/// カーソルの位置が変わったとき
		/// </summary>
		void Caret_PositionChanged(object sender, EventArgs e)
		{
			if (m_totalLength != this.Document.TextLength)
			{
				//挿入または削除があったとき
				m_totalLength = this.Document.TextLength;
				if (m_parseActionSaveFile == false)	//セーブ時ではないとき
				{
					ParseFile();
				}
			}

			//表示更新
			if (GlobalStatus.FormManager.MainForm != null)
			{
				GlobalStatus.FormManager.MainForm.UpdateStatusBarCaretPos(this);
			}
		}

		/// <summary>
		/// ファイル内容を構文解析しデータを格納する
		/// </summary>
		public void ParseFile()
		{
			if (m_useCodeComp)	
			{
				switch (FileType.GetKrkrType(this.FileName))
				{
					case FileType.KrkrType.Kag:
					case FileType.KrkrType.Tjs:
						//構文解析対応ファイルの時
						GlobalStatus.ParserSrv.ParseFile(this.FileName);
						break;
				}
			}
		}
		#endregion

		#region ツールチップ関連
		/// <summary>
		/// ツールチップ表示リクエストが発生したとき
		/// </summary>
		void ActiveTextArea_ToolTipRequest(object sender, ToolTipRequestEventArgs e)
		{
			if (m_useCodeComp == false)
			{
				return;	//入力補完が無効なときはTipは表示しない
			}
			if (e.ToolTipShown)
			{
				return;	//Tipが表示中は新たに表示しない
			}
			if (e.InDocument == false)
			{
				return;
			}

			string tip = "";
			switch (FileType.GetKrkrType(this.FileName))
			{
				case FileType.KrkrType.Kag:
					tip = KagToolTip.GetText(Document, e.LogicalPosition.Y, e.LogicalPosition.X);
					break;
				case FileType.KrkrType.Tjs:
					//未実装
					break;
			}

			if (tip != "")
			{
				e.ShowToolTip(tip);
			}
		}
		#endregion

		#region ドラッグ・ドロップ関連
		/// <summary>
		/// ドラッグされたとき
		/// </summary>
		void ActiveTextArea_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))	//ドロップされたのがファイルの時
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

		/// <summary>
		/// ドロップしたとき
		/// </summary>
		void ActiveTextArea_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))	//ファイルの時
			{
				//ドロップされたファイル名リストを取得する
				string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
				foreach (string fileName in fileList)
				{
					GlobalStatus.EditorManager.LoadFile(fileName);	//エディタを開く
				}
			}
		}
		#endregion

		#region ジャンプ関連
		/// <summary>
		/// 現在のカーソル位置からシナリオファイル・ラベルの指定が存在するときはジャンプする
		/// </summary>
		public void JumpKagScenarioLabelFromCaret()
		{
			switch (FileType.GetKrkrType(this.FileName))
			{
				case FileType.KrkrType.Kag:	//KAGファイルのとき
					//カーソル位置のKAGタグ情報を取得する
					int colNum = GetKagTagEndFormCaret();
					KagTagKindInfo info = KagUtility.GetTagKind(Document, this.ActiveTextArea.Caret.Line, colNum);
					if (info == null)
					{
						util.Msgbox.Error("タグ・マクロのある位置にカーソルが無いため移動できません");
						return;
					}

					//ジャンプ先情報をKLAGタグ情報から取得する
					string storage;
					string target;
					info.AttrTable.TryGetValue("storage", out storage);
					info.AttrTable.TryGetValue("target", out target);
					if (storage == null && target == null)
					{
						Debug.WriteLine("ジャンプ指定（storage or target）がない");
						util.Msgbox.Error("ジャンプ指定属性が無いため移動できません");
						return;
					}

					//ファイルのフルパスを取得
					if (storage == null)
					{
						storage = this.FileName;
					}
					else
					{
						if (GlobalStatus.Project == null || Directory.Exists(GlobalStatus.Project.DataFullPath) == false)
						{
							return;	//検索パスが不明
						}
						string[] files = Directory.GetFiles(GlobalStatus.Project.DataFullPath, storage, SearchOption.AllDirectories);
						if (files == null || files.Length == 0)
						{
							util.Msgbox.Error(storage + "が見つからないため移動できません");
							return;	//ファイルが見つからない
						}

						storage = files[0];	//見つかったファイルパスをセットする
					}

					//ラベルをセットしファイルを開く
					if (target == null)
					{
						GlobalStatus.EditorManager.LoadFile(storage);	//ファイルを開くだけ
					}
					else
					{
						//ラベルを検索するようにファイルを開く
						EditorSearchOption searchOption = new EditorSearchOption();
						searchOption.HeadStart = true;
						searchOption.Direction = SearchDirection.Down;
						searchOption.Regex = true;
						searchOption.SearchKeywordTable.Add("^\\t*\\" + target);
						GlobalStatus.EditorManager.LoadFile(storage, searchOption);
					}

					Debug.WriteLine(String.Format("JumpScenarioLabelFromCaret: storage={0}, target={1}", storage, target));			
					break;
			}
		}

		/// <summary>
		/// 現在のカーソル位置からKAGタグの終わり位置を検索し返す
		/// </summary>
		/// <returns>KAGタグの終わり（']'または行末）</returns>
		public int GetKagTagEndFormCaret()
		{
			int lineNum = this.ActiveTextArea.Caret.Line;
			int colNum = this.ActiveTextArea.Caret.Column;
			LineSegment lineSeg = this.Document.GetLineSegment(lineNum);
			if (lineSeg == null)
			{
				return -1;
			}
			string lineText = this.Document.GetText(lineSeg);
			int i;
			for (i = colNum; i < lineSeg.Length; i++)
			{
				if (lineText[i] == ']')
				{
					break;
				}
			}
			colNum = i;

			return colNum;
		}

		/// <summary>
		/// 現在のカーソル位置からKAGタグ・マクロ/TJSクラス・関数の定義場所へ遷移する
		/// </summary>
		public void JumpDefineFuncFromCaret()
		{
			switch (FileType.GetKrkrType(this.FileName))
			{
				//KAGファイルの時はタグ・マクロ定義元へジャンプする
				case FileType.KrkrType.Kag:
					//カーソル位置のKAGタグ情報を取得する
					int colNum = GetKagTagEndFormCaret();
					KagTagKindInfo info = KagUtility.GetTagKind(Document, this.ActiveTextArea.Caret.Line, colNum);
					if (info == null)
					{
						util.Msgbox.Error("タグ・マクロのある位置にカーソルが無いため定義へ移動できません");
						return;
					}

					//KAGタグ情報からマクロ情報を取得する
					KagMacro macro = KagUtility.GetKagMacro(info.TagName);
					if (macro == null)
					{
						//タグ情報が取得できなかった
						util.Msgbox.Error("タグ・マクロの情報が取得できませんでした");
						return;
					}

					GlobalStatus.EditorManager.LoadFile(macro.FilePath, macro.LineNumber);
					break;
				//TJSファイルの時は関数・クラス定義元へジャンプする
				case FileType.KrkrType.Tjs:
					//未実装
					break;
			}
		}

		/// <summary>
		/// 現在のカーソル位置にある定義（KAGのときはタグ・マクロ、TJSのときはクラス・関数）をGrep検索する
		/// </summary>
		public void SearchGrepReferenceDefineFromCaret()
		{
			switch (FileType.GetKrkrType(this.FileName))
			{
				//KAGファイルの時はタグ・マクロ定義をGrep検索する
				case FileType.KrkrType.Kag:
					//カーソル位置のKAGタグ情報を取得する
					int colNum = GetKagTagEndFormCaret();
					KagTagKindInfo info = KagUtility.GetTagKind(Document, this.ActiveTextArea.Caret.Line, colNum);
					if (info == null)
					{
						util.Msgbox.Error("タグ・マクロのある位置にカーソルが無いため検索できません");
						return;
					}

					string searchText = "";
					searchText = String.Format("\\[{0}|^\\t*@{0}", info.TagName);

					//Grep検索用オプションをセットする
					EditorSearchOption searchOption = new EditorSearchOption();
					searchOption.SearchKeywordTable.Add(searchText);
					searchOption.Regex = true;
					searchOption.Type = SearchType.Grep;
					searchOption.GrepOption.Pos = GrepPotision.Project;
					searchOption.GrepOption.FileExtTable.Add("*.ks");
					searchOption.GrepOption.UseSubFolder = true;
					GlobalStatus.FormManager.SearchResultForm.Show(GlobalStatus.FormManager.MainForm.MainDockPanel);
					GlobalStatus.FormManager.SearchResultForm.Grep(searchOption);	//検索開始
					break;
				//TJSファイルの時は関数・クラス定義をGrep検索する
				case FileType.KrkrType.Tjs:
					//未実装
					break;
			}
		}
		#endregion

	}
}
