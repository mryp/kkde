using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace kkde.project
{
	/// <summary>
	/// プロジェクトの種類
	/// </summary>
	public enum ProjectType
	{
		/// <summary>
		/// 空のプロジェクト
		/// </summary>
		Empty,
		/// <summary>
		/// KAG3プロジェクト
		/// </summary>
		Kag3,
		/// <summary>
		/// KAGEX++プロジェクト
		/// </summary>
		Kagexpp
	}

	/// <summary>
	/// プロジェクトファイルの中身を定義するクラス
	/// </summary>
	public class ProjectFile
	{
		#region 定数
		/// <summary>
		/// プロジェクトのファイルの拡張子（ドットなし）を返す
		/// </summary>
		public const string PROJECT_EXT_NAME = "krkrproj";

		/// <summary>
		/// データフォルダのデフォルト相対パス（プロジェクトファイル保存ディレクトリから見たとき）
		/// </summary>
		private const string DEF_DATA_DIR_NAME = "data";

		/// <summary>
		/// 実行ファイルのデフォルト相対パス（プロジェクトファイル保存ディレクトリから見たとき）
		/// </summary>
		private const string DEF_EXE_FILE_NAME = "krkr.eXe";

		/// <summary>
		/// 実行パラメーターのデフォルト値
		/// </summary>
		private const string DEF_EXE_ARGV = "-kkde_debug";

		/// <summary>
		/// 実行エラー時のログファイルの相対パス（EXEファイルから見たとき）
		/// </summary>
		private const string DEF_LOG_FILE_NAME = @"savedata\krkr.console.log";
		#endregion

		#region フィールド
		string m_name = "";
		string m_datapath = "";
		string m_exepath = "";
		string m_filepath = "";
		ProjectType m_type = ProjectType.Empty;
		string m_exeArgv = "";
		#endregion

		#region プロパティ
		/// <summary>
		/// プロジェクト名
		/// </summary>
		public string Name
		{
			get { return m_name; }
			set { m_name = value; }
		}

		/// <summary>
		/// データフォルダ（data）パス
		/// </summary>
		public string DataPath
		{
			get { return m_datapath; }
			set { m_datapath = value; }
		}

		/// <summary>
		/// 実行ファイル（krkr.eXe）パス
		/// </summary>
		public string ExePath
		{
			get { return m_exepath; }
			set { m_exepath = value; }
		}

		/// <summary>
		/// 実行パラメーター（実行時に吉里吉里に渡すパラメーター）
		/// </summary>
		public string ExeArgv
		{
			get { return m_exeArgv; }
			set { m_exeArgv = value; }
		}

		/// <summary>
		/// データフォルダのフルパス
		/// </summary>
		[System.Xml.Serialization.XmlIgnoreAttribute]
		public string DataFullPath
		{
			get	{ return toFullPath(m_datapath); }
			set { m_datapath = toRelativaPath(value); }
		}

		/// <summary>
		/// 実行ファイル（krkr.exe）のフルパス
		/// </summary>
		[System.Xml.Serialization.XmlIgnoreAttribute]
		public string ExeFullPath
		{
			get { return toFullPath(m_exepath);	}
			set { m_exepath = toRelativaPath(value); }
		}

		/// <summary>
		/// プロジェクトファイル
		/// </summary>
		public string FilePath
		{
			get { return m_filepath; }
		}

		/// <summary>
		/// プロジェクトディレクトリパス
		/// </summary>
		public string DirPath
		{
			get
			{
				if (m_filepath == "")
				{
				    return "";
				}
				return Path.GetDirectoryName(m_filepath);
			}
		}
		
		/// <summary>
		/// プロジェクトの種類
		/// </summary>
		public ProjectType Type
		{
			get { return m_type; }
			set { m_type = value; }
		}

		/// <summary>
		/// ログファイルパス（読み取り専用）
		/// </summary>
		public string LogFilePath
		{
			get
			{
				if (this.ExeFullPath == "")
				{
					return "";
				}
				return Path.Combine(Path.GetDirectoryName(this.ExeFullPath), DEF_LOG_FILE_NAME);
			}
		}

		/// <summary>
		/// プロジェクトファイルを開いているかどうか（読み取り専用）
		/// 開いているときtrueを返す
		/// </summary>
		public bool IsOpened
		{
			get
			{
				if (m_name == "" || m_datapath == "" || m_exepath == "")
				{
					return false;	//必要な物がセットされていないので開いていない
				}

				return true;	//開いている
			}
		}
		#endregion

		#region メソッド
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ProjectFile()
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="filePath">作成するファイルパス</param>
		public ProjectFile(string filePath)
		{
			m_filepath = filePath;
		}

		/// <summary>
		/// プロジェクトデータをセットする
		/// </summary>
		/// <param name="name">プロジェクト名</param>
		/// <param name="type">プロジェクトのシステムタイプ</param>
		/// <param name="dirPath">プロジェクトファイルのあるディレクトリパス</param>
		public void SetData(string name, ProjectType type, string dirPath)
		{
			m_name = name;
			m_filepath = Path.Combine(dirPath, String.Format("{0}.{1}", name, PROJECT_EXT_NAME));
			m_datapath = DEF_DATA_DIR_NAME;
			m_exepath = DEF_EXE_FILE_NAME;
			m_type = type;
			m_exeArgv = DEF_EXE_ARGV;
		}

		/// <summary>
		/// プロジェクトファイルを作成する
		/// </summary>
		/// <param name="fileName">作成するプロジェクトファイル名</param>
		public void Save(string fileName)
		{
			//プロジェクトファイルを作成する
			//m_filepath = fileName;
			//XmlSerializer serializer = new XmlSerializer(typeof(ProjectFile));
			//FileStream fs;
			//using (fs = new FileStream(fileName, FileMode.Create))
			//{
			//    serializer.Serialize(fs, this);
			//}

			m_filepath = fileName;
			saveToXml(m_filepath);
		}

		/// <summary>
		/// プロジェクトファイルを読み込む
		/// </summary>
		/// <param name="fileName">読み込むプロジェクトファイル名</param>
		public bool Load(string fileName)
		{
			//プロジェクトファイルを読み込み自分自身にコピーする
			//m_filepath = fileName;
			//XmlSerializer serializer = new XmlSerializer(typeof(ProjectFile));
			//FileStream fs;
			//using (fs = new FileStream(fileName, FileMode.Open))
			//{
			//    ProjectFile file = (ProjectFile)serializer.Deserialize(fs);
			//    this.m_name = file.Name;
			//    this.m_datapath = file.DataPath;
			//    this.m_exepath = file.ExePath;
			//    this.m_type = file.Type;
			//    this.m_exeArgv = file.m_exeArgv;
			//}

			//XMLファイルを読み込む
			m_filepath = fileName;
			loadFromXml(m_filepath);
			return true;
		}

		/// <summary>
		/// デバッグ用文字列を返す
		/// </summary>
		public override string ToString()
		{
			string text = "";
			text += "プロジェクト名: " + Name + "\n";
			text += "データフォルダパス: " + DataPath + "\n";
			text += "実行ファイルパス: " + ExePath + "\n";
			text += "タイプ: " + Type.ToString();
			text += "実行パラメータ: " + ExeArgv + "\n";

			return text;
		}

		/// <summary>
		/// プロジェクトファイルからの相対パスを絶対パスに変換する
		/// </summary>
		/// <param name="path">相対パス</param>
		/// <returns>絶対パス</returns>
		private string toFullPath(string path)
		{
			if (DirPath == "")
			{
				return path;
			}

			Uri basePath = new Uri(DirPath + "\\");
			Uri fullPath = new Uri(basePath, path);

			return fullPath.LocalPath;
		}

		/// <summary>
		/// 絶対パスをプロジェクトファイルからの相対パスに変換する
		/// </summary>
		/// <param name="path">絶対パス</param>
		/// <returns>相対パス</returns>
		private string toRelativaPath(string path)
		{
			if (DirPath == "")
			{
				return path;
			}

			Uri u1 = new Uri(DirPath + "\\");
			Uri u2 = new Uri(u1, path);
			string relativePath = u1.MakeRelativeUri(u2).ToString();
			relativePath = System.Web.HttpUtility.UrlDecode(relativePath).Replace('/', '\\');	//URLデコードして、'/'を'\'に変更する

			return relativePath;
		}
		#endregion

		#region プロジェクトファイルの読み込み・書き込み関連メソッド
		/// <summary>
		/// XMLファイルから設定値を読み込む
		/// </summary>
		/// <param name="fileName"></param>
		private void loadFromXml(string fileName)
		{
			FileStream fs;
			using (fs = new FileStream(fileName, FileMode.Open))
			{
				XmlDocument doc = new XmlDocument();
				doc.Load(fs);

				//ルートノードを読み込む
				XmlNodeList nodeList = doc.GetElementsByTagName("ProjectFile");
				if (nodeList == null || nodeList.Count == 0)
				{
					throw new ApplicationException("プロジェクトファイル読み込みエラー\nこのファイルは不正なプロジェクトファイルです");
				}

				//設定値を読み込む
				foreach (XmlElement element in nodeList[0].ChildNodes)
				{
					switch (element.Name)
					{
						case "Name":
							this.m_name = element.InnerText;
							break;
						case "DataPath":
							this.m_datapath = element.InnerText;
							break;
						case "ExePath":
							this.m_exepath = element.InnerText;
							break;
						case "ExeArgv":
							this.m_exeArgv = element.InnerText;
							break;
						case "Type":
							this.m_type = (ProjectType)Enum.Parse(typeof(ProjectType), element.InnerText);
							break;
					}
				}
			}
		}

		/// <summary>
		/// 現在の設定値をXMLファイルに保存する
		/// </summary>
		/// <param name="m_filepath"></param>
		private void saveToXml(string filePath)
		{
			using (FileStream fs = new FileStream(filePath, FileMode.Create))
			{
				using (XmlTextWriter xw = new XmlTextWriter(fs, Encoding.UTF8))
				{
					xw.Formatting = Formatting.Indented;

					xw.WriteStartDocument();
					xw.WriteStartElement("ProjectFile");

					writeElement(xw, "Name", this.Name);
					writeElement(xw, "DataPath", this.DataPath);
					writeElement(xw, "ExePath", this.ExePath);
					writeElement(xw, "ExeArgv", this.ExeArgv);
					writeElement(xw, "Type", this.Type);

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
		#endregion
	}
}
