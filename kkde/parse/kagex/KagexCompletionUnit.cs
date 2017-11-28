using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace kkde.parse.kagex
{
	//データを格納するテーブル定義
	using DicTable = Dictionary<string, object>;

	/// <summary>
	/// テーブル内データクラス
	/// </summary>
	public class TableItem
	{
		string m_key;
		object m_value;

		public string Key
		{
			get { return m_key; }
			set { m_key = value; }
		}

		public object Value
		{
			get { return m_value; }
			set { m_value = value; }
		}

		public TableItem(string key, object val)
		{
			m_key = key;
			m_value = val;
		}
	}

	/// <summary>
	/// KAGEXの解析結果を保持するクラス
	/// </summary>
	public class KagexCompletionUnit : CompletionUnit
	{
		DicTable m_rootTable = new DicTable();
		Stack<DicTable> m_stack = new Stack<DicTable>();
		Stack<int> m_arrayValStack = new Stack<int>();
		int m_arrayNumber = 0;

		#region 初期化
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public KagexCompletionUnit(string filePath)
			: base(filePath)
		{
		}
		#endregion

		#region データ登録メソッド
		/// <summary>
		/// 辞書を追加する
		/// </summary>
		public void PushDic()
		{
			DicTable table = new DicTable();
			m_stack.Push(table);
		}

		/// <summary>
		/// 辞書を前のものに戻す
		/// </summary>
		public void PopDic()
		{
			if (m_stack.Peek() != null)
			{
				m_rootTable = m_stack.Pop();
			}
		}

		/// <summary>
		/// 現在の辞書を返す
		/// </summary>
		/// <returns></returns>
		public DicTable PeekDic()
		{
			return m_stack.Peek();
		}

		/// <summary>
		/// 辞書にデータを追加する
		/// </summary>
		/// <param name="item"></param>
		public void AddDicItem(TableItem item)
		{
			if (item != null)
			{
				//Debug.WriteLine("Add key=" + item.Key + " val=" + item.Value);
				PeekDic().Add(item.Key, item.Value);
			}
		}

		/// <summary>
		/// データを作成する
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public TableItem CreateDicItem(object key, object value)
		{
			return new TableItem((string)key, value);
		}

		/// <summary>
		/// 配列を追加する
		/// </summary>
		public void PushArray()
		{
			DicTable table = new DicTable();
			m_stack.Push(table);
			m_arrayValStack.Push(m_arrayNumber);
			m_arrayNumber = 0;
		}

		/// <summary>
		/// 配列を前のものに戻す
		/// </summary>
		public void PopArray()
		{
			if (m_stack.Peek() != null)
			{
				this.m_rootTable = m_stack.Pop();
				this.m_arrayNumber = m_arrayValStack.Pop();
			}
		}

		/// <summary>
		/// 現在の配列データを返す
		/// </summary>
		/// <returns></returns>
		public DicTable PeekArray()
		{
			return m_stack.Peek();
		}

		/// <summary>
		/// 配列データを追加する
		/// </summary>
		/// <param name="val"></param>
		public void AddArray(object val)
		{
			if (val != null)
			{
				PeekArray().Add(m_arrayNumber.ToString(), val);
				this.m_arrayNumber++;
			}
		}
		#endregion

		#region データ取得用メソッド
		/// <summary>
		/// 指定したキーとテーブルから子キーのリストを作成し返す
		/// </summary>
		/// <param name="key">キー 例:"stages"</param>
		/// <param name="rootTable">キーを探すテーブル</param>
		/// <returns>キーリスト</returns>
		private List<string> getKeyList(string key, DicTable rootTable)
		{
			List<string> keyList = new List<string>();
			if (rootTable == null)
			{
				return keyList;	//空を返す
			}

			DicTable table = null;
			foreach (string k in rootTable.Keys)
			{
				if (k == key)
				{
					table = (DicTable)rootTable[key];
					break;
				}
			}
			if (table == null)
			{
				//見つからなかった
				return keyList;	//空を返す
			}

			foreach (string k in table.Keys)
			{
				keyList.Add(k);
			}

			keyList.Sort();
			return keyList;
		}

		/// <summary>
		/// ルートテーブルから子キーのリストを作成し返す
		/// </summary>
		/// <param name="key">取得するリストの親キー</param>
		/// <returns>キーリスト</returns>
		private List<string> getKeyList(string key)
		{
			return getKeyList(key, m_rootTable);
		}

		/// <summary>
		/// 指定したキーの値を取得する
		/// </summary>
		/// <param name="topKey">1つめのキー（例："stages"）</param>
		/// <param name="secondKey">2つめのキー（例："カプセル背景"）</param>
		/// <param name="valueKey">値を取得するキー（例："image"）</param>
		/// <returns>取得した値 取得できなかったときは空文字を返す</returns>
		private string getSecondRankValueString(string topKey, string secondKey, string valueKey)
		{
			return getSecondRankValueString(this.m_rootTable, topKey, secondKey, valueKey);
		}

		/// <summary>
		/// 指定したキーの値を取得する
		/// </summary>
		/// <param name="rootTable">検索を開始するルートのテーブル</param>
		/// <param name="topKey">1つめのキー（例："stages"）</param>
		/// <param name="secondKey">2つめのキー（例："カプセル背景"）</param>
		/// <param name="valueKey">値を取得するキー（例："image"）</param>
		/// <returns>取得した値 取得できなかったときは空文字を返す</returns>
		private string getSecondRankValueString(DicTable rootTable, string topKey, string secondKey, string valueKey)
		{
			if (rootTable == null)
			{
				return "";
			}

			string str = "";
			DicTable table = rootTable[topKey] as DicTable;
			if (table != null)
			{
				DicTable table2 = table[secondKey] as DicTable;
				if ((table2 != null) && (table2[valueKey] is string))
				{
					str = table2[valueKey].ToString();
				}
			}

			return str;
		}

		/// <summary>
		/// ルートテーブルの指定したキーの値を取得する
		/// </summary>
		/// <param name="key">取得する値のキー</param>
		/// <returns>値</returns>
		private string getTableValue(string key)
		{
			return getTableValue(m_rootTable, key);
		}

		/// <summary>
		/// 指定したテーブルでkeyに対応する値を取得する
		/// </summary>
		/// <param name="rootTable">検索するテーブル</param>
		/// <param name="key">取得する値のキー</param>
		/// <returns>値</returns>
		private string getTableValue(DicTable rootTable, string key)
		{
			string val = rootTable[key] as string;
			if (val == null)
			{
				val = "";	//見つからなかったので空文字を返す
			}

			return val;
		}

		/// <summary>
		/// 姿勢以下を持つテーブルを取得する
		/// </summary>
		/// <param name="charName">キャラクタ名</param>
		/// <returns>姿勢以下を持つテーブル</returns>
		private DicTable getCharPosesTable(string charName)
		{
			DicTable charTable = m_rootTable["characters"] as DicTable;
			if (charTable == null)
			{
				return null;
			}

			DicTable nameTable = charTable[charName] as DicTable;
			if (nameTable == null)
			{
				return null;
			}

			DicTable poseTable = nameTable["poses"] as DicTable;
			if (poseTable == null)
			{
				return null;
			}

			return poseTable;
		}

		/// <summary>
		/// 姿勢の下にあるテーブルから指定したキーに対応する値を取得する
		/// </summary>
		/// <param name="name">キャラクタ名</param>
		/// <param name="pose">姿勢名</param>
		/// <param name="key">キー</param>
		/// <returns>値</returns>
		private string getCharPoseChildKeyVal(string name, string pose, string key)
		{
			string val = "";
			DicTable table = getCharPosesTable(name);
			if (table != null && table[pose] != null)
			{
				DicTable poseTable = table[pose] as DicTable;
				if (poseTable[key] is string)
				{
					val = (string)poseTable[key];
				}
			}

			return val;
		}

		/// <summary>
		/// キャラクタ名リストを取得する
		/// </summary>
		/// <returns>キャラクタ名リスト</returns>
		public List<string> GetCharNameList()
		{
			return this.getKeyList("characters");
		}

		/// <summary>
		/// 指定したキャラクタのデフォルト姿勢を取得する
		/// </summary>
		/// <param name="charName"></param>
		/// <returns></returns>
		public string GetCharDefaultPose(string charName)
		{
			return getSecondRankValueString("characters", charName, "defaultPose");
		}

		/// <summary>
		/// 姿勢名リストを取得する
		/// </summary>
		/// <param name="charName">キャラクタ名</param>
		/// <returns>姿勢名リスト</returns>
		public List<string> GetCharPoseList(string charName)
		{
			List<string> list = new List<string>();
			DicTable table = m_rootTable["characters"] as DicTable;
			if (table != null)
			{
				list = this.getKeyList("poses", table[charName] as DicTable);
				list.Remove("defaultPose");
				list.Remove("defaultTrans");
			}

			return list;
		}

		/// <summary>
		/// 表情名リストを取得する
		/// </summary>
		/// <param name="charName">キャラクタ名</param>
		/// <param name="charPose">姿勢名</param>
		/// <returns>表情名リスト</returns>
		public List<string> GetCharFaceList(string charName, string charPose)
		{
			List<string> list = new List<string>();
			DicTable table = this.getCharPosesTable(charName);
			if (table == null)
			{
				return list;
			}

			list = this.getKeyList("faces", table[charPose] as DicTable);
			return list;
		}

		/// <summary>
		/// 服装名リストを取得する
		/// </summary>
		/// <param name="charName">キャラクタ名</param>
		/// <param name="charPose">姿勢名</param>
		/// <returns>服装名リスト</returns>
		public List<string> GetCharDressList(string charName, string charPose)
		{
			List<string> list = new List<string>();
			DicTable table = this.getCharPosesTable(charName);
			if (table == null)
			{
				return list;
			}

			list = this.getKeyList("dresses", table[charPose] as DicTable);
			return list;
		}

		/// <summary>
		/// キャラクタの画像名テンプレート値を取得する
		/// （例：温子_頭手姿勢_LEVEL_DRESS_FACE）
		/// </summary>
		/// <param name="name">キャラクタ名</param>
		/// <param name="pose">姿勢名</param>
		/// <returns>画像テンプレート値</returns>
		public string GetCharImageValue(string name, string pose)
		{
			return getCharPoseChildKeyVal(name, pose, "image");
		}

		/// <summary>
		/// デフォルトの服装名を取得する
		/// </summary>
		/// <param name="name">キャラクタ名</param>
		/// <param name="pose">姿勢名</param>
		/// <returns>デフォルトの服装名</returns>
		public string GetCharDefaultDressValue(string name, string pose)
		{
			return getCharPoseChildKeyVal(name, pose, "defaultDress");
		}

		/// <summary>
		/// デフォルトの表情名を取得する
		/// </summary>
		/// <param name="name">キャラクタ名</param>
		/// <param name="pose">姿勢名</param>
		/// <returns>デフォルトの表情名</returns>
		public string GetCharDefaultFaceValue(string name, string pose)
		{
			return getCharPoseChildKeyVal(name, pose, "defaultFace");
		}

		/// <summary>
		/// 指定した服装の値を取得する
		/// </summary>
		/// <param name="name">キャラクタ名</param>
		/// <param name="pose">姿勢名</param>
		/// <param name="dress">服装名</param>
		/// <returns>服装の値</returns>
		public string GetCharDressValue(string name, string pose, string dress)
		{
			string str = "";
			DicTable table = getCharPosesTable(name);
			if (table != null)
			{
				if (table[pose] != null)
				{
					str = this.getSecondRankValueString(table, pose, "dresses", dress);
				}
			}

			return str;
		}

		/// <summary>
		/// 指定した表情の値を取得する
		/// </summary>
		/// <param name="name">キャラクタ名</param>
		/// <param name="pose">姿勢名</param>
		/// <param name="face">表情名</param>
		/// <returns>表情の値</returns>
		public string GetCharFaceValue(string name, string pose, string face)
		{
			string str = "";
			DicTable table = getCharPosesTable(name);
			if (table != null)
			{
				if (table[pose] != null)
				{
					str = this.getSecondRankValueString(table, pose, "faces", face);
				}
			}

			return str;
		}

		/// <summary>
		/// ポジション名リストを取得する
		/// </summary>
		/// <returns></returns>
		public List<string> GetPosList()
		{
			return this.getKeyList("positions");
		}

		/// <summary>
		/// アクション名リストを取得する
		/// </summary>
		/// <returns></returns>
		public List<string> GetActionList()
		{
			return this.getKeyList("actions");
		}

		/// <summary>
		/// イベント名リストを取得する
		/// </summary>
		/// <returns></returns>
		public List<string> GetEventList()
		{
			return this.getKeyList("events");
		}

		/// <summary>
		/// イベントのimageキーの値を取得する
		/// </summary>
		/// <param name="eventName">イベント名</param>
		/// <returns>imageキーの値</returns>
		public string GetEventImage(string eventName)
		{
			return this.getSecondRankValueString("events", eventName, "image");
		}

		/// <summary>
		/// 舞台名リストを取得する
		/// </summary>
		/// <returns>舞台名リスト</returns>
		public List<string> GetStageList()
		{
			return this.getKeyList("stages");
		}

		/// <summary>
		/// 舞台のimageキーの値を取得する
		/// </summary>
		/// <param name="stageName">舞台名</param>
		/// <returns>imageキーの値</returns>
		public string GetStageImage(string stageName)
		{
			return this.getSecondRankValueString("stages", stageName, "image");
		}

		/// <summary>
		/// 時間名リストを取得する
		/// </summary>
		/// <returns>時間名リスト</returns>
		public List<string> GetTimeList()
		{
			return this.getKeyList("times");
		}

		/// <summary>
		/// 時間のprefixキーの値を取得する
		/// </summary>
		/// <param name="timeName">時間名</param>
		/// <returns>prefixキーの値</returns>
		public string GetTimePrefix(string timeName)
		{
			return this.getSecondRankValueString("times", timeName, "prefix");
		}
		
		/// <summary>
		/// トランジション名リストを取得する
		/// </summary>
		/// <returns>トランジション名リスト</returns>
		public List<string> GetTransList()
		{
			return this.getKeyList("transitions");
		}

		/// <summary>
		/// レベル名リストを取得する
		/// </summary>
		/// <returns>レベル名リスト</returns>
		public List<string> GetLevelList()
		{
			return this.getKeyList("levels");
		}

		/// <summary>
		/// レベルのnameキー値を取得する
		/// </summary>
		/// <param name="level">レベル名</param>
		/// <returns>nameキー値</returns>
		public string GetLevelName(string level)
		{
			return getSecondRankValueString("levels", level, "name");
		}

		/// <summary>
		/// ルート直下のデフォルト時間を取得する
		/// </summary>
		/// <returns>デフォルト時間</returns>
		public string GetDefaultTime()
		{
			return getTableValue("defaultTime");
		}

		/// <summary>
		/// ルート直下のデフォルトレベルを取得する
		/// </summary>
		/// <returns>デフォルトレベル</returns>
		public string GetDefaultLevel()
		{
			return getTableValue("defaultLevel");
		}
		#endregion

		/// <summary>
		/// デバッグ表示その１
		/// </summary>
		public void DebugPrint()
		{
			List<string> list;
			list = GetStageList();
			foreach (string str in list)
			{
				Debug.WriteLine(str);
			}

			list = GetTimeList();
			foreach (string str in list)
			{
				Debug.WriteLine(str);
			}

			list = GetEventList();
			foreach (string str in list)
			{
				Debug.WriteLine(str);
			}

			list = GetActionList();
			foreach (string str in list)
			{
				Debug.WriteLine(str);
			}

			list = GetTransList();
			foreach (string str in list)
			{
				Debug.WriteLine(str);
			}

			list = GetPosList();
			foreach (string str in list)
			{
				Debug.WriteLine(str);
			}

			list = GetCharNameList();
			foreach (string name in list)
			{
				Debug.WriteLine(name);
				List<string> poseList = GetCharPoseList(name);
				foreach (string pose in poseList)
				{
					Debug.WriteLine("\t" + pose);
					Debug.WriteLine("\t\timage=" + GetCharImageValue(name, pose));
					Debug.WriteLine("\t\tdefaultDress=" + GetCharDefaultDressValue(name, pose));
					Debug.WriteLine("\t\tdefaultFace=" + GetCharDefaultFaceValue(name, pose));

					Debug.WriteLine("\t\tdress:");
					List<string> dressList = GetCharDressList(name, pose);
					foreach (string dress in dressList)
					{
						Debug.WriteLine("\t\t\t" + dress + " => " + GetCharDressValue(name, pose, dress));
					}

					Debug.WriteLine("\t\tface:");
					List<string> faceList = GetCharFaceList(name, pose);
					foreach (string face in faceList)
					{
						Debug.WriteLine("\t\t\t" + face + " => " + GetCharFaceValue(name, pose, face));
					}
				}

			}
		}
	}
}
