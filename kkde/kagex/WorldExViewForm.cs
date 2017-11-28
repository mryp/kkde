using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using kkde.global;
using kkde.project;
using kkde.option;

namespace kkde.kagex
{
	/// <summary>
	/// ワールド拡張ビューワー
	/// </summary>
	public partial class WorldExViewForm : WeifenLuo.WinFormsUI.DockContent
	{
		/// <summary>
		/// 現在選択しているビューのオプションを保持するクラス
		/// </summary>
		private class ObjectOption
		{
			bool isAddedTagSign = true;
			bool isAddedMsgTag = true;

			bool isBgmTag = false;
			bool isSeTag = false;

			/// <summary>
			/// タグ記号（@ or [）を追加するかどうか
			/// </summary>
			public bool IsAddedTagSign
			{
				get { return isAddedTagSign; }
				set { isAddedTagSign = value; }
			}

			/// <summary>
			/// msgoff-msgonタグで囲むかどうか
			/// </summary>
			public bool IsAddedMsgTag
			{
				get { return isAddedMsgTag; }
				set { isAddedMsgTag = value; }
			}

			/// <summary>
			/// BGMタグをかどうか
			/// </summary>
			public bool IsBgmTag
			{
				get { return isBgmTag; }
				set { isBgmTag = value; }
			}

			/// <summary>
			/// SEタグかどうか
			/// </summary>
			public bool IsSeTag
			{
				get { return isSeTag; }
				set { isSeTag = value; }
			}


			/// <summary>
			/// コンストラクタ
			/// </summary>
			/// <param name="tagSign"></param>
			/// <param name="msgTag"></param>
			public ObjectOption(bool tagSign, bool msgTag)
			{
				isAddedTagSign = tagSign;
				isAddedMsgTag = msgTag;
			}
		}

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

		#region 定数
		const string SEARCH_STAGE_PREFIX = "TIME";
		const string SEARCH_CHAR_LEVEL_PREFIX = "LEVEL";
		const string SEARCH_CHAR_DRESS_PREFIX = "DRESS";
		const string SEARCH_CHAR_FACE_PREFIX = "FACE";
		#endregion

		#region フィールド
		/// <summary>
		/// KAGEX解析結果を保持する変数
		/// </summary>
		kkde.parse.kagex.KagexCompletionUnit m_cu = null;

		/// <summary>
		/// 検索フィルター文字列
		/// </summary>
		string m_searchFilter = "";

		/// <summary>
		/// 現在選択しているキャラクターパーツ
		/// </summary>
		CharParts m_selectedCharParts;
		#endregion

		#region 初期化
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public WorldExViewForm()
		{
			InitializeComponent();

			setSearchTarget(CharParts.Name);
		}

		/// <summary>
		/// ツリーを初期化する
		/// </summary>
		public void InitView()
		{
			m_cu = GlobalStatus.ParserSrv.GetKagexEnvinitInfo();
			if (m_cu == null)
			{
				return;	//初期化できるデータがない
			}

			initChar();
			initEvent();
			initStage();
			initTime();
			initPos();
			initTrans();
			initAction();
			initBgm();
			initSe();

			//ツールバーの設定
			EditorOption option = GlobalStatus.EditorManager.GetEditorOption(FileType.KrkrType.Kag);
			if (option == null || option.KagCompOption == null)
			{
				return;	//オプションが取得できない
			}
			toolItemMsgtagState.Checked = option.KagCompOption.IsAddedMsgTagWorldEx;
		}

