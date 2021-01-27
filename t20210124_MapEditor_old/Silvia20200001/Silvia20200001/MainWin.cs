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
			Ground.I.TileCatalog.Load();
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
				// none
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
				string file = SaveLoadDialogs.LoadFile(
					"マップファイルを開く",
					"マップファイル:txt",
					Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "マップファイル.txt")
					);

				if (file != null)
				{
					Map map = Map.Load(file);
					EditorWin editor = new EditorWin(map);
					editor.Show();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("" + ex, "失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

				if (10 <= files.Length) // ? なんか多い
				{
					if (MessageBox.Show(
						string.Format("{0} 件のファイルを処理します。", files.Length),
						"ファイル数ちょっと多くね？",
						MessageBoxButtons.OKCancel,
						MessageBoxIcon.Information
						)
						!= DialogResult.OK
						)
						return;
				}

#if true
				Map[] maps = new Map[files.Length];

				for (int index = 0; index < files.Length; index++)
				{
					string file = files[index];
					Map map = Map.Load(file);
					maps[index] = map;
				}
				foreach (Map map in maps)
				{
					EditorWin editor = new EditorWin(map);
					editor.Show();
				}
#elif true // old
				foreach (Map map in files.Select(file => Map.Load(file)).ToList())
				//foreach (Map map in files.Select(file => Map.Load(file)))
				{
					EditorWin editor = new EditorWin(map);
					editor.Show();
				}
#else // old
				foreach (string file in files)
				{
					Map map = Map.Load(file);
					EditorWin editor = new EditorWin(map);
					editor.Show();
				}
#endif
			}
			catch (Exception ex)
			{
				MessageBox.Show("" + ex, "失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

		private void タイルフォルダを再設定するToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string bkTilesDir = Ground.I.TilesDir;
			try
			{
				if (1 <= Ground.I.Editors.Count)
				{
					MessageBox.Show(
						"編集ウィンドウを全て閉じて下さい。",
						"処理できません",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information
						);
					return;
				}
				if (SaveLoadDialogs.SelectFolder(ref Ground.I.TilesDir, "タイルフォルダを開く"))
				{
					Ground.I.TileCatalog.Load();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("" + ex, "失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);

				Ground.I.TilesDir = bkTilesDir;
				Ground.I.TileCatalog.Load(); // 失敗を想定しない。
			}
		}
	}
}
