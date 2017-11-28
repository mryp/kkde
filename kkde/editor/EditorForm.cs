using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using kkde.global;
using kkde.project;

namespace kkde.editor
{
	/// <summary>
	/// エディタを表示するためのフォーム
	/// </summary>
	public partial class EditorForm : WeifenLuo.WinFormsUI.DockContent, IEditorDocContent
	{
		#region フィールド
		/// <summary>
		/// エディタ内容が保存済みかどうかを保持する変数
		/// trueの時は保存されていない。falseのときは保存されている
		/// </summary>
		bool m_isTextChanged = false;

		/// <summary>
		/// エディタを閉じたかどうか
		/// </summary>
		bool m_IsClosed = false;
		#endregion

		#region プロパティ
		/// <summary>
		/// フォームで表示しているテキストエディタを返す
		/// </summary>
		public TextEditorEx TextEditor
		{
			get
			{
				return mainTextEditor;
			}
		}

		/// <summary>
		/// エディタ内容が変更されているかどうか
		/// 変更されている（未保存）ときはtrueを返す
		/// </summary>
		public bool IsTextChanged
		{
			get { return m_isTextChanged; }
		}

		/// <summary>
		/// エディタを閉じた後かどうか
		/// trueのときはすでに閉じている
		/// </summary>
		public bool IsClosed
		{
			get { return m_IsClosed; }
		}

		/// <summary>
		/// ファイル名
		/// </summary>
		public String FileName
		{
			get
			{
				return this.TextEditor.FileName;
			}
		}
		#endregion

		#region 初期化メソッド
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public EditorForm()
		{
			InitializeComponent();

			initEditorOption();
			initTab();
		}

		/// <summary>
		/// タブの状態を初期化する
		/// </summary>
		private void initTab()
		{
			setTabText(false, "無題");
		}

		/// <summary>
		/// 設定からエディタの初期化を行う
		/// </summary>
		private void initEditorOption()
		{
			mainTextEditor.FileName = "無題";
			mainTextEditor.Font = new Font("ＭＳ ゴシック", 10);
			mainTextEditor.Document.DocumentChanged += new ICSharpCode.TextEditor.Document.DocumentEventHandler(Document_DocumentChanged);
		}
		#endregion

		#region ファイル関連メソッド
		/// <summary>
		/// テキストファイルをエディタに読み込む
		/// </summary>
		/// <param name="fileName">読み込むファイル名</param>
		public void LoadFile(string fileName)
		{
			mainTextEditor.LoadFile(fileName);
			setTabText(false);
		}

		/// <summary>
		/// テキストファイルに保存する
		/// </summary>
		/// <param name="fileName">保存するファイル名</param>
		public void SaveFile(string fileName)
		{
			mainTextEditor.SaveFile(fileName);
			setTabText(false);
		}
		#endregion

		#region タブ関連メソッド
		/// <summary>
		/// タブテキストをセットする
		/// </summary>
		/// <param name="isChanged">変更があるかどうか（変更ありの時はtrueをしていする）</param>
		private void setTabText(bool isChanged)
		{
			if (File.Exists(mainTextEditor.FileName) == false)
			{
				return;
			}
			setTabText(isChanged, Path.GetFileName(mainTextEditor.FileName));
		}

		/// <summary>
		/// タブテキストをセットする
		/// </summary>
		/// <param name="isChanged">変更があるかどうか</param>
		/// <param name="text">タブに表示するテキスト</param>
		private void setTabText(bool isChanged, string text)
		{
			if (isChanged == true)
			{
				//変更ありの時は米印をつける
				text = text + " *";
			}

			m_isTextChanged = isChanged;
			this.TabText = text;
		}

		/// <summary>
		/// フォームを閉じる
		/// </summary>
		private void tabMenuItemClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// 上書き保存
		/// </summary>
		private void tabMenuItemSave_Click(object sender, EventArgs e)
		{
			this.SaveFile(this.mainTextEditor.FileName);
		}

		/// <summary>
		/// このタブ以外を閉じる
		/// </summary>
		private void tabMenuItemCloseOther_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.CloseFileOtherAll();
		}

