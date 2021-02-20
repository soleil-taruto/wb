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

			Main_20210220();
			//new Test0001().Test01();
			//new Test0001().Test02();
			//new Test0002().Test01();

			// --

			//Console.WriteLine("Press ENTER key.");
			//Console.ReadLine();
		}

		private void Main_20210220()
		{
			MakePicture("Background", 960, 540, 6, 6, new I3Color(50, 150, 0), new I3Color(0, 100, 100));
		}

		private void MakePicture(string name, int w, int h, int tileW, int tileH, I3Color colorA, I3Color colorB)
		{
			Canvas canvas = new Canvas(w, h);

			for (int x = 0; x < w; x++)
			{
				for (int y = 0; y < h; y++)
				{
					canvas[x, y] = ((x / tileW + y / tileH) % 2 == 0 ? colorA : colorB).WithAlpha();
				}
			}
			canvas.Save(Path.Combine(Consts.OUTPUT_DIR, name + ".png"));
		}
	}
}