		/// <summary>
		/// キャラクターの状態を更新する
		/// </summary>
		private void initChar()
		{
			charTreeView.Nodes.Clear();

			List<string> nameList = m_cu.GetCharNameList();
			foreach (string name in nameList)				//名前追加
			{
				if (filterCharItem(name, CharParts.Name) == false)
				{
					continue;	//表示できない
				}
				TreeNode nameNode = charTreeView.Nodes.Add(name);

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
		/// リストビューを指定されたリストで初期化する
		/// </summary>
		/// <param name="listView">初期化するビュー</param>
		/// <param name="initList">初期化するリスト</param>
		private void initListViewItem(ListView listView, List<string> initList)
		{
			if (listView == null || initList == null)
			{
				return;
			}

			listView.Items.Clear();
			foreach (string text in initList)
			{
				if (m_searchFilter != "")
				{
					//フィルターが設定されているときはフィルターで検索する
					if (text.IndexOf(m_searchFilter) == -1)
					{
						continue;	//見つからなかったら登録しない
					}
				}
				listView.Items.Add(text);
			}
		}

		/// <summary>
		/// イベントを初期化する
		/// </summary>
		private void initEvent()
		{
			List<string> list = m_cu.GetEventList();
			initListViewItem(eventListView, list);
		}

		/// <summary>
		/// 背景絵を初期化する
		/// </summary>
		private void initStage()
		{
			List<string> list = m_cu.GetStageList();
			initListViewItem(stageListView, list);
		}

		/// <summary>
		/// 舞台時間を初期化する
		/// </summary>
		private void initTime()
		{
			List<string> list = m_cu.GetTimeList();
			initListViewItem(timeListView, list);
		}

		/// <summary>
		/// ポジションを初期化する
		/// </summary>
		private void initPos()
		{
			List<string> list = m_cu.GetPosList();
			initListViewItem(posListView, list);
		}

		/// <summary>
		/// トランジションを初期化する
		/// </summary>
		private void initTrans()
		{
			List<string> list = m_cu.GetTransList();
			initListViewItem(transListView, list);
		}

		/// <summary>
		/// アクションを初期化する
		/// </summary>
		private void initAction()
		{
			List<string> list = m_cu.GetActionList();
			initListViewItem(actionListView, list);
		}

		/// <summary>
		/// データフォルダの指定したフォルダ以下のファイル名リストを検索し取得する
		/// </summary>
		/// <param name="rootDirName"></param>
		/// <param name="searchExt"></param>
		/// <returns></returns>
		private List<string> getFileNameList(string rootDirName, string searchExt)
		{
			List<string> list = new List<string>();

			//フォルダ以下からBGMを検索しリストを作成する
			string searchDirPath = Path.Combine(GlobalStatus.Project.DataFullPath, rootDirName);
			string[] fileList = util.FileUtil.GetDirectoryFile(searchDirPath, searchExt, SearchOption.AllDirectories);
			if (fileList == null)
			{
				return list;
			}
			
			foreach (string path in fileList)
			{
				list.Add(Path.GetFileNameWithoutExtension(path));
			}
			list.Sort();

			return list;
		}

		/// <summary>
		/// BGMリストを初期化する
		/// </summary>
		private void initBgm()
		{
			EditorOption option = GlobalStatus.EditorManager.GetEditorOption(FileType.KrkrType.Kag);
			if (option == null || option.KagCompOption == null)
			{
				return;	//オプションが取得できない
			}

			List<string> list = getFileNameList(option.KagCompOption.WorldExSearchPathBgm, option.KagCompOption.BgmFileExt);
			initListViewItem(bgmListView, list);
		}

		/// <summary>
		/// 効果音リストを初期化する
		/// </summary>
		private void initSe()
		{
			EditorOption option = GlobalStatus.EditorManager.GetEditorOption(FileType.KrkrType.Kag);
			if (option == null || option.KagCompOption == null)
			{
				return;	//オプションが取得できない
			}

			List<string> list = getFileNameList(option.KagCompOption.WorldExSearchPathSe, option.KagCompOption.SeFileExt);
			initListViewItem(seListView, list);
		}
		#endregion

		#region メニューイベント
		/// <summary>
		/// タグコピーを行う
		/// </summary>
		private void menuItemCopyTag_Click(object sender, EventArgs e)
		{
			ObjectOption option = getSelectedViewOption();
			if (option == null)
			{
				return;
			}

			if (mainTabControl.SelectedTab == charTabPage)
			{
				//立ち絵のときはツリーノードから取得する
				TreeNode selectNode = charTreeView.SelectedNode;
				if (selectNode == null)
				{
					return;
				}

				copyTagSelectedItem(selectNode, option);
			}
			else
			{
				//現在選択表示しているリストビューを検索する
				ListView listView = getSelectedListView();
				if (listView == null)
				{
					return;	//対応するものが見つからない
				}

				copyTagSelectedItem(listView, option);
			}
		}

		/// <summary>
		/// プレビュー
		/// </summary>
		private void menuItemPreview_Click(object sender, EventArgs e)
		{
			EditorOption option = GlobalStatus.EditorManager.GetEditorOption(FileType.KrkrType.Kag);
			if (option == null || option.KagCompOption == null)
			{
				return;	//オプションが取得できない
			}

			string key = "";
			string name = "";
			if (mainTabControl.SelectedTab == charTabPage)
			{
				CharObject charObject = getCharObjectFromNode(charTreeView.SelectedNode);
				if (charObject == null)
				{
					return;	//選択しているものがない
				}

				previewImage(searchCharFilePath(option.KagCompOption.WorldExSearchPathChar, charObject, option.KagCompOption.ImageFileExt));
			}
			else if (mainTabControl.SelectedTab == eventTabPage)
			{
				key = getSelectedItemText(eventListView);
				name = m_cu.GetEventImage(key);
				previewImage(searchFilePath(option.KagCompOption.WorldExSearchPathEvent, name, option.KagCompOption.ImageFileExt));
			}
			else if (mainTabControl.SelectedTab == stageTabPage)
			{
				key = getSelectedItemText(stageListView);
				name = m_cu.GetStageImage(key);
				previewImage(searchStageFilePath(option.KagCompOption.WorldExSearchPathStage, name, option.KagCompOption.ImageFileExt));
			}
			else if (mainTabControl.SelectedTab == bgmTabPage)
			{
				name = getSelectedItemText(bgmListView);
				previewSound(searchFilePath(option.KagCompOption.WorldExSearchPathBgm, name, option.KagCompOption.BgmFileExt));
			}
			else if (mainTabControl.SelectedTab == seTabPage)
			{
				name = getSelectedItemText(seListView);
				previewSound(searchFilePath(option.KagCompOption.WorldExSearchPathSe, name, option.KagCompOption.BgmFileExt));
			}
			else
			{
				MessageBox.Show("この項目はプレビューできません。\n立ち絵・イベント絵・背景絵・BGM・効果音のアイテムです");
			}
		}

		/// <summary>
		/// ポップを開こうとしたとき
		/// </summary>
		private void objectPopMenu_Opening(object sender, CancelEventArgs e)
		{
			if (menuItemCopyTagEx.DropDown.Items.Count == 0)
			{
				if (mainTabControl.SelectedTab == charTabPage)
				{
					menuItemCopyTagEx.Enabled = true;
					createTagExCopyMenuChar();
					menuItemPreview.Enabled = true;
					menuItemPreviewEx.Enabled = true;
				}
				else if (mainTabControl.SelectedTab == eventTabPage)
				{
					menuItemCopyTagEx.Enabled = true;
					createTagExCopyMenuEvent();
					menuItemPreview.Enabled = true;
					menuItemPreviewEx.Enabled = true;
				}
				else if (mainTabControl.SelectedTab == stageTabPage)
				{
					menuItemCopyTagEx.Enabled = true;
					createTagExCopyMenuStrage();
					menuItemPreview.Enabled = true;
					menuItemPreviewEx.Enabled = true;
				}
				else if (mainTabControl.SelectedTab == bgmTabPage)
				{
					menuItemCopyTagEx.Enabled = false;
					menuItemPreview.Enabled = true;
					menuItemPreviewEx.Enabled = false;
					menuItemCopyTagEx.DropDown.Items.Add("dummy");
				}
				else if (mainTabControl.SelectedTab == seTabPage)
				{
					menuItemCopyTagEx.Enabled = false;
					menuItemPreview.Enabled = true;
					menuItemPreviewEx.Enabled = false;
					menuItemCopyTagEx.DropDown.Items.Add("dummy");
				}
				else
				{
					menuItemCopyTagEx.Enabled = false;
					menuItemPreview.Enabled = false;
					menuItemPreviewEx.Enabled = true;
					menuItemCopyTagEx.DropDown.Items.Add("dummy");
				}
			}
		}

		/// <summary>
		/// 立ち絵ツリーを表示しているときの拡張タグコピーメニューを作成する
		/// </summary>
		private void createTagExCopyMenuChar()
		{
			if (m_cu == null)
			{
				return;	//何もしない
			}

			EventHandler handler = new EventHandler(menuItemCopyTagEx_Click);

			//トランジションメニューを作成
			ToolStripDropDownItem transItem = new ToolStripMenuItem("トランジションを追加");
			List<string> transList = m_cu.GetTransList();
			foreach (string trans in transList)
			{
				transItem.DropDown.Items.Add(trans, null, handler);
			}

			//アクションメニューを作成
			ToolStripDropDownItem actionItem = new ToolStripMenuItem("アクションを追加");
			List<string> actionList = m_cu.GetActionList();
			foreach (string action in actionList)
			{
				actionItem.DropDown.Items.Add(action, null, handler);
			}

			//ポジションメニューを作成
			ToolStripDropDownItem posItem = new ToolStripMenuItem("ポジションを追加");
			List<string> posList = m_cu.GetPosList();
			foreach (string pos in posList)
			{
				posItem.DropDown.Items.Add(pos, null, handler);
			}

			//アクション + ポジション メニューを作成
			ToolStripDropDownItem actionAndPosItem = new ToolStripMenuItem("アクション + ポジションを追加");
			foreach (string action in actionList)
			{
				ToolStripDropDownItem item = (ToolStripDropDownItem)actionAndPosItem.DropDown.Items.Add(action);
				foreach (string pos in posList)
				{
					item.DropDown.Items.Add(String.Format("{0} {1}", action, pos), null, handler);
				}
			}

			//トランジション + アクションメニューを作成
			ToolStripDropDownItem transAndActionItem = new ToolStripMenuItem("トランジション + アクションを追加");
			foreach (string trans in transList)
			{
				ToolStripDropDownItem item = (ToolStripDropDownItem)transAndActionItem.DropDown.Items.Add(trans);
				foreach (string action in actionList)
				{
					item.DropDown.Items.Add(String.Format("{0} {1}", trans, action), null, handler);
				}
			}

			//トランジション + ポジション メニューを作成
			ToolStripDropDownItem transAndPosItem = new ToolStripMenuItem("トランジション + ポジションを追加");
			foreach (string trans in transList)
			{
				ToolStripDropDownItem item = (ToolStripDropDownItem)transAndPosItem.DropDown.Items.Add(trans);
				foreach (string pos in posList)
				{
					item.DropDown.Items.Add(String.Format("{0} {1}", trans, pos), null, handler);
				}
			}

			//トランジション + ポジション + アクション メニューを作成
			ToolStripDropDownItem transAndPosAndActionItem = new ToolStripMenuItem("トランジション + ポジション + アクションを追加");
			foreach (string trans in transList)
			{
				ToolStripDropDownItem item1 = (ToolStripDropDownItem)transAndPosAndActionItem.DropDown.Items.Add(trans);
				foreach (string pos in posList)
				{
					ToolStripDropDownItem item2 = (ToolStripDropDownItem)item1.DropDown.Items.Add(
						String.Format("{0} {1}", trans, pos), null, handler);
					foreach (string action in actionList)
					{
						item2.DropDown.Items.Add(String.Format("{0} {1} {2}", trans, pos, action), null, handler);
					}
				}
			}

			//作成したメニューをすべてくっつける
			menuItemCopyTagEx.DropDown.Items.Add(transItem);
			menuItemCopyTagEx.DropDown.Items.Add(actionItem);
			menuItemCopyTagEx.DropDown.Items.Add(posItem);
			menuItemCopyTagEx.DropDown.Items.Add(actionAndPosItem);
			menuItemCopyTagEx.DropDown.Items.Add(transAndActionItem);
			menuItemCopyTagEx.DropDown.Items.Add(transAndPosItem);
			menuItemCopyTagEx.DropDown.Items.Add(transAndPosAndActionItem);
		}

		/// <summary>
		/// イベント絵リストを表示しているときの拡張タグコピーメニューを作成する
		/// </summary>
		private void createTagExCopyMenuEvent()
		{
			EventHandler handler = new EventHandler(menuItemCopyTagEx_Click);

			//トランジションメニューを作成
			ToolStripDropDownItem transItem = new ToolStripMenuItem("トランジションを追加");
			List<string> transList = m_cu.GetTransList();
			foreach (string trans in transList)
			{
				transItem.DropDown.Items.Add(trans, null, handler);
			}

			menuItemCopyTagEx.DropDown.Items.Add(transItem);
		}

		/// <summary>
		/// 背景絵リストを表示しているときの拡張タグコピーメニューを作成する
		/// </summary>
		private void createTagExCopyMenuStrage()
		{
			EventHandler handler = new EventHandler(menuItemCopyTagEx_Click);

			//トランジションメニューを作成
			ToolStripDropDownItem transItem = new ToolStripMenuItem("トランジションを追加");
			List<string> transList = m_cu.GetTransList();
			foreach (string trans in transList)
			{
				transItem.DropDown.Items.Add(trans, null, handler);
			}

			//時間メニューを作成
			ToolStripDropDownItem timeItem = new ToolStripMenuItem("時間を追加");
			List<string> timeList = m_cu.GetTimeList();
			foreach (string time in timeList)
			{
				timeItem.DropDown.Items.Add(time, null, handler);
			}

			//トランジション + 時間 メニューを作成
			ToolStripDropDownItem timeAdnTransItem = new ToolStripMenuItem("時間 + トランジションを追加");
			foreach (string time in timeList)
			{
				ToolStripDropDownItem item = (ToolStripDropDownItem)timeAdnTransItem.DropDown.Items.Add(time);
				foreach (string trans in transList)
				{
					item.DropDown.Items.Add(String.Format("{0} {1}", time, trans), null, handler);
				}
			}

			menuItemCopyTagEx.DropDown.Items.Add(transItem);
			menuItemCopyTagEx.DropDown.Items.Add(timeItem);
			menuItemCopyTagEx.DropDown.Items.Add(timeAdnTransItem);
		}

		/// <summary>
		/// 拡張タグメニューのアイテムをクリックしたとき
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemCopyTagEx_Click(object sender, EventArgs e)
		{
			ObjectOption option = getSelectedViewOption();
			if (option == null)
			{
				return;
			}
			EditorOption editorOption = GlobalStatus.EditorManager.GetEditorOption(FileType.KrkrType.Kag);
			if (editorOption == null || editorOption.KagCompOption == null)
			{
				return;	//オプションが取得できない
			}

			if (mainTabControl.SelectedTab == charTabPage)
			{
				//立ち絵のときはツリーノードから取得する
				TreeNode selectNode = charTreeView.SelectedNode;
				if (selectNode == null)
				{
					return;
				}

				if (editorOption.KagCompOption.IsInsertedTagCopyExWorldEx)
				{
					insertTagSelectedItem(selectNode, option, " " + sender.ToString());
				}
				else
				{
					copyTagSelectedItem(selectNode, option, " " + sender.ToString());
				}
			}
			else
			{
				//現在選択表示しているリストビューを検索する
				ListView listView = getSelectedListView();
				if (listView == null)
				{
					return;	//対応するものが見つからない
				}

				if (editorOption.KagCompOption.IsInsertedTagCopyExWorldEx)
				{
					insertTagSelectedItem(listView, option, " " + sender.ToString());
				}
				else
				{
					copyTagSelectedItem(listView, option, " " + sender.ToString());
				}
			}
			
		}

		/// <summary>
		/// 拡張プレビュー
		/// </summary>
		private void menuItemPreviewEx_Click(object sender, EventArgs e)
		{
		}

		/// <summary>
		/// 拡張プレビュー→吉里吉里で実行
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemPreviewExShow_Click(object sender, EventArgs e)
		{
			ObjectOption option = new ObjectOption(false, false);	//何も付けないモノを作成する

			if (mainTabControl.SelectedTab == charTabPage)
			{
				//立ち絵のときはツリーノードから取得する
				TreeNode selectNode = charTreeView.SelectedNode;
				if (selectNode == null)
				{
					return;
				}

				GlobalStatus.WorldExPreview.SetChar(getSelectedItemText(selectNode, option));
				GlobalStatus.WorldExPreview.Run();
			}
			else
			{
				//現在選択表示しているリストビューを検索する
				ListView listView = getSelectedListView();
				if (listView == null)
				{
					return;	//対応するものが見つからない
				}

				if (mainTabControl.SelectedTab == eventTabPage)
				{
					GlobalStatus.WorldExPreview.SetEvent(getSelectedItemText(listView, option));
				}
				else if (mainTabControl.SelectedTab == stageTabPage)
				{
					GlobalStatus.WorldExPreview.SetStage(getSelectedItemText(listView, option));
				}
				else if (mainTabControl.SelectedTab == timeTabPage)
				{
					GlobalStatus.WorldExPreview.SetTime(getSelectedItemText(listView, option));
				}
				else if (mainTabControl.SelectedTab == transTabPage)
				{
					GlobalStatus.WorldExPreview.SetTrans(getSelectedItemText(listView, option));
				}
				else if (mainTabControl.SelectedTab == actionTabPage)
				{
					GlobalStatus.WorldExPreview.SetAction(getSelectedItemText(listView, option));
				}
				else if (mainTabControl.SelectedTab == posTabPage)
				{
					GlobalStatus.WorldExPreview.SetPos(getSelectedItemText(listView, option));
				}
				else
				{
					MessageBox.Show("この項目は拡張プレビューで表示きません。\n立ち絵・イベント絵・背景絵・舞台時間・トランジション・アクション・位置のアイテムです");
					return;
				}

				GlobalStatus.WorldExPreview.Run();
			}

			if (GlobalStatus.FormManager.WorldExPreviewAttrForm.Visible)
			{
				GlobalStatus.FormManager.WorldExPreviewAttrForm.UpdateObject(GlobalStatus.WorldExPreview.ActiveTarget);
			}
		}

		/// <summary>
		/// 拡張プレビュー→アクティブアイテムのドロップダウンを開こうとしたとき
		/// </summary>
		private void menuItemPreviewExActive_DropDownOpening(object sender, EventArgs e)
		{
			switch (GlobalStatus.WorldExPreview.ActiveTarget)
			{
				case KagexPreview.TargetObject.Char:
					menuItemPreviewExActiveChar.Checked = true;
					menuItemPreviewExActiveEvent.Checked = false;
					menuItemPreviewExActiveStage.Checked = false;
					break;
				case KagexPreview.TargetObject.Event:
					menuItemPreviewExActiveChar.Checked = false;
					menuItemPreviewExActiveEvent.Checked = true;
					menuItemPreviewExActiveStage.Checked = false;
					break;
				case KagexPreview.TargetObject.Stage:
					menuItemPreviewExActiveChar.Checked = false;
					menuItemPreviewExActiveEvent.Checked = false;
					menuItemPreviewExActiveStage.Checked = true;
					break;
			}
		}

		/// <summary>
		/// 立ち絵をアクティブにする
		/// </summary>
		private void menuItemPreviewExActiveChar_Click(object sender, EventArgs e)
		{
			GlobalStatus.WorldExPreview.ActiveTarget = KagexPreview.TargetObject.Char;
			menuItemPreviewExShow_Click(sender, e);
		}

		/// <summary>
		/// イベント絵をアクティブにする
		/// </summary>
		private void menuItemPreviewExActiveEvent_Click(object sender, EventArgs e)
		{
			GlobalStatus.WorldExPreview.ActiveTarget = KagexPreview.TargetObject.Event;
			menuItemPreviewExShow_Click(sender, e);
		}

		/// <summary>
		/// 背景絵をアクティブにする
		/// </summary>
		private void menuItemPreviewExActiveStage_Click(object sender, EventArgs e)
		{
			GlobalStatus.WorldExPreview.ActiveTarget = KagexPreview.TargetObject.Stage;
			menuItemPreviewExShow_Click(sender, e);
		}

		/// <summary>
		/// 最新の情報に更新
		/// </summary>
		private void menuItemUpdate_Click(object sender, EventArgs e)
		{
			InitView();
		}
		#endregion

		#region ツリー・リストイベント
		/// <summary>
		/// ツリービューでマウスボタンを押したときの位置調整を行う
		/// </summary>
		private void charTreeView_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Left)
			{
				charTreeView.SelectedNode = charTreeView.GetNodeAt(e.X, e.Y);
			}
		}

