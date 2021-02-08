using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Charlotte.Commons;

namespace Charlotte
{
	public class Canvas
	{
		public Bitmap Bitmap;

		public Canvas(int w, int h)
		{
			this.Bitmap = new Bitmap(w, h);
		}

		public Canvas(Bitmap bmp)
		{
			this.Bitmap = bmp;
		}

		public int W
		{
			get
			{
				return this.Bitmap.Width;
			}
		}

		public int H
		{
			get
			{
				return this.Bitmap.Height;
			}
		}

		public void DrawImage(Bitmap image, int x, int y)
		{
			this.Fill(Color.Transparent, new I4Rect(x, y, image.Width, image.Height));

			using (Graphics g = Graphics.FromImage(this.Bitmap))
			{
				g.DrawImage(image, x, y);
			}
		}

		public void DrawString(string text, int fontSize, Color color, int x, int y)
		{
			using (Graphics g = Graphics.FromImage(this.Bitmap))
			{
				g.DrawString(text, new Font("メイリオ", fontSize, FontStyle.Regular), new SolidBrush(color), new Point(x, y));
			}
		}

		/// <summary>
		/// 指定サイズに拡大・縮小する。
		/// </summary>
		/// <param name="w">目的の幅</param>
		/// <param name="h">目的の高さ</param>
		public void Expand(int w, int h)
		{
			this.Bitmap = Expand(this.Bitmap, w, h);
		}

		private static Bitmap Expand(Bitmap src, int w, int h)
		{
			Bitmap dest = new Bitmap(w, h);

			new Canvas(dest).Fill(Color.Transparent);

			using (Graphics g = Graphics.FromImage(dest))
			{
				g.InterpolationMode = InterpolationMode.HighQualityBicubic; // 一番綺麗らしい。
				g.DrawImage(src, 0, 0, w, h);
			}
			return dest;
		}

		/// <summary>
		/// 指定サイズに拡大・縮小する。
		/// 縦横比を維持する。
		/// </summary>
		/// <param name="w">目的の幅</param>
		/// <param name="h">目的の高さ</param>
		public void ExpandKAR(int w, int h, Color backColor)
		{
			I4Rect interior;
			I4Rect exterior;

			Common.AdjustRect(new I2Size(this.W, this.H), new I4Rect(0, 0, w, h), out interior, out exterior);

			this.Expand(interior.W, interior.H);
			this.PutMargin(backColor, new Margin(interior.L, interior.T, w - interior.R, h - interior.B));
		}

		public void Fill(Color color)
		{
			this.Fill(color, new I4Rect(0, 0, this.W, this.H));
		}

		public void Fill(Color color, I4Rect rect)
		{
			ProcMain.WriteLog("Fill.1"); // cout

			for (int x = 0; x < rect.W; x++)
			{
				for (int y = 0; y < rect.H; y++)
				{
					this.Bitmap.SetPixel(rect.L + x, rect.T + y, color);
				}
			}
			ProcMain.WriteLog("Fill.2"); // cout
		}

		public I4Rect GetEntityRect(Color marginColor)
		{
			int xMin = this.W;
			int xMax = -1;
			int yMin = this.H;
			int yMax = -1;

			for (int x = 0; x < this.W; x++)
			{
				for (int y = 0; y < this.H; y++)
				{
					Color pixel = this.Bitmap.GetPixel(x, y);

					if (!Common.IsSameColor(pixel, marginColor))
					{
						xMin = Math.Min(xMin, x);
						xMax = Math.Max(xMax, x);
						yMin = Math.Min(yMin, y);
						yMax = Math.Max(yMax, y);
					}
				}
			}

			if (xMax == -1)
				throw new Exception("no entity");

			return I4Rect.LTRB(xMin, yMin, xMax + 1, yMax + 1);
		}

		/// <summary>
		/// このキャンバスの指定矩形領域を切り取って、自身に再設定する。
		/// </summary>
		/// <param name="rect">矩形領域</param>
		public void Cut(I4Rect rect)
		{
			this.Bitmap = Cut(this.Bitmap, rect);
		}

		private static Bitmap Cut(Bitmap src, I4Rect rect)
		{
			if (
				rect.L < 0 ||
				rect.T < 0 ||
				src.Width < rect.R ||
				src.Height < rect.B
				)
				throw new ArgumentException("Bad rect");

			Bitmap dest = new Bitmap(rect.W, rect.H);

			using (Graphics g = Graphics.FromImage(dest))
			{
				g.DrawImage(src, new Rectangle(0, 0, rect.W, rect.H), rect.ToRectangle(), GraphicsUnit.Pixel);
			}
			return dest;
		}

		/// <summary>
		/// マージンを追加する。
		/// </summary>
		/// <param name="marginColor">マージン部分の色</param>
		/// <param name="margin">マージン</param>
		public void PutMargin(Color marginColor, Margin margin)
		{
			this.Bitmap = PutMargin(this.Bitmap, marginColor, margin);
		}

		private static Bitmap PutMargin(Bitmap src, Color marginColor, Margin margin)
		{
			Canvas canvas = new Canvas(
				margin.L + src.Width + margin.R,
				margin.T + src.Height + margin.B
				);

			canvas.Fill(marginColor);
			canvas.DrawImage(src, margin.L, margin.T);

			return canvas.Bitmap;
		}
	}
}
