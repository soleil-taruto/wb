namespace Charlotte
{
	partial class EditorWin
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorWin));
			this.MainTimer = new System.Windows.Forms.Timer(this.components);
			this.SouthBar = new System.Windows.Forms.StatusStrip();
			this.NorthBar = new System.Windows.Forms.MenuStrip();
			this.StatusBar = new System.Windows.Forms.ToolStripStatusLabel();
			this.SubStatusBar = new System.Windows.Forms.ToolStripStatusLabel();
			this.アプリToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.保存して閉じるToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.閉じるToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MainSheet = new System.Windows.Forms.DataGridView();
			this.SouthBar.SuspendLayout();
			this.NorthBar.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MainSheet)).BeginInit();
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
			this.SouthBar.TabIndex = 0;
			this.SouthBar.Text = "statusStrip1";
			// 
			// NorthBar
			// 
			this.NorthBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.アプリToolStripMenuItem});
			this.NorthBar.Location = new System.Drawing.Point(0, 0);
			this.NorthBar.Name = "NorthBar";
			this.NorthBar.Size = new System.Drawing.Size(784, 24);
			this.NorthBar.TabIndex = 1;
			this.NorthBar.Text = "menuStrip1";
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
			// アプリToolStripMenuItem
			// 
			this.アプリToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.保存ToolStripMenuItem,
            this.保存して閉じるToolStripMenuItem,
            this.閉じるToolStripMenuItem});
			this.アプリToolStripMenuItem.Name = "アプリToolStripMenuItem";
			this.アプリToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
			this.アプリToolStripMenuItem.Text = "アプリ";
			// 
			// 保存ToolStripMenuItem
			// 
			this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
			this.保存ToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.保存ToolStripMenuItem.Text = "保存";
			this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
			// 
			// 保存して閉じるToolStripMenuItem
			// 
			this.保存して閉じるToolStripMenuItem.Name = "保存して閉じるToolStripMenuItem";
			this.保存して閉じるToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.保存して閉じるToolStripMenuItem.Text = "保存して閉じる";
			this.保存して閉じるToolStripMenuItem.Click += new System.EventHandler(this.保存して閉じるToolStripMenuItem_Click);
			// 
			// 閉じるToolStripMenuItem
			// 
			this.閉じるToolStripMenuItem.Name = "閉じるToolStripMenuItem";
			this.閉じるToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
			this.閉じるToolStripMenuItem.Text = "保存しないで閉じる";
			this.閉じるToolStripMenuItem.Click += new System.EventHandler(this.閉じるToolStripMenuItem_Click);
			// 
			// MainSheet
			// 
			this.MainSheet.AllowUserToAddRows = false;
			this.MainSheet.AllowUserToDeleteRows = false;
			this.MainSheet.AllowUserToResizeColumns = false;
			this.MainSheet.AllowUserToResizeRows = false;
			this.MainSheet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainSheet.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.MainSheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.MainSheet.ColumnHeadersVisible = false;
			this.MainSheet.Location = new System.Drawing.Point(12, 27);
			this.MainSheet.Name = "MainSheet";
			this.MainSheet.ReadOnly = true;
			this.MainSheet.RowHeadersVisible = false;
			this.MainSheet.RowTemplate.Height = 21;
			this.MainSheet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.MainSheet.Size = new System.Drawing.Size(760, 509);
			this.MainSheet.TabIndex = 2;
			this.MainSheet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MainSheet_CellContentClick);
			// 
			// EditorWin
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 561);
			this.Controls.Add(this.MainSheet);
			this.Controls.Add(this.SouthBar);
			this.Controls.Add(this.NorthBar);
			this.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.NorthBar;
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "EditorWin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "EditorWin";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditorWin_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EditorWin_FormClosed);
			this.Load += new System.EventHandler(this.EditorWin_Load);
			this.Shown += new System.EventHandler(this.EditorWin_Shown);
			this.SouthBar.ResumeLayout(false);
			this.SouthBar.PerformLayout();
			this.NorthBar.ResumeLayout(false);
			this.NorthBar.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.MainSheet)).EndInit();
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
		private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 保存して閉じるToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 閉じるToolStripMenuItem;
		private System.Windows.Forms.DataGridView MainSheet;
	}
}