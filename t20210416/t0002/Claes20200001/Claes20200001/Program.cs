using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
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
			if (ProcMain.DEBUG)
			{
				Main3();
			}
			else
			{
				Main4();
			}
			Common.OpenOutputDirIfCreated();
		}

		private void Main3()
		{
			// -- choose one --

			Main4();
			//new Test0001().Test01();
			//new Test0002().Test01();
			//new Test0003().Test01();

			// --

			Common.Pause();
		}

		private void Main4()
		{
			Main4_a(@"C:\temp\t0001_out");
		}

		private void Main4_a(string dir)
		{
			Main4_a2(SCommon.Concat(Directory.GetFiles(dir).Select(file => File.ReadAllLines(file, Encoding.UTF8))));
		}

		private class DistanceInfo
		{
			public SCommon.SimpleDateTime TimeStamp;
			public string StrDistance;
			public double Distance;
		}

		private List<DistanceInfo> Distances = new List<DistanceInfo>();

		private class VelocityInfo
		{
			public DistanceInfo A;
			public DistanceInfo B;
			public double Velocity;
		}

		private List<VelocityInfo> Velocities = new List<VelocityInfo>();

		private class VelocityDiffInfo
		{
			public VelocityInfo A;
			public VelocityInfo B;
			public double VelocityDiff;
		}

		private List<VelocityDiffInfo> VelocityDiffs = new List<VelocityDiffInfo>();

		private void Main4_a2(IEnumerable<string> lines)
		{
			foreach (string line in lines)
			{
				if (
					line.StartsWith("S1A: ") ||
					line.StartsWith("S1B: ")
					)
				{
					if (!Regex.IsMatch(line.Substring(5), "^[0-9]{4}/[0-9]{2}/[0-9]{2} \\([月火水木金土日]\\) [0-9]{2}:[0-9]{2}:[0-9]{2}, [.0-9]+$"))
						throw null; // 想定外

					long valTimeStamp =
						long.Parse(line.Substring(5, 4)) * 10000000000 +
						long.Parse(line.Substring(10, 2)) * 100000000 +
						long.Parse(line.Substring(13, 2)) * 1000000 +
						long.Parse(line.Substring(20, 2)) * 10000 +
						long.Parse(line.Substring(23, 2)) * 100 +
						long.Parse(line.Substring(26, 2));

					SCommon.SimpleDateTime timeStamp = SCommon.SimpleDateTime.FromTimeStamp(valTimeStamp);

					string strDistance = line.Substring(30);

					Distances.Add(new DistanceInfo()
					{
						TimeStamp = timeStamp,
						StrDistance = strDistance,
					});
				}
			}

			Distances.Sort((a, b) => SCommon.Comp(a.TimeStamp.ToTimeStamp(), b.TimeStamp.ToTimeStamp()));

			// Check
			for (int index = 1; index < Distances.Count; index++)
				if (Distances[index].TimeStamp == Distances[index - 1].TimeStamp) // ? 同じ日時
					if (Distances[index].StrDistance != Distances[index - 1].StrDistance) // ? 異なる距離
						throw null; // 想定外

			Distances = Distances.OrderedDistinct((a, b) => a.TimeStamp == b.TimeStamp).ToList();

			// Check
			foreach (DistanceInfo d in Distances)
				if (
					d.TimeStamp.Hour != 1 ||
					d.TimeStamp.Minute != 0 ||
					d.TimeStamp.Second != 0
					)
					throw null; // 想定外 -- いつも 01:00:00 みたい

			// Check
			for (int index = 1; index < Distances.Count; index++)
				if (Distances[index].TimeStamp != Distances[index - 1].TimeStamp + 86400) // ? 24時間差ではない
					throw null; // 想定外

			foreach (DistanceInfo d in Distances)
				d.Distance = double.Parse(d.StrDistance);

#if true
			for (int index = 1; index < Distances.Count; index++)
			{
				DistanceInfo d1 = Distances[index];
				DistanceInfo d2 = Distances[index - 1];

				double velocity = (d1.Distance - d2.Distance) / 86400.0;

				Console.WriteLine("velocity: " + velocity.ToString("F19"));

				Velocities.Add(new VelocityInfo()
				{
					A = d2,
					B = d1,
					Velocity = velocity,
				});
			}
#else
			SCommon.SimpleDateTime startTimeStamp = Distances[0].TimeStamp;
			SCommon.SimpleDateTime endTimeStamp = Distances[Distances.Count - 1].TimeStamp;

			for (SCommon.SimpleDateTime timeStamp = startTimeStamp + 12 * 3600; timeStamp.ToTimeStamp() < endTimeStamp.ToTimeStamp(); timeStamp += 24 * 3600)
			{
				DistanceInfo d1 = Distances.Last(d => d.TimeStamp.ToTimeStamp() < timeStamp.ToTimeStamp());
				DistanceInfo d2 = Distances.First(d => timeStamp.ToTimeStamp() < d.TimeStamp.ToTimeStamp());

				double velocity = (d1.Distance - d2.Distance) / (d1.TimeStamp.ToSec() - d2.TimeStamp.ToSec());

				Console.WriteLine(timeStamp + " ==> " + velocity.ToString("F19") + " km/s");
			}
#endif

			for (int index = 1; index < Velocities.Count; index++)
			{
				VelocityInfo v1 = Velocities[index];
				VelocityInfo v2 = Velocities[index - 1];

				double velocityDiff = v1.Velocity - v2.Velocity;

				Console.WriteLine("velocityDiff: " + velocityDiff.ToString("F19"));

				VelocityDiffs.Add(new VelocityDiffInfo()
				{
					A = v2,
					B = v1,
					VelocityDiff = velocityDiff,
				});
			}

			for (int index = 1; index < VelocityDiffs.Count; index++)
			{
				VelocityDiffInfo v1 = VelocityDiffs[index];
				VelocityDiffInfo v2 = VelocityDiffs[index - 1];

				double velocityDiffDiff = v1.VelocityDiff - v2.VelocityDiff;

				Console.WriteLine("velocityDiffDiff: " + velocityDiffDiff.ToString("F19"));
			}

			// ----

			Console.WriteLine("====");

			foreach (VelocityInfo v in Velocities)
			{
				SCommon.SimpleDateTime timeStamp = v.A.TimeStamp + 12 * 3600;
				double velocity = v.Velocity;

				Console.WriteLine(timeStamp + " ==> " + velocity.ToString("F19") + " km/s");
			}
			Console.WriteLine("====");

			using (CsvFileWriter writer = new CsvFileWriter(Common.NextOutputPath() + ".csv"))
			{
				foreach (VelocityInfo v in Velocities)
				{
					writer.WriteCell("" + v.Velocity.ToString("F19"));
					writer.EndRow();
				}
			}
		}
	}
}