		/// <summary>
		/// リストビューでアイテムをダブルクリックしたとき
		/// </summary>
		private void listView_DoubleClick(object sender, EventArgs e)
		{
			if (mainTabControl.SelectedTab == charTabPage)
			{
				//立ち絵のときはツリーノードから取得する
				TreeNode selectNode = charTreeView.SelectedNode;
				if (selectNode == null)
				{
					return;
				}
				if (selectNode.Nodes.Count > 0)
				{
					return;	//子ノードが存在するときはコピーしない（ツリーを開く動作と紛らわしいため最後のノードのみ選択可とする）
				}
			}
			else
			{
				//現在選択表示しているリストビューを検索する
				ListView listView = getSelectedListView();
				if (listView == null)
				{
					return;	//対応するものが見つからない
				}
			}
			
			EditorOption editorOption = GlobalStatus.EditorManager.GetEditorOption(FileType.KrkrType.Kag);
			if (editorOption == null || editorOption.KagCompOption == null)
			{
				return;
			}

			if ((mainTabControl.SelectedTab == bgmTabPage)
			|| mainTabControl.SelectedTab == seTabPage)
			{
				//BGMとSEは必ずプレビューを実行する（拡張プレビューが行えないため）
				menuItemPreview_Click(sender, e);		//プレビューを実行
				return;
			}

			if (editorOption.KagCompOption.WorldExDoubleDef == KagCompletionOption.WorldExViewDCOption.Preview)
			{
				menuItemPreview_Click(sender, e);		//プレビューを実行
			}
			else if (editorOption.KagCompOption.WorldExDoubleDef == KagCompletionOption.WorldExViewDCOption.PreviewEx)
			{
				menuItemPreviewExShow_Click(sender, e);	//拡張プレビューを実行
			}
		}

