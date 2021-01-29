using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;

namespace Charlotte
{
	public class Test0001 : Program
	{
		public void Test01()
		{
			for (int c = 0; c < 1000; c++) // テスト回数
			{
				Test01_a();
			}
		}

		private void Test01_a()
		{
			for (int c = 1; c <= 26; c++)
			{
				Test01_a2(c);
			}
		}

		private void Test01_a2(int testDataLen)
		{
			Console.WriteLine("testDataLen: " + testDataLen);

			this.TestData = Enumerable.Range(0, testDataLen).ToArray();

			SCommon.CRandom.Shuffle(this.TestData);

			this.Perform();

			this.TestData = null;
		}

		private int[] TestData;

		protected override void Input()
		{
			this.N = this.TestData.Length;

			//this.Q = 1000;
			this.Q = 200;
			//this.Q = 100;
		}

		protected override void Output()
		{
			if (SCommon.Comp(this.TestData, this.Sq, SCommon.Comp) != 0)
				throw null; // [WA]

			Console.WriteLine("OK");
			Console.WriteLine("Q: " + Q);
		}

		protected override int P_Ask(int a, int b)
		{
			if (a == b)
				throw null; // never

			return SCommon.Comp(
				SCommon.IndexOf(this.TestData, v => v == a),
				SCommon.IndexOf(this.TestData, v => v == b)
				);
		}
	}
}
