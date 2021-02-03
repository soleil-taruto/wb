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
using System.Numerics;

namespace Charlotte
{
	class Program
	{
		static void Main(string[] args)
		{
			ProcMain.CUIMain(Main2);
		}

		private static void Main2(ArgsReader ar)
		{
			// -- choose one --

			//Main3();
			//new Test0001().Test01();
			//new Test0001().Test02();
			new Test0001().Test03();

			// --

			Console.WriteLine("Press ENTER key.");
			Console.ReadLine();
		}

		private static void Main3()
		{
			Search(2, 1000000);
		}

		private static void Search(int minval, int maxval)
		{
			for (int value = minval; value <= maxval; value++)
			{
				if (Prime.IsPrime(value) && Prime.IsPrime(((BigInteger)value * value) + 4))
				{
					Console.WriteLine(value);
				}
			}
		}
	}
}
