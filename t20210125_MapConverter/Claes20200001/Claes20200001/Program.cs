using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Commons;
using Charlotte.Maps;
using Charlotte.Tests;
using Charlotte.Platinums;

namespace Charlotte
{
	class Program
	{
		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2);
		}

		private void Main2(ArgsReader ar)
		{
			Ground.I = new Ground();

			if (ar.ArgIs("//D"))
			{
				TestMain(); // テスト
			}
			else
			{
				ProductMain(ar); // 本番
			}
		}

		private void TestMain()
		{
			// -- choose one --

			new Test0001().Test01();
			//new Test0001().Test02();
			//new Test0001().Test03();

			// --

			Console.WriteLine("Press ENTER to exit.");
			Console.ReadLine();
		}

		private void ProductMain(ArgsReader ar)
		{
			try
			{
				Main3(ar);

				Console.WriteLine("OK!"); // cout
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		private void Main3(ArgsReader ar)
		{
			if (ar.ArgIs("/M2C")) // Map --> Platinum Csv Data
			{
				string mapFile = ar.NextArg();
				string csvDataDir = ar.NextArg();
				string settingFile = Common.MapFileToSettingFile(mapFile);

				Map map = new Map(mapFile);
				Setting setting = new Setting(settingFile);

				CsvData csvData = new CsvData();

				csvData.LoadFromMap(map, setting);
				csvData.SaveToDir(csvDataDir);

				return;
			}
			if (ar.ArgIs("/C2M")) // Platinum Csv Data --> Map
			{
				string csvDataDir = ar.NextArg();
				string mapFile = ar.NextArg();
				string settingFile = Common.MapFileToSettingFile(mapFile);

				Map map = new Map(mapFile);
				Setting setting = new Setting(settingFile);

				CsvData csvData = new CsvData();

				csvData.LoadFromDir(csvDataDir);
				csvData.SaveToMap(map, setting);

				map.Save(mapFile);

				return;
			}
		}
	}
}
