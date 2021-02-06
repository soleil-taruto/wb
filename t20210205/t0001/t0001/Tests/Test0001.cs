using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Commons;

namespace Charlotte.Tests
{
	public class Test0001
	{
		private uint X;

		private uint Rand()
		{
			// Xorshift-32

			this.X ^= this.X << 13;
			this.X ^= this.X >> 17;
			this.X ^= this.X << 5;

			return this.X;
		}

		// ====
		// ====
		// ====

		public void Test01()
		{
			for (int c = 0; c < 2100000000; c++) // 0 ～ 2.1 GB
			//for (int c = 0; c < 500000000; c++) // 0 ～ 500 MB
			{
				this.X = (uint)c;
				this.Rand();

				//if (2020010100 <= this.X && this.X <= 2029123123) // かなり当たる。
				//if (IsLikeADateHH(this.X)) // かなり当たる。
				if (
					this.X == 2020122821 ||
					this.X == 2021020523
					)
				{
					Console.WriteLine(c + " ==> " + this.X);
				}
			}
		}

		private bool IsLikeADateHH(uint x)
		{
			if (2020010100 <= x && x <= 2029123123) // ? 2020年～2029年
			//if (2020010100 <= x && x <= 2021123123) // ? 2020年～2021年
			{
				uint hh = x % 100;
				x /= 100;
				uint dd = x % 100;
				x /= 100;
				uint mm = x % 100;

				if (
					0 <= hh && hh <= 23 && // 時
					1 <= dd && dd <= 31 && // 日
					1 <= mm && mm <= 12 // 月
					)
					return true;
			}
			return false;
		}
	}
}
