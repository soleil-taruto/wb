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

		/// <summary>
		/// 始点から終点までの間の指定レートの位置の値を返す。
		/// </summary>
		/// <param name="a">始点</param>
		/// <param name="b">終点</param>
		/// <param name="rate">レート</param>
		/// <returns>レートの値</returns>
		public static double AToBRate(double a, double b, double rate)
		{
			return a + (b - a) * rate;
		}

		/// <summary>
		/// 始点から終点までの間の位置をレートに変換する。
		/// </summary>
		/// <param name="a">始点</param>
		/// <param name="b">終点</param>
		/// <param name="value">位置</param>
		/// <returns>レート</returns>
		public static double RateAToB(double a, double b, double value)
		{
			return (value - a) / (b - a);
		}
	}
}
