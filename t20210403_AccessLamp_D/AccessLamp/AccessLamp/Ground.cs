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

		public static EventWaitHandle Ev停止 = new EventWaitHandle(false, EventResetMode.AutoReset, "{a3bee2ed-771c-46d8-ab65-999fcb9e493a}");

		public static Setting Setting = new Setting();
		public static bool SaveSettingRequest = false;

		public static List<PerfCntrInfo> ReadPerfCntrList = new List<PerfCntrInfo>();
		public static List<PerfCntrInfo> WritePerfCntrList = new List<PerfCntrInfo>();
	}
}
