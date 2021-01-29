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

				//ProductMain();
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
			int[] nq = Console.ReadLine().Split(' ').Select(v => int.Parse(v)).ToArray();
			int n = nq[0];
			//int q = nq[1]; // ignore

			Contest0001 contest = new Contest0001()
			{
				N = n,
				P_Ask = (a, b) =>
				{
					Console.WriteLine("? " + Common.ALPHA[a] + " " + Common.ALPHA[b]);

					string line = Console.ReadLine();

					if (line == "<")
						return -1;

					if (line == ">")
						return 1;

					throw null; // never
				},
			};

			contest.Perform();

			Console.WriteLine("! " + string.Concat(contest.Ans.Select(v => Common.ALPHA[v])));
		}
	}

	public class Contest0001
	{
		public int N;
		public Func<int, int, int> P_Ask;

		// <---- prm

		public int[] Ans;

		// <---- ret

		public List<int> Sq = new List<int>();
		public int AskedCount;

		public void Perform()
		{
			for (int i = 0; i < N; i++)
				Insert(i);

			Ans = Sq.ToArray();
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

	public static class Common
	{
		public static string ALPHA = string.Concat(Enumerable.Range(0, 26).Select(v => (char)('A' + v)));
	}

	public static class TCommon
	{
		// none
	}
}
