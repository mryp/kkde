using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

using kkde.editor;
using System.Drawing;
using System.Xml;

namespace kkde.option
{
	/// <summary>
	/// 環境設定情報を保持するクラス
	/// </summary>
	public class EnvOption
	{
		#region 定数
		public const string APP_DATA_DIR_NAME = "KKDE";
		public const string DIR_NAME = "config";
		public const string EXT_NAME = "config";
		#endregion

		#region クラス・列挙
		/// <summary>
		/// ヘルプを表示するウィンドウ
		/// </summary>
		public enum HelpWindowState
		{
			/// <summary>
			/// ドッキングウィンドウを使用する
			/// </summary>
			DockingWindow,
			/// <summary>
			/// 独立ダイアログウィンドウを使用する
			/// </summary>
			DialogWindow,
		}
		#endregion

		#region フィールド
		//履歴関連
		private kkde.editor.HistoryTable m_historyProject = new kkde.editor.HistoryTable();
		private kkde.editor.HistoryTable m_historyFile = new kkde.editor.HistoryTable();
		private List<ProjectStringTable> m_openedList = new List<ProjectStringTable>();
		private List<ProjectBookmarkTable> m_bookmarkList = new List<ProjectBookmarkTable>();

		//検索関連
		private kkde.search.EditorSearchOption m_searchOption = new kkde.search.EditorSearchOption();
		private bool m_searchResultShowDiaglog = false;

		//実行関連
		private bool m_execSaveAll = true;
		private bool m_execOpenLog = true;
		private bool m_execKrkrKill = true;
		private bool m_execAddClearOption = true;

		//システム関連
		private int m_codeComplateSeepTime = 1000;

		//プロジェクト関連
		private string m_lastProjectPath = "";
		private bool m_projectOpenedLastProject = true;
		private bool m_projectOpenedLastFile = true;
		private bool m_projectWatchProjectPath = true;
//		private bool m_projectTreeShowHideFile = false;
//		private string m_projectTreeHideFileExt = ".bak;";

		//ウィンドウ関連
		private FormStartPosition m_windowStartPos = FormStartPosition.WindowsDefaultLocation;
		private FormWindowState m_windowState = FormWindowState.Normal;
		private Size m_windowSize = new Size(800, 600);

		//ツールバー関連
		private ToolBarPos m_toolBarFilePos = new ToolBarPos(new Point(3, 0), ToolBarDirection.Top);
		private ToolBarPos m_toolBarEditPos = new ToolBarPos(new Point(107, 0), ToolBarDirection.Top);
		private ToolBarPos m_toolBarSearchPos = new ToolBarPos(new Point(292, 0), ToolBarDirection.Top);
		private ToolBarPos m_toolBarBookmarkPos = new ToolBarPos(new Point(425, 0), ToolBarDirection.Top);
		private ToolBarPos m_toolBarExecPos = new ToolBarPos(new Point(529, 0), ToolBarDirection.Top);
		private ToolBarPos m_toolBarViewPos = new ToolBarPos(new Point(587, 0), ToolBarDirection.Top);
		
		//ヘルプ関連
		private HelpWindowState m_helpHelpWindow = HelpWindowState.DockingWindow;
		#endregion

		#region 履歴関連プロパティ
		/// <summary>
		/// プロジェクトを開いたときの履歴
		/// </summary>
		public kkde.editor.HistoryTable HistoryProject
		{
			get { return m_historyProject; }
			set { m_historyProject = value; }
		}

		/// <summary>
		/// ファイルを開いたときの履歴
		/// </summary>
		public kkde.editor.HistoryTable HistoryFile
		{
			get { return m_historyFile; }
			set { m_historyFile = value; }
		}

		/// <summary>
		/// 開いているファイルリスト
		/// </summary>
		public List<kkde.editor.ProjectStringTable> OpenedList
		{
			get { return m_openedList; }
			set { m_openedList = value; }
		}

		/// <summary>
		/// プロジェクトのブックマークリスト
		/// </summary>
		public List<ProjectBookmarkTable> BookmarkList
		{
			get { return m_bookmarkList; }
			set { m_bookmarkList = value; }
		}
		#endregion

		#region 検索関連プロパティ
		/// <summary>
		/// 検索オプション情報
		/// </summary>
		public kkde.search.EditorSearchOption SearchOption
		{
			get { return m_searchOption; }
			set { m_searchOption = value; }
		}

		/// <summary>
		/// 検索時にメッセージダイアログを表示するかどうか
		/// </summary>
		public bool SearchResultShowDiaglog
		{
			get { return m_searchResultShowDiaglog; }
			set { m_searchResultShowDiaglog = value; }
		}
		#endregion

		#region プロジェクト関連プロパティ
		/// <summary>
		/// 最後に読み込んだプロジェクトパス
		/// </summary>
		public string LastProjectPath
		{
			get { return m_lastProjectPath; }
			set { m_lastProjectPath = value; }
		}

		/// <summary>
		/// 最後に開いたプロジェクトを開く
		/// </summary>
		public bool ProjectOpenedLastProject
		{
			get { return m_projectOpenedLastProject; }
			set { m_projectOpenedLastProject = value; }
		}

