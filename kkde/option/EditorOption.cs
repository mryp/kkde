using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using ICSharpCode.TextEditor.Document;
using System.Drawing;
using System.Xml;

namespace kkde.option
{
	/// <summary>
	/// エディタ設定を保持するクラス
	/// </summary>
	public class EditorOption
	{
		#region 定数
		public const string LINE_TERM_NAME_CRLF = "CRLF";
		public const string LINE_TERM_NAME_LF = "LF";
		public const string LINE_TERM_NAME_CR = "CR";

		public const string ENCODING_NAME_SHIFT_JIS = "shift_jis";
		public const string ENCODING_NAME_UTF16 = "utf-16";

		public static readonly string[] LINE_TERM_NAME_LIST = { LINE_TERM_NAME_CRLF, LINE_TERM_NAME_LF, LINE_TERM_NAME_CR };
		public static readonly string[] ENCODING_NAME_LIST = { ENCODING_NAME_SHIFT_JIS, ENCODING_NAME_UTF16 };
		#endregion

		#region フィールド
		//返り値用
		Font m_font = null;
		Encoding m_encoding = null;
		string m_lineTerminator = "";

		//エディタプロパティ保存用
		string m_fontName = "ＭＳ ゴシック";
		int m_fontSize = 10;
		string m_encodingName = "shift_jis";
		int m_tabIndent = 4;
		int m_indentationSize = 4;
		IndentStyle m_indentStyle = IndentStyle.None;
		DocumentSelectionMode m_documentSelectionMode = DocumentSelectionMode.Normal;
		BracketMatchingStyle m_bracketMatchingStyle = BracketMatchingStyle.After;
		bool m_allowCaretBeyondEOL = false;
		bool m_showMatchingBracket = true;
		bool m_showLineNumbers = true;
		bool m_showSpaces = false;
		bool m_showTabs = false;
		bool m_showEOLMarker = false;
		bool m_showInvalidLines = false;
		bool m_showWideSpaces = false;
		bool m_isIconBarVisible = true;
		bool m_enableFolding = true;
		bool m_showHorizontalRuler = true;
		bool m_showVerticalRuler = true;
		bool m_convertTabsToSpaces = false;
		bool m_useAntiAliasedFont = false;
		bool m_createBackupCopy = false;
		bool m_mouseWheelScrollDown = true;
		bool m_mouseWheelTextZoom = false;
		bool m_hideMouseCursor = false;
		bool m_cutCopyWholeLine = true;
		int m_verticalRulerRow = 80;
		LineViewerStyle m_lineViewerStyle = LineViewerStyle.None;
		string m_lineTerminatorName = "CRLF";
		bool m_autoInsertCurlyBracket = true;
		bool m_useCustomLine = false;

		//入力補完関連
		bool m_useCodeCompletion = true;
		bool m_parseActionFileSave = false;
		KagCompletionOption m_kagCompOption = new KagCompletionOption();
		#endregion

		#region プロパティ
		/// <summary>
		/// フォント名
		/// </summary>
		public string FontName
		{
			get { return m_fontName; }
			set { m_fontName = value; m_font = null; }
		}

		/// <summary>
		/// フォントサイズ
		/// </summary>
		public int FontSize
		{
			get { return m_fontSize; }
			set { m_fontSize = value; m_font = null; }
		}

		/// <summary>
		/// エンコーディング名
		/// </summary>
		public string EncodingName
		{
			get { return m_encodingName; }
			set { m_encodingName = value; m_encoding = null; }
		}

		/// <summary>
		/// フリーカーソルモード
		/// </summary>
		public bool AllowCaretBeyondEOL
		{
			get { return m_allowCaretBeyondEOL; }
			set { m_allowCaretBeyondEOL = value; }
		}

		/// <summary>
		/// タブインデント時の空白幅数
		/// </summary>
		public int TabIndent
		{
			get { return m_tabIndent; }
			set { m_tabIndent = value; }
		}

		/// <summary>
		/// インデント動作時の空白幅数
		/// </summary>
		public int IndentationSize
		{
			get { return m_indentationSize; }
			set { m_indentationSize = value; }
		}

		/// <summary>
		/// インデントの仕方
		/// </summary>
		public IndentStyle IndentStyle
		{
			get { return m_indentStyle; }
			set { m_indentStyle = value; }
		}

		/// <summary>
		/// 選択モード（複数選択もできるらしいが・・・）
		/// </summary>
		public DocumentSelectionMode DocumentSelectionMode
		{
			get { return m_documentSelectionMode; }
			set { m_documentSelectionMode = value; }
		}

		/// <summary>
		/// ブロックハイライトを行うカーソルの位置
		/// </summary>
		public BracketMatchingStyle BracketMatchingStyle
		{
			get { return m_bracketMatchingStyle; }
			set { m_bracketMatchingStyle = value; }
		}

