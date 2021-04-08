﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AccessLamp
{
	public static class Logger
	{
		private static string LOG_FILE
		{
			get
			{
				return Path.Combine(Environment.GetEnvironmentVariable("TMP"), Program.APP_IDENT + ".log");
			}
		}

		private const long LOG_FILE_SIZE_MAX = 1000000;

		public static void Clear()
		{
			try
			{
				File.WriteAllBytes(LOG_FILE, new byte[0]); // ファイルを空にする。
			}
			catch
			{ }
		}

		public static void WriteLog(object message)
		{
			try
			{
				using (StreamWriter writer = new StreamWriter(LOG_FILE, File.Exists(LOG_FILE) && new FileInfo(LOG_FILE).Length < LOG_FILE_SIZE_MAX, Encoding.UTF8))
				{
					writer.WriteLine("[" + DateTime.Now + "] " + message);
				}
			}
			catch
			{ }
		}
	}
}
