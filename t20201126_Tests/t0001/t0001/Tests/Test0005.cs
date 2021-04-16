using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Charlotte.Commons;

namespace Charlotte.Tests
{
	public class Test0005
	{
		public void Test01()
		{
			for (int c = 0; c < 1000000; c++)
			{
				if (c % 100000 == 0) Console.WriteLine("*1 " + c); // cout

				Test01_a(0, 300);
			}
			for (int c = 0; c < 300; c++)
			{
				if (c % 30 == 0) Console.WriteLine("*2 " + c); // cout

				Test01_a(301, 1000000);
			}
			for (int c = 0; c < 10; c++)
			{
				Console.WriteLine("*3 " + c); // cout

				Test01_a(1000001, 30000000);
			}
		}

		private void Test01_a(int minlen, int maxlen)
		{
			byte[] src = SCommon.CRandom.GetBytes(minlen, maxlen);
			string enc = Base64.I.Encode(src);
			byte[] dec = Base64.I.Decode(enc);

			//if (!Regex.IsMatch(enc, "^[A-Za-z0-9\\+\\/\\=]*$"))
			if (!Regex.IsMatch(enc, "^[A-Za-z0-9\\+\\/]*[\\=]{0,3}$"))
				throw null; // BUG !!!

			if (SCommon.Comp(src, dec) != 0)
				throw null; // BUG !!!
		}

		public void Test02() // manual test
		{
			Console.WriteLine(TimeStampToSec.ToTimeStamp(DateTime.Now));

			Console.WriteLine(TimeStampToSec.ToSec(10101000000));
			Console.WriteLine(TimeStampToSec.ToSec(9223372031231235959)); // --> 29106150842822399

			Console.WriteLine(TimeStampToSec.ToTimeStamp(0));
			Console.WriteLine(TimeStampToSec.ToTimeStamp(1));
			Console.WriteLine(TimeStampToSec.ToTimeStamp(29106150842822398)); // Max - 1
			Console.WriteLine(TimeStampToSec.ToTimeStamp(29106150842822399)); // Max
			Console.WriteLine(TimeStampToSec.ToTimeStamp(29106150842822400)); // Max + 1
		}

		public void Test03()
		{
			for (long sec = 0; sec <= 1000; sec++) Test03_a(sec);
			for (long sec = 1000; sec <= 10000; sec += 10) Test03_a(sec);
			for (long sec = 10000; sec <= 100000; sec += 100) Test03_a(sec);
			for (long sec = 100000; sec <= 1000000; sec += 1000) Test03_a(sec);
			for (long sec = 1000000; sec <= 10000000; sec += 10000) Test03_a(sec);
			for (long sec = 10000000; sec <= 100000000; sec += 100000) Test03_a(sec);
			for (long sec = 100000000; sec <= 1000000000; sec += 1000000) Test03_a(sec);
			for (long sec = 1000000000; sec <= 10000000000; sec += 10000000) Test03_a(sec);
			for (long sec = 10000000000; sec <= 100000000000; sec += 100000000) Test03_a(sec);
			for (long sec = 100000000000; sec <= 1000000000000; sec += 1000000000) Test03_a(sec);
			for (long sec = 1000000000000; sec <= 10000000000000; sec += 10000000000) Test03_a(sec);
			for (long sec = 10000000000000; sec <= 100000000000000; sec += 100000000000) Test03_a(sec);
			for (long sec = 100000000000000; sec <= 1000000000000000; sec += 1000000000000) Test03_a(sec);
			for (long sec = 1000000000000000; sec <= 10000000000000000; sec += 10000000000000) Test03_a(sec);
			for (long sec = 29106150800000000; sec <= 29106150840000000; sec += 1000) Test03_a(sec);
			for (long sec = 29106150840000000; sec <= 29106150842000000; sec += 100) Test03_a(sec);
			for (long sec = 29106150842000000; sec <= 29106150842700000; sec += 10) Test03_a(sec);
			for (long sec = 29106150842700000; sec <= 29106150842822399; sec++) Test03_a(sec);

			Test03_b(-3, 0); // Min - 3  -->  Min
			Test03_b(-2, 0); // Min - 2  -->  Min
			Test03_b(-1, 0); // Min - 1  -->  Min
			Test03_b(0, 0);  // Min      -->  Min
			Test03_b(1, 1);  // Min + 1  -->  Min + 1
			Test03_b(2, 2);  // Min + 2  -->  Min + 2
			Test03_b(3, 3);  // Min + 3  -->  Min + 3

			Test03_b(29106150842822396, 29106150842822396); // Max - 3  -->  Max - 3
			Test03_b(29106150842822397, 29106150842822397); // Max - 2  -->  Max - 2
			Test03_b(29106150842822398, 29106150842822398); // Max - 1  -->  Max - 1
			Test03_b(29106150842822399, 29106150842822399); // Max      -->  Max
			Test03_b(29106150842822400, 29106150842822399); // Max + 1  -->  Max
			Test03_b(29106150842822401, 29106150842822399); // Max + 2  -->  Max
			Test03_b(29106150842822402, 29106150842822399); // Max + 3  -->  Max
		}

