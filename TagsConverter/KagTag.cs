using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TagsConverter
{
	/// <summary>
	/// KAGタグの説明内容を保持するクラス
	/// </summary>
	public class KagTag
	{
		string m_name = "";
		string m_shortInfo = "";
		string m_group = "";
		string m_remarks = "";
		Dictionary<string, KagTagAttr> m_attrTable = new Dictionary<string, KagTagAttr>();

		KagTagAttr m_condAttr = new KagTagAttr();
		KagTagAttr m_delayrunAttr = null;

		/// <summary>
		/// タグ名
		/// </summary>
		public string Name
		{
			get { return m_name; }
			set 
			{
				m_name = value;
				AddCondAttr();
			}
		}

		/// <summary>
		/// タグの短い説明
		/// </summary>
		public string ShortInfo
		{
			get { return m_shortInfo; }
			set { m_shortInfo = value; }
		}

		/// <summary>
		/// タグのグループ
		/// </summary>
		public string Group
		{
			get { return m_group; }
			set { m_group = value; }
		}

		/// <summary>
		/// タグの長い説明
		/// </summary>
		public string Remarks
		{
			get { return m_remarks; }
			set { m_remarks = value; }
		}
		
		/// <summary>
		/// 属性テーブル
		/// </summary>
		public Dictionary<string, KagTagAttr> AttrTable
		{
			get { return m_attrTable; }
			set { m_attrTable = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public KagTag(bool kagex)
		{
			//cond属性の初期化
			m_condAttr.Name = "cond";
			m_condAttr.ShortInfo = "TJS式評価";
			m_condAttr.Info = "式を評価した結果が真の時のみにそのタグが実行されます";
			m_condAttr.Required = false;
			m_condAttr.Format = "TJS式";

			if (kagex)
			{
				m_delayrunAttr = new KagTagAttr();
				m_delayrunAttr.Name = "delayrun";
				m_delayrunAttr.ShortInfo = "遅延実行パラメーター指定";
				m_delayrunAttr.Info = "遅延実行を行うパラメーターを指定します（数値を指定すると指定したミリ秒後に実行され、文字列を指定するとBGM/SEの該当するラベルのタイミングで実行されます）。";
				m_delayrunAttr.Required = false;
				m_delayrunAttr.Format = "ミリ秒時間;vl1;vl2;vl3;任意文字列";
			}
		}

		/// <summary>
		/// 長い説明を必要な分だけ取得する
		/// </summary>
		/// <param name="remarks"></param>
		public void SetRemarks(string remarks)
		{
			int pos = remarks.IndexOf("。");
			if (pos == -1)
			{
				m_remarks = remarks.Trim();
				return;
			}

			m_remarks = remarks.Substring(0, pos).Trim();
			return;
		}

		/// <summary>
		/// cond属性を追加する
		/// </summary>
		public void AddCondAttr()
		{
			if (m_name == "")
			{
				return;	//何もしない
			}

			if (HaveCondAttr(m_name) == true)
			{
				if (m_attrTable.ContainsKey("cond") == false)
				{
					//cond属性を追加
					m_attrTable.Add(m_condAttr.Name, m_condAttr);
				}
				if (m_delayrunAttr != null && m_attrTable.ContainsKey("delayrun") == false)
				{
					m_attrTable.Add(m_delayrunAttr.Name, m_delayrunAttr);
				}
			}

			return;
		}

		/// <summary>
		/// cond属性を持っているかどうか
		/// 持っているときtrue
		/// </summary>
		/// <returns></returns>
		public static bool HaveCondAttr(string name)
		{
			bool ret;

			switch (name)
			{
				case "macro":
				case "endmacro":
				case "if":
				case "else":
				case "elsif":
				case "endif":
				case "ignore":
				case "endignore":
				case "iscript":
				case "endscript":
					ret = false;
					break;
				default:
					ret = true;
					break;
			}

			return ret;
		}
	}
}
