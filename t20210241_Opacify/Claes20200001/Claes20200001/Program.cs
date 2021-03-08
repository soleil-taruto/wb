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

			Main3();
			//new Test0001().Test01();
			//new Test0001().Test02();
			//new Test0002().Test01();

			// --

			//Console.WriteLine("Press ENTER key.");
			//Console.ReadLine();
		}

		private void Main3()
		{
			Main3_a(@"C:\wb\20210214_シャニマス画像DL");
		}

		private void Main3_a(string dir)
		{
			foreach (string file in Directory.GetFiles(dir).Sort(SCommon.CompIgnoreCase))
			{
				Canvas canvas = Canvas.Load(file);

				{
					Canvas dest = new Canvas(canvas.W, canvas.H);

					dest.Fill(new I4Color(255, 255, 255, 255));
					dest.DrawImage(canvas, 0, 0, true);

					canvas = dest;
				}

				canvas.Save(Path.Combine(Consts.OUTPUT_DIR, Path.GetFileName(file)));
			}
		}
	}
}
