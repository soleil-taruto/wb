using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Charlotte.Maps
{
	public class Tile
	{
		public string Name { get; private set; }
		public Bitmap Picture { get; private set; }

		public Tile(string name, Bitmap picture)
		{
			this.Name = name;
			this.Picture = picture;
		}
	}
}
