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

		private Canvas Canvas;

		private void Main4()
		{
			Canvas = new Canvas(800, 600);

			Canvas.Fill(new I4Color(0, 0, 0, 0));

			I3Color mainFrameColor = new I3Color(200, 100, 0);
			I3Color subFrameColor = new I3Color(0, 200, 100);

			DrawFrame(25, 29, 295, 569, mainFrameColor);
			DrawFrame(306, 24, 389, 145, subFrameColor);
			DrawFrame(412, 24, 495, 145, subFrameColor);
			DrawFrame(506, 29, 776, 569, mainFrameColor);

			Canvas.Save(Common.NextOutputPath() + ".png");
		}

		private void DrawFrame(int l, int t, int r, int b, I3Color frameColor)
		{
			const int FRAME_WIDTH = 3;
			I4Color BACK_COLOR = new I4Color(0, 0, 0, 128);
			I4Rect rect = I4Rect.LTRB(l, t, r, b);

			Canvas.FillRect(
				I4Rect.LTRB(
					rect.L - FRAME_WIDTH,
					rect.T - FRAME_WIDTH,
					rect.R + FRAME_WIDTH,
					rect.B + FRAME_WIDTH
					),
				frameColor.WithAlpha()
				);

			Canvas.FillRect(rect, BACK_COLOR);
		}
	}
}