		/// <summary>
		/// 最後に開いていたファイルを開く
		/// </summary>
		public bool ProjectOpenedLastFile
		{
			get { return m_projectOpenedLastFile; }
			set { m_projectOpenedLastFile = value; }
		}

		/// <summary>
		/// プロジェクトパスを監視し、プロジェクトツリーに反映する
		/// </summary>
		public bool ProjectWatchProjectPath
		{
			get { return m_projectWatchProjectPath; }
			set { m_projectWatchProjectPath = value; }
		}
		#endregion

		#region 実行関連プロパティ
		/// <summary>
		/// 実行時に全保存を行う
		/// </summary>
		public bool ExecSaveAll
		{
			get { return m_execSaveAll; }
			set { m_execSaveAll = value; }
		}

		/// <summary>
		/// 実行時にログが存在するときはログファイルを開く
		/// </summary>
		public bool ExecOpenLog
		{
			get { return m_execOpenLog; }
			set { m_execOpenLog = value; }
		}

		/// <summary>
		/// 吉里吉里実行時にすでに起動しているときは強制終了する
		/// </summary>
		public bool ExecKrkrKill
		{
			get { return m_execKrkrKill; }
			set { m_execKrkrKill = value; }
		}

		/// <summary>
		/// 実行時にClearオプションを付加する
		/// </summary>
		public bool ExecAddClearOption
		{
			get { return m_execAddClearOption; }
			set { m_execAddClearOption = value; }
		}
		#endregion

		#region システム関連プロパティ
		/// <summary>
		/// 入力補完時のスリープタイム
		/// </summary>
		public int CodeComplateSeepTime
		{
			get { return m_codeComplateSeepTime; }
			set { m_codeComplateSeepTime = value; }
		}
		#endregion

		#region ウィンドウ関連
		/// <summary>
		/// ウィンドウの表示開始位置
		/// </summary>
		public FormStartPosition WindowStartPos
		{
			get { return m_windowStartPos; }
			set { m_windowStartPos = value; }
		}

		/// <summary>
		/// ウィンドウの表示状態
		/// </summary>
		public FormWindowState WindowState
		{
			get { return m_windowState; }
			set { m_windowState = value; }
		}

		/// <summary>
		/// ウィンドウのサイズ
		/// </summary>
		public Size WindowSize
		{
			get { return m_windowSize; }
			set { m_windowSize = value; }
		}
		#endregion

		#region ツールバー関連プロパティ
		/// <summary>
		/// ファイルツールバーの位置
		/// </summary>
		public ToolBarPos ToolBarFilePos
		{
			get { return m_toolBarFilePos; }
			set { m_toolBarFilePos = value; }
		}

		/// <summary>
		/// 編集ツールバーの位置
		/// </summary>
		public ToolBarPos ToolBarEditPos
		{
			get { return m_toolBarEditPos; }
			set { m_toolBarEditPos = value; }
		}

		/// <summary>
		/// 検索ツールバーの位置
		/// </summary>
		public ToolBarPos ToolBarSearchPos
		{
			get { return m_toolBarSearchPos; }
			set { m_toolBarSearchPos = value; }
		}

		/// <summary>
		/// ブックマークツールバーの位置
		/// </summary>
		public ToolBarPos ToolBarBookmarkPos
		{
			get { return m_toolBarBookmarkPos; }
			set { m_toolBarBookmarkPos = value; }
		}

		/// <summary>
		/// 実行ツールバーの位置
		/// </summary>
		public ToolBarPos ToolBarExecPos
		{
			get { return m_toolBarExecPos; }
			set { m_toolBarExecPos = value; }
		}

		/// <summary>
		/// 表示ツールバーの位置
		/// </summary>
		public ToolBarPos ToolBarViewPos
		{
			get { return m_toolBarViewPos; }
			set { m_toolBarViewPos = value; }
		}

		#endregion

		#region ヘルプ
		/// <summary>
		/// ヘルプを表示するウィンドウ
		/// </summary>
		public HelpWindowState HelpHelpWindow
		{
			get { return m_helpHelpWindow; }
			set { m_helpHelpWindow = value; }
		}
		#endregion

		#region 静的プロパティ
		/// <summary>
		/// アプリケーションデータパス
		/// </summary>
		public static string AppDataPath
		{
			get
			{
				string dirPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
				dirPath = Path.Combine(dirPath, APP_DATA_DIR_NAME);
				if (Directory.Exists(dirPath) == false)
				{
					Directory.CreateDirectory(dirPath);	//フォルダがないときは作成する
				}

				return dirPath;
			}
		}

		/// <summary>
		/// 設定ファイルを保存するディレクトリパス（読み取り専用）
		/// </summary>
		public static string DirectoryPath
		{
			get
			{
				//フォルダパスを生成する
				string dirPath = Path.Combine(AppDataPath, DIR_NAME);
				//string dirPath = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
				//dirPath = Path.Combine(dirPath, DIR_NAME);

				//フォルダが存在するかどうかをチェック
				if (Directory.Exists(dirPath) == false)
				{
					//フォルダが存在しないときは作成する
					Directory.CreateDirectory(dirPath);
				}

				return dirPath;
			}
		}

