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
using Charlotte.Maps;

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
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			this.ResizeLeftSheets();
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void CloseWindow()
		{
			try
			{
				// test
				//if (MessageBox.Show(
				//    "アプリケーションを終了して宜しいですか？",
				//    "終了確認",
				//    MessageBoxButtons.YesNo,
				//    MessageBoxIcon.Question
				//    )
				//    != System.Windows.Forms.DialogResult.Yes
				//    )
				//    return;
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

		private void TileSheet_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			// noop
		}

		private void MapSheet_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			// noop
		}

		private void LoadMapFileMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string file = SaveLoadDialogs.LoadFile(
					"マップファイルを開く",
					"マップファイル:txt",
					//Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "マップデータ.txt")
					"C:\\マップデータ.txt"
					);

				if (file != null)
				{
					Ground.I.Map = new Map();
					Ground.I.Map.Load(file);

					this.LoadSheets();
				}
			}
			catch (Exception ex)
			{
				Ground.I.Map = null;

				this.ClearSheets();

				MessageBox.Show("" + ex, "失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void SaveMapFileMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				Ground.I.Map.Save();
			}
			catch (Exception ex)
			{
				MessageBox.Show("" + ex, "失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void SaveMapFileAsMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string file = SaveLoadDialogs.SaveFile(
					"マップファイルを保存する",
					"txt",
					//Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "マップデータ.txt")
					"C:\\マップデータ.txt"
					);

				if (file != null)
				{
					Ground.I.Map.MapFile = file;
					Ground.I.Map.Save();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("" + ex, "失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void MainWin_ResizeBegin(object sender, EventArgs e)
		{
			//this.ResizeLeftSheets(); // -> SizeChanged
		}

		private void MainWin_Resize(object sender, EventArgs e)
		{
			//this.ResizeLeftSheets(); // -> SizeChanged
		}

		private void MainWin_ResizeEnd(object sender, EventArgs e)
		{
			//this.ResizeLeftSheets(); // -> SizeChanged
		}

		private void MainWin_SizeChanged(object sender, EventArgs e)
		{
			this.ResizeLeftSheets();
		}

		private void ResizeLeftSheets()
		{
			const int MARGIN = 5;

			int h = this.MapSheet.Height;

			h -= MARGIN;
			h /= 2;

			this.TileSheet.Height = h;
			this.EnemySheet.Top = this.TileSheet.Top + h + MARGIN;
			this.EnemySheet.Height = h;
		}

		private void ClearSheets()
		{
			this.TileSheet.RowCount = 0;
			this.TileSheet.ColumnCount = 0;

			this.EnemySheet.RowCount = 0;
			this.EnemySheet.ColumnCount = 0;

			this.MapSheet.RowCount = 0;
			this.MapSheet.ColumnCount = 0;

			this.PalletSheet.RowCount = 0;
			this.PalletSheet.ColumnCount = 0;
		}

		private void LoadSheets()
		{
			this.ClearSheets();

			// TODO
		}

		private void SaveSheets()
		{
			this.ClearSheets();

			// TODO
		}
	}
}
