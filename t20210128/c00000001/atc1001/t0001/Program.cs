using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

class Program
{
	static void Main()
	{
		int a = int.Parse(Console.ReadLine());
		int[] bc = Console.ReadLine().Split(' ').Select(v => int.Parse(v)).ToArray();
		string s = Console.ReadLine();

		Console.WriteLine((a + bc[0] + bc[1]) + " " + s);
	}
}
