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
			int n = int.Parse(Console.ReadLine());
			int[] aa = Console.ReadLine().Split(' ').Select(v => int.Parse(v)).ToArray();
			int[] bb = Console.ReadLine().Split(' ').Select(v => int.Parse(v)).ToArray();

			long total = 0L;

			for (int i = 0; i < n; i++)
				total += aa[i] * bb[i];

			Console.WriteLine(total == 0L ? "Yes" : "No");
		}
	}
}
