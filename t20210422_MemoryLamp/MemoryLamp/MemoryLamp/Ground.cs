using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MemoryLamp
{
	public static class Ground
	{
		public static string SelfFile;
		public static string SelfDir;

		public static EventWaitHandle Ev停止 = new EventWaitHandle(false, EventResetMode.AutoReset, Program.APP_IDENT + "_EV_STOP");
	}
}
