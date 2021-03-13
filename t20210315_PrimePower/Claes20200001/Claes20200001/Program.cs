using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
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
			// -- choose one --

			Main3();
			//new Test0001().Test01();
			//new Test0001().Test02();
			//new Test0002().Test01();

			// --

			//Console.WriteLine("Press ENTER key.");
			//Console.ReadLine();

			Common.OpenOutputDirIfCreated();
		}

		private class NumberInfo
		{
			public int Value;
			public bool PrimePowerFlag;
			public int Prime;
			public int Exponent;
		}

		private NumberInfo[] Numbers = Enumerable.Range(0, 1000000).Select(value => new NumberInfo()
		{
			Value = value,
			PrimePowerFlag = false,
			Prime = -1,
			Exponent = -1,
		})
		.ToArray();

		private void Main3()
		{
			PutPrime(2);

			for (int count = 3; count < Numbers.Length; count += 2)
				if (Common.IsPrime(count))
					PutPrime(count);

			OutputRange(1, 9);
			OutputRange(10, 99);
			OutputRange(100, 999);
			OutputRange(1000, 9999);
			OutputRange(10000, 99999);
			OutputRange(100000, 999999);
		}

		private void PutPrime(int prime)
		{
			int exponent = 1;

			for (long count = (long)prime; count < (long)Numbers.Length; count *= (long)prime)
			{
				int value = (int)count;

				//Numbers[value].Value = value;
				Numbers[value].PrimePowerFlag = true;
				Numbers[value].Prime = prime;
				Numbers[value].Exponent = exponent;

				exponent++;
			}
		}

		private void OutputRange(int minval, int maxval)
		{
			using (CsvFileWriter writer = new CsvFileWriter(Common.NextOutputPath() + ".csv"))
			{
				for (int value = minval; value <= maxval; value++)
				{
					NumberInfo number = Numbers[value];

					writer.WriteCell("" + number.Value);
					writer.WriteCell(number.PrimePowerFlag ? "PP" : "NPP");
					writer.WriteCell("" + number.Exponent);
					writer.WriteCell("" + number.Prime);
					writer.EndRow();
				}
			}
		}
	}
}
