using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte
{
	public class Program
	{
		static void Main()
		{
#if DEBUG
			TestMain();
#else
			ProductMain();
#endif
		}

#if DEBUG
		private static void TestMain()
		{
			try
			{
				// -- choose one --

				new Test0001().Test01();
				//new Test0002().Test01();
				//new Test0003().Test01();

				// --

				Console.WriteLine("OK!");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			Console.WriteLine("Press ENTER");
			Console.ReadLine();
		}
#endif

		private static void ProductMain()
		{
			new Program().Perform();
		}

		protected int N;

		protected enum Op_e
		{
			AND = 1,
			OR
		};

		protected Op_e[] Ops;
		protected long Ans;

		protected virtual void Input()
		{
			N = int.Parse(Console.ReadLine());
			Ops = new Op_e[N];

			for (int i = 0; i < N; i++)
			{
				Ops[i] = Console.ReadLine() == "AND" ? Op_e.AND : Op_e.OR;
			}
		}

		protected virtual void Output()
		{
			Console.WriteLine(Ans);
		}

		protected void Perform()
		{
			Input();
			Ans = Search(N);
			Output();
		}

		private long Search(int index)
		{
			if (index == 0)
				return 1;

			Op_e op = Ops[index - 1];
			long ct;
			long cf;

			if (op == Op_e.AND)
			{
				ct = Search(index - 1);
				cf = 0;
			}
			else
			{
				ct = 1L << index;
				cf = Search(index - 1);
			}
			return ct + cf;
		}
	}
}
