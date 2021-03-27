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
			//Common.OpenOutputDirIfCreated();
		}

		private void Main3()
		{
			// -- choose one --

			new Test0001().Test01();
			//new Test0002().Test01();
			//new Test0003().Test01();

			// --

			//Common.Pause();
		}

		private void Main4()
		{
			if (!Directory.Exists(Consts.LOG_DIR))
				throw new Exception("no LOG_DIR");

			string[] logFiles = P_GetFiles(Consts.LOG_DIR);
			//string[] logFiles = Directory.GetFiles(Consts.LOG_DIR); // old

			Array.Sort(logFiles, SCommon.CompIgnoreCase);

			if (1 <= logFiles.Length)
				logFiles = logFiles.Take(logFiles.Length - 1).ToArray(); // 最新のファイルは出力中かもしれないので除外する。

			EraseKnownLogFiles(ref logFiles);
			CollectReport(logFiles);
			PrintReportLines();
		}

		public string[] P_GetFiles(string dir)
		{
			using (WorkingDir wd = new WorkingDir())
			{
				string outFile = wd.MakePath();
				SCommon.Batch(new string[] { "DIR /B > \"" + outFile + "\"" }, dir);
				string[] files = File.ReadAllLines(outFile, SCommon.ENCODING_SJIS);
				files = files.Select(file => Path.Combine(dir, file)).ToArray();
				return files;
			}
		}

		private void EraseKnownLogFiles(ref string[] logFiles)
		{
			if (File.Exists(Consts.LAST_LOG_FILE_SAVE_FILE))
			{
				string lastLogFile = File.ReadAllText(Consts.LAST_LOG_FILE_SAVE_FILE, Encoding.UTF8);
				int p = SCommon.IndexOf(logFiles, logFile => SCommon.EqualsIgnoreCase(logFile, lastLogFile));

				if (p != -1)
					logFiles = logFiles.Skip(p + 1).ToArray(); // pまで(pを含めて)除去する。
			}
			if (1 <= logFiles.Length)
				File.WriteAllText(Consts.LAST_LOG_FILE_SAVE_FILE, logFiles[logFiles.Length - 1], Encoding.UTF8); // 更新
		}

		private void CollectReport(string[] logFiles)
		{
			throw new NotImplementedException();
		}

		private void PrintReportLines()
		{
			throw new NotImplementedException();
		}
	}
}
