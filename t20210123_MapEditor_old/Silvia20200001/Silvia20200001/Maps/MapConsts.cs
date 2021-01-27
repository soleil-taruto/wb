using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Maps
{
	/// <summary>
	/// 定数
	/// </summary>
	public static class MapConsts
	{
		/// <summary>
		/// 何もない空間のタイル名
		/// </summary>
		public const string TILE_NONE = "None";

		/// <summary>
		/// 何も配置しない場合の敵の名前
		/// </summary>
		public const string ENEMY_NONE = "None";

		/// <summary>
		/// カタログ設定ファイルのローカル名
		/// </summary>
		public const string CATALOG_LOCAL_FILE = "TileEnemyCatalog.conf";

		/// <summary>
		/// パレットファイルのローカル名
		/// </summary>
		public const string PALLET_LOCAL_FILE = "Pallet.txt";

		public const int TILE_W = 32;
		public const int TILE_H = 32;

		public const int MAP_W_MIN = 30;
		public const int MAP_H_MIN = 17;
		public const int MAP_W_MAX = 1000;
		public const int MAP_H_MAX = 1000;
		public const int MAP_WxH_MAX = 30000;
	}
}
