using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace Charlotte.Tests
{
	public class ProcessingTimeTest
	{
		public void Test00()
		{
			Perform(new MutexLoggerComplex());
			Perform(new MutexLoggerComplex());
			Perform(new MutexLoggerComplex());
		}

		public void Test01()
		{
			const int TEST_CNT = 10;

			for (int c = 0; c < TEST_CNT; c++)
				Perform(new MutexLoggerSimple());

			for (int c = 0; c < TEST_CNT; c++)
				Perform(new MutexLoggerComplex());
		}

		public void Test02()
		{
			const int TEST_CNT = 10;

			for (int c = 0; c < TEST_CNT; c++)
			{
				Perform(new MutexLoggerSimple());
				Perform(new MutexLoggerComplex());
			}
		}

		// ==== ==== ====
		// ==== ==== ====
		// ==== ==== ====

		private const int TH_NUM = 20;
		private const int WR_NUM = 100;
		private const int LN_NUM = 3;

		private void Perform(Logger logger)
		{
			Thread[] ths = Enumerable.Range(1, TH_NUM).Select(thNo => new Thread(() =>
			{
				for (int wrNo = 1; wrNo <= WR_NUM; wrNo++)
				{
					string prefix = thNo + "_" + wrNo + "_";

					logger.WriteLog(new string[]
					{
						prefix + 1,
						prefix + 2,
						prefix + 3, // == LN_NUM
					});
				}
			}
			))
			.ToArray();

			logger.Clear();

			Stopwatch sw = new Stopwatch();
			sw.Start();

			foreach (Thread th in ths)
				th.Start();

			foreach (Thread th in ths)
				th.Join();

			sw.Stop();
			Console.WriteLine(logger + " ---> " + sw.ElapsedMilliseconds / 1000.0 + " 秒");

			CheckLogFile();

			TryDispose(logger);
		}

		private void CheckLogFile()
		{
			string[] lines = File.ReadAllLines(Consts.LOG_FILE, Encoding.UTF8);

			if (lines.Length != TH_NUM * WR_NUM * LN_NUM)
				throw null; // NG !!!

			CLF_Lines = lines.Reverse().ToArray(); // 反転していることに注意！

			for (int thNo = 1; thNo <= TH_NUM; thNo++)
			{
				CLF_ReadIndex = 0;

				for (int wrNo = 1; wrNo <= WR_NUM; wrNo++)
				{
					for (int lnNo = 1; lnNo <= LN_NUM; lnNo++)
					{
						CLF_FindNext(thNo + "_" + wrNo + "_" + lnNo);
					}
				}
			}
		}

		private string[] CLF_Lines;
		private int CLF_ReadIndex;

		private void CLF_FindNext(string targLine)
		{
			// 見つからなければ例外を投げる。-- NG !!!

			while (CLF_Lines[CLF_ReadIndex++] != targLine)
			{ }
		}

		private static void TryDispose(object instance)
		{
			IDisposable d = instance as IDisposable;

			if (d != null)
				d.Dispose();
		}
	}
}
