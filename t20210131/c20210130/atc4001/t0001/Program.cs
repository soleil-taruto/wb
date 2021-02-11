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
			Contest0001 contest = new Contest0001()
			{
				N = Common.ReadValue64(),
			};

			contest.Perform();

			Console.WriteLine(contest.Ans);
		}
	}

	public class Contest0001
	{
		public long N;

		// <---- prm // HACK: abolished !!!

		public long Ans;

		// <---- ret // HACK: abolished !!!

		public void Perform()
		{
			long n = N;
			long ans = 1;
			long stair = 0;

			for (int w = 2; ; w++)
			{
				stair += w - 1;
				long s = n - stair;

				if (s <= 0)
					break;

				if (s % w == 0)
					ans++;
			}
			ans *= 2;

			Ans = ans;
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

		public static long[][] ReadValueTable64(int count)
		{
			return Enumerable.Range(0, count).Select(v => ReadValues64()).ToArray();
		}

		public static long ReadValue64(string line = null)
		{
			return ReadValues64()[0];
		}

		public static long[] ReadValues64(string line = null)
		{
			return ReadTokens(line).Select(v => long.Parse(v)).ToArray();
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
