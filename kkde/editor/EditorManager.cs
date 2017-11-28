using System;
using System.Collections.Generic;
using System.Text;
using WeifenLuo.WinFormsUI;
using System.IO;
using System.Windows.Forms;
using kkde.global;
using ICSharpCode.TextEditor.Document;
using System.Diagnostics;
using kkde.project;
using kkde.search;
using kkde.option;
using kkde.screen;

namespace kkde.editor
{
	/// <summary>
	/// エディタフォームをまとめて管理するクラス
	/// </summary>
	public class EditorManager
	{
		#region フィールド
		/// <summary>
		/// メインのドッキングパネル
		/// このパネルにエディタをくっつける
		/// </summary>
		DockPanel m_mainPanel;

		CompletionImageList m_imageList;
		#endregion

		#region プロパティ
		/// <summary>
		/// 現在アクティブになっているエディタフォームのオブジェクトを返す
		/// </summary>
		public EditorForm ActiveEditorForm
		{
			get
			{
				if (m_mainPanel.ActiveDocument == null)
				{
					return null;
				}
				if (m_mainPanel.ActiveDocument is EditorForm)
				{
					return (EditorForm)m_mainPanel.ActiveDocument;
				}

				return null;
			}
		}

		/// <summary>
		/// 現在アクティブになっているテキストエディタのオブジェクトを返す
		/// </summary>
		public TextEditorEx ActiveEditor
		{
			get
			{
				if (m_mainPanel.ActiveDocument == null)
				{
					return null;
				}
				if (m_mainPanel.ActiveDocument is EditorForm)
				{
					return ((EditorForm)m_mainPanel.ActiveDocument).TextEditor;
				}

				return null;
			}
		}

		/// <summary>
		/// 現在アクティブになっているドキュメントコンテンツのオブジェクトを返す
		/// </summary>
		public IEditorDocContent ActiveDocument
		{
			get
			{
				if (m_mainPanel.ActiveDocument == null)
				{
					return null;
				}
				if (m_mainPanel.ActiveDocument is IEditorDocContent)
				{
					return ((IEditorDocContent)m_mainPanel.ActiveDocument);
				}

				return null;
			}
		}

		/// <summary>
		/// 入力補完用イメージリスト
		/// </summary>
		public CompletionImageList CompImageList
		{
			get { return m_imageList; }
			set { m_imageList = value; }
		}
		#endregion

		#region 初期化メソッド
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="dockPanel">エディタをくっつけるドッキングパネル</param>
		public EditorManager(DockPanel dockPanel)
		{
			m_mainPanel = dockPanel;
		}
		#endregion

		#region エディタファイル操作関連メソッド
		/// <summary>
		/// エディタフォームを新規に作成して返す
		/// </summary>
		/// <returns>新規作成フォーム</returns>
		private EditorForm createEditorForm()
		{
			EditorForm editor = new EditorForm();
			editor.FormClosing += new FormClosingEventHandler(editor_FormClosing);

			return editor;
		}

