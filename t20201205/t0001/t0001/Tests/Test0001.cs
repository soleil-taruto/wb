using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tests
{
	public class Test0001
	{
		public void Test01()
		{
			this.Test01_Main(
				@"C:\temp\1.txt",
				@"C:\temp\2.txt",
				@"C:\temp\3.txt"
				);
		}

		private void Test01_Main(string inputFile, string removeTargFile, string outputFile)
		{
			string[] removeTargs = File.ReadAllLines(removeTargFile, Encoding.UTF8)
				.Where(line => line != "")
				.ToArray();

			File.WriteAllLines(
				outputFile,
				File.ReadAllLines(inputFile, Encoding.UTF8).Where(line => !Common.Contains(removeTargs, line)),
				Encoding.UTF8
				);
		}
	}
}