		/// <summary>
		/// 設定ファイルを保存するファイルパス（読み取り専用）
		/// </summary>
		public static string FilePath
		{
			get
			{
				//アプリケーション名をベースにして設定ファイル名を作成する
				string fileName = Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.ExecutablePath);
				return Path.Combine(DirectoryPath, fileName + "." + EXT_NAME);
			}
		}
		#endregion

		#region メソッド
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public EnvOption()
		{
		}
		#endregion

		#region クラスメソッド
		/// <summary>
		/// 環境設定をファイルに保存する
		/// </summary>
		/// <param name="fileName">作成する環境設定ファイル名</param>
		/// <param name="option">保存する環境設定</param>
		public static void SaveFile(string fileName, EnvOption option)
		{
			//環境設定をファイルに保存する
			//XmlSerializer serializer = new XmlSerializer(typeof(EnvOption));
			//FileStream fs;
			//using (fs = new FileStream(fileName, FileMode.Create))
			//{
			//    serializer.Serialize(fs, option);
			//}

			try
			{
				saveToXml(fileName, option);
			}
			catch (Exception err)
			{
				util.Msgbox.Error("環境設定ファイルの保存に失敗しました\n" + err.Message);
			}
		}

		/// <summary>
		/// 環境設定をファイルから読み込む
		/// </summary>
		/// <param name="fileName">読み込む環境設定ファイル名</param>
		/// <returns>読み込んだ環境設定情報</returns>
		public static EnvOption LoadFile(string fileName)
		{
			//プロジェクトファイルを読み込み自分自身にコピーする
			//XmlSerializer serializer = new XmlSerializer(typeof(EnvOption));
			//FileStream fs;
			//EnvOption option = null;
			//using (fs = new FileStream(fileName, FileMode.Open))
			//{
			//    option = (EnvOption)serializer.Deserialize(fs);
			//}
			//return option;

			EnvOption option = null;
			try
			{
				option = loadFromXml(fileName);
			}
			catch (Exception err)
			{
				util.Msgbox.Error("環境設定ファイルが読み込めませんでした\n" + err.Message);
				option = new EnvOption();	//デフォルト値をセットする
			}

			return option;
		}

		/// <summary>
		/// プロジェクトテーブルリストからキーに該当するテーブルを取得する
		/// キーが見つからないときは新規に作成する
		/// </summary>
		/// <param name="key">検索するキー</param>
		/// <param name="tableList">検索されるテーブルリスト</param>
		/// <returns>テーブル</returns>
		public ProjectStringTable GetProjectOpenedTable(string key)
		{
			ProjectStringTable projectTable = null;
			foreach (ProjectStringTable table in m_openedList)
			{
				if (table.ProjectPath == key)
				{
					projectTable = table;
					break;
				}
			}
			if (projectTable == null)
			{
				//無いときは新たに作成する
				projectTable = new ProjectStringTable();
				projectTable.ProjectPath = key;
				m_openedList.Add(projectTable);
			}

			return projectTable;
		}

		/// <summary>
		/// プロジェクトテーブルリストからキーに該当するテーブルを取得する
		/// キーが見つからないときは新規に作成する
		/// </summary>
		/// <param name="key">検索するキー</param>
		/// <param name="tableList">検索されるテーブルリスト</param>
		/// <returns>テーブル</returns>
		public ProjectBookmarkTable GetProjectBookmarkTable(string key)
		{
			ProjectBookmarkTable projectTable = null;
			foreach (ProjectBookmarkTable table in m_bookmarkList)
			{
				if (table.ProjectPath == key)
				{
					projectTable = table;
					break;
				}
			}
			if (projectTable == null)
			{
				//無いときは新たに作成する
				projectTable = new ProjectBookmarkTable();
				projectTable.ProjectPath = key;
				m_bookmarkList.Add(projectTable);
			}

			return projectTable;
		}
		#endregion

