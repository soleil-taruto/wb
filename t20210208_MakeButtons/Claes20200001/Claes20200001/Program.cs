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

			//MakeButtons_20210208();
			//MakeButtons_20210209();
			MakeButtons_20210209_002();
			//new Test0001().Test01();
			//new Test0001().Test02();
			//new Test0002().Test01();

			// --

			//Console.WriteLine("Press ENTER key.");
			//Console.ReadLine();
		}

		private void MakeButtons_20210208()
		{
			//MakeButtons_20210208_a("フルスクリーン", 50);
			//MakeButtons_20210208_a("ウィンドウ", 300);

			MakeButtons_20210208_a("はじめから", 300);
			MakeButtons_20210208_a("つづきから", 300);
			MakeButtons_20210208_a("コンフィグ", 300);
			MakeButtons_20210208_a("ＣＧモード", 300);
			MakeButtons_20210208_a("回想モード", 300);
			MakeButtons_20210208_a("ＥＮＤ", 550);
		}

		private void MakeButtons_20210208_a(string text, int text_x)
		{
			int w = 2400;
			int h = 480;
			int frame = 60;

			I4Color backColor = new I4Color(255, 255, 255, 0);
			I4Color frameColor = new I4Color(255, 128, 192, 255);
			I4Color textColor = new I4Color(255, 255, 255, 255);

			Canvas canvas = new Canvas(w, h);

			canvas.Fill(backColor);
			canvas.DrawCircle(new D2Point(0 + h / 2, h / 2), h / 2, frameColor);
			canvas.DrawCircle(new D2Point(w - h / 2, h / 2), h / 2, frameColor);
			canvas.FillRect(new I4Rect(h / 2, frame * 0, w - h, h - frame * 0), frameColor);
			canvas.FillRect(new I4Rect(h / 2, frame * 1, w - h, h - frame * 2), backColor);

			canvas = canvas.DrawString(text, 180, textColor, h / 2 + text_x, 70);

			canvas = canvas.Expand(w / 6, h / 6);

			canvas.Save(Path.Combine(Consts.OUTPUT_DIR, Common.ZenToHan(text) + ".png"));
		}

		private void MakeButtons_20210209()
		{
			I4Color color = new I4Color(255, 64, 128, 255);

			MakeButtons_20210209_a(2400, color, "はじめから", 300);
			MakeButtons_20210209_a(2400, color, "つづきから", 300);
			MakeButtons_20210209_a(2400, color, "設定", 675);
			MakeButtons_20210209_a(2400, color, "終了", 675);

			color = new I4Color(168, 192, 64, 255);

			MakeButtons_20210209_a(2400, color, "フルスクリーン", 50);
			MakeButtons_20210209_a(2400, color, "ウィンドウ", 300);
			MakeButtons_20210209_a(2400, color, "960 x 540", 300);
			MakeButtons_20210209_a(2400, color, "1080 x 607", 225);
			MakeButtons_20210209_a(2400, color, "1200 x 675", 225);
			MakeButtons_20210209_a(2400, color, "1320 x 742", 225);
			MakeButtons_20210209_a(2400, color, "1440 x 810", 225);
			MakeButtons_20210209_a(2400, color, "1560 x 877", 225);
			MakeButtons_20210209_a(2400, color, "1680 x 945", 225);
			MakeButtons_20210209_a(2400, color, "1800 x 1012", 150);
			MakeButtons_20210209_a(2400, color, "1920 x 1080", 150);
			MakeButtons_20210209_a(2400, color, "2040 x 1147", 150);
			MakeButtons_20210209_a(2400, color, "2160 x 1215", 150);
			MakeButtons_20210209_a(5220, color, "フルスクリーン 画面に合わせる (非推奨)", 50);
			MakeButtons_20210209_a(5220, color, "フルスクリーン 縦横比を維持する (推奨)", 50);

			color = new I4Color(64, 192, 192, 255);

			MakeButtons_20210209_a(2700, color, "デフォルトに戻す", 50);
			MakeButtons_20210209_a(2400, color, "戻る", 675);
		}

		private void MakeButtons_20210209_a(int w, I4Color frameColor, string text, int text_x)
		{
			//int w = 2400;
			int h = 480;
			int frame = 60;

			I4Color backColor = new I4Color(255, 255, 255, 0);
			//I4Color frameColor = new I4Color(255, 128, 192, 255);
			I4Color textColor = new I4Color(255, 255, 255, 255);

			Canvas canvas = new Canvas(w, h);

			canvas.Fill(backColor);
			canvas.DrawCircle(new D2Point(0 + h / 2, h / 2), h / 2, frameColor);
			canvas.DrawCircle(new D2Point(w - h / 2, h / 2), h / 2, frameColor);
			canvas.FillRect(new I4Rect(h / 2, frame * 0, w - h, h - frame * 0), frameColor);
			canvas.FillRect(new I4Rect(h / 2, frame * 1, w - h, h - frame * 2), backColor);

			{
				Func<I4Color, I4Color> a_fill = col =>
				{
					col.R /= 2;
					col.G /= 2;
					col.B /= 2;

					return col;
				};

				canvas.FillRect(new I4Rect(1 * w / 2, 0 * h / 2, w / 2, h / 2), a_fill);
				canvas.FillRect(new I4Rect(0 * w / 2, 1 * h / 2, w / 2, h / 2), a_fill);
			}

			canvas = canvas.DrawString(text, 180, textColor, h / 2 + text_x, 70);

			canvas = canvas.Expand(w / 6, h / 6);

			canvas.Save(Path.Combine(Consts.OUTPUT_DIR, Common.ZenToHan(text) + ".png"));
		}

		private void MakeButtons_20210209_002()
		{
			I2Size canvasSize = new I2Size(1920, 1080);
			I4Color behindColor = new I4Color(192, 255, 255, 100);
			I4Color frontColor = new I4Color(192, 255, 255, 255);

			MakeFrame(
				"基本設定",
				canvasSize,
				behindColor,
				frontColor,
				new I2Point[]
				{
					new I2Point(550, 50),
					new I2Point(1050, 50),
					new I2Point(1050, 150),
				},
				new I2Point[]
				{
					new I2Point(50, 50),
					new I2Point(550, 50),
					new I2Point(550, 150),
					new I2Point(1870, 150),
					new I2Point(1870, 1030),
					new I2Point(50, 1030),
					new I2Point(50, 50),
				}
				);

			MakeFrame(
				"拡張設定",
				canvasSize,
				behindColor,
				frontColor,
				new I2Point[]
				{
					new I2Point(50, 150),
					new I2Point(50, 50),
					new I2Point(550, 50),
				},
				new I2Point[]
				{
					new I2Point(50, 150),
					new I2Point(550, 150),
					new I2Point(550, 50),
					new I2Point(1050, 50),
					new I2Point(1050, 150),
					new I2Point(1870, 150),
					new I2Point(1870, 1030),
					new I2Point(50, 1030),
					new I2Point(50, 150),
				}
				);

			MakeFrame(
				"詳細設定",
				canvasSize,
				behindColor,
				frontColor,
				new I2Point[] { },
				new I2Point[]
				{
					new I2Point(50, 50),
					new I2Point(1870, 50),
					new I2Point(1870, 1030),
					new I2Point(50, 1030),
					new I2Point(50, 50),
				}
				);

			MakeFrame(
				"TrackBar",
				new I2Size(600, 80),
				behindColor,
				frontColor,
				new I2Point[] { },
				new I2Point[]
				{
					new I2Point(10, 10),
					new I2Point(585, 10),
					new I2Point(585, 65),
					new I2Point(10, 65),
					new I2Point(10, 10),
				}
				);

			MakeFrame(
				"TrackBar_つまみ",
				new I2Size(80, 80),
				behindColor,
				frontColor,
				new I2Point[] { },
				new I2Point[]
				{
					new I2Point(20, 20),
					new I2Point(55, 20),
					new I2Point(55, 55),
					new I2Point(20, 55),
					new I2Point(20, 20),
				}
				);
		}

		private void MakeFrame(string name, I2Size canvasSize, I4Color color1, I4Color color2, I2Point[] pts1, I2Point[] pts2)
		{
			int LINE_WIDTH = 5;

			Canvas canvas = new Canvas(canvasSize.W, canvasSize.H);

			canvas.Fill(new I4Color(255, 255, 255, 0));

			Action<I4Color, I2Point[], int, int> a_drawLine = (color, pts, xBure, yBure) =>
			{
				for (int index = 1; index < pts.Length; index++)
				{
					I2Point ptA = pts[index - 1];
					I2Point ptB = pts[index];

					if (ptA.X == ptB.X) // Y-方向
					{
						int yMin = Math.Min(ptA.Y, ptB.Y);
						int yMax = Math.Max(ptA.Y, ptB.Y);

						for (int y = yMin; y <= yMax; y++)
							canvas[ptA.X + xBure, y + yBure] = color;
					}
					else // X-方向
					{
						int xMin = Math.Min(ptA.X, ptB.X);
						int xMax = Math.Max(ptA.X, ptB.X);

						for (int x = xMin; x <= xMax; x++)
							canvas[x + xBure, ptA.Y + yBure] = color;
					}
				}
			};

			for (int xc = 0; xc < LINE_WIDTH; xc++)
				for (int yc = 0; yc < LINE_WIDTH; yc++)
					a_drawLine(color1, pts1, xc, yc);

			for (int xc = 0; xc < LINE_WIDTH; xc++)
				for (int yc = 0; yc < LINE_WIDTH; yc++)
					a_drawLine(color2, pts2, xc, yc);

			canvas.Save(Path.Combine(Consts.OUTPUT_DIR, name + ".png"));
		}
	}
}
