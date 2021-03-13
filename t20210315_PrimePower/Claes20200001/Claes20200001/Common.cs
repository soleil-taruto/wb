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
		// sync > @ GetOutputDir

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

		// < sync

		public static bool IsPrime(int value)
		{
			if (value < 2)
				return false;

			if (value == 2)
				return true;

			if (value % 2 == 0)
				return false;

			int s = Square(value);

			for (int d = 3; d <= s; d += 2)
				if (value % d == 0)
					return false;

			return true;
		}

		public static int Square(int value)
		{
			if (value < 0)
				throw null;

			int l = 0;
			int r = 46341; // Keisan 2 P 31 - 1 r 2 == 46340.9500010519*

			while (l + 1 < r)
			{
				int m = (l + r) / 2;

				if (m * m <= value)
					l = m;
				else
					r = m;
			}
			return l;
		}
	}
}
