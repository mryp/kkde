using System;
using System.Collections.Generic;
using System.Text;
using kkde.global;
using System.Windows.Forms;

namespace kkde.kagex
{
	/// <summary>
	/// 立ち絵ツリーを作成するクラス
	/// </summary>
	public class WorldExCharTreeNode
	{
		/// <summary>
		/// キャラクタオブジェクトを表すクラス
		/// </summary>
		private class CharObject
		{
			string m_name = "";
			string m_pose = "";
			string m_dress = "";
			string m_face = "";

			/// <summary>
			/// 名前
			/// </summary>
			public string Name
			{
				get { return m_name; }
				set { m_name = value; }
			}

			/// <summary>
			/// 姿勢
			/// </summary>
			public string Pose
			{
				get { return m_pose; }
				set { m_pose = value; }
			}

			/// <summary>
			/// 服装
			/// </summary>
			public string Dress
			{
				get { return m_dress; }
				set { m_dress = value; }
			}

			/// <summary>
			/// 表情
			/// </summary>
			public string Face
			{
				get { return m_face; }
				set { m_face = value; }
			}

			/// <summary>
			/// コンストラクタ
			/// </summary>
			public CharObject()
			{
			}

			/// <summary>
			/// タグ文字を返す（@ などは付加しない）
			/// </summary>
			/// <returns></returns>
			public string ToTagString()
			{
				string tag = String.Format("{0} {1} {2} {3}", Name, Pose, Dress, Face);
				return tag.Trim();	//空白を削除
			}
		}

		/// <summary>
		/// キャラクターのパーツ
		/// </summary>
		private enum CharParts
		{
			/// <summary>
			/// 名前
			/// </summary>
			Name,
			/// <summary>
			/// 姿勢
			/// </summary>
			Pose,
			/// <summary>
			/// 服装
			/// </summary>
			Dress,
			/// <summary>
			/// 表情
			/// </summary>
			Face,
		}

		/// <summary>
		/// KAGEX解析結果を保持する変数
		/// </summary>
		kkde.parse.kagex.KagexCompletionUnit m_cu = null;

		/// <summary>
		/// ルートノード
		/// </summary>
		TreeNode m_rootNode = null;

		/// <summary>
		/// 検索フィルター文字列
		/// </summary>
		string m_searchFilter = "";

		/// <summary>
		/// 現在選択しているキャラクターパーツ
		/// </summary>
		CharParts m_selectedCharParts = CharParts.Name;

		/// <summary>
		/// ルートノードを取得する
		/// </summary>
		public TreeNode RootNode
		{
			get { return m_rootNode; }
		}

		public string SearchFilter
		{
			get { return m_searchFilter; }
			set { m_searchFilter = value; }
		}

		private CharParts SelectedCharParts
		{
			get { return m_selectedCharParts; }
			set { m_selectedCharParts = value; }
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public WorldExCharTreeNode()
		{
			m_rootNode = new TreeNode("立ち絵");
			initChar();
		}

		/// <summary>
		/// キャラクターの状態を更新する
		/// </summary>
		private void initChar()
		{
			m_cu = GlobalStatus.ParserSrv.GetKagexEnvinitInfo();
			if (m_cu == null)
			{
				return;	//初期化できるデータがない
			}
			m_rootNode.Nodes.Clear();

			List<string> nameList = m_cu.GetCharNameList();
			foreach (string name in nameList)				//名前追加
			{
				if (filterCharItem(name, CharParts.Name) == false)
				{
					continue;	//表示できない
				}
				TreeNode nameNode = m_rootNode.Nodes.Add(name);

				List<string> poseList = m_cu.GetCharPoseList(name);
				foreach (string pose in poseList)			//姿勢追加
				{
					if (filterCharItem(pose, CharParts.Pose) == false)
					{
						continue;	//表示できない
					}
					TreeNode poseNode = nameNode.Nodes.Add(pose);

					List<string> dressList = m_cu.GetCharDressList(name, pose);
					List<string> faceList = m_cu.GetCharFaceList(name, pose);
					foreach (string dress in dressList)		//服装追加
					{
						if (filterCharItem(dress, CharParts.Dress) == false)
						{
							continue;	//表示できない
						}
						TreeNode dressNode = poseNode.Nodes.Add(dress);

						foreach (string face in faceList)	//表情追加
						{
							if (filterCharItem(face, CharParts.Face) == false)
							{
								continue;	//表示できない
							}
							dressNode.Nodes.Add(face);
						}
					}
				}
			}
		}

		/// <summary>
		/// 指定した文字列がフィルターをかけて表示できるかどうかチェックする
		/// </summary>
		/// <param name="text"></param>
		/// <param name="parts"></param>
		/// <returns>trueのとき表示可能</returns>
		private bool filterCharItem(string text, CharParts parts)
		{
			if (m_searchFilter == "")
			{
				return true;	//検索が指定されていないときはOKとする
			}
			if (m_selectedCharParts != parts)
			{
				return true;	//検索対象ではないのでOKとする
			}

			if (text.IndexOf(m_searchFilter) == -1)
			{
				return false;	//見つからなかった
			}
			else
			{
				return true;	//見つかった
			}
		}

		/// <summary>
		/// 指定したノードのキャラクタオブジェクトを取得する
		/// </summary>
		/// <param name="node">キャラクタオブジェクトを取得するノード</param>
		/// <returns>キャラクタオブジェクト</returns>
		public string GetCharText(TreeNode node)
		{
			if (node == null)
			{
				return null;
			}

			//キャラクターツリーは必ず"名前\姿勢\服装\表情"の順番で並んでいるので
			//それらを分割してオブジェクトに格納する
			string[] itemList = node.FullPath.Split('\\');
			if (itemList == null)
			{
				return null;
			}

			CharObject charObject = new CharObject();
			for (int i = 0; i < itemList.Length; i++)
			{
				switch (i)
				{
					case 0:	//名前
						charObject.Name = itemList[i];
						break;
					case 1:	//姿勢
						charObject.Pose = itemList[i];
						break;
					case 2:	//服装
						charObject.Dress = itemList[i];
						break;
					case 3:	//表情
						charObject.Face = itemList[i];
						break;
				}
			}

			return charObject.ToTagString();
		}
	}
}
