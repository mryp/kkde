using System;
using System.Collections.Generic;
using System.Text;

using ICSharpCode.TextEditor.Actions;
using ICSharpCode.TextEditor.Gui.CompletionWindow;
using kkde.project;
using kkde.global;

namespace kkde.editor
{
	/// <summary>
	/// ショートカットキーでの入力補完起動用アクション
	/// </summary>
	public class CompletionAction : AbstractEditAction
	{
		public override void Execute(ICSharpCode.TextEditor.TextArea textArea)
		{
			TextEditorEx editor = (TextEditorEx)textArea.MotherTextEditorControl;
			switch (FileType.GetKrkrType(editor.FileName))
			{
				case FileType.KrkrType.Kag:
					//KAG入力補完
					editor.m_codeCompletionWindow = CodeCompletionWindow.ShowCompletionWindow(GlobalStatus.FormManager.MainForm
						, editor, editor.FileName, editor.KagCompDataPrv, '\0');
					break;
				case FileType.KrkrType.Tjs:
					//未実装
					break;
			}
		}
	}
}
