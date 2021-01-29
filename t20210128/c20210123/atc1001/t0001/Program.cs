using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

class Program
{
	static void Main()
	{
		string line = Console.ReadLine();

		if (line[0] == line[1] && line[0] == line[2])
			Console.WriteLine("Won");
		else
			Console.WriteLine("Lost");
	}
}
