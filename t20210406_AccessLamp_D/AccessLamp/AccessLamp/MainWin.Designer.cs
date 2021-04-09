namespace AccessLamp
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
			this.RClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.MainWinMenu_Item_設定 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.MainWinMenu_Item_終了 = new System.Windows.Forms.ToolStripMenuItem();
			this.TaskTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.TaskTrayIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.TaskTrayIconMenu_Item_設定 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.TaskTrayIconMenu_Item_終了 = new System.Windows.Forms.ToolStripMenuItem();
			this.RClickMenu.SuspendLayout();
			this.TaskTrayIconMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainTimer
			// 
			this.MainTimer.Enabled = true;
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// RClickMenu
			// 
			this.RClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainWinMenu_Item_設定,
            this.toolStripMenuItem1,
            this.MainWinMenu_Item_終了});
			this.RClickMenu.Name = "RClickMenu";
			this.RClickMenu.Size = new System.Drawing.Size(99, 54);
			// 
			// MainWinMenu_Item_設定
			// 
			this.MainWinMenu_Item_設定.Name = "MainWinMenu_Item_設定";
			this.MainWinMenu_Item_設定.Size = new System.Drawing.Size(98, 22);
			this.MainWinMenu_Item_設定.Text = "設定";
			this.MainWinMenu_Item_設定.Click += new System.EventHandler(this.MainWinMenu_Item_設定_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(95, 6);
			// 
			// MainWinMenu_Item_終了
			// 
			this.MainWinMenu_Item_終了.Name = "MainWinMenu_Item_終了";
			this.MainWinMenu_Item_終了.Size = new System.Drawing.Size(98, 22);
			this.MainWinMenu_Item_終了.Text = "終了";
			this.MainWinMenu_Item_終了.Click += new System.EventHandler(this.MainWinMenu_Item_終了_Click);
			// 
			// TaskTrayIcon
			// 
			this.TaskTrayIcon.ContextMenuStrip = this.TaskTrayIconMenu;
			this.TaskTrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("TaskTrayIcon.Icon")));
			this.TaskTrayIcon.Text = "AccessLamp_D";
			// 
			// TaskTrayIconMenu
			// 
			this.TaskTrayIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TaskTrayIconMenu_Item_設定,
            this.toolStripMenuItem2,
            this.TaskTrayIconMenu_Item_終了});
			this.TaskTrayIconMenu.Name = "TaskTrayIconMenu";
			this.TaskTrayIconMenu.Size = new System.Drawing.Size(99, 54);
			// 
			// TaskTrayIconMenu_Item_設定
			// 
			this.TaskTrayIconMenu_Item_設定.Name = "TaskTrayIconMenu_Item_設定";
			this.TaskTrayIconMenu_Item_設定.Size = new System.Drawing.Size(98, 22);
			this.TaskTrayIconMenu_Item_設定.Text = "設定";
			this.TaskTrayIconMenu_Item_設定.Click += new System.EventHandler(this.TaskTrayIconMenu_Item_設定_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(95, 6);
			// 
			// TaskTrayIconMenu_Item_終了
			// 
			this.TaskTrayIconMenu_Item_終了.Name = "TaskTrayIconMenu_Item_終了";
			this.TaskTrayIconMenu_Item_終了.Size = new System.Drawing.Size(98, 22);
			this.TaskTrayIconMenu_Item_終了.Text = "終了";
			this.TaskTrayIconMenu_Item_終了.Click += new System.EventHandler(this.TaskTrayIconMenu_Item_終了_Click);
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(300, 300);
			this.ContextMenuStrip = this.RClickMenu;
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainWin";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "AccessLamp_D";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.Shown += new System.EventHandler(this.MainWin_Shown);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainWin_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainWin_MouseMove);
			this.RClickMenu.ResumeLayout(false);
			this.TaskTrayIconMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer MainTimer;
		private System.Windows.Forms.ContextMenuStrip RClickMenu;
		private System.Windows.Forms.ToolStripMenuItem MainWinMenu_Item_設定;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem MainWinMenu_Item_終了;
		private System.Windows.Forms.NotifyIcon TaskTrayIcon;
		private System.Windows.Forms.ContextMenuStrip TaskTrayIconMenu;
		private System.Windows.Forms.ToolStripMenuItem TaskTrayIconMenu_Item_設定;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem TaskTrayIconMenu_Item_終了;
	}
}

