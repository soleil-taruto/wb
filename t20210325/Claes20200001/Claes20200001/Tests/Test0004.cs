using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;

namespace Charlotte.Tests
{
	public class Test0004
	{
		public void Test01()
		{
			Test01_b(0, 1);

			for (int test_cnt = 0; test_cnt < 1000; test_cnt++) // テスト回数
			{
				Console.WriteLine("test_cnt: " + test_cnt); // cout

				Test01_a(10);
				Test01_a(30);
				Test01_a(100);
				Test01_a(300);
				Test01_a(1000);
				//Test01_a(3000);
				//Test01_a(10000);
			}
		}

		private void Test01_a(int maxCount)
		{
			Test01_a2(SCommon.CRandom.GetInt(maxCount) + 1);
		}

		private void Test01_a2(int count)
		{
			Test01_b(count, 10);
			Test01_b(count, 30);
			Test01_b(count, 100);
			Test01_b(count, 300);
			Test01_b(count, 1000);
			//Test01_b(count, 3000);
			//Test01_b(count, 10000);
		}

		private void Test01_b(int count, int vallmt)
		{
			List<int> list1 = MakeTestList(count, vallmt);
			List<int> list2 = list1.ToList(); // as copy

			P_Distinct(list1);
			list1.Sort((a, b) => a - b);

			list2.Sort((a, b) => a - b);
			list2 = list2.SortedDistinct((a, b) => a == b).ToList();

			if (SCommon.Comp(list1, list2, (a, b) => a - b) != 0)
				throw null; // BUG !!!
		}

		private void P_Distinct(List<int> list)
		{
		restart:
			for (int index = 1; index < list.Count; index++)
			{
				for (int ndx = 0; ndx < index; ndx++)
				{
					if (list[index] == list[ndx])
					{
						list.RemoveAt(index);
						goto restart;
					}
				}
			}
		}

		private List<int> MakeTestList(int count, int vallmt)
		{
			return Enumerable.Range(1, count).Select(dummy => SCommon.CRandom.GetInt(vallmt)).ToList();
		}
	}
}
