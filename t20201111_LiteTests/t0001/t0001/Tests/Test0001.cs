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

		public void Test03()
		{
			for (int insPos = 0; insPos <= 5; insPos++)
			{
				string[] arr = new string[] { "1", "2", "3", "4", "5" };
				string[] arr2 = new string[] { "AAA", "BBB", "CCC" };

				IEnumerable<string> arr3 = arr.Take(insPos).Concat(arr2).Concat(arr.Skip(insPos));

				Console.WriteLine(insPos + " -> " + string.Join(", ", arr3));
			}
		}

		public void Test04()
		{
			string[] arr = "1:2:3:4:5".Split(':');

			string[] arr2 = arr.ToArray(); // shallow copy

			arr2[1] = "AAA";
			arr2[2] = "BBB";
			arr2[3] = "CCC";

			Console.WriteLine(string.Join(", ", arr));
			Console.WriteLine(string.Join(", ", arr2));
		}

		public void Test05()
		{
			bool flag;

			flag = false;
			flag |= false;
			Console.WriteLine(flag); // False

			flag = false;
			flag |= true;
			Console.WriteLine(flag); // True

			flag = true;
			flag |= false;
			Console.WriteLine(flag); // True

			flag = true;
			flag |= true;
			Console.WriteLine(flag); // True
		}
	}
}