		/// <summary>
		/// 左のタブをすべて閉じる
		/// </summary>
		private void tabMenuItemCloseLeft_Click(object sender, EventArgs e)
		{
			GlobalStatus.EditorManager.CloseFileLeftAll();
		}

		/// <summary>
		/// 右のタブをすべて閉じる
		/// </summary>
		private void tabMenuItemCloseRight_Click(object sender, EventArgs e)
		{
			//未実装
		}

		/// <summary>
		/// フォームをアクティブにする
		/// </summary>
		public void ActivateForm()
		{
			this.Activate();
		}

		/// <summary>
		/// フォームを閉じる
		/// </summary>
		public void CloseForm()
		{
			this.Close();
		}
		#endregion

		#region イベント
		/// <summary>
		/// ドキュメント内容が変更されたとき
		/// </summary>
		void Document_DocumentChanged(object sender, ICSharpCode.TextEditor.Document.DocumentEventArgs e)
		{
			if (m_isTextChanged == false)
			{
				setTabText(true);
			}
		}

		/// <summary>
		/// エディタを閉じたとき
		/// </summary>
		private void EditorForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			m_IsClosed = true;
		}

		/// <summary>
		/// 選択範囲を切り取る
		/// </summary>
		private void editorMenuItemCut_Click(object sender, EventArgs e)
		{
			mainTextEditor.CutText();
		}

		/// <summary>
		/// 選択範囲をコピーする
		/// </summary>
		private void editorMenuItemCopy_Click(object sender, EventArgs e)
		{
			mainTextEditor.CopyText();
		}

		/// <summary>
		/// クリップボードの内容を貼り付ける
		/// </summary>
		private void editorMenuItemPaste_Click(object sender, EventArgs e)
		{
			mainTextEditor.PasteText();
		}

		/// <summary>
		/// 選択範囲をコメント化
		/// </summary>
		private void editorMenuItemComment_Click(object sender, EventArgs e)
		{
			mainTextEditor.DoCommentOut();
		}

		/// <summary>
		/// 選択範囲のコメント解除
		/// </summary>
		private void editorMenuItemUnComment_Click(object sender, EventArgs e)
		{
			mainTextEditor.DoUncommentOut();
		}

		/// <summary>
		/// エディタポップを表示しようとしたとき
		/// </summary>
		private void editorPopMenu_Opening(object sender, CancelEventArgs e)
		{
			switch (FileType.GetKrkrType(mainTextEditor.FileName))
			{
				case FileType.KrkrType.Kag:	//KAGファイルのとき
					menuItemJumpDefine.Enabled = true;
					menuItemJumpLabel.Enabled = true;
					menuItemJumpGrepDefine.Enabled = true;
					break;
				case FileType.KrkrType.Tjs:
					//今のところ未実装
					menuItemJumpDefine.Enabled = false;
					menuItemJumpLabel.Enabled = false;
					menuItemJumpGrepDefine.Enabled = false;
					break;
				default:
					menuItemJumpDefine.Enabled = false;
					menuItemJumpLabel.Enabled = false;
					menuItemJumpGrepDefine.Enabled = false;
					break;
			}
		}

		/// <summary>
		/// 定義へ移動
		/// </summary>
		private void menuItemJumpDefine_Click(object sender, EventArgs e)
		{
			mainTextEditor.JumpDefineFuncFromCaret();
		}

		/// <summary>
		/// 全ての参照を検索
		/// </summary>
		private void menuItemJumpGrepDefine_Click(object sender, EventArgs e)
		{
			mainTextEditor.SearchGrepReferenceDefineFromCaret();
		}

		/// <summary>
		/// 指定ラベルへ移動
		/// </summary>
		private void menuItemJumpLabel_Click(object sender, EventArgs e)
		{
			mainTextEditor.JumpKagScenarioLabelFromCaret();
		}

		private void EditorForm_DoubleClick(object sender, EventArgs e)
		{
			Debug.WriteLine("ダブルクリック！");
		}

		private void menuItemRegionMsgonoff_Click(object sender, EventArgs e)
		{
			mainTextEditor.RegionSelectText("@msgoff", "@msgon");
		}

		private void menuItemRegionTrans_Click(object sender, EventArgs e)
		{
			mainTextEditor.RegionSelectText("@begintrans", "@endtrans");
		}
		#endregion
	}
}
