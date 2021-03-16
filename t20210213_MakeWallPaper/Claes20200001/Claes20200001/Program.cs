using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
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

			MakeWallPapers_20210316();
			//MakeWallPapers_20210213();
			//MakeWallPapers_20210213_02();
			//new Test0001().Test01();
			//new Test0001().Test02();
			//new Test0002().Test01();

			// --

			//Console.WriteLine("Press ENTER key.");
			//Console.ReadLine();
		}

		private void MakeWallPapers_20210316()
		{
			MakeWallPaper(@"C:\etc\画像\85748489_p0.jpg");
			MakeWallPaper(@"C:\etc\画像\6428005_p0.png");
			MakeWallPaper(@"C:\etc\画像\50752377_p0.jpg");
			//MakeWallPaper(@"C:\etc\画像\53031871_p0.jpg");
			MakeWallPaper(@"C:\etc\画像\78521124_p0.jpg");
			MakeWallPaper(@"C:\etc\画像\73649310_p0.jpg");
			//MakeWallPaper(@"C:\etc\画像\73807156_p0.jpg");
			//MakeWallPaper(@"C:\etc\画像\73985947_p0.jpg");
			//MakeWallPaper(@"C:\etc\画像\75044056_p0.jpg");
			MakeWallPaper(@"C:\etc\画像\77566730_p0.jpg");
			MakeWallPaper(@"C:\etc\画像\60439044_p0.png");
		}

		private void MakeWallPapers_20210213()
		{
			MakeWallPaper(@"C:\etc\画像\1-160G5160302.jpg");
			MakeWallPaper(@"C:\etc\画像\1-160G5160313.jpg");
			MakeWallPaper(@"C:\etc\画像\52097663_p0.jpg");
			MakeWallPaper(@"C:\etc\画像\202012100031.jpg");
			MakeWallPaper(@"C:\etc\画像\202012221757.jpg");
			MakeWallPaper(@"C:\etc\画像\d0935ac710e60d81809c3e3c71719e51.jpg");
		}

		private void MakeWallPaper(string file)
		{
			const int W = 1920;
			const int H = 1080;

			Canvas src = Canvas.Load(file);
			Canvas dest = new Canvas(W, H);

			I4Rect interior;
			I4Rect exterior;

			Common.AdjustRect(src.Size, new I4Rect(new I2Point(0, 0), dest.Size), out interior, out exterior);

			{
				Canvas canvas = src.Expand(exterior.Size);

				canvas = canvas.Cut(new I4Rect(
					(canvas.W - dest.W) / 2,
					(canvas.H - dest.H) / 2,
					dest.W,
					dest.H
					));

				dest.DrawImage(canvas, 0, 0);
			}

			dest.Fill(color => new I4Color(
				color.R / 3,
				color.G / 3,
				color.B / 3,
				color.A
				));

			{
				Canvas canvas = src.Expand(interior.Size);

				dest.DrawImage(
					canvas,
					(dest.W - canvas.W) / 2,
					(dest.H - canvas.H) / 2
					);
			}

			dest.Save(Path.Combine(Consts.OUTPUT_DIR, Path.GetFileNameWithoutExtension(file) + ".png"));
		}

		private void MakeWallPapers_20210213_02()
		{
			MakeWallPaper_02(@"C:\etc\画像\1-160G5160232-50.jpg");
			MakeWallPaper_02(@"C:\etc\画像\58745223_p0.png", 100);
			MakeWallPaper_02(@"C:\etc\画像\81609917_p0.jpg", 100);
		}

		private void MakeWallPaper_02(string file, int almostDarkLevel = 30)
		{
			const int W = 1920;
			const int H = 1080;

			Canvas src = Canvas.Load(file);

			// 外周の黒い部分を切り取る。
			{
				I4Rect rect = src.GetEntityRect(color =>
					almostDarkLevel <= color.R ||
					almostDarkLevel <= color.G ||
					almostDarkLevel <= color.B
					);

				src = src.Cut(rect);
			}

			Canvas dest = new Canvas(W, H);

			I4Rect interior;
			I4Rect exterior;

			Common.AdjustRect(src.Size, new I4Rect(new I2Point(0, 0), dest.Size), out interior, out exterior);

			{
				Canvas canvas = src.Expand(exterior.Size);

				canvas = canvas.Cut(new I4Rect(
					(canvas.W - dest.W) / 2,
					(canvas.H - dest.H) / 2,
					dest.W,
					dest.H
					));

				dest.DrawImage(canvas, 0, 0);
			}

			dest.Fill(color => new I4Color(
				color.R / 3,
				color.G / 3,
				color.B / 3,
				color.A
				));

			{
				Canvas canvas = src.Expand(interior.Size);

				dest.DrawImage(
					canvas,
					(dest.W - canvas.W) / 2,
					(dest.H - canvas.H) / 2
					);
			}

			dest.Save(Path.Combine(Consts.OUTPUT_DIR, Path.GetFileNameWithoutExtension(file) + ".png"));
		}
	}
}