		#region XMLファイル読み込みメソッド
		private static EnvOption loadFromXml(string filePath)
		{
			EnvOption option = new EnvOption();
			FileStream fs;
			using (fs = new FileStream(filePath, FileMode.Open))
			{
				XmlDocument doc = new XmlDocument();
				doc.Load(fs);

				//ルートノードを読み込む
				XmlNodeList nodeList = doc.GetElementsByTagName("EnvOption");
				if (nodeList == null || nodeList.Count == 0)
				{
					throw new ApplicationException("環境設定ファイル読み込みエラー\nこのファイルは不正な設定ファイルです");
				}

				//設定値を読み込む
				foreach (XmlElement element in nodeList[0].ChildNodes)
				{
					switch (element.Name)
					{
						case "HistoryProject":
							option.HistoryProject = getHistoryTableFromXmlElement(element);
							break;
						case "HistoryFile":
							option.HistoryFile = getHistoryTableFromXmlElement(element);
							break;
						case "OpenedList":
							option.OpenedList = getOpenedListFromXmlElement(element);
							break;
						case "BookmarkList":
							option.BookmarkList = getBookmarkListFromXmlElement(element);
							break;
						case "SearchOption":
							option.SearchOption = getSearchOptionFromXmlElement(element);
							break;
						case "SearchResultShowDiaglog":
							option.SearchResultShowDiaglog = Boolean.Parse(element.InnerText);
							break;
						case "LastProjectPath":
							option.LastProjectPath = element.InnerText;
							break;
						case "ProjectOpenedLastProject":
							option.ProjectOpenedLastProject = Boolean.Parse(element.InnerText);
							break;
						case "ProjectOpenedLastFile":
							option.ProjectOpenedLastFile = Boolean.Parse(element.InnerText);
							break;
						case "ProjectWatchProjectPath":
							option.ProjectWatchProjectPath = Boolean.Parse(element.InnerText);
							break;
						case "ExecSaveAll":
							option.ExecSaveAll = Boolean.Parse(element.InnerText);
							break;
						case "ExecOpenLog":
							option.ExecOpenLog = Boolean.Parse(element.InnerText);
							break;
						case "ExecKrkrKill":
							option.ExecKrkrKill = Boolean.Parse(element.InnerText);
							break;
						case "ExecAddClearOption":
							option.ExecAddClearOption = Boolean.Parse(element.InnerText);
							break;
						case "CodeComplateSeepTime":
							option.CodeComplateSeepTime = Int32.Parse(element.InnerText);
							break;
						case "WindowStartPos":
							option.WindowStartPos = (FormStartPosition)Enum.Parse(typeof(FormStartPosition), element.InnerText);
							break;
						case "WindowState":
							option.WindowState = (FormWindowState)Enum.Parse(typeof(FormWindowState), element.InnerText);
							break;
						case "WindowSize":
							option.WindowSize = getSizeFromXmlElement(element);
							break;
						case "ToolBarFilePos":
							option.ToolBarFilePos = getToolBarPosFromXmlElement(element);
							break;
						case "ToolBarEditPos":
							option.ToolBarEditPos = getToolBarPosFromXmlElement(element);
							break;
						case "ToolBarSearchPos":
							option.ToolBarSearchPos = getToolBarPosFromXmlElement(element);
							break;
						case "ToolBarBookmarkPos":
							option.ToolBarBookmarkPos = getToolBarPosFromXmlElement(element);
							break;
						case "ToolBarExecPos":
							option.ToolBarExecPos = getToolBarPosFromXmlElement(element);
							break;
						case "ToolBarViewPos":
							option.ToolBarViewPos = getToolBarPosFromXmlElement(element);
							break;
						case "HelpHelpWindow":
							option.HelpHelpWindow = (HelpWindowState)Enum.Parse(typeof(HelpWindowState), element.InnerText);
							break;
					}
				}
			}

			return option;
		}

		/// <summary>
		/// XmlElementからSize情報を取得する
		/// element例：
		/// <WindowSize>
		///   <Width>1217</Width>
		///   <Height>974</Height>
		/// </WindowSize>
		/// </summary>
		/// <param name="sizeElement"></param>
		/// <returns></returns>
		private static Size getSizeFromXmlElement(XmlElement sizeElement)
		{
			Size size = new Size();
			foreach (XmlElement element in sizeElement.ChildNodes)
			{
				switch (element.Name)
				{
					case "Width":
						size.Width = Int32.Parse(element.InnerText);
						break;
					case "Height":
						size.Height = Int32.Parse(element.InnerText);
						break;
				}
			}

			return size;
		}

		/// <summary>
		/// XmlElementからToolBarPos情報を取得する
		/// element例：
		/// <ToolBarViewPos>
		///   <Direction>Top</Direction>
		///   <Location>
		///     <X>564</X>
		///     <Y>0</Y>
		///   </Location>
		/// </ToolBarViewPos>
		/// </summary>
		/// <param name="toolBarElement"></param>
		/// <returns></returns>
		private static ToolBarPos getToolBarPosFromXmlElement(XmlElement toolBarElement)
		{
			ToolBarPos toolBarPos = new ToolBarPos();
			foreach (XmlElement element in toolBarElement.ChildNodes)
			{
				switch (element.Name)
				{
					case "Direction":
						toolBarPos.Direction = (ToolBarDirection)Enum.Parse(typeof(ToolBarDirection), element.InnerText);
						break;
					case "Location":
						toolBarPos.Location = getPointFromXmlElement(element);
						break;
				}
			}

			return toolBarPos;
		}

		/// <summary>
		/// XmlElementからPoint情報を取得する
		/// element例：
		/// <Location>
		///   <X>564</X>
		///   <Y>0</Y>
		/// </Location>
		/// </summary>
		/// <param name="pointElement"></param>
		/// <returns></returns>
		private static Point getPointFromXmlElement(XmlElement pointElement)
		{
			Point p = new Point();
			foreach (XmlElement element in pointElement.ChildNodes)
			{
				switch (element.Name)
				{
					case "X":
						p.X = Int32.Parse(element.InnerText);
						break;
					case "Y":
						p.Y = Int32.Parse(element.InnerText);
						break;
				}
			}

			return p;
		}