		/// <summary>
		/// リストビューのアイテムをドラッグするとき
		/// </summary>
		private void listView_ItemDrag(object sender, ItemDragEventArgs e)
		{
			ListView listView = sender as ListView;
			if (listView == null)
			{
				return;
			}

			ObjectOption option = getSelectedViewOption();
			if (option == null)
			{
				return;
			}

			string text = getSelectedItemText(listView, option);
			if (text == "")
			{
				return;
			}

			//ドラッグ開始
			listView.DoDragDrop(text, DragDropEffects.Copy);
		}

		/// <summary>
		/// 立ち絵ツリーでドラッグを行うとき
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void charTreeView_ItemDrag(object sender, ItemDragEventArgs e)
		{
			ObjectOption option = getSelectedViewOption();
			if (option == null)
			{
				return;
			}

			//立ち絵のときはツリーノードから取得する
			TreeNode selectNode = charTreeView.SelectedNode;
			if (selectNode == null)
			{
				return;
			}

			string text = getSelectedItemText(selectNode, option);
			if (text == "")
			{
				return;
			}

			//ドラッグ開始
			charTreeView.DoDragDrop(text, DragDropEffects.Copy);
		}

		/// <summary>
		/// 現在選択しているタブに対応するリストビューを取得する
		/// </summary>
		/// <returns></returns>
		private ListView getSelectedListView()
		{
			ListView listView = null;
			if (mainTabControl.SelectedTab == charTabPage)
			{
				listView = null;
			}
			else if (mainTabControl.SelectedTab == eventTabPage)
			{
				listView = eventListView;
			}
			else if (mainTabControl.SelectedTab == stageTabPage)
			{
				listView = stageListView;
			}
			else if (mainTabControl.SelectedTab == timeTabPage)
			{
				listView = timeListView;
			}
			else if (mainTabControl.SelectedTab == posTabPage)
			{
				listView = posListView;
			}
			else if (mainTabControl.SelectedTab == transTabPage)
			{
				listView = transListView;
			}
			else if (mainTabControl.SelectedTab == actionTabPage)
			{
				listView = actionListView;
			}
			else if (mainTabControl.SelectedTab == bgmTabPage)
			{
				listView = bgmListView;
			}
			else if (mainTabControl.SelectedTab == seTabPage)
			{
				listView = seListView;
			}

			return listView;
		}

