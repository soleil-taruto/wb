using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace AccessLamp
{
	public class Setting
	{
		private static string SettingFile
		{
			get
			{
				return Path.Combine(Ground.SelfDir, Path.GetFileNameWithoutExtension(Ground.SelfFile)) + ".dat";
			}
		}

		public PCInstanceInfo[] PCInstances = new PCInstanceInfo[] { new PCInstanceInfo() };

		public int MainWin_L = 0;
		public int MainWin_T = 0;

		public int MainWin_余白の幅 = 10;

		public bool タスクバーにアイコンを表示する = true;
		public bool タスクトレイにアイコンを表示する = false;
		public bool 常に手前に表示する = true;
		public bool 位置を固定する = false;
		public bool ランプを縦に並べる = false;

		public Color BackgroundColor = Color.FromArgb(64, 64, 64);
		public Color DeniedBackColor = Color.DarkGray;
		public Color DeniedForeColor = Color.White;
		public Color IdleBackColor = Color.Cyan;
		public Color IdleForeColor = Color.Black;
		public Color BusyBackColor = Color.Orange;
		public Color BusyForeColor = Color.Black;
		public Color VeryBusyBackColor = Color.Red;
		public Color VeryBusyForeColor = Color.Black;

		public FontSettingInfo FontSetting = new FontSettingInfo();

		public void LoadFromFile()
		{
			if (!File.Exists(SettingFile))
				return;

			string[] lines = File.ReadAllLines(SettingFile, Encoding.UTF8);
			int c = 0;

			int instanceNum = int.Parse(lines[c++]);

			{
				List<PCInstanceInfo> dest = new List<PCInstanceInfo>();

				for (int instance_index = 0; instance_index < instanceNum; instance_index++)
				{
					string[] instanceLines = Enumerable
						.Range(0, int.Parse(lines[c++]))
						.Select(dummy => lines[c++])
						.ToArray();

					dest.Add(PCInstanceInfo.Deserialize(instanceLines));
				}
				this.PCInstances = dest.ToArray();
			}

			this.MainWin_L = int.Parse(lines[c++]);
			this.MainWin_T = int.Parse(lines[c++]);

			this.MainWin_余白の幅 = int.Parse(lines[c++]);

			this.タスクバーにアイコンを表示する = int.Parse(lines[c++]) != 0;
			this.タスクトレイにアイコンを表示する = int.Parse(lines[c++]) != 0;
			this.常に手前に表示する = int.Parse(lines[c++]) != 0;
			this.位置を固定する = int.Parse(lines[c++]) != 0;
			this.ランプを縦に並べる = int.Parse(lines[c++]) != 0;

			this.BackgroundColor = Common.ToColor(lines[c++]);
			this.DeniedBackColor = Common.ToColor(lines[c++]);
			this.DeniedForeColor = Common.ToColor(lines[c++]);
			this.IdleBackColor = Common.ToColor(lines[c++]);
			this.IdleForeColor = Common.ToColor(lines[c++]);
			this.BusyBackColor = Common.ToColor(lines[c++]);
			this.BusyForeColor = Common.ToColor(lines[c++]);
			this.VeryBusyBackColor = Common.ToColor(lines[c++]);
			this.VeryBusyForeColor = Common.ToColor(lines[c++]);

			this.FontSetting = FontSettingInfo.Deserialize(Enumerable
				.Range(0, int.Parse(lines[c++]))
				.Select(dummy => lines[c++])
				.ToArray());
		}

		public void SaveToFile()
		{
			List<string> lines = new List<string>();

			lines.Add("" + this.PCInstances.Length);

			foreach (PCInstanceInfo instance in this.PCInstances)
			{
				string[] instanceLines = instance.Serialize();

				lines.Add("" + instanceLines.Length);
				lines.AddRange(instanceLines);
			}

			lines.Add("" + this.MainWin_L);
			lines.Add("" + this.MainWin_T);

			lines.Add("" + this.MainWin_余白の幅);

			lines.Add("" + (this.タスクバーにアイコンを表示する ? 1 : 0));
			lines.Add("" + (this.タスクトレイにアイコンを表示する ? 1 : 0));
			lines.Add("" + (this.常に手前に表示する ? 1 : 0));
			lines.Add("" + (this.位置を固定する ? 1 : 0));
			lines.Add("" + (this.ランプを縦に並べる ? 1 : 0));

			lines.Add(Common.ToString(this.BackgroundColor));
			lines.Add(Common.ToString(this.DeniedBackColor));
			lines.Add(Common.ToString(this.DeniedForeColor));
			lines.Add(Common.ToString(this.IdleBackColor));
			lines.Add(Common.ToString(this.IdleForeColor));
			lines.Add(Common.ToString(this.BusyBackColor));
			lines.Add(Common.ToString(this.BusyForeColor));
			lines.Add(Common.ToString(this.VeryBusyBackColor));
			lines.Add(Common.ToString(this.VeryBusyForeColor));

			{
				string[] fontSettingLines = Ground.Setting.FontSetting.Serialize();

				lines.Add("" + fontSettingLines.Length);
				lines.AddRange(fontSettingLines);
			}

			File.WriteAllLines(SettingFile, lines, Encoding.UTF8);
		}
	}
}