		/// <summary>
		/// XmlElementからHistoryTable情報を取得する
		/// element例:
		/// <HistoryProject>
		///   <List>
		///     <string>D:\src\krkr\game\abyss\trial\trunk\abyss.krkrproj</string>
		///      <string>C:\Users\mry\Desktop\AAA\AAA.krkrproj</string>
		///   </List>
		///   <MaxCount>8</MaxCount>
		/// </HistoryProject>
		/// </summary>
		/// <param name="historyTableElement"></param>
		/// <returns></returns>
		private static HistoryTable getHistoryTableFromXmlElement(XmlElement historyTableElement)
		{
			HistoryTable table = new HistoryTable();
			foreach (XmlElement element in historyTableElement.ChildNodes)
			{
				switch (element.Name)
				{
					case "List":
						table.List = getStringListFromXmlElement(element);
						break;
					case "MaxCount":
						table.MaxCount = Int32.Parse(element.InnerText);
						break;
				}
			}

			return table;
		}

		/// <summary>
		/// XmlElementから文字列リスト情報を取得する
		/// element例：
		/// <List>
		///   <string>D:\src\krkr\game\abyss\trial\trunk\abyss.krkrproj</string>
		///    <string>C:\Users\mry\Desktop\AAA\AAA.krkrproj</string>
		/// </List> 
		/// </summary>
		/// <param name="listElement"></param>
		/// <returns></returns>
		private static List<string> getStringListFromXmlElement(XmlElement listElement)
		{
			List<string> list = new List<string>();
			foreach (XmlElement element in listElement.ChildNodes)
			{
				if (element.Name == "string")
				{
					list.Add(element.InnerText);
				}
			}

			return list;
		}

		/// <summary>
		/// XmlElementからProjectStringTableリストを取得する
		/// element例：
		/// <OpenedList>
		///   <ProjectStringTable>
		///     <ProjectPath>D:\src\krkr\game\abyss\trial\trunk\abyss.krkrproj</ProjectPath>
		///     <List>
		///       <string>D:\src\krkr\game\abyss\trial\trunk\data\main\debug_kagex.ks</string>
		///     </List>
		///   </ProjectStringTable>
		///   <ProjectStringTable>
		///     <ProjectPath>C:\Users\mry\Desktop\AAA\AAA.krkrproj</ProjectPath>
 		///     <List>
 		///       <string>C:\Users\mry\Desktop\AAA\data\system\envinit.tjs</string>
		///     </List>
 		///   </ProjectStringTable>
		/// </OpenedList>
		/// </summary>
		/// <param name="openedListElement"></param>
		/// <returns></returns>
		private static List<ProjectStringTable> getOpenedListFromXmlElement(XmlElement openedListElement)
		{
			List<ProjectStringTable> openedList = new List<ProjectStringTable>();
			foreach (XmlElement element in openedListElement.ChildNodes)
			{
				switch (element.Name)
				{
					case "ProjectStringTable":
						openedList.Add(getProjectStringTableFromxmlElement(element));
						break;
				}
			}

			return openedList;
		}

		/// <summary>
		/// XmlElementからProjectStringTable情報を取得する
		/// element例：
		/// <ProjectStringTable>
		///   <ProjectPath>D:\src\krkr\game\abyss\trial\trunk\abyss.krkrproj</ProjectPath>
		///   <List>
		///     <string>D:\src\krkr\game\abyss\trial\trunk\data\main\debug_kagex.ks</string>
		///   </List>
		/// </ProjectStringTable>
		/// </summary>
		/// <param name="tableElement"></param>
		/// <returns></returns>
		private static ProjectStringTable getProjectStringTableFromxmlElement(XmlElement tableElement)
		{
			ProjectStringTable projectStringTable = new ProjectStringTable();
			foreach (XmlElement element in tableElement.ChildNodes)
			{
				switch (element.Name)
				{
					case "ProjectPath":
						projectStringTable.ProjectPath = element.InnerText;
						break;
					case "List":
						projectStringTable.List = getStringListFromXmlElement(element);
						break;
				}
			}

			return projectStringTable;
		}

		/// <summary>
		/// XmlElementからProjectBookmarkTableリストを取得する
		/// 例：
		/// <BookmarkList>
		///   <ProjectBookmarkTable>
		///     <ProjectPath>D:\src\krkr\game\abyss\trial\trunk\abyss.krkrproj</ProjectPath>
		///       <List>
		///         <BookmarkInfo>
		///           <LineNumber>11</LineNumber>
		///           <FilePath>D:\src\krkr\game\abyss\trial\trunk\data\main\debug_kagex.ks</FilePath>
		///           <Name>ブックマーク</Name>
		///         </BookmarkInfo>
		///         <BookmarkInfo>
		///           <LineNumber>19</LineNumber>
		///           <FilePath>D:\src\krkr\game\abyss\trial\trunk\data\main\debug_kagex.ks</FilePath>
		///           <Name>ブックマーク</Name>
		///         </BookmarkInfo>
		///       </List>
		///     </ProjectBookmarkTable>
		///     <ProjectBookmarkTable>
		///       <ProjectPath>C:\Users\mry\Desktop\AAA\AAA.krkrproj</ProjectPath>
		///       <List />
		///   </ProjectBookmarkTable>
		/// </BookmarkList>
		/// </summary>
		/// <param name="bookmarkListElement"></param>
		/// <returns></returns>
		private static List<ProjectBookmarkTable> getBookmarkListFromXmlElement(XmlElement bookmarkListElement)
		{
			List<ProjectBookmarkTable> bookmardList = new List<ProjectBookmarkTable>();
			foreach (XmlElement element in bookmarkListElement.ChildNodes)
			{
				switch (element.Name)
				{
					case "ProjectBookmarkTable":
						bookmardList.Add(getProjectBookmarkTableFromxmlElement(element));
						break;
				}
			}

			return bookmardList;
		}

