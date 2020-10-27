using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public static class Common
	{
		public static T TryGet<T>(Func<T> getter, T defval)
		{
			try
			{
				return getter();
			}
			catch
			{
				return defval;
			}
		}
	}
}
