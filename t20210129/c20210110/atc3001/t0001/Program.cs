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

			int p = GetMax(aa, 0, aa.Length / 2);
			int q = GetMax(aa, aa.Length / 2, aa.Length);

			Console.WriteLine((aa[p] < aa[q] ? p : q) + 1);
		}

		private static int GetMax(int[] aa, int start, int end)
		{
			int p = start;

			for (int i = start + 1; i < end; i++)
				if (aa[p] < aa[i])
					p = i;

			return p;
		}
	}
}
