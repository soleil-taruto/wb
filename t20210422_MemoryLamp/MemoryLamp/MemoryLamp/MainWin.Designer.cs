namespace MemoryLamp
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
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.TaskTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.TaskTrayIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.TaskTrayIconMenu_Item_終了 = new System.Windows.Forms.ToolStripMenuItem();
			this.TaskTrayIconMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainTimer
			// 
			this.MainTimer.Enabled = true;
			this.MainTimer.Interval = 1000;
			this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
			// 
			// TaskTrayIcon
			// 
			this.TaskTrayIcon.ContextMenuStrip = this.TaskTrayIconMenu;
			this.TaskTrayIcon.Text = "MemoryLamp";
			// 
			// TaskTrayIconMenu
			// 
			this.TaskTrayIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TaskTrayIconMenu_Item_終了});
			this.TaskTrayIconMenu.Name = "TaskTrayIconMenu";
			this.TaskTrayIconMenu.Size = new System.Drawing.Size(99, 26);
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
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Location = new System.Drawing.Point(-400, -400);
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainWin";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "MemoryLamp_MainWin";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
			this.Load += new System.EventHandler(this.MainWin_Load);
			this.Shown += new System.EventHandler(this.MainWin_Shown);
			this.TaskTrayIconMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer MainTimer;
		private System.Windows.Forms.NotifyIcon TaskTrayIcon;
		private System.Windows.Forms.ContextMenuStrip TaskTrayIconMenu;
		private System.Windows.Forms.ToolStripMenuItem TaskTrayIconMenu_Item_終了;
	}
}

