using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;

using kkde.project;
using kkde.global;
using kkde.option;
using kkde.editor;

namespace kkde.parse
{
	/// <summary>
	/// パースを管理実行するクラス
	/// </summary>
	public class ParserService
	{
		#region 内部クラス
		/// <summary>
		/// パース結果を返すときのクラス
		/// </summary>
		private class ParserResult
		{
			CompletionUnit m_cu;

			/// <summary>
			/// パース結果
			/// </summary>
			public CompletionUnit Cu
			{
				get { return m_cu; }
				set { m_cu = value; }
			}

			/// <summary>
			/// コンストラクタ
			/// </summary>
			/// <param name="cu">パース結果</param>
			public ParserResult(CompletionUnit cu)
			{
				m_cu = cu;
			}
		}

		/// <summary>
		/// パース実行関数引数用クラス
		/// </summary>
		private class ParserArg
		{
			/// <summary>
			/// 引数モード
			/// </summary>
			public enum Mode
			{
				File,	//ファイルモード
				Directory,	//ディレクトリモード
			}

			protected Mode m_modeType;
			protected int m_sleepTime = 0;

			/// <summary>
			/// モード
			/// </summary>
			public Mode ModeType
			{
				get { return m_modeType; }
			}

			/// <summary>
			/// スリープタイム
			/// </summary>
			public int SleepTime
			{
				get { return m_sleepTime; }
			}
		}

		/// <summary>
		/// パース実行関数用引数クラス（ファイルモード用）
		/// </summary>
		private class FileParserArg : ParserArg
		{
			string m_filePath = "";
			string m_text = "";

			/// <summary>
			/// 文字列
			/// </summary>
			public string Text
			{
				get { return m_text; }
			}

			/// <summary>
			/// ファイルパス
			/// </summary>
			public string filePath
			{
				get { return m_filePath; }
			}

			/// <summary>
			/// コンストラクタ（ファイル用）
			/// </summary>
			/// <param name="filePath">ファイルパス</param>
			/// <param name="text">ファイルのテキスト文字列</param>
			public FileParserArg(string filePath, string text, int sleepTime)
			{
				this.m_modeType = Mode.File;
				this.m_sleepTime = sleepTime;
				m_text = text;
				m_filePath = filePath;
			}
		}

		/// <summary>
		/// パース実行関数用引数クラス（ディレクトリモード）
		/// </summary>
		private class DirectoryParserArg : ParserArg
		{
			string[] m_fileList = null;
			
			/// <summary>
			/// ファイルパスリスト
			/// </summary>
			public string[] FileList
			{
				get { return m_fileList; }
			}

			/// <summary>
			/// コンストラクタ
			/// </summary>
			/// <param name="fileList"></param>
			public DirectoryParserArg(string[] fileList, int sleepTime)
			{
				this.m_modeType = Mode.Directory;
				this.m_sleepTime = sleepTime;
				m_fileList = fileList;
			}
		}
		#endregion

		#region フィールド
		/// <summary>
		/// このサービスでパース可能なファイル拡張子
		/// </summary>
		const string SUPPORTED_EXT = "*.tjs;*.ks";

		/// <summary>
		/// 実行スレッド用オブジェクト
		/// </summary>
		BackgroundWorker m_bgworker;

		/// <summary>
		/// 解析結果を管理するオブジェクト
		/// </summary>
		CompletionUnitManager m_cuManger = new CompletionUnitManager();
		#endregion

		#region 初期化関連
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ParserService()
		{
			m_bgworker = new BackgroundWorker();
			m_bgworker.WorkerReportsProgress = true;
			m_bgworker.WorkerSupportsCancellation = true;
			m_bgworker.DoWork += new DoWorkEventHandler(BackThreadParser_DoWork);
			m_bgworker.ProgressChanged += new ProgressChangedEventHandler(BackThreadParser_ProgressChanged);
			m_bgworker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackThreadParser_RunWorkerCompleted);
		}
		#endregion

