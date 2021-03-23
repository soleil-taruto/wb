using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using Charlotte.Commons;

namespace Charlotte
{
	public static class Common
	{
		/// <summary>
		/// 始点から終点までの間の指定レートの位置の値を返す。
		/// </summary>
		/// <param name="a">始点</param>
		/// <param name="b">終点</param>
		/// <param name="rate">レート</param>
		/// <returns>レートの値</returns>
		public static double AToBRate(double a, double b, double rate)
		{
			return a + (b - a) * rate;
		}

		/// <summary>
		/// 始点から終点までの間の位置をレートに変換する。
		/// </summary>
		/// <param name="a">始点</param>
		/// <param name="b">終点</param>
		/// <param name="value">位置</param>
		/// <returns>レート</returns>
		public static double RateAToB(double a, double b, double value)
		{
			return (value - a) / (b - a);
		}
	}
}
