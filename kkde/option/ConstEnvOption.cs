using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace kkde.option
{
	/// <summary>
	/// 定数な環境設定
	/// </summary>
	public class ConstEnvOption
	{
		#region フィールド
		/// <summary>
		/// テンプレートプロジェクトの相対パス
		/// </summary>
		private static string m_templateProjectPath = "template\\project";

		/// <summary>
		/// テンプレートファイルの相対パス
		/// </summary>
		private static string m_templateFilePath = "template\\file";

		/// <summary>
		/// 吉里吉里リリーサ実行ファイルの相対パス
		/// </summary>
		private static string m_exeReleasePath = @"krkr\kirikiri2\tools\krkrrel.exe";

		/// <summary>
		/// 吉里吉里２リファレンスファイルの相対パス
		/// </summary>
		private static string m_htmlKrkr2ManualPath = @"krkr\kirikiri2\kr2doc\index.html";

		/// <summary>
		/// TJS2リファレンスファイルの相対パス
		/// </summary>
		private static string m_htmlTjs2ManualPath = @"krkr\kirikiri2\tjs2doc\index.html";

		/// <summary>
		/// KAG3リファレンスファイルの相対パス
		/// </summary>
		private static string m_htmlKag3ManualPath = @"krkr\kag3\kag3doc\index.html";

		/// <summary>
		/// KAG3タグリファレンスファイルの相対パス
		/// </summary>
		private static string m_htmlKag3TagRefPath = @"krkr\kag3\kag3doc\contents\Tags.html";

		/// <summary>
		/// KAG3タグ定義ファイルの相対パス
		/// </summary>
		private static string m_kag3tagDefFilePath = @"template\completion\kag3tag.ks";

		/// <summary>
		/// KAGEXタグ定義ファイルの相対パス
		/// </summary>
		private static string m_kagextagDefFilePath = @"template\completion\kagextag.ks";

		/// <summary>
		/// KAGEXワールド拡張定義ファイルの相対パス
		/// </summary>
		private static string m_worldextagDefFilePath = @"template\completion\worldextag.ks";
		#endregion

		#region プロパティ
		/// <summary>
		/// プログラムのルートディレクトリパス（読み取り専用）
		/// </summary>
		public static string ApplicationDirectoryPath
		{
			get
			{
				return Path.GetDirectoryName(Application.ExecutablePath);
			}
		}

		/// <summary>
		/// テンプレートプロジェクトのフルパス（読み取り専用）
		/// </summary>
		public static string TemplateProjectPath
		{
			get
			{
				return Path.Combine(ApplicationDirectoryPath, m_templateProjectPath);
			}
		}

		/// <summary>
		/// テンプレートファイル格納場所のフルパス（読み取り専用）
		/// テンプレートファイルはファイル追加時のテンプレートとなるファイルのこと
		/// </summary>
		public static string TemplateFilePath
		{
			get
			{
				return Path.Combine(ApplicationDirectoryPath, m_templateFilePath);
			}
		}

		/// <summary>
		/// ドッキングパネルの状態を保存するファイルパス
		/// </summary>
		public static string DockPanelFilePath
		{
			get
			{
				return Path.Combine(EnvOption.DirectoryPath, "panel.config");
			}
		}

		/// <summary>
		/// カラー定義まとめファイルを保存するパス
		/// </summary>
		public static string SyntaxModesFilePath
		{
			get
			{
				return Path.Combine(EnvOption.DirectoryPath, "SyntaxModes.xml");
			}
		}

		/// <summary>
		/// KAG強調表示設定ファイルのパス
		/// </summary>
		public static string KagModeFilePath
		{
			get
			{
				return Path.Combine(EnvOption.DirectoryPath, "kag_mode.xshd");
			}
		}

		/// <summary>
		/// TJS強調表示設定ファイルのパス
		/// </summary>
		public static string TjsModeFilePath
		{
			get
			{
				return Path.Combine(EnvOption.DirectoryPath, "tjs2_mode.xshd");
			}
		}

		/// <summary>
		/// その他のファイル強調表示設定ファイルのパス
		/// </summary>
		public static string DefModeFilePath
		{
			get
			{
				return Path.Combine(EnvOption.DirectoryPath, "def_mode.xshd");
			}
		}

		/// <summary>
		/// KAGエディタ設定ファイルのパス
		/// </summary>
		public static string KagOptionFilePath
		{
			get
			{
				return Path.Combine(EnvOption.DirectoryPath, "kag_option.xml");
			}
		}

		/// <summary>
		/// TJSエディタ設定ファイルのパス
		/// </summary>
		public static string TjsOptionFilePath
		{
			get
			{
				return Path.Combine(EnvOption.DirectoryPath, "tjs2_option.xml");
			}
		}

		/// <summary>
		/// その他のファイルのエディタ設定ファイルのパス
		/// </summary>
		public static string DefOptionFilePath
		{
			get
			{
				return Path.Combine(EnvOption.DirectoryPath, "def_option.xml");
			}
		}

		/// <summary>
		/// 吉里吉里リリーサーの実行ファイルパス
		/// </summary>
		public static string ReleaserExePath
		{
			get
			{
				return Path.Combine(ApplicationDirectoryPath, m_exeReleasePath);
			}
		}

		/// <summary>
		/// 吉里吉里２マニュアルファイルパス
		/// </summary>
		public static string Krkr2ManualPath
		{
			get
			{
				return Path.Combine(ApplicationDirectoryPath, m_htmlKrkr2ManualPath);
			}
		}

		/// <summary>
		/// TJS2マニュアルファイルパス
		/// </summary>
		public static string Tjs2ManualPath
		{
			get
			{
				return Path.Combine(ApplicationDirectoryPath, m_htmlTjs2ManualPath);
			}
		}


		/// <summary>
		/// KAG3マニュアルファイルパス
		/// </summary>
		public static string Kag3ManualPath
		{
			get
			{
				return Path.Combine(ApplicationDirectoryPath, m_htmlKag3ManualPath);
			}
		}

		/// <summary>
		/// KAG3タグリファレンスファイルのパス
		/// </summary>
		public static string Kag3TagRefPath
		{
			get
			{
				return Path.Combine(ApplicationDirectoryPath, m_htmlKag3TagRefPath);
			}
		}

		/// <summary>
		/// KAG3入力補完定義ファイルパス
		/// </summary>
		public static string Kag3tagDefFilePath
		{
			get 
			{
				return Path.Combine(ApplicationDirectoryPath, m_kag3tagDefFilePath);
			}
		}

		/// <summary>
		/// KAGEX入力補完定義ファイルパス
		/// </summary>
		public static string KagextagDefFilePath
		{
			get 
			{
				return Path.Combine(ApplicationDirectoryPath, m_kagextagDefFilePath);
			}
		}

		/// <summary>
		/// KAGEXワールド拡張定義ファイルのパス
		/// </summary>
		public static string WorldextagDefFilePath
		{
			get
			{
				return Path.Combine(ApplicationDirectoryPath, m_worldextagDefFilePath);
			}
		}
		#endregion
	}
}
