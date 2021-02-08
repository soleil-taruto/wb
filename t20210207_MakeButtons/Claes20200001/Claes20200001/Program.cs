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

			Console.WriteLine("Press ENTER key.");
			Console.ReadLine();
		}

		private void MakeButtons_20210208()
		{
			MakeButtons_20210208_a("フルスクリーン");
			MakeButtons_20210208_a("ウィンドウ");
		}

		private void MakeButtons_20210208_a(string text)
		{
			//MakeButton01(300, 60, 10, 10, Color.White, Color.Blue, Color.Yellow, text); // test
			MakeButton01(300, 60, 10, 10, Color.White, Color.Transparent, Color.Yellow, text);
		}

		private void MakeButton01(int w, int h, int margin, int border, Color borderColor, Color backColor, Color textColor, string text)
		{
			Canvas canvas = CanvasUtils.GetText(text, 200, new Margin(0), backColor, textColor);

			canvas.ExpandKAR(w, h, backColor);
			canvas.PutMargin(backColor, new Margin(margin));
			canvas.PutMargin(borderColor, new Margin(border));
			canvas.Bitmap.Save(Path.Combine(Consts.OUTPUT_DIR, text + ".png"));
		}
	}
}
