﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
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
			if (ProcMain.DEBUG)
			{
				Main3();
			}
			else
			{
				Main4();
			}
			Common.OpenOutputDirIfCreated();
		}

		private void Main3()
		{
			// -- choose one --

			Main4();
			//new Test0001().Test01();
			//new Test0002().Test01();
			//new Test0003().Test01();

			// --

			//Common.Pause();
		}

		private List<SourceCodeRange> SourceCodeRanges = new List<SourceCodeRange>();

		private void Main4()
		{
			if (!Directory.Exists(Consts.ROOT_DIR))
				throw new Exception("no ROOT_DIR");

			this.CollectMain();

			this.SourceCodeRanges.Sort((a, b) =>
			{
				if (a == b) // ? 同じレンジ
					return 0;

				int ret;

				ret = SCommon.Comp(a.Name, b.Name);
				if (ret != 0)
					return ret;

				ret = a.IndentLength - b.IndentLength;
				if (ret != 0)
					return ret;

				ret = SCommon.Comp(a.Hash, b.Hash);
				if (ret != 0)
					return ret;

				ret = SCommon.Comp(a.FilePath, b.FilePath);
				if (ret != 0)
					return ret;

				ret = a.LineIndexOfFile - b.LineIndexOfFile;
				if (ret != 0)
					return ret;

				throw null; // never -- 同じレンジ && 異なるインスタンス -> 有り得ない。
			});

			this.ReportMain();
		}

		private void CollectMain()
		{
			foreach (string csFile in Directory.GetFiles(Consts.ROOT_DIR, "*.cs", SearchOption.AllDirectories)
				.Select(v => SCommon.MakeFullPath(v)) // 2bs
				)
			{
				if (SCommon.ContainsIgnoreCase(csFile, "\\Properties\\")) // ? プロジェクトのプロパティ配下 -> 除外する。
					continue;

				if (SCommon.EndsWithIgnoreCase(csFile, ".Designer.cs")) // ? フォームデザイナ -> 除外する。
					continue;

				this.SourceCodeRanges.Add(new SourceCodeRange(csFile));
				this.CollectInCSFile(csFile);
			}
		}

		private void CollectInCSFile(string csFile)
		{
			string[] lines = File.ReadAllLines(csFile, Encoding.UTF8);

			for (int index = 0; index < lines.Length; index++)
			{
				int indentLen = Common.GetIndentLength(lines[index]);

				if (1 <= indentLen)
				{
					string entity = lines[index].Substring(indentLen);

					if (
						2 <= indentLen && // ファイル全体(WholeFile == true)があるので、アウタークラスは除外する。
						(
							entity.StartsWith("public ") ||
							entity.StartsWith("protected ") ||
							entity.StartsWith("private ")
							) &&
						index + 2 < lines.Length && // "{", "}" それぞれの行が存在するはず。
						Common.GetIndentLength(lines[index + 1]) == indentLen &&
						lines[index + 1].Substring(indentLen) == "{"
						)
					{
						int closingLineIndex = GetClosingLineIndex(lines, index + 2, indentLen, "}", "};");

						if (closingLineIndex != -1)
						{
							int declareIndex = index;

							Predicate<string> a_entityIsDocCommentOrAnnotation = v =>
								v.StartsWith("///") ||
								v.StartsWith("[");

							// インナークラス・メソッドのドキュメンテーション_コメント・アノテーションまで拡張する。
							while (
								1 <= index &&
								Common.GetIndentLength(lines[index - 1]) == indentLen &&
								a_entityIsDocCommentOrAnnotation(lines[index - 1].Substring(indentLen))
								)
								index--;

							int lineCount = (closingLineIndex + 1) - index;
							this.SourceCodeRanges.Add(new SourceCodeRange(csFile, lines, index, lineCount, declareIndex));
							//index += lineCount - 1; // このインナークラス・メソッドをスキップ
							index = declareIndex + 1; // "{" をスキップ
							continue;
						}
					}

					if (entity.StartsWith("#region "))
					{
						int closingLineIndex = GetClosingLineIndex(lines, index + 1, indentLen, "#endregion", null);

						if (closingLineIndex == -1)
							throw null; // never

						int lineCount = (closingLineIndex + 1) - index;
						this.SourceCodeRanges.Add(new SourceCodeRange(csFile, lines, index, lineCount, index));
						//index += lineCount - 1; // このリージョンをスキップ
						continue;
					}
				}
			}
		}

		private int GetClosingLineIndex(string[] lines, int startLineIndex, int indentLen, string closingLineEntity, string closingLineEntity_収集対象外)
		{
			for (int index = startLineIndex; index < lines.Length; index++)
			{
				if (indentLen == Common.GetIndentLength(lines[index]))
				{
					string entity = lines[index].Substring(indentLen);

					if (entity == closingLineEntity)
						return index;

					if (entity == closingLineEntity_収集対象外)
						return -1;
				}
			}
			throw new Exception("閉じていない。");
		}

		private void ReportMain()
		{
			using (StreamWriter writer = new StreamWriter(Path.Combine(Common.GetOutputDir(), "全データ.txt"), false, Encoding.UTF8))
			{
				foreach (SourceCodeRange sourceCodeRange in this.SourceCodeRanges)
				{
					writer.WriteLine(sourceCodeRange.IndentLength + ":" + sourceCodeRange.Name);
					writer.WriteLine("\t" + sourceCodeRange.Hash);
					writer.WriteLine("\t" + (sourceCodeRange.WholeFile ? "ファイル全体" : "ファイル部分"));
					writer.WriteLine("\t" + sourceCodeRange.FilePath);
					writer.WriteLine("\t" + (sourceCodeRange.LineIndexOfFile + 1));
					writer.WriteLine("\t" + sourceCodeRange.Lines.Length);
					//writer.WriteLine("\t" + sourceCodeRange.IndentLength);

					for (int index = 0; index < sourceCodeRange.Lines.Length; index++)
						writer.WriteLine("\t[" + (index + 1).ToString("D4") + "]\t" + sourceCodeRange.Lines[index]);

					writer.WriteLine("");
				}
			}
		}
	}
}
