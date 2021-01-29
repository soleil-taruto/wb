using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

class Program
{
	static void Main()
	{
		int[] nx = Console.ReadLine().Split(' ').Select(v => int.Parse(v)).ToArray();
		int n = nx[0];
		int x = nx[1];

		x *= 100;

		for (int i = 0; i < n; i++)
		{
			int[] vp = Console.ReadLine().Split(' ').Select(v => int.Parse(v)).ToArray();
			int vol = vp[0];
			int pct = vp[1];

			x -= vol * pct;

			if (x < 0)
			{
				Console.WriteLine(i + 1);
				return;
			}
		}
		Console.WriteLine(-1);
	}
}
