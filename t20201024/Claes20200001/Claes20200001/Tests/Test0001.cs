using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tests
{
	public class Test0001
	{
		public void Test01()
		{
			foreach (string drive in Directory.GetLogicalDrives())
			{
				DriveInfo driveInfo = new DriveInfo(drive.Substring(0, 1));

				Console.WriteLine(string.Join(
					", ",
					driveInfo.Name,
					driveInfo.DriveType,
					Common.TryGet(() => driveInfo.DriveFormat, "ERROR"),
					Common.TryGet(() => "" + driveInfo.AvailableFreeSpace, "ERROR"),
					driveInfo.IsReady,
					driveInfo.RootDirectory,
					Common.TryGet(() => "" + driveInfo.TotalFreeSpace, "ERROR"),
					Common.TryGet(() => "" + driveInfo.TotalSize, "ERROR"),
					Common.TryGet(() => "" + driveInfo.VolumeLabel, "ERROR")
					));
			}

			Console.WriteLine("Press ENTER key.");
			Console.ReadLine();
		}
	}
}
