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
			if (!Directory.Exists(Consts.ROOT_DIR))
				throw new Exception("no ROOT_DIR");

			for (int chain = 1; chain <= 22; chain++)
			{
				MakeChain(chain);
			}
		}

		private void MakeChain(int chain)
		{
			Canvas canvas = new Canvas(124, 34);

			canvas.Fill(new I4Color(
				128 * ((chain >> 0) & 1),
				128 * ((chain >> 1) & 1),
				128 * ((chain >> 2) & 1), 200));
			canvas = canvas.DrawString(chain.ToString("D2") + " CHAIN !", 20, "Impact", new I4Color(255, 255, 255, 255), 0, 0);

			canvas.Save(Path.Combine(Consts.ROOT_DIR, chain.ToString("D2") + "連鎖.png"));
		}
	}
}
