using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AccessLamp
{
	public static class Ground
	{
		public static string SelfFile;
		public static string SelfDir;

		public static EventWaitHandle Ev停止 = new EventWaitHandle(false, EventResetMode.AutoReset, Program.APP_IDENT + "_EV_STOP");

		public static Logger Logger;

		public static Setting Setting = new Setting();
		public static bool SaveSettingRequest = false;

		public static List<PerfCntrInfo> ReadPerfCntrList = new List<PerfCntrInfo>();
		public static List<PerfCntrInfo> WritePerfCntrList = new List<PerfCntrInfo>();
	}
}
