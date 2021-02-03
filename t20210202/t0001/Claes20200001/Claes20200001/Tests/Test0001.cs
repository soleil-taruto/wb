using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Commons;
using System.Numerics;

namespace Charlotte.Tests
{
	public class Test0001
	{
		public void Test01()
		{
			Test01_a(1, 100000L);
			//Test01_a(1, 10000000000L);
		}

		private void Test01_a(long minval, long maxval)
		{
			File.WriteAllLines(@"C:\temp\1.txt", GetPrimes(minval, maxval).Select(v => "" + v), Encoding.ASCII);
		}

		private IEnumerable<long> GetPrimes(long minval, long maxval)
		{
			for (long value = minval; value <= maxval; value++)
			{
				if (Prime.IsPrime(value))
				{
					//Console.WriteLine(value);
					yield return value;
				}
			}
		}

		public void Test02()
		{
			for (int value = 0; value <= 9999; value++)
			{
				Test02_a("" + value);
			}
			for (int testcnt = 0; testcnt < 1000; testcnt++)
			{
				string str;

				{
					StringBuilder buff = new StringBuilder();
					int len = SCommon.CRandom.GetRange(3, 1000);

					buff.Append(SCommon.CRandom.ChooseOne(SCommon.DECIMAL.Substring(1).ToArray()));

					for (int i = 0; i < len; i++)
						buff.Append(SCommon.CRandom.ChooseOne(SCommon.DECIMAL.ToArray()));

					str = buff.ToString();
				}

				Test02_a(str);
			}
		}

		private void Test02_a(string str)
		{
			string ret = Common.ToString(Common.ToBigInteger(str));

			Console.WriteLine("< " + str);
			Console.WriteLine("> " + ret);

			if (str != ret)
				throw null; // bug
		}

		public void Test03()
		{
			Test03_a(1, 1300);
		}

		private void Test03_a(int eMin, int eMax)
		{
			for (int e = eMin; e <= eMax; e++)
			{
				BigInteger n = BigInteger.Pow(2, e) - 1;
				bool p = Prime.IsPrime(n);

				//Console.WriteLine(e + " --> " + p);

				if (p)
					Console.WriteLine(e);
			}
		}
	}
}
