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

			MakeAllBlock("ブロック", "", 255);
			MakeAllBlock("散り", "散り", 200);
		}

		private Canvas _srcCanvas;
		private string _destNamePrefix;
		private int _destAlpha;

		private void MakeAllBlock(string srcName, string destNamePrefix, int destAlpha)
		{
			string srcImgFile = Path.Combine(Consts.ROOT_DIR, "_orig", srcName + ".png");

			if (!File.Exists(srcImgFile))
				throw new Exception("no srcImgFile");

			_srcCanvas = Canvas.Load(srcImgFile);
			_destNamePrefix = destNamePrefix;
			_destAlpha = destAlpha;

			MakeBlock("お邪魔", 1.0, 1.0, 1.0);
			MakeBlock("黄", 1.0, 1.0, 0.0);
			MakeBlock("紫", 1.0, 0.5, 1.0);
			MakeBlock("青", 0.0, 1.0, 1.0);
			MakeBlock("赤", 1.0, 0.0, 0.0);
			MakeBlock("緑", 0.5, 1.0, 0.5);
		}

		private Canvas _canvas;

		private void MakeBlock(string destName, double rRate, double gRate, double bRate)
		{
			_canvas = _srcCanvas.Copy();

			_canvas.Fill(dot =>
			{
				if (
					dot.R == 255 &&
					dot.G == 0 &&
					dot.B == 0 &&
					dot.A == 255
					)
				{
					dot = new I4Color(0, 0, 0, 0);
				}
				else
				{
					dot = new I4Color(
						SCommon.ToInt(dot.R * rRate),
						SCommon.ToInt(dot.G * gRate),
						SCommon.ToInt(dot.B * bRate),
						_destAlpha
						);
				}
				return dot;
			});

			string destImgFile = Path.Combine(Consts.ROOT_DIR, _destNamePrefix + destName + ".png");
			_canvas.Save(destImgFile);
		}
	}
}
