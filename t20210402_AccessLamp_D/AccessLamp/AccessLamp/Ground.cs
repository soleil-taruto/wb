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

		public static EventWaitHandle Ev停止 = new EventWaitHandle(false, EventResetMode.AutoReset, "{e2383ea3-f3d7-4baf-8ace-e6fee2bcfec5}");

		public static Setting Setting = new Setting();
		public static bool SaveSettingRequest = false;

		public static List<PerfCntrInfo> ReadPerfCntrList = new List<PerfCntrInfo>();
		public static List<PerfCntrInfo> WritePerfCntrList = new List<PerfCntrInfo>();
	}
}
