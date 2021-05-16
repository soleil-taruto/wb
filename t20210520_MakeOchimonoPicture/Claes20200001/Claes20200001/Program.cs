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

			MakePauseButton(@"System\Button\ゲームに戻る.png", "　ゲームに戻る");
			MakePauseButton(@"System\Button\ゲームに戻る選択中.png", "◆ゲームに戻る");
			MakePauseButton(@"System\Button\ゲームを終了する.png", "　ゲームを終了する");
			MakePauseButton(@"System\Button\ゲームを終了する選択中.png", "◆ゲームを終了する");
			MakePauseButton(@"System\Button\タイトルに戻る.png", "　タイトルに戻る");
			MakePauseButton(@"System\Button\タイトルに戻る選択中.png", "◆タイトルに戻る");

			MakeTextPanel(@"System\ロード中.png", 400, 80, 190, "Impact", FontStyle.Regular, new I3Color(255, 255, 255), "Now Loading...");

			MakeTextPanel(@"画像\ready.png", 800, 600, 800, "Impact", FontStyle.Bold, new I3Color(255, 255, 0), new I3Color(0, 0, 255), 40, "READY", 0, 600);

			MakeTextPanel(@"画像\勝利.png", 400, 600, 300, "Impact", FontStyle.Regular, new I3Color(255, 128, 0), new I3Color(255, 255, 0), 40, "WINNER!", 20, 950);
			MakeTextPanel(@"画像\敗北.png", 400, 600, 300, "Impact", FontStyle.Regular, new I3Color(0, 255, 255), new I3Color(0, 0, 255), 40, "LOSER...", 120, 950, new I4Color(0, 0, 0, 100));
		}

		private void MakePauseButton(string relFile, string title)
		{
			string file = Path.Combine(Consts.ROOT_DIR, relFile);
			Canvas canvas = new Canvas(226 * 4, 33 * 4);

			canvas.Fill(new I4Color(0, 0, 0, 0));
			canvas = canvas.DrawString(title, 70, "メイリオ", FontStyle.Bold, new I4Color(255, 255, 255, 255), 0, 0);
			canvas = canvas.Expand(226, 33);

			canvas.Save(file);
		}

		private void MakeTextPanel(string relFile, int w, int h, int fontSize, string fontName, FontStyle fontStyle, I3Color color, string text, int x = 0, int y = 0)
		{
			const int EXPAND_RATE = 4;

			string file = Path.Combine(Consts.ROOT_DIR, relFile);
			Canvas canvas = new Canvas(w * EXPAND_RATE, h * EXPAND_RATE);

			canvas.Fill(new I4Color(0, 0, 0, 0)); // 本番
			//canvas.Fill(new I4Color(0, 0, 0, 255)); // テスト用

			canvas = canvas.DrawString(text, fontSize, fontName, fontStyle, color.WithAlpha(), x, y);
			canvas = canvas.Expand(w, h);

			canvas.Save(file);
		}

		private void MakeTextPanel(string relFile, int w, int h, int fontSize, string fontName, FontStyle fontStyle, I3Color color, I3Color borderColor, int borderWidth, string text, int x, int y, I4Color? backColor = null)
		{
			const int EXPAND_RATE = 4;

			string file = Path.Combine(Consts.ROOT_DIR, relFile);
			Canvas canvas = new Canvas(w * EXPAND_RATE, h * EXPAND_RATE);

			if (backColor != null)
			{
				canvas.Fill(backColor.Value);
			}
			else
			{
				canvas.Fill(new I4Color(0, 0, 0, 0)); // 本番
				//canvas.Fill(new I4Color(0, 0, 0, 255)); // テスト用
			}

			for (int xc = -1; xc <= 1; xc++)
			{
				for (int yc = -1; yc <= 1; yc++)
				{
					ProcMain.WriteLog("*1");
					canvas = canvas.DrawString(text, fontSize, fontName, fontStyle, borderColor.WithAlpha(), x + xc * borderWidth, y + yc * borderWidth);
				}
			}
			ProcMain.WriteLog("*2");
			canvas = canvas.DrawString(text, fontSize, fontName, fontStyle, color.WithAlpha(), x, y);
			ProcMain.WriteLog("*3");
			canvas = canvas.Expand(w, h);

			canvas.Save(file);
		}
	}
}
