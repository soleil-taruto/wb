﻿using System;
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

			//MakeButtons_20210421_DoremyRockman();
			MakeButtons_20210421_SSAGame();
			//new Test0001().Test01();
			//new Test0001().Test02();
			//new Test0002().Test01();

			// --

			//Console.WriteLine("Press ENTER key.");
			//Console.ReadLine();
		}

		private void MakeButtons_20210421_DoremyRockman()
		{
			I4Color color = new I4Color(255, 64, 64, 255);

			MakeButtons_20210209_a(2400, color, "ゲームスタート", 60);
			MakeButtons_20210209_a(2400, color, "コンテニュー", 180);
			MakeButtons_20210209_a(2400, color, "設定", 675);
			MakeButtons_20210209_a(2400, color, "終了", 675);
		}

		private void MakeButtons_20210421_SSAGame()
		{
			I4Color color = new I4Color(233, 255, 33, 255);

			MakeButtons_20210209_a(2400, color, "ゲームスタート", 60);
			MakeButtons_20210209_a(2400, color, "コンテニュー", 180);
			MakeButtons_20210209_a(2400, color, "設定", 675);
			MakeButtons_20210209_a(2400, color, "終了", 675);
		}

		private void MakeButtons_20210209_a(int w, I4Color frameColor, string text, int text_x)
		{
			//int w = 2400;
			int h = 480;
			int frame = 60;

			I4Color backColor = new I4Color(255, 255, 255, 0);
			//I4Color frameColor = new I4Color(255, 128, 192, 255);
			I4Color textColor = new I4Color(255, 255, 255, 255);
			//I4Color textColor = new I4Color(0, 0, 255, 255); // test

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

			string name = text;

			{
				int p = text.IndexOf(':');

				if (p != -1)
				{
					name = text.Substring(0, p);
					text = text.Substring(p + 1);
				}
			}

			canvas = canvas.DrawString(text, 180, textColor, h / 2 + text_x, 70);
			canvas = canvas.Expand(w / 12, h / 12);
			//canvas = canvas.Expand(w / 6, h / 6); // old
			canvas.Save(Path.Combine(Consts.OUTPUT_DIR, Common.ZenToHan(name) + ".png"));
		}
	}
}
