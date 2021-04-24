using System;
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
			this.ReportMain();
		}

		private void CollectMain()
		{
			foreach (string csFile in Directory.GetFiles(Consts.ROOT_DIR, "*.cs", SearchOption.AllDirectories))
			{
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
						entity.StartsWith("public ") ||
						entity.StartsWith("protected ") ||
						entity.StartsWith("private ")
						)
					{
						if (
							index + 2 < lines.Length &&
							Common.GetIndentLength(lines[index + 1]) == indentLen &&
							lines[index + 1].Substring(indentLen) == "{"
							)
						{
							int closingLineIndex = GetClosingLineIndex(lines, index + 2, indentLen, "}", "};");

							if (closingLineIndex != -1)
								this.SourceCodeRanges.Add(new SourceCodeRange(csFile, index, (closingLineIndex + 1) - index));
						}
					}

					if (entity.StartsWith("#region "))
					{
						int closingLineIndex = GetClosingLineIndex(lines, index + 1, indentLen, "#endregion", null);

						if (closingLineIndex != -1)
							this.SourceCodeRanges.Add(new SourceCodeRange(csFile, index, (closingLineIndex + 1) - index));
					}
				}
			}
		}

		private int GetClosingLineIndex(string[] lines, int startLineIndex, int targIndentLen, string closingLineEntity, string closingLineEntity_02)
		{
			for (int index = startLineIndex; index < lines.Length; index++)
			{
				int indentLen = Common.GetIndentLength(lines[index]);

				if (indentLen == 0)
				{
					// noop
				}
				else
				{
					if (indentLen < targIndentLen)
						break;

					string entity = lines[index].Substring(targIndentLen);

					if (entity == closingLineEntity)
						return index;

					if (entity == closingLineEntity_02)
						return -1;
				}
			}
			Console.WriteLine("イレギュラーなフォーマット"); // test test test
			return -1;
		}

		private void ReportMain()
		{
			// TODO
		}
	}
}
