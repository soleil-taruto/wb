using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tests
{
	public class Test0002
	{
		public void Test01()
		{
			Test01_a(0, 100000);
			Test01_a(int.MaxValue - 50000, (long)int.MaxValue + 50000);
			Test01_a(uint.MaxValue - 100000, uint.MaxValue);
		}

		private void Test01_a(long minval, long maxval)
		{
			for (long value = minval; value <= maxval; value++)
			{
				if (value % 10000 == 0) Console.WriteLine(value); // cout

				bool ans_01 = Prime.IsPrime((uint)value);
				bool ans_02 = Test_IsPrime(value);

				if (ans_01 != ans_02)
					throw null; // bug
			}
		}

		private bool Test_IsPrime(long value)
		{
			if (value < 2)
				return false;

			if (value == 2)
				return true;

			if (value % 2 == 0)
				return false;

			long s = Square(value);

			for (long d = 3; d <= s; d += 2)
				if (value % d == 0)
					return false;

			return true;
		}

		private long Square(long value)
		{
			if (value < 0)
				throw null;

			long l = 0;
			long r = 3037000500; // Keisan 2 P 63 - 1 r 2 == 3037000499.9760496922*

			while (l + 1 < r)
			{
				long m = (l + r) / 2;

				if (m * m <= value)
					l = m;
				else
					r = m;
			}
			return l;
		}
	}
}
