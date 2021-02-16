using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	/// <summary>
	/// 擬似乱数列
	/// </summary>
	public class DDRandom0002 : IDDRandom
	{
		private uint X;

		public DDRandom0002(uint seed)
		{
			this.X = seed;
		}

		/// <summary>
		/// 0以上2^32未満の乱数を返す。
		/// </summary>
		/// <returns>乱数</returns>
		public uint Next()
		{
			ulong uu1 = (ulong)this.Next2() << 32;
			ulong uu2 = (ulong)this.Next2() << 32;

			uint u1 = (uint)(uu1 % 4294967311ul); // 2^32 以降 1 番目の素数
			uint u2 = (uint)(uu2 % 4294967357ul); // 2^32 以降 2 番目の素数

			return u1 ^ u2;
		}

		private uint Next2()
		{
			return this.X = (uint)((ulong)1103515245 * this.X + 12345);
		}
	}
}
