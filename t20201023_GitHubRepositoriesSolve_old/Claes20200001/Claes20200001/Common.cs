﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Commons;

namespace Charlotte
{
	public static class Common
	{
		public static IEnumerable<string> GetRepositoryFiles(string dir)
		{
			string[] files = P_GetRepositoryFiles(dir);

			Console.Write("[*");

			const int STAR_MAX = 76;
			int star = 0;

			for (int index = 0; index < files.Length; index++)
			{
				int targStar = (index * STAR_MAX) / files.Length;

				while (star < targStar)
				{
					Console.Write("*");
					star++;
				}
				yield return files[index];
			}
			while (star < STAR_MAX)
			{
				Console.Write("*");
				star++;
			}
			Console.WriteLine("]");
		}

		private static string[] P_GetRepositoryFiles(string dir)
		{
			return E_GetRepositoryFiles(dir).ToArray();
		}

		private static IEnumerable<string> E_GetRepositoryFiles(string dir)
		{
			foreach (string subDir in Directory.GetDirectories(dir))
			{
				if (SCommon.EqualsIgnoreCase(Path.GetFileName(subDir), ".git"))
					continue;

				foreach (string file in Directory.GetFiles(subDir, "*", SearchOption.AllDirectories))
					yield return file;
			}
			foreach (string file in Directory.GetFiles(dir))
			{
				if (SCommon.EqualsIgnoreCase(Path.GetFileName(file), ".gitattributes"))
					continue;

				yield return file;
			}
		}

		public static IEnumerable<string> ReadAllLines_SJIS(string file)
		{
			const int LINE_LEN_MAX = 1000;

			using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
			{
				byte[] buff = new byte[LINE_LEN_MAX];

				for (; ; )
				{
					int index = 0;

					// 行の長さが LINE_LEN_MAX バイトを超える場合 LINE_LEN_MAX バイト目と LINE_LEN_MAX + 1 バイト目の間に改行があるものとして処理する。

					while (index < LINE_LEN_MAX)
					{
						int chr = reader.ReadByte();

						if (chr == -1)
						{
							if (index == 0)
								goto endLoop;

							break;
						}
						if (chr == 0x0d) // ? CR
							continue;

						if (chr == 0x0a) // ? LF
							break;

						buff[index++] = (byte)chr;
					}
					byte[] l_buff = SCommon.GetSubBytes(buff, 0, index);
					string line = SCommon.ToJString(l_buff, true, false, true, true);
					byte[] b_line = SCommon.ENCODING_SJIS.GetBytes(line);

					if (SCommon.Comp(l_buff, b_line) != 0) // ? buff は SJIS ではない。
						break;

					yield return line;
				}
			endLoop:
				;
			}
		}

		public static byte[] PutUTF8Bom(byte[] bText)
		{
			byte[] BOM = new byte[] { 0xef, 0xbb, 0xbf };

			// ? bText has BOM
			if (
				3 <= bText.Length &&
				bText[0] == BOM[0] &&
				bText[1] == BOM[1] &&
				bText[2] == BOM[2]
				)
			{
				// noop
			}
			else
			{
				bText = BOM.Concat(bText).ToArray();
			}
			return bText;
		}
	}
}
