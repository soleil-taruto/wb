using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Commons;

namespace Charlotte
{
	public static class MapConverterCUI
	{
		private static string ProgramFile
		{
			get
			{
				string file = "MapConverter-CUI.exe";

				if (!File.Exists(file))
				{
					file = @"..\..\..\..\Claes20200001\Claes20200001\bin\Release\Claes20200001.exe";

					if (!File.Exists(file))
						throw new Exception("no MapConverter-CUI.exe");
				}
				return file;
			}
		}

		public static string[] MapFileToPlatinumCsvData(string mapFile, string csvDataDir)
		{
			return SCommon.Batch(new string[]
			{
				ProgramFile + " /M2C \"" + mapFile + "\" \"" + csvDataDir + "\"",
			},
			"",
			SCommon.StartProcessWindowStyle_e.INVISIBLE
			);
		}

		public static string[] PlatinumCsvDataToMapFile(string csvDataDir, string mapFile)
		{
			return SCommon.Batch(new string[]
			{
				ProgramFile + " /C2M \"" + csvDataDir + "\" \"" + mapFile + "\"",
			},
			"",
			SCommon.StartProcessWindowStyle_e.INVISIBLE
			);
		}
	}
}
