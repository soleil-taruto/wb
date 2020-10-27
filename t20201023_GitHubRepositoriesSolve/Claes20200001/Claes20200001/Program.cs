using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Commons;

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
			if (!Directory.Exists(Consts.REPOSITORIES_ROOT_DIR))
				throw new Exception("リポジトリのルート・ディレクトリが見つかりません。");

			foreach (string dir in Directory.GetDirectories(Consts.REPOSITORIES_ROOT_DIR))
				Solve(dir);
		}

		private void Solve(string dir)
		{
			SolveNonAsciiCharactersPaths(dir);
			SolveEmptyFolders(dir);
		}

		private void SolveNonAsciiCharactersPaths(string dir)
		{
			foreach (string subDir in Directory.GetDirectories(dir))
			{
				if (Path.GetFileName(subDir) == ".git")
					continue;

				SolveNonAsciiCharactersPaths(subDir);
				SNACP_Path(subDir);
			}
			foreach (string file in Directory.GetFiles(dir))
			{
				SNACP_Path(file);
			}
		}

		private void SNACP_Path(string path)
		{
			string localPath = Path.GetFileName(path);
			string localPathNew = ToAsciiCharactersLocalPath(localPath);

			if (localPath != localPathNew)
			{
				string pathNew = Path.Combine(Path.GetDirectoryName(path), localPathNew);

				if (File.Exists(pathNew) || Directory.Exists(pathNew))
					throw new Exception("変更後のパス名は既に存在します。");

				if (File.Exists(path))
					File.Move(path, pathNew);
				else
					Directory.Move(path, pathNew);
			}
		}

		private string ToAsciiCharactersLocalPath(string localPath)
		{
			StringBuilder buff = new StringBuilder();

			foreach (char chr in localPath)
			{
				if (chr < 0x100)
					buff.Append(chr);
				else
					buff.Append(((ushort)chr).ToString("x4"));
			}
			return buff.ToString();
		}

		private void SolveEmptyFolders(string dir)
		{
			if (
				Directory.GetDirectories(dir).Length == 0 &&
				Directory.GetFiles(dir).Length == 0
				)
			{
				string outFile = Path.Combine(dir, "____EMPTY____");

				File.WriteAllBytes(outFile, SCommon.EMPTY_BYTES);

				return;
			}

			foreach (string subDir in Directory.GetDirectories(dir))
			{
				if (Path.GetFileName(subDir) == ".git")
					continue;

				SolveEmptyFolders(subDir);
			}
		}
	}
}