		/// <summary>
		/// ブロックハイライトを表示するかどうか
		/// </summary>
		public bool ShowMatchingBracket
		{
			get { return m_showMatchingBracket; }
			set { m_showMatchingBracket = value; }
		}

		/// <summary>
		/// 行番号を表示するかどうか
		/// </summary>
		public bool ShowLineNumbers
		{
			get { return m_showLineNumbers; }
			set { m_showLineNumbers = value; }
		}

		/// <summary>
		/// 半角スペース記号を表示するかどうか
		/// </summary>
		public bool ShowSpaces
		{
			get { return m_showSpaces; }
			set { m_showSpaces = value; }
		}

		/// <summary>
		/// 全角スペース記号を表示するかどうか
		/// </summary>
		public bool ShowWideSpaces
		{
			get { return m_showWideSpaces; }
			set { m_showWideSpaces = value; }
		}

		/// <summary>
		/// タブ記号を表示するかどうか
		/// </summary>
		public bool ShowTabs
		{
			get { return m_showTabs; }
			set { m_showTabs = value; }
		}

		/// <summary>
		/// 行末記号を表示するかどうか
		/// </summary>
		public bool ShowEOLMarker
		{
			get { return m_showEOLMarker; }
			set { m_showEOLMarker = value; }
		}

		/// <summary>
		/// EOF以降の文字なしライン記号を表示するかどうか
		/// </summary>
		public bool ShowInvalidLines
		{
			get { return m_showInvalidLines; }
			set { m_showInvalidLines = value; }
		}

		/// <summary>
		/// アイコン表示領域（行番号左の領域）を表示するかどうか
		/// </summary>
		public bool IsIconBarVisible
		{
			get { return m_isIconBarVisible; }
			set { m_isIconBarVisible = value; }
		}

		/// <summary>
		/// 折りたたみ領域を有効にするかどうか
		/// </summary>
		public bool EnableFolding
		{
			get { return m_enableFolding; }
			set { m_enableFolding = value; }
		}

		/// <summary>
		/// 横ルーラーを表示するかどうか
		/// </summary>
		public bool ShowHorizontalRuler
		{
			get { return m_showHorizontalRuler; }
			set { m_showHorizontalRuler = value; }
		}

		/// <summary>
		/// 縦ルーラーを表示するかどうか
		/// </summary>
		public bool ShowVerticalRuler
		{
			get { return m_showVerticalRuler; }
			set { m_showVerticalRuler = value; }
		}

		/// <summary>
		/// タブ入力時に空白記号に置き換えるかどうか
		/// </summary>
		public bool ConvertTabsToSpaces
		{
			get { return m_convertTabsToSpaces; }
			set { m_convertTabsToSpaces = value; }
		}

		/// <summary>
		/// アンチエイリアシングフォントを使用するかどうか
		/// </summary>
		public bool UseAntiAliasedFont
		{
			get { return m_useAntiAliasedFont; }
			set { m_useAntiAliasedFont = value; }
		}

		/// <summary>
		/// バックアップファイルを作成するかどうか
		/// </summary>
		public bool CreateBackupCopy
		{
			get { return m_createBackupCopy; }
			set { m_createBackupCopy = value; }
		}

		/// <summary>
		/// マウスホイールでのスクロール方向（ホイールを下に回転するとスクロールバーが下に移動するかどうか）
		/// （trueのとき通常、falseのとき通常と逆）
		/// </summary>
		public bool MouseWheelScrollDown
		{
			get { return m_mouseWheelScrollDown; }
			set { m_mouseWheelScrollDown = value; }
		}

		/// <summary>
		/// Ctrlキーを押しながらマウスホイールでフォントサイズを変更するかどうか
		/// </summary>
		public bool MouseWheelTextZoom
		{
			get { return m_mouseWheelTextZoom; }
			set { m_mouseWheelTextZoom = value; }
		}

		/// <summary>
		/// 文字入力中にマウスカーソルを隠すかどうか
		/// </summary>
		public bool HideMouseCursor
		{
			get { return m_hideMouseCursor; }
			set { m_hideMouseCursor = value; }
		}

		/// <summary>
		/// コピー・切り取り時に選択文字列がないときはカーソル行をコピー・切り取りする
		/// </summary>
		public bool CutCopyWholeLine
		{
			get { return m_cutCopyWholeLine; }
			set { m_cutCopyWholeLine = value; }
		}

		/// <summary>
		/// 縦ルーラーの位置
		/// </summary>
		public int VerticalRulerRow
		{
			get { return m_verticalRulerRow; }
			set { m_verticalRulerRow = value; }
		}

		/// <summary>
		/// カーソル行の表示スタイル
		/// </summary>
		public LineViewerStyle LineViewerStyle
		{
			get { return m_lineViewerStyle; }
			set { m_lineViewerStyle = value; }
		}

