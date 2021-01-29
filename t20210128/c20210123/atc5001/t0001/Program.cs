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

				ProductMain();
				//new Test0001().Test01();
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

		protected int N;
		protected long[][] 駒s;
		protected int M;
		protected int[][] 操作s;
		protected int Q;
		protected int[][] クエリs;

		protected virtual void Input()
		{
			N = int.Parse(Console.ReadLine());
			駒s = InputRows_Long(N);
			M = int.Parse(Console.ReadLine());
			操作s = InputRows(M);
			Q = int.Parse(Console.ReadLine());
			クエリs = InputRows(Q);
		}

		private int[][] InputRows(int count)
		{
			return Enumerable.Range(0, count).Select(v => Console.ReadLine().Split(' ').Select(vv => int.Parse(vv)).ToArray()).ToArray();
		}

		private long[][] InputRows_Long(int count)
		{
			return Enumerable.Range(0, count).Select(v => Console.ReadLine().Split(' ').Select(vv => long.Parse(vv)).ToArray()).ToArray();
		}

		protected List<long[]> Anss = new List<long[]>();

		protected virtual void Output()
		{
			foreach (long[] ans in Anss)
				Console.WriteLine(ans[0] + " " + ans[1]);
		}

		protected void Perform()
		{
			Input();
			Anss.Clear();

			foreach (int[] クエリ in クエリs)
				Execクエリ(クエリ);

			Output();
		}

		private void Execクエリ(int[] クエリ)
		{
			int end = クエリ[0];
			long[] pos = 駒s[クエリ[1] - 1];
			long x = pos[0];
			long y = pos[1];

			for (int i = 0; i < end; i++)
			{
				switch (操作s[i][0])
				{
					case 1: // 時計回り 90度
						{
							long tmp = x;
							x = y;
							y = tmp;

							y *= -1;
						}
						break;

					case 2: // 反時計回り 90度
						{
							long tmp = x;
							x = y;
							y = tmp;

							x *= -1;
						}
						break;

					case 3: // x = p を軸に反転
						{
							long p = 操作s[i][1];

							x = x - p;
							x = p - x;
						}
						break;

					case 4: // y = p を軸に反転
						{
							long p = 操作s[i][1];

							y = y - p;
							y = p - y;
						}
						break;

					default:
						throw null; // never
				}
			}
			Anss.Add(new long[] { x, y });
		}
	}
}
