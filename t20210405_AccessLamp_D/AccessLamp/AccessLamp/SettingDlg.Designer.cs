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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingDlg));
			this.InstanceList = new System.Windows.Forms.ListBox();
			this.InstanceListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.選択解除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.InstanceList説明 = new System.Windows.Forms.Label();
			this.BtnUp = new System.Windows.Forms.Button();
			this.BtnDown = new System.Windows.Forms.Button();
			this.BtnEdit = new System.Windows.Forms.Button();
			this.MainTab = new System.Windows.Forms.TabControl();
			this.MainTab_01 = new System.Windows.Forms.TabPage();
			this.MainTab_02 = new System.Windows.Forms.TabPage();
			this.BackgrounSample = new System.Windows.Forms.Panel();
			this.VeryBusyLampSample = new System.Windows.Forms.Label();
			this.BusyLampSample = new System.Windows.Forms.Label();
			this.IdleLampSample = new System.Windows.Forms.Label();
			this.DeniedLampSample = new System.Windows.Forms.Label();
			this.BtnVeryBusyForeColor = new System.Windows.Forms.Button();
			this.BtnVeryBusyBackColor = new System.Windows.Forms.Button();
			this.BtnBusyForeColor = new System.Windows.Forms.Button();
			this.BtnBusyBackColor = new System.Windows.Forms.Button();
			this.BtnIdleForeColor = new System.Windows.Forms.Button();
			this.BtnIdleBackColor = new System.Windows.Forms.Button();
			this.BtnDeniedForeColor = new System.Windows.Forms.Button();
			this.BtnDeniedBackColor = new System.Windows.Forms.Button();
			this.BtnBackgroundColor = new System.Windows.Forms.Button();
			this.InstanceListMenu.SuspendLayout();
			this.MainTab.SuspendLayout();
			this.MainTab_01.SuspendLayout();
			this.MainTab_02.SuspendLayout();
			this.BackgrounSample.SuspendLayout();
			this.SuspendLayout();
			// 
			// InstanceList
			// 
			this.InstanceList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.InstanceList.ContextMenuStrip = this.InstanceListMenu;
			this.InstanceList.FormattingEnabled = true;
			this.InstanceList.ItemHeight = 20;
			this.InstanceList.Location = new System.Drawing.Point(6, 26);
			this.InstanceList.Name = "InstanceList";
			this.InstanceList.ScrollAlwaysVisible = true;
			this.InstanceList.Size = new System.Drawing.Size(474, 364);
			this.InstanceList.TabIndex = 1;
			this.InstanceList.SelectedIndexChanged += new System.EventHandler(this.InstanceList_SelectedIndexChanged);
			// 
			// InstanceListMenu
			// 
			this.InstanceListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.選択解除ToolStripMenuItem});
			this.InstanceListMenu.Name = "InstanceListMenu";
			this.InstanceListMenu.Size = new System.Drawing.Size(123, 26);
			// 
			// 選択解除ToolStripMenuItem
			// 
			this.選択解除ToolStripMenuItem.Name = "選択解除ToolStripMenuItem";
			this.選択解除ToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
			this.選択解除ToolStripMenuItem.Text = "選択解除";
			this.選択解除ToolStripMenuItem.Click += new System.EventHandler(this.選択解除ToolStripMenuItem_Click);
			// 
			// InstanceList説明
			// 
			this.InstanceList説明.AutoSize = true;
			this.InstanceList説明.Location = new System.Drawing.Point(6, 3);
			this.InstanceList説明.Name = "InstanceList説明";
			this.InstanceList説明.Size = new System.Drawing.Size(373, 20);
			this.InstanceList説明.TabIndex = 0;
			this.InstanceList説明.Text = "行選択して Edit 押下で編集 / Up , Down 押下でオーダー変更";
			// 
			// BtnUp
			// 
			this.BtnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnUp.Location = new System.Drawing.Point(486, 26);
			this.BtnUp.Name = "BtnUp";
			this.BtnUp.Size = new System.Drawing.Size(60, 60);
			this.BtnUp.TabIndex = 2;
			this.BtnUp.Text = "Up";
			this.BtnUp.UseVisualStyleBackColor = true;
			this.BtnUp.Click += new System.EventHandler(this.BtnUp_Click);
			// 
			// BtnDown
			// 
			this.BtnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnDown.Location = new System.Drawing.Point(486, 92);
			this.BtnDown.Name = "BtnDown";
			this.BtnDown.Size = new System.Drawing.Size(60, 60);
			this.BtnDown.TabIndex = 3;
			this.BtnDown.Text = "Down";
			this.BtnDown.UseVisualStyleBackColor = true;
			this.BtnDown.Click += new System.EventHandler(this.BtnDown_Click);
			// 
			// BtnEdit
			// 
			this.BtnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnEdit.Location = new System.Drawing.Point(486, 178);
			this.BtnEdit.Name = "BtnEdit";
			this.BtnEdit.Size = new System.Drawing.Size(60, 60);
			this.BtnEdit.TabIndex = 4;
			this.BtnEdit.Text = "Edit";
			this.BtnEdit.UseVisualStyleBackColor = true;
			this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
			// 
			// MainTab
			// 
			this.MainTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainTab.Controls.Add(this.MainTab_01);
			this.MainTab.Controls.Add(this.MainTab_02);
			this.MainTab.Location = new System.Drawing.Point(12, 12);
			this.MainTab.Name = "MainTab";
			this.MainTab.SelectedIndex = 0;
			this.MainTab.Size = new System.Drawing.Size(560, 429);
			this.MainTab.TabIndex = 0;
			// 
			// MainTab_01
			// 
			this.MainTab_01.Controls.Add(this.InstanceList);
			this.MainTab_01.Controls.Add(this.BtnUp);
			this.MainTab_01.Controls.Add(this.InstanceList説明);
			this.MainTab_01.Controls.Add(this.BtnEdit);
			this.MainTab_01.Controls.Add(this.BtnDown);
			this.MainTab_01.Location = new System.Drawing.Point(4, 29);
			this.MainTab_01.Name = "MainTab_01";
			this.MainTab_01.Padding = new System.Windows.Forms.Padding(3);
			this.MainTab_01.Size = new System.Drawing.Size(552, 396);
			this.MainTab_01.TabIndex = 0;
			this.MainTab_01.Text = "インスタンス選択";
			this.MainTab_01.UseVisualStyleBackColor = true;
			this.MainTab_01.Click += new System.EventHandler(this.MainTab_01_Click);
			// 
			// MainTab_02
			// 
			this.MainTab_02.Controls.Add(this.BackgrounSample);
			this.MainTab_02.Controls.Add(this.BtnVeryBusyForeColor);
			this.MainTab_02.Controls.Add(this.BtnVeryBusyBackColor);
			this.MainTab_02.Controls.Add(this.BtnBusyForeColor);
			this.MainTab_02.Controls.Add(this.BtnBusyBackColor);
			this.MainTab_02.Controls.Add(this.BtnIdleForeColor);
			this.MainTab_02.Controls.Add(this.BtnIdleBackColor);
			this.MainTab_02.Controls.Add(this.BtnDeniedForeColor);
			this.MainTab_02.Controls.Add(this.BtnDeniedBackColor);
			this.MainTab_02.Controls.Add(this.BtnBackgroundColor);
			this.MainTab_02.Location = new System.Drawing.Point(4, 29);
			this.MainTab_02.Name = "MainTab_02";
			this.MainTab_02.Padding = new System.Windows.Forms.Padding(3);
			this.MainTab_02.Size = new System.Drawing.Size(552, 396);
			this.MainTab_02.TabIndex = 1;
			this.MainTab_02.Text = "色指定";
			this.MainTab_02.UseVisualStyleBackColor = true;
			this.MainTab_02.Click += new System.EventHandler(this.MainTab_02_Click);
			// 
			// BackgrounSample
			// 
			this.BackgrounSample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.BackgrounSample.Controls.Add(this.VeryBusyLampSample);
			this.BackgrounSample.Controls.Add(this.BusyLampSample);
			this.BackgrounSample.Controls.Add(this.IdleLampSample);
			this.BackgrounSample.Controls.Add(this.DeniedLampSample);
			this.BackgrounSample.Location = new System.Drawing.Point(358, 62);
			this.BackgrounSample.Name = "BackgrounSample";
			this.BackgrounSample.Size = new System.Drawing.Size(188, 218);
			this.BackgrounSample.TabIndex = 9;
			// 
			// VeryBusyLampSample
			// 
			this.VeryBusyLampSample.AutoSize = true;
			this.VeryBusyLampSample.Location = new System.Drawing.Point(15, 183);
			this.VeryBusyLampSample.Name = "VeryBusyLampSample";
			this.VeryBusyLampSample.Size = new System.Drawing.Size(158, 20);
			this.VeryBusyLampSample.TabIndex = 3;
			this.VeryBusyLampSample.Text = "Sample Sample Sample";
			// 
			// BusyLampSample
			// 
			this.BusyLampSample.AutoSize = true;
			this.BusyLampSample.Location = new System.Drawing.Point(15, 127);
			this.BusyLampSample.Name = "BusyLampSample";
			this.BusyLampSample.Size = new System.Drawing.Size(158, 20);
			this.BusyLampSample.TabIndex = 2;
			this.BusyLampSample.Text = "Sample Sample Sample";
			// 
			// IdleLampSample
			// 
			this.IdleLampSample.AutoSize = true;
			this.IdleLampSample.Location = new System.Drawing.Point(15, 71);
			this.IdleLampSample.Name = "IdleLampSample";
			this.IdleLampSample.Size = new System.Drawing.Size(158, 20);
			this.IdleLampSample.TabIndex = 1;
			this.IdleLampSample.Text = "Sample Sample Sample";
			// 
			// DeniedLampSample
			// 
			this.DeniedLampSample.AutoSize = true;
			this.DeniedLampSample.Location = new System.Drawing.Point(15, 15);
			this.DeniedLampSample.Name = "DeniedLampSample";
			this.DeniedLampSample.Size = new System.Drawing.Size(158, 20);
			this.DeniedLampSample.TabIndex = 0;
			this.DeniedLampSample.Text = "Sample Sample Sample";
			// 
			// BtnVeryBusyForeColor
			// 
			this.BtnVeryBusyForeColor.Location = new System.Drawing.Point(182, 230);
			this.BtnVeryBusyForeColor.Name = "BtnVeryBusyForeColor";
			this.BtnVeryBusyForeColor.Size = new System.Drawing.Size(170, 50);
			this.BtnVeryBusyForeColor.TabIndex = 8;
			this.BtnVeryBusyForeColor.Text = "Very_Busy 文字色";
			this.BtnVeryBusyForeColor.UseVisualStyleBackColor = true;
			this.BtnVeryBusyForeColor.Click += new System.EventHandler(this.BtnVeryBusyForeColor_Click);
			// 
			// BtnVeryBusyBackColor
			// 
			this.BtnVeryBusyBackColor.Location = new System.Drawing.Point(6, 230);
			this.BtnVeryBusyBackColor.Name = "BtnVeryBusyBackColor";
			this.BtnVeryBusyBackColor.Size = new System.Drawing.Size(170, 50);
			this.BtnVeryBusyBackColor.TabIndex = 7;
			this.BtnVeryBusyBackColor.Text = "Very_Busy 背景色";
			this.BtnVeryBusyBackColor.UseVisualStyleBackColor = true;
			this.BtnVeryBusyBackColor.Click += new System.EventHandler(this.BtnVeryBusyBackColor_Click);
			// 
			// BtnBusyForeColor
			// 
			this.BtnBusyForeColor.Location = new System.Drawing.Point(182, 174);
			this.BtnBusyForeColor.Name = "BtnBusyForeColor";
			this.BtnBusyForeColor.Size = new System.Drawing.Size(170, 50);
			this.BtnBusyForeColor.TabIndex = 6;
			this.BtnBusyForeColor.Text = "Busy 文字色";
			this.BtnBusyForeColor.UseVisualStyleBackColor = true;
			this.BtnBusyForeColor.Click += new System.EventHandler(this.BtnBusyForeColor_Click);
			// 
			// BtnBusyBackColor
			// 
			this.BtnBusyBackColor.Location = new System.Drawing.Point(6, 174);
			this.BtnBusyBackColor.Name = "BtnBusyBackColor";
			this.BtnBusyBackColor.Size = new System.Drawing.Size(170, 50);
			this.BtnBusyBackColor.TabIndex = 5;
			this.BtnBusyBackColor.Text = "Busy 背景色";
			this.BtnBusyBackColor.UseVisualStyleBackColor = true;
			this.BtnBusyBackColor.Click += new System.EventHandler(this.BtnBusyBackColor_Click);
			// 
			// BtnIdleForeColor
			// 
			this.BtnIdleForeColor.Location = new System.Drawing.Point(182, 118);
			this.BtnIdleForeColor.Name = "BtnIdleForeColor";
			this.BtnIdleForeColor.Size = new System.Drawing.Size(170, 50);
			this.BtnIdleForeColor.TabIndex = 4;
			this.BtnIdleForeColor.Text = "Idle 文字色";
			this.BtnIdleForeColor.UseVisualStyleBackColor = true;
			this.BtnIdleForeColor.Click += new System.EventHandler(this.BtnIdleForeColor_Click);
			// 
			// BtnIdleBackColor
			// 
			this.BtnIdleBackColor.Location = new System.Drawing.Point(6, 118);
			this.BtnIdleBackColor.Name = "BtnIdleBackColor";
			this.BtnIdleBackColor.Size = new System.Drawing.Size(170, 50);
			this.BtnIdleBackColor.TabIndex = 3;
			this.BtnIdleBackColor.Text = "Idle 背景色";
			this.BtnIdleBackColor.UseVisualStyleBackColor = true;
			this.BtnIdleBackColor.Click += new System.EventHandler(this.BtnIdleBackColor_Click);
			// 
			// BtnDeniedForeColor
			// 
			this.BtnDeniedForeColor.Location = new System.Drawing.Point(182, 62);
			this.BtnDeniedForeColor.Name = "BtnDeniedForeColor";
			this.BtnDeniedForeColor.Size = new System.Drawing.Size(170, 50);
			this.BtnDeniedForeColor.TabIndex = 2;
			this.BtnDeniedForeColor.Text = "Denied 文字色";
			this.BtnDeniedForeColor.UseVisualStyleBackColor = true;
			this.BtnDeniedForeColor.Click += new System.EventHandler(this.BtnDeniedForeColor_Click);
			// 
			// BtnDeniedBackColor
			// 
			this.BtnDeniedBackColor.Location = new System.Drawing.Point(6, 62);
			this.BtnDeniedBackColor.Name = "BtnDeniedBackColor";
			this.BtnDeniedBackColor.Size = new System.Drawing.Size(170, 50);
			this.BtnDeniedBackColor.TabIndex = 1;
			this.BtnDeniedBackColor.Text = "Denied 背景色";
			this.BtnDeniedBackColor.UseVisualStyleBackColor = true;
			this.BtnDeniedBackColor.Click += new System.EventHandler(this.BtnDeniedBackColor_Click);
			// 
			// BtnBackgroundColor
			// 
			this.BtnBackgroundColor.Location = new System.Drawing.Point(6, 6);
			this.BtnBackgroundColor.Name = "BtnBackgroundColor";
			this.BtnBackgroundColor.Size = new System.Drawing.Size(346, 50);
			this.BtnBackgroundColor.TabIndex = 0;
			this.BtnBackgroundColor.Text = "背景色";
			this.BtnBackgroundColor.UseVisualStyleBackColor = true;
			this.BtnBackgroundColor.Click += new System.EventHandler(this.BtnBackgroundColor_Click);
			// 
			// SettingDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 453);
			this.Controls.Add(this.MainTab);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MinimizeBox = false;
			this.Name = "SettingDlg";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "AccessLamp_D 設定";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingDlg_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingDlg_FormClosed);
			this.Load += new System.EventHandler(this.SettingDlg_Load);
			this.Shown += new System.EventHandler(this.SettingDlg_Shown);
			this.InstanceListMenu.ResumeLayout(false);
			this.MainTab.ResumeLayout(false);
			this.MainTab_01.ResumeLayout(false);
			this.MainTab_01.PerformLayout();
			this.MainTab_02.ResumeLayout(false);
			this.BackgrounSample.ResumeLayout(false);
			this.BackgrounSample.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox InstanceList;
		private System.Windows.Forms.Label InstanceList説明;
		private System.Windows.Forms.Button BtnUp;
		private System.Windows.Forms.Button BtnDown;
		private System.Windows.Forms.Button BtnEdit;
		private System.Windows.Forms.ContextMenuStrip InstanceListMenu;
		private System.Windows.Forms.ToolStripMenuItem 選択解除ToolStripMenuItem;
		private System.Windows.Forms.TabControl MainTab;
		private System.Windows.Forms.TabPage MainTab_01;
		private System.Windows.Forms.TabPage MainTab_02;
		private System.Windows.Forms.Button BtnVeryBusyForeColor;
		private System.Windows.Forms.Button BtnVeryBusyBackColor;
		private System.Windows.Forms.Button BtnBusyForeColor;
		private System.Windows.Forms.Button BtnBusyBackColor;
		private System.Windows.Forms.Button BtnIdleForeColor;
		private System.Windows.Forms.Button BtnIdleBackColor;
		private System.Windows.Forms.Button BtnDeniedForeColor;
		private System.Windows.Forms.Button BtnDeniedBackColor;
		private System.Windows.Forms.Button BtnBackgroundColor;
		private System.Windows.Forms.Panel BackgrounSample;
		private System.Windows.Forms.Label DeniedLampSample;
		private System.Windows.Forms.Label VeryBusyLampSample;
		private System.Windows.Forms.Label BusyLampSample;
		private System.Windows.Forms.Label IdleLampSample;
	}
}