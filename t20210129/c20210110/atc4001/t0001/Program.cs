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
			int[] nc = Common.ReadValues();

			Contest0001 contest = new Contest0001()
			{
				N = nc[0],
				C = nc[1],
				Services = Common.ReadValueTable(nc[0]),
			};

			contest.Perform();

			Console.WriteLine(contest.TotalPay);
		}
	}

	public class Contest0001
	{
		public int N;
		public int C;
		public int[][] Services;

		// <---- prm

		public long TotalPay;

		// <---- ret

		public class EdgeInfo
		{
			public bool IsEnd;
			public int Day;
			public int[] Service;
		}

		public EdgeInfo[] Edges;

		public void Perform()
		{
			Edges = Enumerable.Concat(
				Services.Select(service => new EdgeInfo()
				{
					IsEnd = false,
					Day = service[0] - 1,
					Service = service,
				}),
				Services.Select(service => new EdgeInfo()
				{
					IsEnd = true,
					Day = service[1],
					Service = service,
				})
				)
				.ToArray();

			EdgeInfo[][] days = Common.Grouping(Edges, (a, b) => a.Day - b.Day);
			long currPay = 0L;

			for (int i = 0; i < days.Length; i++)
			{
				EdgeInfo[] day = days[i];

				if (1 <= i)
				{
					TotalPay += Math.Min(currPay, C) * (day[0].Day - days[i - 1][0].Day);
				}
				foreach (EdgeInfo edge in day)
				{
					if (edge.IsEnd)
						currPay -= edge.Service[2];
					else
						currPay += edge.Service[2];
				}
			}
		}
	}

	public static class Common
	{
		#region コンソール入力

		public static int[][] ReadValueTable(int count)
		{
			return Enumerable.Range(0, count).Select(v => ReadValues()).ToArray();
		}

		public static int ReadValue(string line = null)
		{
			return ReadValues()[0];
		}

		public static int[] ReadValues(string line = null)
		{
			return ReadTokens(line).Select(v => int.Parse(v)).ToArray();
		}

		public static string[] ReadTokens(string line = null)
		{
			return ReadLine_N(line).Split(' ');
		}

		public static string ReadLine_N(string line = null)
		{
			if (line == null)
				line = Console.ReadLine();

			return line;
		}

		#endregion

		public static string DECIMAL = string.Concat(Enumerable.Range(0, 10).Select(v => (char)('0' + v)));
		public static string ALPHA = string.Concat(Enumerable.Range(0, 26).Select(v => (char)('A' + v)));
		public static string alpha = string.Concat(Enumerable.Range(0, 26).Select(v => (char)('a' + v)));

		public static T[][] Grouping<T>(T[] arr, Comparison<T> comp)
		{
			List<T[]> dest = new List<T[]>();

			Array.Sort(arr, comp);

			for (int c = 0; c < arr.Length; )
			{
				int d;

				for (d = c + 1; d < arr.Length; d++)
					if (comp(arr[c], arr[d]) != 0)
						break;

				dest.Add(GetSubArray(arr, c, d - c));
				c = d;
			}
			return dest.ToArray();
		}

		public static T[] GetSubArray<T>(T[] arr, int start, int count)
		{
			T[] dest = new T[count];
			Array.Copy(arr, start, dest, 0, count);
			return dest;
		}
	}
}
