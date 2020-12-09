using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tests
{
	public class Test0001
	{
		public string[] GetEnclosed(string text, string[] tags, int startIndex = 0, bool ignoreCase = false)
		{
			List<int> sections = new List<int>();

			sections.Add(0);

			foreach (string tag in tags)
			{
				int start;

				if (ignoreCase)
					start = text.ToLower().IndexOf(tag.ToLower(), startIndex);
				else
					start = text.IndexOf(tag, startIndex);

				if (start == -1)
					return null;

				int end = start + tag.Length;

				sections.Add(start);
				sections.Add(end);

				startIndex = end;
			}
			sections.Add(text.Length);

			return Enumerable.Range(0, sections.Count - 1).Select(index => text.Substring(sections[index], sections[index + 1] - sections[index])).ToArray();
		}

		// ==== ==== ====
		// ==== ==== ====
		// ==== ==== ====

		public void Test01()
		{
			Test01_a("AAAAA<ABC>BBBB</ABC>CCC<ABC>DD</ABC>E<ABC></ABC><ABC>zzzzzz</ABC>", "<ABC>:</ABC>".Split(':'));
		}

		private void Test01_a(string text, string[] tags)
		{
			int startIndex = 0;

			for (int contentNo = 1; ; contentNo++)
			{
				string[] parts = GetEnclosed(text, tags, startIndex);

				if (parts == null)
					break;

				parts[2] = "CONTENT-" + contentNo;

				text = string.Join("", parts);
				startIndex = parts[0].Length + parts[1].Length + parts[2].Length + parts[3].Length;
			}
			Console.WriteLine(text);
		}
	}
}
