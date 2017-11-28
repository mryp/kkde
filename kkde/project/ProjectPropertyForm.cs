using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using kkde.global;

namespace kkde.project
{
	/// <summary>
	/// プロジェクトのプロパティを表示する
	/// </summary>
	public partial class ProjectPropertyForm : Form
	{
		#region 定数
		private const int KIND_INDEX_EMPTY = 0;
		private const int KIND_INDEX_KAG3 = 1;
		private const int KIND_INDEX_KAGEXPP = 2;
		#endregion

		#region フィールド
		private bool m_isUpdate = false;
		private ProjectFile m_project = null;
		#endregion

		#region プロパティ
		/// <summary>
		/// プロジェクトファイルの更新があったかどうかを取得する（読み取り専用）
		/// 更新があったときはtrueを返す
		/// </summary>
		public bool IsUpdate
		{
			get
			{
				return m_isUpdate;
			}
		}

		/// <summary>
		/// プロパティをセットしたプロジェクト
		/// </summary>
		public ProjectFile Project
		{
			get { return m_project; }
		}
		#endregion

		#region メソッド
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ProjectPropertyForm(ProjectFile project)
		{
			InitializeComponent();

			m_project = project;
			init();
		}

		/// <summary>
		/// プロジェクトを読み込み画面表示を初期化する
		/// </summary>
		private void init()
		{
			if (m_project == null)
			{
				//プロジェクト新規作成時
				filePathBox.ReadOnly = false;		//変更できるようにする
				filePathRefButton.Enabled = true;	//有効にする
				saveButton.Text = "作成";
				this.Text = "プロジェクトインポート";

				filePathBox.Text = "";		
				nameBox.Text = "";
				dataPathBox.Text = "";
				exePathBox.Text = "";
				exeArgvBox.Text = "";
				kindComboBox.SelectedIndex = KIND_INDEX_KAG3;
			}
			else
			{
				filePathBox.ReadOnly = true;		//ファイルが存在するときは変更不可
				filePathRefButton.Enabled = false;	//無効にする
				saveButton.Text = "保存";
				this.Text = "プロパティ";

				//既存プロジェクト変更時
				if (m_project.IsOpened == false)
				{
					//プロジェクトが無効なので保存できないようにする
					saveButton.Enabled = false;
					return;
				}
				
				filePathBox.Text = m_project.FilePath;
				nameBox.Text = m_project.Name;
				dataPathBox.Text = m_project.DataFullPath;
				exePathBox.Text = m_project.ExeFullPath;
				exeArgvBox.Text = m_project.ExeArgv;
				switch (m_project.Type)
				{
					case ProjectType.Empty:
						kindComboBox.SelectedIndex = KIND_INDEX_EMPTY;
						break;
					case ProjectType.Kag3:
						kindComboBox.SelectedIndex = KIND_INDEX_KAG3;
						break;
					case ProjectType.Kagexpp:
						kindComboBox.SelectedIndex = KIND_INDEX_KAGEXPP;
						break;
					default:
						break;
				}
			}
		}

		/// <summary>
		/// 変更を保存してダイアログを閉じる
		/// </summary>
		private void saveButton_Click(object sender, EventArgs e)
		{
			//初期化
			m_isUpdate = false;

			//エラーチェック
			if (filePathBox.Text == "")
			{
				util.Msgbox.Error("ファイルパスが入力されていません");
				return;
			}
			if (nameBox.Text == "")
			{
				util.Msgbox.Error("プロジェクト名が入力されていません");
				return;
			}
			if (dataPathBox.Text == "")
			{
				util.Msgbox.Error("データフォルダパスが入力されていません");
				return;
			}
			if (exePathBox.Text == "")
			{
				util.Msgbox.Error("実行ファイルパスが入力されていません");
				return;
			}

			if (Directory.Exists(dataPathBox.Text) == false)
			{
				util.Msgbox.Error("データフォルダパスに指定されたフォルダが見つかりません");
				return;
			}
			if (File.Exists(exePathBox.Text) == false)
			{
				util.Msgbox.Error("実行ファイルパスに指定されたファイルが見つかりません");
				return;
			}

			//保存して閉じる
			m_isUpdate = saveProject(nameBox.Text, dataPathBox.Text, exePathBox.Text
				, exeArgvBox.Text, kindComboBox.SelectedIndex, filePathBox.Text);
			this.Close();
		}