		/// <summary>
		/// XmlElementからProjectBookmarkTable情報を取得する
		/// 例：
		///   <ProjectBookmarkTable>
		///     <ProjectPath>D:\src\krkr\game\abyss\trial\trunk\abyss.krkrproj</ProjectPath>
		///       <List>
		///         <BookmarkInfo>
		///           <LineNumber>11</LineNumber>
		///           <FilePath>D:\src\krkr\game\abyss\trial\trunk\data\main\debug_kagex.ks</FilePath>
		///           <Name>ブックマーク</Name>
		///         </BookmarkInfo>
		///         <BookmarkInfo>
		///           <LineNumber>19</LineNumber>
		///           <FilePath>D:\src\krkr\game\abyss\trial\trunk\data\main\debug_kagex.ks</FilePath>
		///           <Name>ブックマーク</Name>
		///         </BookmarkInfo>
		///       </List>
		///     </ProjectBookmarkTable>
		///     <ProjectBookmarkTable>
		///       <ProjectPath>C:\Users\mry\Desktop\AAA\AAA.krkrproj</ProjectPath>
		///       <List />
		///   </ProjectBookmarkTable>
		/// </summary>
		/// <param name="tableElement"></param>
		/// <returns></returns>
		private static ProjectBookmarkTable getProjectBookmarkTableFromxmlElement(XmlElement tableElement)
		{
			ProjectBookmarkTable projectStringTable = new ProjectBookmarkTable();
			foreach (XmlElement element in tableElement.ChildNodes)
			{
				switch (element.Name)
				{
					case "ProjectPath":
						projectStringTable.ProjectPath = element.InnerText;
						break;
					case "List":
						projectStringTable.List = getBookmarkInfoListFromXmlElement(element);
						break;
				}
			}

			return projectStringTable;
		}

		/// <summary>
		/// XmlElementからBookmarkInfoリストを取得する
		/// 例：
		/// <List>
		///   <BookmarkInfo>
		///     <LineNumber>11</LineNumber>
		///     <FilePath>D:\src\krkr\game\abyss\trial\trunk\data\main\debug_kagex.ks</FilePath>
		///     <Name>ブックマーク</Name>
		///   </BookmarkInfo>
		///   <BookmarkInfo>
		///     <LineNumber>19</LineNumber>
		///     <FilePath>D:\src\krkr\game\abyss\trial\trunk\data\main\debug_kagex.ks</FilePath>
		///     <Name>ブックマーク</Name>
		///   </BookmarkInfo>
		/// </List>
		/// </summary>
		/// <param name="bookmarkInfoElement"></param>
		/// <returns></returns>
		private static List<kkde.search.BookmarkInfo> getBookmarkInfoListFromXmlElement(XmlElement bookmarkInfoElement)
		{
			List<kkde.search.BookmarkInfo> list = new List<kkde.search.BookmarkInfo>();
			foreach (XmlElement element in bookmarkInfoElement.ChildNodes)
			{
				if (element.Name == "BookmarkInfo")
				{
					list.Add(getBookmarkInfoFromXmlElement(element));
				}
			}

			return list;
		}

		/// <summary>
		/// XmlElementからBookmarkInfoを取得する
		/// 例：
		/// <BookmarkInfo>
		///   <LineNumber>19</LineNumber>
		///   <FilePath>D:\src\krkr\game\abyss\trial\trunk\data\main\debug_kagex.ks</FilePath>
		///   <Name>ブックマーク</Name>
		/// </BookmarkInfo>
		/// </summary>
		/// <param name="bookmarkElement"></param>
		/// <returns></returns>
		private static kkde.search.BookmarkInfo getBookmarkInfoFromXmlElement(XmlElement bookmarkElement)
		{
			kkde.search.BookmarkInfo info = new kkde.search.BookmarkInfo();
			foreach (XmlElement element in bookmarkElement.ChildNodes)
			{
				switch (element.Name)
				{
					case "LineNumber":
						info.LineNumber = Int32.Parse(element.InnerText);
						break;
					case "FilePath":
						info.FilePath = element.InnerText;
						break;
					case "Name":
						info.Name = element.InnerText;
						break;
				}
			}

			return info;
		}

