using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte
{
	public class MutexLoggerComplex : Logger, IDisposable
	{
		private Mutex M = new Mutex(false, Consts.MUTEX_M);
		private EventWaitHandle Ev = new EventWaitHandle(false, EventResetMode.AutoReset, Consts.EVENT_M);
		private Thread Th;
		private bool KeepTh = true;

		public MutexLoggerComplex()
		{
			Th = new Thread(WriteLogTh);
			Th.Start();
		}

		private Queue<string[]> LinesQueue = new Queue<string[]>();

		public override void WriteLog(string[] lines)
		{
			M.WaitOne();
			LinesQueue.Enqueue(lines);
			M.ReleaseMutex();
			Ev.Set();
		}

		private void WriteLogTh()
		{
			while (KeepTh)
			{
				Ev.WaitOne(-1);
				TryWriteLog();
			}
			TryWriteLog();
		}

		private void TryWriteLog()
		{
			M.WaitOne();
			Queue<string[]> q = LinesQueue;
			LinesQueue = new Queue<string[]>();
			M.ReleaseMutex();

			//Console.WriteLine(q.Count); // test

			base.WriteLog(Join(q));
		}

		private static string[] Join(Queue<string[]> q)
		{
			List<string> lines = new List<string>();

			while (1 <= q.Count)
				lines.AddRange(q.Dequeue());

			return lines.ToArray();
		}

		public void Dispose()
		{
			if (Ev != null)
			{
				KeepTh = false;
				Ev.Set();
				Th.Join();
				Th = null;

				M.Dispose();
				M = null;

				Ev.Dispose();
				Ev = null;
			}
		}
	}
}
