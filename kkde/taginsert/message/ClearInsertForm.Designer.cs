namespace kkde.taginsert.message
{
	partial class ClearInsertForm
	{
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナで生成されたコード

		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.allClearRadioButton = new System.Windows.Forms.RadioButton();
			this.allResetReadioButton = new System.Windows.Forms.RadioButton();
			this.currentClearRadioButton = new System.Windows.Forms.RadioButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.addLabelCheckBox = new System.Windows.Forms.CheckBox();
			this.addLabelNameTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.addNewPageWaitCheckButton = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(160, 216);
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(240, 216);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.currentClearRadioButton);
			this.groupBox1.Controls.Add(this.allResetReadioButton);
			this.groupBox1.Controls.Add(this.allClearRadioButton);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(304, 96);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "クリア対象";
			// 
			// allClearRadioButton
			// 
			this.allClearRadioButton.AutoSize = true;
			this.allClearRadioButton.Location = new System.Drawing.Point(16, 24);
			this.allClearRadioButton.Name = "allClearRadioButton";
			this.allClearRadioButton.Size = new System.Drawing.Size(179, 16);
			this.allClearRadioButton.TabIndex = 0;
			this.allClearRadioButton.TabStop = true;
			this.allClearRadioButton.Text = "全てのメッセージレイヤをクリアする";
			this.allClearRadioButton.UseVisualStyleBackColor = true;
			// 
			// allResetReadioButton
			// 
			this.allResetReadioButton.AutoSize = true;
			this.allResetReadioButton.Location = new System.Drawing.Point(16, 44);
			this.allResetReadioButton.Name = "allResetReadioButton";
			this.allResetReadioButton.Size = new System.Drawing.Size(277, 16);
			this.allResetReadioButton.TabIndex = 1;
			this.allResetReadioButton.TabStop = true;
			this.allResetReadioButton.Text = "全てのメッセージレイヤをクリアし操作対象をリセットする";
			this.allResetReadioButton.UseVisualStyleBackColor = true;
			// 
			// currentClearRadioButton
			// 
			this.currentClearRadioButton.AutoSize = true;
			this.currentClearRadioButton.Location = new System.Drawing.Point(16, 64);
			this.currentClearRadioButton.Name = "currentClearRadioButton";
			this.currentClearRadioButton.Size = new System.Drawing.Size(182, 16);
			this.currentClearRadioButton.TabIndex = 2;
			this.currentClearRadioButton.TabStop = true;
			this.currentClearRadioButton.Text = "現在のメッセージレイヤをクリアする";
			this.currentClearRadioButton.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.addNewPageWaitCheckButton);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.addLabelNameTextBox);
			this.groupBox2.Controls.Add(this.addLabelCheckBox);
			this.groupBox2.Location = new System.Drawing.Point(8, 112);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(304, 96);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "追加オプション";
			// 
			// addLabelCheckBox
			// 
			this.addLabelCheckBox.AutoSize = true;
			this.addLabelCheckBox.Location = new System.Drawing.Point(16, 24);
			this.addLabelCheckBox.Name = "addLabelCheckBox";
			this.addLabelCheckBox.Size = new System.Drawing.Size(104, 16);
			this.addLabelCheckBox.TabIndex = 0;
			this.addLabelCheckBox.Text = "ラベルを追加する";
			this.addLabelCheckBox.UseVisualStyleBackColor = true;
			// 
			// addLabelNameTextBox
			// 
			this.addLabelNameTextBox.Location = new System.Drawing.Point(88, 48);
			this.addLabelNameTextBox.Name = "addLabelNameTextBox";
			this.addLabelNameTextBox.Size = new System.Drawing.Size(200, 19);
			this.addLabelNameTextBox.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(32, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(51, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "ラベル名：";
			// 
			// addNewPageWaitCheckButton
			// 
			this.addNewPageWaitCheckButton.AutoSize = true;
			this.addNewPageWaitCheckButton.Location = new System.Drawing.Point(16, 72);
			this.addNewPageWaitCheckButton.Name = "addNewPageWaitCheckButton";
			this.addNewPageWaitCheckButton.Size = new System.Drawing.Size(139, 16);
			this.addNewPageWaitCheckButton.TabIndex = 3;
			this.addNewPageWaitCheckButton.Text = "改ページ待ちを追加する";
			this.addNewPageWaitCheckButton.UseVisualStyleBackColor = true;
			// 
			// ClearInsertForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.ClientSize = new System.Drawing.Size(321, 246);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "ClearInsertForm";
			this.Text = "メッセージクリア";
			this.Controls.SetChildIndex(this.cancelButton, 0);
			this.Controls.SetChildIndex(this.okButton, 0);
			this.Controls.SetChildIndex(this.groupBox1, 0);
			this.Controls.SetChildIndex(this.groupBox2, 0);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton allResetReadioButton;
		private System.Windows.Forms.RadioButton allClearRadioButton;
		private System.Windows.Forms.RadioButton currentClearRadioButton;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox addLabelNameTextBox;
		private System.Windows.Forms.CheckBox addLabelCheckBox;
		private System.Windows.Forms.CheckBox addNewPageWaitCheckButton;
	}
}
