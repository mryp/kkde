using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Web;
using ICSharpCode.TextEditor.Actions;
using kkde.global;
using kkde.option;
using kkde.parse;
using kkde.parse.complate;
using kkde.project;

namespace kkde.editor
{
	/// <summary>
	/// F1キーを押したときヘルプリファレンスを表示するアクション
	/// </summary>
	public class HelpReferenceAction : AbstractEditAction
	{
		/// <summary>
		/// アクション実行
		/// </summary>
		/// <param name="textArea">エディタ</param>
		public override void Execute(ICSharpCode.TextEditor.TextArea textArea)
		{
			TextEditorEx editor = (TextEditorEx)textArea.MotherTextEditorControl;
			switch (FileType.GetKrkrType(editor.FileName))
			{
				case FileType.KrkrType.Kag:
					showKagTagHelp(editor);
					break;
				case FileType.KrkrType.Tjs:
					//未実装
					break;
			}
		}

		/// <summary>
		/// KAGタグヘルプを表示する
		/// </summary>
		/// <param name="editor">エディタ</param>
		private void showKagTagHelp(TextEditorEx editor)
		{
			//カーソル位置のKAGタグ情報を取得する
			Debug.WriteLine("showKagTagHelp START");
			int colNum = editor.GetKagTagEndFormCaret();
			KagTagKindInfo info = KagUtility.GetTagKind(editor.Document, editor.ActiveTextArea.Caret.Line, colNum);
			if (info == null)
			{
				util.Msgbox.Error("タグ・マクロのある位置にカーソルが無いため検索できません");
				return;
			}
			if (info.TagName == "")
			{
				return;	//タグがない
			}

			//KAGタグ情報からマクロ情報を取得する
			KagMacro macro = KagUtility.GetKagMacro(info.TagName);
			if (macro == null)
			{
				//タグ情報が取得できなかった
				util.Msgbox.Error("タグ・マクロの情報が取得できませんでした");
				return;
			}

			//URLを生成する
			string url = "";
			if (macro.DefType == KagMacro.DefineType.Kag)	//KAGのタグの時
			{
				url = String.Format("{0}#{1}", new Uri(ConstEnvOption.Kag3TagRefPath), HttpUtility.UrlEncode(info.TagName));
			}
			else
			{
				return;	//そのほかのタグは対応するヘルプがない
			}

			switch (GlobalStatus.EnvOption.HelpHelpWindow)
			{
				case EnvOption.HelpWindowState.DockingWindow:
					//ドッキングウィンドウで表示
					GlobalStatus.FormManager.HelpReferenceForm.Show(GlobalStatus.FormManager.MainForm.MainDockPanel);
					GlobalStatus.FormManager.HelpReferenceForm.Navigate(url);
					break;
				case EnvOption.HelpWindowState.DialogWindow:
					//ダイアログウィンドウで表示
					GlobalStatus.FormManager.HelpRefDialogShow();
					GlobalStatus.FormManager.HelpRefDialog.Navigate(url);
					break;
			}
		}
	}
}
