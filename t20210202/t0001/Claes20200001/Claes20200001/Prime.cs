using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using Charlotte.Commons;

namespace Charlotte
{
	public static class Prime
	{
		private const int K = 40;

		private static int[] PRIMES_NN = new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 }; // 1～2桁の素数
		private static BigInteger P_LIMIT = Common.ToBigInteger("3317044064679887385961981");

		public static bool IsPrime(BigInteger value)
		{
			if (value <= 1)
				return false;

			if (value <= 3)
				return true;

			if (value.IsEven)
				return false;

			if (value < 100)
				return PRIMES_NN.Any(v => v == value);

			int valueScale = value.ToByteArray().Length + 10;
			BigInteger d = value >> 1;
			int r = 0;

			while (d.IsEven)
			{
				d >>= 1;
				r++;
			}

			// if n < 3,317,044,064,679,887,385,961,981, it is enough to test a = 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, and 41. @ wiki

			if (value < P_LIMIT)
			{
				foreach (int ix in new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41 })
				{
					BigInteger x = new BigInteger(new byte[] { (byte)ix, 0x00 });

					if (!MillerRabin(x, d, r, value))
						return false;
				}
			}
			else
			{
				for (int c = 0; c < K; c++)
				{
					BigInteger x = new BigInteger(SCommon.CRandom.GetBytes(valueScale).Concat(new byte[] { 0x00 }).ToArray()) % (value - 3) + 2; // c-rand: 2 ～ (value - 2)

					if (!MillerRabin(x, d, r, value))
						return false;
				}
			}
			return true;
		}

		private static bool MillerRabin(BigInteger x, BigInteger d, int r, BigInteger value)
		{
			x = BigInteger.ModPow(x, d, value); // x = (x の d 乗) % value;

			if (x == 1)
				return true;

			if (x == value - 1)
				return true;

			for (int c = 0; c < r; c++)
			{
				x = BigInteger.ModPow(x, 2, value); // x = (x * x) % value;

				if (x == value - 1)
					return true;
			}
			return false;
		}
	}
}