		/// <summary>
		/// 現在選択しているビューに対応するオプションを取得する
		/// </summary>
		/// <returns></returns>
		private ObjectOption getSelectedViewOption()
		{
			ObjectOption option = null;
			bool isAddedTagSign = true;
			bool isAddedMsgTag = true;
			EditorOption editorOption = GlobalStatus.EditorManager.GetEditorOption(FileType.KrkrType.Kag);
			if (editorOption == null || editorOption.KagCompOption == null)
			{
				isAddedTagSign = true;
			}
			else
			{
				isAddedTagSign = editorOption.KagCompOption.IsAddedTagSignWorldEx;
			}
			isAddedMsgTag = toolItemMsgtagState.Checked;

			if (mainTabControl.SelectedTab == charTabPage)
			{
				option = new ObjectOption(isAddedTagSign, isAddedMsgTag);
			}
			else if (mainTabControl.SelectedTab == eventTabPage)
			{
				option = new ObjectOption(isAddedTagSign, isAddedMsgTag);
			}
			else if (mainTabControl.SelectedTab == stageTabPage)
			{
				option = new ObjectOption(isAddedTagSign, isAddedMsgTag);
			}
			else if (mainTabControl.SelectedTab == timeTabPage)
			{
				option = new ObjectOption(false, false);
			}
			else if (mainTabControl.SelectedTab == posTabPage)
			{
				option = new ObjectOption(false, false);
			}
			else if (mainTabControl.SelectedTab == transTabPage)
			{
				option = new ObjectOption(false, false);
			}
			else if (mainTabControl.SelectedTab == actionTabPage)
			{
				option = new ObjectOption(false, false);
			}
			else if (mainTabControl.SelectedTab == bgmTabPage)
			{
				option = new ObjectOption(true, false);
				option.IsBgmTag = true;
			}
			else if (mainTabControl.SelectedTab == seTabPage)
			{
				option = new ObjectOption(true, false);
				option.IsSeTag = true;
			}

			return option;
		}
		#endregion

