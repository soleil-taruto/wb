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
	public partial class EditPCInstanceDlg : Form
	{
		public PCInstanceInfo PCInstance;

		// <---- prm

		public EditPCInstanceDlg()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;
		}

		private void EditPCInstanceDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void EditPCInstanceDlg_Shown(object sender, EventArgs e)
		{
			this.InstanceName.Text = this.PCInstance.Name;
			this.DisplayName.Text = this.PCInstance.DisplayName;
			this.Radio表示.Checked = this.PCInstance.DisplayFlag;
			this.Radio非表示.Checked = !this.PCInstance.DisplayFlag;

			this.UpdateUI();
		}

		private void EditPCInstanceDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void EditPCInstanceDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.PCInstance.DisplayName = Common.ToPlainString(this.DisplayName.Text);
			this.PCInstance.DisplayFlag = this.Radio表示.Checked;
		}

		private void InstanceName_TextChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void DisplayName_TextChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void Radio表示_CheckedChanged(object sender, EventArgs e)
		{
			this.UpdateUI();
		}

		private void Radio非表示_CheckedChanged(object sender, EventArgs e)
		{
			this.UpdateUI();
		}

		private void UpdateUI()
		{
			this.DisplayName.Enabled = this.Radio表示.Checked;
		}
	}
}
