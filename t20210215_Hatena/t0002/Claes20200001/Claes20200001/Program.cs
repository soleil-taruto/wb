using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
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

			//new Test0001().Test01();
			//new Test0001().Test02();
			new ProcessingTimeTest().Test00();
			//new ProcessingTimeTest().Test01();
			//new ProcessingTimeTest().Test02();

			// --

			Console.WriteLine("Press ENTER key.");
			Console.ReadLine();
		}
	}
}
