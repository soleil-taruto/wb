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

			//Common.Pause();
		}

		private void Main4()
		{
			foreach (string dir in Directory.GetDirectories(Consts.ROOT_DIR))
			{
				Proc場所Dir(dir);
			}
		}

		Canvas _canvas;

		private void Proc場所Dir(string dir)
		{
			ProcMain.WriteLog("dir: " + dir); // cout

			string srcImgFile = GetSourceImageFile(dir);
			Canvas srcImg = Canvas.Load(srcImgFile);

			ProcMain.WriteLog("Expand Start");
			srcImg = srcImg.Expand(800, 600);
			ProcMain.WriteLog("Expand OK");

			// ==== 背景 ====

			_canvas = srcImg.Copy();

			_canvas.Fill(dot => new I4Color(
				(dot.R + 255) / 2,
				(dot.G + 255) / 2,
				(dot.B + 255) / 2,
				dot.A
				));

			_canvas.Save(Path.Combine(dir, "背景.png"));

			// ==== 枠 ====

			_canvas = srcImg.Copy();

			I3Color mainFrameColor = new I3Color(200, 100, 0);
			I3Color subFrameColor = new I3Color(0, 200, 100);

			DrawFrame(25, 29, 295, 569, mainFrameColor, 2);
			DrawFrame(306, 24, 389, 145, subFrameColor, 4);
			DrawFrame(412, 24, 495, 145, subFrameColor, 4);
			DrawFrame(506, 29, 776, 569, mainFrameColor, 2);

			_canvas.Save(Path.Combine(dir, "枠.png"));

			// ====

			ProcMain.WriteLog("dir_done"); // cout
		}

		private string GetSourceImageFile(string dir)
		{
			string[] files = Directory.GetFiles(Path.Combine(dir, "_orig"))
				.Where(v => !SCommon.EndsWithIgnoreCase(v, ".txt"))
				.ToArray();

			if (files.Length != 1)
				throw new Exception("元画像ファイルを１つに絞れない。");

			return files[0];
		}

		private void DrawFrame(int l, int t, int r, int b, I3Color frameColor, int frameWidth)
		{
			I4Color BACK_COLOR = new I4Color(255, 255, 255, 0);
			I4Rect rect = I4Rect.LTRB(l, t, r, b);

			_canvas.FillRect(
				I4Rect.LTRB(
					rect.L - frameWidth,
					rect.T - frameWidth,
					rect.R + frameWidth,
					rect.B + frameWidth
					),
				frameColor.WithAlpha()
				);

			_canvas.FillRect(rect, BACK_COLOR);
		}
	}
}