		/// <summary>
		/// 改行記号の種類
		/// </summary>
		public string LineTerminatorName
		{
			get { return m_lineTerminatorName; }
			set { m_lineTerminatorName = value; m_lineTerminator = ""; }
		}

		/// <summary>
		/// ブロック自動挿入（？）
		/// </summary>
		public bool AutoInsertCurlyBracket
		{
			get { return m_autoInsertCurlyBracket; }
			set { m_autoInsertCurlyBracket = value; }
		}

		/// <summary>
		/// カスタム行を使用する
		/// </summary>
		public bool UseCustomLine
		{
			get { return m_useCustomLine; }
			set { m_useCustomLine = value; }
		}

		/// <summary>
		/// 入力補完を行うかどうか
		/// </summary>
		public bool UseCodeCompletion
		{
			get { return m_useCodeCompletion; }
			set { m_useCodeCompletion = value; }
		}

		/// <summary>
		/// ファイル保存時に構文解析を行い、常時は行わない
		/// </summary>
		public bool ParseActionFileSave
		{
			get { return m_parseActionFileSave; }
			set { m_parseActionFileSave = value; }
		}

		/// <summary>
		/// KAG入力補完オプション
		/// </summary>
		public KagCompletionOption KagCompOption
		{
			get { return m_kagCompOption; }
			set { m_kagCompOption = value; }
		}

		#endregion

		#region 読み取り専用プロパティ
		/// <summary>
		/// フォントオブジェクト（読み取り専用）
		/// </summary>
		public Font Font
		{
			get
			{
				if (m_font == null)
				{
					m_font = new Font(m_fontName, m_fontSize);
				}

				return m_font;
			}
		}

		/// <summary>
		/// エンコーディング（読み取り専用）
		/// </summary>
		public Encoding Encoding
		{
			get
			{
				if (m_encoding == null)
				{
					m_encoding = System.Text.Encoding.GetEncoding(m_encodingName);
				}

				return m_encoding;
			}
		}

		/// <summary>
		/// 改行文字（読み取り専用）
		/// </summary>
		public string LineTerminator
		{
			get 
			{
				if (m_lineTerminator == "")
				{
					switch (m_lineTerminatorName)
					{
						case "CRLF":
							m_lineTerminator = "\r\n";
							break;
						case "CR":
							m_lineTerminator = "\r";
							break;
						case "LF":
							m_lineTerminator = "\n";
							break;
						default:
							m_lineTerminator = "\r\n";
							break;
					}
				}

				return m_lineTerminator; 
			}
		}
		#endregion

		#region 初期化メソッド
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public EditorOption()
		{
		}
		#endregion

		#region クラスメソッド
		/// <summary>
		/// 環境設定をファイルに保存する
		/// </summary>
		/// <param name="fileName">作成する環境設定ファイル名</param>
		/// <param name="option">保存する環境設定</param>
		public static void SaveFile(string fileName, EditorOption option)
		{
			//設定をファイルに保存する
			//XmlSerializer serializer = new XmlSerializer(typeof(EditorOption));
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
				util.Msgbox.Error("エディタ設定ファイルの書き込みに失敗しました\n" + err.Message);
			}
		}

		/// <summary>
		/// 環境設定をファイルから読み込む
		/// </summary>
		/// <param name="fileName">読み込む環境設定ファイル名</param>
		/// <returns>読み込んだ環境設定情報</returns>
		public static EditorOption LoadFile(string fileName)
		{
			if (File.Exists(fileName) == false)
			{
				//読み込むファイルがないときはとりあえずデフォルトでファイルを作成する
				SaveFile(fileName, new EditorOption());
			}

			////設定ファイルを読み込み自分自身にコピーする
			//XmlSerializer serializer = new XmlSerializer(typeof(EditorOption));
			//FileStream fs;
			//EditorOption option = null;
			//using (fs = new FileStream(fileName, FileMode.Open))
			//{
			//    option = (EditorOption)serializer.Deserialize(fs);
			//}

			//return option;

			EditorOption option = null;
			try
			{
				option = loadFromXml(fileName);
			}
			catch (Exception err)
			{
				util.Msgbox.Error("エディタ設定ファイル" + Path.GetFileName(fileName) + "の読み込みに失敗しました\n" + err.Message);
				option = new EditorOption();	//とりあえずデフォルトをセット
			}

			return option;
		}

		/// <summary>
		/// デフォルトのオプションを取得する
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static EditorOption GetDefault(kkde.project.FileType.KrkrType type)
		{
			EditorOption option = null;
			switch (type)
			{
				case kkde.project.FileType.KrkrType.Kag:
					option = getDefaultKagFile();
					break;
				case kkde.project.FileType.KrkrType.Tjs:
					option = getDefaultTjsFile();
					break;
				default:
					option = getDefaultUnknownFile();
					break;
			}

			return option;
		}

