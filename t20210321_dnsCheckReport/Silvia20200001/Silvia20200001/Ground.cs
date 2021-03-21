using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using Charlotte.Commons;

namespace Charlotte
{
	public class Ground
	{
		public static Ground I;

		// ★設定項目ここから

		public string MapFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "マップファイル.txt");
		public string CsvDataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "マップデータ");

		// ★設定項目ここまで

		private static string SettingFile
		{
			get
			{
				return ProcMain.SelfFile + "-setting.dat";
			}
		}

		public void SaveSetting()
		{
			List<string> dest = new List<string>();

			dest.Add(ProcMain.APP_IDENT);

			// ★設定項目ここから

			dest.Add(this.MapFile);
			dest.Add(this.CsvDataDir);

			// ★設定項目ここまで

			File.WriteAllLines(SettingFile, dest, Encoding.UTF8);
		}

		public void LoadSetting()
		{
			if (!File.Exists(SettingFile))
				return;

			string[] lines = File.ReadAllLines(SettingFile, Encoding.UTF8);
			int c = 0;

			if (lines[c++] != ProcMain.APP_IDENT)
				throw new Exception("Bad Setting File");

			// ★設定項目ここから

			this.MapFile = lines[c++];
			this.CsvDataDir = lines[c++];

			// ★設定項目ここまで
		}
	}
}
