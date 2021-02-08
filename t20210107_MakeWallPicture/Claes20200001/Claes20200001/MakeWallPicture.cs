using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Charlotte.Commons;

namespace Charlotte
{
	public class MakeWallPicture
	{
		public void Perform()
		{
			//if (!Directory.Exists(Consts.OUTPUT_DIR)) // 存在するはず
			//    throw null;

			SCommon.DeletePath(Consts.OUTPUT_DIR);
			SCommon.CreateDir(Consts.OUTPUT_DIR);

			// -- choose one or more --

			// 1
			Main01_a(1000, 24, 1.0, 1.0, 0.0, 0.5, 1.0, false);
			Main01_a(1060, 26, -0.5, 1.5, 1.0, 0.5, 1.0, true);
			Main01_a(1120, 28, -1.0, 1.0, 1.0, 0.5, 1.0, true);

			// 2
			Main01_a(1000, 20, 1.0, 1.0, 0.0, 0.5, 1.0, false);
			Main01_a(1380, 24, 1.0, 1.0, 1.0, 0.5, 1.0, true);
			Main01_a(1760, 28, 0.0, 1.0, 1.0, 0.5, 1.0, true);

			// 3
			Main01_a(1000, 18, 1.0, 1.0, 0.0, 0.5, 1.0, false);
			Main01_a(1300, 20, 1.0, 1.0, 1.5, 0.5, 1.0, true);
			Main01_a(1600, 24, 0.0, 1.0, 1.5, 0.5, 1.0, true);

			// 4
			Main01_a(1000, 20, 1.0, 1.0, 0.0, 0.5, 1.0, false);
			Main01_a(1200, 24, 0.0, 1.5, 0.2, 0.5, 1.0, true);
			Main01_a(1400, 28, -0.5, 1.0, 0.2, 0.5, 1.0, true);

			// 5
			Main01_a(1000, 20, 1.0, 1.0, 0.0, 0.5, 1.0, false);
			Main01_a(1200, 24, -1.0, 2.0, 0.5, 0.5, 1.0, true);
			Main01_a(1400, 28, -1.5, 1.5, 0.5, 0.5, 1.0, true);

			// 6
			Main01_a(1000, 20, 1.0, 1.0, 0.0, 0.5, 1.0, false);
			Main01_a(1200, 24, 0.0, 1.5, 0.2, 0.5, 1.0, true);
			Main01_a(1400, 28, -0.5, 1.0, 0.2, 0.5, 1.0, true);

			// 7
			Main01_a(1000, 18, 1.0, 1.0, 0.0, 0.5, 1.0, false);
			Main01_a(1300, 20, 1.0, 1.0, 1.5, 0.5, 1.0, true);
			Main01_a(1600, 24, 0.0, 1.0, 1.5, 0.5, 1.0, true);

			// 8
			Main01_a(1000, 16, 1.0, 1.0, 0.0, 0.5, 1.0, false);
			Main01_a(1400, 20, 0.0, 1.5, 1.0, 0.5, 1.0, true);
			Main01_a(1800, 26, 0.0, 1.0, 1.0, 0.5, 1.0, true);

			// 9
			Main01_a(1000, 18, 1.0, 1.0, 0.0, 0.5, 1.0, false);
			Main01_a(1300, 20, 1.0, 1.0, 1.5, 0.5, 1.0, true);
			Main01_a(1600, 24, 0.0, 1.0, 1.5, 0.5, 1.0, true);

			// --
		}

		private int PictureNo = 1;

		private void Main01_a(int bmp_wh, int tile_wh, double startRate, double endRate, double rateBure, double brightMin, double brightMax, bool transFlag)
		{
			// bmp_wh は bmp_wh 以上で最小の tile_wh の倍数に変更する。

			int wh = (bmp_wh + tile_wh - 1) / tile_wh;

			bmp_wh = wh * tile_wh;

			double r = bmp_wh * Math.Sqrt(2.0) * 0.5; // bmp の中心から bmp の各頂点までの距離

			double bmpCenter_x = bmp_wh / 2.0;
			double bmpCenter_y = bmp_wh / 2.0;

			using (Bitmap bmp = new Bitmap(bmp_wh, bmp_wh))
			{
				using (Graphics g = Graphics.FromImage(bmp))
				{
					for (int x = 0; x < wh; x++)
					{
						for (int y = 0; y < wh; y++)
						{
							int tile_l = (x + 0) * tile_wh;
							int tile_t = (y + 0) * tile_wh;
							int tile_r = (x + 1) * tile_wh;
							int tile_b = (y + 1) * tile_wh;

							double tileCenter_x = tile_l + tile_wh / 2.0;
							double tileCenter_y = tile_t + tile_wh / 2.0;

							double distance = Common.GetDistance(new D2Point(tileCenter_x, tileCenter_y) - new D2Point(bmpCenter_x, bmpCenter_y));

							double alpha = Common.AToBRate(startRate, endRate, distance / r) + (SCommon.CRandom.Real() - 0.5) * 2.0 * rateBure;
							alpha = SCommon.ToRange(alpha, 0.0, 1.0);

							double bright = Common.AToBRate(brightMin, brightMax, SCommon.CRandom.Real());

							int iAlpha = SCommon.ToRange((int)(alpha * 256), 0, 255);
							int iBright = SCommon.ToRange((int)(bright * 256), 0, 255);

							if (!transFlag)
								iAlpha = 255;

							g.FillRectangle(new SolidBrush(Color.FromArgb(iAlpha, iBright, iBright, iBright)), tile_l, tile_t, tile_wh, tile_wh);
						}
					}
				}

				bmp.Save(Consts.OUTPUT_FILE_PREFIX + this.PictureNo.ToString("D4") + Consts.OUTPUT_FILE_SUFFIX, ImageFormat.Png);

				this.PictureNo++;
			}
		}
	}
}