		/// <summary>
		/// エディタフォームを閉じようとするときのイベント
		/// </summary>
		private void editor_FormClosing(object sender, FormClosingEventArgs e)
		{
			//変更されているとき
			EditorForm editor = (EditorForm)sender;
			if (editor.IsTextChanged)
			{
				//保存するかどうかのダイアログを表示する
				string msg = String.Format("{0}は更新されています。\n保存しますか？", editor.TabText);
				DialogResult saveFlag = MessageBox.Show(msg, "保存", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
				switch (saveFlag)
				{
					case DialogResult.Yes:
						SaveFile();
						break;
					case DialogResult.No:
						/* 何もせずに閉じる */
						break;
					case DialogResult.Cancel:
					default:
						//閉じる処理をキャンセルする
						e.Cancel = true;
						break;
				}
			}
		}

		/// <summary>
		/// 新しいエディタを作成する
		/// </summary>
		public void CreateNew()
		{
			EditorForm editor = createEditorForm();

			SetOption(editor.TextEditor);
			editor.Show(m_mainPanel);
		}

		/// <summary>
		/// ファイルをダイアログから選択しエディタに開く
		/// </summary>
		public void OpenFile()
		{
			//ファイルを開くためにファイルオープンダイアログを表示する
			OpenFileDialog openDialog = new OpenFileDialog();
			openDialog.Title = "ファイルを開く";
			openDialog.Filter = "KAGシナリオファイル (*.ks)|*.ks|TJSファイル (*.tjs)|*.tjs|KKDEスクリーンファイル (*.kui)|*.kui|テキストファイル (*.txt)|*.txt|すべてのファイル (*.*)|*.*";
			openDialog.InitialDirectory = GlobalStatus.Project.DirPath;
			if (openDialog.ShowDialog() == DialogResult.OK)
			{
				this.LoadFile(openDialog.FileName);
			}
		}

		private void ConvertEncoding(string fileName)
		{
			GetEncoding(fileName);
		}

		/// <summary>
		/// ファイルを読み込んで文字コードを返す
		/// </summary>
		/// <param name="fileName">ファイル名</param>
		/// <returns>文字コード</returns>
		private System.Text.Encoding GetEncoding(string fileName)
		{
			System.Text.Encoding enc = GlobalStatus.TextEnc.GetFileEncoding(fileName);
			if (enc != System.Text.Encoding.Unicode && enc != System.Text.Encoding.GetEncoding("shift_jis"))	//吉里吉里で扱える文字コードか？
			{
				EditorOption option = this.GetEditorOption(fileName);	//変換するべきエンコードを取得する

				if (MessageBox.Show(String.Format("今開こうとしたファイルは {0} で保存されています。"
												+ "\n吉里吉里で読み込めるように {1} に変換しますか？"
					, enc.EncodingName, option.Encoding.EncodingName)
					, "情報", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
				{
					//設定されたエンコード（ShiftJIS又はUnicode）に変換し保存する
					GlobalStatus.TextEnc.SaveToFile(fileName, option.Encoding.WebName);
					enc = GlobalStatus.TextEnc.GetFileEncoding(fileName);
				}
			}

			return enc;
		}

		/// <summary>
		/// ファイルを開く
		/// </summary>
		/// <param name="filePath">開くファイルのパス</param>
		public IEditorDocContent LoadFile(string filePath)
		{
			if (File.Exists(filePath) == false)
			{
				util.Msgbox.Error(filePath + "\nファイルが見つかりませんでした。");
				return null;
			}
			GlobalStatus.EnvOption.HistoryFile.Add(filePath);	//履歴に追加する

			//ドッキングウィンドウからエディタすべて探索する
			IDockContent[] docList = m_mainPanel.Documents;
			foreach (IDockContent doc in docList)
			{
				if (doc is IEditorDocContent)
				{
					if (filePath == ((IEditorDocContent)doc).FileName)
					{
						//すでに開いているときはファイルを読み込まずにそれをアクティブにする
						((IEditorDocContent)doc).ActivateForm();
						return (IEditorDocContent)doc;
					}
				}
			}

			//タブの生成と読み込み
			IEditorDocContent nweDoc = null;
			if (FileType.GetKrkrType(filePath) == FileType.KrkrType.Screen)
			{
				ScreenMakerForm screen = new ScreenMakerForm();
				screen.LoadFile(filePath);
				screen.Show(m_mainPanel);

				nweDoc = screen;
			}
			else
			{
				EditorForm editor = createEditorForm();
				ConvertEncoding(filePath);					//文字コード調整
				editor.LoadFile(filePath);					//ファイル読み込み
				SetOption(editor.TextEditor);				//エディタオプションのセット
				editor.TextEditor.LoadBookmarkFormList();	//ブックマークのセット
				editor.Show(m_mainPanel);					//表示する
				GlobalStatus.FormManager.MainForm.UpdateStatusBar(editor.TextEditor);
				editor.TextEditor.UpdateFolding();			//折りたたみのセット
				editor.TextEditor.ActiveTextArea.Focus();	//フォーカスをセットする

				nweDoc = editor;
			}

			return nweDoc;
		}

		/// <summary>
		/// ファイルを読み込み指定行へカーソルを移動する
		/// </summary>
		/// <param name="filePath"></param>
		/// <param name="lineNumber"></param>
		/// <returns></returns>
		public IEditorDocContent LoadFile(string filePath, int lineNumber)
		{
			if (FileType.GetKrkrType(filePath) == FileType.KrkrType.Screen)
			{
				//指定行へ移動できないためnullを返す
				return null;
			}

			EditorForm editor = (EditorForm)LoadFile(filePath);
			if (editor == null)
			{
				return null;
			}

			editor.TextEditor.MoveCaretLineSelectText(lineNumber);
			return editor;
		}

		/// <summary>
		/// 検索情報を指定しカーソルを移動する
		/// </summary>
		/// <param name="filePath"></param>
		/// <param name="searchOption"></param>
		/// <returns></returns>
		public IEditorDocContent LoadFile(string filePath, EditorSearchOption searchOption)
		{
			if (FileType.GetKrkrType(filePath) == FileType.KrkrType.Screen)
			{
				//指定行へ移動できないためnullを返す
				return null;
			}

			EditorForm editor = (EditorForm)LoadFile(filePath);
			if (editor == null)
			{
				return null;
			}

			//editor.TextEditor.MoveCaretLineSelectText(lineNumber);
			editor.TextEditor.MoveCaretLineSelectText(searchOption);
			return editor;
		}

		/// <summary>
		/// ファイルを保存する
		/// </summary>
		/// <param name="editor">保存するエディタ</param>
		/// <param name="fileName">ファイル名</param>
		public void SaveFile(IEditorDocContent editor, string fileName)
		{
			editor.SaveFile(fileName);				//保存する
			if (editor is EditorForm)
			{
				((EditorForm)editor).TextEditor.ParseFile();			//構文解析開始
			}
		}

		/// <summary>
		/// 上書き保存をする
		/// </summary>
		public void SaveFile()
		{
			SaveFile(ActiveEditorForm);
		}

		/// <summary>
		/// 上書き保存する（保存エディタ指定）
		/// </summary>
		/// <param name="editor">保存するエディタオブジェクト</param>
		public void SaveFile(IEditorDocContent editor)
		{
			if (editor == null)
			{
				//保存するエディタがないので何もしない
				return;
			}

			Debug.WriteLine("SaveFile: FileName=" + editor.FileName);
			if (File.Exists(editor.FileName))
			{
				//ファイルが存在するため上書き保存する
				this.SaveFile(editor, editor.FileName);
			}
			else
			{
				//ファイルがまだ作成されていないため、名前をつけて保存する
				this.SaveFileAs(editor);
			}
		}

		/// <summary>
		/// 名前をつけて保存をする
		/// </summary>
		public void SaveFileAs()
		{
			if (ActiveEditorForm == null)
			{
				return;	//何もしない
			}

			this.SaveFileAs(ActiveEditorForm);
		}

		/// <summary>
		/// 名前をつけて保存する（保存エディタ指定）
		/// </summary>
		/// <param name="editor">保存するエディタオブジェクト</param>
		public void SaveFileAs(IEditorDocContent editor)
		{
			//保存先選択ダイアログを表示する
			SaveFileDialog saveDialog = new SaveFileDialog();
			saveDialog.Title = "名前をつけて保存";
			saveDialog.Filter = "KAGシナリオファイル (*.ks)|*.ks|TJSファイル (*.tjs)|*.tjs|テキストファイル (*.txt)|*.txt|すべてのファイル (*.*)|*.*";
			saveDialog.InitialDirectory = GlobalStatus.Project.DirPath;
			if (saveDialog.ShowDialog() == DialogResult.OK)
			{
				//指定したパスで保存する
				this.SaveFile(editor, saveDialog.FileName);
				if (editor is EditorForm)
				{
					SetOption(((EditorForm)editor).TextEditor);				//エディタオプションのセット
				}
			}
		}

		/// <summary>
		/// すべてのエディタを保存する
		/// </summary>
		public void SaveFileAll()
		{
			if (ActiveEditorForm == null)
			{
				return;	//何もしない
			}

			//現在のドキュメントを保持する
			DockContent startDoc = (DockContent)m_mainPanel.ActiveDocument;

			//ドッキングウィンドウからエディタを検索する
			IDockContent[] docList = m_mainPanel.Documents;
			foreach (IDockContent doc in docList)
			{
				if (doc is IEditorDocContent)
				{
					//上書き保存する
					((IEditorDocContent)doc).ActivateForm();
					this.SaveFile(((IEditorDocContent)doc));
				}
			}

			//アクティブなエディタを元に戻す
			startDoc.Activate();
		}

		/// <summary>
		/// エディタを閉じる
		/// 未保存の時は保存するかどうかのダイアログを表示する
		/// エディタを閉じるのに成功したらtrueを返す
		/// </summary>
		public bool CloseFile()
		{
			IEditorDocContent editor = ActiveDocument;
			if (editor == null)
			{
				//閉じるエディタがないので何もせずに終了
				return true;
			}

			//現在アクティブなエディタの位置を取得する
			int docLen = 0;
			IDockContent[] docList = m_mainPanel.Documents;
			int activeEditorIndex = 0;
			for (int i = 0; i < docList.Length; i++)
			{
				if (editor == docList[i])
				{
					activeEditorIndex = i;
				}
				if (docList[i] is IEditorDocContent)
				{
					docLen++;
				}
			}

			//閉じる
			editor.CloseForm();
			if (editor.IsClosed == true)
			{
				if (docLen > 1)	//自分とこれからアクティブにする個数分だけ存在するとき
				{
					//閉じるエディタの一つ前の位置をアクティブにする
					int index = 0;
					if (activeEditorIndex == 0)
					{
						//前がいないので一つ後ろを指定する
						index = 1;
					}
					else
					{
						//一つ前を指定する
						index = activeEditorIndex - 1;
					}
					((IEditorDocContent)docList[index]).ActivateForm();
				}
			}

			return editor.IsClosed;
		}

		/// <summary>
		/// すべてのエディタを閉じる
		/// すべて閉じたらtrueを返す
		/// </summary>
		public bool CloseFileAll()
		{
			//ドッキングウィンドウからエディタを検索する
			IDockContent[] docList = m_mainPanel.Documents;
			foreach (IDockContent doc in docList)
			{
				if (doc is IEditorDocContent)
				{
					//エディタを閉じる
					IEditorDocContent editor = (IEditorDocContent)doc;
					editor.ActivateForm();
					editor.CloseForm();

					if (editor.IsClosed == false)
					{
						//閉じることができなかったので失敗を返す
						return false;
					}
				}
			}

			//すべて閉じれたので成功を返す
			return true;
		}

		/// <summary>
		/// 現在アクティブなタブ以外を全て閉じる
		/// </summary>
		/// <returns></returns>
		public bool CloseFileOtherAll()
		{
			IEditorDocContent editor = ActiveDocument;
			if (editor == null)
			{
				//現在アクティブなエディタがないので何もせずに終了
				return true;
			}

			//現在アクティブなエディタの位置を取得する
			int docLen = 0;
			IDockContent[] docList = m_mainPanel.Documents;
			int activeEditorIndex = 0;
			for (int i = 0; i < docList.Length; i++)
			{
				if (editor == docList[i])
				{
					activeEditorIndex = i;
				}
				if (docList[i] is IEditorDocContent)
				{
					docLen++;
				}
			}

			//ドッキングウィンドウからエディタを検索する
			for (int i = 0; i < docList.Length; i++)
			{
				if (i != activeEditorIndex)	//アクティブだったエディタではないとき
				{
					if (docList[i] is IEditorDocContent)
					{
						//エディタを閉じる
						editor = (IEditorDocContent)docList[i];
						editor.ActivateForm();
						editor.CloseForm();

						if (editor.IsClosed == false)
						{
							//閉じることができなかったので失敗を返す
							return false;
						}
					}
				}
			}

			//すべて閉じれたので成功を返す
			return true;
		}

		/// <summary>
		/// 現在アクティブなタブの左にあるものを全て閉じる
		/// </summary>
		public bool CloseFileLeftAll()
		{
			//まだ未実装
			return false;
		}

		/// <summary>
		/// 現在アクティブなタブの右にあるもの全て閉じる
		/// </summary>
		/// <returns></returns>
		public bool CloseFileRightAll()
		{
			//まだ未実装
			return false;
		}

		/// <summary>
		/// 現在開いているファイル名リストを取得する
		/// </summary>
		/// <returns></returns>
		public string[] GetOpenedFileList()
		{
			List<string> fileList = new List<string>();

			//ドッキングウィンドウからエディタを検索する
			IDockContent[] docList = m_mainPanel.Documents;
			foreach (IDockContent doc in docList)
			{
				if (doc is IEditorDocContent)
				{
					//開いているファイルリストに追加する
					fileList.Add(((IEditorDocContent)doc).FileName);
				}
			}

			return fileList.ToArray();
		}

		/// <summary>
		/// TextReaderを返す
		/// もしエディタを開いていた場合はエディタから、開いていないときはファイルから読み取る
		/// </summary>
		/// <param name="filePath">開きたいファイルパス</param>
		/// <returns>TextReaderオブジェクト</returns>
		public TextReader GetTextReader(string filePath)
		{
			TextReader reader = null;

			//ドッキングウィンドウからエディタを検索する
			IDockContent[] docList = m_mainPanel.Documents;
			foreach (IDockContent doc in docList)
			{
				if (doc is IEditorDocContent)
				{
					if (filePath == ((IEditorDocContent)doc).FileName)
					{
						//現在開いてる時はその文字列から読み取る
						if (FileType.GetKrkrType(filePath) == FileType.KrkrType.Screen)
						{
							//未実装
						}
						else
						{
							reader = new StringReader(((EditorForm)doc).TextEditor.Document.TextContent);
						}
						break;
					}
				}
			}
			if (reader == null)
			{
				//開いていないときはファイルから読み取る
				reader = new StreamReader(filePath, GlobalStatus.TextEnc.GetFileEncoding(filePath));
			}

			return reader;
		}

		/// <summary>
		/// エディタ内容文字列を取得する
		/// エディタを開いていないときはファイルを読み込み文字列を返す
		/// </summary>
		/// <param name="filePath">文字列を取得するファイル名</param>
		/// <returns>文字列</returns>
		public string GetTextFormEditor(string filePath)
		{
			string ret = null;

			//ドッキングウィンドウからエディタを検索する
			IDockContent[] docList = m_mainPanel.Documents;
			foreach (IDockContent doc in docList)
			{
				if (doc is IEditorDocContent)
				{
					if (filePath == ((IEditorDocContent)doc).FileName)
					{
						if (FileType.GetKrkrType(filePath) == FileType.KrkrType.Screen)
						{
							//未実装
						}
						else
						{
							//現在開いてる時はその文字列から読み取る
							ret = ((EditorForm)doc).TextEditor.Document.TextContent;
						}
						break;
					}
				}
			}
			if (ret == null)
			{
				//開いていないときはファイルから読み取る
				TextReader reader = new StreamReader(filePath, GlobalStatus.TextEnc.GetFileEncoding(filePath));
				ret = reader.ReadToEnd();
				reader.Close();
			}

			return ret;
		}

		/// <summary>
		/// 指定したパスの現在開いているテキストエディタを返す
		/// 開いていないときはnullを返す
		/// </summary>
		/// <param name="filePath">取得したいエディタのファイルパス</param>
		/// <returns>開いているエディタオブジェクト</returns>
		public TextEditorEx GetTextEdtorFromFileName(string filePath)
		{
			//ドッキングウィンドウからエディタすべて探索する
			IDockContent[] docList = m_mainPanel.Documents;
			foreach (IDockContent doc in docList)
			{
				if (doc is EditorForm)
				{
					if (filePath == ((EditorForm)doc).TextEditor.FileName)
					{
						return ((EditorForm)doc).TextEditor;
					}
				}
			}

			return null;
		}

		/// <summary>
		/// エディタにフォーカスをセットする
		/// </summary>
		public void Show()
		{
			if (ActiveEditor != null)
			{
				ActiveEditorForm.Show();
			}
		}

		/// <summary>
		/// エディタを分割/解除する
		/// </summary>
		public void ShowSplit()
		{
			if (ActiveEditor != null)
			{
				ActiveEditor.Split();
			}
		}

		public void ShowNextEditor()
		{
			if (ActiveEditor != null)
			{
				TextEditorEx nowActive = ActiveEditor;
				EditorForm firstActiveForm = null;
				bool isFind = false;
				IDockContent[] docList = m_mainPanel.Documents;
				foreach (IDockContent doc in docList)
				{
					EditorForm editor = doc as EditorForm;
					if (editor == null)
					{
						continue;	//次を検索
					}

					if (firstActiveForm == null)
					{
						firstActiveForm = editor;
					}
					if (nowActive == editor.TextEditor)
					{
						isFind = true;
						continue;	//発見したので次へ行く
					}
					if (isFind)
					{
						//見つけたとの物が次のエディタとして表示する
						editor.Show();
						return;
					}
				}
				if (isFind && firstActiveForm != null)
				{
					//発見しても次がないときは最初の物を表示する
					firstActiveForm.Show();
					return;
				}
			}
		}

		public void ShowPrevEditor()
		{
			if (ActiveEditor != null)
			{
				TextEditorEx nowActive = ActiveEditor;
				EditorForm form = null;
				IDockContent[] docList = m_mainPanel.Documents;
				foreach (IDockContent doc in docList)
				{
					EditorForm editor = doc as EditorForm;
					if (editor == null)
					{
						continue;	//次を検索
					}

					
					if (nowActive == editor.TextEditor)
					{
						if (form == null)
						{
							continue;	//自分自身が一番最初の時は最後まで検索する
						}

						break;
					}
					form = editor;
				}

				if (form != null)
				{
					form.Show();
				}
			}
		}
		#endregion

		#region エディタオプション関連メソッド
		/// <summary>
		/// エディタオプションを取得する
		/// </summary>
		/// <param name="fileName">ファイル名</param>
		/// <returns>エディタオプション</returns>
		public EditorOption GetEditorOption(string fileName)
		{
			return GetEditorOption(FileType.GetKrkrType(fileName));
		}

		/// <summary>
		/// エディタオプションを取得する
		/// </summary>
		/// <param name="type">取得するファイルタイプ</param>
		/// <returns>エディタオプション</returns>
		public EditorOption GetEditorOption(FileType.KrkrType type)
		{
			if (type != FileType.KrkrType.Kag && type != FileType.KrkrType.Tjs)
			{
				//KAGとTJS以外はその他のファイルとして扱う
				type = FileType.KrkrType.Unknown;
			}

			if (GlobalStatus.EditorOption.ContainsKey(type) == false)
			{
				string filePath = GetEditorOptionFilePath(type);
				GlobalStatus.EditorOption.Add(type, EditorOption.LoadFile(filePath));			
			}

			return GlobalStatus.EditorOption[type];
		}

		/// <summary>
		/// エディタ設定のファイルパスを取得する
		/// </summary>
		/// <param name="type">ファイルタイプ</param>
		/// <returns>ファイルパス</returns>
		public string GetEditorOptionFilePath(FileType.KrkrType type)
		{
			string ret = "";
			switch (type)
			{
				case FileType.KrkrType.Kag:
					ret = ConstEnvOption.KagOptionFilePath;
					break;
				case FileType.KrkrType.Tjs:
					ret = ConstEnvOption.TjsOptionFilePath;
					break;
				default:
					ret = ConstEnvOption.DefOptionFilePath;
					break;
			}

			return ret;
		}

		/// <summary>
		/// エディタにオプション情報をセットする
		/// </summary>
		/// <param name="editor">オプション情報をセットするエディタ</param>
		public void SetOption(TextEditorEx editor)
		{
			EditorOption option = GetEditorOption(editor.FileName);
			if (option == null)
			{
				util.Msgbox.Error("エディタ設定の読み込みができませんでした");
				return;
			}

			editor.Encoding = option.Encoding;
			editor.Font = option.Font;
			editor.UseCodeCompletion = option.UseCodeCompletion;
			editor.ParseActionSaveFile = option.ParseActionFileSave;

			editor.Document.TextEditorProperties.AllowCaretBeyondEOL = option.AllowCaretBeyondEOL;
			editor.Document.TextEditorProperties.TabIndent = option.TabIndent;
			editor.Document.TextEditorProperties.IndentationSize = option.IndentationSize;
			editor.Document.TextEditorProperties.IndentStyle = option.IndentStyle;
			editor.Document.TextEditorProperties.DocumentSelectionMode = option.DocumentSelectionMode;
			editor.Document.TextEditorProperties.BracketMatchingStyle = option.BracketMatchingStyle;
			editor.Document.TextEditorProperties.ShowMatchingBracket = option.ShowMatchingBracket;
			editor.Document.TextEditorProperties.ShowLineNumbers = option.ShowLineNumbers;
			editor.Document.TextEditorProperties.ShowSpaces = option.ShowSpaces;
			editor.Document.TextEditorProperties.ShowWideSpaces = option.ShowWideSpaces;
			editor.Document.TextEditorProperties.ShowTabs = option.ShowTabs;
			editor.Document.TextEditorProperties.ShowEOLMarker = option.ShowEOLMarker;
			editor.Document.TextEditorProperties.ShowInvalidLines = option.ShowInvalidLines;
			editor.Document.TextEditorProperties.IsIconBarVisible = option.IsIconBarVisible;
			editor.Document.TextEditorProperties.EnableFolding = option.EnableFolding;
			editor.Document.TextEditorProperties.ShowHorizontalRuler = option.ShowHorizontalRuler;
			editor.Document.TextEditorProperties.ShowVerticalRuler = option.ShowVerticalRuler;
			editor.Document.TextEditorProperties.ConvertTabsToSpaces = option.ConvertTabsToSpaces;
			editor.Document.TextEditorProperties.UseAntiAliasedFont = option.UseAntiAliasedFont;
			editor.Document.TextEditorProperties.CreateBackupCopy = option.CreateBackupCopy;
			editor.Document.TextEditorProperties.MouseWheelScrollDown = option.MouseWheelScrollDown;
			editor.Document.TextEditorProperties.MouseWheelTextZoom = option.MouseWheelTextZoom;
			editor.Document.TextEditorProperties.HideMouseCursor = option.HideMouseCursor;
			editor.Document.TextEditorProperties.CutCopyWholeLine = option.CutCopyWholeLine;
			editor.Document.TextEditorProperties.VerticalRulerRow = option.VerticalRulerRow;
			editor.Document.TextEditorProperties.LineViewerStyle = option.LineViewerStyle;
			editor.Document.TextEditorProperties.LineTerminator = option.LineTerminator;
			editor.Document.TextEditorProperties.AutoInsertCurlyBracket = option.AutoInsertCurlyBracket;
			editor.Document.TextEditorProperties.UseCustomLine = option.UseCustomLine;

			GlobalStatus.FormManager.MainForm.UpdateStatusBar(editor);
			editor.OptionsChanged();
		}

		/// <summary>
		/// すべての開いているエディタに現在のオプション情報を適用する
		/// </summary>
		public void SetOptionAll()
		{
			IDockContent[] docList = m_mainPanel.Documents;
			foreach (IDockContent doc in docList)
			{
				if (doc is EditorForm)
				{
					//エディタにオプションをセットする
					EditorForm editor = (EditorForm)doc;
					
					SetOption(editor.TextEditor);
				}
			}
		}
		#endregion

		#region エディタ編集関連メソッド
		/// <summary>
		/// 現在アクティブなエディタの編集状態を一つ前に元に戻す
		/// </summary>
		public void Undo()
		{
			if (ActiveEditor != null)
			{
				ActiveEditor.Undo();
			}
		}

		/// <summary>
		/// アクティブなエディタの編集状態を元に戻していた場合、それを取り消す
		/// </summary>
		public void Redo()
		{
			if (ActiveEditor != null)
			{
				ActiveEditor.Redo();
			}
		}

		/// <summary>
		/// アクティブなエディタの選択しているテキストを切り取る
		/// </summary>
		public void CutText()
		{
			if (ActiveEditor != null)
			{
				ActiveEditor.CutText();
			}
		}

		/// <summary>
		/// アクティブなエディタの選択しているテキストをコピーする
		/// </summary>
		public void CopyText()
		{
			if (ActiveEditor != null)
			{
				ActiveEditor.CopyText();
			}
		}

		/// <summary>
		/// アクティブなエディタの選択しているテキストを貼り付ける
		/// </summary>
		public void PasteText()
		{
			if (ActiveEditor != null)
			{
				ActiveEditor.PasteText();
			}
		}

		/// <summary>
		/// アクティブなエディタの選択しているテキストを削除する
		/// </summary>
		public void DeleteText()
		{
			if (ActiveEditor != null)
			{
				ActiveEditor.DeleteText();
			}
		}

		/// <summary>
		/// アクティブなエディタのテキストを全選択する
		/// </summary>
		public void SelectAllText()
		{
			if (ActiveEditor != null)
			{
				ActiveEditor.SelectAllText();
			}
		}

		/// <summary>
		/// アクティブなエディタの選択範囲のコメント化・コメント解除を行う
		/// </summary>
		public void ToggleComment()
		{
			if (ActiveEditor != null)
			{
				ActiveEditor.ToggleComment();
			}
		}

		/// <summary>
		/// コメントアウトする
		/// </summary>
		public void CommentOut()
		{
			if (ActiveEditor != null)
			{
				ActiveEditor.DoCommentOut();
			}
		}

		/// <summary>
		/// コメントアウトを解除する
		/// </summary>
		public void UnCommentOut()
		{
			if (ActiveEditor != null)
			{
				ActiveEditor.DoUncommentOut();
			}
		}

		/// <summary>
		/// 折りたたみトグル
		/// </summary>
		public void ToggleFolding()
		{
			if (ActiveEditor != null)
			{
				ActiveEditor.ToggleFolding();
			}
		}

		/// <summary>
		/// すべての折り畳みをトグル
		/// </summary>
		public void ToggleFoldingAll()
		{
			if (ActiveEditor != null)
			{
				ActiveEditor.ToggleAllFoldings();
			}
		}

		/// <summary>
		/// 定義をすべて折りたたむ
		/// </summary>
		public void FoldingDefAll()
		{
			if (ActiveEditor != null)
			{
				ActiveEditor.ShowDefinitionsOnly();
			}
		}

		/// <summary>
		/// 現在のカーソル位置に文字を挿入する
		/// </summary>
		/// <param name="text"></param>
		public void InsertText(string text)
		{
			InsertText(text, false);
		}

		/// <summary>
		/// 現在のカーソル位置に文字を挿入する
		/// </summary>
		/// <param name="text">挿入する文字列</param>
		/// <param name="selectedClear">現在選択文字列があるときはクリアするかどうか（trueのときクリアする）</param>
		public void InsertText(string text, bool selectedClear)
		{
			if (ActiveEditor != null)
			{
				if (selectedClear)
				{
					ActiveEditor.ClearSelectedText();	//選択状態解除
				}
				ActiveEditor.InsertText(text);
			}
		}
		#endregion

		#region 検索関連メソッド
		/// <summary>
		/// 検索を行う
		/// </summary>
		/// <param name="option"></param>
		public void Search(EditorSearchOption option)
		{
			if (ActiveEditor != null)
			{
				ActiveEditor.Search(option);
				//ActiveEditor.MarkSearchString(option);
			}
		}

		/// <summary>
		/// 現在選択している文字列を返す
		/// </summary>
		/// <returns></returns>
		public string GetSelectedText()
		{
			if (ActiveEditor != null)
			{
				return ActiveEditor.GetSelectedText();
			}

			return "";
		}

		/// <summary>
		/// 置換を行う
		/// </summary>
		/// <param name="editorSearchOption">検索オプション</param>
		public void Replace(EditorSearchOption option)
		{
			if (ActiveEditor != null)
			{
				EditorSearchResult ret = ActiveEditor.Search(option);
				if (ret != null && ret.IsHit == true)
				{
					ActiveEditor.ReplaceSelectedText(option);
				}
			}
		}

		/// <summary>
		/// 全置換を行う
		/// </summary>
		/// <param name="editorSearchOption"></param>
		public void ReplaceAll(EditorSearchOption option)
		{
			if (ActiveEditor != null)
			{
				ReplaceAllForm repallForm = new ReplaceAllForm();
				repallForm.Show(ActiveEditorForm);

				int count = 0;	//置換個数
				ActiveEditor.MoveCaretLine(0);	//先頭に移動
				while (true)
				{
					Application.DoEvents();
					if (repallForm.Cancel)
					{
						util.Msgbox.Warning("全置換を中止しました");
						return;	//強制中断
					}

					EditorSearchResult ret = ActiveEditor.Search(option, false);	//検索する（見つからなくてもエラーを表示しない）
					if (ret == null || ret.IsHit == false)
					{
						break;	//もう見つかる物がなくなったのでループ終了
					}

					if (ActiveEditor.ReplaceSelectedText(option) == false)
					{
						break;	//置換に失敗したのでループ終了
					}
					count++;
					repallForm.SetReplaceCount(count);
				}
				if (repallForm.Cancel == false)
				{
					repallForm.Close();	//キャンセルしていないときは閉じる
				}

				//全検索が終了したとき
				if (count == 0)
				{
					util.Msgbox.Warning(String.Format("検索文字が見つかりませんでした"));
				}
				else
				{
					util.Msgbox.Info(String.Format("{0} 個の置換を行いました", count));
				}
			}
		}

		/// <summary>
		/// 指定した行へカーソルを移動する
		/// </summary>
		/// <param name="lineNumber">指定行</param>
		public void MoveCaretLine(int lineNumber)
		{
			if (ActiveEditor != null)
			{
				ActiveEditor.MoveCaretLine(lineNumber);
			}
		}
		#endregion

		#region ブックマーク関連
		/// <summary>
		/// 現在のカーソル行をブックマーク登録する
		/// （すでに登録されているときは解除する）
		/// </summary>
		public void ToggleBookmarkSelectedLine()
		{
			if (ActiveEditor != null)
			{
				ActiveEditor.ToggleBookmarkSelectedLine();
			}
		}

		/// <summary>
		/// ブックマークをすべてクリアする
		/// </summary>
		public void ClearAllBookmark()
		{
			if (ActiveEditor != null)
			{
				ActiveEditor.ClearAllBookmark();
				GlobalStatus.FormManager.BookmarkListForm.RemoveBookmarkAll();
			}
		}

		/// <summary>
		/// 次のブックマークへカーソルを移動
		/// </summary>
		public void MoveNextBookmark()
		{
			if (ActiveEditor != null)
			{
				ActiveEditor.MoveNextBookmark();
			}
		}

		/// <summary>
		/// 前のブックマークへカーソルを移動
		/// </summary>
		public void MovePrevBookmark()
		{
			if (ActiveEditor != null)
			{
				ActiveEditor.MovePrevBookmark();
			}
		}

		/// <summary>
		/// 対応する括弧へ移動
		/// </summary>
		public void MoveMatchingKakko()
		{
			if (ActiveEditor != null)
			{
				ActiveEditor.MoveMatchingKakko();
			}
		}
		#endregion
	}
}
