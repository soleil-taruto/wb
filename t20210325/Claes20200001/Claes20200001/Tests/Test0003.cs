using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;

namespace Charlotte.Tests
{
	public class Test0003
	{
		public void Test01()
		{
			Test01_a("", "", 0);

			for (int testcnt = 0; testcnt < 300000; testcnt++)
			{
				if (testcnt % 10000 == 0) Console.WriteLine("testcnt: " + testcnt); // cout

				string a = MakeTestString(1, 10);
				string b = MakeTestString(1, 10);

				//Test01_a("", "", 0);
				Test01_a("", a, -1);
				Test01_a(a, "", 1);

				Test01_a(a, a, 0);
				Test01_a(a, a + b, -1);
				Test01_a(a + b, a, 1);

				Test01_a("0" + a, "1" + a, -1);
				Test01_a("1" + a, "0" + a, 1);
				Test01_a(a + "0", a + "1", -1);
				Test01_a(a + "1", a + "0", 1);
				Test01_a(a + "0" + b, a + "1" + b, -1);
				Test01_a(a + "1" + b, a + "0" + b, 1);

				string c = MakeTestString(0, 10);
				string d = MakeTestString(0, 10);

				Test01_a("0" + c, "1" + d, -1);
				Test01_a("0" + d, "1" + c, -1);
				Test01_a("1" + c, "0" + d, 1);
				Test01_a("1" + d, "0" + c, 1);
				Test01_a(a + "0" + c, a + "1" + d, -1);
				Test01_a(a + "0" + d, a + "1" + c, -1);
				Test01_a(a + "1" + c, a + "0" + d, 1);
				Test01_a(a + "1" + d, a + "0" + c, 1);
			}
		}

		private void Test01_a(string a, string b, int expect)
		{
			char[] aa = a.ToArray();
			char[] bb = b.ToArray();

			if (SCommon.Comp(aa, bb, (aaa, bbb) => (int)aaa - (int)bbb) != expect)
				throw null; // BUG !!!
		}

		private static char[] MTS_ALLOW_CHARS = SCommon.DECIMAL.ToArray();

		private static string MakeTestString(int minlen, int maxlen)
		{
			return new string(Enumerable.Range(0, SCommon.CRandom.GetRange(minlen, maxlen))
				.Select(dummy => SCommon.CRandom.ChooseOne(MTS_ALLOW_CHARS))
				.ToArray());
		}
	}
}
