using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Commons;

namespace Charlotte
{
	public static class Common
	{
		/// <summary>
		/// マップファイルから設定ファイルを探す。
		/// </summary>
		/// <param name="mapFile">マップファイル名</param>
		/// <returns>設定ファイル名</returns>
		public static string MapFileToSettingFile(string mapFile)
		{
			mapFile = SCommon.MakeFullPath(mapFile);
			string dir = mapFile;

			while (3 < dir.Length) // ? ルートディレクトリではない。
			{
				dir = Path.GetDirectoryName(dir);
				string settingFile = Path.Combine(dir, Consts.SETTING_LOCAL_FILE);

				if (File.Exists(settingFile))
					return settingFile;
			}
			throw new Exception("no Setting File");
		}
	}
}
