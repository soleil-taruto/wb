using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace AccessLamp
{
	public class Common
	{
		public static string CutTrail(string str, int maxlen)
		{
			if (maxlen < str.Length)
			{
				str = str.Substring(0, maxlen);
			}
			return str;
		}

		public static string[] GetInstnaceNames(string categoryName)
		{
			return PerformanceCounterCategory.GetCategories().First(category => category.CategoryName == categoryName).GetInstanceNames();
		}
	}
}
