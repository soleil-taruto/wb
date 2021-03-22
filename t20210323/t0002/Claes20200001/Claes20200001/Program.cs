using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
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
			// -- choose one --

			Main3();
			//new Test0001().Test01();
			//new Test0001().Test02();
			//new Test0002().Test01();

			// --

			//Console.WriteLine("Press ENTER key.");
			//Console.ReadLine();

			Common.OpenOutputDirIfCreated();
		}

		#region TREE

		private static string TREE = @"

C:\BlueFish\BlueFish\HTT\cerulean\home\beta
C:\BlueFish\BlueFish\HTT\cerulean\home\charlotte
C:\BlueFish\BlueFish\HTT\cerulean\home\charlotte\Actor83
C:\BlueFish\BlueFish\HTT\cerulean\home\charlotte\G4Actor8301
C:\BlueFish\BlueFish\HTT\cerulean\home\charlotte\G4Adventure
C:\BlueFish\BlueFish\HTT\cerulean\home\charlotte\G4Dungeon
C:\BlueFish\BlueFish\HTT\cerulean\home\charlotte\G4NovelAdv
C:\BlueFish\BlueFish\HTT\cerulean\home\charlotte\G4RetroRPG
C:\BlueFish\BlueFish\HTT\cerulean\home\charlotte\G4TateShoot
C:\BlueFish\BlueFish\HTT\cerulean\home\charlotte\G4YokoActTK
C:\BlueFish\BlueFish\HTT\cerulean\home\charlotte\G4YokoActTM
C:\BlueFish\BlueFish\HTT\cerulean\home\charlotte\G4YokoShoot
C:\BlueFish\BlueFish\HTT\cerulean\home\charlotte\Hako
C:\BlueFish\BlueFish\HTT\cerulean\home\charlotte\Hako2
C:\BlueFish\BlueFish\HTT\cerulean\home\charlotte\ShirokumaACT
C:\BlueFish\BlueFish\HTT\cerulean\home\source
C:\BlueFish\BlueFish\HTT\cerulean\home\tools
C:\BlueFish\BlueFish\HTT\cerulean\home\tools\Dummy

";

		#endregion

		private void Main3()
		{
			string[] dirs = SCommon.TextToLines(TREE)
				.Where(line => line != "")
				.Select(line => line.Substring(@"C:\BlueFish\BlueFish\HTT\cerulean\home\".Length))
				.ToArray();

			string destRootDir = Path.Combine(Common.GetOutputDir(), "cc_home");

			SCommon.CreateDir(destRootDir);
			MakeIndex(destRootDir, "/");

			foreach (string dir in dirs)
			{
				string destDir = Path.Combine(destRootDir, dir);

				SCommon.CreateDir(destDir);
				MakeIndex(destDir, ":58946/" + dir.Replace('\\', '/'));
			}
		}

		private void MakeIndex(string destDir, string dirPart)
		{
			string indexFile = Path.Combine(destDir, "index.html");
			string url = "http://cerulean.ccsp.mydns.jp" + dirPart;

			File.WriteAllText(
				indexFile,
				@"<html>
<head>
<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8""/>
<meta name=""robots"" content=""noindex"">
</head>
<body>
<h1>移転しました。</h1>
移転先はこちら ⇒ <a href=""" + url + @""">" + url + @"</a>
</body>
</html>
",
				Encoding.UTF8
				);
		}
	}
}
