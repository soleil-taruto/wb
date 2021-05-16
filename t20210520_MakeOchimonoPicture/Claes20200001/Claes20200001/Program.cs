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

		private string RFile = @"C:\Dev\Henrietta\e20190002_Ochimono\dat\補助\Dummy.png";
		private string Res_WDirs = @"

C:\Dev\Henrietta\e20190002_Ochimono\dat\Puzzle\Pair
C:\Dev\Henrietta\e20190002_Ochimono\dat\Puzzle\Solo
C:\Dev\Henrietta\e20190002_Ochimono\dat\Puzzle\必殺
C:\Dev\Henrietta\e20190002_Ochimono\dat\System\Config
C:\Dev\Henrietta\e20190002_Ochimono\dat\カットイン
C:\Dev\Henrietta\e20190002_Ochimono\dat\シナリオ\System
C:\Dev\Henrietta\e20190002_Ochimono\dat\シナリオ\エンディング
C:\Dev\Henrietta\e20190002_Ochimono\dat\シナリオ\画像
C:\Dev\Henrietta\e20190002_Ochimono\dat\シナリオ\選択肢

";

		private void Main4()
		{
			string[] wDirs = SCommon.TextToLines(Res_WDirs)
				.Where(wDir => wDir != "")
				.ToArray();

			if (!File.Exists(RFile))
				throw new Exception("no RFile");

			if (wDirs.Any(wDir => !Directory.Exists(wDir)))
				throw new Exception("Bad wDirs");

			byte[] fileData = File.ReadAllBytes(RFile);
			string[] wFiles = SCommon.Concat(wDirs.Select(wDir => Directory.GetFiles(wDir, "*.png", SearchOption.AllDirectories))).ToArray();

			foreach (string wFile in wFiles)
				File.WriteAllBytes(wFile, fileData);
		}
	}
}
