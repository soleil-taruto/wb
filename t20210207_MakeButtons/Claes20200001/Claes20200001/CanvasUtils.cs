using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Charlotte.Commons;

namespace Charlotte
{
	public static class CanvasUtils
	{
		/// <summary>
		/// 指定テキストを指定フォントサイズで描画したキャンバスを返す。
		/// 文字の上下左右は指定マージン
		/// </summary>
		/// <param name="text">テキスト</param>
		/// <param name="fontSize">フォントサイズ</param>
		/// <param name="margin">マージン</param>
		/// <returns>キャンバス</returns>
		public static Canvas GetText(string text, int fontSize, Margin margin, Color backColor, Color textColor)
		{
			int ZEN_CHAR_WH = (int)(fontSize * 1.33); // 全角1文字分の幅と高さ(推定_ピクセル数)

			int w = 1000;
			int h = 500;
			int x = 50;
			int y = 50;

			Canvas canvas;
			I4Rect rect;

			for (; ; )
			{
				bool retry = false;

				canvas = new Canvas(w, h);
				canvas.Fill(backColor);
				canvas.DrawString(text, fontSize, textColor, x, y);

				rect = canvas.GetEntityRect(backColor);

				//canvas.Bitmap.Save(@"C:\temp\1.png"); // test

				if (rect.L == 0) // ? 左に余裕無し
				{
					x += x / 2; // *= 1.5
					retry = true;
				}
				if (rect.T == 0) // ? 上に余裕無し
				{
					y += y / 2; // *= 1.5
					retry = true;
				}

				// 全角1文字分の隙間が無ければ、全文字描画しきれていないと判断する。

				if (canvas.W - ZEN_CHAR_WH <= rect.R) // ? 右に余裕無し
				{
					w += w / 2; // *= 1.5
					retry = true;
				}
				if (canvas.H - ZEN_CHAR_WH <= rect.B) // ? 下に余裕無し
				{
					h += h / 2; // *= 1.5
					retry = true;
				}

				if (!retry)
					break;
			}
			canvas.Cut(rect);
			canvas.PutMargin(backColor, margin);

			return canvas;
		}
	}
}
