using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Drawing;

namespace AccessLamp
{
	public class Common
	{
		public static string[] GetInstnaceNames(string categoryName)
		{
			return PerformanceCounterCategory.GetCategories().First(category => category.CategoryName == categoryName).GetInstanceNames();
		}

		public static Color ToColor(string str)
		{
			int[] vals = str.Split(':').Select(v => int.Parse(v)).ToArray();

			if (
				vals.Length != 3 ||
				vals.Any(v => v < 0 || 255 < v)
				)
				throw new Exception("Bad Color");

			return Color.FromArgb(vals[0], vals[1], vals[2]);
		}

		public static string ToString(Color color)
		{
			return string.Join(":", color.R, color.G, color.B);
		}

		/// <summary>
		/// 指定文字列から制御コードを削除する。
		/// </summary>
		/// <param name="str">文字列</param>
		/// <returns>制御コードを削除した文字列</returns>
		public static string ToPlainString(string str)
		{
			return new string(str.Select(chr => chr <= 0x1f || 0x7f <= chr && chr <= 0xa0 || chr == 0xad ? '?' : chr).ToArray());
		}
	}
}
