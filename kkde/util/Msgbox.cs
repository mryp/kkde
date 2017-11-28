using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace kkde.util
{
	/// <summary>
	/// メッセージボックス関連クラス
	/// </summary>
	public class Msgbox
	{
		/// <summary>
		/// エラーダイアログを表示する
		/// </summary>
		/// <param name="text">エラーメッセージ</param>
		/// <returns>ダイアログ押したボタン返値</returns>
		public static DialogResult Error(string text)
		{
			return MessageBox.Show(text, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		/// <summary>
		/// 情報ダイアログを表示する
		/// </summary>
		/// <param name="text">エラーメッセージ</param>
		/// <returns>ダイアログ押したボタン返値</returns>
		public static DialogResult Info(string text)
		{
			return MessageBox.Show(text, "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		/// <summary>
		/// 警告ダイアログを表示する
		/// </summary>
		/// <param name="text">警告メッセージ</param>
		/// <returns>ダイアログ押したボタン返値</returns>
		public static DialogResult Warning(string text)
		{
			return MessageBox.Show(text, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

	}
}
