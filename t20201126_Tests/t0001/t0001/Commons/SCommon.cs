using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Charlotte.Commons
{
	public static class SCommon
	{
		public static Encoding ENCODING_SJIS = Encoding.GetEncoding(932);

		public const int IMAX = 1000000000; // 10^9

		public static string DECIMAL = "0123456789";
		public static string ALPHA = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		public static string alpha = "abcdefghijklmnopqrstuvwxyz";

		public static byte[] EMPTY_BYTES = new byte[0];

		public static RandomUnit CRandom = new RandomUnit(new S_CSPRandomNumberGenerator());

		private class S_CSPRandomNumberGenerator : RandomUnit.IRandomNumberGenerator
		{
			private RandomNumberGenerator Rng = new RNGCryptoServiceProvider();
			private byte[] Cache = new byte[4096];

			public byte[] GetBlock()
			{
				this.Rng.GetBytes(this.Cache);
				return this.Cache;
			}

			public void Dispose()
			{
				if (this.Rng != null)
				{
					this.Rng.Dispose();
					this.Rng = null;
				}
			}
		}

		public static int Comp(byte[] a, byte[] b)
		{
			return Comp(a, b, Comp);
		}

		public static int Comp(string a, string b)
		{
			return Comp(Encoding.UTF8.GetBytes(a), Encoding.UTF8.GetBytes(b)); // a.CompareTo(b) ???
		}

		public static int Comp<T>(T[] a, T[] b, Comparison<T> comp)
		{
			int minlen = Math.Min(a.Length, b.Length);

			for (int index = 0; index < minlen; index++)
			{
				int ret = comp(a[index], b[index]);

				if (ret != 0)
					return ret;
			}
			return Comp(a.Length, b.Length);
		}

		public static int Comp(byte a, byte b)
		{
			return (int)a - (int)b;
		}

		public static int Comp(int a, int b)
		{
			if (a < b)
				return -1;

			if (a > b)
				return 1;

			return 0;
		}
	}
}