		#region タグ操作関連メソッド
		/// <summary>
		/// 選択しているアイテムをクリップボードにコピーする
		/// </summary>
		/// <param name="listView">情報を取得するリストビュー</param>
		private void copyTagSelectedItem(ListView listView, ObjectOption option, string addText)
		{
			copyTagSelectedItem(getSelectedItemText(listView, option, addText));
		}

		/// <summary>
		/// 選択しているアイテムをクリップボードにコピーする
		/// </summary>
		/// <param name="listView">情報を取得するリストビュー</param>
		private void copyTagSelectedItem(ListView listView, ObjectOption option)
		{
			copyTagSelectedItem(getSelectedItemText(listView, option));
		}

		/// <summary>
		/// 現在選択しているノードをクリップボードにコピーする
		/// </summary>
		/// <param name="node"></param>
		/// <param name="option"></param>
		private void copyTagSelectedItem(TreeNode node, ObjectOption option, string addText)
		{
			copyTagSelectedItem(getSelectedItemText(node, option, addText));
		}

		/// <summary>
		/// 現在選択しているノードをクリップボードにコピーする
		/// </summary>
		/// <param name="node"></param>
		/// <param name="option"></param>
		private void copyTagSelectedItem(TreeNode node, ObjectOption option)
		{
			copyTagSelectedItem(getSelectedItemText(node, option));
		}

		/// <summary>
		/// 指定された文字列をクリップボードにコピーする
		/// </summary>
		/// <param name="text"></param>
		private void copyTagSelectedItem(string text)
		{
			if (text == "")
			{
				return; //何もしない
			}

			Clipboard.SetText(text);
		}

		/// <summary>
		/// 現在選択しているアイテムを現在のテキストエディタ位置に挿入する
		/// </summary>
		/// <param name="listView">情報を取得するリストビュー</param>
		/// <param name="option">タグオプション</param>
		private void insertTagSelectedItem(ListView listView, ObjectOption option, string addText)
		{
			insertTagSelectedItem(getSelectedItemText(listView, option, addText));
		}

		/// <summary>
		/// 現在選択しているアイテムを現在のテキストエディタ位置に挿入する
		/// </summary>
		/// <param name="listView">情報を取得するリストビュー</param>
		/// <param name="option">タグオプション</param>
		private void insertTagSelectedItem(ListView listView, ObjectOption option)
		{
			insertTagSelectedItem(getSelectedItemText(listView, option));
		}

		/// <summary>
		/// 現在選択しているアイテムを現在のテキストエディタ位置に挿入する
		/// </summary>
		/// <param name="node">選択しているノード</param>/param>
		/// <param name="option">タグオプション</param>
		private void insertTagSelectedItem(TreeNode node, ObjectOption option, string addText)
		{
			insertTagSelectedItem(getSelectedItemText(node, option, addText));
		}

		/// <summary>
		/// 現在選択しているアイテムを現在のテキストエディタ位置に挿入する
		/// </summary>
		/// <param name="node">選択しているノード</param>/param>
		/// <param name="option">タグオプション</param>
		private void insertTagSelectedItem(TreeNode node, ObjectOption option)
		{
			insertTagSelectedItem(getSelectedItemText(node, option));
		}

		/// <summary>
		/// 指定した文字をエディタに挿入する
		/// </summary>
		/// <param name="text">挿入する文字列</param>
		private void insertTagSelectedItem(string text)
		{
			if (text == "")
			{
				return;	//選択項目が取得できなかった
			}

			GlobalStatus.EditorManager.InsertText(text);
		}

		private string getSelectedItemText(ListView listView)
		{
			if (listView.SelectedItems.Count <= 0)
			{
				return "";	//選択項目がない
			}

			return listView.SelectedItems[0].Text;
		}

		/// <summary>
		/// 現在選択しているリストビューのアイテムテキストを返す
		/// </summary>
		/// <param name="listView">情報を取得するリストビュー</param>
		/// <param name="option">タグオプション</param>
		/// <returns>アイテムのテキスト</returns>
		private string getSelectedItemText(ListView listView, ObjectOption option)
		{
			return getSelectedItemText(listView, option, "");
		}

		/// <summary>
		/// 現在選択しているリストビューのアイテムテキストを返す
		/// </summary>
		/// <param name="listView">情報を取得するリストビュー</param>
		/// <param name="option">タグオプション</param>
		/// <returns>アイテムのテキスト</returns>
		private string getSelectedItemText(ListView listView, ObjectOption option, string addText)
		{
			if (listView.SelectedItems.Count <= 0)
			{
				return "";	//選択項目がない
			}

			return getSelectedTagText(listView.SelectedItems[0].Text + addText, option);
		}

