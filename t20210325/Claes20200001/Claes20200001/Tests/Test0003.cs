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

			for (int testcnt = 0; testcnt < 300000; testcnt++) // テスト回数
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

				Test01_b(c, 'A', -1);
				Test01_b(c + "A" + d, 'A', c.Length);

				string e = MakeTestString(0, 10);

				Test01_c(
					c + "A" + d + "B" + e,
					c.Length,
					c.Length + 1 + d.Length,
					c + "B" + d + "A" + e
					);
			}
		}

		private static char[] MTS_ALLOW_CHARS = SCommon.DECIMAL.ToArray();

		private static string MakeTestString(int minlen, int maxlen)
		{
			return new string(Enumerable.Range(0, SCommon.CRandom.GetRange(minlen, maxlen))
				.Select(dummy => SCommon.CRandom.ChooseOne(MTS_ALLOW_CHARS))
				.ToArray());
		}

		private static int CharComp(char a, char b)
		{
			return (int)a - (int)b;
		}

		private void Test01_a(string a, string b, int expect)
		{
			// Array
			{
				char[] aa = a.ToArray();
				char[] bb = b.ToArray();

				if (SCommon.Comp(aa, bb, CharComp) != expect)
					throw null; // BUG !!!
			}

			// List
			{
				List<char> aa = a.ToList();
				List<char> bb = b.ToList();

				if (SCommon.Comp(aa, bb, CharComp) != expect)
					throw null; // BUG !!!
			}
		}

		private void Test01_b(string str, char target, int expect)
		{
			// Array
			{
				char[] ss = str.ToArray();

				if (SCommon.IndexOf(ss, sss => sss == target) != expect)
					throw null; // BUG !!!
			}

			// List
			{
				List<char> ss = str.ToList();

				if (SCommon.IndexOf(ss, sss => sss == target) != expect)
					throw null; // BUG !!!
			}
		}

		private void Test01_c(string str, int a, int b, string expect)
		{
			Test01_c2(str, a, b, expect);
			Test01_c2(str, b, a, expect);
		}

		private void Test01_c2(string str, int a, int b, string expect)
		{
			// Array
			{
				char[] ss = str.ToArray();
				char[] ee = expect.ToArray();

				SCommon.Swap(ss, a, b);

				if (SCommon.Comp(ss, ee, CharComp) != 0) // ? 不一致
					throw null; // BUG !!!
			}

			// List
			{
				List<char> ss = str.ToList();
				List<char> ee = expect.ToList();

				SCommon.Swap(ss, a, b);

				if (SCommon.Comp(ss, ee, CharComp) != 0) // ? 不一致
					throw null; // BUG !!!
			}
		}
	}
}
