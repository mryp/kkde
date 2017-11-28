using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using kkde.global;

namespace kkde.taginsert
{
	/// <summary>
	/// 挿入フォームの基本となるクラス
	/// 少なくともGetCodeはオーバライドすること
	/// </summary>
	public partial class BaseInsertForm : Form, ITagInsertAction
	{
		#region フィールド
		/// <summary>
		/// 入力値
		/// </summary>
		string m_inputText = "";
		#endregion

		#region プロパティ
		/// <summary>
		/// 入力値（読み取り専用）
		/// </summary>
		public string InputText
		{
			get { return m_inputText; }
		}
		#endregion

		#region 初期化・終了処理
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public BaseInsertForm()
		{
			InitializeComponent();
		}
		#endregion

		#region ITagInsertActionインターフェース実装メソッド
		/// <summary>
		/// ダイアログを表示する
		/// </summary>
		/// <param name="inputText">入力文字列</param>
		/// <returns>ダイアログの実行結果</returns>
		public virtual DialogResult ShowDialog(string inputText)
		{
			m_inputText = inputText;

			return this.ShowDialog();
		}

		/// <summary>
		/// 挿入動作時に選択状態をクリアーするかどうかを返す
		/// trueにすると選択解除後、選択していた箇所の後ろに挿入する
		/// falseにすると選択文字列に上書きする形で挿入する（選択していた文字は削除される）
		/// デフォルトはtrue
		/// </summary>
		/// <returns></returns>
		public virtual bool IsSelectedClear()
		{
			return true;
		}

		/// <summary>
		/// 挿入するためのコードを取得する
		/// 継承クラスでは必ず上書きすること
		/// </summary>
		/// <returns>挿入するコード</returns>
		public virtual string GetCode()
		{
			return "";
		}
		#endregion

		#region ボタンイベント処理
		/// <summary>
		/// OKボタンを押したとき
		/// </summary>
		private void okButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// キャンセルボタンを押したとき
		/// </summary>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			//何もしない
			this.Close();
		}
		#endregion

		#region タグ生成関連メソッド
		/// <summary>
		/// 改行コードを取得する
		/// </summary>
		/// <returns></returns>
		public string GetNewLineCode()
		{
			return "\r\n";
		}
		#endregion
	}
}
