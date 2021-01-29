using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

class Program
{
	static void Main()
	{
		new Program().Main2();
	}

	int N;
	int M;
	int[] Xs;
	int[] Dists;

	void Main2()
	{
		int[] nm = Console.ReadLine().Split(' ').Select(v => int.Parse(v)).ToArray();
		N = nm[0];
		M = nm[1];
		Xs = Console.ReadLine().Split(' ').Select(v => int.Parse(v)).ToArray();

		Array.Sort(Xs, (a, b) => a - b);

		Dists = Enumerable.Range(1, M - 1).Select(v => Xs[v] - Xs[v - 1]).ToArray();

		Array.Sort(Dists, (a, b) => a - b);

		int end = Dists.Length;
		end -= (N - 1);
		end = Math.Max(0, end);

		Console.WriteLine(Dists.Take(end).Sum());
	}
}
