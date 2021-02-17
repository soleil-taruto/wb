using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tests
{
	public class Test0004
	{
		public void Test01()
		{
			Class_01 a = new Class_01(1);
			Class_01 b = new Class_01(2);

			Console.WriteLine("a == b  ->  " + (a == b));
			Console.WriteLine("a != b  ->  " + (a != b));
			Console.WriteLine("a.Equals(b)  ->  " + a.Equals(b));

			b = null;

			Console.WriteLine("a == b  ->  " + (a == b));
			Console.WriteLine("a != b  ->  " + (a != b));
			Console.WriteLine("a.Equals(b)  ->  " + a.Equals(b));

			b = new Class_01(1);

			Console.WriteLine("a == b  ->  " + (a == b));
			Console.WriteLine("a != b  ->  " + (a != b));
			Console.WriteLine("a.Equals(b)  ->  " + a.Equals(b));
			Console.WriteLine("a.Equals(null)  ->  " + a.Equals(null));
			Console.WriteLine("a.Equals(\"ABC\")  ->  " + a.Equals("ABC"));
		}

		public class Class_01
		{
			public int Value;

			public Class_01(int value)
			{
				this.Value = value;
			}

			public static bool operator ==(Class_01 a, Class_01 b)
			{
				Console.WriteLine("operator ==, a: " + GetString(a) + ", b: " + GetString(b));

				return
					(object)a == null && (object)b == null ||
					(object)a != null && (object)b != null && a.Value == b.Value;
			}

			public static bool operator !=(Class_01 a, Class_01 b)
			{
				Console.WriteLine("operator !=, a: " + GetString(a) + ", b: " + GetString(b));

				return !(a == b);
			}

			public override int GetHashCode()
			{
				Console.WriteLine("GetHashCode: " + this.Value);

				return this.Value;
			}

			public override bool Equals(object other)
			{
				Console.WriteLine("Equals: " + this.Value + ", " + GetString(other));

				return this == other as Class_01;
			}
		}

		public static string GetString(object value)
		{
			return value == null ? "<null>" : value.ToString();
		}
	}
}
