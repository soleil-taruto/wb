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
		private CsvData Parent;
		public int[,] Cells { get; private set; }
		public Tile[] Tiles { get; private set; }

		public Layer(CsvData parent, int[,] cells, Tile[] tiles)
		{
			this.Parent = parent;
			this.Cells = cells;
			this.Tiles = tiles;
		}

		public Tile GetTile(int x, int y)
		{
			return this.Tiles[this.Cells[x, y]];
		}
	}
}
