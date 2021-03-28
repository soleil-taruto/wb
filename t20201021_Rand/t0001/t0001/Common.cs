using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public static class Common
	{
		public static string LZPad(string str, int minlen, char padding = '0')
		{
			while (str.Length < minlen)
			{
				str = padding + str;
			}
			return str;
		}

		public static string GetMeter(double rate, int length)
		{
			int value = (int)(rate * length);

			value = Math.Min(Math.Max(value, 0), length - 1);

			return new string(Enumerable.Range(0, length).Select(v => v == value ? 'X' : '-').ToArray());
		}
	}
}
