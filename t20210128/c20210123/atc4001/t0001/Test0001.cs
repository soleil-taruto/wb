using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;

namespace Charlotte
{
	public class Test0001 : Program
	{
		public void Test01()
		{
			for (int c = 0; c < 1000; c++) // テスト回数
			{
				Test01_a();

				Console.WriteLine("c: " + c);
			}
		}

		private void Test01_a()
		{
			for (int c = 1; c <= 13; c++) // せいぜいこんくらい
			{
				Test01_a2(c);
			}
		}

		private Op_e[] TestOps;
		private long TestAns_01;
		private long TestAns_02;

		private void Test01_a2(int n)
		{
			TestOps = new Op_e[n];

			for (int i = 0; i < n; i++)
			{
				TestOps[i] = SCommon.CRandom.GetInt(2) == 0 ? Op_e.AND : Op_e.OR;
			}
			Perform();
			TestPerform();

			if (TestAns_01 != TestAns_02)
				throw null; // 不正解
		}

		protected override void Input()
		{
			N = TestOps.Length;
			Ops = TestOps.ToArray(); // Copy
		}

		protected override void Output()
		{
			TestAns_01 = Ans;
		}

		private void TestPerform()
		{
			TestAns_02 = 0;
			Values = new bool[TestOps.Length + 1];
			Search(0);
		}

		private bool[] Values;

		private void Search(int index)
		{
			if (index < Values.Length)
			{
				Values[index] = false;
				Search(index + 1);
				Values[index] = true;
				Search(index + 1);
			}
			else
			{
				bool currVal = Values[0];

				for (int i = 0; i < TestOps.Length; i++)
				{
					if (TestOps[i] == Op_e.AND)
					{
						currVal = currVal && Values[i + 1];
					}
					else // OR
					{
						currVal = currVal || Values[i + 1];
					}
				}
				if (currVal)
				{
					//Console.WriteLine(string.Join(" ", Values.Select(v => "" + v))); // test

					TestAns_02++;
				}
			}
		}
	}
}
