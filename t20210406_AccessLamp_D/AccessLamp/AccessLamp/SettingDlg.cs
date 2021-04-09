using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AccessLamp
{
	public partial class SettingDlg : Form
	{
		public SettingDlg()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;
		}

		private void SettingDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void SettingDlg_Shown(object sender, EventArgs e)
		{
			foreach (PCInstanceInfo instance in Ground.Setting.PCInstances)
				instance.DisplayFlag = true;

			this.InstanceList.Items.Clear();
			this.InstanceList.Items.AddRange(Ground.Setting.PCInstances);
			this.InstanceList.Items.AddRange(Common.GetInstnaceNames("LogicalDisk")
				.Where(instanceName => !Ground.Setting.PCInstances.Any(v => v.Name == instanceName))
				.Select(instanceName => new PCInstanceInfo(instanceName))
				.ToArray());
			this.InstanceList.ClearSelected();

			this.余白の幅.Value = Ground.Setting.MainWin_余白の幅;

			this.タスクバーにアイコンを表示する.Checked = Ground.Setting.タスクバーにアイコンを表示する;
			this.タスクトレイにアイコンを表示する.Checked = Ground.Setting.タスクトレイにアイコンを表示する;
			this.常に手前に表示する.Checked = Ground.Setting.常に手前に表示する;
			this.位置を固定する.Checked = Ground.Setting.位置を固定する;
			this.ランプを縦に並べる.Checked = Ground.Setting.ランプを縦に並べる;

			this.UpdateUI();
		}

		private void UpdateUI()
		{
			for (int index = 0; index < this.InstanceList.Items.Count; index++)
				this.InstanceList.Items[index] = this.InstanceList.Items[index];

			this.BackgrounSample.BackColor = Ground.Setting.BackgroundColor;
			this.DeniedLampSample.BackColor = Ground.Setting.DeniedBackColor;
			this.DeniedLampSample.ForeColor = Ground.Setting.DeniedForeColor;
			this.IdleLampSample.BackColor = Ground.Setting.IdleBackColor;
			this.IdleLampSample.ForeColor = Ground.Setting.IdleForeColor;
			this.BusyLampSample.BackColor = Ground.Setting.BusyBackColor;
			this.BusyLampSample.ForeColor = Ground.Setting.BusyForeColor;
			this.VeryBusyLampSample.BackColor = Ground.Setting.VeryBusyBackColor;
			this.VeryBusyLampSample.ForeColor = Ground.Setting.VeryBusyForeColor;

			this.FontSettingDisplay.Text = Ground.Setting.FontSetting.ToString();
		}

		private void SettingDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void SettingDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			List<PCInstanceInfo> instances = new List<PCInstanceInfo>();

			for (int index = 0; index < this.InstanceList.Items.Count; index++)
				instances.Add((PCInstanceInfo)this.InstanceList.Items[index]);

			Ground.Setting.PCInstances = instances
				.Where(instance => instance.DisplayFlag)
				.ToArray();

			Ground.Setting.MainWin_余白の幅 = (int)this.余白の幅.Value;

			Ground.Setting.タスクバーにアイコンを表示する = this.タスクバーにアイコンを表示する.Checked;
			Ground.Setting.タスクトレイにアイコンを表示する = this.タスクトレイにアイコンを表示する.Checked;
			Ground.Setting.常に手前に表示する = this.常に手前に表示する.Checked;
			Ground.Setting.位置を固定する = this.位置を固定する.Checked;
			Ground.Setting.ランプを縦に並べる = this.ランプを縦に並べる.Checked;
		}

		private void 選択解除ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.InstanceList.ClearSelected();
		}

		private void SwapInstance(int a, int b)
		{
			object tmp = this.InstanceList.Items[a];
			this.InstanceList.Items[a] = this.InstanceList.Items[b];
			this.InstanceList.Items[b] = tmp;
		}

		private void BtnUp_Click(object sender, EventArgs e)
		{
			int index = this.InstanceList.SelectedIndex;

			if (index != -1 && 1 <= index)
			{
				this.SwapInstance(index, index - 1);
				this.InstanceList.SelectedIndex = index - 1;
			}
		}

		private void BtnDown_Click(object sender, EventArgs e)
		{
			int index = this.InstanceList.SelectedIndex;

			if (index != -1 && index <= this.InstanceList.Items.Count - 2)
			{
				this.SwapInstance(index, index + 1);
				this.InstanceList.SelectedIndex = index + 1;
			}
		}

		private void BtnEdit_Click(object sender, EventArgs e)
		{
			int index = this.InstanceList.SelectedIndex;

			if (index != -1)
			{
				this.Visible = false;

				using (EditPCInstanceDlg f = new EditPCInstanceDlg())
				{
					f.PCInstance = (PCInstanceInfo)this.InstanceList.Items[index];
					f.ShowDialog();
				}
				this.Visible = true; // restore

				this.UpdateUI();
			}
		}

		private void EditColor(ref Color color)
		{
			using (ColorDialog cd = new ColorDialog())
			{
				cd.Color = color;

				if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					color = cd.Color;
					this.UpdateUI();
				}
			}
		}

		private void BtnBackgroundColor_Click(object sender, EventArgs e)
		{
			EditColor(ref Ground.Setting.BackgroundColor);
		}

		private void BtnDeniedBackColor_Click(object sender, EventArgs e)
		{
			EditColor(ref Ground.Setting.DeniedBackColor);
		}

		private void BtnDeniedForeColor_Click(object sender, EventArgs e)
		{
			EditColor(ref Ground.Setting.DeniedForeColor);
		}

		private void BtnIdleBackColor_Click(object sender, EventArgs e)
		{
			EditColor(ref Ground.Setting.IdleBackColor);
		}

		private void BtnIdleForeColor_Click(object sender, EventArgs e)
		{
			EditColor(ref Ground.Setting.IdleForeColor);
		}

		private void BtnBusyBackColor_Click(object sender, EventArgs e)
		{
			EditColor(ref Ground.Setting.BusyBackColor);
		}

		private void BtnBusyForeColor_Click(object sender, EventArgs e)
		{
			EditColor(ref Ground.Setting.BusyForeColor);
		}

		private void BtnVeryBusyBackColor_Click(object sender, EventArgs e)
		{
			EditColor(ref Ground.Setting.VeryBusyBackColor);
		}

		private void BtnVeryBusyForeColor_Click(object sender, EventArgs e)
		{
			EditColor(ref Ground.Setting.VeryBusyForeColor);
		}

		private void MainTab_01_Click(object sender, EventArgs e)
		{
			// noop
		}

		private void MainTab_02_Click(object sender, EventArgs e)
		{
			// noop
		}

		private void MainTab_03_Click(object sender, EventArgs e)
		{
			// noop
		}

		private void 編集ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.BtnEdit_Click(null, null);
		}

		private void 表示ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.ActSelectedPCInstance(instance => instance.DisplayFlag = true);
		}

		private void 非表示ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.ActSelectedPCInstance(instance => instance.DisplayFlag = false);
		}

		private void ActSelectedPCInstance(Action<PCInstanceInfo> action)
		{
			int index = this.InstanceList.SelectedIndex;

			if (index != -1)
			{
				PCInstanceInfo instance = (PCInstanceInfo)this.InstanceList.Items[index];
				action(instance);
				this.UpdateUI();
			}
		}

		private void Btnフォント_Click(object sender, EventArgs e)
		{
			using (FontDialog f = new FontDialog())
			{
				f.Font = Ground.Setting.FontSetting.GetFont();
				f.FontMustExist = true;
				f.ShowColor = false;

				if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					Ground.Setting.FontSetting = new FontSettingInfo(f.Font);
					this.UpdateUI();
				}
			}
		}

		private void 枠の幅_ValueChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void タスクバーにアイコンを表示する_CheckedChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void 常に手前に表示する_CheckedChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void 位置を固定する_CheckedChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void ランプを縦に並べる_CheckedChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void InstanceList_SelectedIndexChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void InstanceList_DoubleClick(object sender, EventArgs e)
		{
			//this.BtnEdit_Click(null, null); // ダブルクリック誤爆の懸念があるため、抑止 // 触った感じ誤爆は無さそうだが、ダブルクリックに対応する必要も無さそうなので、抑止のままとする。
		}
	}
}
