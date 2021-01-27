using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Maps
{
	public class MapCell
	{
		public Tile Tile = Ground.I.DefaultTile;
		public string EnemyName = Consts.DEFAULT_ENEMY_NAME;
	}
}
