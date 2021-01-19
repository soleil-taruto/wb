using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Commons;
using Charlotte.Tests;

namespace Charlotte
{
	class Program
	{
		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2);
		}

		private void Main2(ArgsReader ar)
		{
			// -- choose one --

			//TestMain(); // テスト
			ProductMain(); // 本番

			// --
		}

		private void TestMain()
		{
			// -- choose one --

			new Test0001().Test01();
			//new Test0001().Test02();
			//new Test0001().Test03();

			// --

			Console.WriteLine("Press ENTER to exit.");
			Console.ReadLine();
		}

		private PasswordConfig PasswordConfig;

		private void ProductMain()
		{
			PasswordConfig = new PasswordConfig();

			string[] repoDirs = Directory.GetDirectories(Consts.ROOT_DIR);

			foreach (string repoDir in repoDirs)
			{
				string gitConfigFile = Path.Combine(repoDir, ".git", "config");

				if (File.Exists(gitConfigFile))
				{
					UpdateGitConfigFile(gitConfigFile);
				}
			}
		}

		private void UpdateGitConfigFile(string gitConfigFile)
		{
			byte[] bText = File.ReadAllBytes(gitConfigFile);

			if (bText.Any(chr => !UGCF_IsConfigChar(chr)))
				throw new Exception("Bad Encoding");

			string[] lines = SCommon.TextToLines(Encoding.ASCII.GetString(bText));

			for (int index = 0; index < lines.Length; index++)
			{
				string line = lines[index];
				int eqPos = line.IndexOf('=');

				if (eqPos != -1)
				{
					string key = line.Substring(0, eqPos);
					string value = line.Substring(eqPos + 1);

					if (key.Trim() == "url")
					{
						const string PREFIX = "https://";
						const string SUFFIX_A = "@github.com/";

						int p = value.IndexOf(PREFIX);

						if (p == -1)
							throw new Exception("no PREFIX");

						p += PREFIX.Length;

						int q = value.IndexOf(SUFFIX_A, p);

						if (q == -1)
						{
							q = value.IndexOf(SUFFIX_A.Substring(1), p); // Skip '@'

							if (q == -1)
								throw new Exception("no SUFFIX");
						}
						else
							q++; // Skip '@'

						value = value.Substring(0, p) + PasswordConfig.UserName + ":" + PasswordConfig.Password + "@" + value.Substring(q);
						line = key + "=" + value;
						lines[index] = line;
					}
				}
			}
			byte[] bTextNew = Encoding.ASCII.GetBytes(SCommon.LinesToText(lines).Replace("\r\n", "\n")); // 改行コードは LF

			if (SCommon.Comp(bText, bTextNew) != 0) // ? 更新された。
			{
#if true
				File.WriteAllBytes(gitConfigFile, bTextNew);
#else // test test test
				File.WriteAllBytes(@"C:\temp\" + UGCF_TestOutputCount.ToString("D4") + "_1.txt", bText);
				File.WriteAllBytes(@"C:\temp\" + UGCF_TestOutputCount.ToString("D4") + "_2.txt", bTextNew);
				UGCF_TestOutputCount++;
#endif
			}
		}

		//private static int UGCF_TestOutputCount = 0; // test test test

		private static bool UGCF_IsConfigChar(byte chr)
		{
			if (chr == 0x09) // ? Tab
				return true;

			if (chr == 0x0a) // ? LF
				return true;

			// 改行コードは LF を想定する。

			//if (chr == 0x0d) // ? CR
			//    return true;

			if (0x20 <= chr && chr <= 0x7e) // ? ASCII 文字
				return true;

			return false;
		}
	}
}