		/// <summary>
		/// XmlElementからSearchOptionを取得する
		/// </summary>
		/// <param name="optionElement"></param>
		/// <returns></returns>
		private static kkde.search.EditorSearchOption getSearchOptionFromXmlElement(XmlElement optionElement)
		{
			kkde.search.EditorSearchOption option = new kkde.search.EditorSearchOption();
			foreach (XmlElement element in optionElement.ChildNodes)
			{
				switch (element.Name)
				{
					case "SearchKeywordTable":
						option.SearchKeywordTable = getHistoryTableFromXmlElement(element);
						break;
					case "ReplaceKeywordTable":
						option.ReplaceKeywordTable = getHistoryTableFromXmlElement(element);
						break;
					case "Direction":
						option.Direction = (kkde.search.SearchDirection)Enum.Parse(typeof(kkde.search.SearchDirection), element.InnerText);
						break;
					case "IgnoreCase":
						option.IgnoreCase = Boolean.Parse(element.InnerText);
						break;
					case "WordUnit":
						option.WordUnit = Boolean.Parse(element.InnerText);
						break;
					case "Regex":
						option.Regex = Boolean.Parse(element.InnerText);
						break;
					case "Type":
						option.Type = (kkde.search.SearchType)Enum.Parse(typeof(kkde.search.SearchType), element.InnerText);
						break;
					case "GrepOption":
						option.GrepOption = getGrepOptionFromXmlElement(element);
						break;
				}
			}

			return option;
		}

		/// <summary>
		/// XmlElementからGerpOptionを取得する
		/// </summary>
		/// <param name="element"></param>
		/// <returns></returns>
		private static kkde.search.GrepOption getGrepOptionFromXmlElement(XmlElement grepOptionElement)
		{
			kkde.search.GrepOption option = new kkde.search.GrepOption();
			foreach (XmlElement element in grepOptionElement.ChildNodes)
			{
				switch (element.Name)
				{
					case "FileExtTable":
						option.FileExtTable = getHistoryTableFromXmlElement(element);
						break;
					case "Pos":
						option.Pos = (kkde.search.GrepPotision)Enum.Parse(typeof(kkde.search.GrepPotision), element.InnerText);
						break;
					case "FolderPath":
						option.FolderPath = element.InnerText;
						break;
					case "UseSubFolder":
						option.UseSubFolder = Boolean.Parse(element.InnerText);
						break;
				}
			}

			return option;
		}
		#endregion

		#region XML設定ファイル保存処理メソッド
		/// <summary>
		/// 設定情報をXMLファイルで作成し保存する
		/// </summary>
		/// <param name="filePath"></param>
		/// <param name="option"></param>
		private static void saveToXml(string filePath, EnvOption option)
		{
			using (FileStream fs = new FileStream(filePath, FileMode.Create))
			{
				using (XmlTextWriter xw = new XmlTextWriter(fs, Encoding.UTF8))
				{
					xw.Formatting = Formatting.Indented;

					xw.WriteStartDocument();
					xw.WriteStartElement("EnvOption");

					xw.WriteStartElement("HistoryProject");
					writeXmlHistoryTable(xw, option.HistoryProject);
					xw.WriteEndElement();

					xw.WriteStartElement("HistoryFile");
					writeXmlHistoryTable(xw, option.HistoryFile);
					xw.WriteEndElement();

					xw.WriteStartElement("OpenedList");
					writeXmlOpenedList(xw, option.OpenedList);
					xw.WriteEndElement();

					xw.WriteStartElement("BookmarkList");
					writeXmlBookmarkList(xw, option.BookmarkList);
					xw.WriteEndElement();

					xw.WriteStartElement("SearchOption");
					writeXmlSearchOption(xw, option.SearchOption);
					xw.WriteEndElement();

					xw.WriteStartElement("SearchResultShowDiaglog");
					xw.WriteValue(option.SearchResultShowDiaglog);
					xw.WriteEndElement();

					xw.WriteStartElement("LastProjectPath");
					xw.WriteValue(option.LastProjectPath);
					xw.WriteEndElement();

					xw.WriteStartElement("ProjectOpenedLastProject");
					xw.WriteValue(option.ProjectOpenedLastProject);
					xw.WriteEndElement();

					xw.WriteStartElement("ProjectOpenedLastFile");
					xw.WriteValue(option.ProjectOpenedLastFile);
					xw.WriteEndElement();

					xw.WriteStartElement("ProjectWatchProjectPath");
					xw.WriteValue(option.ProjectWatchProjectPath);
					xw.WriteEndElement();

					xw.WriteStartElement("ExecSaveAll");
					xw.WriteValue(option.ExecSaveAll);
					xw.WriteEndElement();

					xw.WriteStartElement("ExecOpenLog");
					xw.WriteValue(option.ExecOpenLog);
					xw.WriteEndElement();

					xw.WriteStartElement("ExecKrkrKill");
					xw.WriteValue(option.ExecKrkrKill);
					xw.WriteEndElement();

					xw.WriteStartElement("ExecAddClearOption");
					xw.WriteValue(option.ExecAddClearOption);
					xw.WriteEndElement();

					xw.WriteStartElement("CodeComplateSeepTime");
					xw.WriteValue(option.CodeComplateSeepTime);
					xw.WriteEndElement();

					xw.WriteStartElement("WindowStartPos");
					xw.WriteValue(option.WindowStartPos.ToString());
					xw.WriteEndElement();

					xw.WriteStartElement("WindowState");
					xw.WriteValue(option.WindowState.ToString());
					xw.WriteEndElement();

					xw.WriteStartElement("WindowSize");
					writeXmlSize(xw, option.WindowSize);
					xw.WriteEndElement();

					xw.WriteStartElement("ToolBarFilePos");
					writeXmlToolBarPos(xw, option.ToolBarFilePos);
					xw.WriteEndElement();

					xw.WriteStartElement("ToolBarEditPos");
					writeXmlToolBarPos(xw, option.ToolBarEditPos);
					xw.WriteEndElement();

					xw.WriteStartElement("ToolBarSearchPos");
					writeXmlToolBarPos(xw, option.ToolBarSearchPos);
					xw.WriteEndElement();

					xw.WriteStartElement("ToolBarBookmarkPos");
					writeXmlToolBarPos(xw, option.ToolBarBookmarkPos);
					xw.WriteEndElement();

					xw.WriteStartElement("ToolBarExecPos");
					writeXmlToolBarPos(xw, option.ToolBarExecPos);
					xw.WriteEndElement();

					xw.WriteStartElement("ToolBarViewPos");
					writeXmlToolBarPos(xw, option.ToolBarViewPos);
					xw.WriteEndElement();

					writeElement(xw, "HelpHelpWindow", option.HelpHelpWindow);

					xw.WriteEndElement();
					xw.WriteEndDocument();
				}
			}
		}

