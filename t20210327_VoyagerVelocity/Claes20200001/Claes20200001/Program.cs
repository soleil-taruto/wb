﻿using System;
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
		}

		private void Main3()
		{
			// -- choose one --

			Main4();
			//new Test0001().Test01();
			//new Test0002().Test01();
			//new Test0003().Test01();

			// --

			//Common.Pause();

			Common.OpenOutputDirIfCreated();
		}

		private void Main4()
		{
			Main4(ProcMain.ArgsReader);
		}

		private void Main4(ArgsReader ar)
		{
			VoyagerDistance vd = new VoyagerDistance();
			SCommon.SimpleDateTime now = SCommon.SimpleDateTime.Now();

			Console.WriteLine(vd.Earth_Voyager_1.GetKm(now));
			Console.WriteLine(vd.Earth_Voyager_2.GetKm(now));
			Console.WriteLine(vd.Sun_Voyager_1.GetKm(now));
			Console.WriteLine(vd.Sun_Voyager_2.GetKm(now));
		}
	}
}
