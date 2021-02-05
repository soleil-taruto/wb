using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;

namespace Charlotte.Tests
{
	public class Test0002
	{
		public void Test01()
		{
			const int DENOM = 1000;

			for (int numer = 0; numer <= DENOM; numer++)
			{
				double ha = (double)numer / DENOM;
				double r = Go(ha);

				Console.WriteLine(ha + " ==> " + r);
			}

			Console.WriteLine("Press ENTER to exit.");
			Console.ReadLine();
		}

		//private const int TIME_DENOM = 1000000;
		private const int TIME_DENOM = 300000;
		//private const int TIME_DENOM = 100000;
		//private const int TIME_DENOM = 30000;
		//private const int TIME_DENOM = 10000;

		private double Go(double ha)
		{
			double h = 1.0;
			double[] dists = new double[2];

			for (int area = 0; area < 2; area++)
			{
				double dist = 0.0;

				for (int timeNumer = 0; timeNumer < TIME_DENOM; timeNumer++)
				{
					double v = 1.0 / h;

					dist += v;
					h += ha;
				}
				dists[area] = dist;
			}
			return dists[0] / dists[1];
		}

		public void Test02()
		{
			double haL = 0.0;
			double haR = 1.0;

			for (int c = 0; c < 300; c++)
			{
				//Console.WriteLine(c + " : " + (haL - haR).ToString("F30")); // cout

				double ha = (haL + haR) / 2.0;
				double r = Go(ha);

				const double TARGET_R = 2.0;

				if (r < TARGET_R)
				{
					haL = ha;
				}
				else // TARGET_R < r
				{
					haR = ha;
				}
			}

			{
				double ha = (haL + haR) / 2.0;

				Console.WriteLine("ha: " + ha.ToString("F30")); // cout

				double haPerH = ha * TIME_DENOM;
				double tForH1 = 1.0 / haPerH;
				double tsec = tForH1 * 3600.0;
				int s = SCommon.ToInt(tsec);

				Console.WriteLine("tsec: " + tsec + " --> " + s); // cout

				s = 12 * 60 * 60 - s;
				int ss = s % 60;
				s /= 60;
				int mm = s % 60;
				s /= 60;
				int hh = s;

				Console.WriteLine("{0:D2}:{1:D2}:{2:D2}:", hh, mm, ss);
			}

			Console.WriteLine("Press ENTER to exit.");
			Console.ReadLine();
		}
	}
}
