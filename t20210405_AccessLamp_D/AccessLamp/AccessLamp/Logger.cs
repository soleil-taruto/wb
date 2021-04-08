using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AccessLamp
{
	public class Logger : IDisposable
	{
		private const long LOG_FILE_SIZE_MAX = 1000000;
		private string LogFile;

		public Logger()
		{
			string tmpDir = Environment.GetEnvironmentVariable("TMP");

			if (string.IsNullOrEmpty(tmpDir) || !Directory.Exists(tmpDir))
				throw new Exception("Bad TMP");

			this.LogFile = Path.Combine(tmpDir, Program.APP_IDENT + ".log");
			this.Clear();
		}

		private void Clear()
		{
			try
			{
				File.WriteAllBytes(this.LogFile, new byte[0]); // 空のファイルを作成する。
			}
			catch
			{ }
		}

		// 注意：このインスタンスが生きている間 LogFile は存在しなければならない。

		public void WriteLog(object message)
		{
			try
			{
				using (StreamWriter writer = new StreamWriter(this.LogFile, new FileInfo(this.LogFile).Length < LOG_FILE_SIZE_MAX, Encoding.UTF8))
				{
					writer.WriteLine("[" + DateTime.Now + "] " + message);
				}
			}
			catch
			{ }
		}

		public void Dispose()
		{
			if (this.LogFile != null)
			{
				this.RemoveIfEmpty();
				this.LogFile = null;
			}
		}

		private void RemoveIfEmpty()
		{
			try
			{
				if (new FileInfo(this.LogFile).Length == 0L)
				{
					File.Delete(this.LogFile);
				}
			}
			catch
			{ }
		}
	}
}
