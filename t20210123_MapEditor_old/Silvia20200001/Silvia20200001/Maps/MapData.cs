using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Maps
{
	public class MapData
	{
		private List<MapCell> Cells;
		public int W { get; private set; }
		public int H { get; private set; }

		public MapData(int w, int h)
		{
			this.Cells = Enumerable.Range(0, w * h).Select(v => new MapCell()).ToList();
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

		public string[] ExtraLines = new string[0];
	}
}
