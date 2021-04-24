using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using Charlotte.Commons;

namespace Charlotte
{
	public static class Common
	{
		public static void Pause()
		{
			Console.WriteLine("Press ENTER key.");
			Console.ReadLine();
		}

		#region GetOutputDir

		private static string GOD_Dir;

		public static string GetOutputDir()
		{
			if (GOD_Dir == null)
				GOD_Dir = GetOutputDir_Main();

			return GOD_Dir;
		}

		private static string GetOutputDir_Main()
		{
			for (int c = 1; c <= 999; c++)
			{
				string dir = "C:\\" + c;

				if (
					!Directory.Exists(dir) &&
					!File.Exists(dir)
					)
				{
					SCommon.CreateDir(dir);
					//SCommon.Batch(new string[] { "START " + dir });
					return dir;
				}
			}
			throw new Exception("C:\\1 ～ 999 は使用できません。");
		}

		public static void OpenOutputDir()
		{
			SCommon.Batch(new string[] { "START " + GetOutputDir() });
		}

		public static void OpenOutputDirIfCreated()
		{
			if (GOD_Dir != null)
			{
				OpenOutputDir();
			}
		}

		private static int NOP_Count = 0;

		public static string NextOutputPath()
		{
			return Path.Combine(GetOutputDir(), (++NOP_Count).ToString("D4"));
		}

		#endregion

		public static int GetIndentLength(string line)
		{
			int index;

			for (index = 0; index < line.Length; index++)
				if (line[index] != '\t')
					break;

			return index;
		}

		public static string FindProjectFileDir(string file)
		{
			string dir = SCommon.MakeFullPath(file);

			for (; ; )
			{
				if (dir.Length == 3) // ? ルートDIRまで到達した。-> プロジェクトファイルが見つからない。
					throw new Exception("プロジェクトファイルが無い。");

				if (dir.Length < 3)
					throw null; // never

				dir = Path.GetDirectoryName(dir);

				if (Directory.GetFiles(dir, "*.csproj").FirstOrDefault() != null) // ? プロジェクトファイルを見つけた。
					break;
			}
			return dir;
		}
	}
}
