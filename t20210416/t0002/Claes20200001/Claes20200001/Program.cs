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
			Main4_a(@"C:\temp\messages");
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

			for (int index = 1; index < Distances.Count; index++)
				if (Distances[index].TimeStamp - Distances[index - 1].TimeStamp == 0) // ? 同じ日時
					if (Distances[index].StrDistance != Distances[index - 1].StrDistance) // ? 異なる距離
						throw null; // 想定外

			Distances = Distances.OrderedDistinct((a, b) => a.TimeStamp - b.TimeStamp == 0).ToList();

			foreach (DistanceInfo d in Distances)
				d.Distance = double.Parse(d.StrDistance);

			SCommon.SimpleDateTime startTimeStamp = Distances[0].TimeStamp;
			SCommon.SimpleDateTime endTimeStamp = Distances[Distances.Count - 1].TimeStamp;

			for (SCommon.SimpleDateTime timeStamp = startTimeStamp + 12 * 3600; timeStamp.ToTimeStamp() < endTimeStamp.ToTimeStamp(); timeStamp += 24 * 3600)
			{
				DistanceInfo d1 = Distances.Last(d => d.TimeStamp.ToTimeStamp() < timeStamp.ToTimeStamp());
				DistanceInfo d2 = Distances.First(d => timeStamp.ToTimeStamp() < d.TimeStamp.ToTimeStamp());

				double velocity = (d1.Distance - d2.Distance) / (d1.TimeStamp.ToSec() - d2.TimeStamp.ToSec());

				Console.WriteLine(timeStamp + " ==> " + velocity.ToString("F19") + " km/s");
			}
		}
	}
}
