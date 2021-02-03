using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Charlotte.Commons;

namespace Charlotte.MakePictures
{
	public class MakePicture
	{
		public void Make01()
		{
			const int BMP_W = 960;
			const int BMP_H = 500;

			using (Bitmap bmp = new Bitmap(BMP_W, BMP_H))
			{
				for (int x = 0; x < BMP_W; x++)
				{
					for (int y = 0; y < BMP_H; y++)
					{
						double a = ((double)y / (BMP_H - 1));

						bmp.SetPixel(x, y, Color.FromArgb(SCommon.ToInt(a * 255.0), 255, 255, 255));
					}
				}
				bmp.Save(Path.Combine(Consts.OUTPUT_DIR, "WallFire.png"), ImageFormat.Png);
			}
		}
	}
}
