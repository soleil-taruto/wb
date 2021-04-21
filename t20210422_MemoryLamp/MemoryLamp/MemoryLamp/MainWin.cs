using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Windows.Forms;

namespace MemoryLamp
{
	public partial class MainWin : Form
	{
		// ALT_F4 抑止 -- 不要

		public MainWin()
		{
			InitializeComponent();
		}

		private Icon[] MemoryUsageIcons;

		private static Icon LoadIcon(string localFile)
		{
			string file = Path.Combine(Ground.SelfDir, localFile);

			if (!File.Exists(file))
			{
				file = @"..\..\..\..\doc\" + localFile;

				if (!File.Exists(file))
					throw new Exception("アイコンファイル無し：" + file);
			}
			return new Icon(file);
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			this.MemoryUsageIcons = new Icon[]
			{
				LoadIcon("MemoryUsage_0-10_Pct.ico"),
				LoadIcon("MemoryUsage_10-20_Pct.ico"),
				LoadIcon("MemoryUsage_20-30_Pct.ico"),
				LoadIcon("MemoryUsage_30-40_Pct.ico"),
				LoadIcon("MemoryUsage_40-50_Pct.ico"),
				LoadIcon("MemoryUsage_50-60_Pct.ico"),
				LoadIcon("MemoryUsage_60-70_Pct.ico"),
				LoadIcon("MemoryUsage_70-80_Pct.ico"),
				LoadIcon("MemoryUsage_80-90_Pct.ico"),
				LoadIcon("MemoryUsage_90-100_Pct.ico"),
			};

			this.TaskTrayIcon.Icon = this.MemoryUsageIcons[0];
			this.TaskTrayIcon.Text = "準備しています...";
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			this.Visible = false;
			this.TaskTrayIcon.Visible = true;
			this.MT_Enabled = true;
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// none
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MT_Enabled = false;
			this.TaskTrayIcon.Visible = false;
		}

		private bool MT_Enabled;
		//private bool MT_Busy;
		private long MT_Count;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			//if (this.MT_Enabled == false || this.MT_Busy)
			if (this.MT_Enabled == false)
				return;

			//this.MT_Busy = true;

			//if (this.MT_Count % 1 == 0) // 1秒毎に実行
			{
				if (Ground.Ev停止.WaitOne(0))
				{
					this.MT_Enabled = false;
					this.Close();
					return;
				}

				{
					int memoryUsagePct = Win32.GetMemoryUsagePct();
					int memoryUsage = memoryUsagePct / 10;

					memoryUsage = Math.Max(0, memoryUsage);
					memoryUsage = Math.Min(9, memoryUsage);

					Icon icon = this.MemoryUsageIcons[memoryUsage];
					string text = "メモリ使用率 : " + memoryUsagePct + " %";

					if (this.TaskTrayIcon.Icon != icon)
						this.TaskTrayIcon.Icon = icon;

					if (this.TaskTrayIcon.Text != text)
						this.TaskTrayIcon.Text = text;
				}

				if (this.MT_Count % 600 == 0) // 10分毎に実行 // 上位の周期の倍数であること。
				{
					GC.Collect();
				}
			}

			//this.MT_Busy = false;
			this.MT_Count++;
		}

		private void TaskTrayIconMenu_Item_終了_Click(object sender, EventArgs e)
		{
			this.MT_Enabled = false;
			this.Close();
		}
	}
}
