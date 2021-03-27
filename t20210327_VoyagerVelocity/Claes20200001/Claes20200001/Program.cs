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

		private class VelocityInfo
		{
			private VoyagerDistance.DistancePairInfo DistancePair;
			private SCommon.SimpleDateTime DateTime;

			public VelocityInfo(VoyagerDistance.DistancePairInfo distancePair, SCommon.SimpleDateTime dateTime)
			{
				this.DistancePair = distancePair;
				this.DateTime = dateTime;
			}

			public double GetKm()
			{
				return this.DistancePair.GetKm(this.DateTime);
			}

			private double GetKmPer(long sec)
			{
				return this.DistancePair.GetKm(this.DateTime + sec) - this.DistancePair.GetKm(this.DateTime);
			}

			public double GetKmPerSecond()
			{
				return this.GetKmPer(1);
			}

			public double GetKmPerMinute()
			{
				return this.GetKmPer(60);
			}

			public double GetKmPerHour()
			{
				return this.GetKmPer(3600);
			}
		}

		private void Main4()
		{
			VoyagerDistance vd = new VoyagerDistance();
			SCommon.SimpleDateTime now = SCommon.SimpleDateTime.Now();

			VelocityInfo velocitry_Earth_v1 = new VelocityInfo(vd.Earth_Voyager_1, now);
			VelocityInfo velocitry_Earth_v2 = new VelocityInfo(vd.Earth_Voyager_2, now);
			VelocityInfo velocitry_Sun_v1 = new VelocityInfo(vd.Sun_Voyager_1, now);
			VelocityInfo velocitry_Sun_v2 = new VelocityInfo(vd.Sun_Voyager_2, now);

			Console.WriteLine("Date Time: " + now);

			PrintVelocity(velocitry_Sun_v1, "  Sun to Voyager 1");
			PrintVelocity(velocitry_Sun_v2, "  Sun to Voyager 2");
			PrintVelocity(velocitry_Earth_v1, "Earth to Voyager 1");
			PrintVelocity(velocitry_Earth_v2, "Earth to Voyager 2");

			Console.WriteLine("Raw Data...");

			PrintRawData(vd.Sun_Voyager_1, "S1");
			PrintRawData(vd.Sun_Voyager_2, "S2");
			PrintRawData(vd.Earth_Voyager_1, "E1");
			PrintRawData(vd.Earth_Voyager_2, "E2");
		}

		private void PrintVelocity(VelocityInfo velocitry, string title)
		{
			Console.WriteLine(title + " --> " + S_ToString(velocitry.GetKm()) + " KM (distance)");
			Console.WriteLine(title + " --> " + S_ToString(velocitry.GetKmPerSecond()) + " KM/S (velocity)");
			Console.WriteLine(title + " --> " + S_ToString(velocitry.GetKmPerMinute()) + " KM/M (velocity)");
			Console.WriteLine(title + " --> " + S_ToString(velocitry.GetKmPerHour()) + " KM/H (velocity)");
		}

		private string S_ToString(double value)
		{
			return Common.LPad(value.ToString("F3"), 15, " ");
		}

		private void PrintRawData(VoyagerDistance.DistancePairInfo distancePair, string title)
		{
			PrintRawData(distancePair.A, title + "A");
			PrintRawData(distancePair.B, title + "B");
		}

		private void PrintRawData(VoyagerDistance.DistanceInfo distance, string title)
		{
			Console.WriteLine(title + ": " + distance.DateTime + ", " + distance.Km.ToString("F4"));
		}
	}
}
