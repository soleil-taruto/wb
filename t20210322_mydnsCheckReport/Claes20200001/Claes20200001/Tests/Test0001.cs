using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using System.IO;

namespace Charlotte.Tests
{
	public class Test0001
	{
		public void Test01()
		{
			string[] files = new Program().P_GetFiles(@"C:\wb\20210214_シャニマス画像DL");

			foreach (string file in files)
			{
				Console.WriteLine(file);

				if (!File.Exists(file))
					throw null; // BUG !!!
			}
		}
	}
}
