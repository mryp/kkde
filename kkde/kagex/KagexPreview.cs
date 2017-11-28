using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using kkde.global;
using kkde.debug;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace kkde.kagex
{
	/// <summary>
	/// KAGEXのプレビュー実行クラス
	/// </summary>
	public class KagexPreview
	{
		/// <summary>
		/// メインアイテム
		/// </summary>
		public enum TargetObject
		{
			Char,
			Event,
			Stage,
		}

		const string TEMPLATE_PATH = "kkde\\kkde_preview.template";
		const string TEMPLATE_HEAD_KEYWORD = "<<__PREVIEW_HEAD__>>";
		const string TEMPLATE_KEYWORD = "<<__PREVIEW_TAG__>>";
		const string SCRIPT_PATH = "kkde\\kkde_preview_run.ks";
		const string INIT_SCRIPT_NAME = "kkde_preview_init.ks";

		#region フィールド
		/// <summary>
		/// 吉里吉里とのメッセージ通信用オブジェクト
		/// </summary>
		MessageManager m_msg;

		/// <summary>
		/// 現在アクティブになっているオブジェクト
		/// </summary>
		TargetObject m_activeTarget = TargetObject.Char;

		EventWorldExAttrType m_eventObject = new EventWorldExAttrType();
		CharWorldExAttrType m_charObject = new CharWorldExAttrType();
		StageWorldExAttrType m_stageObject = new StageWorldExAttrType();
		#endregion

		/// <summary>
		/// イベント絵オブジェクト
		/// </summary>
		public EventWorldExAttrType EventObject
		{
			get { return m_eventObject; }
			set { m_eventObject = value; }
		}

		/// <summary>
		/// 立ち絵オブジェクト
		/// </summary>
		public CharWorldExAttrType CharObject
		{
			get { return m_charObject; }
			set { m_charObject = value; }
		}

		/// <summary>
		/// 背景絵オブジェクト
		/// </summary>
		public StageWorldExAttrType StageObject
		{
			get { return m_stageObject; }
			set { m_stageObject = value; }
		}

		/// <summary>
		/// アクティブなメインアイテム
		/// </summary>
		public TargetObject ActiveTarget
		{
			get { return m_activeTarget; }
			set { m_activeTarget = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public KagexPreview()
		{
			Reset();
			m_msg = new MessageManager();
			m_msg.ReceivMessage += new MessageManager.ReceivMsgHandler(m_msg_ReceivMessage);
		}

		void m_msg_ReceivMessage(string message)
		{
			Debug.WriteLine("メッセージ受信：" + message);
		}

		/// <summary>
		/// 現在設定している項目をリセットする
		/// </summary>
		public void Reset()
		{
			CharObject.ResetAll();
			EventObject.ResetAll();
			StageObject.ResetAll();
		}

		/// <summary>
		/// 立ち絵情報をセットする
		/// </summary>
		/// <param name="c">立ち絵タグ（例：琴子）（タグ記号は付けないこと）</param>
		public void SetChar(string c)
		{
			CharObject.Name = c;
			EventObject.Name = "";	//キャラクターが指定された場合はイベントをクリアする
			m_activeTarget = TargetObject.Char;
		}

		/// <summary>
		/// イベント絵情報をセットする
		/// </summary>
		/// <param name="e">イベント絵タグ（タグ記号は付けないこと）</param>
		public void SetEvent(string e)
		{
			EventObject.Name = e;
			CharObject.Name = "";	//イベントが指定されたときはキャラクターをクリアする
			m_activeTarget = TargetObject.Event;
		}

		/// <summary>
		/// 背景絵情報をセットする
		/// </summary>
		/// <param name="stage">背景絵タグ（タグ記号は付けないこと）</param>
		public void SetStage(string stage)
		{
			StageObject.Name = stage;
			m_activeTarget = TargetObject.Stage;
		}

		/// <summary>
		/// 舞台時間をセットする
		/// </summary>
		/// <param name="time">舞台時間タグ（タグ記号は付けないこと）</param>
		public void SetTime(string time)
		{
			switch (m_activeTarget)
			{
				case TargetObject.Char:
					CharObject.Stime = time;
					break;
				case TargetObject.Stage:
					StageObject.Stime = time;
					break;
			}
		}

		/// <summary>
		/// アクションをセットする
		/// </summary>
		/// <param name="action">アクションタグ（タグ記号は付けないこと）</param>
		public void SetAction(string action)
		{
			switch (m_activeTarget)
			{
				case TargetObject.Char:
					CharObject.Action = action;
					break;
				case TargetObject.Event:
					EventObject.Action = action;
					break;
				case TargetObject.Stage:
					StageObject.Action = action;
					break;
			}
		}

		/// <summary>
		/// トランジションをセットする
		/// </summary>
		/// <param name="trans">トランジションタグ（タグ記号は付けないこと）</param>
		public void SetTrans(string trans)
		{
			switch (m_activeTarget)
			{
				case TargetObject.Char:
					CharObject.Trans = trans;
					break;
				case TargetObject.Event:
					EventObject.Trans = trans;
					break;
				case TargetObject.Stage:
					StageObject.Trans = trans;
					break;
			}
		}

		/// <summary>
		/// 位置をセットする
		/// </summary>
		/// <param name="pos">位置タグ（タグ記号は付けないこと）</param>
		public void SetPos(string pos)
		{
			switch (m_activeTarget)
			{
				case TargetObject.Char:
					CharObject.Pos = pos;
					break;
			}
		}

		/// <summary>
		/// プレビュー開始
		/// </summary>
		/// <param name="headCode">プレビュー用スクリプトの前に置くコード</param>
		public void Run(string headCode)
		{
			//プレビュー用スクリプト作成
			if (createPreviewFile(headCode) == false)
			{
				Debug.WriteLine("プレビュー用スクリプトが作成できませんでした");
				return;
			}

			if (GlobalStatus.KrkrProc.HWnd != IntPtr.Zero)
			{
				//すでに起動していたときはメッセージを送信しジャンプする
				m_msg.SendStringData(GlobalStatus.KrkrProc.HWnd, String.Format("jump|{0}|{1}", INIT_SCRIPT_NAME, "*start"));
			}

			//起動
			if (GlobalStatus.KrkrProc.Start(GlobalStatus.Project, KrkrProcess.StartMode.Preview) == false)
			{
				Debug.WriteLine("プレビュー用画面の起動ができませんでした");
				return;
			}
		}

		/// <summary>
		/// プレビュー開始
		/// </summary>
		public void Run()
		{
			this.Run("");	//ヘッダなしにする
		}

		/// <summary>
		/// プレビュー実行用ファイルを作成する
		/// </summary>
		/// <returns></returns>
		private bool createPreviewFile(string headCode)
		{
			if (GlobalStatus.Project == null)
			{
				Debug.WriteLine("プロジェクトが開かれていません");
				return false;
			}
			
			string templatePath = Path.Combine(GlobalStatus.Project.DataFullPath, TEMPLATE_PATH);
			if (File.Exists(templatePath) == false)
			{
				Debug.WriteLine("テンプレートファイルが見つかりません path=" + templatePath);
				return false;
			}

			try
			{
				string tmplate = "";
				using (StreamReader sr = new StreamReader(templatePath, GlobalStatus.TextEnc.GetFileEncoding(templatePath)))
				{
					tmplate = sr.ReadToEnd();
				}

				string scriptPath = Path.Combine(GlobalStatus.Project.DataFullPath, SCRIPT_PATH);
				kkde.option.EditorOption edOption = GlobalStatus.EditorManager.GetEditorOption(scriptPath);
				using (StreamWriter sw = new StreamWriter(scriptPath, false, edOption.Encoding))
				{
					tmplate = tmplate.Replace(TEMPLATE_HEAD_KEYWORD, headCode);			//ヘッダ置換
					tmplate = tmplate.Replace(TEMPLATE_KEYWORD, createPreviewScript());	//スクリプト置換
					sw.Write(tmplate);
				}
			}
			catch (Exception err)
			{
				util.Msgbox.Error("プレビュー用ファイルが作成できませんでした\n" + err.Message);
				return false;
			}

			return true;
		}

		/// <summary>
		/// プレビューするスクリプトを作成する
		/// </summary>
		/// <returns></returns>
		private string createPreviewScript()
		{
			string script = "";
			if (StageObject.Name != "")
			{
				script += GetKagTagAttr(StageObject) + "\r\n";
			}

			//立ち絵とイベント絵は排他とする
			if (EventObject.Name != "")
			{
				script += GetKagTagAttr(EventObject) + "\r\n";
			}
			else if (CharObject.Name != "")
			{
				script += GetKagTagAttr(CharObject) + "\r\n";
			}

			if (script == "")
			{
				script = "@msgon\nプレビューする項目がありません";
			}
			return script;
		}

		/// <summary>
		/// ターゲットのKAGタグを返す
		/// （タグ記号付き）
		/// </summary>
		/// <param name="target"></param>
		/// <returns>KAGタグ（タグ記号付き）</returns>
		public string GetKagTagAttr(TargetObject target)
		{
			string tag = "";
			switch (target)
			{
				case TargetObject.Char:
					tag = GetKagTagAttr(CharObject);
					break;
				case TargetObject.Event:
					tag = GetKagTagAttr(EventObject);
					break;
				case TargetObject.Stage:
					tag = GetKagTagAttr(StageObject);
					break;
			}

			return tag;
		}

		/// <summary>
		/// 指定したオブジェクトのKAGタグを返す（タグ記号付き）
		/// </summary>
		/// <param name="obj">KAGタグを取得するオブジェクト</param>
		/// <returns>KAGタグ（タグ記号付き）</returns>
		public string GetKagTagAttr(BaseWorldExAttrType obj)
		{
			return String.Format("@{0} {1}", obj.Name, obj.ToKagTagAttr());
		}
	}
}
