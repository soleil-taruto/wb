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

		protected int N;
		protected List<int> Sq;
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
			Sq = new List<int>();
			AskedCount = 0;

			for (int i = 0; i < N; i++)
				Insert(i);

			Output();
		}

		private void Insert(int memberNew)
		{
			int l = 0;
			int r = Sq.Count;

			while (l < r)
			{
				int m = (l + r) / 2;
				int ret = Ask(Sq[m], memberNew);

				if (ret < 0)
					l = m + 1;
				else if (0 < ret)
					r = m;
				else
					throw null; // never
			}
			Sq.Insert(l, memberNew);
		}

		private int Ask(int a, int b)
		{
			AskedCount++;
			return this.P_Ask(a, b);
		}
	}
}
