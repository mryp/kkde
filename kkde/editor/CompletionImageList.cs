using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace kkde.editor
{
	/// <summary>
	/// 入力補完用イメージリスト
	/// </summary>
	public class CompletionImageList
	{
		/// <summary>
		/// 画像インデックス
		/// </summary>
		public enum Index
		{
			Class = 0,
			Method = 1,
			PrivateMethod = 2,
			Property = 3,
			Field = 4,
			PrivateField = 5,
			PrivateClass = 6,
			ExClass = 7,
		}

		/// <summary>
		/// イメージリスト
		/// </summary>
		ImageList m_imageList = null;

		/// <summary>
		/// イメージリスト
		/// </summary>
		public ImageList ImageList
		{
			get { return m_imageList; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="imageList">イメージリスト</param>
		public CompletionImageList(ImageList imageList)
		{
			m_imageList = imageList;
		}
	}
}
