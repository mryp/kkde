using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using kkde.project;
using kkde.search;
using kkde.debug;
using kkde.kag.image;
using kkde.kag.sound;

namespace kkde.global
{
	/// <summary>
	/// ウィンドウフォーム管理クラス
	/// </summary>
	public class FormManager
	{
		/// <summary>
		/// メインフォーム
		/// </summary>
		MainForm m_mainForm = null;

		/// <summary>
		/// プロジェクトフォーム
		/// </summary>
		ProjectForm m_projectForm = null;

		/// <summary>
		/// 検索結果フォーム
		/// </summary>
		SearchResultForm m_searchResultForm = null;

		/// <summary>
		/// 実行ログフォーム
		/// </summary>
		ExecLogForm m_execLogForm = null;

		/// <summary>
		/// ブックマークリストフォーム
		/// </summary>
		BookmarkListForm m_bookmarkListForm = null;

		/// <summary>
		/// KAGラベルツリーフォーム
		/// </summary>
		kag.label.KagLabelForm m_kagLabelForm = null;

		/// <summary>
		/// イメージビューワーフォーム
		/// </summary>
		ImageViewerForm m_imageViewerForm = null;

		/// <summary>
		/// サウンドプレイヤーフォーム
		/// </summary>
		SoundPlayerForm m_soundPlayerForm = null;

		/// <summary>
		/// ワールド拡張ビューワー
		/// </summary>
		kagex.WorldExViewForm m_worldExViewForm = null;

		/// <summary>
		/// アクションエディタ
		/// </summary>
//		kagex.WorldExActionForm_ m_worldExActionForm = null;
		kagex.WorldExActionEditorForm m_worldExActionEditorForm = null;

		/// <summary>
		/// 立ち絵選択ダイアログ
		/// </summary>
		kkde.kagex.WorldExCharSelectDialog m_worldExCharSelectDialog = null;

		/// <summary>
		/// イベント絵・背景絵選択ダイアログ
		/// </summary>
		kkde.kagex.WorldExObjectSelectDialog m_worldExObjectSelectDialog = null;

		/// <summary>
		/// ワールド拡張属性エディタ
		/// </summary>
		kkde.kagex.WorldExPreviewAttr m_worldExPreviewAttrForm = new kkde.kagex.WorldExPreviewAttr();

		/// <summary>
		/// ヘルプリファレンスドッキングフォーム
		/// </summary>
		kkde.help.HelpReferenceForm m_helpRefForm = new kkde.help.HelpReferenceForm();

		/// <summary>
		/// ヘルプリファレンスダイアログ
		/// </summary>
		kkde.help.HelpReferenceDialog m_helpRefDialog = new kkde.help.HelpReferenceDialog();

		/// <summary>
		/// スクリーンエディタ
		/// </summary>
		kkde.screen.ScreenMakerForm m_screenMakerForm = null;

		/// <summary>
		/// スクリーンプロパティ
		/// </summary>
		kkde.screen.ScreenPropertyForm m_screenPropertyForm = null;

		/// <summary>
		/// スクリーンツールボックス
		/// </summary>
		kkde.screen.ScreenToolForm m_screenToolForm = null;

		/// <summary>
		/// メインフォーム
		/// </summary>
		public MainForm MainForm
		{
			get { return m_mainForm; }
			set { m_mainForm = value; }
		}

		/// <summary>
		/// プロジェクトフォームアクセス用プロパティ
		/// </summary>
		public ProjectForm ProjectForm
		{
			get
			{
				if (m_projectForm == null)
				{
					m_projectForm = new ProjectForm();
				}

				return m_projectForm;
			}
		}

		/// <summary>
		/// 検索結果フォームアクセス用プロパティ
		/// </summary>
		public SearchResultForm SearchResultForm
		{
			get
			{
				if (m_searchResultForm == null)
				{
					m_searchResultForm = new SearchResultForm();
				}
				return m_searchResultForm;
			}
		}

		/// <summary>
		/// 実行ログウィンドウアクセス用フォーム
		/// </summary>
		public ExecLogForm ExecLogForm
		{
			get
			{
				if (m_execLogForm == null)
				{
					m_execLogForm = new ExecLogForm();
				}
				return m_execLogForm;
			}
		}

		/// <summary>
		/// ブックマークウィンドウアクセス用フォーム
		/// </summary>
		public BookmarkListForm BookmarkListForm
		{
			get
			{
				if (m_bookmarkListForm == null)
				{
					m_bookmarkListForm = new BookmarkListForm();
				}
				return m_bookmarkListForm;
			}
		}

		/// <summary>
		/// KAGラベルツリーアクセス用フォーム
		/// </summary>
		public kag.label.KagLabelForm KagLabelForm
		{
			get 
			{
				if (m_kagLabelForm == null)
				{
					m_kagLabelForm = new kkde.kag.label.KagLabelForm();
				}

				return m_kagLabelForm; 
			}
		}

		/// <summary>
		/// KAGラベルツリーを隠しているかどうか
		/// 表示しているとき false
		/// 表示していないとき true
		/// </summary>
		public bool IsHiddenKagLabelForm
		{
			get
			{
				if (m_kagLabelForm == null)
				{
					return true;
				}
				if (m_kagLabelForm.IsHidden)
				{
					return true;
				}

				return false;
			}
		}

		/// <summary>
		/// イメージビューワーアクセス用フォーム
		/// </summary>
		public ImageViewerForm ImageViewerForm
		{
			get 
			{
				if (m_imageViewerForm == null)
				{
					m_imageViewerForm = new ImageViewerForm();
				}
				return m_imageViewerForm; 
			}
		}

