using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using Charlotte.Commons;
using Charlotte.Maps;

namespace Charlotte
{
	public partial class EditorWin : Form
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

		public Map Map { get; private set; }

		public EditorWin(Map map)
		{
			this.Map = map;

			InitializeComponent();

			this.MinimumSize = new Size(300, 300);

			this.StatusBar.Text = "";
			this.SubStatusBar.Text = "";
		}

		private void EditorWin_Load(object sender, EventArgs e)
		{
			Ground.I.Editors.Add(this);
		}

		private void EditorWin_Shown(object sender, EventArgs e)
		{
			this.MS_INIT();
			this.MS_Load();
		}

		private void EditorWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// none
		}

		private void EditorWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			Ground.I.Editors.RemoveAll(editor => editor == this);
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

		private void 閉じるToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(
				"保存しないで閉じます。",
				"確認",
				MessageBoxButtons.OKCancel,
				MessageBoxIcon.Information
				)
				!= DialogResult.OK
				)
				return;

			this.CloseWindow();
		}

		private void 保存して閉じるToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				this.Map.Save();
				this.CloseWindow();
			}
			catch (Exception ex)
			{
				MessageBox.Show("" + ex, "失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				this.Map.Save();
			}
			catch (Exception ex)
			{
				MessageBox.Show("" + ex, "失敗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		#region MainSheet events

		private void MainSheet_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			// none
		}

		private void MS_INIT()
		{
			SCommon.SetEnabledDoubleBuffer(this.MainSheet);
			this.MainSheet.RowTemplate.Height = Consts.TILE_H;
		}

		private void MS_Load()
		{
			this.MainSheet.RowCount = 0;
			this.MainSheet.ColumnCount = 0;

			for (int colidx = 0; colidx < this.Map.W; colidx++)
			{
				DataGridViewImageColumn column = new DataGridViewImageColumn();

				column.Width = Consts.TILE_W;

				this.MainSheet.Columns.Add(column);
			}
			this.MainSheet.RowCount = this.Map.H;

			for (int rowidx = 0; rowidx < this.Map.H; rowidx++)
			{
				DataGridViewRow row = this.MainSheet.Rows[rowidx];

				for (int colidx = 0; colidx < this.Map.W; colidx++)
				{
					row.Cells[colidx].Value = this.Map[colidx, rowidx].Tile.Picture;
				}
			}
		}

		#endregion
	}
}
