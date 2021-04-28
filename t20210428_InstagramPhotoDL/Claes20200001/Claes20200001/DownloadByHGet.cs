using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using System.IO;

namespace Charlotte
{
	/// <summary>
	/// 以下に倣ってファイルをダウンロードする。
	/// -- https://github.com/soleil-taruto/Factory/blob/main/SubTools/BlueFish/InstagramDL.c
	/// </summary>
	public static class DownloadByHGet
	{
		private const string HGET_EXE_FILE = @"C:\app\Kit\HGet\HGet.exe";

		public static byte[] Download(string url)
		{
			using (WorkingDir wd = new WorkingDir())
			{
				string successfulFile = wd.GetPath("Successful.flg");
				string resHeaderFile = wd.GetPath("Header.dat");
				string resBodyFile = wd.GetPath("Body.dat");

				SCommon.Batch(
					new string[] { HGET_EXE_FILE +
						" /RSF " + Path.GetFileName(successfulFile) +
						" /RHF " + Path.GetFileName(resHeaderFile) +
						" /RBF " + Path.GetFileName(resBodyFile) +
						" /L /RBFX 5000000 /- \"" + url + "\"" },
					wd.GetPath(".")
					);

				if (!File.Exists(successfulFile))
					throw new Exception("Failed download");

				if (!File.Exists(resHeaderFile))
					throw new Exception("no resHeaderFile");

				if (!File.Exists(resBodyFile))
					throw new Exception("no resBodyFile");

				return File.ReadAllBytes(resBodyFile);
			}
		}
	}
}