		private void Test03_a(long sec)
		{
			long timeStamp = TimeStampToSec.ToTimeStamp(sec);
			long sec2 = TimeStampToSec.ToSec(timeStamp);

			Console.WriteLine(sec + " --> " + timeStamp + " --> " + sec2); // cout

			if (sec != sec2)
				throw null; // BUG !!!
		}

		private void Test03_b(long sec, long expect)
		{
			long timeStamp = TimeStampToSec.ToTimeStamp(sec);
			long sec2 = TimeStampToSec.ToSec(timeStamp);

			Console.WriteLine(sec + " --> " + timeStamp + " --> " + sec2 + " (" + expect + ")"); // cout

			if (sec2 != expect)
				throw null; // BUG !!!
		}

		public void Test04() // manual test
		{
			Console.WriteLine(new SimpleDateTime(TimeStampToSec.ToSec(DateTime.Now)));

			Console.WriteLine(SimpleDateTime.FromTimeStamp(44440101000000));
			Console.WriteLine(SimpleDateTime.FromTimeStamp(44441231235959));

			Console.WriteLine(SimpleDateTime.FromTimeStamp(10101000000));
			Console.WriteLine(SimpleDateTime.FromTimeStamp(99990101000000));
			Console.WriteLine(SimpleDateTime.FromTimeStamp(99991231235959));
			Console.WriteLine(SimpleDateTime.FromTimeStamp(999990101000000));
			Console.WriteLine(SimpleDateTime.FromTimeStamp(999991231235959));
			Console.WriteLine(SimpleDateTime.FromTimeStamp(9999990101000000));
			Console.WriteLine(SimpleDateTime.FromTimeStamp(9999991231235959));
			Console.WriteLine(SimpleDateTime.FromTimeStamp(99999990101000000));
			Console.WriteLine(SimpleDateTime.FromTimeStamp(99999991231235959));
			Console.WriteLine(SimpleDateTime.FromTimeStamp(999999990101000000));
			Console.WriteLine(SimpleDateTime.FromTimeStamp(999999991231235959));
			Console.WriteLine(SimpleDateTime.FromTimeStamp(9223372030101000000));
			Console.WriteLine(SimpleDateTime.FromTimeStamp(9223372031231235959));

			{
				SimpleDateTime timeStamp = SimpleDateTime.FromTimeStamp(19991231235959);
				Console.WriteLine(timeStamp);
				timeStamp += 1;
				Console.WriteLine(timeStamp);
				timeStamp -= 1;
				Console.WriteLine(timeStamp);

				Console.WriteLine(timeStamp + 1);
				Console.WriteLine(timeStamp - 1);
				Console.WriteLine(1 + timeStamp);
				//Console.WriteLine(1 - timeStamp); // syntax error
			}

			{
				SimpleDateTime a = SimpleDateTime.FromTimeStamp(20220101000000);
				SimpleDateTime b = SimpleDateTime.FromTimeStamp(20210101000000);

				Console.WriteLine(a);
				Console.WriteLine(b);
				Console.WriteLine(a - b);
				Console.WriteLine(b - a);
				//Console.WriteLine(a + b); // syntax error
			}

			{
				SimpleDateTime timeStamp = SimpleDateTime.FromTimeStamp(67890405112233);

				Console.WriteLine(timeStamp.Year);
				Console.WriteLine(timeStamp.Month);
				Console.WriteLine(timeStamp.Day);
				Console.WriteLine(timeStamp.Hour);
				Console.WriteLine(timeStamp.Minute);
				Console.WriteLine(timeStamp.Second);
				Console.WriteLine(timeStamp.Weekday);

				//timeStamp.Year = 1234; // syntax error
			}

			Console.WriteLine(SimpleDateTime.Now());
			Console.WriteLine(new SimpleDateTime(DateTime.Now));
			Console.WriteLine(new SimpleDateTime(DateTime.Now).ToDateTime());

			Console.WriteLine(SimpleDateTime.FromTimeStamp(99991231235959).ToDateTime()); // DateTime.MaxValue
			//Console.WriteLine(SimpleDateTime.FromTimeStamp(100000101000000).ToDateTime()); // DateTime.MaxValue + 1 // 例外
		}