		/// <summary>
		/// KAGファイルのデフォルトオプションを取得する
		/// </summary>
		/// <returns></returns>
		private static EditorOption getDefaultKagFile()
		{
			EditorOption option = new EditorOption();

			option.FontName = "ＭＳ ゴシック";
			option.FontSize = 10;
			option.EncodingName = "shift_jis";
			option.TabIndent = 4;
			option.IndentationSize = 4;
			option.IndentStyle = IndentStyle.None;
			option.DocumentSelectionMode = DocumentSelectionMode.Normal;
			option.BracketMatchingStyle = BracketMatchingStyle.After;
			option.AllowCaretBeyondEOL = false;
			option.ShowMatchingBracket = true;
			option.ShowLineNumbers = true;
			option.ShowSpaces = false;
			option.ShowTabs = true;
			option.ShowEOLMarker = true;
			option.ShowInvalidLines = false;
			option.ShowWideSpaces = true;
			option.IsIconBarVisible = true;
			option.EnableFolding = true;
			option.ShowHorizontalRuler = true;
			option.ShowVerticalRuler = true;
			option.ConvertTabsToSpaces = false;
			option.UseAntiAliasedFont = false;
			option.CreateBackupCopy = false;
			option.MouseWheelScrollDown = true;
			option.MouseWheelTextZoom = false;
			option.HideMouseCursor = false;
			option.CutCopyWholeLine = true;
			option.VerticalRulerRow = 80;
			option.LineViewerStyle = LineViewerStyle.None;
			option.LineTerminatorName = "CRLF";
			option.AutoInsertCurlyBracket = false;
			option.UseCustomLine = false;
			option.UseCodeCompletion = true;
			option.ParseActionFileSave = false;

			return option;
		}

		/// <summary>
		/// TJSファイルのデフォルトオプションを取得する
		/// </summary>
		/// <returns></returns>
		private static EditorOption getDefaultTjsFile()
		{
			EditorOption option = new EditorOption();

			option.FontName = "ＭＳ ゴシック";
			option.FontSize = 10;
			option.EncodingName = "shift_jis";
			option.TabIndent = 4;
			option.IndentationSize = 4;
			option.IndentStyle = IndentStyle.Auto;
			option.DocumentSelectionMode = DocumentSelectionMode.Normal;
			option.BracketMatchingStyle = BracketMatchingStyle.After;
			option.AllowCaretBeyondEOL = false;
			option.ShowMatchingBracket = true;
			option.ShowLineNumbers = true;
			option.ShowSpaces = false;
			option.ShowTabs = true;
			option.ShowEOLMarker = true;
			option.ShowInvalidLines = false;
			option.ShowWideSpaces = true;
			option.IsIconBarVisible = true;
			option.EnableFolding = true;
			option.ShowHorizontalRuler = true;
			option.ShowVerticalRuler = false;
			option.ConvertTabsToSpaces = false;
			option.UseAntiAliasedFont = false;
			option.CreateBackupCopy = false;
			option.MouseWheelScrollDown = true;
			option.MouseWheelTextZoom = false;
			option.HideMouseCursor = false;
			option.CutCopyWholeLine = true;
			option.VerticalRulerRow = 80;
			option.LineViewerStyle = LineViewerStyle.None;
			option.LineTerminatorName = "CRLF";
			option.AutoInsertCurlyBracket = true;
			option.UseCustomLine = false;
			option.UseCodeCompletion = true;
			option.ParseActionFileSave = false;

			return option;
		}

		/// <summary>
		/// そのほかのファイルのデフォルトオプションを取得する
		/// </summary>
		/// <returns></returns>
		private static EditorOption getDefaultUnknownFile()
		{
			EditorOption option = new EditorOption();

			option.FontName = "ＭＳ ゴシック";
			option.FontSize = 10;
			option.EncodingName = "shift_jis";
			option.TabIndent = 4;
			option.IndentationSize = 4;
			option.IndentStyle = IndentStyle.None;
			option.DocumentSelectionMode = DocumentSelectionMode.Normal;
			option.BracketMatchingStyle = BracketMatchingStyle.After;
			option.AllowCaretBeyondEOL = false;
			option.ShowMatchingBracket = true;
			option.ShowLineNumbers = true;
			option.ShowSpaces = false;
			option.ShowTabs = false;
			option.ShowEOLMarker = true;
			option.ShowInvalidLines = false;
			option.ShowWideSpaces = false;
			option.IsIconBarVisible = true;
			option.EnableFolding = true;
			option.ShowHorizontalRuler = true;
			option.ShowVerticalRuler = false;
			option.ConvertTabsToSpaces = false;
			option.UseAntiAliasedFont = false;
			option.CreateBackupCopy = false;
			option.MouseWheelScrollDown = true;
			option.MouseWheelTextZoom = false;
			option.HideMouseCursor = false;
			option.CutCopyWholeLine = true;
			option.VerticalRulerRow = 80;
			option.LineViewerStyle = LineViewerStyle.None;
			option.LineTerminatorName = "CRLF";
			option.AutoInsertCurlyBracket = true;
			option.UseCustomLine = false;
			option.UseCodeCompletion = false;
			option.ParseActionFileSave = false;

			return option;
		}

