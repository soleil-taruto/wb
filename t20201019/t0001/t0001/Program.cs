using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Charlotte
{
	class Program
	{
		static void Main(string[] args)
		{
			// -- choose one --

			Test01();

			// --

			Console.WriteLine("OK!");
			Console.ReadLine();
		}

		private static void Test01()
		{
			for (int c1 = 0; c1 <= 255; c1++)
			{
				for (int c2 = 0; c2 <= 255; c2++)
				{
					bool ret1 = JChar0001.S_JChar.I.Contains((byte)c1, (byte)c2);
					bool ret2 = JChar0002.S_JChar.I.Contains((byte)c1, (byte)c2);

					if (ret1 != ret2)
						throw null; // bugged !!!
				}
			}
		}
	}
}
