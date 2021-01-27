using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Commons;

namespace Charlotte.Maps
{
	public static class MapSaver
	{
		public static void Save(Map map)
		{
			List<string> dest = new List<string>();

			dest.Add("" + map.W);
			dest.Add("" + map.H);

			for (int x = 0; x < map.W; x++)
			{
				for (int y = 0; y < map.H; y++)
				{
					List<string> tokens = new List<string>();
					MapCell cell = map[x, y];

					tokens.Add("" + (cell.Tile.Name == Consts.DEFAULT_TILE_NAME ? 0 : 1));
					tokens.Add(cell.Tile.Name);
					tokens.Add(cell.EnemyName);

					dest.Add(string.Join("\t", tokens));
				}
			}
			dest.AddRange(map.ExtraLines);

			File.WriteAllLines(map.LoadedFile, dest, SCommon.ENCODING_SJIS);
		}
	}
}
