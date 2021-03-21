using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Commons;

namespace Charlotte.Maps
{
	public class Map
	{
		private MapCell[] Cells;
		public int W { get; private set; }
		public int H { get; private set; }

		private string[] ExtraLines = new string[0];

		public Map(string file)
		{
			string[] lines = File.ReadAllLines(file, SCommon.ENCODING_SJIS);
			int c = 0;

			int w = int.Parse(lines[c++]);
			int h = int.Parse(lines[c++]);

			this.Resize(w, h);

			for (int x = 0; x < w; x++)
			{
				for (int y = 0; y < h; y++)
				{
					while (c < lines.Length && lines[c] == "")
						c++;

					if (lines.Length <= c)
						return;

					string[] tokens = SCommon.Tokenize(lines[c++], "\t");
					int d = 0;

					d++; // Skip

					MapCell cell = this[x, y];

					cell.TileName = tokens[d++];
					cell.EnemyName = tokens[d++];
				}
			}
			this.ExtraLines = lines.Skip(c).ToArray();
		}

		public void Resize(int w, int h)
		{
			this.Cells = Enumerable.Range(0, w * h).Select(v => new MapCell()).ToArray();
			this.W = w;
			this.H = h;
		}

		public MapCell this[int x, int y]
		{
			get
			{
				return this.Cells[x * this.H + y];
			}
		}

		public void Save(string file)
		{
			List<string> dest = new List<string>();

			dest.Add("" + this.W);
			dest.Add("" + this.H);

			for (int x = 0; x < this.W; x++)
			{
				for (int y = 0; y < this.H; y++)
				{
					List<string> tokens = new List<string>();
					MapCell cell = this[x, y];

					tokens.Add("" + (cell.TileName == Consts.DEFAULT_TILE_NAME ? 0 : 1));
					tokens.Add(cell.TileName);
					tokens.Add(cell.EnemyName);

					dest.Add(string.Join("\t", tokens));
				}
			}
			dest.AddRange(this.ExtraLines);

			File.WriteAllLines(file, dest, SCommon.ENCODING_SJIS);
		}
	}
}
