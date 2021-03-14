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
			public int Exponent;
		}

		private NumberInfo[] Numbers = Enumerable.Range(0, 1000000).Select(value => new NumberInfo()
		{
			Value = value,
			Exponent = -1,
		})
		.ToArray();

		private void Main3()
		{
			PutPrime(2);

			for (int count = 3; count < Numbers.Length; count += 2)
				if (Common.IsPrime(count))
					PutPrime(count);

			OutputRange(100000, 999999);
		}

		private void PutPrime(int prime)
		{
			int exponent = 1;

			for (long count = (long)prime; count < (long)Numbers.Length; count *= (long)prime)
			{
				int value = (int)count;

				//Numbers[value].Value = value;
				Numbers[value].Exponent = exponent;

				exponent++;
			}
		}

		private void OutputRange(int minval, int maxval)
		{
			List<string> notPrimePowerLines = new List<string>();
			List<string> primeLines = new List<string>();
			List<string> primePowerLines = new List<string>(); // 2乗以上の素数べき

			for (int value = minval; value <= maxval; value++)
			{
				NumberInfo number = Numbers[value];

				if (number.Exponent == -1)
				{
					notPrimePowerLines.Add("" + number.Value);
				}
				else if (number.Exponent == 1)
				{
					primeLines.Add("" + number.Value);
				}
				else if (2 <= number.Exponent)
				{
					primePowerLines.Add("" + number.Value);
				}
				else
				{
					throw null; // never
				}
			}
			File.WriteAllLines(
				Path.Combine(Common.GetOutputDir(), "notPrimePower.txt"),
				notPrimePowerLines,
				Encoding.ASCII
				);
			File.WriteAllLines(
				Path.Combine(Common.GetOutputDir(), "Prime.txt"),
				primeLines,
				Encoding.ASCII
				);
			File.WriteAllLines(
				Path.Combine(Common.GetOutputDir(), "PrimePower.txt"),
				primePowerLines,
				Encoding.ASCII
				);

			foreach (int maxDenom in new int[] { 101, 201, 301, 401, 501 })
			{
				File.WriteAllLines(
					Path.Combine(Common.GetOutputDir(), "notPrimePower_" + maxDenom + ".txt"),
					notPrimePowerLines.Where(line => Is指定値以下で割り切れない(int.Parse(line), maxDenom)),
					Encoding.ASCII
					);
				File.WriteAllLines(
					Path.Combine(Common.GetOutputDir(), "PrimePower_" + maxDenom + ".txt"),
					primePowerLines.Where(line => Is指定値以下で割り切れない(int.Parse(line), maxDenom)),
					Encoding.ASCII
					);
			}
		}

		private bool Is指定値以下で割り切れない(int value, int maxDenom)
		{
			if (value % 2 == 0)
				return false;

			for (int denom = 3; denom <= maxDenom; denom += 2)
				if (value % denom == 0)
					return false;

			return true;
		}
	}
}
