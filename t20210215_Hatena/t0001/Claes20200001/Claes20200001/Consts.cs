using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;

namespace Charlotte
{
	public static class Consts
	{
		public const string LOG_FILE = @"C:\temp\Log.txt";
		public const string LOG_FILE_TMP = @"C:\temp\Log.tmp";

		public const string MUTEX_M = ProcMain.APP_IDENT + "_MUTEX_M"; // ユニークな文字列
		public const string MUTEX_W = ProcMain.APP_IDENT + "_MUTEX_W"; // ユニークな文字列
		public const string MUTEX_C = ProcMain.APP_IDENT + "_MUTEX_C"; // ユニークな文字列
	}
}
