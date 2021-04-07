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
		// ALT_F4 抑止 -- 不要

		public MainWin()
		{
			InitializeComponent();
		}

		private static Color DeniedColor = Color.FromArgb(195, 195, 195);
		private static Color IdleColor = Color.FromArgb(155, 220, 235);
		private static Color BusyColor = Color.FromArgb(255, 200, 15);
		private static Color VeryBusyColor = Color.FromArgb(255, 30, 30);

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

			foreach (string instanceName in Ground.Setting.InstanceNames)
			{
				PerformanceCounter r = null;
				PerformanceCounter w = null;

				try
				{
					r = new PerformanceCounter("LogicalDisk", "Disk Read Bytes/sec", instanceName);
				}
				catch
				{ }

				try
				{
					w = new PerformanceCounter("LogicalDisk", "Disk Write Bytes/sec", instanceName);
				}
				catch
				{ }

				Ground.ReadPerfCntrList.Add(new PerfCntrInfo(r));
				Ground.WritePerfCntrList.Add(new PerfCntrInfo(w));
			}
		}

		private List<Label> MonitorLabels = new List<Label>();
		private List<Panel> ReadLamps = new List<Panel>();
		private List<Panel> WriteLamps = new List<Panel>();

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
			this.MonitorLabels.ForEach(label => this.UnloadUIControl(label));
			this.MonitorLabels.Clear();

			this.ReadLamps.ForEach(lamp => this.UnloadUIControl(lamp));
			this.ReadLamps.Clear();

			this.WriteLamps.ForEach(lamp => this.UnloadUIControl(lamp));
			this.WriteLamps.Clear();
		}

		private void PostControlAdded(Control control)
		{
			control.MouseDown += this.MainWin_MouseDown;
			control.MouseMove += this.MainWin_MouseMove;
		}

		private void LoadUIControls()
		{
			this.UnloadUIControls();

			const int MARGIN = 10;
			const int LABEL_H = 20;
			const int LABEL_LEN_MAX = 10;

			int perfCntrNum = Ground.ReadPerfCntrList.Count;

			for (int index = 0; index < perfCntrNum; index++)
			{
				Label label;

				label = new Label();
				label.Left = MARGIN + (Consts.LAMP_W + MARGIN) * index;
				label.Top = MARGIN;
				label.Width = Consts.LAMP_W;
				label.Height = LABEL_H;
				label.ForeColor = Color.White;
				label.Text = Common.CutTrail(Ground.Setting.InstanceNames[index], LABEL_LEN_MAX);
				this.Controls.Add(label);
				this.MonitorLabels.Add(label);
				this.PostControlAdded(label);

				Panel lamp;

				lamp = new Panel();
				lamp.Left = MARGIN + (Consts.LAMP_W + MARGIN) * index;
				lamp.Top = MARGIN + LABEL_H + MARGIN;
				lamp.Width = Consts.LAMP_W;
				lamp.Height = Consts.LAMP_H;
				lamp.BackColor = DeniedColor;
				this.Controls.Add(lamp);
				this.ReadLamps.Add(lamp);
				this.PostControlAdded(lamp);

				lamp = new Panel();
				lamp.Left = MARGIN + (Consts.LAMP_W + MARGIN) * index;
				lamp.Top = MARGIN + LABEL_H + MARGIN + Consts.LAMP_H + MARGIN;
				lamp.Width = Consts.LAMP_W;
				lamp.Height = Consts.LAMP_H;
				lamp.BackColor = DeniedColor;
				this.Controls.Add(lamp);
				this.WriteLamps.Add(lamp);
				this.PostControlAdded(lamp);
			}
			this.Width = MARGIN + (Consts.LAMP_W + MARGIN) * Math.Max(perfCntrNum, 1);
			this.Height = MARGIN + LABEL_H + MARGIN + (Consts.LAMP_H + MARGIN) * 2;
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

			this.MT_Busy = true;
			try
			{
				int perfCntrNum = Ground.ReadPerfCntrList.Count;

				for (int index = 0; index < perfCntrNum; index++)
				{
					this.UpdateLamp(Ground.ReadPerfCntrList[index], this.ReadLamps[index]);
					this.UpdateLamp(Ground.WritePerfCntrList[index], this.WriteLamps[index]);
				}
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
			}
			finally
			{
				this.MT_Busy = false;
				this.MT_Count++;
			}
		}

		private void UpdateLamp(PerfCntrInfo perfCntr, Panel lamp)
		{
			perfCntr.Update();
			perfCntr.ErrorCount = 0; // 成功したのでエラーカウント_クリア

			Color bmp;

			switch (perfCntr.GetStatus())
			{
				case PerfCntrInfo.Status_e.DENIED: bmp = DeniedColor; break;
				case PerfCntrInfo.Status_e.IDLE: bmp = IdleColor; break;
				case PerfCntrInfo.Status_e.BUSY: bmp = BusyColor; break;
				case PerfCntrInfo.Status_e.VERY_BUSY: bmp = VeryBusyColor; break;

				default:
					throw null; // never
			}
			if (lamp.BackColor != bmp)
				lamp.BackColor = bmp;
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