		/// <summary>
		/// プロジェクトを保存する
		/// 以前と変更が全くなかったときは保存を行わない
		/// </summary>
		/// <param name="name">プロジェクト名</param>
		/// <param name="dataPath">データフォルダパス</param>
		/// <param name="exePath">実行ファイルパス</param>
		/// <param name="kindIndex">プロジェクトの種類選択インデックス値</param>
		/// <returns>保存を行ったときはtrueを返す</returns>
		private bool saveProject(string name, string dataPath, string exePath, string exeArgv, int kindIndex, string filePath)
		{
			bool isUpdate = false;

			if (m_project == null)
			{
				//新規プロジェクト作成時
				isUpdate = true;	//必ず作成する

				m_project = new ProjectFile(filePath);
				m_project.Name = name;
				m_project.DataFullPath = dataPath;
				m_project.ExeFullPath = exePath;
				m_project.ExeArgv = exeArgv;
				m_project.Type = convertKindIndexToProjectType(kindIndex);
				m_project.Save(filePath);
			}
			else
			{
				//既存プロジェクト保存時
				if (m_project.Name != name)
				{
					isUpdate = true;
					m_project.Name = name;
				}
				if (m_project.DataFullPath != dataPath)
				{
					isUpdate = true;
					m_project.DataFullPath = dataPath;
				}
				if (m_project.ExeFullPath != exePath)
				{
					isUpdate = true;
					m_project.ExeFullPath = exePath;
				}
				if (m_project.ExeArgv != exeArgv)
				{
					isUpdate = true;
					m_project.ExeArgv = exeArgv;
				}
				ProjectType type = convertKindIndexToProjectType(kindIndex);
				if (m_project.Type != type)
				{
					isUpdate = true;
					m_project.Type = type;
				}

				if (isUpdate)
				{
					//更新があったときだけ保存する
					m_project.Save(m_project.FilePath);
				}
			}

			return isUpdate;
		}

		/// <summary>
		/// プロジェクトの種類コンボボックスの選択インデックスからプロジェクトタイプを返す
		/// </summary>
		/// <param name="kindIndex">選択インデックス値</param>
		/// <returns>プロジェクトの種類</returns>
		private ProjectType convertKindIndexToProjectType(int kindIndex)
		{
			ProjectType type;

			switch (kindIndex)
			{
				case KIND_INDEX_EMPTY:
					type = ProjectType.Empty;
					break;
				case KIND_INDEX_KAG3:
					type = ProjectType.Kag3;
					break;
				case KIND_INDEX_KAGEXPP:
					type = ProjectType.Kagexpp;
					break;
				default:
					type = ProjectType.Empty;
					break;
			}

			return type;
		}

		/// <summary>
		/// 変更を保存せずにダイアログを閉じる
		/// </summary>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			m_isUpdate = false;	//更新なし
			this.Close();
		}

		/// <summary>
		/// データフォルダパスを取得しセットする
		/// </summary>
		private void dataPathRefButton_Click(object sender, EventArgs e)
		{
			if (dataFolderDialog.SelectedPath == "")
			{
				if (dataPathBox.Text == "" && filePathBox.Text != "")
				{
					dataFolderDialog.SelectedPath = Path.GetDirectoryName(filePathBox.Text);
				}
				else 
				{
					dataFolderDialog.SelectedPath = dataPathBox.Text;
				}
			}

			if (dataFolderDialog.ShowDialog() == DialogResult.OK)
			{
				dataPathBox.Text = dataFolderDialog.SelectedPath;
			}
		}

		/// <summary>
		/// 実行ファイルパスを取得しセットする
		/// </summary>
		private void exePathRefButton_Click(object sender, EventArgs e)
		{
			if (exeFileOpenDialog.InitialDirectory == "")
			{
				if (exePathBox.Text == "" && filePathBox.Text != "")
				{
					exeFileOpenDialog.InitialDirectory = Path.GetDirectoryName(filePathBox.Text);
				}
				else if (exePathBox.Text != "")
				{
					exeFileOpenDialog.InitialDirectory = Path.GetDirectoryName(exePathBox.Text);
				}
			}

			if (exeFileOpenDialog.ShowDialog() == DialogResult.OK)
			{
				exePathBox.Text = exeFileOpenDialog.FileName;
			}
		}

		/// <summary>
		/// プロジェクトファイルパスを取得しセットする
		/// </summary>
		private void filePathRefButton_Click(object sender, EventArgs e)
		{
			projectFileSaveDialog.DefaultExt = ProjectFile.PROJECT_EXT_NAME;
			projectFileSaveDialog.Filter = String.Format("プロジェクトファイル (*.{0})|*.{0}", ProjectFile.PROJECT_EXT_NAME);
			if (projectFileSaveDialog.InitialDirectory == "")
			{
				if (filePathBox.Text != "")
				{
					projectFileSaveDialog.InitialDirectory = Path.GetDirectoryName(filePathBox.Text);
				}
			}

			if (projectFileSaveDialog.ShowDialog() == DialogResult.OK)
			{
				filePathBox.Text = projectFileSaveDialog.FileName;
			}
		}
		#endregion
	}
}
