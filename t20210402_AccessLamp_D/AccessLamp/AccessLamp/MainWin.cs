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

		private Bitmap DeniedPicture;
		private Bitmap IdlePicture;
		private Bitmap BusyPicture;
		private Bitmap VeryBusyPicture;

		public List<PerfCntrInfo> PerfCntrList = new List<PerfCntrInfo>();

		private static string GetResourceFile(string localFile)
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

		private static Bitmap LoadPicture(string localFile)
		{
			try
			{
				return new Bitmap(GetResourceFile(localFile));
			}
			catch
			{
				MessageBox.Show(
					"画像ファイル '" + localFile + "' の読み込みに失敗しました。\n" +
					"作業フォルダまたは実行ファイルと同じフォルダに画像ファイルが存在することを確認して下さい。",
					"AccessLamp_D Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);

				return new Bitmap(Consts.PICTURE_W, Consts.PICTURE_H);
			}
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
		private List<PictureBox> ReadPictures = new List<PictureBox>();
		private List<PictureBox> WritePictures = new List<PictureBox>();

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

			this.ReadPictures.ForEach(picBox => this.UnloadUIControl(picBox));
			this.ReadPictures.Clear();

			this.WritePictures.ForEach(picBox => this.UnloadUIControl(picBox));
			this.WritePictures.Clear();
		}

		private void PostControlAdded(Control control)
		{
			control.MouseDown += this.MainWin_MouseDown;
			control.MouseMove += this.MainWin_MouseMove;
			control.MouseUp += this.MainWin_MouseUp;
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
				label.Left = MARGIN + (Consts.PICTURE_W + MARGIN) * index;
				label.Top = MARGIN;
				label.Width = Consts.PICTURE_W;
				label.Height = LABEL_H;
				label.ForeColor = Color.White;
				label.Text = Common.CutTrail(Ground.Setting.InstanceNames[index], LABEL_LEN_MAX);
				this.Controls.Add(label);
				this.MonitorLabels.Add(label);
				this.PostControlAdded(label);

				PictureBox picBox;

				picBox = new PictureBox();
				picBox.Left = MARGIN + (Consts.PICTURE_W + MARGIN) * index;
				picBox.Top = MARGIN + LABEL_H + MARGIN;
				picBox.Width = Consts.PICTURE_W;
				picBox.Height = Consts.PICTURE_H;
				picBox.Image = this.DeniedPicture;
				this.Controls.Add(picBox);
				this.ReadPictures.Add(picBox);
				this.PostControlAdded(picBox);

				picBox = new PictureBox();
				picBox.Left = MARGIN + (Consts.PICTURE_W + MARGIN) * index;
				picBox.Top = MARGIN + LABEL_H + MARGIN + Consts.PICTURE_H + MARGIN;
				picBox.Width = Consts.PICTURE_W;
				picBox.Height = Consts.PICTURE_H;
				picBox.Image = this.DeniedPicture;
				this.Controls.Add(picBox);
				this.WritePictures.Add(picBox);
				this.PostControlAdded(picBox);
			}
			this.Width = MARGIN + (Consts.PICTURE_W + MARGIN) * Math.Max(perfCntrNum, 1);
			this.Height = MARGIN + LABEL_H + MARGIN + (Consts.PICTURE_H + MARGIN) * 2;
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			this.DeniedPicture = LoadPicture("Denied.png");
			this.IdlePicture = LoadPicture("Idle.png");
			this.BusyPicture = LoadPicture("Busy.png");
			this.VeryBusyPicture = LoadPicture("VeryBusy.png");

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
					this.UpdatePicture(Ground.ReadPerfCntrList[index], this.ReadPictures[index]);
					this.UpdatePicture(Ground.WritePerfCntrList[index], this.WritePictures[index]);
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
				}
			}
			finally
			{
				this.MT_Busy = false;
				this.MT_Count++;
			}
		}

		private void UpdatePicture(PerfCntrInfo perfCntr, PictureBox picBox)
		{
			perfCntr.Update();

			Bitmap bmp;

			switch (perfCntr.GetStatus())
			{
				case PerfCntrInfo.Status_e.DENIED: bmp = this.DeniedPicture; break;
				case PerfCntrInfo.Status_e.IDLE: bmp = this.IdlePicture; break;
				case PerfCntrInfo.Status_e.BUSY: bmp = this.BusyPicture; break;
				case PerfCntrInfo.Status_e.VERY_BUSY: bmp = this.VeryBusyPicture; break;

				default:
					throw null; // never
			}
			if (picBox.Image != bmp)
				picBox.Image = bmp;
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

		private bool MouseDown_Active;
		private int MouseDown_X;
		private int MouseDown_Y;

		private void MainWin_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.MouseDown_Active = true;
				this.MouseDown_X = e.X;
				this.MouseDown_Y = e.Y;
			}
		}

		private void MainWin_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.MouseDown_Active)
			{
				this.Location = new Point(
					this.Location.X + e.X - this.MouseDown_X,
					this.Location.Y + e.Y - this.MouseDown_Y
					);
			}
		}

		private void MainWin_MouseUp(object sender, MouseEventArgs e)
		{
			this.MouseDown_Active = false;
		}
	}
}
