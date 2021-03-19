using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using Charlotte.Commons;
using Charlotte.Tests;

namespace Charlotte
{
	class Program
	{
		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2);
		}

		private void Main2(ArgsReader ar)
		{
			SCommon.DeletePath(Consts.OUTPUT_DIR);
			SCommon.CreateDir(Consts.OUTPUT_DIR);

			// -- choose one --

			Main3();
			//new Test0001().Test01();
			//new Test0001().Test02();
			//new Test0002().Test01();

			// --

			//Console.WriteLine("Press ENTER key.");
			//Console.ReadLine();
		}

		// from https://ja.wikipedia.org/wiki/%E3%83%9C%E3%82%A4%E3%82%B8%E3%83%A3%E3%83%BC1%E5%8F%B7
		//
		private static string VOYAGER_01_TBL = @"

1996/1/5,92.37,17.445
1997/1/3,97.78,17.395
1998/1/2,103.16,17.351
1999/1/1,108.54,17.314
2000/1/7,114.03,17.283
2001/1/12,119.51,17.258
2002/1/4,124.78,17.236
2003/1/3,130.15,17.216
2004/1/2,135.57,17.203
2005/1/7,141.04,17.180
2006/1/6,146.41,17.159
2007/1/5,151.76,17.136
2008/1/4,157.12,17.110
2009/1/2,162.47,17.093
2010/1/1,167.81,17.074
2011/1/7,173.26,17.060
2012/1/6,178.59,17.049
2013/1/4,183.93,17.042
2014/1/3,189.27,17.035
2015/1/16,194.81,17.027
2016/12/29,205.25,17.015

";

		private class VelocityInfo
		{
			public SCommon.SimpleDateTime DateTime;
			public double KMPerSec;
		}

		private void Main3()
		{
			string text = VOYAGER_01_TBL;
			string[] lines = SCommon.TextToLines(text).Where(line => line != "").ToArray();

			VelocityInfo[] velocities = lines.Select(line =>
			{
				string[] tokens = line.Split(',');

				if (tokens.Length != 3)
					throw null;

				int[] tsVals = tokens[0].Split('/').Select(token => int.Parse(token)).ToArray();

				if (tsVals.Length != 3)
					throw null;

				long timeStamp =
					10000000000L * tsVals[0] +
					100000000L * tsVals[1] +
					1000000L * tsVals[2];

				return new VelocityInfo()
				{
					DateTime = SCommon.SimpleDateTime.FromTimeStamp(timeStamp),
					KMPerSec = double.Parse(tokens[2]),
				};
			})
			.ToArray();

			File.WriteAllLines(
				Path.Combine(Consts.OUTPUT_DIR, "Velocities.csv"),
				velocities.Select(velocity => string.Join(
					",",
					((velocity.DateTime.ToSec() - velocities[0].DateTime.ToSec()) / 86400.0).ToString("F9"),
					velocity.KMPerSec.ToString("F9")
					)),
				Encoding.UTF8
				);
		}
	}
}
