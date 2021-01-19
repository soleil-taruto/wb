using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Commons;

namespace Charlotte
{
	public static class Common
	{
		public static string[] ReadConfigFile(string file)
		{
			string[] lines = File.ReadAllLines(file, SCommon.ENCODING_SJIS).Where(line => line != "" && line[0] != ';').ToArray();

			// 有効設定項目数のチェック
			//
			if (int.Parse(lines[0]) != lines.Length)
				throw new Exception("Bad Config File");

			return lines.Skip(1).ToArray(); // 有効設定項目数を除去
		}
	}
}
