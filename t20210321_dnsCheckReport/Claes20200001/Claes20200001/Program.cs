using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
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
				TestMain(); // テスト
			}
			else
			{
				ProductMain(ar); // 本番
			}
		}

		private void TestMain()
		{
			// -- choose one --

			new Test0001().Test01();
			//new Test0001().Test02();
			//new Test0001().Test03();

			// --

			Console.WriteLine("Press ENTER to exit.");
			Console.ReadLine();
		}

		private void ProductMain(ArgsReader ar)
		{
			try
			{
				Main3(ar);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		private List<string> ReportLines = new List<string>();

		private void Main3(ArgsReader ar)
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
			string currTimeGroup = null;

			foreach (string logFile in logFiles)
			{
				string timeGroup = CR_GetTimeGroup(logFile);
				int ipCount = CR_GetIPCount(logFile);

				if (timeGroup != currTimeGroup)
				{
					ReportLines.Add(timeGroup);
					currTimeGroup = timeGroup;
				}
				ReportLines[ReportLines.Count - 1] += ipCount;
			}
		}

		private string CR_GetTimeGroup(string logFile)
		{
			string name = Path.GetFileNameWithoutExtension(logFile);
			int p = name.IndexOf('_');

			if (p != -1)
				name = name.Substring(p + 1);

			name = name.Substring(0, Math.Min(name.Length, 10));
			name = "[" + name + "] ";
			return name;
		}

		private int CR_GetIPCount(string logFile)
		{
			string[] lines = File.ReadAllLines(logFile, SCommon.ENCODING_SJIS);
			lines = lines.Select(line => CR_EraseTimeStamp(line)).ToArray();
			int ipCount = lines.Where(line => CR_IsIPLine(line)).Count();
			ipCount = Math.Min(ipCount, 9);
			return ipCount;
		}

		private string CR_EraseTimeStamp(string line)
		{
			int p = line.IndexOf(']');

			if (p != -1)
				line = line.Substring(p + 1);

			line = line.Trim();
			return line;
		}

		private bool CR_IsIPLine(string line)
		{
			return Regex.IsMatch(line, @"^[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+$");
		}

		private void PrintReportLines()
		{
			foreach (string line in ReportLines)
				Console.WriteLine(line);
		}
	}
}