		public void Test05()
		{
			long SEC_MIN = 0L;
			long SEC_MAX = TimeStampToSec.ToSec(99991231235959);

			for (long sec = SEC_MIN; sec <= 1000; sec++) Test05_a(sec);
			for (long sec = SEC_MIN; sec <= 10000; sec += 10) Test05_a(sec);
			for (long sec = SEC_MIN; sec <= 100000; sec += 100) Test05_a(sec);
			for (long sec = SEC_MIN; sec <= 1000000; sec += 1000) Test05_a(sec);
			for (long sec = SEC_MIN; sec <= 10000000; sec += 10000) Test05_a(sec);
			for (long sec = SEC_MIN; sec <= 100000000; sec += 100000) Test05_a(sec);
			for (long sec = SEC_MIN; sec <= 1000000000; sec += 1000000) Test05_a(sec);
			for (long sec = SEC_MIN; sec <= 10000000000; sec += 10000000) Test05_a(sec);
			for (long sec = SEC_MIN; sec <= 100000000000; sec += 100000000) Test05_a(sec);
			for (long sec = SEC_MIN; sec <= 300000000000; sec += 300000000) Test05_a(sec);
			for (long sec = SEC_MAX - 100000000000; sec <= SEC_MAX; sec += 100000000) Test05_a(sec);
			for (long sec = SEC_MAX - 10000000000; sec <= SEC_MAX; sec += 10000000) Test05_a(sec);
			for (long sec = SEC_MAX - 1000000000; sec <= SEC_MAX; sec += 1000000) Test05_a(sec);
			for (long sec = SEC_MAX - 100000000; sec <= SEC_MAX; sec += 100000) Test05_a(sec);
			for (long sec = SEC_MAX - 10000000; sec <= SEC_MAX; sec += 10000) Test05_a(sec);
			for (long sec = SEC_MAX - 1000000; sec <= SEC_MAX; sec += 1000) Test05_a(sec);
			for (long sec = SEC_MAX - 100000; sec <= SEC_MAX; sec += 100) Test05_a(sec);
			for (long sec = SEC_MAX - 10000; sec <= SEC_MAX; sec += 10) Test05_a(sec);
			for (long sec = SEC_MAX - 1000; sec <= SEC_MAX; sec++) Test05_a(sec);
		}

		private void Test05_a(long sec)
		{
			DateTime dt = new SimpleDateTime(sec).ToDateTime();
			DateTime dt2 = new SimpleDateTime(dt).ToDateTime();
			long sec2 = TimeStampToSec.ToSec(dt2);

			Console.WriteLine(sec + " --> " + dt + " --> " + dt2 + " --> " + sec2); // cout

			if ((dt - dt2) != new TimeSpan(0L))
				throw null; // BUG !!!

			if (sec != sec2)
				throw null; // BUG !!!
		}

		// ==== ==== ====
		// ==== ==== ====
		// ==== ==== ====

