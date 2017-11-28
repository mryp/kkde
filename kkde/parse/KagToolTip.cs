using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.TextEditor.Document;
using System.Drawing;
using kkde.parse.complate;
using kkde.global;

namespace kkde.parse
{
	/// <summary>
	/// KAGツールチップ表示内容管理クラス
	/// </summary>
	public static class KagToolTip
	{
		/// <summary>
		/// ツールチップで表示するテキストを取得する
		/// </summary>
		/// <param name="document"></param>
		/// <param name="lineNumber"></param>
		/// <param name="colNumber"></param>
		/// <returns></returns>
		public static string GetText(IDocument document, int lineNumber, int colNumber)
		{
			if (document == null)
			{
				return null;	//ドキュメントがないとき
			}

			string tip = "";
			string word = TextUtilities.GetWordAt(document, document.PositionToOffset(new Point(colNumber, lineNumber)));
			KagTagKindInfo info = KagUtility.GetTagKind(document, lineNumber, colNumber);
			if (info == null)
			{
				return "";	//取得できなかった
			}
			switch (info.Kind)
			{
				case KagCompletionData.Kind.KagTagName:
					tip = getTagComment(word);
					break;
				case KagCompletionData.Kind.AttrName:
					tip = getTagAttrComment(word, info);
					break;
				default:
					break;	//不明とか属性値は何もしない
			}

			return tip;
		}

		/// <summary>
		/// 指定したマクロ名の説明を取得する
		/// </summary>
		/// <param name="info"></param>
		/// <param name="macroName"></param>
		/// <returns></returns>
		private static string getTagComment(string macroName)
		{
			KagMacro macro = KagUtility.GetKagMacro(macroName);
			if (macro == null)
			{
				return "";
			}

			return macro.Comment;
		}

		/// <summary>
		/// 指定した属性の説明を取得する
		/// </summary>
		/// <param name="info"></param>
		/// <param name="attrName"></param>
		/// <returns></returns>
		private static string getTagAttrComment(string attrName, KagTagKindInfo info)
		{
			KagMacro[] macroList = GlobalStatus.ParserSrv.GetKagMacroList();
			if (macroList.Length == 0)
			{
				return "";	//一つもないとき
			}

			KagMacroAttr attr = KagUtility.GetKagMacroAttr(attrName, info, macroList);
			if (attr == null)
			{
				return "";	//属性が取得できなかったとき
			}

			return attr.Comment;
		}


	}
}
