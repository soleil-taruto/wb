using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tests
{
	public class Test0001
	{
		public void Test01()
		{
			Console.WriteLine(123456789ul.ToString("D12"));
		}

		public void Test02()
		{
			bool f = false;

			Console.WriteLine(f);
			f ^= 1 == 2; // false ^ false -> false
			Console.WriteLine(f);
			f ^= 1 == 1; // false ^ true -> true
			Console.WriteLine(f);
			f ^= 1 == 2; // true ^ false -> true
			Console.WriteLine(f);
			f ^= 1 == 1; // true ^ true -> false
			Console.WriteLine(f);
		}
	}
}
