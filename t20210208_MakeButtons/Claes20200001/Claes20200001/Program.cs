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

			MakeButtons_20210208();
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
	}
}
