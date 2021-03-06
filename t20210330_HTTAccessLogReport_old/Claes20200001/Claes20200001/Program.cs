﻿using System;
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
				Main4(ar);
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

		private int RankMax;
		private int LogLineLenMax;

		private void Main4(ArgsReader ar)
		{
			RankMax = int.Parse(ar.NextArg());
			LogLineLenMax = int.Parse(ar.NextArg());

			if (!Directory.Exists(Consts.LOG_DIR))
				throw new Exception("no LOG_DIR");

			string[] logFiles = P_GetFiles(Consts.LOG_DIR);
			//string[] logFiles = Directory.GetFiles(Consts.LOG_DIR); // old

			Array.Sort(logFiles, SCommon.CompIgnoreCase);

			if (1 <= logFiles.Length)
				logFiles = logFiles.Take(logFiles.Length - 1).ToArray(); // 最新のファイルは出力中かもしれないので除外する。

			EraseKnownLogFiles(ref logFiles);
			AggregateAndReport(logFiles);
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

		private void AggregateAndReport(string[] logFiles)
		{
			using (Aggregate aggrIP = new Aggregate())
			using (Aggregate aggrRequest = new Aggregate())
			using (Aggregate aggrHost = new Aggregate())
			using (Aggregate aggrAgent = new Aggregate())
			using (Aggregate aggrDomain = new Aggregate())
			using (Aggregate aggrPath = new Aggregate())
			using (Aggregate aggrStatus = new Aggregate())
			{
				foreach (string logFile in logFiles)
				{
					using (StreamReader reader = new StreamReader(logFile, SCommon.ENCODING_SJIS))
					{
						foreach (string line in IgnoreLocalIPRequest(ReadLogLines(reader)))
						{
							if (IsIPLine(line))
							{
								aggrIP.Add(line);
							}
							else if (line[0] == 'R')
							{
								aggrRequest.Add(line.Substring(1));
							}
							else if (line[0] == 'H')
							{
								aggrHost.Add(line.Substring(1));
							}
							else if (line[0] == 'A')
							{
								aggrAgent.Add(line.Substring(1));
							}
							else if (line[0] == 'D')
							{
								aggrDomain.Add(line.Substring(1));
							}
							else if (line[0] == 'P')
							{
								aggrPath.Add(line.Substring(1));
							}
							else if (line[0] == 'S')
							{
								aggrStatus.Add(line.Substring(1));
							}
						}
					}
				}
				PrintReport(aggrIP, "IP");
				PrintReport(aggrRequest, "Request");
				PrintReport(aggrHost, "Host");
				PrintReport(aggrAgent, "Agent");
				PrintReport(aggrDomain, "Domain");
				PrintReport(aggrPath, "Path");
				PrintReport(aggrStatus, "Status");
			}
		}

		private IEnumerable<string> ReadLogLines(StreamReader reader)
		{
			for (; ; )
			{
				string line = reader.ReadLine();

				if (line == null)
					break;

				line = SCommon.ToJString(SCommon.ENCODING_SJIS.GetBytes(line), true, false, false, true); // 2bs?

				if (line == "")
					continue;

				line = Common.CutTrail(line, LogLineLenMax);

				yield return line;
			}
		}

		private IEnumerable<string> IgnoreLocalIPRequest(IEnumerable<string> lines)
		{
			const string LOCAL_IP = "192.168.123.254"; // 室内(内側)ルータからのリクエストのIPアドレス

			bool allowFlag = true;

			foreach (string line in lines)
			{
				if (line == LOCAL_IP)
					allowFlag = false;
				else if (IsIPLine(line))
					allowFlag = true;

				if (allowFlag)
					yield return line;
			}
		}

		private void PrintReport(Aggregate aggr, string title)
		{
			Console.WriteLine("[" + title + "]");

			foreach (string line in aggr.Top(RankMax))
			{
				Console.WriteLine(line);
			}
		}

		private static bool IsIPLine(string line)
		{
			return Regex.IsMatch(line, @"^[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+$");
		}
	}
}
