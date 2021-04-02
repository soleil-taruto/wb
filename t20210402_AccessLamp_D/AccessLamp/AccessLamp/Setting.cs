using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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

		public string[] InstanceNames = new string[] { "C:" };

		public void LoadFromFile()
		{
			if (!File.Exists(SettingFile))
				return;

			string[] lines = File.ReadAllLines(SettingFile, Encoding.UTF8);
			int c = 0;

			this.InstanceNames = Enumerable
				.Range(0, int.Parse(lines[c++]))
				.Select(dummy => lines[c++])
				.ToArray();
		}

		public void SaveToFile()
		{
			List<string> lines = new List<string>();

			lines.Add("" + this.InstanceNames.Length);
			lines.AddRange(this.InstanceNames);

			File.WriteAllLines(SettingFile, lines, Encoding.UTF8);
		}
	}
}
