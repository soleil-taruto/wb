using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Charlotte.Commons
{
	public static class SCommon
	{
		public const int IMAX = 1000000000; // 10^9

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
	}
}
