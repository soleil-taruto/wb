using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
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

		private EventWaitHandle EvStop;

		private void Main4(ArgsReader ar)
		{
			ProcMain.WriteLog("HTTAccessLogLog.1");

			EvStop = new EventWaitHandle(false, EventResetMode.AutoReset, "{5d192915-d80d-47f6-9c73-28fbe6809a80}");

			if (ar.ArgIs("/S"))
				EvStop.Set();
			else
				ServiceWatch();

			EvStop.Dispose();
			EvStop = null;

			ProcMain.WriteLog("HTTAccessLogLog.2");
		}

		private void ServiceWatch()
		{
			ProcMain.WriteLog("ServiceWatch.1");

			if (!Directory.Exists(Consts.HTT_DIR))
				throw new Exception("no HTT_DIR");

			if (!Directory.Exists(Consts.DEST_LOG_DIR))
				throw new Exception("no LOG_DIR");

			FileSystemWatcher watcher = new FileSystemWatcher(Consts.HTT_DIR);

			watcher.NotifyFilter =
				NotifyFilters.Attributes |
				NotifyFilters.CreationTime |
				NotifyFilters.DirectoryName |
				NotifyFilters.FileName |
				NotifyFilters.LastAccess |
				NotifyFilters.LastWrite |
				NotifyFilters.Security |
				NotifyFilters.Size |
				0;

			bool modified;

			watcher.Changed += (sender, ev) =>
			{
				ProcMain.WriteLog("W_Changed");
				modified = true;
			};

			watcher.Created += (sender, ev) =>
			{
				ProcMain.WriteLog("W_CREATED");
				modified = true;
			};

			watcher.Deleted += (sender, ev) =>
			{
				ProcMain.WriteLog("W_DELETED");
				modified = true;
			};

			watcher.Renamed += (sender, ev) =>
			{
				ProcMain.WriteLog("W_Renamed");
				modified = true;
			};

			watcher.Error += (sender, ex) =>
			{
				ProcMain.WriteLog("W_Error_ex: " + ex + " (処理続行)");
			};

			watcher.Filter = Consts.ACCESS_LOG_FILE_FILTER;
			watcher.IncludeSubdirectories = false;

			watcher.EnableRaisingEvents = true; // 監視開始

			ProcMain.WriteLog("ServiceWatch.2");

			for (; ; )
			{
				modified = false;

				for (int loopcnt = 0; loopcnt < 30; loopcnt++)
				{
					if (modified)
						break;

					if (EvStop.WaitOne(2000))
						goto endWatch;
				}
				ProcMain.WriteLog("modified: " + modified);
				ServiceOperation();
				GC.Collect();
			}
		endWatch:
			ProcMain.WriteLog("ServiceWatch.3");

			watcher.EnableRaisingEvents = false; // 監視停止

			ProcMain.WriteLog("ServiceWatch.4");
		}

		private void ServiceOperation()
		{
			ProcMain.WriteLog("ServiceOperation.1");

			SO_MoveLog(Consts.ACCESS_LOG_FILE_02); // 古いログから処理する。
			SO_MoveLog(Consts.ACCESS_LOG_FILE_01);

			ProcMain.WriteLog("ServiceOperation.2");
		}

		private void SO_MoveLog(string logFile)
		{
			ProcMain.WriteLog("SO_MoveLog.1");

			using (Mutex mutex = new Mutex(false, Consts.ACCESS_LOG_MUTEX_NAME))
			{
				if (!File.Exists(logFile))
					File.WriteAllBytes(logFile, SCommon.EMPTY_BYTES);

				string[] lines = File.ReadAllLines(logFile, SCommon.ENCODING_SJIS)
					.Where(line => line != "")
					.ToArray();

				SO_WriteLogLines(lines);

				File.Delete(logFile); // 2bs
				File.WriteAllBytes(logFile, SCommon.EMPTY_BYTES); // 存在を忘れないように空ファイルとして残しておく
			}
			ProcMain.WriteLog("SO_MoveLog.2");
		}

		private long SO_WLL_FileCounter;

		private void SO_WriteLogLines(string[] lines)
		{
			int dateAndHour = (int)(SCommon.TimeStampToSec.ToTimeStamp(DateTime.Now) / 10000);
			long fileCounter = (long)dateAndHour * 1000;
			SO_WLL_FileCounter = Math.Max(SO_WLL_FileCounter, fileCounter);

			string wFile = Path.Combine(Consts.DEST_LOG_DIR, "HTTAccessLogLog_" + SO_WLL_FileCounter + ".log");

			ProcMain.WriteLog("wFile: " + wFile);

			File.AppendAllLines(wFile, lines, SCommon.ENCODING_SJIS);

			if (5000000 < new FileInfo(wFile).Length) // ? 5 MB < // 1つのファイルが際限なく大きくならないように
			{
				ProcMain.WriteLog("Increment FileCounter");
				SO_WLL_FileCounter++;
			}
		}
	}
}
