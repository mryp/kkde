using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using kkde.global;
using System.Diagnostics;
using ICSharpCode.TextEditor.Document;

namespace kkde.search
{


	/// <summary>
	/// Grep検索を行うクラス
	/// </summary>
	public class Grep
	{
		/// <summary>
		/// 検索結果を保持するクラス
		/// </summary>
		public class SearchResult
		{
			string m_filePath = "";
			int m_lineNumber = 0;
			string m_lineText = "";

			/// <summary>
			/// ファイルパス
			/// </summary>
			public string FilePath
			{
				get { return m_filePath; }
				set { m_filePath = value; }
			}

			/// <summary>
			/// 行番号
			/// </summary>
			public int LineNumber
			{
				get { return m_lineNumber; }
				set { m_lineNumber = value; }
			}
			
			/// <summary>
			/// 検索してヒットした文字列
			/// </summary>
			public string LineText
			{
				get { return m_lineText; }
				set { m_lineText = value; }
			}

			/// <summary>
			/// コンストラクタ
			/// </summary>
			public SearchResult(string filePath, int lineNumber, string lineText)
			{
				m_filePath = filePath;
				m_lineNumber = lineNumber;
				m_lineText = lineText;
			}
		}

		/// <summary>
		/// 検索オプション
		/// </summary>
		EditorSearchOption m_option = null;

		/// <summary>
		/// 検索オプション情報
		/// </summary>
		public EditorSearchOption Option
		{
			get { return m_option; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Grep(EditorSearchOption option)
		{
			//エラーチェック
			if (option.SearchKeyword == "")
			{
				util.Msgbox.Error("検索キーワードが指定されていません");
				return;
			}
			if (option.GrepOption.FileExt == "")
			{
				util.Msgbox.Error("検索種類が指定されていません");
				return;
			}
			if (option.GrepOption.Pos == GrepPotision.Folder)
			{
				if (Directory.Exists(option.GrepOption.FolderPath) == false)
				{
					util.Msgbox.Error("検索フォルダが見つかりません");
					return;
				}
			}
			else if (option.GrepOption.Pos == GrepPotision.Project)
			{
				if (Directory.Exists(GlobalStatus.Project.DataFullPath) == false)
				{
					util.Msgbox.Error("プロジェクトフォルダが見つかりません");
					return;
				}
			}

			m_option = option;
		}

		/// <summary>
		/// Grep検索を行う
		/// </summary>
		public List<SearchResult> Search(string filePath)
		{
			//エラーチェック
			if (m_option == null)
			{
				return null;	//オプションが不正なとき
			}
			if (File.Exists(filePath) == false)
			{
				return null;	//ファイルが見つからなかったとき
			}

			List<SearchResult> list = new List<SearchResult>();
			StreamReader sr;
			using (sr = new StreamReader(filePath, Encoding.GetEncoding(932)))
			{
				int lineNum = 0;
				GapTextBufferStrategy buffer = new GapTextBufferStrategy();
				EditorSearchResult result = null;
				while (sr.Peek() > -1)
				{
					//データセット
					lineNum++;
					string line = sr.ReadLine();
					buffer.SetContent(line);

					//検索開始
					result = util.TextUtil.SearchNext(m_option, 0, buffer);
					if (result != null && result.IsHit)
					{
						//見つかったのでリストに追加する
						list.Add(new SearchResult(filePath, lineNum, line));
					}
				}
			}

			return list;
		}

		/// <summary>
		/// 検索するファイルリストを取得する
		/// </summary>
		/// <returns></returns>
		public string[] GetSearchFileList()
		{
			if (m_option == null)
			{
				return null;
			}

			//検索場所を設定する
			string path = m_option.GrepOption.SearchStartPath;
			SearchOption searchOption = SearchOption.TopDirectoryOnly;
			if (m_option.GrepOption.Pos == GrepPotision.Project)
			{
				searchOption = SearchOption.AllDirectories;
			}
			else if (m_option.GrepOption.Pos == GrepPotision.Folder)
			{
				if (m_option.GrepOption.UseSubFolder)
				{
					searchOption = SearchOption.AllDirectories;
				}
				else
				{
					searchOption = SearchOption.TopDirectoryOnly;
				}
			}
			else
			{
				util.Msgbox.Error("検索場所指定が不正です");
				return null;
			}			

			//指定した拡張子のファイルパスリストを取得する
			string[] pathList = util.FileUtil.GetDirectoryFile(path, m_option.GrepOption.FileExt, searchOption);
			if (pathList == null)
			{
				return null;
			}

			return pathList;
		}
	}
}
