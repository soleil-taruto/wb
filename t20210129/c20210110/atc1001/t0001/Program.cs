using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte
{
	public class Program
	{
		static void Main()
		{
			int[] xy = Console.ReadLine().Split(' ').Select(v => int.Parse(v)).ToArray();

			Console.WriteLine(Math.Abs(xy[0] - xy[1]) <= 2 ? "Yes" : "No");
		}
	}
}
