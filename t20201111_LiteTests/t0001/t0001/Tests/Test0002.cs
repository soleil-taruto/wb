using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tests
{
	public class Test0002
	{
		public void Test01()
		{
			foreach (int count in this.Counter01())
				Console.WriteLine(count);
		}

		private IEnumerable<int> Counter01()
		{
			// yield break; しても finally を通るよね -> ちゃんと通る。

			try
			{
				Console.WriteLine("Counter01_loop.1");

				for (int count = 1; ; count++)
				{
					Console.WriteLine("Counter01_loop.2.1: " + count);

					if (count == 10)
						yield break;

					Console.WriteLine("Counter01_loop.2.2: " + count);

					yield return count;

					Console.WriteLine("Counter01_loop.2.3: " + count);
				}
				//Console.WriteLine("Counter01_loop.3");
			}
			finally
			{
				Console.WriteLine("Counter01_finally");

				//yield return -1; // ここに yield は書けない。
			}
		}
	}
}

