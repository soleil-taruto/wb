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

	private static string ALPHA = string.Concat(Enumerable.Range(0, 26).Select(v => (char)('A' + v)));

	private int[,] Map; // [a,b] -- a < b, values: 0 == none, -1 == a < b, 1 == a > b
	private int[] Sq;
	private int N;

	private void Main2()
	{
		int[] nq = Console.ReadLine().Split(' ').Select(v => int.Parse(v)).ToArray();
		int n = nq[0];
		//int q = nq[1]; // ignore

		Map = new int[n, n];
		Sq = Enumerable.Range(0, n).ToArray();
		N = n;

		Array.Sort(Sq, Comp);

		Console.WriteLine("! " + string.Concat(Sq.Select(v => ALPHA[v])));
	}

	private int Comp(int a, int b)
	{
		if (a > b)
			return -Comp(b, a);

		if (a == b)
			return 0;

		if (Map[a, b] == 0)
			Map[a, b] = Search(a, b);

		return Map[a, b];
	}

	private int Search(int a, int b)
	{
		if (Search(a, b, -1))
			return -1;

		if (Search(a, b, 1))
			return 1;

		return Ask(a, b);
	}

	private bool Search(int a, int b, int relation)
	{
		if (a == b)
			return true;

		for (int i = 0; i < a; i++)
			if (Map[i, a] == -relation && Search(i, b, relation))
				return true;

		for (int i = a + 1; i < N; i++)
			if (Map[a, i] == relation && Search(i, b, relation))
				return true;

		return false;
	}

	private int Ask(int a, int b)
	{
		Console.WriteLine("? " + ALPHA[a] + " " + ALPHA[b]);

		string ans = Console.ReadLine();

		if (ans == "<")
			return -1;

		if (ans == ">")
			return 1;

		throw null; // never
	}
}
