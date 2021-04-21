namespace AccessLamp
{
	partial class EditPCInstanceDlg
	{
		/// <summary>
		/// 必要なデザイナー変数です。
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

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditPCInstanceDlg));
			this.Nameラベル = new System.Windows.Forms.Label();
			this.InstanceName = new System.Windows.Forms.TextBox();
			this.DisplayName = new System.Windows.Forms.TextBox();
			this.DisplayNameラベル = new System.Windows.Forms.Label();
			this.Radio表示 = new System.Windows.Forms.RadioButton();
			this.Radio非表示 = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// Nameラベル
			// 
			this.Nameラベル.AutoSize = true;
			this.Nameラベル.Location = new System.Drawing.Point(12, 15);
			this.Nameラベル.Name = "Nameラベル";
			this.Nameラベル.Size = new System.Drawing.Size(100, 20);
			this.Nameラベル.TabIndex = 0;
			this.Nameラベル.Text = "インスタンス名";
			// 
			// InstanceName
			// 
			this.InstanceName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.InstanceName.Location = new System.Drawing.Point(118, 12);
			this.InstanceName.Name = "InstanceName";
			this.InstanceName.ReadOnly = true;
			this.InstanceName.Size = new System.Drawing.Size(354, 27);
			this.InstanceName.TabIndex = 1;
			this.InstanceName.TextChanged += new System.EventHandler(this.InstanceName_TextChanged);
			// 
			// DisplayName
			// 
			this.DisplayName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DisplayName.Location = new System.Drawing.Point(118, 45);
			this.DisplayName.MaxLength = 300;
			this.DisplayName.Name = "DisplayName";
			this.DisplayName.Size = new System.Drawing.Size(354, 27);
			this.DisplayName.TabIndex = 2;
			this.DisplayName.TextChanged += new System.EventHandler(this.DisplayName_TextChanged);
			// 
			// DisplayNameラベル
			// 
			this.DisplayNameラベル.AutoSize = true;
			this.DisplayNameラベル.Location = new System.Drawing.Point(12, 48);
			this.DisplayNameラベル.Name = "DisplayNameラベル";
			this.DisplayNameラベル.Size = new System.Drawing.Size(48, 20);
			this.DisplayNameラベル.TabIndex = 3;
			this.DisplayNameラベル.Text = "表示名";
			// 
			// Radio表示
			// 
			this.Radio表示.AutoSize = true;
			this.Radio表示.Location = new System.Drawing.Point(118, 78);
			this.Radio表示.Name = "Radio表示";
			this.Radio表示.Size = new System.Drawing.Size(53, 24);
			this.Radio表示.TabIndex = 4;
			this.Radio表示.TabStop = true;
			this.Radio表示.Text = "表示";
			this.Radio表示.UseVisualStyleBackColor = true;
			this.Radio表示.CheckedChanged += new System.EventHandler(this.Radio表示_CheckedChanged);
			// 
			// Radio非表示
			// 
			this.Radio非表示.AutoSize = true;
			this.Radio非表示.Location = new System.Drawing.Point(177, 78);
			this.Radio非表示.Name = "Radio非表示";
			this.Radio非表示.Size = new System.Drawing.Size(66, 24);
			this.Radio非表示.TabIndex = 5;
			this.Radio非表示.TabStop = true;
			this.Radio非表示.Text = "非表示";
			this.Radio非表示.UseVisualStyleBackColor = true;
			this.Radio非表示.CheckedChanged += new System.EventHandler(this.Radio非表示_CheckedChanged);
			// 
			// EditPCInstanceDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 121);
			this.Controls.Add(this.Radio非表示);
			this.Controls.Add(this.Radio表示);
			this.Controls.Add(this.DisplayNameラベル);
			this.Controls.Add(this.DisplayName);
			this.Controls.Add(this.InstanceName);
			this.Controls.Add(this.Nameラベル);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MinimizeBox = false;
			this.Name = "EditPCInstanceDlg";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AccessLamp_D / インスタンス編集";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditPCInstanceDlg_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EditPCInstanceDlg_FormClosed);
			this.Load += new System.EventHandler(this.EditPCInstanceDlg_Load);
			this.Shown += new System.EventHandler(this.EditPCInstanceDlg_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label Nameラベル;
		private System.Windows.Forms.TextBox InstanceName;
		private System.Windows.Forms.TextBox DisplayName;
		private System.Windows.Forms.Label DisplayNameラベル;
		private System.Windows.Forms.RadioButton Radio表示;
		private System.Windows.Forms.RadioButton Radio非表示;
	}
}