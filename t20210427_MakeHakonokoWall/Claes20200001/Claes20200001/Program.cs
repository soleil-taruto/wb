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

			MakeHakonokoWall();
			//new Test0001().Test01();
			//new Test0001().Test02();
			//new Test0002().Test01();

			// --

			//Console.WriteLine("Press ENTER key.");
			//Console.ReadLine();
		}

		private void MakeHakonokoWall()
		{
			MakeHakonokoWall_File(new I3Color(0, 120, 180));
			MakeHakonokoWall_File(new I3Color(0, 180, 220));
			MakeHakonokoWall_File(new I3Color(200, 150, 0));
			MakeHakonokoWall_File(new I3Color(60, 120, 90));
			MakeHakonokoWall_File(new I3Color(120, 30, 150));
			MakeHakonokoWall_File(new I3Color(200, 150, 150));
			MakeHakonokoWall_File(new I3Color(75, 70, 75));
			MakeHakonokoWall_File(new I3Color(130, 130, 130));
			MakeHakonokoWall_File(new I3Color(105, 105, 100));
			MakeHakonokoWall_File(new I3Color(100, 220, 100));
			MakeHakonokoWall_File(new I3Color(120, 120, 120));
			MakeHakonokoWall_File(new I3Color(60, 75, 75));
			MakeHakonokoWall_File(new I3Color(135, 40, 70));
			MakeHakonokoWall_File(new I3Color(145, 175, 255));
			MakeHakonokoWall_File(new I3Color(135, 135, 90));
			MakeHakonokoWall_File(new I3Color(165, 165, 150));
			MakeHakonokoWall_File(new I3Color(45, 0, 0));
			MakeHakonokoWall_File(new I3Color(30, 45, 30));
		}

		private void MakeHakonokoWall_File(I3Color themeColor)
		{
			MakeHakonokoWall_Main(
				themeColor,
				@"C:\Dev\Elsa2\e20210245_Hakonoko\dat\dat\Novel\背景.png",
				color => color.R,
				color => color.B
				);

			MakeHakonokoWall_Main(
				themeColor,
				@"C:\Dev\Elsa2\e20210245_Hakonoko\dat\dat\箱から出る\背景.png",
				color => color.B,
				color => color.R
				);
		}

		private int OutputCounter = 0;

		private void MakeHakonokoWall_Main(I3Color themeColor, string srcImageFile, Func<I4Color, int> getDarkLevel, Func<I4Color, int> getLightLevel)
		{
			Canvas src = Canvas.Load(srcImageFile);
			Canvas dest = new Canvas(src.W, src.H);

			for (int x = 0; x < src.W; x++)
			{
				for (int y = 0; y < src.H; y++)
				{
					int darkLevel = getDarkLevel(src[x, y]);
					int lightLevel = getLightLevel(src[x, y]);

					dest[x, y] = new I4Color(
						GetColorLevel(darkLevel, lightLevel, themeColor.R),
						GetColorLevel(darkLevel, lightLevel, themeColor.G),
						GetColorLevel(darkLevel, lightLevel, themeColor.B),
						255
						);
				}
			}
			dest.Save(@"C:\temp\" + (OutputCounter++) + ".png");
		}

		private int GetColorLevel(int darkLevel, int lightLevel, int themeLevel)
		{
			return SCommon.ToInt(Common.AToBRate(darkLevel, lightLevel, themeLevel / 255.0));
		}
	}
}
