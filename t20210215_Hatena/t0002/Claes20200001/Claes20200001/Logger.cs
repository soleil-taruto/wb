using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte
{
	public class Logger
	{
		public void Clear()
		{
			if (File.Exists(Consts.LOG_FILE))
				File.Delete(Consts.LOG_FILE);
		}

		/// <summary>
		/// ログファイルに行リストを書き込む
		/// 但し、後に書き込んだ行ほどファイルの先頭に近くなるようにする。
		/// </summary>
		/// <param name="lines">行リスト</param>
		public virtual void WriteLog(string[] lines)
		{
			if (File.Exists(Consts.LOG_FILE))
			{
				if (File.Exists(Consts.LOG_FILE_TMP))
					File.Delete(Consts.LOG_FILE_TMP); // 念のため

				File.Move(Consts.LOG_FILE, Consts.LOG_FILE_TMP);

				using (StreamReader reader = new StreamReader(Consts.LOG_FILE_TMP, Encoding.UTF8))
				using (StreamWriter writer = new StreamWriter(Consts.LOG_FILE, false, Encoding.UTF8))
				{
					foreach (string line in lines.Reverse())
						writer.WriteLine(line);

					for (string line; (line = reader.ReadLine()) != null; )
						writer.WriteLine(line);
				}
				File.Delete(Consts.LOG_FILE_TMP);
			}
			else
			{
				File.WriteAllLines(Consts.LOG_FILE, lines.Reverse(), Encoding.UTF8);
			}
		}
	}
}
