using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;

namespace Charlotte
{
	public class Test0001
	{
		public void Test01()
		{
			for (int c = 0; c < 1000; c++) // テスト回数
			{
				Test01_a();
			}
		}

		private void Test01_a()
		{
			for (int c = 1; c <= 26; c++)
			{
				Test01_a2(c);
			}
		}

		private int AskedCountMax;

		private void Test01_a2(int len)
		{
			Console.WriteLine("len: " + len);

			int[] sq = Enumerable.Range(0, len).ToArray();
			SCommon.CRandom.Shuffle(sq);

			Contest0001 contest = new Contest0001()
			{
				N = sq.Length,
				P_Ask = (a, b) =>
				{
					if (a == b)
						throw null; // never

					return SCommon.Comp(
						SCommon.IndexOf(sq, v => v == a),
						SCommon.IndexOf(sq, v => v == b)
						);
				},
			};

			contest.Perform();

			if (SCommon.Comp(sq, contest.Ans, SCommon.Comp) != 0)
				throw null; // 不正解

			AskedCountMax = Math.Max(AskedCountMax, contest.AskedCount);

			Console.WriteLine("OK " + AskedCountMax + " " + contest.AskedCount);
		}
	}
}
