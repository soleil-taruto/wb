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

			Main_20210217();
			//new Test0001().Test01();
			//new Test0001().Test02();
			//new Test0002().Test01();

			// --

			//Console.WriteLine("Press ENTER key.");
			//Console.ReadLine();
		}

		private void Main_20210217()
		{
			MakeImage("SaveData_DefaultThumbnail", 290, 200, "NO DATA", 180, -10, 240);
			MakeImage("DummyScreen", 960, 540);
			MakeImage("DummyScreen(2倍)", 1920, 1080);
		}

		private void MakeImage(string name, int w, int h, string text = null, int fontSize = 100, int textX = 100, int textY = 100)
		{
			Console.WriteLine("name: " + name); // cout

			Canvas canvas = new Canvas(w * 4, h * 4);

			canvas.Fill(new I4Color(100, 120, 140, 255));

			{
				const int DENOM = 500;

				for (int numer = 0; numer <= DENOM; numer++)
				{
					if (numer % (DENOM / 10) == 0) Console.WriteLine("numer: " + numer); // cout

					DrawRandomLine(
						canvas,
						numer * 1.0 / DENOM,
						text == null ? 255 : 150
						);
				}
			}

			if (text != null)
			{
				canvas = canvas.DrawString(
					text,
					fontSize,
					new I4Color(255, 255, 233, 255),
					textX,
					textY
					);
			}
			canvas = canvas.Expand(w, h);
			canvas.Save(Path.Combine(Consts.OUTPUT_DIR, name + ".png"));
		}

		private void DrawRandomLine(Canvas canvas, double rate, int colorLevelMax)
		{
			double diagonalLen = Common.GetDistance(new D2Point(canvas.W, canvas.H));
			double iRate = 1.0 - rate;
			double centerX = SCommon.CRandom.Real() * canvas.W;
			double centerY = SCommon.CRandom.Real() * canvas.H;
			double r = SCommon.CRandom.GetRange((int)(diagonalLen * 1.3), (int)(diagonalLen * 13.0));
			double rot = SCommon.CRandom.Real() * Math.PI * 2.0;
			double width = (int)(7 + iRate * iRate * diagonalLen * 0.5);

			centerX += Math.Cos(rot) * r;
			centerY += Math.Sin(rot) * r;

			double[,] colorMap = new double[4, 3] // [左右上下(0-3), RGB(0-2)]
			{
				{ SCommon.CRandom.Real(), SCommon.CRandom.Real(), SCommon.CRandom.Real() },
				{ SCommon.CRandom.Real(), SCommon.CRandom.Real(), SCommon.CRandom.Real() },
				{ SCommon.CRandom.Real(), SCommon.CRandom.Real(), SCommon.CRandom.Real() },
				{ SCommon.CRandom.Real(), SCommon.CRandom.Real(), SCommon.CRandom.Real() },
			};

			for (int x = 0; x < canvas.W; x++)
			{
				for (int y = 0; y < canvas.H; y++)
				{
					double d = Common.GetDistance(new D2Point(x, y) - new D2Point(centerX, centerY));

					if (Math.Abs(d - r) < width)
					{
						double xRate = (double)x / (canvas.W - 1);
						double yRate = (double)y / (canvas.H - 1);

						I4Color color = new I4Color(
							SCommon.ToInt((
								colorMap[0, 0] * (1.0 - xRate) + colorMap[1, 0] * xRate +
								colorMap[2, 0] * (1.0 - yRate) + colorMap[3, 0] * yRate) * 0.5 * colorLevelMax),
							SCommon.ToInt((
								colorMap[0, 1] * (1.0 - xRate) + colorMap[1, 1] * xRate +
								colorMap[2, 1] * (1.0 - yRate) + colorMap[3, 1] * yRate) * 0.5 * colorLevelMax),
							SCommon.ToInt((
								colorMap[0, 2] * (1.0 - xRate) + colorMap[1, 2] * xRate +
								colorMap[2, 2] * (1.0 - yRate) + colorMap[3, 2] * yRate) * 0.5 * colorLevelMax),
							255
							);

						canvas[x, y] = color;
					}
				}
			}
		}
	}
}