		/// <summary>
		/// 指定したノードのキャラクタオブジェクトを取得する
		/// </summary>
		/// <param name="node">キャラクタオブジェクトを取得するノード</param>
		/// <returns>キャラクタオブジェクト</returns>
		private CharObject getCharObjectFromNode(TreeNode node)
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

			return charObject;
		}

		/// <summary>
		/// 現在選択しているノードのタグ文字列を返す
		/// </summary>
		/// <param name="node">ノード</param>
		/// <param name="option">タグオプション</param>
		/// <returns>タグ文字列</returns>
		private string getSelectedItemText(TreeNode node, ObjectOption option)
		{
			return getSelectedItemText(node, option, "");
		}

		/// <summary>
		/// 現在選択しているノードのタグ文字列を返す
		/// </summary>
		/// <param name="node">ノード</param>
		/// <param name="option">タグオプション</param>
		/// <returns>タグ文字列</returns>
		private string getSelectedItemText(TreeNode node, ObjectOption option, string addText)
		{
			CharObject charObject = getCharObjectFromNode(node);
			if (charObject == null || charObject.ToTagString() == "")
			{
				return "";
			}

			return getSelectedTagText(charObject.ToTagString() + addText, option);
		}

		/// <summary>
		/// 指定した文字列にオプションをセットして返す
		/// </summary>
		/// <param name="text">テキスト</param>
		/// <param name="option">セットするオプション情報</param>
		/// <returns>タグ</returns>
		private string getSelectedTagText(string text, ObjectOption option)
		{
			if (text == "")
			{
				return "";
			}
			EditorOption editorOption = GlobalStatus.EditorManager.GetEditorOption(FileType.KrkrType.Kag);
			if (editorOption == null || editorOption.KagCompOption == null)
			{
				return "";	//オプションが取得できない
			}

			if (option.IsBgmTag)		//BGMのときはタグ名をBGMとする
			{
				text = "bgm " + text;
			}
			if (option.IsSeTag)			//SEのときはタグ名をSEとする
			{
				text = "se " + text;
			}

			//タグ記号を追加する
			if (option.IsAddedTagSign)
			{
				if (editorOption.KagCompOption.WorldExCopyTagType == KagCompletionOption.TagType.Atmark)
				{
					text = "@" + text;
				}
				else
				{
					text = "[" + text + "]";
				}
			}
			//msgoff-msgonで囲む
			if (option.IsAddedMsgTag)
			{
				if (editorOption.KagCompOption.WorldExCopyTagType == KagCompletionOption.TagType.Atmark)
				{
					text = "@msgoff\n" + text + "\n@msgon";
				}
				else
				{
					text = "[msgoff]\n" + text + "\n[msgon]";
				}
			}

			return text;
		}
		#endregion

		#region プレビュー関連メソッド
		/// <summary>
		/// データフォルダの指定フォルダ以下からファイルを検索しパスを返す
		/// </summary>
		/// <param name="rootDirName">データフォルダいかにある検索するフォルダ名</param>
		/// <param name="fileName">ファイル名（拡張しなし）</param>
		/// <returns>検索したファイル名 見つからなかったときは空文字を返す</returns>
		private string searchFilePath(string rootDirName, string fileName, string searchExt)
		{
			string[] dirList = rootDirName.Split(';');
			foreach (string dinrName in dirList)
			{
				//フォルダ以下からBGMを検索する
				string searchDirPath = "";
				if (rootDirName == null || rootDirName == "")
				{
					searchDirPath = GlobalStatus.Project.DataFullPath;
				}
				else
				{
					searchDirPath = Path.Combine(GlobalStatus.Project.DataFullPath, dinrName);
				}

				string searchFileName = searchExt.Replace("*", fileName);
				string[] fileList = util.FileUtil.GetDirectoryFile(searchDirPath, searchFileName, SearchOption.TopDirectoryOnly);
				if (fileList == null)
				{
					return "";
				}

				if (fileList.Length > 0)
				{
					return fileList[0];
				}
			}

			return "";
		}

		/// <summary>
		/// データフォルダ指定以下のファイルを検索する
		/// 背景絵専用の舞台時間考慮したファイル検索動作を行う
		/// </summary>
		/// <param name="rootDirName">検索するデータフォルダ以下にあるフォルダ名</param>
		/// <param name="fileName">検索するファイル名（拡張し無し）</param>
		/// <param name="searchExt">検索拡張子（例："*.png;*.jpg"）</param>
		/// <returns>検索したファイル名 見つからなかったときは空文字を返す</returns>
		private string searchStageFilePath(string rootDirName, string fileName, string searchExt)
		{
			if (fileName.IndexOf(SEARCH_STAGE_PREFIX) != -1)	//舞台時間指定が必要なとき
			{
				string[] dirList = rootDirName.Split(';');
				foreach (string dinrName in dirList)
				{
					//まずデフォルト舞台時間で検索を行う
					string deftime = m_cu.GetDefaultTime();
					string stageFileName = searchStageFilePathFormTimePrefix(dinrName, fileName, searchExt, deftime);
					if (stageFileName != "")
					{
						return stageFileName;	//デフォルト時間で見つかったのでそれを返す
					}

					//そのほかの時間で検索してみる
					List<string> timeList = m_cu.GetTimeList();
					foreach (string time in timeList)
					{
						stageFileName = searchStageFilePathFormTimePrefix(dinrName, fileName, searchExt, time);
						if (stageFileName != "")
						{
							return stageFileName;	//見つかった
						}
					}
				}
			}
			else
			{
				//舞台時間が指定されていないときはそのまま検索する
				return searchFilePath(rootDirName, fileName, searchExt);
			}

			return "";	//見つからなかったとき
		}

