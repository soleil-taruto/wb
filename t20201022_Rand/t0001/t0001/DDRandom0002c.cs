using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	/// <summary>
	/// 擬似乱数列
	/// </summary>
	public class DDRandom0002c : IDDRandom
	{
		private ulong X;

		public DDRandom0002c(ulong seed)
		{
			this.X = seed;
		}

		/// <summary>
		/// [0,2^32)
		/// </summary>
		/// <returns>乱数</returns>
		public uint Next()
		{
			ulong uu1 = this.Next2();

			uint u1 = (uint)(uu1 % 4294967311ul); // 2^32 以降 1 番目の素数

			return u1;
		}

		private ulong Next2()
		{
			return this.X = 1103515245 * (ulong)(uint)this.X + 12345;
		}
	}
}