		// sync > @ Base64_TimeStamp

		#region Base64

		public class Base64
		{
			private static Base64 _i = null;

			public static Base64 I
			{
				get
				{
					if (_i == null)
						_i = new Base64();

					return _i;
				}
			}

			private char[] Chars;
			private byte[] CharMap;

			private const char CHAR_PADDING = '=';

			private Base64()
			{
				this.Chars = (SCommon.ALPHA + SCommon.alpha + SCommon.DECIMAL + "+/").ToArray();
				this.CharMap = new byte[(int)char.MaxValue + 1];

				for (int index = 0; index < 64; index++)
					this.CharMap[this.Chars[index]] = (byte)index;
			}

			public string Encode(byte[] src)
			{
				char[] dest = new char[((src.Length + 2) / 3) * 4];
				int writer = 0;
				int index = 0;
				int chr;

				while (index + 3 <= src.Length)
				{
					chr = (src[index++] & 0xff) << 16;
					chr |= (src[index++] & 0xff) << 8;
					chr |= src[index++] & 0xff;
					dest[writer++] = this.Chars[chr >> 18];
					dest[writer++] = this.Chars[(chr >> 12) & 0x3f];
					dest[writer++] = this.Chars[(chr >> 6) & 0x3f];
					dest[writer++] = this.Chars[chr & 0x3f];
				}
				if (index + 2 == src.Length)
				{
					chr = (src[index++] & 0xff) << 8;
					chr |= src[index++] & 0xff;
					dest[writer++] = this.Chars[chr >> 10];
					dest[writer++] = this.Chars[(chr >> 4) & 0x3f];
					dest[writer++] = this.Chars[(chr << 2) & 0x3c];
					dest[writer++] = CHAR_PADDING;
				}
				else if (index + 1 == src.Length)
				{
					chr = src[index++] & 0xff;
					dest[writer++] = this.Chars[chr >> 2];
					dest[writer++] = this.Chars[(chr << 4) & 0x30];
					dest[writer++] = CHAR_PADDING;
					dest[writer++] = CHAR_PADDING;
				}
				return new string(dest);
			}

			public byte[] Decode(string src)
			{
				int destSize = (src.Length / 4) * 3;

				if (destSize != 0)
				{
					if (src[src.Length - 2] == CHAR_PADDING)
					{
						destSize -= 2;
					}
					else if (src[src.Length - 1] == CHAR_PADDING)
					{
						destSize--;
					}
				}
				byte[] dest = new byte[destSize];
				int writer = 0;
				int index = 0;
				int chr;

				while (writer + 3 <= destSize)
				{
					chr = (this.CharMap[src[index++]] & 0x3f) << 18;
					chr |= (this.CharMap[src[index++]] & 0x3f) << 12;
					chr |= (this.CharMap[src[index++]] & 0x3f) << 6;
					chr |= this.CharMap[src[index++]] & 0x3f;
					dest[writer++] = (byte)(chr >> 16);
					dest[writer++] = (byte)((chr >> 8) & 0xff);
					dest[writer++] = (byte)(chr & 0xff);
				}
				if (writer + 2 == destSize)
				{
					chr = (this.CharMap[src[index++]] & 0x3f) << 10;
					chr |= (this.CharMap[src[index++]] & 0x3f) << 4;
					chr |= (this.CharMap[src[index++]] & 0x3c) >> 2;
					dest[writer++] = (byte)(chr >> 8);
					dest[writer++] = (byte)(chr & 0xff);
				}
				else if (writer + 1 == destSize)
				{
					chr = (this.CharMap[src[index++]] & 0x3f) << 2;
					chr |= (this.CharMap[src[index++]] & 0x30) >> 4;
					dest[writer++] = (byte)chr;
				}
				return dest;
			}
		}

		#endregion

		#region TimeStampToSec

