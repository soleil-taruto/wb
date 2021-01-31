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
			for (int n = 1; n <= 108; n++)
			{
				Contest0001 contest = new Contest0001()
				{
					N = n,
				};

				contest.Perform();

				long ans_01 = contest.Ans;
				long ans_02 = TestPerform(n);

				Console.WriteLine(n + " --> " + ans_01 + ", " + ans_02);

				if (ans_01 != ans_02)
					throw null; // 不正解
			}
		}

		private int TestPerform(int n)
		{
			const int E_MIN = -300;
			const int E_MAX = 300;

			int ans = 0;

			for (int s = E_MIN; s <= E_MAX; s++)
			{
				for (int e = s; e <= E_MAX; e++)
				{
					int t = 0;

					for (int c = s; c <= e; c++)
						t += c;

					if (t == n)
						ans++;
				}
			}
			return ans;
		}
	}
}
