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
		protected int[] Arr;
		protected int AteMax;

		protected virtual void Input()
		{
			N = int.Parse(Console.ReadLine());
			Arr = Console.ReadLine().Split(' ').Select(v => int.Parse(v)).ToArray();
		}

		protected virtual void Output()
		{
			Console.WriteLine(AteMax);
		}

		protected void Perform()
		{
			Input();
			AteMax = 0;
			Search();
			Output();
		}

		private void Search()
		{
			Queue<int[]> q = new Queue<int[]>();

			q.Enqueue(new int[] { 0, N });

			while (1 <= q.Count)
			{
				int[] range = q.Dequeue();

				if (range[0] < range[1])
				{
					int mi = IndexOfMin(range[0], range[1]);

					AteMax = Math.Max(AteMax, (range[1] - range[0]) * Arr[mi]);

					q.Enqueue(new int[] { range[0], mi });
					q.Enqueue(new int[] { mi + 1, range[1] });
				}
			}
		}

		private int IndexOfMin(int start, int end)
		{
			int m = Arr[start];
			int mi = start;

			for (int i = start + 1; i < end; i++)
			{
				if (Arr[i] < m)
				{
					m = Arr[i];
					mi = i;
				}
			}
			return mi;
		}
	}
}
