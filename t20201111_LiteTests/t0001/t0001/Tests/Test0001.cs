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

		#region Test06

		public void Test06()
		{
			Test06_a("-2147483651", true); // int.MinValue - 3
			Test06_a("-2147483650", true); // int.MinValue - 2
			Test06_a("-2147483649", true); // int.MinValue - 1
			Test06_a("-2147483648"); // int.MinValue
			Test06_a("-2147483647"); // int.MinValue + 1
			Test06_a("-2147483646"); // int.MinValue + 2
			Test06_a("-2147483645"); // int.MinValue + 3

			Test06_a("2147483644"); // int.MaxValue - 3
			Test06_a("2147483645"); // int.MaxValue - 2
			Test06_a("2147483646"); // int.MaxValue - 1
			Test06_a("2147483647"); // int.MaxValue
			Test06_a("2147483648", true); // int.MaxValue + 1
			Test06_a("2147483649", true); // int.MaxValue + 2
			Test06_a("2147483650", true); // int.MaxValue + 3

			Test06_a2("-2147483648", int.MinValue);
			Test06_a2("-2147483647", int.MinValue + 1);
			Test06_a2("-2147483646", int.MinValue + 2);
			Test06_a2("-2147483645", int.MinValue + 3);

			Test06_a2("2147483644", int.MaxValue - 3);
			Test06_a2("2147483645", int.MaxValue - 2);
			Test06_a2("2147483646", int.MaxValue - 1);
			Test06_a2("2147483647", int.MaxValue);

			Test06_b("-9223372036854775811", true); // long.MinValue - 3
			Test06_b("-9223372036854775810", true); // long.MinValue - 2
			Test06_b("-9223372036854775809", true); // long.MinValue - 1
			Test06_b("-9223372036854775808"); // long.MinValue
			Test06_b("-9223372036854775807"); // long.MinValue + 1
			Test06_b("-9223372036854775806"); // long.MinValue + 2
			Test06_b("-9223372036854775805"); // long.MinValue + 3

			Test06_b("9223372036854775804"); // long.MaxValue - 3
			Test06_b("9223372036854775805"); // long.MaxValue - 2
			Test06_b("9223372036854775806"); // long.MaxValue - 1
			Test06_b("9223372036854775807"); // long.MaxValue
			Test06_b("9223372036854775808", true); // long.MaxValue + 1
			Test06_b("9223372036854775809", true); // long.MaxValue + 2
			Test06_b("9223372036854775810", true); // long.MaxValue + 3

			Test06_b2("-9223372036854775808", long.MinValue);
			Test06_b2("-9223372036854775807", long.MinValue + 1);
			Test06_b2("-9223372036854775806", long.MinValue + 2);
			Test06_b2("-9223372036854775805", long.MinValue + 3);

			Test06_b2("9223372036854775804", long.MaxValue - 3);
			Test06_b2("9223372036854775805", long.MaxValue - 2);
			Test06_b2("9223372036854775806", long.MaxValue - 1);
			Test06_b2("9223372036854775807", long.MaxValue);

			Test06_c(-2147483648L, int.MinValue);
			Test06_c(-2147483647L, int.MinValue + 1);
			Test06_c(-2147483646L, int.MinValue + 2);
			Test06_c(-2147483645L, int.MinValue + 3);

			Test06_c(2147483644L, int.MaxValue - 3);
			Test06_c(2147483645L, int.MaxValue - 2);
			Test06_c(2147483646L, int.MaxValue - 1);
			Test06_c(2147483647L, int.MaxValue);
		}

		private void Test06_a(string sVal, bool expectThrow = false)
		{
			Console.WriteLine(sVal); // cout

			bool throwed = false;
			try
			{
				if (sVal != "" + int.Parse(sVal))
					throw null; // ng!
			}
			catch
			{
				throwed = true;
			}
			if (throwed != expectThrow)
				throw null; // ng
		}

		private void Test06_a2(string sVal, int expect)
		{
			Console.WriteLine(sVal + ", " + expect); // cout

			if (int.Parse(sVal) != expect)
				throw null; // ng!
		}

		private void Test06_b(string sVal, bool expectThrow = false)
		{
			Console.WriteLine(sVal); // cout

			bool throwed = false;
			try
			{
				if (sVal != "" + long.Parse(sVal))
					throw null; // ng!
			}
			catch
			{
				throwed = true;
			}
			if (throwed != expectThrow)
				throw null; // ng
		}

		private void Test06_b2(string sVal, long expect)
		{
			Console.WriteLine(sVal + ", " + expect); // cout

			if (long.Parse(sVal) != expect)
				throw null; // ng!
		}

		private void Test06_c(long lVal, int value)
		{
			if (lVal != value)
				throw null; // ng!

			long tmp = value;

			if (lVal != tmp)
				throw null; // ng!
		}

		#endregion
	}
}
