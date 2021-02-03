using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public static class Prime
	{
		public static bool IsPrime(int value)
		{
			return 2 <= value && IsPrime((uint)value);
		}

		private static uint[] PRIMES_NN = new uint[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 }; // 1～2桁の素数

		public static bool IsPrime(uint value)
		{
			if (value <= 1)
				return false;

			if (value <= 3)
				return true;

			if (value % 2 == 0)
				return false;

			if (value < 100)
				return PRIMES_NN.Any(v => v == value);

			uint d = value >> 1;
			int r = 0;

			while (d % 2 == 0)
			{
				d >>= 1;
				r++;
			}

			// if n < 4,759,123,141, it is enough to test a = 2, 7, and 61; @ wiki

			foreach (uint x in new uint[] { 2, 7, 61 })
				if (!MillerRabin(x, d, r, value))
					return false;

			return true;
		}

		private static bool MillerRabin(uint x, uint d, int r, uint value)
		{
			x = ModPow(x, d, value); // x = (x の d 乗) % value;

			if (x == 1)
				return true;

			if (x == value - 1)
				return true;

			for (int c = 0; c < r; c++)
			{
				x = (uint)(((ulong)x * x) % value); //x = ModPow(x, 2, value); // x = (x * x) % value;

				if (x == value - 1)
					return true;
			}
			return false;
		}

		private static uint ModPow(uint x, uint d, uint value)
		{
			if (d == 0)
				return 1;

			if (d == 1)
				return x;

			uint ret = ModPow((uint)(((ulong)x * x) % value), d / 2, value);

			if (d % 2 != 0)
				ret = (uint)(((ulong)ret * x) % value);

			return ret;
		}
	}
}
