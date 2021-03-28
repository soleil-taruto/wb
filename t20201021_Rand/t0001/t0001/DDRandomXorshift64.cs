using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	/// <summary>
	/// 擬似乱数列
	/// </summary>
	public class DDRandomXorshift64 : IDDRandom
	{
		private ulong X;

		public DDRandomXorshift64(ulong seed)
		{
			this.X = seed;
		}

		/// <summary>
		/// 0以上2^32未満の乱数を返す。
		/// </summary>
		/// <returns>乱数</returns>
		public uint Next()
		{
			// Xorshift-64

			this.X ^= this.X << 13;
			this.X ^= this.X >> 7;
			this.X ^= this.X << 17;

			return (uint)this.X;
		}
	}
}
