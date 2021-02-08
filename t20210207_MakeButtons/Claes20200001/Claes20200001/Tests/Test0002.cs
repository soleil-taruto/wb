using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Charlotte.Tests
{
	public class Test0002
	{
		public void Test01()
		{
			Console.WriteLine(Color.Blue == Color.Blue);
			Console.WriteLine(Color.Red == Color.Blue);

			{
				Color c1 = Color.FromArgb(100, 150, 200);
				Color c2 = Color.FromArgb(100, 150, 200);
				Color c3 = Color.FromArgb(100, 150, 201);

				Console.WriteLine(c1 == c2);
				Console.WriteLine(c1 == c3);
			}

			{
				Color c1 = Color.FromArgb(0, 100, 150, 200);
				Color c2 = Color.FromArgb(0, 100, 150, 200);
				Color c3 = Color.FromArgb(1, 100, 150, 200);

				Console.WriteLine(c1 == c2);
				Console.WriteLine(c1 == c3);
			}
		}
	}
}
