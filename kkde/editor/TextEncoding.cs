using System;
using System.Collections.Generic;
using System.Text;

using G_PROJECT;

namespace kkde.editor
{
	class TextEncoding : TxtEnc
	{
		#region フィールド
		Encoding defaultEncoding = Encoding.GetEncoding("shift_jis");
		#endregion

		#region プロパティ
		/// <summary>
		/// 結果が不明のときとりあえず返すデフォルトのエンコード
		/// </summary>
		public Encoding DefaultEncoding
		{
			get { return defaultEncoding; }
			set { defaultEncoding = value; }
		}
		#endregion

		#region メソッド
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public TextEncoding()
			: base()
		{
		}

		/// <summary>
		/// ファイル名からエンコードを判別して返す
		/// </summary>
		/// <param name="fileName">ファイル名</param>
		/// <returns>文字エンコーディング</returns>
		public Encoding GetFileEncoding(string fileName)
		{
			System.Text.Encoding enc = this.SetFromTextFile(fileName);
			if (enc == null)
			{
				return defaultEncoding;
			}
			else
			{
				return enc;
			}
		}
		#endregion
	}

	/// <summary>
	/// テキストエンコーディングの表示や状態を保持する構造体
	/// </summary>
	public struct TextEncodingState
	{
		#region フィールド
		/// <summary>
		/// エンコード名
		/// </summary>
		string name;
		#endregion

		#region プロパティ
		/// <summary>
		/// 保存している改行コード名
		/// </summary>
		public string Name
		{
			get
			{
				return name;
			}
		}
		#endregion

		#region メソッド
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="name">エンコーディングの名前</param>
		public TextEncodingState(string name)
		{
			switch (name)
			{
				case "shift_jis":
				case "utf-16":
					this.name = name;
					break;
				default:
					this.name = "shift_jis";
					break;
			}
		}

		/// <summary>
		/// 文字列化
		/// </summary>
		/// <returns>文字列</returns>
		public override string ToString()
		{
			string result = "";
			switch (name)
			{
				case "shift_jis":
					result = Encoding.GetEncoding(name).EncodingName;
					break;
				case "utf-16":
					result = Encoding.Unicode.EncodingName;
					break;
				default:
					result = "？不明？";
					break;
			}

			return result;
		}
		#endregion
	}
}
