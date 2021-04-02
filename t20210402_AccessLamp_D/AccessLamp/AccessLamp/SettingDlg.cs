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
			this.SelectableInstanceNames.Items.Clear();
			this.SelectableInstanceNames.Items.AddRange(Common.GetInstnaceNames("LogicalDisk"));
			this.SelectableInstanceNames.ClearSelected();

			for (int index = 0; index < this.SelectableInstanceNames.Items.Count; index++)
				if (Ground.Setting.InstanceNames.Contains(this.SelectableInstanceNames.Items[index].ToString()))
					this.SelectableInstanceNames.SetSelected(index, true);
		}

		private void SettingDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void SettingDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			List<string> dest = new List<string>();

			for (int index = 0; index < this.SelectableInstanceNames.Items.Count; index++)
				if (this.SelectableInstanceNames.GetSelected(index))
					dest.Add(this.SelectableInstanceNames.Items[index].ToString());

			Ground.Setting.InstanceNames = dest.ToArray();
		}

		private void SelectableInstanceNames_SelectedIndexChanged(object sender, EventArgs e)
		{
			// noop
		}
	}
}