		/// <summary>
		/// 改行文字から改行コードを生成する
		/// </summary>
		/// <param name="lineTerm"></param>
		/// <returns></returns>
		public static string GetLineTermName(string lineTerm)
		{
			string name = "";
			switch (lineTerm)
			{
				case "\r":
					name = LINE_TERM_NAME_CR;
					break;
				case "\n":
					name =  LINE_TERM_NAME_LF;
					break;
				case "\r\n":
					name = LINE_TERM_NAME_CRLF;
					break;
				default:
					name = LINE_TERM_NAME_CRLF;
					break;
			}

			return name;
		}
		#endregion

		#region XMLファイルの読み込みメソッド
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		private static EditorOption loadFromXml(string filePath)
		{
			EditorOption option = new EditorOption();
			FileStream fs;
			using (fs = new FileStream(filePath, FileMode.Open))
			{
				XmlDocument doc = new XmlDocument();
				doc.Load(fs);

				//ルートノードを読み込む
				XmlNodeList nodeList = doc.GetElementsByTagName("EditorOption");
				if (nodeList == null || nodeList.Count == 0)
				{
					throw new ApplicationException("環境設定ファイル読み込みエラー\nこのファイルは不正な設定ファイルです");
				}

				//設定値を読み込む
				foreach (XmlElement element in nodeList[0].ChildNodes)
				{
					switch (element.Name)
					{
						//デフォルト共通
						case "FontName":
							option.FontName = element.InnerText;
							break;
						case "FontSize":
							option.FontSize = Int32.Parse(element.InnerText);
							break;
						case "EncodingName":
							option.EncodingName = element.InnerText;
							break;
						case "AllowCaretBeyondEOL":
							option.AllowCaretBeyondEOL = Boolean.Parse(element.InnerText);
							break;
						case "TabIndent":
							option.TabIndent = Int32.Parse(element.InnerText);
							break;
						case "IndentationSize":
							option.IndentationSize = Int32.Parse(element.InnerText);
							break;
						case "IndentStyle":
							option.IndentStyle = (IndentStyle)Enum.Parse(typeof(IndentStyle), element.InnerText);
							break;
						case "DocumentSelectionMode":
							option.DocumentSelectionMode = (DocumentSelectionMode)Enum.Parse(typeof(DocumentSelectionMode), element.InnerText);
							break;
						case "BracketMatchingStyle":
							option.BracketMatchingStyle = (BracketMatchingStyle)Enum.Parse(typeof(BracketMatchingStyle), element.InnerText);
							break;
						case "ShowMatchingBracket":
							option.ShowMatchingBracket = Boolean.Parse(element.InnerText);
							break;
						case "ShowLineNumbers":
							option.ShowLineNumbers = Boolean.Parse(element.InnerText);
							break;
						case "ShowSpaces":
							option.ShowSpaces = Boolean.Parse(element.InnerText);
							break;
						case "ShowWideSpaces":
							option.ShowWideSpaces = Boolean.Parse(element.InnerText);
							break;
						case "ShowTabs":
							option.ShowTabs = Boolean.Parse(element.InnerText);
							break;
						case "ShowEOLMarker":
							option.ShowEOLMarker = Boolean.Parse(element.InnerText);
							break;
						case "ShowInvalidLines":
							option.ShowInvalidLines = Boolean.Parse(element.InnerText);
							break;
						case "IsIconBarVisible":
							option.IsIconBarVisible = Boolean.Parse(element.InnerText);
							break;
						case "EnableFolding":
							option.EnableFolding = Boolean.Parse(element.InnerText);
							break;
						case "ShowHorizontalRuler":
							option.ShowHorizontalRuler = Boolean.Parse(element.InnerText);
							break;
						case "ShowVerticalRuler":
							option.ShowVerticalRuler = Boolean.Parse(element.InnerText);
							break;
						case "ConvertTabsToSpaces":
							option.ConvertTabsToSpaces = Boolean.Parse(element.InnerText);
							break;
						case "UseAntiAliasedFont":
							option.UseAntiAliasedFont = Boolean.Parse(element.InnerText);
							break;
						case "CreateBackupCopy":
							option.CreateBackupCopy = Boolean.Parse(element.InnerText);
							break;
						case "MouseWheelScrollDown":
							option.MouseWheelScrollDown = Boolean.Parse(element.InnerText);
							break;
						case "MouseWheelTextZoom":
							option.MouseWheelTextZoom = Boolean.Parse(element.InnerText);
							break;
						case "HideMouseCursor":
							option.HideMouseCursor = Boolean.Parse(element.InnerText);
							break;
						case "CutCopyWholeLine":
							option.CutCopyWholeLine = Boolean.Parse(element.InnerText);
							break;
						case "VerticalRulerRow":
							option.VerticalRulerRow = Int32.Parse(element.InnerText);
							break;
						case "LineViewerStyle":
							option.LineViewerStyle = (LineViewerStyle)Enum.Parse(typeof(LineViewerStyle), element.InnerText);
							break;
						case "LineTerminatorName":
							option.LineTerminatorName = element.InnerText;
							break;
						case "AutoInsertCurlyBracket":
							option.AutoInsertCurlyBracket = Boolean.Parse(element.InnerText);
							break;
						case "UseCustomLine":
							option.UseCustomLine = Boolean.Parse(element.InnerText);
							break;
						case "UseCodeCompletion":
							option.UseCodeCompletion = Boolean.Parse(element.InnerText);
							break;
						case "ParseActionFileSave":
							option.ParseActionFileSave = Boolean.Parse(element.InnerText);
							break;
						case "KagCompOption":
							option.KagCompOption = getKagCompOptionFromXmlElement(element);
							break;
					}
				}
			}

			return option;
		}

