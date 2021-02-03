using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Numerics;
using Charlotte.Commons;

namespace Charlotte
{
	public static class Common
	{
		/// <summary>
		/// 10^19
		/// </summary>
		private static BigInteger BIGINT_10_P_19 = (BigInteger)10000000000000000000UL;

		/// <summary>
		/// 0以上の10進文字列を BigInteger に変換します。
		/// </summary>
		/// <param name="strint">整数文字列</param>
		/// <returns>整数</returns>
		public static BigInteger ToBigInteger(string strint)
		{
			if (!Regex.IsMatch(strint, "^[0-9]*$"))
				throw new Exception("strint is not decimal integer");

			BigInteger value;
			int index = strint.Length % 19;

			if (1 <= index)
				value = ulong.Parse(strint.Substring(0, index));
			else
				value = 0;

			for (; index < strint.Length; index += 19)
			{
				value *= BIGINT_10_P_19;
				value += ulong.Parse(strint.Substring(index, 19));
			}
			return value;
		}

		/// <summary>
		/// 0以上の整数を文字列に変換します。
		/// </summary>
		/// <param name="value">整数</param>
		/// <returns>整数文字列</returns>
		public static string ToString(BigInteger value)
		{
			if (value < 0)
				throw new Exception("value is negative");

			List<string> units = new List<string>();

			while (!value.IsZero)
			{
				byte[] bUnit = (value % BIGINT_10_P_19).ToByteArray();
				int bUnitLen = Math.Min(bUnit.Length, 8);
				ulong unit = 0;

				for (int index = 0; index < bUnitLen; index++)
					unit |= (ulong)bUnit[index] << (index * 8);

				units.Add(unit.ToString("D19"));
				value /= BIGINT_10_P_19;
			}
			units.Add("0");
			units.Reverse();
			string strint = string.Join("", units);

			{
				int index;

				for (index = 0; index + 1 < strint.Length && strint[index] == '0'; index++)
				{ }

				strint = strint.Substring(index);
			}

			return strint;
		}
	}
}
