namespace kkde.option
{
	partial class EnvOptionForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.envTabControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.historyResetProjectReset = new System.Windows.Forms.Button();
			this.historyFileResetButton = new System.Windows.Forms.Button();
			this.historyProjectMaxNumBox = new System.Windows.Forms.NumericUpDown();
			this.historyFileMaxNumBox = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.execAddClearOption = new System.Windows.Forms.CheckBox();
			this.execOpenLogCheckBox = new System.Windows.Forms.CheckBox();
			this.execOpenedFileSaveAllCheckBox = new System.Windows.Forms.CheckBox();
			this.execOpenedKrkrKillCheckBox = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.projectWatchProjectPathCheckBox = new System.Windows.Forms.CheckBox();
			this.projectOpenedLastFileCheckBox = new System.Windows.Forms.CheckBox();
			this.projectOpenedLastProjectCheckBox = new System.Windows.Forms.CheckBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.systemCompleteSleepTimeNumBox = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.helpViewerDialogRadioButton = new System.Windows.Forms.RadioButton();
			this.helpViewerDockingRadioButton = new System.Windows.Forms.RadioButton();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.envTabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.historyProjectMaxNumBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.historyFileMaxNumBox)).BeginInit();
			this.tabPage2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.systemCompleteSleepTimeNumBox)).BeginInit();
			this.tabPage4.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.SuspendLayout();
			// 
			// envTabControl
			// 
			this.envTabControl.Controls.Add(this.tabPage1);
			this.envTabControl.Controls.Add(this.tabPage2);
			this.envTabControl.Controls.Add(this.tabPage3);
			this.envTabControl.Controls.Add(this.tabPage4);
			this.envTabControl.Dock = System.Windows.Forms.DockStyle.Top;
			this.envTabControl.Location = new System.Drawing.Point(0, 0);
			this.envTabControl.Name = "envTabControl";
			this.envTabControl.SelectedIndex = 0;
			this.envTabControl.Size = new System.Drawing.Size(479, 256);
			this.envTabControl.TabIndex = 2;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.groupBox2);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(471, 230);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "全般";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.historyResetProjectReset);
			this.groupBox2.Controls.Add(this.historyFileResetButton);
			this.groupBox2.Controls.Add(this.historyProjectMaxNumBox);
			this.groupBox2.Controls.Add(this.historyFileMaxNumBox);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Location = new System.Drawing.Point(8, 8);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(448, 112);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "最近使ったファイル";
			// 
			// historyResetProjectReset
			// 
			this.historyResetProjectReset.Location = new System.Drawing.Point(152, 72);
			this.historyResetProjectReset.Name = "historyResetProjectReset";
			this.historyResetProjectReset.Size = new System.Drawing.Size(128, 23);
			this.historyResetProjectReset.TabIndex = 3;
			this.historyResetProjectReset.Text = "プロジェクト履歴リセット";
			this.historyResetProjectReset.UseVisualStyleBackColor = true;
			this.historyResetProjectReset.Click += new System.EventHandler(this.historyResetProjectReset_Click);
			// 
			// historyFileResetButton
			// 
			this.historyFileResetButton.Location = new System.Drawing.Point(16, 72);
			this.historyFileResetButton.Name = "historyFileResetButton";
			this.historyFileResetButton.Size = new System.Drawing.Size(128, 23);
			this.historyFileResetButton.TabIndex = 2;
			this.historyFileResetButton.Text = "ファイル履歴リセット";
			this.historyFileResetButton.UseVisualStyleBackColor = true;
			this.historyFileResetButton.Click += new System.EventHandler(this.historyFileResetButton_Click);
			// 
			// historyProjectMaxNumBox
			// 
			this.historyProjectMaxNumBox.Location = new System.Drawing.Point(144, 44);
			this.historyProjectMaxNumBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.historyProjectMaxNumBox.Name = "historyProjectMaxNumBox";
			this.historyProjectMaxNumBox.Size = new System.Drawing.Size(80, 19);
			this.historyProjectMaxNumBox.TabIndex = 1;
			this.historyProjectMaxNumBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// historyFileMaxNumBox
			// 
			this.historyFileMaxNumBox.Location = new System.Drawing.Point(144, 20);
			this.historyFileMaxNumBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.historyFileMaxNumBox.Name = "historyFileMaxNumBox";
			this.historyFileMaxNumBox.Size = new System.Drawing.Size(80, 19);
			this.historyFileMaxNumBox.TabIndex = 0;
			this.historyFileMaxNumBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(122, 12);
			this.label2.TabIndex = 5;
			this.label2.Text = "プロジェクト履歴最大数：";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(105, 12);
			this.label1.TabIndex = 4;
			this.label1.Text = "ファイル履歴最大数：";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.groupBox3);
			this.tabPage2.Controls.Add(this.groupBox1);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(471, 230);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "プロジェクト";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.execAddClearOption);
			this.groupBox3.Controls.Add(this.execOpenLogCheckBox);
			this.groupBox3.Controls.Add(this.execOpenedFileSaveAllCheckBox);
			this.groupBox3.Controls.Add(this.execOpenedKrkrKillCheckBox);
			this.groupBox3.Location = new System.Drawing.Point(8, 112);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(448, 112);
			this.groupBox3.TabIndex = 1;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "実行";
			// 
			// execAddClearOption
			// 
			this.execAddClearOption.AutoSize = true;
			this.execAddClearOption.Location = new System.Drawing.Point(16, 84);
			this.execAddClearOption.Name = "execAddClearOption";
			this.execAddClearOption.Size = new System.Drawing.Size(240, 16);
			this.execAddClearOption.TabIndex = 3;
			this.execAddClearOption.Text = "実行時に-logerror=clearオプションを付加する";
			this.execAddClearOption.UseVisualStyleBackColor = true;
			// 
			// execOpenLogCheckBox
			// 
			this.execOpenLogCheckBox.AutoSize = true;
			this.execOpenLogCheckBox.Location = new System.Drawing.Point(16, 64);
			this.execOpenLogCheckBox.Name = "execOpenLogCheckBox";
			this.execOpenLogCheckBox.Size = new System.Drawing.Size(211, 16);
			this.execOpenLogCheckBox.TabIndex = 2;
			this.execOpenLogCheckBox.Text = "実行終了時に吉里吉里ログを表示する";
			this.execOpenLogCheckBox.UseVisualStyleBackColor = true;
			// 
			// execOpenedFileSaveAllCheckBox
			// 
			this.execOpenedFileSaveAllCheckBox.AutoSize = true;
			this.execOpenedFileSaveAllCheckBox.Location = new System.Drawing.Point(16, 44);
			this.execOpenedFileSaveAllCheckBox.Name = "execOpenedFileSaveAllCheckBox";
			this.execOpenedFileSaveAllCheckBox.Size = new System.Drawing.Size(234, 16);
			this.execOpenedFileSaveAllCheckBox.TabIndex = 1;
			this.execOpenedFileSaveAllCheckBox.Text = "実行前に開いているファイルをすべて保存する";
			this.execOpenedFileSaveAllCheckBox.UseVisualStyleBackColor = true;
			// 
			// execOpenedKrkrKillCheckBox
			// 
			this.execOpenedKrkrKillCheckBox.AutoSize = true;
			this.execOpenedKrkrKillCheckBox.Location = new System.Drawing.Point(16, 24);
			this.execOpenedKrkrKillCheckBox.Name = "execOpenedKrkrKillCheckBox";
			this.execOpenedKrkrKillCheckBox.Size = new System.Drawing.Size(319, 16);
			this.execOpenedKrkrKillCheckBox.TabIndex = 0;
			this.execOpenedKrkrKillCheckBox.Text = "実行時にすでに吉里吉里が起動しているときは強制終了させる";
			this.execOpenedKrkrKillCheckBox.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.projectWatchProjectPathCheckBox);
			this.groupBox1.Controls.Add(this.projectOpenedLastFileCheckBox);
			this.groupBox1.Controls.Add(this.projectOpenedLastProjectCheckBox);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(448, 96);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "プロジェクト";
			// 
			// projectWatchProjectPathCheckBox
			// 
			this.projectWatchProjectPathCheckBox.AutoSize = true;
			this.projectWatchProjectPathCheckBox.Location = new System.Drawing.Point(16, 64);
			this.projectWatchProjectPathCheckBox.Name = "projectWatchProjectPathCheckBox";
			this.projectWatchProjectPathCheckBox.Size = new System.Drawing.Size(266, 16);
			this.projectWatchProjectPathCheckBox.TabIndex = 2;
			this.projectWatchProjectPathCheckBox.Text = "プロジェクトフォルダ内を監視しツリーを自動更新する";
			this.projectWatchProjectPathCheckBox.UseVisualStyleBackColor = true;
			// 
			// projectOpenedLastFileCheckBox
			// 
			this.projectOpenedLastFileCheckBox.AutoSize = true;
			this.projectOpenedLastFileCheckBox.Location = new System.Drawing.Point(16, 44);
			this.projectOpenedLastFileCheckBox.Name = "projectOpenedLastFileCheckBox";
			this.projectOpenedLastFileCheckBox.Size = new System.Drawing.Size(301, 16);
			this.projectOpenedLastFileCheckBox.TabIndex = 1;
			this.projectOpenedLastFileCheckBox.Text = "プロジェクトオープン時に前回最後に開いていたファイルを開く";
			this.projectOpenedLastFileCheckBox.UseVisualStyleBackColor = true;
			// 
			// projectOpenedLastProjectCheckBox
			// 
			this.projectOpenedLastProjectCheckBox.AutoSize = true;
			this.projectOpenedLastProjectCheckBox.Location = new System.Drawing.Point(16, 24);
			this.projectOpenedLastProjectCheckBox.Name = "projectOpenedLastProjectCheckBox";
			this.projectOpenedLastProjectCheckBox.Size = new System.Drawing.Size(226, 16);
			this.projectOpenedLastProjectCheckBox.TabIndex = 0;
			this.projectOpenedLastProjectCheckBox.Text = "起動時に前回最後開いたプロジェクトを開く";
			this.projectOpenedLastProjectCheckBox.UseVisualStyleBackColor = true;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.groupBox4);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(471, 230);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "システム";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.systemCompleteSleepTimeNumBox);
			this.groupBox4.Controls.Add(this.label3);
			this.groupBox4.Location = new System.Drawing.Point(8, 8);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(448, 56);
			this.groupBox4.TabIndex = 0;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "システム";
			// 
			// systemCompleteSleepTimeNumBox
			// 
			this.systemCompleteSleepTimeNumBox.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.systemCompleteSleepTimeNumBox.Location = new System.Drawing.Point(120, 20);
			this.systemCompleteSleepTimeNumBox.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.systemCompleteSleepTimeNumBox.Name = "systemCompleteSleepTimeNumBox";
			this.systemCompleteSleepTimeNumBox.Size = new System.Drawing.Size(96, 19);
			this.systemCompleteSleepTimeNumBox.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(16, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(95, 12);
			this.label3.TabIndex = 1;
			this.label3.Text = "テキスト解析間隔：";
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.groupBox5);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(471, 230);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "ヘルプ";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.helpViewerDialogRadioButton);
			this.groupBox5.Controls.Add(this.helpViewerDockingRadioButton);
			this.groupBox5.Location = new System.Drawing.Point(8, 8);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(448, 72);
			this.groupBox5.TabIndex = 0;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "リファレンス表示";
			// 
			// helpViewerDialogRadioButton
			// 
			this.helpViewerDialogRadioButton.AutoSize = true;
			this.helpViewerDialogRadioButton.Location = new System.Drawing.Point(16, 44);
			this.helpViewerDialogRadioButton.Name = "helpViewerDialogRadioButton";
			this.helpViewerDialogRadioButton.Size = new System.Drawing.Size(187, 16);
			this.helpViewerDialogRadioButton.TabIndex = 1;
			this.helpViewerDialogRadioButton.TabStop = true;
			this.helpViewerDialogRadioButton.Text = "外部ダイアログウィンドウを使用する";
			this.helpViewerDialogRadioButton.UseVisualStyleBackColor = true;
			// 
			// helpViewerDockingRadioButton
			// 
			this.helpViewerDockingRadioButton.AutoSize = true;
			this.helpViewerDockingRadioButton.Location = new System.Drawing.Point(16, 24);
			this.helpViewerDockingRadioButton.Name = "helpViewerDockingRadioButton";
			this.helpViewerDockingRadioButton.Size = new System.Drawing.Size(186, 16);
			this.helpViewerDockingRadioButton.TabIndex = 0;
			this.helpViewerDockingRadioButton.TabStop = true;
			this.helpViewerDockingRadioButton.Text = "ドッキング可能ウィンドウを使用する";
			this.helpViewerDockingRadioButton.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(320, 264);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(400, 264);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "キャンセル";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// checkBox3
			// 
			this.checkBox3.AutoSize = true;
			this.checkBox3.Location = new System.Drawing.Point(16, 64);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(266, 16);
			this.checkBox3.TabIndex = 2;
			this.checkBox3.Text = "プロジェクトフォルダ内を監視しツリーを自動更新する";
			this.checkBox3.UseVisualStyleBackColor = true;
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Location = new System.Drawing.Point(16, 44);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(301, 16);
			this.checkBox2.TabIndex = 1;
			this.checkBox2.Text = "プロジェクトオープン時に前回最後に開いていたファイルを開く";
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(16, 24);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(226, 16);
			this.checkBox1.TabIndex = 0;
			this.checkBox1.Text = "起動時に前回最後開いたプロジェクトを開く";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// EnvOptionForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(479, 296);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.envTabControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EnvOptionForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "環境設定ダイアログ";
			this.Load += new System.EventHandler(this.EnvOptionForm_Load);
			this.envTabControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.historyProjectMaxNumBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.historyFileMaxNumBox)).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.tabPage3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.systemCompleteSleepTimeNumBox)).EndInit();
			this.tabPage4.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl envTabControl;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button historyResetProjectReset;
		private System.Windows.Forms.Button historyFileResetButton;
		private System.Windows.Forms.NumericUpDown historyProjectMaxNumBox;
		private System.Windows.Forms.NumericUpDown historyFileMaxNumBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.CheckBox execOpenLogCheckBox;
		private System.Windows.Forms.CheckBox execOpenedFileSaveAllCheckBox;
		private System.Windows.Forms.CheckBox execOpenedKrkrKillCheckBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox projectWatchProjectPathCheckBox;
		private System.Windows.Forms.CheckBox projectOpenedLastFileCheckBox;
		private System.Windows.Forms.CheckBox projectOpenedLastProjectCheckBox;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.NumericUpDown systemCompleteSleepTimeNumBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox execAddClearOption;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.RadioButton helpViewerDialogRadioButton;
		private System.Windows.Forms.RadioButton helpViewerDockingRadioButton;
	}
}