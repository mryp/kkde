using System;
using System.Collections.Generic;
using System.Text;

using ICSharpCode.TextEditor.Gui.CompletionWindow;
using kkde.global;
using kkde.project;
using kkde.option;

namespace kkde.parse.complate
{
	/// <summary>
	/// KAG入力補完リスト表示アイテムクラス
	/// </summary>
	public class KagCompletionData : ICompletionData
	{
		/// <summary>
		/// アイテムを表す種類
		/// </summary>
		public enum Kind
		{
			/// <summary>
			/// KAGタグ名
			/// </summary>
			KagTagName,
			/// <summary>
			/// KAGEXタグ名
			/// </summary>
			KagexTagName,
			/// <summary>
			/// ユーザーマクロ名
			/// </summary>
			UserTagName,
			/// <summary>
			/// 属性名
			/// </summary>
			AttrName,
			/// <summary>
			/// 属性値
			/// </summary>
			AttrValue,
			/// <summary>
			/// その他
			/// </summary>
			Unknown,
		}

		#region フィールド
		string m_text = "";
		string m_description = "";
		Kind m_kind = Kind.Unknown;
		#endregion

		#region プロパティ
		/// <summary>
		/// 表示するアイコン番号
		/// </summary>
		public int ImageIndex
		{
			get 
			{
				return getImageIndexFormKind(m_kind); 
			}
		}

		/// <summary>
		/// 入力補完リストに表示する文字列
		/// </summary>
		public string Text
		{
			get
			{
				return m_text;
			}
			set
			{
				m_text = value;
			}
		}

		/// <summary>
		/// 項目選択時に表示される詳細説明
		/// </summary>
		public string Description
		{
			get { return m_description; }
		}

		/// <summary>
		/// 優先度（次回表示時の重み付け？）
		/// </summary>
		public double Priority
		{
			get { return 0; }
		}
		#endregion

		#region メソッド
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="text">リストに表示する文字列</param>
		/// <param name="description">項目選択のTips</param>
		public KagCompletionData(string text, string description, Kind kind)
		{
			m_text = text;
			m_description = description;
			m_kind = kind;
		}

		/// <summary>
		/// 項目選択決定時にエディタへのアクション
		/// </summary>
		/// <param name="textArea">操作するエディタ</param>
		/// <param name="ch">入力された文字</param>
		/// <returns>成功したときtrue</returns>
		public bool InsertAction(ICSharpCode.TextEditor.TextArea textArea, char ch)
		{
			string text = "";
			EditorOption option = GlobalStatus.EditorManager.GetEditorOption(FileType.KrkrType.Kag);
			if ((m_kind == Kind.AttrValue) && option.KagCompOption.UseAttrValueDqRegion)
			{
				text = "\"" + m_text + "\"";
			}
			else
			{
				text = m_text;
			}

			textArea.InsertString(text);
			return false;
		}

		/// <summary>
		/// 並び替え用比較関数
		/// </summary>
		/// <param name="obj">TjsCompletionDataのオブジェクト</param>
		/// <returns>比較結果</returns>
		public int CompareTo(object obj)
		{
			if (obj == null || !(obj is KagCompletionData))
			{
				return -1;
			}
			return m_text.CompareTo(((KagCompletionData)obj).Text);	//文字列で比較する
		}

		/// <summary>
		/// 種類から画像番号を取得する
		/// </summary>
		/// <param name="m_kind"></param>
		/// <returns></returns>
		private int getImageIndexFormKind(Kind kind)
		{
			kkde.editor.CompletionImageList.Index index = 0;
			switch (kind)
			{
				case Kind.KagTagName:
					index = kkde.editor.CompletionImageList.Index.Class;
					break;
				case Kind.KagexTagName:
					index = kkde.editor.CompletionImageList.Index.ExClass;
					break;
				case Kind.UserTagName:
					index = kkde.editor.CompletionImageList.Index.PrivateClass;
					break;
				case Kind.AttrName:
					index = kkde.editor.CompletionImageList.Index.Method;
					break;
				case Kind.AttrValue:
					index = kkde.editor.CompletionImageList.Index.Field;
					break;
			}

			return (int)index;
		}
		#endregion
	}
}