		private static KagCompletionOption getKagCompOptionFromXmlElement(XmlElement kagOptionElement)
		{
			KagCompletionOption option = new KagCompletionOption();
			foreach (XmlElement element in kagOptionElement.ChildNodes)
			{
				switch (element.Name)
				{
					case "UseAttrValueDqRegion":
						option.UseAttrValueDqRegion = Boolean.Parse(element.InnerText);
						break;
					case "ScenarioFileExt":
						option.ScenarioFileExt = element.InnerText;
						break;
					case "ImageFileExt":
						option.ImageFileExt = element.InnerText;
						break;
					case "SeFileExt":
						option.SeFileExt = element.InnerText;
						break;
					case "CursorFileExt":
						option.CursorFileExt = element.InnerText;
						break;
					case "BgmFileExt":
						option.BgmFileExt = element.InnerText;
						break;
					case "ActionFileExt":
						option.ActionFileExt = element.InnerText;
						break;
					case "PluginFileExt":
						option.PluginFileExt = element.InnerText;
						break;
					case "FontFileExt":
						option.FontFileExt = element.InnerText;
						break;
					case "VideoFileExt":
						option.VideoFileExt = element.InnerText;
						break;
					case "ZeroOverNumberList":
						option.ZeroOverNumberList = element.InnerText;
						break;
					case "OneOverNumberList":
						option.OneOverNumberList = element.InnerText;
						break;
					case "PercentNumberList":
						option.PercentNumberList = element.InnerText;
						break;
					case "ByteNumberList":
						option.ByteNumberList = element.InnerText;
						break;
					case "MsTimeNumberList":
						option.MsTimeNumberList = element.InnerText;
						break;
					case "RealNumberList":
						option.RealNumberList = element.InnerText;
						break;
					case "PmHundredNumberList":
						option.PmHundredNumberList = element.InnerText;
						break;
					case "RgbNumberList":
						option.RgbNumberList = element.InnerText;
						break;
					case "OtherStringList":
						option.OtherStringList = element.InnerText;
						break;
					case "TjsStringList":
						option.TjsStringList = element.InnerText;
						break;
					case "FontStringList":
						option.FontStringList = element.InnerText;
						break;
					case "LayerMaxNumber":
						option.LayerMaxNumber = Int32.Parse(element.InnerText);
						break;
					case "MessageLayerMaxNumber":
						option.MessageLayerMaxNumber = Int32.Parse(element.InnerText);
						break;
					case "SeBufferMaxNumber":
						option.SeBufferMaxNumber = Int32.Parse(element.InnerText);
						break;
					case "VideoBufferMaxNumber":
						option.VideoBufferMaxNumber = Int32.Parse(element.InnerText);
						break;
					case "BaseLayerList":
						option.BaseLayerList = element.InnerText;
						break;
					case "BoolValueList":
						option.BoolValueList = element.InnerText;
						break;
					case "LayerPageList":
						option.LayerPageList = element.InnerText;
						break;
					case "LayerPosList":
						option.LayerPosList = element.InnerText;
						break;
					case "CursorDefList":
						option.CursorDefList = element.InnerText;
						break;
					case "ColorcompModeList":
						option.ColorcompModeList = element.InnerText;
						break;
					case "KagexAction":
						option.KagexAction = element.InnerText;
						break;
					case "KagexLtbType":
						option.KagexLtbType = element.InnerText;
						break;					
					case "IsInsertedTagCopyExWorldEx":
						option.IsInsertedTagCopyExWorldEx = Boolean.Parse(element.InnerText);
						break;
					case "IsAddedTagSignWorldEx":
						option.IsAddedTagSignWorldEx = Boolean.Parse(element.InnerText);
						break;
					case "IsAddedMsgTagWorldEx":
						option.IsAddedMsgTagWorldEx = Boolean.Parse(element.InnerText);
						break;
					case "WorldExSearchPathChar":
						option.WorldExSearchPathChar = element.InnerText;
						break;
					case "WorldExSearchPathEvent":
						option.WorldExSearchPathEvent = element.InnerText;
						break;
					case "WorldExSearchPathStage":
						option.WorldExSearchPathStage = element.InnerText;
						break;
					case "WorldExSearchPathBgm":
						option.WorldExSearchPathBgm = element.InnerText;
						break;
					case "WorldExSearchPathSe":
						option.WorldExSearchPathSe = element.InnerText;
						break;
					case "WorldExCopyTagType":
						option.WorldExCopyTagType = (KagCompletionOption.TagType)Enum.Parse(typeof(KagCompletionOption.TagType), element.InnerText);
						break;
					case "WorldExDoubleDef":
						option.WorldExDoubleDef = (KagCompletionOption.WorldExViewDCOption)Enum.Parse(typeof(KagCompletionOption.WorldExViewDCOption), element.InnerText);
						break;
				}
			}

			return option;
		}
		#endregion

