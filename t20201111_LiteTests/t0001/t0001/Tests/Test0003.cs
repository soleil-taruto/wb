using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tests
{
	public class Test0003
	{
		public void Test01()
		{
			Struct_01 s = new Struct_01(1);
			Console.WriteLine(s.Value); // --> 1
			s.UpdateMe();
			Console.WriteLine(s.Value); // --> 2
		}

		private class Class_01
		{
			public int Value;

			public Class_01(int value)
			{
				this.Value = value;
			}

			public void UpdateMe()
			{
				//this = new Class_01(2); // syntax error
			}
		}

		private struct Struct_01
		{
			public int Value;

			public Struct_01(int value)
			{
				this.Value = value;
			}

			public void UpdateMe()
			{
				this = new Struct_01(2);
			}
		}
	}
}
