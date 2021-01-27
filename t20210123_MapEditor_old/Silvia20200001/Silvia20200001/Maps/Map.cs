using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;
using System.IO;

namespace Charlotte.Maps
{
	public class Map
	{
		public string MapFile;
		public string CatalogFile;
		public string PalletFile;

		public EnemyCatalog EnemyCatalog;
		public TileCatalog TileCatalog;
		public MapData MapData;
		public MapData Pallet;

		#region Load

		public void Load(string mapFile)
		{
			this.MapFile = SCommon.MakeFullPath(mapFile);
			this.CatalogFile = this.GetCatalogFile();
			this.PalletFile = this.GetPalletFile();

			this.EnemyCatalog = new EnemyCatalog();
			this.TileCatalog = new TileCatalog();

			this.LoadCatalog();
			this.MapData = this.LoadMapData(this.MapFile);
			this.Pallet = this.LoadMapData(this.PalletFile);
		}

		private string GetCatalogFile()
		{
			string dir = this.MapFile;

			do
			{
				dir = Path.GetDirectoryName(dir);
				string file = Path.Combine(dir, MapConsts.CATALOG_LOCAL_FILE);

				if (File.Exists(file))
					return file;
			}
			while (3 < dir.Length); // ? ルートディレクトリではない。

			throw new Exception("カタログ設定ファイルが無い。");
		}

		private string GetPalletFile()
		{
			return Path.Combine(Path.GetDirectoryName(this.CatalogFile), "");
		}

		private void LoadCatalog()
		{
			string[] lines = File.ReadAllLines(this.CatalogFile, SCommon.ENCODING_SJIS).Where(line => line != "" && line[0] != ';').ToArray();
			int c = 0;

			for (; ; ) // タイル_リスト
			{
				string line = lines[c++];

				if (line == "\\d") // ? リスト終了
					break;

				string[] tokens = SCommon.Tokenize(line, "\t");
				int d = 0;

				Tile tile = new Tile();

				tile.Name = tokens[d++];
				tile.DisplayName = tokens[d++];
				tile.PictureFile = tokens[d++];

				this.TileCatalog.Tiles.Add(tile);
			}

			for (; ; ) // 敵_リスト
			{
				string line = lines[c++];

				if (line == "\\d") // ? リスト終了
					break;

				string[] tokens = SCommon.Tokenize(line, "\t");
				int d = 0;

				Enemy enemy = new Enemy();

				enemy.Name = tokens[d++];
				enemy.DisplayName = tokens[d++];

				this.EnemyCatalog.Enemies.Add(enemy);
			}

			if (lines[c++] != "\\e") // ? 設定項目終了
				throw new Exception("Bad Config");
		}

		private MapData LoadMapData(string file)
		{
			string[] lines = File.ReadAllLines(file, SCommon.ENCODING_SJIS);
			int c = 0;

			int w = int.Parse(lines[c++]);
			int h = int.Parse(lines[c++]);

			MapData mapData = new MapData(w, h);

			for (int x = 0; x < w; x++)
			{
				for (int y = 0; y < h; y++)
				{
					if (lines.Length <= c)
						goto endLoad;

					string[] tokens = SCommon.Tokenize(lines[c++], "\t");
					int d = 0;

					d++; // Skip

					string tileName = lines[c++];
					string enemyName = lines[c++];

					MapCell cell = mapData[x, y];

					cell.Tile = this.TileCatalog.GetTile(tileName);
					cell.Enemy = this.EnemyCatalog.GetEnemy(enemyName);
				}
			}
			mapData.ExtraLines = lines.Skip(c).ToArray();

		endLoad:
			return mapData;
		}

		#endregion

		#region Save

		public void Save()
		{
			List<string> dest = new List<string>();

			dest.Add("" + this.MapData.W);
			dest.Add("" + this.MapData.H);

			for (int x = 0; x < this.MapData.W; x++)
			{
				for (int y = 0; y < this.MapData.H; y++)
				{
					MapCell cell = this.MapData[x, y];

					Tile tile = cell.Tile;
					Enemy enemy = cell.Enemy;

					List<string> tokens = new List<string>();

					if (tile == null)
					{
						tokens.Add("" + 0);
						tokens.Add(MapConsts.TILE_NONE);
					}
					else
					{
						tokens.Add("" + 1);
						tokens.Add(tile.Name);
					}
					if (enemy == null)
						tokens.Add(MapConsts.ENEMY_NONE);
					else
						tokens.Add(enemy.Name);

					dest.Add(string.Join("\t", tokens));
				}
			}
			dest.AddRange(this.MapData.ExtraLines);

			File.WriteAllLines(this.MapFile, dest, SCommon.ENCODING_SJIS);
		}

		#endregion
	}
}
