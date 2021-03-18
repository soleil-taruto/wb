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

			Console.WriteLine("Press ENTER key.");
			Console.ReadLine();
		}

		private void Main3()
		{
			VoyagerDistance vd = new VoyagerDistance();

			Print(vd.Earth_Voyager_1, "Earth to Voyager 1");
			Print(vd.Earth_Voyager_2, "Earth to Voyager 2");
			Print(vd.Sun_Voyager_1, "Sun to Voyager 1");
			Print(vd.Sun_Voyager_2, "Sun to Voyager 2");
		}

		private void Print(VoyagerDistance.DistancePairInfo pair, string prompt)
		{
			Print(pair.A, prompt);
			Print(pair.B, prompt);

			SCommon.SimpleDateTime now = SCommon.SimpleDateTime.Now();

			Console.WriteLine(prompt + " --> " + now + ", " + pair.GetKm(now) + " KM (NOW)");
		}

		private void Print(VoyagerDistance.DistanceInfo info, string prompt)
		{
			Console.WriteLine(prompt + " --> " + info.DateTime + ", " + info.Km + " KM");
		}
	}
}
