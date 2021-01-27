using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Charlotte
{
	public static class Consts
	{
		public const string SETTING_LOCAL_FILE = "Setting.conf";

		public const int TILE_W = 32;
		public const int TILE_H = 32;

		public const int MAP_W_MIN = 30;
		public const int MAP_H_MIN = 17;
		public const int MAP_W_MAX = 1000;
		public const int MAP_H_MAX = 1000;
		public const int MAP_WxH_MAX = 30000;

		public const string DEFAULT_TILE_NAME = "None";
		public const string DEFAULT_ENEMY_NAME = "None";

		// PL_* == Platinum マップデータ

		public const string PL_CSV_DATA_LOCAL_FILE = "Map.csv";
		public const string PL_PALLET_TILE_LOCAL_FILE = "Tile.bmp";
		public const string PL_PALLET_ENEMY_LOCAL_FILE = "Enemy.bmp";
		public const string PL_NAME_LIST_TILE_LOCAL_FILE = "Tile.bmp-names.dat";
		public const string PL_NAME_LIST_ENEMY_LOCAL_FILE = "Enemy.bmp-names.dat";

		public const string PL_TILE_LAYER_NAME = "タイル";
		public const string PL_ENEMY_LAYER_NAME = "敵";

		public static Color PL_DUMMY_COLOR = Color.DarkBlue;
	}
}
