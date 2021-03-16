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

			//Main3();
			//Main3_20210309();
			//Main3_20210315();
			Main3_20210316();
			//new Test0001().Test01();
			//new Test0001().Test02();
			//new Test0002().Test01();

			// --

			//Console.WriteLine("Press ENTER key.");
			//Console.ReadLine();
		}

		private void Main3()
		{
			Main3_a(@"C:\temp\20210305172153\Map\0001.bmp", @"C:\temp\20210308111726\Map\0001.bmp");
			Main3_a(@"C:\temp\20210305172153\Map\0002.bmp", @"C:\temp\20210308111726\Map\0002.bmp");
			Main3_a(@"C:\temp\20210305172153\Map\0003.bmp", @"C:\temp\20210308111726\Map\0003.bmp");
		}

		private void Main3_20210309()
		{
			Main3_a(@"C:\temp\20210308235056\Map\0001.bmp", @"C:\temp\20210309102737\Map\0001.bmp");
			Main3_a(@"C:\temp\20210308235056\Map\0002.bmp", @"C:\temp\20210309102737\Map\0002.bmp");
			Main3_a(@"C:\temp\20210308235056\Map\0003.bmp", @"C:\temp\20210309102737\Map\0003.bmp");
			Main3_a(@"C:\temp\20210308235056\Map\0004.bmp", @"C:\temp\20210309102737\Map\0004.bmp");
			Main3_a(@"C:\temp\20210308235056\Map\0005.bmp", @"C:\temp\20210309102737\Map\0005.bmp");
			Main3_a(@"C:\temp\20210308235056\Map\0006.bmp", @"C:\temp\20210309102737\Map\0006.bmp");
			Main3_a(@"C:\temp\20210308235056\Map\0007.bmp", @"C:\temp\20210309102737\Map\0007.bmp");
			Main3_a(@"C:\temp\20210308235056\Map\0008.bmp", @"C:\temp\20210309102737\Map\0008.bmp");
			Main3_a(@"C:\temp\20210308235056\Map\0009.bmp", @"C:\temp\20210309102737\Map\0009.bmp");
		}

		private void Main3_20210315()
		{
			Main3_a(@"C:\temp\20210312230930\e20130001_Hako\Map\0001.bmp", @"C:\temp\20210315045101\e20130001_Hako\Map\0001.bmp");
			Main3_a(@"C:\temp\20210312230930\e20130001_Hako\Map\0002.bmp", @"C:\temp\20210315045101\e20130001_Hako\Map\0002.bmp");
			Main3_a(@"C:\temp\20210312230930\e20130001_Hako\Map\0003.bmp", @"C:\temp\20210315045101\e20130001_Hako\Map\0003.bmp");
			Main3_a(@"C:\temp\20210312230930\e20130001_Hako\Map\0004.bmp", @"C:\temp\20210315045101\e20130001_Hako\Map\0004.bmp");
			Main3_a(@"C:\temp\20210312230930\e20130001_Hako\Map\0005.bmp", @"C:\temp\20210315045101\e20130001_Hako\Map\0005.bmp");
			Main3_a(@"C:\temp\20210312230930\e20130001_Hako\Map\0006.bmp", @"C:\temp\20210315045101\e20130001_Hako\Map\0006.bmp");
			Main3_a(@"C:\temp\20210312230930\e20130001_Hako\Map\0007.bmp", @"C:\temp\20210315045101\e20130001_Hako\Map\0007.bmp");
			Main3_a(@"C:\temp\20210312230930\e20130001_Hako\Map\0008.bmp", @"C:\temp\20210315045101\e20130001_Hako\Map\0008.bmp");
			Main3_a(@"C:\temp\20210312230930\e20130001_Hako\Map\0009.bmp", @"C:\temp\20210315045101\e20130001_Hako\Map\0009.bmp");
		}

		private void Main3_20210316()
		{
			Main3_20210316_a(@"C:\temp\20210316165906\e20130001_Hako", @"C:\temp\20210316203551\e20130001_Hako");
		}

		private void Main3_20210316_a(string dir1, string dir2)
		{
			Main3_a(Path.Combine(dir1, @"Map\0001.bmp"), Path.Combine(dir2, @"Map\0001.bmp"));
			Main3_a(Path.Combine(dir1, @"Map\0002.bmp"), Path.Combine(dir2, @"Map\0002.bmp"));
			Main3_a(Path.Combine(dir1, @"Map\0003.bmp"), Path.Combine(dir2, @"Map\0003.bmp"));
			Main3_a(Path.Combine(dir1, @"Map\0004.bmp"), Path.Combine(dir2, @"Map\0004.bmp"));
			Main3_a(Path.Combine(dir1, @"Map\0005.bmp"), Path.Combine(dir2, @"Map\0005.bmp"));
			Main3_a(Path.Combine(dir1, @"Map\0006.bmp"), Path.Combine(dir2, @"Map\0006.bmp"));
			Main3_a(Path.Combine(dir1, @"Map\0007.bmp"), Path.Combine(dir2, @"Map\0007.bmp"));
			Main3_a(Path.Combine(dir1, @"Map\0008.bmp"), Path.Combine(dir2, @"Map\0008.bmp"));
			Main3_a(Path.Combine(dir1, @"Map\0009.bmp"), Path.Combine(dir2, @"Map\0009.bmp"));
		}

		private static int OutputFileCount = 0;

		private void Main3_a(string mapFile1, string mapFile2)
		{
			Canvas map1 = Canvas.Load(mapFile1);
			Canvas map2 = Canvas.Load(mapFile2);

			if (
				map1.W != map2.W ||
				map1.H != map2.H
				)
				throw new Exception("サイズ不一致");

			int w = map1.W;
			int h = map1.H;

			Canvas dest = new Canvas(w, h);

			for (int x = 0; x < w; x++)
			{
				for (int y = 0; y < h; y++)
				{
					I4Color dot1 = map1[x, y];
					I4Color dot2 = map2[x, y];
					I4Color destDot;

					if (
						dot1.R == dot2.R &&
						dot1.G == dot2.G &&
						dot1.B == dot2.B
						)
						destDot = new I3Color(0, 0, 0).WithAlpha();
					else
						destDot = new I3Color(255, 255, 255).WithAlpha();

					dest[x, y] = destDot;
				}
			}

			OutputFileCount++;

			dest.Save(Path.Combine(Consts.OUTPUT_DIR, OutputFileCount.ToString("D4") + ".png"));

			Canvas mixDest = new Canvas(w, h * 3);

			mixDest.DrawImage(dest, 0, h * 0);
			mixDest.DrawImage(map1, 0, h * 1);
			mixDest.DrawImage(map2, 0, h * 2);

			mixDest.Save(Path.Combine(Consts.OUTPUT_DIR, OutputFileCount.ToString("D4") + "_mix.png"));
		}
	}
}
