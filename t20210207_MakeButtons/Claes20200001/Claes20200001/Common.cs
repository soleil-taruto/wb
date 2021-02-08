using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Commons;
using System.Drawing;

namespace Charlotte
{
	public static class Common
	{
		public static double GetDistance(D2Point pt)
		{
			return Math.Sqrt(pt.X * pt.X + pt.Y * pt.Y);
		}

		/// <summary>
		/// 始点から終点までの間の指定レートの位置の値を返す。
		/// </summary>
		/// <param name="a">始点</param>
		/// <param name="b">終点</param>
		/// <param name="rate">レート</param>
		/// <returns>レートの値</returns>
		public static double AToBRate(double a, double b, double rate)
		{
			return a + (b - a) * rate;
		}

		public static bool IsSameColor(Color a, Color b)
		{
			return
				a.A == b.A &&
				a.R == b.R &&
				a.G == b.G &&
				a.B == b.B;
		}

		/// <summary>
		/// サイズを(アスペクト比を維持して)矩形領域いっぱいに広げる。
		/// 整数_版
		/// </summary>
		/// <param name="size">サイズ</param>
		/// <param name="rect">矩形領域</param>
		/// <param name="interior">矩形領域の内側に張り付く場合の出力先</param>
		/// <param name="exterior">矩形領域の外側に張り付く場合の出力先</param>
		public static void AdjustRect(I2Size size, I4Rect rect, out I4Rect interior, out I4Rect exterior)
		{
			int w_h = SCommon.ToInt(((double)rect.H * size.W) / size.H); // 高さを基準にした幅
			int h_w = SCommon.ToInt(((double)rect.W * size.H) / size.W); // 幅を基準にした高さ

			I4Rect rect1;
			I4Rect rect2;

			rect1.L = rect.L + (rect.W - w_h) / 2;
			rect1.T = rect.T;
			rect1.W = w_h;
			rect1.H = rect.H;

			rect2.L = rect.L;
			rect2.T = rect.T + (rect.H - h_w) / 2;
			rect2.W = rect.W;
			rect2.H = h_w;

			if (w_h < rect.W)
			{
				interior = rect1;
				exterior = rect2;
			}
			else
			{
				interior = rect2;
				exterior = rect1;
			}
		}

		/// <summary>
		/// サイズを(アスペクト比を維持して)矩形領域いっぱいに広げる。
		/// 浮動小数点数_版
		/// </summary>
		/// <param name="size">サイズ</param>
		/// <param name="rect">矩形領域</param>
		/// <param name="interior">矩形領域の内側に張り付く場合の出力先</param>
		/// <param name="exterior">矩形領域の外側に張り付く場合の出力先</param>
		public static void AdjustRect(D2Size size, D4Rect rect, out D4Rect interior, out D4Rect exterior)
		{
			double w_h = (rect.H * size.W) / size.H; // 高さを基準にした幅
			double h_w = (rect.W * size.H) / size.W; // 幅を基準にした高さ

			D4Rect rect1;
			D4Rect rect2;

			rect1.L = rect.L + (rect.W - w_h) / 2.0;
			rect1.T = rect.T;
			rect1.W = w_h;
			rect1.H = rect.H;

			rect2.L = rect.L;
			rect2.T = rect.T + (rect.H - h_w) / 2.0;
			rect2.W = rect.W;
			rect2.H = h_w;

			if (w_h < rect.W)
			{
				interior = rect1;
				exterior = rect2;
			}
			else
			{
				interior = rect2;
				exterior = rect1;
			}
		}
	}
}
