using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace kkde.project
{
	/// <summary>
	/// KKDEで扱うファイルタイプ
	/// </summary>
	public class FileType
	{
		/// <summary>
		/// 吉里吉里で扱うファイルタイプ
		/// </summary>
		public enum KrkrType
		{
			/// <summary>
			/// KAGシナリオ
			/// </summary>
			Kag,
			/// <summary>
			/// TJSスクリプト
			/// </summary>
			Tjs,
			/// <summary>
			/// テキストファイル
			/// </summary>
			Text,
			/// <summary>
			/// イメージファイル
			/// </summary>
			Image,
			/// <summary>
			/// サウンドファイル
			/// </summary>
			Sound,
			/// <summary>
			/// スクリーンファイル
			/// </summary>
			Screen,
			/// <summary>
			/// 未対応ファイル
			/// </summary>
			Unknown,
		}

		public enum SoundType
		{
			/// <summary>
			/// OGGファイル
			/// </summary>
			Ogg,
			/// <summary>
			/// WAVEファイル
			/// </summary>
			Wave,
			/// <summary>
			/// MIDIファイル
			/// </summary>
			Midi,
			/// <summary>
			/// 未対応ファイル
			/// </summary>
			Unknown,
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FileType()
		{
		}

		/// <summary>
		/// 拡張子付きファイル名から吉里吉里の対応ファイルタイプを返す
		/// </summary>
		/// <param name="fileName">ファイル名</param>
		/// <returns>吉里吉里ファイルタイプ</returns>
		public static KrkrType GetKrkrType(string fileName)
		{
			if (File.Exists(fileName) == false)
			{
				//ファイルではないとき不明を返す
				return KrkrType.Unknown;
			}

			KrkrType ret;
			string ext = Path.GetExtension(fileName);
			ext = ext.ToLower();	//小文字で統一する
			switch (ext)
			{
				case ".ks":
					ret = KrkrType.Kag;
					break;
				case ".tjs":
					ret = KrkrType.Tjs;
					break;
				case ".txt":
					ret = KrkrType.Text;
					break;
				case ".png":
				case ".jpg":
				case ".jpe":
				case ".jpeg":
				case ".bmp":
					ret = KrkrType.Image;
					break;
				case ".ogg":
				case ".wav":
				case ".mid":
					ret = KrkrType.Sound;
					break;
				case ".kui":
					ret = KrkrType.Screen;
					break;
				default:
					ret = KrkrType.Unknown;
					break;
			}

			return ret;
		}

		/// <summary>
		/// 拡張子付きファイル名から音楽ファイルの種類を返す
		/// </summary>
		/// <param name="fileName">ファイル名</param>
		/// <returns>音楽の種類</returns>
		public static SoundType GetSoundType(string fileName)
		{
			if (File.Exists(fileName) == false)
			{
				//ファイルではないとき不明を返す
				return SoundType.Unknown;
			}

			SoundType ret;
			string ext = Path.GetExtension(fileName);
			ext = ext.ToLower();	//小文字で統一する
			switch (ext)
			{
				case ".ogg":
					ret = SoundType.Ogg;
					break;
				case ".wav":
					ret = SoundType.Wave;
					break;
				case ".mid":
					ret = SoundType.Midi;
					break;
				default:
					ret = SoundType.Unknown;
					break;
			}

			return ret;
		}

		/// <summary>
		/// 吉里吉里で扱うテキストファイルの拡張子をタイプから返す
		/// </summary>
		/// <param name="type">テキストファイル系のファイルタイプ</param>
		/// <returns>拡張子（ドット付き）</returns>
		public static string GetKrkrTextFileExt(KrkrType type)
		{
			string ret = "";
			switch (type)
			{
				case KrkrType.Kag:
					ret = ".ks";
					break;
				case KrkrType.Tjs:
					ret = ".tjs";
					break;
				case KrkrType.Text:
					ret = ".txt";
					break;
				case KrkrType.Screen:
					ret = ".kui";
					break;
				default:	//画像などはその他を返す
					ret = "";
					break;
			}

			return ret;
		}

		/// <summary>
		/// 吉里吉里で扱うファイルの検索用拡張子リストをタイプから返す
		/// </summary>
		/// <param name="type"></param>
		/// <returns>検索用拡張子リスト（例：*.ks）</returns>
		public static string GetKrkrFileExtForSearch(KrkrType type)
		{
			string ret = "";
			switch (type)
			{
				case KrkrType.Kag:
					ret = "*.ks";
					break;
				case KrkrType.Tjs:
					ret = "*.tjs";
					break;
				case KrkrType.Text:
					ret = "*.txt";
					break;
				case KrkrType.Image:
					ret = "*.png;*.bmp;*.jpg;*.jpe;*.jpeg";
					break;
				case KrkrType.Sound:
					ret = "*.ogg;*.wav;*.mid";
					break;
				case KrkrType.Screen:
					ret = "*.kui";
					break;
				default:
					ret = "*.*";
					break;
			}

			return ret;
		}

		/// <summary>
		/// 吉里吉里ファイルタイプから表示文字列を取得する
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static string KrkrTypeToString(KrkrType type)
		{
			string ret = "";
			switch (type)
			{
				case KrkrType.Kag:
					ret = "KAGシナリオ";
					break;
				case KrkrType.Tjs:
					ret = "TJSスクリプト";
					break;
				case KrkrType.Image:
					ret = "画像";
					break;
				case KrkrType.Text:
					ret = "テキストファイル";
					break;
				case KrkrType.Sound:
					ret = "サウンドファイル";
					break;
				case KrkrType.Screen:
					ret = "KKDEスクリーンファイル";
					break;
				default:
					ret = "その他のファイル";
					break;
			}

			return ret;
		}

		/// <summary>
		/// 表示文字から吉里吉里ファイルタイプを返す
		/// </summary>
		/// <param name="typeString">表示文字列</param>
		/// <returns>吉里吉里ファイルタイプ</returns>
		public static KrkrType StringToKrkrType(string typeString)
		{
			KrkrType ret;

			if (KrkrTypeToString(KrkrType.Kag) == typeString)
			{
				ret = KrkrType.Kag;
			}
			else if (KrkrTypeToString(KrkrType.Tjs) == typeString)
			{
				ret = KrkrType.Tjs;
			}
			else if (KrkrTypeToString(KrkrType.Image) == typeString)
			{
				ret = KrkrType.Image;
			}
			else if (KrkrTypeToString(KrkrType.Text) == typeString)
			{
				ret = KrkrType.Text;
			}
			else if (KrkrTypeToString(KrkrType.Sound) == typeString)
			{
				ret = KrkrType.Sound;
			}
			else if (KrkrTypeToString(KrkrType.Screen) == typeString)
			{
				ret = KrkrType.Screen;
			}
			else
			{
				ret = KrkrType.Unknown;
			}

			return ret;
		}

		/// <summary>
		/// KAGEXワールド拡張定義ファイルかどうか
		/// </summary>
		/// <param name="filePath">ファイルパス</param>
		/// <returns>KAGEXワールド拡張定義ファイルの時はtrue</returns>
		public static bool IsKagexEnvinitFileName(string filePath)
		{
			if (Path.GetFileName(filePath) == "envinit.tjs")
			{
				return true;
			}

			return false;
		}
	}
}