		#region XML設定ファイル書き込みメソッド
		/// <summary>
		/// 設定ファイルをXMLに保存する
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="option"></param>
		private static void saveToXml(string filePath, EditorOption option)
		{
			using (FileStream fs = new FileStream(filePath, FileMode.Create))
			{
				using (XmlTextWriter xw = new XmlTextWriter(fs, Encoding.UTF8))
				{
					xw.Formatting = Formatting.Indented;

					xw.WriteStartDocument();
					xw.WriteStartElement("EditorOption");

					writeElement(xw, "FontName", option.FontName);
					writeElement(xw, "FontSize", option.FontSize);
					writeElement(xw, "EncodingName", option.EncodingName);
					writeElement(xw, "AllowCaretBeyondEOL", option.AllowCaretBeyondEOL);
					writeElement(xw, "TabIndent", option.TabIndent);
					writeElement(xw, "IndentationSize", option.IndentationSize);
					writeElement(xw, "IndentStyle", option.IndentStyle);
					writeElement(xw, "DocumentSelectionMode", option.DocumentSelectionMode);
					writeElement(xw, "BracketMatchingStyle", option.BracketMatchingStyle);
					writeElement(xw, "ShowMatchingBracket", option.ShowMatchingBracket);
					writeElement(xw, "ShowLineNumbers", option.ShowLineNumbers);
					writeElement(xw, "ShowSpaces", option.ShowSpaces);
					writeElement(xw, "ShowWideSpaces", option.ShowWideSpaces);
					writeElement(xw, "ShowTabs", option.ShowTabs);
					writeElement(xw, "ShowEOLMarker", option.ShowEOLMarker);
					writeElement(xw, "ShowInvalidLines", option.ShowInvalidLines);
					writeElement(xw, "IsIconBarVisible", option.IsIconBarVisible);
					writeElement(xw, "EnableFolding", option.EnableFolding);
					writeElement(xw, "ShowHorizontalRuler", option.ShowHorizontalRuler);
					writeElement(xw, "ShowVerticalRuler", option.ShowVerticalRuler);
					writeElement(xw, "ConvertTabsToSpaces", option.ConvertTabsToSpaces);
					writeElement(xw, "UseAntiAliasedFont", option.UseAntiAliasedFont);
					writeElement(xw, "CreateBackupCopy", option.CreateBackupCopy);
					writeElement(xw, "MouseWheelScrollDown", option.MouseWheelScrollDown);
					writeElement(xw, "MouseWheelTextZoom", option.MouseWheelTextZoom);
					writeElement(xw, "HideMouseCursor", option.HideMouseCursor);
					writeElement(xw, "CutCopyWholeLine", option.CutCopyWholeLine);
					writeElement(xw, "VerticalRulerRow", option.VerticalRulerRow);
					writeElement(xw, "LineViewerStyle", option.LineViewerStyle);
					writeElement(xw, "LineTerminatorName", option.LineTerminatorName);
					writeElement(xw, "AutoInsertCurlyBracket", option.AutoInsertCurlyBracket);
					writeElement(xw, "UseCustomLine", option.UseCustomLine);
					writeElement(xw, "UseCodeCompletion", option.UseCodeCompletion);
					writeElement(xw, "ParseActionFileSave", option.ParseActionFileSave);

					xw.WriteStartElement("KagCompOption");
					writeElement(xw, "UseAttrValueDqRegion", option.KagCompOption.UseAttrValueDqRegion);
					writeElement(xw, "ScenarioFileExt", option.KagCompOption.ScenarioFileExt);
					writeElement(xw, "ImageFileExt", option.KagCompOption.ImageFileExt);
					writeElement(xw, "SeFileExt", option.KagCompOption.SeFileExt);
					writeElement(xw, "CursorFileExt", option.KagCompOption.CursorFileExt);
					writeElement(xw, "BgmFileExt", option.KagCompOption.BgmFileExt);
					writeElement(xw, "ActionFileExt", option.KagCompOption.ActionFileExt);
					writeElement(xw, "PluginFileExt", option.KagCompOption.PluginFileExt);
					writeElement(xw, "FontFileExt", option.KagCompOption.FontFileExt);
					writeElement(xw, "VideoFileExt", option.KagCompOption.VideoFileExt);
					writeElement(xw, "ZeroOverNumberList", option.KagCompOption.ZeroOverNumberList);
					writeElement(xw, "OneOverNumberList", option.KagCompOption.OneOverNumberList);
					writeElement(xw, "PercentNumberList", option.KagCompOption.PercentNumberList);
					writeElement(xw, "ByteNumberList", option.KagCompOption.ByteNumberList);
					writeElement(xw, "MsTimeNumberList", option.KagCompOption.MsTimeNumberList);
					writeElement(xw, "RealNumberList", option.KagCompOption.RealNumberList);
					writeElement(xw, "PmHundredNumberList", option.KagCompOption.PmHundredNumberList);
					writeElement(xw, "RgbNumberList", option.KagCompOption.RgbNumberList);
					writeElement(xw, "OtherStringList", option.KagCompOption.OtherStringList);
					writeElement(xw, "TjsStringList", option.KagCompOption.TjsStringList);
					writeElement(xw, "FontStringList", option.KagCompOption.FontStringList);
					writeElement(xw, "LayerMaxNumber", option.KagCompOption.LayerMaxNumber);
					writeElement(xw, "MessageLayerMaxNumber", option.KagCompOption.MessageLayerMaxNumber);
					writeElement(xw, "SeBufferMaxNumber", option.KagCompOption.SeBufferMaxNumber);
					writeElement(xw, "VideoBufferMaxNumber", option.KagCompOption.VideoBufferMaxNumber);
					writeElement(xw, "BaseLayerList", option.KagCompOption.BaseLayerList);
					writeElement(xw, "BoolValueList", option.KagCompOption.BoolValueList);
					writeElement(xw, "LayerPageList", option.KagCompOption.LayerPageList);
					writeElement(xw, "LayerPosList", option.KagCompOption.LayerPosList);
					writeElement(xw, "CursorDefList", option.KagCompOption.CursorDefList);
					writeElement(xw, "ColorcompModeList", option.KagCompOption.ColorcompModeList);
					writeElement(xw, "KagexAction", option.KagCompOption.KagexAction);
					writeElement(xw, "KagexLtbType", option.KagCompOption.KagexLtbType);

					writeElement(xw, "IsInsertedTagCopyExWorldEx", option.KagCompOption.IsInsertedTagCopyExWorldEx);
					writeElement(xw, "IsAddedTagSignWorldEx", option.KagCompOption.IsAddedTagSignWorldEx);
					writeElement(xw, "IsAddedMsgTagWorldEx", option.KagCompOption.IsAddedMsgTagWorldEx);
					writeElement(xw, "WorldExSearchPathChar", option.KagCompOption.WorldExSearchPathChar);
					writeElement(xw, "WorldExSearchPathEvent", option.KagCompOption.WorldExSearchPathEvent);
					writeElement(xw, "WorldExSearchPathStage", option.KagCompOption.WorldExSearchPathStage);
					writeElement(xw, "WorldExSearchPathBgm", option.KagCompOption.WorldExSearchPathBgm);
					writeElement(xw, "WorldExSearchPathSe", option.KagCompOption.WorldExSearchPathSe);
					writeElement(xw, "WorldExCopyTagType", option.KagCompOption.WorldExCopyTagType);
					writeElement(xw, "WorldExDoubleDef", option.KagCompOption.WorldExDoubleDef);
					xw.WriteEndElement();

					xw.WriteEndElement();
					xw.WriteEndDocument();
				}
			}
		}

		private static void writeElement(XmlTextWriter xw, string elementName, Enum e)
		{
			xw.WriteStartElement(elementName);
			xw.WriteValue(e.ToString());
			xw.WriteEndElement();
		}

		private static void writeElement(XmlTextWriter xw, string elementName, object value)
		{
			xw.WriteStartElement(elementName);
			xw.WriteValue(value);
			xw.WriteEndElement();
		}
		#endregion
	}
}
