using System;
using System.Collections.Generic;
using System.Text;

using kkde.project;
using kkde.editor;
using kkde.search;
using kkde.debug;
using kkde.parse;
using kkde.option;
using kkde.kagex;

namespace kkde.global
{
	/// <summary>
	/// 現在の全体で使用する状態を管理するクラス
	/// </summary>
	public class GlobalStatus
	{
		#region フィールド
		private static ProjectFile m_project = new ProjectFile();
		private static EnvOption m_envOption = new EnvOption();
		private static EditorManager m_edMgr = null;
		private static Dictionary<FileType.KrkrType, EditorOption> m_edOption = new Dictionary<FileType.KrkrType, EditorOption>(3);
		private static KrkrProcess m_krkrProcess = null;
		private static TextEncoding m_textEnc = new TextEncoding();
		private static FormManager m_formMgr = new FormManager();
		private static ParserService m_parserService = new ParserService();
		private static KagexPreview m_kagexPreview = null;
		private static LabelExecManager m_labelExecMgr = null;
		#endregion

		#region プロパティ
		/// <summary>
		/// 現在開いているプロジェクト
		/// </summary>
		public static ProjectFile Project
		{
			get { return GlobalStatus.m_project; }
			set { GlobalStatus.m_project = value; }
		}

		/// <summary>
		/// 環境設定情報
		/// </summary>
		public static EnvOption EnvOption
		{
			get { return GlobalStatus.m_envOption; }
			set { GlobalStatus.m_envOption = value; }
		}

		/// <summary>
		/// エディタ管理情報
		/// </summary>
		public static EditorManager EditorManager
		{
			get 
			{
				System.Diagnostics.Debug.Assert(GlobalStatus.m_edMgr != null, "ERROR! エディタの初期化が行われていないのにエディタ管理情報を参照しようとしました。");
				return GlobalStatus.m_edMgr; 
			}
			set { GlobalStatus.m_edMgr = value; }
		}

		/// <summary>
		/// エディタ設定情報リスト
		/// </summary>
		public static Dictionary<FileType.KrkrType, EditorOption> EditorOption
		{
			get { return GlobalStatus.m_edOption; }
			set { GlobalStatus.m_edOption = value; }
		}

		/// <summary>
		/// 吉里吉里実行管理情報
		/// </summary>
		public static KrkrProcess KrkrProc
		{
			get { return GlobalStatus.m_krkrProcess; }
			set { GlobalStatus.m_krkrProcess = value; }
		}

		/// <summary>
		/// エンコーディング情報
		/// </summary>
		internal static TextEncoding TextEnc
		{
			get { return GlobalStatus.m_textEnc; }
			set { GlobalStatus.m_textEnc = value; }
		}

		/// <summary>
		/// ウィンドウ管理情報
		/// </summary>
		public static FormManager FormManager
		{
			get { return GlobalStatus.m_formMgr; }
			set { GlobalStatus.m_formMgr = value; }
		}

		/// <summary>
		/// パーサー管理
		/// </summary>
		public static ParserService ParserSrv
		{
			get { return GlobalStatus.m_parserService; }
			set { GlobalStatus.m_parserService = value; }
		}

		/// <summary>
		/// KAGEXワールド拡張プレビューを実行する
		/// </summary>
		public static KagexPreview WorldExPreview
		{
			get 
			{
				if (GlobalStatus.m_kagexPreview == null)
				{
					GlobalStatus.m_kagexPreview = new KagexPreview();
				}
				return GlobalStatus.m_kagexPreview; 
			}
			set { GlobalStatus.m_kagexPreview = value; }
		}

		/// <summary>
		/// ラベルから実行
		/// </summary>
		public static LabelExecManager LabelExecMgr
		{
			get 
			{
				if (GlobalStatus.m_labelExecMgr == null)
				{
					GlobalStatus.m_labelExecMgr = new LabelExecManager();
				}
				return GlobalStatus.m_labelExecMgr; 
			}
			set { GlobalStatus.m_labelExecMgr = value; }
		}
		#endregion

		#region メソッド
		#endregion
	}
}
