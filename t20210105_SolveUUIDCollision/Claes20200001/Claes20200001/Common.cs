using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Commons;

namespace Charlotte
{
	public static class Common
	{
		public static string EraseStartEnd(string str, string startPtn, string endPtn)
		{
			if (!str.StartsWith(startPtn))
				return null;

			str = str.Substring(startPtn.Length);

			if (!str.EndsWith(endPtn))
				return null;

			str = str.Substring(0, str.Length - endPtn.Length);
			return str;
		}
	}
}
