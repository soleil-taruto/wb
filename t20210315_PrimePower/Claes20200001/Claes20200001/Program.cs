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

			Console.WriteLine("Press ENTER key.");
			Console.ReadLine();

			Common.OpenOutputDirIfCreated();
		}

		private bool[] PrimeFlags = new bool[1000000];

		private void Main3()
		{
			PutPrime(2);

			for (int value = 3; value < PrimeFlags.Length; value += 2)
				if (Common.IsPrime(value))
					PutPrime(value);

			OutputRange(1, 9);
			OutputRange(10, 99);
			OutputRange(100, 999);
			OutputRange(1000, 9999);
			OutputRange(10000, 99999);
			OutputRange(100000, 999999);
		}

		private void PutPrime(int prime)
		{
#if true // 2乗 ～ n乗
			for (long count = (long)prime * prime; count < (long)PrimeFlags.Length; count *= (long)prime)
			{
				PrimeFlags[(int)count] = true;
			}
#elif true // 1乗 ～ n乗
			for (long count = (long)prime; count < (long)PrimeFlags.Length; count *= (long)prime)
			{
				PrimeFlags[(int)count] = true;
			}
#else // 1乗のみ
			PrimeFlags[prime] = true;
#endif
		}

		private void OutputRange(int minval, int maxval)
		{
			int primeCount = 0;
			int notPrimeCount = 0;

			using (CsvFileWriter writer = new CsvFileWriter(Common.NextOutputPath() + ".csv"))
			{
				for (int value = minval; value <= maxval; value++)
				{
					bool primeFalg = PrimeFlags[value];

					if (primeFalg)
						primeCount++;
					else
						notPrimeCount++;

					writer.WriteCell("" + value);
					writer.WriteCell(primeFalg ? "P" : "N");
					writer.EndRow();
				}
				Console.WriteLine("minval: " + minval);
				Console.WriteLine("maxval: " + maxval);
				Console.WriteLine("primeRate: " + ((double)primeCount / (primeCount + notPrimeCount)));
				Console.WriteLine("primeCount: " + primeCount);
				Console.WriteLine("notPrimeCount: " + notPrimeCount);
			}
		}
	}
}
