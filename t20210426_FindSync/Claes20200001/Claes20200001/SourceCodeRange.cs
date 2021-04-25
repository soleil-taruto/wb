using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Commons;

namespace Charlotte
{
	public class SourceCodeRange
	{
		public readonly bool WholeFile;
		public readonly string FilePath;
		public readonly int LineIndexOfFile;
		public readonly string[] Lines;
		public readonly int IndentLength;
		public readonly string Name;
		public readonly string Hash;

		public SourceCodeRange(string file)
		{
			this.WholeFile = true;
			this.FilePath = file;
			this.LineIndexOfFile = 0;
			this.Lines = File.ReadAllLines(file, Encoding.UTF8);
			this.IndentLength = 0;
			this.Name = FileToName(file);
			this.Hash = SCommon.Hex.ToString(SCommon.GetSHA512(File.ReadAllBytes(file)));
		}

		private static string FileToName(string file)
		{
			file = SCommon.MakeFullPath(file);
			string projectFileDir = Common.FindProjectFileDir(file);
			string name = SCommon.ChangeRoot(file, projectFileDir, "<Project>");
			return name;
		}

		public SourceCodeRange(string file, string[] fileLines, int firstLineIndex, int lineCount, int declareLineIndex)
		{
			this.WholeFile = false;
			this.FilePath = file;
			this.LineIndexOfFile = firstLineIndex;
			this.Lines = fileLines
				.Skip(firstLineIndex)
				.Take(lineCount)
				.ToArray();
			this.IndentLength = Common.GetIndentLength(fileLines[declareLineIndex]);
			this.Name = fileLines[declareLineIndex].Substring(this.IndentLength);
			this.Hash = SCommon.Hex.ToString(SCommon.GetSHA512(Encoding.UTF8.GetBytes(SCommon.LinesToText(this.Lines))));
		}
	}
}
