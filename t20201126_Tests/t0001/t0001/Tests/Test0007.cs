using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;

namespace Charlotte.Tests
{
	public class Test0007
	{
		public void Test01()
		{
			Test01_a(
				"1行目\r\n" +
				"2行目\r\n" +
				"3行目",
				new string[] { "1行目", "2行目", "3行目" }
				);
			Test01_a(
				"1行目\r\n" +
				"2行目\r\n" +
				"3行目\r\n",
				new string[] { "1行目", "2行目", "3行目" }
				);
			Test01_a(
				"",
				new string[] { }
				);
			Test01_a(
				"a",
				new string[] { "a" }
				);
			Test01_a(
				"\r\n",
				new string[] { "" }
				);
			Test01_a(
				"\r\n" +
				"\r\n",
				new string[] { "", "" }
				);
		}

		private void Test01_a(string text, string[] expect)
		{
			string[] lines = SCommon.TextToLines(text);

			if (SCommon.Comp(lines, expect, SCommon.Comp) != 0) // ? 不一致
				throw null; // BUG

			Console.WriteLine("OK"); // cout
		}
	}
}
