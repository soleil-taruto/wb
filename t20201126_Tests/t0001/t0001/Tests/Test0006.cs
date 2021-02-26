using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;

namespace Charlotte.Tests
{
	public class Test0006
	{
		public void Test01()
		{
			string[][] rows = new string[][]
			{
				new string[] { "いろは", "にほへと", "ちりぬるを" },
				new string[] { "わかよたれそ", "つねならむ" },
				new string[] { "うゐ～" },
			};

			Test01_a(rows, SCommon.ENCODING_SJIS);
			Test01_a(rows, Encoding.UTF8); // memo: UTF-8 の .csv を SJIS 指定で読んでもちゃんと読めてしまう。-- StreamReader の第3引数
			Test01_a(rows, Encoding.Unicode); // 同じ理由で読める。
			Test01_a(rows, Encoding.BigEndianUnicode); // 同じ理由で読める。
			Test01_a(rows, Encoding.UTF32); // 同じ理由で読める。
		}

		private void Test01_a(string[][] rows, Encoding encoding)
		{
			Console.WriteLine(encoding); // cout

			string CSV_FILE = @"C:\temp\Test01.csv";

			string[][] rows2;

			using (CsvFileWriter writer = new CsvFileWriter(CSV_FILE, false, encoding))
			{
				writer.WriteRows(rows);
			}
			using (CsvFileReader reader = new CsvFileReader(CSV_FILE))
			{
				rows2 = reader.ReadToEnd();
			}

			if (SCommon.Comp(rows, rows2, (a, b) => SCommon.Comp(a, b, SCommon.Comp)) != 0) // ? 不一致
			{
				throw null; // BUG
			}
			Console.WriteLine("OK"); // cout
		}
	}
}
