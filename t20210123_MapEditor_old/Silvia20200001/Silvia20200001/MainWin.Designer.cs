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
			this.LoadMapFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SaveMapFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MapSheet = new System.Windows.Forms.DataGridView();
			this.TileSheet = new System.Windows.Forms.DataGridView();
			this.EnemySheet = new System.Windows.Forms.DataGridView();
			this.PalletSheet = new System.Windows.Forms.DataGridView();
			this.SaveMapFileAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SouthBar.SuspendLayout();
			this.NorthBar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MapSheet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TileSheet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.EnemySheet)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PalletSheet)).BeginInit();
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
			this.SouthBar.Location = new System.Drawing.Point(0, 539);
			this.SouthBar.Name = "SouthBar";
			this.SouthBar.Size = new System.Drawing.Size(784, 22);
			this.SouthBar.TabIndex = 5;
			this.SouthBar.Text = "statusStrip1";
			// 
			// StatusBar
			// 
			this.StatusBar.Name = "StatusBar";
			this.StatusBar.Size = new System.Drawing.Size(693, 17);
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
			this.NorthBar.Size = new System.Drawing.Size(784, 24);
			this.NorthBar.TabIndex = 0;
			this.NorthBar.Text = "menuStrip1";
			// 
			// アプリToolStripMenuItem
			// 
			this.アプリToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadMapFileMenuItem,
            this.SaveMapFileMenuItem,
            this.SaveMapFileAsMenuItem,
            this.toolStripMenuItem1,
            this.終了ToolStripMenuItem});
			this.アプリToolStripMenuItem.Name = "アプリToolStripMenuItem";
			this.アプリToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
			this.アプリToolStripMenuItem.Text = "アプリ";
			// 
			// LoadMapFileMenuItem
			// 
			this.LoadMapFileMenuItem.Name = "LoadMapFileMenuItem";
			this.LoadMapFileMenuItem.Size = new System.Drawing.Size(180, 22);
			this.LoadMapFileMenuItem.Text = "マップファイル読み込み";
			this.LoadMapFileMenuItem.Click += new System.EventHandler(this.LoadMapFileMenuItem_Click);
			// 
			// SaveMapFileMenuItem
			// 
			this.SaveMapFileMenuItem.Name = "SaveMapFileMenuItem";
			this.SaveMapFileMenuItem.Size = new System.Drawing.Size(180, 22);
			this.SaveMapFileMenuItem.Text = "保存";
			this.SaveMapFileMenuItem.Click += new System.EventHandler(this.SaveMapFileMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
			// 
			// 終了ToolStripMenuItem
			// 
			this.終了ToolStripMenuItem.Name = "終了ToolStripMenuItem";
			this.終了ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.終了ToolStripMenuItem.Text = "終了";
			this.終了ToolStripMenuItem.Click += new System.EventHandler(this.終了ToolStripMenuItem_Click);
			// 
			// MapSheet
			// 
			this.MapSheet.AllowUserToAddRows = false;
			this.MapSheet.AllowUserToDeleteRows = false;
			this.MapSheet.AllowUserToResizeColumns = false;
			this.MapSheet.AllowUserToResizeRows = false;
			this.MapSheet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MapSheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.MapSheet.Location = new System.Drawing.Point(218, 27);
			this.MapSheet.Name = "MapSheet";
			this.MapSheet.ReadOnly = true;
			this.MapSheet.RowTemplate.Height = 21;
			this.MapSheet.Size = new System.Drawing.Size(348, 509);
			this.MapSheet.TabIndex = 3;
			this.MapSheet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MapSheet_CellContentClick);
			// 
			// TileSheet
			// 
			this.TileSheet.AllowUserToAddRows = false;
			this.TileSheet.AllowUserToDeleteRows = false;
			this.TileSheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.TileSheet.Location = new System.Drawing.Point(12, 27);
			this.TileSheet.Name = "TileSheet";
			this.TileSheet.ReadOnly = true;
			this.TileSheet.RowTemplate.Height = 21;
			this.TileSheet.Size = new System.Drawing.Size(200, 200);
			this.TileSheet.TabIndex = 1;
			this.TileSheet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TileSheet_CellContentClick);
			// 
			// EnemySheet
			// 
			this.EnemySheet.AllowUserToAddRows = false;
			this.EnemySheet.AllowUserToDeleteRows = false;
			this.EnemySheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.EnemySheet.Location = new System.Drawing.Point(12, 233);
			this.EnemySheet.Name = "EnemySheet";
			this.EnemySheet.ReadOnly = true;
			this.EnemySheet.RowTemplate.Height = 21;
			this.EnemySheet.Size = new System.Drawing.Size(200, 200);
			this.EnemySheet.TabIndex = 2;
			// 
			// PalletSheet
			// 
			this.PalletSheet.AllowUserToAddRows = false;
			this.PalletSheet.AllowUserToDeleteRows = false;
			this.PalletSheet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PalletSheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.PalletSheet.Location = new System.Drawing.Point(572, 27);
			this.PalletSheet.Name = "PalletSheet";
			this.PalletSheet.ReadOnly = true;
			this.PalletSheet.RowTemplate.Height = 21;
			this.PalletSheet.Size = new System.Drawing.Size(200, 509);
			this.PalletSheet.TabIndex = 4;
			// 
			// SaveMapFileAsMenuItem
			// 
			this.SaveMapFileAsMenuItem.Name = "SaveMapFileAsMenuItem";
			this.SaveMapFileAsMenuItem.Size = new System.Drawing.Size(180, 22);
			this.SaveMapFileAsMenuItem.Text = "別のファイル名で保存";
			this.SaveMapFileAsMenuItem.Click += new System.EventHandler(this.SaveMapFileAsMenuItem_Click);
			// 
			// MainWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 561);
			this.Controls.Add(this.PalletSheet);
			this.Controls.Add(this.EnemySheet);
			this.Controls.Add(this.TileSheet);
			this.Controls.Add(this.MapSheet);
			this.Controls.Add(this.SouthBar);
			this.Controls.Add(this.NorthBar);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.NorthBar;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "MainWin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "マップエディタ";
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
			((System.ComponentModel.ISupportInitialize)(this.MapSheet)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TileSheet)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.EnemySheet)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PalletSheet)).EndInit();
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
		private System.Windows.Forms.DataGridView MapSheet;
		private System.Windows.Forms.DataGridView TileSheet;
		private System.Windows.Forms.ToolStripMenuItem LoadMapFileMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SaveMapFileMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.DataGridView EnemySheet;
		private System.Windows.Forms.DataGridView PalletSheet;
		private System.Windows.Forms.ToolStripMenuItem SaveMapFileAsMenuItem;
	}
}