		/// <summary>
		/// データフォルダ以下からファイルを検索する
		/// 背景絵専用で舞台時間指定を行う必要がある
		/// </summary>
		/// <param name="rootDirName">検索するデータフォルダ以下にあるフォルダ名</param>
		/// <param name="fileName">検索するファイル名（拡張し無し）</param>
		/// <param name="searchExt">検索拡張子（例："*.png;*.jpg"）</param>
		/// <param name="time">舞台時間（例："昼"）</param>
		/// <returns>検索したファイル名 見つからなかったときは空文字を返す</returns>
		private string searchStageFilePathFormTimePrefix(string rootDirName, string fileName, string searchExt, string time)
		{
			string prefix = m_cu.GetTimePrefix(time);
			string stageFileName = fileName.Replace(SEARCH_STAGE_PREFIX, prefix);
			return searchFilePath(rootDirName, stageFileName, searchExt);
		}

		/// <summary>
		/// データフォルダ以下からファイルを検索する
		/// 立ち絵専用
		/// </summary>
		/// <param name="rootDirName">検索するデータフォルダいかにあるフォルダ名</param>
		/// <param name="c">キャラクタオブジェクト</param>
		/// <param name="searchExt">検索拡張し</param>
		/// <returns>検索したファイルパス（見つからなかったときは空文字を返す）</returns>
		private string searchCharFilePath(string rootDirName, CharObject c, string searchExt)
		{
			if (c == null)
			{
				return "";
			}
			if (c.Name == "" || c.Pose == "")
			{
				return "";	//名前と姿勢は必須とする
			}
			string baseName = m_cu.GetCharImageValue(c.Name, c.Pose);
			
			//レベルを置換する
			if (baseName.IndexOf(SEARCH_CHAR_LEVEL_PREFIX) != -1)
			{
				string level = m_cu.GetDefaultLevel();
				baseName = baseName.Replace(SEARCH_CHAR_LEVEL_PREFIX, m_cu.GetLevelName(level));
			}

			//服装を置換する
			if (baseName.IndexOf(SEARCH_CHAR_DRESS_PREFIX) != -1)
			{
				string dress = "";
				if (c.Dress == "")	//服装指定が無いとき
				{
					dress = m_cu.GetCharDefaultDressValue(c.Name, c.Pose);
				}
				else
				{
					dress = c.Dress;
				}
				baseName = baseName.Replace(SEARCH_CHAR_DRESS_PREFIX, dress);
			}

			//表情を置換する
			if (baseName.IndexOf(SEARCH_CHAR_FACE_PREFIX) != -1)
			{
				string face = "";
				if (c.Face == "")	//表情指定が無いとき
				{
					face = m_cu.GetCharDefaultFaceValue(c.Name, c.Pose);
				}
				else
				{
					face = c.Face;
				}
				baseName = baseName.Replace(SEARCH_CHAR_FACE_PREFIX, face);
			}

			return searchFilePath(rootDirName, baseName, searchExt);
		}

		/// <summary>
		/// サウンドファイルを再生する
		/// </summary>
		/// <param name="playPath">再生するファイルパス</param>
		private void previewSound(string playPath)
		{
			if (playPath == "")
			{
				return;	//再生するファイルがない
			}

			GlobalStatus.FormManager.SoundPlayerForm.ShowPlay(playPath, true);
		}

		/// <summary>
		/// 画像ファイルを表示する
		/// </summary>
		/// <param name="imagePath">表示するファイルパス</param>
		private void previewImage(string imagePath)
		{
			if (imagePath == "")
			{
				return;	//表示するファイルが無い
			}

			GlobalStatus.FormManager.ImageViewerForm.ShowImage(imagePath, true);
		}
		#endregion

		#region タブ関連イベント
		private void mainTabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			Debug.WriteLine("chage Tab!");
			menuItemCopyTagEx.DropDown.Items.Clear();	//メニューを初期化
		}
		#endregion

		#region ツールバー関連イベント
		private void toolItemMsgtagState_Click(object sender, EventArgs e)
		{
			//ここでは特に何もしない
		}

		/// <summary>
		/// 検索フィルターから内容を更新する
		/// </summary>
		private void updateSearchFilter()
		{
			m_searchFilter = toolItemSearchComboBox.Text;
			InitView();	//初期化
		}

		/// <summary>
		/// 検索文字を設定し再構築するを行う
		/// </summary>
		private void toolItemSearchButton_Click(object sender, EventArgs e)
		{
			updateSearchFilter();
		}

		/// <summary>
		/// 検索フィルターコンボボックスでエンターキーを押したとき
		/// </summary>
		private void toolItemSearchComboBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Return)
			{
				//エンターキーを押すと検索を実行する
				updateSearchFilter();
			}
		}

		/// <summary>
		/// フィルター対象をセットする
		/// </summary>
		/// <param name="parts"></param>
		private void setSearchTarget(CharParts parts)
		{
			m_selectedCharParts = parts;
			toolItemSearchTargetName.Checked = (parts == CharParts.Name);
			toolItemSearchTargetPose.Checked = (parts == CharParts.Pose);
			toolItemSearchTargetDress.Checked = (parts == CharParts.Dress);
			toolItemSearchTargetFace.Checked = (parts == CharParts.Face);
		}

		/// <summary>
		/// フィルター対象を名前にする
		/// </summary>
		private void toolItemSearchTargetName_Click(object sender, EventArgs e)
		{
			setSearchTarget(CharParts.Name);
		}

		/// <summary>
		/// フィルター対象を姿勢にする
		/// </summary>
		private void toolItemSearchTargetPose_Click(object sender, EventArgs e)
		{
			setSearchTarget(CharParts.Pose);
		}

		/// <summary>
		/// フィルター対象を服装にする
		/// </summary>
		private void toolItemSearchTargetDress_Click(object sender, EventArgs e)
		{
			setSearchTarget(CharParts.Dress);
		}

		/// <summary>
		/// フィルター対象を表情にする
		/// </summary>
		private void toolItemSearchTargetFace_Click(object sender, EventArgs e)
		{
			setSearchTarget(CharParts.Face);
		}

		/// <summary>
		/// 属性エディタを起動
		/// </summary>
		private void toolItemPreviewAttr_Click(object sender, EventArgs e)
		{
			GlobalStatus.FormManager.WorldExPReviewAttrFromShow();
		}
		#endregion
	}
}
