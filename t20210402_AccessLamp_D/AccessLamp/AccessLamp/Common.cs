using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace AccessLamp
{
	public class Common
	{
		public static string[] GetInstnaceNames(string categoryName)
		{
			return PerformanceCounterCategory.GetCategories().First(category => category.CategoryName == categoryName).GetInstanceNames();
		}
	}
}
