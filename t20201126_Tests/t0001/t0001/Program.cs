using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tests;

namespace Charlotte
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				new Program().Main2();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			Console.WriteLine("Press ENTER to exit.");
			Console.ReadLine();
		}

		private void Main2()
		{
			// -- choose one --

			//new Test0001().Test01();
			//new Test0002().Test01();
			//new Test0003().Test01();
			//new Test0004().Test01();
			//new Test0005().Test01();
			//new Test0005().Test02(); // manual test
			//new Test0005().Test03();
			//new Test0005().Test04(); // manual test
			new Test0006().Test01();

			// --
		}
	}
}
