using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Charlotte
{
	class Program
	{
		static void Main(string[] args)
		{
			// -- choose one --

			Test01();

			// --
		}

		private static void Test01()
		{
			//Test01a("0001", new DDRandom0001(0u));
			//Test01a("0002", new DDRandom0002(0u));
			//Test01a("0002b", new DDRandom0002b(0ul));
			//Test01a("0002c", new DDRandom0002c(0ul));
			//Test01a("0002cx", new DDRandom0002cx(0ul));
			//Test01a("0003", new DDRandom0003(0u));
			//Test01a("0004", new DDRandom0004(0u));
			Test01a("Xorshift32", new DDRandomXorshift32(1u));
			Test01a("Xorshift64", new DDRandomXorshift64(1ul));
			Test01a("Xorshift64Hi", new DDRandomXorshift64Hi(1ul));
		}

		private static void Test01a(string name, IDDRandom random)
		{
			using (StreamWriter writer = new StreamWriter(@"C:\temp\" + name + ".txt", false, Encoding.ASCII))
			{
				for (int c = 0; c < 1000000; c++)
				{
					uint value = random.Next();

					writer.WriteLine(
						Common.LZPad(Convert.ToString(value, 2), 32) + "\t" +
						value.ToString("x8") + "\t" +
						Common.LZPad("" + value, 10) + "\t" +
						((double)value / uint.MaxValue).ToString("F9") + "\t" +
						Common.GetMeter((double)value / uint.MaxValue, 150)
						);
				}
			}
		}
	}
}
