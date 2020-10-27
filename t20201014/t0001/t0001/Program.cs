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
			Test0001 t = new Test0001();

			Console.WriteLine("" + (int)t.E0001); // "0" が表示される。

			switch (t.E0001)
			{
				case Test0001.E0001_e.A: Console.WriteLine("A"); break;
				case Test0001.E0001_e.B: Console.WriteLine("B"); break;
				case Test0001.E0001_e.C: Console.WriteLine("C"); break;

				default:
					throw null; // ここへ来る。
			}
		}
	}
}
