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
#if DEBUG
			TestMain();
#else
			ProductMain();
#endif
		}

#if DEBUG
		private static void TestMain()
		{
			try
			{
				// -- choose one --

				new Test0001().Test01();
				//new Test0002().Test01();
				//new Test0003().Test01();

				// --

				Console.WriteLine("OK!");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			Console.WriteLine("Press ENTER");
			Console.ReadLine();
		}
#endif

		private static void ProductMain()
		{
			new Program().Perform();
		}

		private static string ALPHA = string.Concat(Enumerable.Range(0, 26).Select(v => (char)('A' + v)));

		protected int[,] Map; // [a,b] -- a < b, values: 0 == none, -1 == a < b, 1 == a > b
		protected int[] Sq;
		protected int N;
		protected int AskedCount;

		protected virtual void Input()
		{
			int[] nq = Console.ReadLine().Split(' ').Select(v => int.Parse(v)).ToArray();
			int n = nq[0];
			//int q = nq[1]; // ignore

			N = n;
		}

		protected virtual void Output()
		{
			Console.WriteLine("! " + string.Concat(Sq.Select(v => ALPHA[v])));
		}

		protected virtual int P_Ask(int a, int b)
		{
			Console.WriteLine("? " + ALPHA[a] + " " + ALPHA[b]);

			string ans = Console.ReadLine();

			if (ans == "<")
				return -1;

			if (ans == ">")
				return 1;

			throw null; // never
		}

		protected void Perform()
		{
			Input();

			Map = new int[N, N];
			Sq = Enumerable.Range(0, N).ToArray();
			AskedCount = 0;

			Array.Sort(Sq, Comp);

			Output();
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
			AskedCount++;
			return this.P_Ask(a, b);
		}
	}
}
