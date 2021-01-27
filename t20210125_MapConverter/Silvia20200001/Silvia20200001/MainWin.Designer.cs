namespace Charlotte
{
	partial class MainWin
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWin));
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.SouthBar = new System.Windows.Forms.StatusStrip();
			this.StatusBar = new System.Windows.Forms.ToolStripStatusLabel();
			this.SubStatusBar = new System.Windows.Forms.ToolStripStatusLabel();
			this.NorthBar = new System.Windows.Forms.MenuStrip();
			this.アプリToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.マップファイルを開くToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenCsvDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.DropLabel = new System.Windows.Forms.Label();
			this.DropPanel = new System.Windows.Forms.Panel();
			this.ConsoleOut = new System.Windows.Forms.TextBox();
			this.SouthBar.SuspendLayout();
			this.NorthBar.SuspendLayout();
			this.DropPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainTimer
			// 
			this.MainTimer.Enabled = true;
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// SouthBar
			// 
			this.SouthBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusBar,
            this.SubStatusBar});
			this.SouthBar.Location = new System.Drawing.Point(0, 339);
			this.SouthBar.Name = "SouthBar";
			this.SouthBar.Size = new System.Drawing.Size(584, 22);
			this.SouthBar.TabIndex = 3;
			this.SouthBar.Text = "statusStrip1";
			// 
			// StatusBar
			// 
			this.StatusBar.Name = "StatusBar";
			this.StatusBar.Size = new System.Drawing.Size(493, 17);
			this.StatusBar.Spring = true;
			this.StatusBar.Text = "StatusBar";
			this.StatusBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// SubStatusBar
			// 
			this.SubStatusBar.Name = "SubStatusBar";
			this.SubStatusBar.Size = new System.Drawing.Size(76, 17);
			this.SubStatusBar.Text = "SubStatusBar";
			// 
			// NorthBar
			// 
			this.NorthBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.アプリToolStripMenuItem});
			this.NorthBar.Location = new System.Drawing.Point(0, 0);
			this.NorthBar.Name = "NorthBar";
			this.NorthBar.Size = new System.Drawing.Size(584, 24);
			this.NorthBar.TabIndex = 0;
			this.NorthBar.Text = "menuStrip1";
			// 
			// アプリToolStripMenuItem
			// 
			this.アプリToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.マップファイルを開くToolStripMenuItem,
            this.OpenCsvDataMenuItem,
            this.toolStripMenuItem1,
            this.終了ToolStripMenuItem});
			this.アプリToolStripMenuItem.Name = "アプリToolStripMenuItem";
			this.アプリToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
			this.アプリToolStripMenuItem.Text = "アプリ";
			// 
			// マップファイルを開くToolStripMenuItem
			// 
			this.マップファイルを開くToolStripMenuItem.Name = "マップファイルを開くToolStripMenuItem";
			this.マップファイルを開くToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
			this.マップファイルを開くToolStripMenuItem.Text = "マップファイルを開く";
			this.マップファイルを開くToolStripMenuItem.Click += new System.EventHandler(this.マップファイルを開くToolStripMenuItem_Click);
			// 
			// OpenCsvDataMenuItem
			// 
			this.OpenCsvDataMenuItem.Name = "OpenCsvDataMenuItem";
			this.OpenCsvDataMenuItem.Size = new System.Drawing.Size(212, 22);
			this.OpenCsvDataMenuItem.Text = "Platinum - マップデータを開く";
			this.OpenCsvDataMenuItem.Click += new System.EventHandler(this.OpenCsvDataMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(209, 6);
			// 
			// 終了ToolStripMenuItem
			// 
			this.終了ToolStripMenuItem.Name = "終了ToolStripMenuItem";
			this.終了ToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
			this.終了ToolStripMenuItem.Text = "終了";
			this.終了ToolStripMenuItem.Click += new System.EventHandler(this.終了ToolStripMenuItem_Click);
			// 
			// DropLabel
			// 
			this.DropLabel.AutoSize = true;
			this.DropLabel.Location = new System.Drawing.Point(30, 30);
			this.DropLabel.Name = "DropLabel";
			this.DropLabel.Size = new System.Drawing.Size(243, 40);
			this.DropLabel.TabIndex = 0;
			this.DropLabel.Text = "マップファイル または マップデータ を\r\nここへドラッグ＆ドロップして下さい。";
			this.DropLabel.Click += new System.EventHandler(this.DropLabel_Click);
			// 
			// DropPanel
			// 
			this.DropPanel.AllowDrop = true;
			this.DropPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DropPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.DropPanel.Controls.Add(this.DropLabel);
			this.DropPanel.Location = new System.Drawing.Point(12, 27);
			this.DropPanel.Name = "DropPanel";
			this.DropPanel.Size = new System.Drawing.Size(560, 200);
			this.DropPanel.TabIndex = 1;
			this.DropPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.DropPanel_DragDrop);
			this.DropPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.DropPanel_DragEnter);
			this.DropPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.DropPanel_Paint);
			// 
			// ConsoleOut
			// 
			this.ConsoleOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ConsoleOut.Location = new System.Drawing.Point(12, 233);
			this.ConsoleOut.Multiline = true;
			this.ConsoleOut.Name = "ConsoleOut";
			this.ConsoleOut.ReadOnly = true;
			this.ConsoleOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.ConsoleOut.Size = new System.Drawing.Size(560, 103);
			this.ConsoleOut.TabIndex = 2;
			this.ConsoleOut.Text = "1行目\r\n2行目\r\n3行目\r\n4行目\r\n5行目";
			this.ConsoleOut.WordWrap = false;
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 361);
			this.Controls.Add(this.ConsoleOut);
			this.Controls.Add(this.DropPanel);
			this.Controls.Add(this.SouthBar);
			this.Controls.Add(this.NorthBar);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.NorthBar;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "MainWin";
			this.Text = "マップファイル変換";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.Shown += new System.EventHandler(this.MainWin_Shown);
			this.ResizeBegin += new System.EventHandler(this.MainWin_ResizeBegin);
			this.ResizeEnd += new System.EventHandler(this.MainWin_ResizeEnd);
			this.SizeChanged += new System.EventHandler(this.MainWin_SizeChanged);
			this.Resize += new System.EventHandler(this.MainWin_Resize);
			this.SouthBar.ResumeLayout(false);
			this.SouthBar.PerformLayout();
			this.NorthBar.ResumeLayout(false);
			this.NorthBar.PerformLayout();
			this.DropPanel.ResumeLayout(false);
			this.DropPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Timer MainTimer;
		private System.Windows.Forms.StatusStrip SouthBar;
		private System.Windows.Forms.ToolStripStatusLabel StatusBar;
		private System.Windows.Forms.ToolStripStatusLabel SubStatusBar;
		private System.Windows.Forms.MenuStrip NorthBar;
		private System.Windows.Forms.ToolStripMenuItem アプリToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 終了ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem マップファイルを開くToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.Label DropLabel;
		private System.Windows.Forms.Panel DropPanel;
		private System.Windows.Forms.ToolStripMenuItem OpenCsvDataMenuItem;
		private System.Windows.Forms.TextBox ConsoleOut;
	}
}