		/// <summary>
		/// 日時を 1/1/1 00:00:00 からの経過秒数に変換およびその逆を行います。
		/// 日時のフォーマット
		/// -- YMMDDhhmmss
		/// -- YYMMDDhhmmss
		/// -- YYYMMDDhhmmss
		/// -- YYYYMMDDhhmmss
		/// -- YYYYYMMDDhhmmss
		/// -- YYYYYYMMDDhhmmss
		/// -- YYYYYYYMMDDhhmmss
		/// -- YYYYYYYYMMDDhhmmss
		/// -- YYYYYYYYYMMDDhhmmss -- 但し YYYYYYYYY == 100000000 ～ 922337203
		/// ---- 年の桁数は 1 ～ 9 桁
		/// 日時の範囲
		/// -- 最小 1/1/1 00:00:00
		/// -- 最大 922337203/12/31 23:59:59
		/// </summary>
		public static class TimeStampToSec
		{
			private const int YEAR_MIN = 1;
			private const int YEAR_MAX = 922337203;

			private const long TIME_STAMP_MIN = 10101000000L;
			private const long TIME_STAMP_MAX = 9223372031231235959L;

			public static long ToSec(long timeStamp)
			{
				if (timeStamp < TIME_STAMP_MIN || TIME_STAMP_MAX < timeStamp)
					return 0L;

				int s = (int)(timeStamp % 100L);
				timeStamp /= 100L;
				int i = (int)(timeStamp % 100L);
				timeStamp /= 100L;
				int h = (int)(timeStamp % 100L);
				timeStamp /= 100L;
				int d = (int)(timeStamp % 100L);
				timeStamp /= 100L;
				int m = (int)(timeStamp % 100L);
				int y = (int)(timeStamp / 100L);

				if (
					//y < YEAR_MIN || YEAR_MAX < y ||
					m < 1 || 12 < m ||
					d < 1 || 31 < d ||
					h < 0 || 23 < h ||
					i < 0 || 59 < i ||
					s < 0 || 59 < s
					)
					return 0L;

				if (m <= 2)
					y--;

				long ret = y / 400;
				ret *= 365 * 400 + 97;

				y %= 400;

				ret += y * 365;
				ret += y / 4;
				ret -= y / 100;

				if (2 < m)
				{
					ret -= 31 * 10 - 4;
					m -= 3;
					ret += (m / 5) * (31 * 5 - 2);
					m %= 5;
					ret += (m / 2) * (31 * 2 - 1);
					m %= 2;
					ret += m * 31;
				}
				else
					ret += (m - 1) * 31;

				ret += d - 1;
				ret *= 24;
				ret += h;
				ret *= 60;
				ret += i;
				ret *= 60;
				ret += s;

				return ret;
			}

			public static long ToTimeStamp(long sec)
			{
				if (sec < 0L)
					return TIME_STAMP_MIN;

				int s = (int)(sec % 60L);
				sec /= 60L;
				int i = (int)(sec % 60L);
				sec /= 60L;
				int h = (int)(sec % 24L);
				sec /= 24L;

				int day = (int)(sec % 146097);
				sec /= 146097;
				sec *= 400;
				sec++;

				if (YEAR_MAX < sec)
					return TIME_STAMP_MAX;

				int y = (int)sec;
				int m = 1;
				int d;

				day += Math.Min((day + 306) / 36524, 3);
				y += (day / 1461) * 4;
				day %= 1461;

				day += Math.Min((day + 306) / 365, 3);
				y += day / 366;
				day %= 366;

				if (60 <= day)
				{
					m += 2;
					day -= 60;
					m += (day / 153) * 5;
					day %= 153;
					m += (day / 61) * 2;
					day %= 61;
				}
				m += day / 31;
				day %= 31;
				d = day + 1;

				if (y < YEAR_MIN)
					return TIME_STAMP_MIN;

				if (YEAR_MAX < y)
					return TIME_STAMP_MAX;

				if (
					//y < YEAR_MIN || YEAR_MAX < y ||
					m < 1 || 12 < m ||
					d < 1 || 31 < d ||
					h < 0 || 23 < h ||
					m < 0 || 59 < m ||
					s < 0 || 59 < s
					)
					throw null; // never

				return
					y * 10000000000L +
					m * 100000000L +
					d * 1000000L +
					h * 10000L +
					i * 100L +
					s;
			}