		private static void writeElement(XmlTextWriter xw, string elementName, object value)
		{
			xw.WriteStartElement(elementName);
			xw.WriteValue(value);
			xw.WriteEndElement();
		}

		private static void writeElement(XmlTextWriter xw, string elementName, Enum e)
		{
			xw.WriteStartElement(elementName);
			xw.WriteValue(e.ToString());
			xw.WriteEndElement();
		}

		private static void writeXmlStringList(XmlTextWriter xw, List<string> list)
		{
			xw.WriteStartElement("List");
			foreach (string text in list)
			{
				writeElement(xw, "string", text);
			}
			xw.WriteEndElement();
		}

		private static void writeXmlHistoryTable(XmlTextWriter xw, HistoryTable historyTable)
		{
			//リスト書き込み
			writeXmlStringList(xw, historyTable.List);
			writeElement(xw, "MaxCount", historyTable.MaxCount);
		}

		private static void writeXmlOpenedList(XmlTextWriter xw, List<ProjectStringTable> list)
		{
			foreach (ProjectStringTable table in list)
			{
				xw.WriteStartElement("ProjectStringTable");
				writeElement(xw, "ProjectPath", table.ProjectPath);
				writeXmlStringList(xw, table.List);	//リスト書き込み
				xw.WriteEndElement();
			}
		}

		private static void writeXmlBookmarkList(XmlTextWriter xw, List<ProjectBookmarkTable> list)
		{
			foreach (ProjectBookmarkTable table in list)
			{
				xw.WriteStartElement("ProjectBookmarkTable");
				writeElement(xw, "ProjectPath", table.ProjectPath);

				xw.WriteStartElement("List");
				foreach (kkde.search.BookmarkInfo info in table.List)
				{
					xw.WriteStartElement("BookmarkInfo");
					writeElement(xw, "LineNumber", info.LineNumber);
					writeElement(xw, "FilePath", info.FilePath);
					writeElement(xw, "Name", info.Name);
					xw.WriteEndElement();
				}
				xw.WriteEndElement();
				xw.WriteEndElement();
			}
		}

		private static void writeXmlSize(XmlTextWriter xw, Size size)
		{
			writeElement(xw, "Width", size.Width);
			writeElement(xw, "Height", size.Height);
		}

		private static void writeXmlToolBarPos(XmlTextWriter xw, ToolBarPos toolBarPos)
		{
			writeElement(xw, "Direction", toolBarPos.Direction.ToString());
			xw.WriteStartElement("Location");
			writeElement(xw, "X", toolBarPos.Location.X);
			writeElement(xw, "Y", toolBarPos.Location.Y);
			xw.WriteEndElement();
		}

		private static void writeXmlSearchOption(XmlTextWriter xw, kkde.search.EditorSearchOption option)
		{
			xw.WriteStartElement("SearchKeywordTable");
			writeXmlHistoryTable(xw, option.SearchKeywordTable);
			xw.WriteEndElement();

			xw.WriteStartElement("ReplaceKeywordTable");
			writeXmlHistoryTable(xw, option.ReplaceKeywordTable);
			xw.WriteEndElement();

			writeElement(xw, "Direction", option.Direction.ToString());
			writeElement(xw, "IgnoreCase", option.IgnoreCase);
			writeElement(xw, "WordUnit", option.WordUnit);
			writeElement(xw, "Regex", option.Regex);
			writeElement(xw, "Type", option.Type.ToString());

			xw.WriteStartElement("GrepOption");
			xw.WriteStartElement("FileExtTable");
			writeXmlHistoryTable(xw, option.GrepOption.FileExtTable);
			xw.WriteEndElement();
			writeElement(xw, "Pos", option.GrepOption.Pos.ToString());
			writeElement(xw, "FolderPath", option.GrepOption.FolderPath);
			writeElement(xw, "UseSubFolder", option.GrepOption.UseSubFolder);
			xw.WriteEndElement();
		}
		#endregion
	}
}
