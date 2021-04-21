using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MemoryLamp
{
	public static class Win32
	{
		[StructLayout(LayoutKind.Sequential)]
		private struct MEMORYSTATUSEX
		{
			public uint dwLength;
			public uint dwMemoryLoad;
			public ulong ullTotalPhys;
			public ulong ullAvailPhys;
			public ulong ullTotalPageFile;
			public ulong ullAvailPageFile;
			public ulong ullTotalVirtual;
			public ulong ullAvailVirtual;
			public ulong ullAvailExtendedVirtual;
		}

		[return: MarshalAs(UnmanagedType.Bool)]
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool GlobalMemoryStatusEx(ref MEMORYSTATUSEX ms);

		/// <summary>
		/// 物理メモリ使用率を返す。
		/// </summary>
		/// <returns>物理メモリ使用率(百分率)</returns>
		public static int GetMemoryUsagePct()
		{
			MEMORYSTATUSEX ms = new MEMORYSTATUSEX();

			ms.dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX));

			GlobalMemoryStatusEx(ref ms);

			uint memoryLoad = ms.dwMemoryLoad;
			//ulong memoryFree = ms.ullAvailPhys;
			//ulong memorySize = ms.ullTotalPhys;
			//ulong pageFileFree = ms.ullAvailPageFile;
			//ulong pageFileSize = ms.ullTotalPageFile;
			//ulong virtualFree = ms.ullAvailVirtual;
			//ulong virtualSize = ms.ullTotalVirtual;
			//ulong extendedVirtualFree = ms.ullAvailExtendedVirtual;

			return (int)memoryLoad;
		}
	}
}
