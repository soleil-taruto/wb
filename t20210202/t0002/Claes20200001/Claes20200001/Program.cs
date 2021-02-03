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
			ProcMain.CUIMain(Main2);
		}

		private static void Main2(ArgsReader ar)
		{
			// -- choose one --

			//new Test0001().Test01();
			new Test0002().Test01();

			// --

			Console.WriteLine("Press ENTER key.");
			Console.ReadLine();
		}
	}
}
