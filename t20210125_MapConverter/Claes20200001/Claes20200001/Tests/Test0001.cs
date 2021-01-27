using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using Charlotte.Maps;
using Charlotte.Platinums;

namespace Charlotte.Tests
{
	public class Test0001
	{
		public void Test01()
		{
			Test01_a(@"C:\Dev\Elsa\e20201220_DoremyRockman\dat\res\Worlds\Stage_Raimu_v001\Map\Start.txt");
		}

		private void Test01_a(string mapFile)
		{
			string settingFile = Common.MapFileToSettingFile(mapFile);

			Map map = new Map(mapFile);
			Setting setting = new Setting(settingFile);

			CsvData csvData = new CsvData();

			csvData.LoadFromMap(map, setting);
			csvData.SaveToDir(@"C:\temp\CsvData_0000");
		}

		public void Test02()
		{
			Test02_a(@"C:\Dev\Elsa\e20201220_DoremyRockman\dat\res\Worlds\Stage_Raimu_v001\Map\Start.txt", @"C:\temp\CsvData_0000");
		}

		private void Test02_a(string mapFile, string csvDataDir)
		{
			string settingFile = Common.MapFileToSettingFile(mapFile);

			Map map = new Map(mapFile);
			Setting setting = new Setting(settingFile);

			CsvData csvData = new CsvData();

			csvData.LoadFromDir(csvDataDir);
			csvData.SaveToMap(map, setting);

			map.Save(@"C:\temp\t0001.txt");
		}
	}
}
