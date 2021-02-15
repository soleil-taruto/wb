using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte
{
	public class MutexLoggerSimple : Logger, IDisposable
	{
		private Mutex M = new Mutex(false, Consts.MUTEX_M);

		public override void WriteLog(string[] lines)
		{
			M.WaitOne();
			base.WriteLog(lines);
			M.ReleaseMutex();
		}

		public void Dispose()
		{
			if (M != null)
			{
				M.Dispose();
				M = null;
			}
		}
	}
}
