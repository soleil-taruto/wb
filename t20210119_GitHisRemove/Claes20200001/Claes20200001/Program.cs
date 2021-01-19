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

		private void ProductMain()
		{
			// -- choose one or more --

			//GitHisRemove(@"C:\be\GitHubRepositories\Rico", @"/obj/"); // 2021.1.19
			//GitHisRemove(@"C:\be\GitHubRepositories\Rico", @"/bin/"); // 2021.1.19

			// --

			Console.WriteLine("Press ENTER to exit.");
			Console.ReadLine();
		}

		/// <summary>
		/// リポジトリの履歴から指定パターンを含むファイルを削除する。
		/// </summary>
		/// <param name="targetRepoDir">リポジトリの(ローカル上の)ディレクトリ</param>
		/// <param name="targetFilePattern">指定パターン(パスデリミタは'/')</param>
		private void GitHisRemove(string targetRepoDir, string targetFilePattern)
		{
			targetRepoDir = SCommon.MakeFullPath(targetRepoDir);

			Console.WriteLine("targetRepoDir: " + targetRepoDir); // cout
			Console.WriteLine("targetFilePattern: " + targetFilePattern); // cout

			if (string.IsNullOrEmpty(targetRepoDir))
				throw new ArgumentException("Bad targetRepoDir");

			if (!Directory.Exists(targetRepoDir))
				throw new ArgumentException("no targetRepoDir");

			{
				string dotGitDir = Path.Combine(targetRepoDir, ".git");

				if (!Directory.Exists(dotGitDir))
					throw new ArgumentException("no dotGitDir");
			}

			if (string.IsNullOrEmpty(targetFilePattern))
				throw new ArgumentException("Bad targetFilePattern");

			// 引数チェック_ここまで

			List<string> hisLogs = new List<string>();

			// Collect hisLogs
			{
				string[] lines = SCommon.Batch(
					new string[] { Common.GetGitExeFile() + " log --name-status" },
					targetRepoDir,
					SCommon.StartProcessWindowStyle_e.MINIMIZED
					);

				foreach (string line in lines)
				{
					string[] tokens = SCommon.Tokenize(line, "\t");

					foreach (string token in tokens.Skip(1)) // 2つ目以降を..
						if (token.Contains(targetFilePattern))
							hisLogs.Add(token);
				}
				Common.Distinct(hisLogs, SCommon.Comp);
			}

			foreach (string hisLog in hisLogs)
				Console.WriteLine("hisLog: " + hisLog); // cout

			// 安全のため..
			//Console.WriteLine("Press ENTER to continue.");
			//Console.ReadLine();

			// 現在のファイルを削除する。
			{
				string[] currentFiles = Common.E_GetRepoFiles(targetRepoDir).Select(v => SCommon.MakeFullPath(v)).ToArray();

				foreach (string hisLog in hisLogs)
					TryRemoveCurrentFile(targetRepoDir, hisLog, currentFiles);

				string[] lines = SCommon.Batch(
					new string[]
					{
						Common.GetGitExeFile() + " add *",
						Common.GetGitExeFile() + " commit -m \"remove from history " + DateTime.Now.ToString("yyyyMMddHHmmss") + "\"",
						Common.GetGitExeFile() + " push",
					},
					targetRepoDir,
					SCommon.StartProcessWindowStyle_e.MINIMIZED
					);

				foreach (string line in lines)
					Console.WriteLine("B.1 " + line); // cout
			}

			// 過去のファイルを削除する。
			{
				foreach (string hisLog in hisLogs)
				{
					Console.WriteLine("rm_hisLog: " + hisLog); // cout

					string[] lines = SCommon.Batch(
						new string[]
						{
							Common.GetGitExeFile() + " filter-branch -f --tree-filter \"rm -f '" + hisLog + "'\" HEAD",
							Common.GetGitExeFile() + " push -f",
						},
						targetRepoDir,
						SCommon.StartProcessWindowStyle_e.MINIMIZED
						);

					foreach (string line in lines)
						Console.WriteLine("B.2 " + line); // cout
				}
			}
		}

		private static void TryRemoveCurrentFile(string repoDir, string hisLog, string[] currentFiles)
		{
			string file = Path.Combine(repoDir, hisLog.Replace('/', '\\'));

			if (
				currentFiles.Any(currentFile => SCommon.EqualsIgnoreCase(currentFile, file)) &&
				File.Exists(file)
				)
				File.Delete(file);
		}
	}
}
