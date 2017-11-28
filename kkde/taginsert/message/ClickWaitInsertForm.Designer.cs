namespace kkde.taginsert.message
{
	partial class ClickWaitInsertForm
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
			this.lwaitRadioButton = new System.Windows.Forms.RadioButton();
			this.lwaitAddNewLineCheckBox = new System.Windows.Forms.CheckBox();
			this.pwaitAddClearLayerCheckBox = new System.Windows.Forms.CheckBox();
			this.pwaitRadioButton = new System.Windows.Forms.RadioButton();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(120, 128);
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(200, 128);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lwaitRadioButton);
			this.groupBox1.Controls.Add(this.lwaitAddNewLineCheckBox);
			this.groupBox1.Controls.Add(this.pwaitAddClearLayerCheckBox);
			this.groupBox1.Controls.Add(this.pwaitRadioButton);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(264, 112);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "クリック待ちオプション";
			// 
			// lwaitRadioButton
			// 
			this.lwaitRadioButton.AutoSize = true;
			this.lwaitRadioButton.Checked = true;
			this.lwaitRadioButton.Location = new System.Drawing.Point(16, 16);
			this.lwaitRadioButton.Name = "lwaitRadioButton";
			this.lwaitRadioButton.Size = new System.Drawing.Size(98, 16);
			this.lwaitRadioButton.TabIndex = 6;
			this.lwaitRadioButton.TabStop = true;
			this.lwaitRadioButton.Text = "行末クリック待ち";
			this.lwaitRadioButton.UseVisualStyleBackColor = true;
			// 
			// lwaitAddNewLineCheckBox
			// 
			this.lwaitAddNewLineCheckBox.AutoSize = true;
			this.lwaitAddNewLineCheckBox.Location = new System.Drawing.Point(32, 36);
			this.lwaitAddNewLineCheckBox.Name = "lwaitAddNewLineCheckBox";
			this.lwaitAddNewLineCheckBox.Size = new System.Drawing.Size(117, 16);
			this.lwaitAddNewLineCheckBox.TabIndex = 8;
			this.lwaitAddNewLineCheckBox.Text = "改行タグを追加する";
			this.lwaitAddNewLineCheckBox.UseVisualStyleBackColor = true;
			// 
			// pwaitAddClearLayerCheckBox
			// 
			this.pwaitAddClearLayerCheckBox.AutoSize = true;
			this.pwaitAddClearLayerCheckBox.Location = new System.Drawing.Point(32, 84);
			this.pwaitAddClearLayerCheckBox.Name = "pwaitAddClearLayerCheckBox";
			this.pwaitAddClearLayerCheckBox.Size = new System.Drawing.Size(173, 16);
			this.pwaitAddClearLayerCheckBox.TabIndex = 9;
			this.pwaitAddClearLayerCheckBox.Text = "メッセージレイヤクリアを追加する";
			this.pwaitAddClearLayerCheckBox.UseVisualStyleBackColor = true;
			// 
			// pwaitRadioButton
			// 
			this.pwaitRadioButton.AutoSize = true;
			this.pwaitRadioButton.Location = new System.Drawing.Point(16, 64);
			this.pwaitRadioButton.Name = "pwaitRadioButton";
			this.pwaitRadioButton.Size = new System.Drawing.Size(116, 16);
			this.pwaitRadioButton.TabIndex = 7;
			this.pwaitRadioButton.Text = "改ページクリック待ち";
			this.pwaitRadioButton.UseVisualStyleBackColor = true;
			// 
			// ClickWaitInsertForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.ClientSize = new System.Drawing.Size(283, 158);
			this.Controls.Add(this.groupBox1);
			this.Name = "ClickWaitInsertForm";
			this.Text = "クリック待ち";
			this.Controls.SetChildIndex(this.okButton, 0);
			this.Controls.SetChildIndex(this.cancelButton, 0);
			this.Controls.SetChildIndex(this.groupBox1, 0);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton lwaitRadioButton;
		private System.Windows.Forms.CheckBox lwaitAddNewLineCheckBox;
		private System.Windows.Forms.CheckBox pwaitAddClearLayerCheckBox;
		private System.Windows.Forms.RadioButton pwaitRadioButton;

	}
}
