using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using Charlotte.Commons;

namespace Charlotte.Tests
{
	public class Test0001
	{
		public void Test01()
		{
			for (int testcnt = 0; testcnt < 1000; testcnt++) // テスト回数
			{
				if (testcnt % 30 == 0) Console.WriteLine("testcnt: " + testcnt); // cout

				Test01_a(0, 3, 0, 3);
				Test01_a(0, 5, 0, 10);
				Test01_a(0, 10, 0, 5);
				Test01_a(0, 10, 0, 1000);
				Test01_a(0, 100, 0, 100);
				Test01_a(0, 1000, 0, 10);
			}
		}

		private void Test01_a(int strNumMin, int strNumMax, int strLenMin, int strLenMax)
		{
			string[] plainStrings = MakePlainStrings(SCommon.CRandom.GetRange(strNumMin, strNumMax), strLenMin, strLenMax);
			string serializedString = SCommon.Serializer.I.Join(plainStrings);

			if (!Regex.IsMatch(serializedString, "^[+/:=0-9A-Za-z]*$"))
				throw null; // BUG !!!

			string[] deserializedStrings = SCommon.Serializer.I.Split(serializedString);

			if (SCommon.Comp(plainStrings, deserializedStrings, SCommon.Comp) != 0)
				throw null; // BUG !!!
		}

		private static string[] MakePlainStrings(int strNum, int strLenMin, int strLenMax)
		{
			return Enumerable.Range(0, strNum)
				.Select(dummy => MakePlainString(SCommon.CRandom.GetRange(strLenMin, strLenMax)))
				.ToArray();
		}

		private static char[] MPS_AllowChars =
			(
				"\r\n\t " +
				SCommon.HALF +
				SCommon.MBC_DECIMAL +
				SCommon.MBC_ALPHA +
				SCommon.mbc_alpha +
				SCommon.MBC_SPACE +
				SCommon.MBC_PUNCT +
				SCommon.MBC_HIRA +
				SCommon.MBC_KANA +
				"東海道中膝栗毛"
			)
			.ToArray();

		private static string MakePlainString(int strLen)
		{
			return new string(Enumerable.Range(0, strLen)
				.Select(dummy => SCommon.CRandom.ChooseOne(MPS_AllowChars))
				.ToArray());
		}
	}
}
