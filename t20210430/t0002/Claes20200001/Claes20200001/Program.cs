using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Charlotte.Commons;
using Charlotte.Tests;

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
			if (ProcMain.DEBUG)
			{
				Main3();
			}
			else
			{
				Main4();
			}
			Common.OpenOutputDirIfCreated();
		}

		private void Main3()
		{
			// -- choose one --

			Main4();
			//new Test0001().Test01();
			//new Test0002().Test01();
			//new Test0003().Test01();

			// --

			//Common.Pause();
		}

		private void Main4()
		{
			using (CsvFileWriter writer = new CsvFileWriter(Common.NextOutputPath() + ".csv"))
			{
				for (int variation = 1; variation <= 1000; variation++)
				{
					Console.WriteLine("variation: " + variation); // cout

					writer.WriteCell("" + variation);
					writer.WriteCell("" + GetTryCountAverage(variation).ToString("F3"));
					writer.EndRow();
				}
			}
		}

		private double GetTryCountAverage(int variation)
		{
			const int TEST_COUNT = 30000;
			int tryCountSum = 0;

			for (int testCount = 0; testCount < TEST_COUNT; testCount++)
				tryCountSum += GetTryCount(variation);

			return (double)tryCountSum / TEST_COUNT;
		}

		private int GetTryCount(int variation)
		{
			bool[] knownMap = new bool[variation];
			int tryCount = 0;

			for (; ; )
			{
				tryCount++;

				int value = SCommon.CRandom.GetInt(variation);

				if (knownMap[value])
					break;

				knownMap[value] = true;
			}
			return tryCount;
		}
	}
}
