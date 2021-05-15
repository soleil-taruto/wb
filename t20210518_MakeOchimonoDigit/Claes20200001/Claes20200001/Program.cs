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

		#region DigitDesigns

		private string[] DigitDesigns = new string[]
		{
			"0011100:!",
			"0011100:#",
			"0011100:$",
			"0011100:%",
			"0011100:&",
			"1110100:(",
			"0010111:)",
			"0011100:;",
			"0011100:？",
			"0011100:@",
			"1110100:[",
			"0010111:]",
			"0000100:_",
			"0011100:＜",
			"0011100:＞",
			"0011100:↑",
			"0011100:→",
			"0011100:↓",
			"0011100:←",
			"1111011:A",
			"1101101:B",
			"1110100:C",
			"0101111:D",
			"0011100:Dq",
			"1111100:E",
			"1111000:F",
			"1110101:G",
			"1101001:H",
			"0010101:I",
			"0100111:J",
			"1111001:K",
			"1100100:L",
			"0111001:M",
			"0101001:N",
			"0101101:O",
			"1111010:P",
			"1011011:Q",
			"0101000:R",
			"1001101:S",
			"0011100:Sq",
			"0011100:Star",
			"1101100:T",
			"1100111:U",
			"0100101:V",
			"0110101:W",
			"0011100:wo",
			"1101011:X",
			"1001111:Y",
			"0110110:Z",
			"1110111:数字_0",
			"0000011:数字_1",
			"0111110:数字_2",
			"0011111:数字_3",
			"1001011:数字_4",
			"1011101:数字_5",
			"1111101:数字_6",
			"1010011:数字_7",
			"1111111:数字_8",
			"1011111:数字_9",
		};

		#endregion

		private void Main4()
		{
			if (!Directory.Exists(Consts.ROOT_DIR))
				throw new Exception("no ROOT_DIR");

			foreach (string digDsgn in DigitDesigns)
			{
				string[] tokens = digDsgn.Split(':');
				string dsgn = tokens[0];
				string name = tokens[1];

				MakeDigit(dsgn.Select(chr => chr == '1').ToArray(), name);
			}
		}

		private I4Color BACK_COLOR = new I4Color(0, 255, 255, 255);
		private I4Color INNER_COLOR = new I4Color(255, 255, 255, 255);
		private I4Color OUTER_COLOR = new I4Color(0, 0, 0, 255);
		private I4Color DARK_SHADOW_COLOR = new I4Color(150, 150, 150, 255);
		private I4Color LIGHT_SHADOW_COLOR = new I4Color(200, 200, 200, 255);

		private Canvas _canvas;

		private void MakeDigit(bool[] design, string name)
		{
			_canvas = new Canvas(16, 21);
			_canvas.Fill(BACK_COLOR);

			if (design[0]) DrawLine(0, 0, 0, 1);
			if (design[1]) DrawLine(0, 1, 0, 2);
			if (design[2]) DrawLine(0, 0, 1, 0);
			if (design[3]) DrawLine(0, 1, 1, 1);
			if (design[4]) DrawLine(0, 2, 1, 2);
			if (design[5]) DrawLine(1, 0, 1, 1);
			if (design[6]) DrawLine(1, 1, 1, 2);

			DrawOuter();
			DrawDarkShadow();
			DrawLightShadow();

			_canvas.Save(Path.Combine(Consts.ROOT_DIR, name + ".png"));
		}

		private void DrawLine(int x1, int y1, int x2, int y2)
		{
			x1 *= 2;
			y1 *= 2;
			x2 *= 2; x2++;
			y2 *= 2; y2++;

			Func<int, int> a_x = v => v * 3 + (v / 2) * 3 + 2;
			Func<int, int> a_y = v => v * 3 + (v / 2) * 1 + 2;

			x1 = a_x(x1);
			y1 = a_y(y1);
			x2 = a_x(x2);
			y2 = a_y(y2);

			_canvas.FillRect(I4Rect.LTRB(x1, y1, x2, y2), INNER_COLOR);
		}

		private void DrawOuter()
		{
			Draw(BACK_COLOR, (x, y) =>
			{
				for (int xc = -1; xc <= 1; xc++)
				{
					for (int yc = -1; yc <= 1; yc++)
					{
						int sx = x + xc;
						int sy = y + yc;

						I4Color dot = _canvas[sx, sy];

						if (dot.IsSame(INNER_COLOR))
						{
							_canvas[x, y] = OUTER_COLOR;
							return;
						}
					}
				}
			});
		}

		private void DrawDarkShadow()
		{
			Draw(INNER_COLOR, (x, y) =>
			{
				Func<int, int, bool> a_cond = (xc, yc) =>
				{
					int sx = x + xc;
					int sy = y + yc;

					I4Color dot = _canvas[sx, sy];

					return dot.IsSame(OUTER_COLOR);
				};

				if (
					a_cond(1, 0) ||
					a_cond(0, 1) ||
					a_cond(1, 1)
					)
					_canvas[x, y] = DARK_SHADOW_COLOR;
			});
		}

		private void DrawLightShadow()
		{
			Draw(INNER_COLOR, (x, y) =>
			{
				Func<int, int, bool> a_cond = (xc, yc) =>
				{
					int sx = x + xc;
					int sy = y + yc;

					I4Color dot = _canvas[sx, sy];

					return
						dot.IsSame(OUTER_COLOR) ||
						dot.IsSame(DARK_SHADOW_COLOR);
				};

				if (
					a_cond(-1, -1) ||
					a_cond(0, -1) ||
					a_cond(-1, 0)
					)
					_canvas[x, y] = LIGHT_SHADOW_COLOR;
			});
		}

		private void Draw(I4Color targetDot, Action<int, int> a_drawDot)
		{
			for (int x = 1; x < _canvas.W - 1; x++)
			{
				for (int y = 1; y < _canvas.H - 1; y++)
				{
					if (_canvas[x, y].IsSame(targetDot))
					{
						a_drawDot(x, y);
					}
				}
			}
		}
	}
}
