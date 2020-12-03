using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tests
{
	public class Test0002
	{
		public static string[] ParseIsland(string text, string singleTag, bool ignoreCase = false)
		{
			int start;

			if (ignoreCase)
				start = text.ToLower().IndexOf(singleTag.ToLower());
			else
				start = text.IndexOf(singleTag);

			if (start == -1)
				return null;

			int end = start + singleTag.Length;

			return new string[]
			{
				text.Substring(0, start),
				text.Substring(start, end - start),
				text.Substring(end),
			};
		}

		public static string[] ParseEnclosed(string text, string openTag, string closeTag, bool ignoreCase = false)
		{
			string[] starts = ParseIsland(text, openTag, ignoreCase);

			if (starts == null)
				return null;

			string[] ends = ParseIsland(starts[2], closeTag, ignoreCase);

			if (ends == null)
				return null;

			return new string[]
			{
				starts[0],
				starts[1],
				ends[0],
				ends[1],
				ends[2],
			};
		}

		// ==== ==== ====
		// ==== ==== ====
		// ==== ==== ====

		public void Test01()
		{
			Test01_a("AAAAA<ABC>BBBB</ABC>CCC<ABC>DD</ABC>E<ABC></ABC><ABC>zzzzzz</ABC>", "<ABC>", "</ABC>");
		}

		private void Test01_a(string text, string openTag, string closeTag)
		{
			int startIndex = 0;

			for (int contentNo = 1; ; contentNo++)
			{
				string[] parts = ParseEnclosed(text.Substring(startIndex), openTag, closeTag);

				if (parts == null)
					break;

				parts[2] = "CONTENT-" + contentNo;

				text = text.Substring(0, startIndex) + string.Join("", parts);
				startIndex += parts[0].Length + parts[1].Length + parts[2].Length + parts[3].Length;
			}
			Console.WriteLine(text);
		}
	}
}
