﻿using System;
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

			Console.WriteLine("OK!");
			Console.ReadLine();
		}

		private void Main2()
		{
			// -- choose one --

			//new Test0001().Test01();
			//new Test0001().Test02();
			//new Test0001().Test03();
			//new Test0001().Test04();
			//new Test0001().Test05();
			//new Test0001().Test06();
			new Test0001().Test07();
			//new Test0002().Test01();
			//new Test0003().Test01();
			//new Test0004().Test01();
			//new Test0005().Test01();

			// --
		}
	}
}
