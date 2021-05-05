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
			// -- choose one --

			Conv(@"C:\Dev\Elsa2\e20210245_Hakonoko\dat\dat\ぱくたそ\kazukiphotomon04.jpg");

			// --
		}

		private void Conv(string rFile)
		{
			Canvas canvas = Canvas.Load(rFile);

			for (int x = 0; x < canvas.W; x++)
			{
				for (int y = 0; y < canvas.H; y++)
				{
					I4Color dot = canvas[x, y];
					int level = SCommon.ToInt((dot.R + dot.G + dot.B) / 3);
					canvas[x, y] = new I4Color(level, level, level, dot.A);
				}
			}
			canvas.Save(Common.NextOutputPath() + ".png");
		}
	}
}
