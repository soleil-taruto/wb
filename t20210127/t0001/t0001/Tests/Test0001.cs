using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Commons;

namespace Charlotte.Tests
{
	public class Test0001
	{
		public void Test01()
		{
			const int TIMELINE_SIZE = 86400;
			bool[] timeline = new bool[TIMELINE_SIZE];

			using (CsvFileWriter writer = new CsvFileWriter(@"C:\temp\CarRate.csv"))
			{
				for (double carRate = 0.001; carRate < 0.002; carRate += 0.000001)
				{
					writer.WriteCell(carRate.ToString("F5"));

					for (int sec = 0; sec < TIMELINE_SIZE; sec++)
						timeline[sec] = SCommon.CRandom.Real() < carRate;

					{
						int carPassCount = 0;
						const int STEP = 1800;

						for (int count = 0; count < TIMELINE_SIZE; count += STEP)
							if (Enumerable.Range(count, STEP).Any(v => timeline[v]))
								carPassCount++;

						writer.WriteCell(((double)carPassCount / (TIMELINE_SIZE / STEP)).ToString("F3"));
					}

					{
						int carPassCount = 0;
						const int STEP = 600;

						for (int count = 0; count < TIMELINE_SIZE; count += STEP)
							if (Enumerable.Range(count, STEP).Any(v => timeline[v]))
								carPassCount++;

						writer.WriteCell(((double)carPassCount / (TIMELINE_SIZE / STEP)).ToString("F3"));
					}

					writer.EndRow();
				}
			}
		}
	}
}
