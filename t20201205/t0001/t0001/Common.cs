using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Charlotte
{
	public static class Common
	{
		public static bool Contains(string[] lines, string targLine)
		{
			foreach (string line in lines)
				if (line == targLine)
					return true;

			return false;
		}
	}
}
