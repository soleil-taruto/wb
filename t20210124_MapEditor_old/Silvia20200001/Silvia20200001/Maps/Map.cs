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
		public string LoadedFile;

		private MapCell[] Cells;
		public int W { get; private set; }
		public int H { get; private set; }

		public Map(int w, int h)
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

		public string[] ExtraLines = new string[0];

		public static Map Load(string file)
		{
			return MapLoader.Load(file);
		}

		public void Save()
		{
			MapSaver.Save(this);
		}
	}
}
