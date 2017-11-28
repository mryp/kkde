using System;
using System.Collections.Generic;
using System.Text;

namespace kkde.taginsert
{
	/// <summary>
	/// タグ挿入動作を規定するインターフェイス
	/// </summary>
	public interface ITagInsertAction
	{
		/// <summary>
		/// 挿入時に選択状態をクリアーするかどうか
		/// </summary>
		/// <returns>クリアーするときtrue</returns>
		bool IsSelectedClear();

		/// <summary>
		/// 作成したスクリプト・コードを取得する
		/// </summary>
		/// <returns>作成したコード</returns>
		string GetCode();

		/// <summary>
		/// ダイアログを表示する
		/// </summary>
		/// <param name="inputText"></param>
		/// <returns></returns>
		System.Windows.Forms.DialogResult ShowDialog(string inputText);
	}
}
