using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;

using kkde.global;
using kkde.option;

namespace kkde.project
{
	/// <summary>
	/// 新規作成ダイアログ
	/// </summary>
	public partial class NewCreateForm : Form
	{
		/// <summary>
		/// 作成したプロジェクトを格納する変数
		/// </summary>
		private ProjectFile m_projectFile = null;

		/// <summary>
		/// 作成したプロジェクトのデータ（読み取り専用）
		/// </summary>
		public ProjectFile CreateProject
		{
			get
			{
				return m_projectFile;
			}
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public NewCreateForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 作成ボタンが押されたとき
		/// </summary>
		private void okButton_Click(object sender, EventArgs e)
		{
			//エラーチェック
			if (projectNameTextBox.Text == "")
			{
				kkde.util.Msgbox.Error("プロジェクト名が入力されていません");
				return;
			}
			if (Directory.Exists(saveDirectoryTextBox.Text) == false)
			{
				kkde.util.Msgbox.Error("指定された保存場所のパスが正しくありません");
				return;
			}
			if (kkde.util.FileUtil.IsValidFileName(projectNameTextBox.Text) == false)
			{
				kkde.util.Msgbox.Error("指定されたプロジェクト名はファイル名として使えない文字が含まれています\nプロジェクト名にはファイル名として使用できない文字を含めないでください");
				return;
			}

			//プロジェクトを作成する
			ProjectType projType;
			if (projectEmptyRadioButton.Checked)
			{
				projType = ProjectType.Empty;
			}
			else if (projectKag3RadioButton.Checked)
			{
				projType = ProjectType.Kag3;
			}
			else if (projectKagexppRadioButton.Checked)
			{
				projType = ProjectType.Kagexpp;
			}
			else
			{
				kkde.util.Msgbox.Error("指定されたプロジェクトは存在しません");
				return;
			}
			ProjectFile projectFile = new ProjectFile();
			projectFile.SetData(projectNameTextBox.Text, projType, Path.Combine(saveDirectoryTextBox.Text, projectNameTextBox.Text)); 
			if (Directory.Exists(projectFile.DataFullPath) == true)
			{
				kkde.util.Msgbox.Error("指定されたプロジェクトはすでに同名のものが存在します\n別のプロジェクト名を指定するか、保存場所を変更してください");
				return;
			}
			
			//プロジェクトを作成する
			if (createProject(projectFile) == false)
			{
				//プロジェクトの作成に失敗したとき
				return;
			}

			//正常終了処理を行う
			m_projectFile = projectFile;
			this.Close();
			return;
		}

		/// <summary>
		/// プロジェクトを作成する
		/// </summary>
		/// <param name="projectFile">作成するプロジェクトの情報</param>
		/// <returns>プロジェクトの作成が成功したらtrueを返す</returns>
		private bool createProject(ProjectFile projectFile)
		{
			string savedir = saveDirectoryTextBox.Text;
			string copyProjectPath = "";

			//プロジェクトタイプからコピーするべきテンプレートフォルダを決定する
			switch (projectFile.Type)
			{
				case ProjectType.Empty:
					copyProjectPath = Path.Combine(ConstEnvOption.TemplateProjectPath, "empty");
					break;
				case ProjectType.Kag3:
					copyProjectPath = Path.Combine(ConstEnvOption.TemplateProjectPath, "kag3");
					break;
				case ProjectType.Kagexpp:
					copyProjectPath = Path.Combine(ConstEnvOption.TemplateProjectPath, "kagexpp");
					break;
			}

			//プロジェクトディレクトリのコピーとプロジェクトファイルの作成を行う
			try
			{
				kkde.util.FileUtil.CopyDirectory(copyProjectPath, projectFile.DirPath);
				projectFile.Save(projectFile.FilePath);
			}
			catch (IOException ioerr)
			{
				kkde.util.Msgbox.Error(String.Format("{0}\n{1}", "プロジェクトファイルの作成が出来ませんでした", ioerr.Message));
				return false;
			}
			catch (Exception err)
			{
				kkde.util.Msgbox.Error(String.Format("{0}\n{1}", "プロジェクトの作成に失敗しました", err.Message));
				return false;
			}

			return true;
		}

		/// <summary>
		/// キャンセルボタンが押されたとき
		/// </summary>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			//何もせずに終了
			this.Close();
		}

		/// <summary>
		/// 保存場所パスを選択するダイアログを表示する
		/// </summary>
		private void refSavePathButton_Click(object sender, EventArgs e)
		{
			if (saveFolderDialog.SelectedPath == "")
			{
				saveFolderDialog.SelectedPath = ConstEnvOption.ApplicationDirectoryPath;
			}

			if (saveFolderDialog.ShowDialog() == DialogResult.OK)
			{
				saveDirectoryTextBox.Text = saveFolderDialog.SelectedPath;
			}
		}
	}
}
