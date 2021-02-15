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
		private Mutex W = new Mutex(false, Consts.MUTEX_W);
		private Mutex C = new Mutex(false, Consts.MUTEX_C);

		private Queue<string[]> LinesQueue = new Queue<string[]>();

		public override void WriteLog(string[] lines)
		{
			C.WaitOne();

			LinesQueue.Enqueue(lines);

			if (!W.WaitOne(0))
			{
				C.ReleaseMutex();
				return;
			}
			C.ReleaseMutex();
			M.WaitOne();
			C.WaitOne();

			Queue<string[]> q = LinesQueue;
			LinesQueue = new Queue<string[]>();

			//Console.WriteLine(q.Count); // test

			W.ReleaseMutex();
			C.ReleaseMutex();

			base.WriteLog(Join(q));

			M.ReleaseMutex();
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
			if (C != null)
			{
				M.Dispose();
				M = null;

				W.Dispose();
				W = null;

				C.Dispose();
				C = null;
			}
		}
	}
}
