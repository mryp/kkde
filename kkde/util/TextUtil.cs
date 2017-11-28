using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using kkde.search;
using ICSharpCode.TextEditor.Document;

namespace kkde.util
{
	/// <summary>
	/// テキスト関連のユーティリティクラス
	/// </summary>
	public class TextUtil
	{
		/// <summary>
		/// 次を検索し見つかったときは位置を返す
		/// </summary>
		/// <param name="option">検索オプション</param>
		/// <param name="startOffset">検索開始位置</param>
		/// <param name="buffer">検索する文字列バッファ</param>
		/// <returns>見つかった位置</returns>
		public static EditorSearchResult SearchNext(EditorSearchOption option, int startOffset, ITextBufferStrategy buffer)
		{
			EditorSearchResult result = null;
			if (option.Regex)
			{
				//正規表現で検索
				result = getSearchPosRegex(option, startOffset, buffer);
			}
			else
			{
				//通常の文字列として検索
				result = getSearchPosString(option, startOffset, buffer);
			}

			return result;
		}

		/// <summary>
		/// 文字列検索を行いヒットした位置を返す
		/// </summary>
		/// <param name="option">検索オプション</param>
		/// <returns>ヒットした位置</returns>
		private static EditorSearchResult getSearchPosString(EditorSearchOption option, int offset, ITextBufferStrategy buffer)
		{
			EditorSearchResult result = new EditorSearchResult();
			offset = getFirstSearchOffset(offset, option);

			//検索キーワードの設定
			string keyword;
			if (option.IgnoreCase)
			{
				keyword = option.SearchKeyword;
			}
			else
			{
				keyword = option.SearchKeyword.ToUpper();
			}

			//一つづつオフセットをずらしていきヒットするまで続ける
			while ((offset = getNextSearchOffset(offset, buffer.Length, option)) != -1)
			{
				if (matchCaseString(buffer, offset, keyword, option.IgnoreCase))
				{
					if (option.WordUnit == false || isSearchWholeWordAt(buffer, offset, keyword.Length))
					{
						result.Offset = offset;
						break;
					}
				}
			}
			if (offset == -1)
			{
				//ヒットしなかったとき
				result.Offset = -1;
			}

			result.Length = option.SearchKeyword.Length;
			return result;
		}

		/// <summary>
		/// 指定したオプションから検索方向への次のオフセット位置を返す
		/// </summary>
		/// <param name="offset">現在のオフセット位置</param>
		/// <param name="endOffset">一番最後のオフセット位置</param>
		/// <param name="option">検索方向を含むオプション</param>
		/// <returns>次のオフセット位置 次がないときは-1を返す</returns>
		private static int getNextSearchOffset(int offset, int endOffset, EditorSearchOption option)
		{
			int ret = -1;

			if (option.Direction == SearchDirection.Down)
			{
				ret = offset + 1;
				if (ret > endOffset - option.SearchKeyword.Length)
				{
					//最後を超えている
					ret = -1;
				}
			}
			else if (option.Direction == SearchDirection.Up)
			{
				ret = offset - 1;
				if (ret < 0)
				{
					//最初を超えている
					ret = -1;
				}
			}

			return ret;
		}

		/// <summary>
		/// 検索開始のオフセット位置をセットする
		/// </summary>
		/// <param name="offset">現在のオフセット位置</param>
		/// <param name="option"></param>
		/// <returns></returns>
		private static int getFirstSearchOffset(int offset, EditorSearchOption option)
		{
			int ret = -1;
			if (option.Direction == SearchDirection.Down)
			{
				ret = offset - 1;	//一つ上から開始する
			}
			else if (option.Direction == SearchDirection.Up)
			{
				ret = offset;		//同じ位置から開始する
			}

			return ret;
		}

		/// <summary>
		/// 検索にヒットするかどうかチェックする
		/// </summary>
		/// <param name="buff">検索される文字列</param>
		/// <param name="offset">現在のカーソル位置</param>
		/// <param name="pattern">検索する文字列</param>
		/// <param name="ignoreCase">大文字小文字を区別するかどうか</param>
		/// <returns>ヒットしたときtrue</returns>
		private static bool matchCaseString(ITextBufferStrategy buff, int offset, string pattern, bool ignoreCase)
		{
			char c;
			for (int i = 0; i < pattern.Length; ++i)
			{
				if (ignoreCase)
				{
					c = buff.GetCharAt(offset + i);
				}
				else
				{
					c = Char.ToUpper(buff.GetCharAt(offset + i));
				}
				if (offset + i >= buff.Length || c != pattern[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// 指定したオフセット位置の物が単語かどうかチェックする
		/// 単語の時はtrueを返す
		/// </summary>
		/// <param name="buff">検索される文字列</param>
		/// <param name="offset">カーソル位置</param>
		/// <param name="length">検索する文字列の長さ</param>
		/// <returns>単語の時はtrue</returns>
		private static bool isSearchWholeWordAt(ITextBufferStrategy buff, int offset, int length)
		{
			return (offset - 1 < 0 || !Char.IsLetterOrDigit(buff.GetCharAt(offset - 1))) &&
				   (offset + length + 1 >= buff.Length || !Char.IsLetterOrDigit(buff.GetCharAt(offset + length)));
		}

		/// <summary>
		/// 正規表現検索を行い、ヒットした位置を返す
		/// </summary>
		/// <param name="option"></param>
		/// <returns></returns>
		private static EditorSearchResult getSearchPosRegex(EditorSearchOption option, int offset, ITextBufferStrategy buffer)
		{
			EditorSearchResult result = new EditorSearchResult();
			Regex regex = new Regex(option.SearchKeyword, getSearchRegexOptions(option));

			Match match = regex.Match(buffer.GetText(0, buffer.Length), offset);
			while (match.Success)
			{
				if (option.WordUnit == false || isSearchWholeWordAt(buffer, match.Index, match.Value.Length))
				{
					result.Offset = match.Index;
					result.Length = match.Value.Length;
					break;
				}

				match = match.NextMatch();
			}

			return result;
		}

		/// <summary>
		/// 検索する正規表現のオプションを作成し返す
		/// </summary>
		/// <param name="option"></param>
		/// <returns></returns>
		private static RegexOptions getSearchRegexOptions(EditorSearchOption option)
		{
			RegexOptions regOption = RegexOptions.Multiline;
			if (option.IgnoreCase)
			{
				if (option.Direction == SearchDirection.Down)
				{
					regOption = RegexOptions.Multiline;
				}
				else if (option.Direction == SearchDirection.Up)
				{
					regOption = RegexOptions.RightToLeft | RegexOptions.Multiline;
				}
			}
			else
			{
				if (option.Direction == SearchDirection.Down)
				{
					regOption = RegexOptions.Multiline | RegexOptions.IgnoreCase;
				}
				else if (option.Direction == SearchDirection.Up)
				{
					regOption = RegexOptions.RightToLeft | RegexOptions.Multiline | RegexOptions.IgnoreCase;
				}
			}

			return regOption;
		}
	}
}
