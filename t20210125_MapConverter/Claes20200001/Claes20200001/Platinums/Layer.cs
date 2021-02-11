using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Charlotte.Maps;

namespace Charlotte.Platinums
{
	public class Layer
	{
		public CsvData Parent;
		public int[,] Cells;
		public Tile[] Tiles;

		// <---- prm // HACK: abolished !!!

		public Tile GetTile(int x, int y)
		{
			return this.Tiles[this.Cells[x, y]];
		}
	}
}
