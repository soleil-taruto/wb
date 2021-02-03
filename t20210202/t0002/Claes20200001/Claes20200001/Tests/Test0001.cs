using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tests
{
	public class Test0001
	{
		public void Test01()
		{
			Test01_a(-100, 30000000);
		}

		private void Test01_a(int minval, int maxval)
		{
			for (int value = minval; value <= maxval; value++)
			{
				if (value % 1000000 == 0) Console.WriteLine(value); // cout

				bool ans_01 = Prime.IsPrime(value);
				bool ans_02 = Test_IsPrime(value);

				if (ans_01 != ans_02)
					throw null; // bug
			}
		}

		private bool Test_IsPrime(int value)
		{
			if (value < 2)
				return false;

			if (value == 2)
				return true;

			if (value % 2 == 0)
				return false;

			int s = Square(value);

			for (int d = 3; d <= s; d += 2)
				if (value % d == 0)
					return false;

			return true;
		}

		private int Square(int value)
		{
			if (value < 0)
				throw null;

			int l = 0;
			int r = 46341; // Keisan 2 P 31 - 1 r 2 == 46340.9500010519*

			while (l + 1 < r)
			{
				int m = (l + r) / 2;

				if (m * m <= value)
					l = m;
				else
					r = m;
			}
			return l;
		}
	}
}