		/// <summary>
		/// サウンドプレイヤーアクセス用
		/// </summary>
		public SoundPlayerForm SoundPlayerForm
		{
			get 
			{
				if (m_soundPlayerForm == null)
				{
					m_soundPlayerForm = new SoundPlayerForm();
				}
				return m_soundPlayerForm; 
			}
		}

		/// <summary>
		/// ワールド拡張ビュワーアクセス用
		/// </summary>
		public kagex.WorldExViewForm WorldExViewForm
		{
			get 
			{
				if (m_worldExViewForm == null)
				{
					m_worldExViewForm = new kkde.kagex.WorldExViewForm();
				}
				return m_worldExViewForm; 
			}
		}

		/// <summary>
		/// ワールド拡張ビューワーを隠しているかどうか
		/// 表示しているとき false
		/// 表示していないとき true
		/// </summary>
		public bool IsHiddenWorldExViewForm
		{
			get
			{
				if (m_worldExViewForm == null)
				{
					return true;
				}
				if (m_worldExViewForm.IsHidden)
				{
					return true;
				}

				return false;
			}
		}

		/// <summary>
		/// ワールド拡張アクションエディタアクセス用
		/// </summary>
		public kagex.WorldExActionEditorForm WorldExActionEditorForm
		{
			get
			{
				if (m_worldExActionEditorForm == null)
				{
					m_worldExActionEditorForm = new kkde.kagex.WorldExActionEditorForm();
				}
				return m_worldExActionEditorForm; 
			}
		}

		/// <summary>
		/// ワールド拡張アクションエディタを表示する
		/// </summary>
		public void WorldExActionEditorFormShow()
		{
			if (WorldExActionEditorForm.Visible == false)	//表示していないときは表示する
			{
				WorldExActionEditorForm.Show();
			}
			else
			{
				WorldExActionEditorForm.Activate();
			}
		}

		/// <summary>
		/// 立ち絵選択ダイアログアクセス用
		/// </summary>
		public kkde.kagex.WorldExCharSelectDialog WorldExCharSelectDialog
		{
			get
			{
				if (m_worldExCharSelectDialog == null)
				{
					m_worldExCharSelectDialog = new kkde.kagex.WorldExCharSelectDialog();
				}
				return m_worldExCharSelectDialog; 
			}
		}

		/// <summary>
		/// イベント絵・背景選択ダイアログアクセス用
		/// </summary>
		public kkde.kagex.WorldExObjectSelectDialog WorldExObjectSelectDialog
		{
			get 
			{
				if (m_worldExObjectSelectDialog == null)
				{
					m_worldExObjectSelectDialog = new kkde.kagex.WorldExObjectSelectDialog();
				}
				return m_worldExObjectSelectDialog; 
			}
		}

		/// <summary>
		/// ワールド拡張プレビューエディタフォームオブジェクトアクセス用
		/// </summary>
		public kkde.kagex.WorldExPreviewAttr WorldExPreviewAttrForm
		{
			get
			{
				if (m_worldExPreviewAttrForm == null)
				{
					m_worldExPreviewAttrForm = new kkde.kagex.WorldExPreviewAttr();
				}
				return m_worldExPreviewAttrForm; 
			}
		}

		/// <summary>
		/// ワールド拡張プレビューエディタを表示する
		/// </summary>
		public void WorldExPReviewAttrFromShow()
		{
			if (WorldExPreviewAttrForm.Visible == false)	//表示していないときは表示する
			{
				WorldExPreviewAttrForm.Show();
			}
			else
			{
				WorldExPreviewAttrForm.Activate();
			}
		}

		/// <summary>
		/// ヘルプリファレンスドッキングウィンドウアクセス用
		/// </summary>
		public kkde.help.HelpReferenceForm HelpReferenceForm
		{
			get
			{
				if (m_helpRefForm == null)
				{
					m_helpRefForm = new kkde.help.HelpReferenceForm();
				}
				return m_helpRefForm; 
			}
		}

		/// <summary>
		/// ヘルプリファレンスダイアログアクセス用
		/// </summary>
		public kkde.help.HelpReferenceDialog HelpRefDialog
		{
			get
			{
				if (m_helpRefDialog == null)
				{
					m_helpRefDialog = new kkde.help.HelpReferenceDialog();
				}
				return m_helpRefDialog; 
			}
		}

		/// <summary>
		/// ヘルプリファレンスダイアログを表示する
		/// </summary>
		public void HelpRefDialogShow()
		{
			if (HelpRefDialog.Visible == false)
			{
				HelpRefDialog.Show();
			}
			else
			{
				HelpRefDialog.Activate();
			}
		}

		/// <summary>
		/// スクリーンエディタ
		/// </summary>
		public kkde.screen.ScreenMakerForm ScreenEditor
		{
			get
			{
				if (m_screenMakerForm == null)
				{
					m_screenMakerForm = new kkde.screen.ScreenMakerForm();
				}
				return m_screenMakerForm; 
			}
		}

		/// <summary>
		/// スクリーンプロパティ
		/// </summary>
		public kkde.screen.ScreenPropertyForm ScreenProperty
		{
			get
			{
				if (m_screenPropertyForm == null)
				{
					m_screenPropertyForm = new kkde.screen.ScreenPropertyForm();
				}
				return m_screenPropertyForm;
			}
		}

		/// <summary>
		/// スクリーンツールボックス
		/// </summary>
		public kkde.screen.ScreenToolForm ScreenToolBox
		{
			get
			{
				if (m_screenToolForm == null)
				{
					m_screenToolForm = new kkde.screen.ScreenToolForm();
				}
				return m_screenToolForm;
			}
		}


		/// <summary>
		/// コンストラクタ
		/// </summary>
		public FormManager()
		{
		}
	}
}
