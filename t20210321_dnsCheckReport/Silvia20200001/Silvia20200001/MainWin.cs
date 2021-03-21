using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Security.Permissions;
using System.Windows.Forms;
using Charlotte.Commons;

namespace Charlotte
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
			{
				this.Acts.Add(() =>
				{
					this.CloseWindow();
					return false;
				});

				return;
			}
			base.WndProc(ref m);
		}

		#endregion

		public MainWin()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;

			this.ConsoleOut.Text = "";
			this.StatusBar.Text = "";
			this.SubStatusBar.Text = "";
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			// init console log
			{
				string logFile = ProcMain.SelfFile + "-console.log";

				File.WriteAllBytes(logFile, SCommon.EMPTY_BYTES);

				ProcMain.WriteLog = message =>
				{
					string line = "[" + DateTime.Now + "] " + message;

					for (int tryCount = 1; ; tryCount++)
					{
						try
						{
							File.AppendAllLines(logFile, new string[] { line }, Encoding.UTF8);
							return;
						}
						catch
						{ }

						if (3 <= tryCount)
							break;

						Thread.Sleep(50);
					}
				};
			}

			Ground.I = new Ground();
			Ground.I.LoadSetting();
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			this.Resized();
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// none
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			// none
		}

		private void CloseWindow()
		{
			try
			{
				Ground.I.SaveSetting();
			}
			catch (Exception ex)
			{
				ProcMain.WriteLog(ex);
			}
			this.Close();
		}

		private List<Func<bool>> Acts = new List<Func<bool>>();

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			this.Acts.RemoveAll(act =>
			{
				try
				{
					return !act();
				}
				catch (Exception ex)
				{
					ProcMain.WriteLog(ex);
					return true;
				}
			});
		}

		private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.CloseWindow();
		}

		private void マップファイルを開くToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string file = SaveLoadDialogs.LoadFile("マップファイルを開く", "マップファイル:txt", Ground.I.MapFile);

				if (file != null)
				{
					file = SCommon.MakeFullPath(file);
					Ground.I.MapFile = file;

					this.MapFileOpened(Ground.I.MapFile);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, "" + ex, "失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void OpenCsvDataMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (SaveLoadDialogs.SelectFolder(ref Ground.I.CsvDataDir, "Platinum - マップデータを開く"))
				{
					Ground.I.CsvDataDir = SCommon.MakeFullPath(Ground.I.CsvDataDir);

					this.CsvDataDirOpened(Ground.I.CsvDataDir);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, "" + ex, "失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void DropPanel_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Copy;
		}

		private void DropPanel_DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				string path = ((string[])e.Data.GetData(DataFormats.FileDrop, false))[0];

				path = SCommon.MakeFullPath(path);

				if (File.Exists(path))
				{
					if (SCommon.EqualsIgnoreCase(Path.GetExtension(path), Consts.MAP_EXT))
					{
						this.MapFileOpened(path);
					}
					else if (SCommon.EqualsIgnoreCase(Path.GetFileName(path), Consts.CSV_DATA_LOCAL_FILE))
					{
						this.CsvDataDirOpened(Path.GetDirectoryName(path));
					}
				}
				else if (Directory.Exists(path))
				{
					this.CsvDataDirOpened(path);
				}
				else
				{
					throw new Exception("not file or directory");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, "" + ex, "失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void MainWin_ResizeBegin(object sender, EventArgs e)
		{
			//this.Resized(); // -> SizeChanged
		}

		private void MainWin_Resize(object sender, EventArgs e)
		{
			//this.Resized(); // -> SizeChanged
		}

		private void MainWin_ResizeEnd(object sender, EventArgs e)
		{
			//this.Resized(); // -> SizeChanged
		}

		private void MainWin_SizeChanged(object sender, EventArgs e)
		{
			this.Resized();
		}

		private void Resized()
		{
			this.DropLabel.Left = (this.DropPanel.Width - this.DropLabel.Width) / 2;
			this.DropLabel.Top = (this.DropPanel.Height - this.DropLabel.Height) / 2;
		}

		private void DropPanel_Paint(object sender, PaintEventArgs e)
		{
			// none
		}

		private void DropLabel_Click(object sender, EventArgs e)
		{
			// none
		}

		private void MapFileOpened(string mapFile)
		{
			string csvDataDir = Path.Combine(Path.GetDirectoryName(mapFile), Path.GetFileNameWithoutExtension(mapFile));

			if (MessageBox.Show(
				this,
				"マップファイルから Plutinum 用マップデータを作成します。\r\n" +
				"入力：" + mapFile + "\r\n" +
				"出力：" + csvDataDir,
				"確認",
				MessageBoxButtons.OKCancel,
				MessageBoxIcon.Information
				)
				!= DialogResult.OK
				)
				return;

			if (File.Exists(csvDataDir) || Directory.Exists(csvDataDir))
			{
				if (MessageBox.Show(
					this,
					csvDataDir + " を上書きします。",
					"確認",
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Warning
					)
					!= DialogResult.OK
					)
					return;
			}
			string[] outLines = MapConverterCUI.MapFileToPlatinumCsvData(mapFile, csvDataDir);

			this.ConsoleOut.Text = string.Join("\r\n", outLines);
			this.ConsoleOut.ScrollToCaret();
		}

		private void CsvDataDirOpened(string csvDataDir)
		{
			string mapFile = csvDataDir + Consts.MAP_EXT;

			if (MessageBox.Show(
				this,
				"Plutinum 用マップデータからマップファイルを作成します。\r\n" +
				"入力：" + csvDataDir + "\r\n" +
				"出力：" + mapFile,
				"確認",
				MessageBoxButtons.OKCancel,
				MessageBoxIcon.Information
				)
				!= DialogResult.OK
				)
				return;

			if (File.Exists(mapFile) || Directory.Exists(mapFile))
			{
				if (MessageBox.Show(
					this,
					mapFile + " を上書きします。",
					"確認",
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Warning
					)
					!= DialogResult.OK
					)
					return;
			}
			string[] outLines = MapConverterCUI.PlatinumCsvDataToMapFile(csvDataDir, mapFile);

			this.ConsoleOut.Text = string.Join("\r\n", outLines);
			this.ConsoleOut.ScrollToCaret();
		}
	}
}
