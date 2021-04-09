namespace AccessLamp
{
	partial class SettingDlg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingDlg));
			this.SelectableInstanceNames = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// SelectableInstanceNames
			// 
			this.SelectableInstanceNames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SelectableInstanceNames.FormattingEnabled = true;
			this.SelectableInstanceNames.ItemHeight = 20;
			this.SelectableInstanceNames.Location = new System.Drawing.Point(12, 32);
			this.SelectableInstanceNames.Name = "SelectableInstanceNames";
			this.SelectableInstanceNames.ScrollAlwaysVisible = true;
			this.SelectableInstanceNames.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
			this.SelectableInstanceNames.Size = new System.Drawing.Size(360, 364);
			this.SelectableInstanceNames.TabIndex = 0;
			this.SelectableInstanceNames.SelectedIndexChanged += new System.EventHandler(this.SelectableInstanceNames_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(256, 20);
			this.label1.TabIndex = 1;
			this.label1.Text = "表示するインスタンスを選択して下さい。";
			// 
			// SettingDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 408);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.SelectableInstanceNames);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingDlg";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AccessLamp_D / 設定";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingDlg_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingDlg_FormClosed);
			this.Load += new System.EventHandler(this.SettingDlg_Load);
			this.Shown += new System.EventHandler(this.SettingDlg_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox SelectableInstanceNames;
		private System.Windows.Forms.Label label1;
	}
}