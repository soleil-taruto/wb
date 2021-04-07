﻿using System;
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
		// ALT_F4 抑止 -- 不要

		public MainWin()
		{
			InitializeComponent();
		}

		private void UnloadPerfCntrList()
		{
			Ground.ReadPerfCntrList.ForEach(perfCntr => perfCntr.Dispose());
			Ground.ReadPerfCntrList.Clear();

			Ground.WritePerfCntrList.ForEach(perfCntr => perfCntr.Dispose());
			Ground.WritePerfCntrList.Clear();
		}

		private void LoadPerfCntrList()
		{
			this.UnloadPerfCntrList();

			foreach (PCInstanceInfo instance in Ground.Setting.PCInstances)
			{
				PerformanceCounter r = null;
				PerformanceCounter w = null;

				try
				{
					r = new PerformanceCounter("LogicalDisk", "Disk Read Bytes/sec", instance.Name);
				}
				catch
				{ }

				try
				{
					w = new PerformanceCounter("LogicalDisk", "Disk Write Bytes/sec", instance.Name);
				}
				catch
				{ }

				Ground.ReadPerfCntrList.Add(new PerfCntrInfo(r));
				Ground.WritePerfCntrList.Add(new PerfCntrInfo(w));
			}
		}

		private List<Label> Lamps = new List<Label>();

		private void UnloadUIControl(Control control)
		{
			try
			{
				this.Controls.Remove(control);
			}
			catch
			{ }

			try
			{
				control.Dispose();
			}
			catch
			{ }
		}

		private void UnloadUIControls()
		{
			this.Lamps.ForEach(lamp => this.UnloadUIControl(lamp));
			this.Lamps.Clear();
		}

		private void PostControlAdded(Control control)
		{
			control.MouseDown += this.MainWin_MouseDown;
			control.MouseMove += this.MainWin_MouseMove;
		}

		private void LoadUIControls()
		{
			this.UnloadUIControls();

			const int MARGIN = 5;
			const int W_EMPTY = 50;
			const int H_EMPTY = 50;

			int perfCntrNum = Ground.ReadPerfCntrList.Count;

			for (int index = 0; index < perfCntrNum; index++)
			{
				Label lamp;

				lamp = new Label();
				lamp.Left = MARGIN + this.Lamps.Select(v => v.Width + MARGIN).Sum();
				lamp.Top = MARGIN;
				//lamp.Width = 100;
				//lamp.Height = 100;
				//lamp.ForeColor = Color.White;
				//lamp.BackColor = Color.Gray;
				lamp.Font = Ground.Setting.FontSetting.GetFont();
				lamp.AutoSize = true;
				lamp.Text = Ground.Setting.PCInstances[index].DisplayName;
				this.Controls.Add(lamp);
				this.Lamps.Add(lamp);
				this.PostControlAdded(lamp);
			}

			if (1 <= this.Lamps.Count)
			{
				this.Width = MARGIN + this.Lamps.Select(v => v.Width + MARGIN).Sum();
				this.Height = MARGIN + this.Lamps[0].Height + MARGIN;
			}
			else
			{
				this.Width = W_EMPTY;
				this.Height = H_EMPTY;
			}

			this.BackColor = Ground.Setting.BackgroundColor;
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			this.LoadPerfCntrList();
			this.LoadUIControls();
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			this.Left = Ground.Setting.MainWin_L;
			this.Top = Ground.Setting.MainWin_T;

			GC.Collect();

			this.MT_Enabled = true;
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.MT_Enabled = false;
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MT_Enabled = false;

			Ground.Setting.MainWin_L = this.Left;
			Ground.Setting.MainWin_T = this.Top;

			this.UnloadPerfCntrList();
		}

		private bool MT_Enabled;
		private bool MT_Busy;
		private long MT_Count;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MT_Enabled == false || this.MT_Busy)
				return;

			PerfCntrInfo currPerfCntr = null;

			this.MT_Busy = true;
			try
			{
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
					if (Ground.SaveSettingRequest)
					{
						Ground.Setting.SaveToFile();
						Ground.SaveSettingRequest = false;
					}
				}

				// <--- UpdateLamp で落ちるかもしれないので、先に実行

				int perfCntrNum = Ground.ReadPerfCntrList.Count;

				for (int index = 0; index < perfCntrNum; index++)
				{
					PerfCntrInfo rPc = Ground.ReadPerfCntrList[index];
					PerfCntrInfo wPc = Ground.WritePerfCntrList[index];

					currPerfCntr = rPc;
					currPerfCntr.Update();
					currPerfCntr = wPc;
					currPerfCntr.Update();
					currPerfCntr = null;

					PerfCntrInfo.Status_e rSt = rPc.GetStatus();
					PerfCntrInfo.Status_e wSt = wPc.GetStatus();

					Color backColor;
					Color foreColor;

					if (rSt == PerfCntrInfo.Status_e.DENIED || wSt == PerfCntrInfo.Status_e.DENIED)
					{
						backColor = Ground.Setting.DeniedBackColor;
						foreColor = Ground.Setting.DeniedForeColor;
					}
					else if (rSt == PerfCntrInfo.Status_e.VERY_BUSY || wSt == PerfCntrInfo.Status_e.VERY_BUSY)
					{
						backColor = Ground.Setting.VeryBusyBackColor;
						foreColor = Ground.Setting.VeryBusyForeColor;
					}
					else if (rSt == PerfCntrInfo.Status_e.BUSY || wSt == PerfCntrInfo.Status_e.BUSY)
					{
						backColor = Ground.Setting.BusyBackColor;
						foreColor = Ground.Setting.BusyForeColor;
					}
					else
					{
						backColor = Ground.Setting.IdleBackColor;
						foreColor = Ground.Setting.IdleForeColor;
					}

					Label monitorLabel = this.Lamps[index];

					if (monitorLabel.BackColor != backColor)
						monitorLabel.BackColor = backColor;

					if (monitorLabel.ForeColor != foreColor)
						monitorLabel.ForeColor = foreColor;
				}
			}
			catch
			{
				if (currPerfCntr != null)
				{
					if (currPerfCntr.ErrorCount < PerfCntrInfo.ERROR_COUNT_MAX)
						currPerfCntr.ErrorCount++;
					else
						currPerfCntr.Dispose();
				}
			}
			finally
			{
				this.MT_Busy = false;
				this.MT_Count++;
			}
		}

		private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.MT_Enabled = false;
			this.Close();
		}

		private void 設定ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.MT_Enabled = false;
			this.Visible = false;

			using (SettingDlg f = new SettingDlg())
			{
				f.ShowDialog();
			}

			this.Visible = true;

			this.LoadPerfCntrList();
			this.LoadUIControls();

			Ground.Setting.SaveToFile();

			GC.Collect();

			this.MT_Enabled = true;
		}

		private int MouseDown_X;
		private int MouseDown_Y;

		private void MainWin_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.MouseDown_X = e.X;
				this.MouseDown_Y = e.Y;
			}
		}

		private void MainWin_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.Location = new Point(
					this.Location.X + e.X - this.MouseDown_X,
					this.Location.Y + e.Y - this.MouseDown_Y
					);

				Ground.Setting.MainWin_L = this.Location.X;
				Ground.Setting.MainWin_T = this.Location.Y;
				Ground.SaveSettingRequest = true;
			}
		}
	}
}
