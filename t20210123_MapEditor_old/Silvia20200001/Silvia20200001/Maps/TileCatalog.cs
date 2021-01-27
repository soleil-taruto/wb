using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;

namespace Charlotte.Maps
{
	public class TileCatalog
	{
		public List<Tile> Tiles = new List<Tile>();

		public Tile GetTile(string name)
		{
			int index = SCommon.IndexOf(this.Tiles, tile => tile.Name == name);

			if (index == -1)
				throw new Exception("no tile: " + name);

			return this.Tiles[index];
		}
	}
}
