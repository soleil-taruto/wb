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

		public static EventWaitHandle Ev停止 = new EventWaitHandle(false, EventResetMode.AutoReset, "{34e2ebab-db4f-4181-a5bb-a56ceca43eef}");

		public static Setting Setting = new Setting();
		public static bool SaveSettingRequest = false;

		public static List<PerfCntrInfo> ReadPerfCntrList = new List<PerfCntrInfo>();
		public static List<PerfCntrInfo> WritePerfCntrList = new List<PerfCntrInfo>();
	}
}
