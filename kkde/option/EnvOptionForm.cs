using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using kkde.global;

namespace kkde.option
{
	/// <summary>
	/// 環境設定ダイアログ
	/// </summary>
	public partial class EnvOptionForm : Form
	{
		public EnvOptionForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 設定内容を読み込む
		/// </summary>
		private void EnvOptionForm_Load(object sender, EventArgs e)
		{
			//全般
			historyFileMaxNumBox.Value = GlobalStatus.EnvOption.HistoryFile.MaxCount;
			historyProjectMaxNumBox.Value = GlobalStatus.EnvOption.HistoryProject.MaxCount;

			//プロジェクト
			projectOpenedLastProjectCheckBox.Checked = GlobalStatus.EnvOption.ProjectOpenedLastProject;
			projectOpenedLastFileCheckBox.Checked = GlobalStatus.EnvOption.ProjectOpenedLastFile;
			projectWatchProjectPathCheckBox.Checked = GlobalStatus.EnvOption.ProjectWatchProjectPath;

			//実行
			execOpenedKrkrKillCheckBox.Checked = GlobalStatus.EnvOption.ExecKrkrKill;
			execOpenedFileSaveAllCheckBox.Checked = GlobalStatus.EnvOption.ExecSaveAll;
			execOpenLogCheckBox.Checked = GlobalStatus.EnvOption.ExecOpenLog;
			execAddClearOption.Checked = GlobalStatus.EnvOption.ExecAddClearOption;

			//システム
			systemCompleteSleepTimeNumBox.Value = GlobalStatus.EnvOption.CodeComplateSeepTime;

			//ヘルプ
			switch (GlobalStatus.EnvOption.HelpHelpWindow)
			{
				case EnvOption.HelpWindowState.DockingWindow:
					helpViewerDockingRadioButton.Checked = true;
					break;
				case EnvOption.HelpWindowState.DialogWindow:
					helpViewerDialogRadioButton.Checked = true;
					break;
			}
		}

		/// <summary>
		/// 設定を保存して閉じる
		/// </summary>
		private void okButton_Click(object sender, EventArgs e)
		{
			//全般
			GlobalStatus.EnvOption.HistoryFile.MaxCount = (int)historyFileMaxNumBox.Value;
			GlobalStatus.EnvOption.HistoryProject.MaxCount = (int)historyProjectMaxNumBox.Value;

			//プロジェクト
			GlobalStatus.EnvOption.ProjectOpenedLastProject = projectOpenedLastProjectCheckBox.Checked;
			GlobalStatus.EnvOption.ProjectOpenedLastFile = projectOpenedLastFileCheckBox.Checked;
			GlobalStatus.EnvOption.ProjectWatchProjectPath = projectWatchProjectPathCheckBox.Checked;

			//実行
			GlobalStatus.EnvOption.ExecKrkrKill = execOpenedKrkrKillCheckBox.Checked;
			GlobalStatus.EnvOption.ExecSaveAll = execOpenedFileSaveAllCheckBox.Checked;
			GlobalStatus.EnvOption.ExecOpenLog = execOpenLogCheckBox.Checked;
			GlobalStatus.EnvOption.ExecAddClearOption = execAddClearOption.Checked;

			//システム
			GlobalStatus.EnvOption.CodeComplateSeepTime = (int)systemCompleteSleepTimeNumBox.Value;

			//ヘルプ
			if (helpViewerDockingRadioButton.Checked)
			{
				GlobalStatus.EnvOption.HelpHelpWindow = EnvOption.HelpWindowState.DockingWindow;
			}
			else if (helpViewerDialogRadioButton.Checked)
			{
				GlobalStatus.EnvOption.HelpHelpWindow = EnvOption.HelpWindowState.DialogWindow;
			}

			//反映
			GlobalStatus.FormManager.ProjectForm.ChangeOption();

			this.Close();
		}

		/// <summary>
		/// 設定を保存せずに閉じる
		/// </summary>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// ファイル履歴をリセットする
		/// </summary>
		private void historyFileResetButton_Click(object sender, EventArgs e)
		{
			GlobalStatus.EnvOption.HistoryFile.List.Clear();
			util.Msgbox.Info("ファイル履歴をリセットしました");
		}

		/// <summary>
		/// プロジェクト履歴をリセットする
		/// </summary>
		private void historyResetProjectReset_Click(object sender, EventArgs e)
		{
			GlobalStatus.EnvOption.HistoryProject.List.Clear();
			util.Msgbox.Info("プロジェクト履歴をリセットしました");
		}
	}
}
