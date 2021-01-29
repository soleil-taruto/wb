using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

class Program
{
	static void Main()
	{
		new Program().Main2();
	}

	class City
	{
		public int LineIndex;
		public int Pref;
		public int BirthYear;
		public int X;

		public string Get認識番号()
		{
			return Pref.ToString("D6") + X.ToString("D6");
		}
	}

	int N;
	int M;
	City[] Cities;

	void Main2()
	{
		int[] nm = Console.ReadLine().Split(' ').Select(v => int.Parse(v)).ToArray();
		N = nm[0];
		M = nm[1];
		Cities = new City[M];

		for (int i = 0; i < M; i++)
		{
			int[] vs = Console.ReadLine().Split(' ').Select(v => int.Parse(v)).ToArray();

			Cities[i] = new City()
			{
				LineIndex = i,
				Pref = vs[0],
				BirthYear = vs[1],
			};
		}

		Array.Sort(Cities, (a, b) =>
		{
			if (a == b)
				return 0;

			int ret = a.Pref - b.Pref;

			if (ret != 0)
				return ret;

			ret = a.BirthYear - b.BirthYear;

			if (ret != 0)
				return ret;

			throw null; // never
		});

		int lastPref = -1;
		int x = -1;

		foreach (City city in Cities)
		{
			if (lastPref != city.Pref)
			{
				lastPref = city.Pref;
				x = 1;
			}
			city.X = x;
			x++;
		}

		Array.Sort(Cities, (a, b) => a.LineIndex - b.LineIndex);

		foreach (City city in Cities)
		{
			Console.WriteLine(city.Get認識番号());
		}
	}
}
