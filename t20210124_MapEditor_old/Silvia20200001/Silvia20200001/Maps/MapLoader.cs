using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using System.IO;

namespace Charlotte.Maps
{
	public static class MapLoader
	{
		public static Map Load(string file)
		{
			file = SCommon.MakeFullPath(file);

			string[] lines = File.ReadAllLines(file, SCommon.ENCODING_SJIS);
			int c = 0;

			int w = int.Parse(lines[c++]);
			int h = int.Parse(lines[c++]);

			Map map = new Map(w, h);

			map.LoadedFile = file;

			for (int x = 0; x < w; x++)
			{
				for (int y = 0; y < h; y++)
				{
					while (c < lines.Length && lines[c] == "")
						c++;

					if (lines.Length <= c)
						goto endLoad;

					string[] tokens = SCommon.Tokenize(lines[c++], "\t");
					int d = 0;

					d++; // Skip

					string tileName = tokens[d++];
					string enemyName = tokens[d++];

					MapCell cell = map[x, y];

					cell.Tile = Ground.I.TileCatalog.GetTile(tileName);
					cell.EnemyName = enemyName;
				}
			}
			map.ExtraLines = lines.Skip(c).ToArray();

		endLoad:
			return map;
		}
	}
}
