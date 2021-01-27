using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using Charlotte.Commons;

namespace Charlotte.Maps
{
	public class TileCatalog
	{
		private List<Tile> Tiles = new List<Tile>();

		public void Load()
		{
			this.Tiles.Clear();

			foreach (string file in Directory.GetFiles(Ground.I.TilesDir))
			{
				this.Tiles.Add(new Tile()
				{
					Name = Path.GetFileNameWithoutExtension(file),
					Picture = (Bitmap)Bitmap.FromFile(file),
				});
			}
		}

		public Tile GetTile(string name)
		{
			int index = SCommon.IndexOf(this.Tiles, tile => tile.Name == name);

			if (index == -1)
				throw new Exception("不明なタイル名：" + name);

			return this.Tiles[index];
		}
	}
}
