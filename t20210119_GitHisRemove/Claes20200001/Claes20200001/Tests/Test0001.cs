using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;

namespace Charlotte.Tests
{
	public class Test0001
	{
		public static void Distinct<T>(List<T> list, Comparison<T> comp)
		{
			for (int index = list.Count - 1; 1 <= index; index--)
				if (list.Take(index).Any(element => comp(element, list[index]) == 0))
					list.RemoveAt(index);
		}

		// ==== ==== ====
		// ==== ==== ====
		// ==== ==== ====

		private static void Distinct_02(List<int> list)
		{
		restart:
			for (int j = 1; j < list.Count; j++)
			{
				for (int i = 0; i < j; i++)
				{
					if (list[i] == list[j])
					{
						list.RemoveAt(j);
						goto restart;
					}
				}
			}
		}

		public void Test01()
		{
			for (int c = 0; c < 1000; c++)
			{
				Console.WriteLine("c: " + c);

				Test01_a(10, 3000);
				Test01_a(30, 1000);
				Test01_a(100, 300);
				Test01_a(300, 100);
				Test01_a(1000, 30);
				Test01_a(3000, 10);
			}
		}

		private void Test01_a(int valcnt, int vallmt)
		{
			int[] vals = Enumerable.Range(1, valcnt).Select(v => SCommon.CRandom.GetInt(vallmt)).ToArray();
			List<int> vals1 = vals.ToList();
			List<int> vals2 = vals.ToList();

			Distinct(vals1, (a, b) => a - b);
			Distinct_02(vals2);

			Test01_a_Check(vals1, vals2);
		}

		private static void Test01_a_Check(List<int> vals1, List<int> vals2)
		{
			int count = vals1.Count;

			if (count != vals2.Count)
				throw null;

			for (int index = 0; index < count; index++)
				if (vals1[index] != vals2[index])
					throw null;
		}
	}
}