			public static long ToSec(DateTime dateTime)
			{
				return ToSec(ToTimeStamp(dateTime));
			}

			public static long ToTimeStamp(DateTime dateTime)
			{
				return
					10000000000L * dateTime
						.Year + // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
					100000000L * dateTime
						.Month + // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
					1000000L * dateTime
						.Day + // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
					10000L * dateTime
						.Hour + // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
					100L * dateTime
						.Minute + // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
					dateTime
						.Second; // KeepComment:@^_ConfuserElsa // NoRename:@^_ConfuserElsa
			}
		}

		#endregion

		#region SimpleDateTime

		/// <summary>
		/// 日時の範囲：1/1/1 00:00:00 ～ 922337203/12/31 23:59:59
		/// </summary>
		public struct SimpleDateTime
		{
			public readonly int Year;
			public readonly int Month;
			public readonly int Day;
			public readonly int Hour;
			public readonly int Minute;
			public readonly int Second;
			public readonly string Weekday;

			public static SimpleDateTime Now()
			{
				return new SimpleDateTime(DateTime.Now);
			}

			public static SimpleDateTime FromTimeStamp(long timeStamp)
			{
				return new SimpleDateTime(TimeStampToSec.ToSec(timeStamp));
			}

			public SimpleDateTime(DateTime dateTime)
				: this(TimeStampToSec.ToSec(dateTime))
			{ }

			public SimpleDateTime(long sec)
			{
				long timeStamp = TimeStampToSec.ToTimeStamp(sec);
				long t = timeStamp;

				this.Second = (int)(t % 100L);
				t /= 100L;
				this.Minute = (int)(t % 100L);
				t /= 100L;
				this.Hour = (int)(t % 100L);
				t /= 100L;
				this.Day = (int)(t % 100L);
				t /= 100L;
				this.Month = (int)(t % 100L);
				this.Year = (int)(t / 100L);

				this.Weekday = new string(new char[] { "月火水木金土日"[(int)(TimeStampToSec.ToSec(timeStamp) / 86400 % 7)] });
			}

			public override string ToString()
			{
				return this.ToString("{0}/{1:D2}/{2:D2} ({3}) {4:D2}:{5:D2}:{6:D2}");
			}

			public string ToString(string format)
			{
				return string.Format(format, this.Year, this.Month, this.Day, this.Weekday, this.Hour, this.Minute, this.Second);
			}

			public DateTime ToDateTime()
			{
				return new DateTime(this.Year, this.Month, this.Day, this.Hour, this.Minute, this.Second);
			}

			public long ToTimeStamp()
			{
				return
					10000000000L * this.Year +
					100000000L * this.Month +
					1000000L * this.Day +
					10000L * this.Hour +
					100L * this.Minute +
					this.Second;
			}

			public long ToSec()
			{
				return TimeStampToSec.ToSec(this.ToTimeStamp());
			}

			public static SimpleDateTime operator +(SimpleDateTime instance, long sec)
			{
				return new SimpleDateTime(instance.ToSec() + sec);
			}

			public static SimpleDateTime operator +(long sec, SimpleDateTime instance)
			{
				return new SimpleDateTime(instance.ToSec() + sec);
			}

			public static SimpleDateTime operator -(SimpleDateTime instance, long sec)
			{
				return new SimpleDateTime(instance.ToSec() - sec);
			}

			public static long operator -(SimpleDateTime a, SimpleDateTime b)
			{
				return a.ToSec() - b.ToSec();
			}

			public static bool operator ==(SimpleDateTime a, SimpleDateTime b)
			{
				return a.ToTimeStamp() == b.ToTimeStamp();
			}

			public static bool operator !=(SimpleDateTime a, SimpleDateTime b)
			{
				return !(a == b);
			}

			public override bool Equals(object other)
			{
				return other is SimpleDateTime && this == (SimpleDateTime)other;
			}

			public override int GetHashCode()
			{
				return this.ToTimeStamp().GetHashCode();
			}
		}

		#endregion

		// < sync
	}
}