		#region 構文解析関連
		/// <summary>
		/// バックグラウンドでパースを実行する
		/// </summary>
		void BackThreadParser_DoWork(object sender, DoWorkEventArgs e)
		{
			ParserArg arg = (ParserArg)e.Argument;
			if (arg == null)
			{
				return;	//終了
			}

			if (arg.ModeType == ParserArg.Mode.Directory)	//ディレクトリモードの時
			{
				DirectoryParserArg dirArg = (DirectoryParserArg)arg;
				string[] pathList = dirArg.FileList;
				if (pathList == null || pathList.Length == 0)
				{
					return;	//終了
				}

				int count = 0;
				string text = "";
				StreamReader sr = null;
				foreach (string path in pathList)
				{
					count++;
					using (sr = new StreamReader(path, GlobalStatus.TextEnc.GetFileEncoding(path)))
					{
						text = sr.ReadToEnd();
					}
					CompletionUnit cu = BackThreadParser_GetCompletionUnit(path, text);
					if (cu == null)
					{
						continue;
					}

					//キャンセルされているかどうかチェック
					if (m_bgworker.CancellationPending == true)
					{
						return;
					}
					m_bgworker.ReportProgress(count, new ParserResult(cu));	//結果を送信する
				}
			}
			else if (arg.ModeType == ParserArg.Mode.File)	//ファイルモードの時
			{
				FileParserArg fileArg = (FileParserArg)arg;
				if (fileArg.filePath == "" || fileArg.Text == "")
				{
					return;	//終了
				}

				CompletionUnit cu = BackThreadParser_GetCompletionUnit(fileArg.filePath, fileArg.Text);
				if (cu == null)
				{
					return;	//終了
				}

				//キャンセルされているかどうかチェック
				if (m_bgworker.CancellationPending == true)
				{
					return;
				}
				m_bgworker.ReportProgress(1, new ParserResult(cu));	//結果を送信する
			}

			if (arg.SleepTime > 0)
			{
				System.Threading.Thread.Sleep(arg.SleepTime);	//ちょっと休憩
			}
		}

		/// <summary>
		/// パーサーを取得する
		/// </summary>
		/// <param name="filePath">ファイルパス</param>
		/// <param name="text">パースする文字列</param>
		/// <returns>パーサー</returns>
		CompletionUnit BackThreadParser_GetCompletionUnit(string filePath, string text)
		{
			IParser parser = null;
			TextReader reader = null;
			try
			{
				FileType.KrkrType type = FileType.GetKrkrType(filePath);
				if (type == FileType.KrkrType.Kag)
				{
					//KAG用パーサを取得
					reader = new StringReader(text);
					KagLexer lexer = new KagLexer(reader);
					parser = new KagParser(filePath, lexer);
					parser.Parse();
				}
				else if (type == FileType.KrkrType.Tjs)
				{
					if (FileType.IsKagexEnvinitFileName(filePath))
					{
						//KAGEX用
						kagex.KagexEnvinitLexer lexer = new kkde.parse.kagex.KagexEnvinitLexer(text);
						parser = new kagex.KagexEnvinitParser(filePath, lexer);
						parser.Parse();
					}
					else
					{
						//TJS用パーサーを取得
						//未実装
						parser = null;
					}
				}
			}
			catch (Exception err)
			{
				Debug.WriteLine("Parse ERROR: " + err.ToString());
				parser = null;
			}
			finally
			{
				if (reader != null)
				{
					reader.Close();
					reader.Dispose();
				}
			}

			if (parser == null)
			{
				return null;
			}
			return parser.CompletionUnit;
		}

		/// <summary>
		/// バックグラウンドでのパース途中結果を受け取ったとき
		/// </summary>
		void BackThreadParser_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			ParserResult result = (ParserResult)e.UserState;
			if (result == null || result.Cu == null)
			{
				return;	//空の時は何もしない
			}

