using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;

namespace AccessLamp
{
	public partial class MainWin : Form
	{
		#region ALT_F4 抑止

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x112;
			const long SC_CLOSE = 0xF060L;

			if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE)
				return;

			base.WndProc(ref m);
		}

		#endregion

		public MainWin()
		{
			InitializeComponent();
		}

		private Icon IconIdle;
		private Icon IconR;
		private Icon IconW;
		private Icon IconRW;
		private Icon IconBusyR;
		private Icon IconBusyW;
		private Icon IconBusyRW;
		public List<PerfCntrInfo> PerfCntrList = new List<PerfCntrInfo>();

		private static string GetIconFile(string localFile)
		{
			if (File.Exists(localFile))
				return localFile;

			try
			{
				string selfFile = Assembly.GetEntryAssembly().Location;
				string selfDir = Path.GetDirectoryName(selfFile);
				string selfDirFile = Path.Combine(selfDir, localFile);

				if (File.Exists(selfDirFile))
					return selfDirFile;
			}
			catch
			{ }

			return Path.Combine("..\\..\\..\\..\\doc", localFile);
		}

		private static Icon LoadIcon(string localFile)
		{
			try
			{
				return new Icon(GetIconFile(localFile));
			}
			catch
			{
				MessageBox.Show(
					"アイコンファイル '" + localFile + "' の読み込みに失敗しました。\n" +
					"作業フォルダまたは実行ファイルと同じフォルダにアイコンファイルが存在することを確認して下さい。",
					"AccessLamp Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);

				return SystemIcons.Error;
			}
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			this.IconIdle = LoadIcon("Idle.ico");
			this.IconR = LoadIcon("Read.ico");
			this.IconW = LoadIcon("Write.ico");
			this.IconRW = LoadIcon("ReadWrite.ico");
			this.IconBusyR = LoadIcon("BusyRead.ico");
			this.IconBusyW = LoadIcon("BusyWrite.ico");
			this.IconBusyRW = LoadIcon("BusyReadWrite.ico");

			{
				Exception firstEx = null;
				List<char> drvs = new List<char>();

				try
				{
					string[] args = Environment.GetCommandLineArgs();

					for (int index = 1; index < args.Length; index++)
					{
						foreach (char chr in args[index].ToUpper())
						{
							if (Common.ALPHA.Contains(chr))
							{
								drvs.Add(chr);
							}
						}
					}
				}
				catch
				{ }

				if (drvs.Count == 0)
				{
					foreach (char drv in Common.ALPHA)
					{
						DriveInfo di = new DriveInfo("" + drv);

						if (di.DriveType == DriveType.Fixed)
						{
							drvs.Add(drv);
						}
					}
				}
				Common.ToUnique(drvs);

				foreach (char drv in drvs)
				{
					try
					{
						PerformanceCounter r = new PerformanceCounter("LogicalDisk", "Disk Read Bytes/sec", drv + ":");
						PerformanceCounter w = new PerformanceCounter("LogicalDisk", "Disk Write Bytes/sec", drv + ":");

						PerfCntrInfo info = new PerfCntrInfo();

						info.Drv = drv;
						info.R = r;
						info.W = w;

						this.PerfCntrList.Add(info);
					}
					catch (Exception ex)
					{
						if (firstEx == null)
							firstEx = ex;
					}
				}
				if (this.PerfCntrList.Count == 0)
				{
					string sDrv = new string(drvs.ToArray());

					MessageBox.Show(
						"パフォーマンスカウンタのオープンに失敗しました。\ndrv: " + sDrv + "\nex: " + firstEx,
						"AccessLamp Error",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
						);
				}
			}

			GC.Collect();
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			this.MT_Enabled = true;
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.MT_Enabled = false;
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MT_Enabled = false;

			foreach (PerfCntrInfo info in this.PerfCntrList)
			{
				info.Close();
			}
		}

		private int Chain_R;
		private int Chain_W;

		private bool ChainFltr(bool f, ref int c)
		{
			const int C_MAX = 3;

			if (f)
			{
				if (c < C_MAX)
					c++;
			}
			else
			{
				if (0 < c)
					c--;
			}
			return c == C_MAX;
		}

		private bool MT_Enabled;
		private bool MT_Busy;
		private long MT_Count;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MT_Enabled == false || this.MT_Busy)
				return;

			this.MT_Busy = true;

			int currPCPos = -1;

			try
			{
				bool rf = false;
				bool wf = false;

				for (int index = 0; index < this.PerfCntrList.Count; index++)
				{
					currPCPos = index;

					if (0f < this.PerfCntrList[index].R.NextValue())
						rf = true;

					if (0f < this.PerfCntrList[index].W.NextValue())
						wf = true;

					currPCPos = -1;
					this.PerfCntrList[index].ErrorCount = 0;
				}

				bool sf =
					this.ChainFltr(rf, ref this.Chain_R) ||
					this.ChainFltr(wf, ref this.Chain_W);

				Icon nextIcon = this.IconIdle;

				if (sf)
				{
					if (rf)
					{
						if (wf)
							nextIcon = this.IconBusyRW;
						else
							nextIcon = this.IconBusyR;
					}
					else // (sf && !rf) なら (wf) であるはず。
					{
						nextIcon = this.IconBusyW;
					}
				}
				else
				{
					if (rf)
					{
						if (wf)
							nextIcon = this.IconRW;
						else
							nextIcon = this.IconR;
					}
					else if (wf)
					{
						nextIcon = this.IconW;
					}
				}

				////if (this.TaskTrayIcon.Icon != nextIcon)
				////    this.TaskTrayIcon.Icon = nextIcon;

				if (this.MT_Count % 6000 == 0) // 10分毎に実行
				{
					GC.Collect();
				}
				if (this.MT_Count % 30 == 0) // 3秒毎に実行
				{
					if (Ground.Ev停止.WaitOne(0))
					{
						this.MT_Enabled = false;
						this.Close();
						return;
					}
				}
			}
			catch (Exception ex)
			{
				if (currPCPos == -1)
					throw ex;

				this.PerfCntrList[currPCPos].ErrorCount++;

				if (this.PerfCntrList[currPCPos].ErrorCount < 5) // < 0.5[sec]
					return;

				this.PerfCntrList[currPCPos].Close();
				this.PerfCntrList.RemoveAt(currPCPos);
			}
			finally
			{
				this.MT_Busy = false;
				this.MT_Count++;
			}
		}
	}
}
