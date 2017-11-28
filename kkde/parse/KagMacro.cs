using System;
using System.Collections.Generic;
using System.Text;
using kkde.option;

namespace kkde.parse
{
	/// <summary>
	/// KAGマクロを保持するクラス
	/// </summary>
	public class KagMacro
	{
		/// <summary>
		/// マクロを定義している場所
		/// </summary>
		public enum DefineType
		{
			/// <summary>
			/// KAGで定義されている
			/// </summary>
			Kag,
			/// <summary>
			/// KAGEXで定義されている
			/// </summary>
			Kagex,
			/// <summary>
			/// ユーザーで定義されている
			/// </summary>
			User,
		}

		string m_name;
		string m_comment;
		string m_filePath;
		int m_lineNumber;
		Dictionary<string, KagMacroAttr> m_attrTable = new Dictionary<string, KagMacroAttr>();
		List<string> m_asteriskTagList = new List<string>();
		DefineType m_deftype = DefineType.Kagex;

		/// <summary>
		/// マクロ名
		/// </summary>
		public string Name
		{
			get { return m_name; }
			set { m_name = value; }
		}
		
		/// <summary>
		/// マクロの説明
		/// </summary>
		public string Comment
		{
			get { return m_comment; }
			set { m_comment = value; }
		}
		
		/// <summary>
		/// マクロが記述されているファイルパス
		/// </summary>
		public string FilePath
		{
			get { return m_filePath; }
			set 
			{
				m_filePath = value;
				if (m_filePath == ConstEnvOption.Kag3tagDefFilePath)
				{
					m_deftype = DefineType.Kag;
				}
				else if (m_filePath == ConstEnvOption.KagextagDefFilePath)
				{
					m_deftype = DefineType.Kagex;
				}
				else if (m_filePath == ConstEnvOption.WorldextagDefFilePath)
				{
					m_deftype = DefineType.Kagex;
				}
				else
				{
					m_deftype = DefineType.User;
				}
			}
		}
		
		/// <summary>
		/// マクロが記述されている行番号
		/// </summary>
		public int LineNumber
		{
			get { return m_lineNumber; }
			set { m_lineNumber = value; }
		}
		
		/// <summary>
		/// 属性テーブル
		/// </summary>
		public Dictionary<string, KagMacroAttr> AttrTable
		{
			get { return m_attrTable; }
			set { m_attrTable = value; }
		}

		/// <summary>
		/// 属性省略（＊）がかかれているタグ名リスト
		/// </summary>
		public List<string> AsteriskTagList
		{
			get { return m_asteriskTagList; }
			set { m_asteriskTagList = value; }
		}

		/// <summary>
		/// タグの定義場所
		/// </summary>
		public DefineType DefType
		{
			get { return m_deftype; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public KagMacro()
		{
		}

		/// <summary>
		/// ＊が存在するタグ名をリストに追加する
		/// </summary>
		/// <param name="tagName">＊のタグ名</param>
		public void AddAsteriskTagName(string tagName)
		{
			if (tagName == null || tagName == "")
			{
				System.Diagnostics.Debug.WriteLine("AddAsteriskTagName param err!");
				return;
			}

			if (m_asteriskTagList.Contains(tagName) == false)
			{
				m_asteriskTagList.Add(tagName);	//存在しないときだけ追加する
			}
		}

		/// <summary>
		/// マクロ属性を追加する
		/// </summary>
		/// <param name="attr">追加する属性</param>
		public void AddAttr(KagMacroAttr attr)
		{
			if (attr == null || attr.Name == null || attr.Name == "")
			{
				System.Diagnostics.Debug.WriteLine("AddAttr param err!");
				return;
			}
			if (m_attrTable.ContainsKey(attr.Name) == false)
			{
				m_attrTable.Add(attr.Name, attr);	//ぞんざいしないときだけ追加する
			}
		}

		/// <summary>
		/// 属性名から属性オブジェクトを返す
		/// </summary>
		/// <param name="attrName">検索する属性名</param>
		/// <returns>属性オブジェクト 見つからなかったときはnullを返す</returns>
		public KagMacroAttr GetMacroAttr(string attrName)
		{
			if (attrName == "")
			{
				return null;
			}
			if (m_attrTable.ContainsKey(attrName) == false)
			{
				return null;
			}
			return m_attrTable[attrName];
		}
	}
}
