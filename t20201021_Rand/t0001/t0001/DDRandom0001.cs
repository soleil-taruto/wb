using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	/// <summary>
	/// 擬似乱数列
	/// </summary>
	public class DDRandom0001 : IDDRandom
	{
		private uint X;

		public DDRandom0001(uint seed)
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
			ulong uu3 = (ulong)this.Next2() << 32;
			ulong uu4 = (ulong)this.Next2() << 32;
			ulong uu5 = (ulong)this.Next2() << 32;
			ulong uu6 = (ulong)this.Next2() << 32;
			ulong uu7 = (ulong)this.Next2() << 32;
			ulong uu8 = (ulong)this.Next2() << 32;
			ulong uu9 = (ulong)this.Next2() << 32;

			uint u1 = (uint)(uu1 % 4294967311ul); // 2^32 以降 1 番目の素数
			uint u2 = (uint)(uu2 % 4294967357ul); // 2^32 以降 2 番目の素数
			uint u3 = (uint)(uu3 % 4294967371ul); // 2^32 以降 3 番目の素数
			uint u4 = (uint)(uu4 % 4294967377ul); // 2^32 以降 4 番目の素数
			uint u5 = (uint)(uu5 % 4294967387ul); // 2^32 以降 5 番目の素数
			uint u6 = (uint)(uu6 % 4294967389ul); // 2^32 以降 6 番目の素数
			uint u7 = (uint)(uu7 % 4294967459ul); // 2^32 以降 7 番目の素数
			uint u8 = (uint)(uu8 % 4294967477ul); // 2^32 以降 8 番目の素数
			uint u9 = (uint)(uu9 % 4294967497ul); // 2^32 以降 9 番目の素数

			return u1 ^ u2 ^ u3 ^ u4 ^ u5 ^ u6 ^ u7 ^ u8 ^ u9;
		}

		private uint Next2()
		{
			return this.X = (uint)((ulong)1103515245 * this.X + 12345);
		}
	}
}