			//Debug.WriteLine("BackThreadParser_ProgressChanged add filePath=" + result.Cu.FilePath);
			m_cuManger.Add(result.Cu.FilePath, result.Cu);
			reflectParseResult(result.Cu.FilePath);			//更新
		}

		/// <summary>
		/// バックグラウンドでのすべてのパースが完了したとき
		/// </summary>
		void BackThreadParser_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			util.DebugTimer.Stop("●Parse Tiem");
			//Debug.WriteLine("●BackThreadParser_RunWorkerCompleted time=" + (DateTime.Now - m_time).ToString());
		}

		/// <summary>
		/// ディレクトリ内のすべてのファイルをパースする
		/// </summary>
		/// <param name="dirPath">ディレクトリパス</param>
		public void ParseDirectory(string dirPath, ProjectType type)
		{
			//パラメーターチェック
			if (Directory.Exists(dirPath) == false)
			{
				return;
			}

			List<string> list = new List<string>();
			switch (type)
			{
				case ProjectType.Kag3:
					list.Add(ConstEnvOption.Kag3tagDefFilePath);
					break;
				case ProjectType.Kagexpp:
					list.Add(ConstEnvOption.Kag3tagDefFilePath);
					list.Add(ConstEnvOption.KagextagDefFilePath);
					list.Add(ConstEnvOption.WorldextagDefFilePath);
					break;
				default:
					//何もしない
					break;
			}

			//ファイルパスを取得する
			string[] pathList = util.FileUtil.GetDirectoryFile(dirPath, SUPPORTED_EXT, SearchOption.AllDirectories);
			if (pathList == null)
			{
				if (list.Count == 0)
				{
					return;	//定義ファイルも読み込まないときは何もしない
				}
			}
			else
			{
				list.AddRange(pathList);
			}
			

			if (m_bgworker.IsBusy == false)
			{
				//パース開始
				util.DebugTimer.Start();
				m_bgworker.RunWorkerAsync(new DirectoryParserArg(list.ToArray(), GlobalStatus.EnvOption.CodeComplateSeepTime));
			}
			else
			{
				Debug.WriteLine("■ParseDirectory IS BUSY!");
			}
		}

		/// <summary>
		/// ファイルを構文解析する
		/// </summary>
		/// <param name="filePath">ファイルパス</param>
		public void ParseFile(string filePath)
		{
			//パラメーターチェック
			if (File.Exists(filePath) == false)
			{
				return;	//ファイルが存在しないので何もしない
			}
			FileType.KrkrType type = FileType.GetKrkrType(filePath);
			if (isSupportedType(type) == false)
			{
				return;	//非対応ファイル
			}

			if (m_bgworker.IsBusy == false)
			{
				//Debug.WriteLine("●ParseFile Start!");
				util.DebugTimer.Start();
				string text = kkde.global.GlobalStatus.EditorManager.GetTextFormEditor(filePath);
				m_bgworker.RunWorkerAsync(new FileParserArg(filePath, text, GlobalStatus.EnvOption.CodeComplateSeepTime));
			}
			else
			{
				Debug.WriteLine("■ParseFile IS BUSY!");
			}
		}

		/// <summary>
		/// 指定したファイルタイプがパース対応のタイプかどうか
		/// </summary>
		/// <param name="type">チェックするファイルタイプ</param>
		/// <returns>対応ファイルタイプの時はtrue</returns>
		private bool isSupportedType(FileType.KrkrType type)
		{
			bool ret = false;
			switch (type)
			{
				case FileType.KrkrType.Kag:
				case FileType.KrkrType.Tjs:
					ret = true;
					break;
				default:
					ret = false;
					break;
			}

			return ret;
		}

		/// <summary>
		/// 解析結果をすべてクリアーする
		/// </summary>
		public void ClearCompetionUnit()
		{
			m_bgworker.CancelAsync();	//パースを止める
			m_cuManger.Clear();			//オブジェクトをすべてクリアー
		}

		/// <summary>
		/// 解析結果を反映する
		/// </summary>
		/// <param name="filePath"></param>
		private void reflectParseResult(string filePath)
		{
			//ファイルタイプ別更新
			FileType.KrkrType type = FileType.GetKrkrType(filePath);
			switch (type)
			{
				case FileType.KrkrType.Kag:
					if (GlobalStatus.FormManager.IsHiddenKagLabelForm == false)	//表示しているときだけ
					{
						GlobalStatus.FormManager.KagLabelForm.InitTreeFileItem(filePath);
					}
					break;
				case FileType.KrkrType.Tjs:
					if (FileType.IsKagexEnvinitFileName(filePath))
					{
						if (GlobalStatus.FormManager.IsHiddenWorldExViewForm == false)	//表示しているとき
						{
							GlobalStatus.FormManager.WorldExViewForm.InitView();
						}
					}
					else
					{
						//TJSはまだ未実装
					}
					break;
			}
			
			//エディタ関連更新
			TextEditorEx editor = GlobalStatus.EditorManager.GetTextEdtorFromFileName(filePath);
			if (editor != null)
			{
				editor.UpdateFolding();	//折りたたみを更新する
			}
		}
		#endregion

		#region 解析結果表示関連
		/// <summary>
		/// ラベルリストを指定したファイルから取得する
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public KagLabelItem[] GetLabelList(string filePath)
		{
			if (m_cuManger.KagTable.ContainsKey(filePath) == false)
			{
				return null;
			}

			return m_cuManger.KagTable[filePath].LabelList.ToArray();
		}

		/// <summary>
		/// ラベルリストをすべてのファイルから取得する
		/// </summary>
		/// <returns>ラベルリスト</returns>
		public KagLabelItem[] GetLabelListAll()
		{
			List<KagLabelItem> list = new List<KagLabelItem>();
			foreach (string filePath in m_cuManger.KagTable.Keys)
			{
				list.AddRange(m_cuManger.KagTable[filePath].LabelList);
			}

			return list.ToArray();
		}

		/// <summary>
		/// KAGマクロリストを取得する
		/// </summary>
		/// <returns></returns>
		public KagMacro[] GetKagMacroList()
		{
			//優先順位を生かすため一端辞書に格納する
			Dictionary<string, KagMacro> dic = new Dictionary<string, KagMacro>();

			//KAG3
			if (m_cuManger.KagTable.ContainsKey(ConstEnvOption.Kag3tagDefFilePath))
			{
				//KAG3
				foreach (KagMacro macro in m_cuManger.KagTable[ConstEnvOption.Kag3tagDefFilePath].MacroTable.Values)
				{
					dic.Add(macro.Name, macro);
				}
			}
			if (m_cuManger.KagTable.ContainsKey(ConstEnvOption.KagextagDefFilePath))
			{
				//KAGEX
				foreach (KagMacro macro in m_cuManger.KagTable[ConstEnvOption.KagextagDefFilePath].MacroTable.Values)
				{
					if (dic.ContainsKey(macro.Name))
					{
						dic.Remove(macro.Name);		//KAGEXの記述を優先するため以前のものは削除する
					}
					dic.Add(macro.Name, macro);
				}
			}
			if (m_cuManger.KagTable.ContainsKey(ConstEnvOption.WorldextagDefFilePath))
			{
				//ワールド拡張
				foreach (KagMacro macro in m_cuManger.KagTable[ConstEnvOption.WorldextagDefFilePath].MacroTable.Values)
				{
					if (dic.ContainsKey(macro.Name))
					{
						dic.Remove(macro.Name);		//ワールド拡張の記述を優先するため以前のものは削除する
					}
					dic.Add(macro.Name, macro);
				}
			}
			//ユーザーマクロ
			foreach (string filePath in m_cuManger.KagTable.Keys)
			{
				foreach (KagMacro macro in m_cuManger.KagTable[filePath].MacroTable.Values)
				{
					if (dic.ContainsKey(macro.Name))
					{
						dic.Remove(macro.Name);	//ユーザーマクロの記述を優先するため以前のものは削除する
					}
					dic.Add(macro.Name, macro);
				}
			}

			//配列を返すため一端リストに格納する
			List<KagMacro> list = new List<KagMacro>();
			list.AddRange(dic.Values);
			return list.ToArray();
		}

		/// <summary>
		/// 折りたたみ範囲を取得する
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public RegionItem[] getRegionList(string filePath)
		{
			if (m_cuManger.KagTable.ContainsKey(filePath) == false)
			{
				return null;
			}

			return m_cuManger.KagTable[filePath].RegionList.ToArray();
		}

		/// <summary>
		/// KAGEXの解析結果を取得する
		/// 存在しないときはnulを返す
		/// </summary>
		/// <returns>KAGEX解析結果</returns>
		public kagex.KagexCompletionUnit GetKagexEnvinitInfo()
		{
			kagex.KagexCompletionUnit cu = m_cuManger.KagexEnvinitInfo;
			return cu;
		}
		#endregion

		#region デバッグ用
		public void DebugKagexEnvinit(string key)
		{
			kagex.KagexCompletionUnit cu = m_cuManger.KagexEnvinitInfo;
			if (cu == null)
			{
				return;
			}

			cu.DebugPrint();
		}
		#endregion

	}
}
