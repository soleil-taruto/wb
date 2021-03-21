using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using Charlotte.Commons;
using Charlotte.Maps;
using System.Drawing.Imaging;

namespace Charlotte.Platinums
{
	public class CsvData
	{
		public int W;
		public int H;

		public Layer TileLayer;
		public Layer EnemyLayer;

		public I3Color TransparentColor;

		public void LoadFromMap(Map map, Setting setting)
		{
			this.W = map.W;
			this.H = map.H;

			this.TileLayer = this.LFM_LoadLayer((x, y) => map[x, y].TileName, setting.Tiles);
			this.EnemyLayer = this.LFM_LoadLayer((x, y) => map[x, y].EnemyName, setting.Enemies);

			this.TransparentColor = setting.TransparentColor;
		}

		private Layer LFM_LoadLayer(Func<int, int, string> getCellName, List<Tile> tiles)
		{
			int[,] cells = new int[this.W, this.H];

			for (int x = 0; x < this.W; x++)
			{
				for (int y = 0; y < this.H; y++)
				{
					string name = getCellName(x, y);
					int cell = SCommon.IndexOf(tiles, tile => tile.Name == name);

					if (cell == -1)
						throw new Exception("no Tile: " + name);

					cells[x, y] = cell;
				}
			}

			Layer layer = new Layer(
				this,
				cells,
				tiles.ToArray()
				);

			return layer;
		}

		/// <summary>
		/// このインスタンスの内容を保存する。
		/// 保存先：map
		/// </summary>
		/// <param name="map">保存先マップ</param>
		/// <param name="setting">設定</param>
		public void SaveToMap(Map map, Setting setting)
		{
			// ---- Check ----

			for (int x = 0; x < this.W; x++)
			{
				for (int y = 0; y < this.H; y++)
				{
					STM_CheckTileName(setting.Tiles, this.TileLayer.GetTile(x, y).Name);
					STM_CheckTileName(setting.Enemies, this.EnemyLayer.GetTile(x, y).Name);
				}
			}

			// ----

			map.Resize(this.W, this.H);

			for (int x = 0; x < this.W; x++)
			{
				for (int y = 0; y < this.H; y++)
				{
					MapCell cell = map[x, y];

					cell.TileName = this.TileLayer.GetTile(x, y).Name;
					cell.EnemyName = this.EnemyLayer.GetTile(x, y).Name;
				}
			}
		}

		private static void STM_CheckTileName(List<Tile> tiles, string name)
		{
			if (!tiles.Any(tile => tile.Name == name))
				throw new Exception("no Tile: " + name);
		}

		public void LoadFromDir(string dir)
		{
			dir = SCommon.MakeFullPath(dir);

			if (!Directory.Exists(dir))
				throw new Exception("no dir");

			string csvDataFile = Path.Combine(dir, Consts.PL_CSV_DATA_LOCAL_FILE);
			string palletTileFile = Path.Combine(dir, Consts.PL_PALLET_TILE_LOCAL_FILE);
			string palletEnemyFile = Path.Combine(dir, Consts.PL_PALLET_ENEMY_LOCAL_FILE);
			string nameListTileFile = Path.Combine(dir, Consts.PL_NAME_LIST_TILE_LOCAL_FILE);
			string nameListEnemyFile = Path.Combine(dir, Consts.PL_NAME_LIST_ENEMY_LOCAL_FILE);

			if (!File.Exists(csvDataFile)) throw new Exception("no csvDataFile");
			if (!File.Exists(palletTileFile)) throw new Exception("no palletTileFile");
			if (!File.Exists(palletEnemyFile)) throw new Exception("no palletEnemyFile");
			if (!File.Exists(nameListTileFile)) throw new Exception("no nameListTileFile");
			if (!File.Exists(nameListEnemyFile)) throw new Exception("no nameListEnemyFile");

			using (CsvFileReader reader = new CsvFileReader(csvDataFile))
			{
				string[] row = reader.ReadRow();
				int c = 0;

				this.W = int.Parse(row[c++]); // マップの幅
				this.H = int.Parse(row[c++]); ; // マップの高さ
				if (int.Parse(row[c++]) != Consts.TILE_W) throw new Exception("NG: マップチップの幅"); // マップチップの幅
				if (int.Parse(row[c++]) != Consts.TILE_H) throw new Exception("NG: マップチップの高さ"); // マップチップの高さ
				if (int.Parse(row[c++]) != 2) throw new Exception("NG: レイヤー数"); // レイヤー数
				if (int.Parse(row[c++]) != 16) throw new Exception("NG: ビットカウント"); // ビットカウント(8,16)
				if (int.Parse(row[c++]) != 1) throw new Exception("NG: パスの格納方法"); // パスの格納方法(0=このCSVファイルからの相対パス,1=ファイル名のみを格納)
				if (int.Parse(row[c++]) != 1) throw new Exception("NG: パーツアライメント情報"); // パーツアライメント情報(0=パーツアライメント固定(8bit設定で16個折り返し,16bit設定で256個折り返し),1=パーツアライメント非固定)
				c++; // 透明パーツ有効フラグ(0=無効,1=有効)
				c++; // 透明パーツ番号
				if (int.Parse(row[c++]) != 1) throw new Exception("NG: カラーキー有効フラグ"); // カラーキー有効フラグ(0=無効,1=有効)
				int colorKey = int.Parse(row[c++]); // カラーキー(10進数で出力され,16進数で表すと0x00BBGGRRという形式です)

				this.TransparentColor = new I3Color(
					colorKey % 256,
					(colorKey / 256) % 256,
					(colorKey / 65536)
					);

				// ---- レイヤー.01 ----

				row = reader.ReadRow();
				c = 0;

				if (row[c++] != Consts.PL_TILE_LAYER_NAME) throw new Exception("NG: レイヤー名.01"); // レイヤー名
				if (row[c++] != Consts.PL_PALLET_TILE_LOCAL_FILE) throw new Exception("NG: パレットファイル名.01"); // パレットファイル名
				c++; // 可視状態(0=不可視,1=可視)

				// ---- レイヤー.02 ----

				row = reader.ReadRow();
				c = 0;

				if (row[c++] != Consts.PL_ENEMY_LAYER_NAME) throw new Exception("NG: レイヤー名.02"); // レイヤー名
				if (row[c++] != Consts.PL_PALLET_ENEMY_LOCAL_FILE) throw new Exception("NG: パレットファイル名.02"); // パレットファイル名
				c++; // 可視状態(0=不可視,1=可視)

				// ----

				LFD_DummyPicture = new Bitmap(Consts.TILE_W, Consts.TILE_H);

				using (Graphics g = Graphics.FromImage(LFD_DummyPicture))
				{
					g.FillRectangle(new SolidBrush(Consts.PL_DUMMY_COLOR), 0, 0, Consts.TILE_W, Consts.TILE_H);
				}

				this.TileLayer = this.LFD_LoadLayer(reader, palletTileFile, nameListTileFile);
				this.EnemyLayer = this.LFD_LoadLayer(reader, palletEnemyFile, nameListEnemyFile);

				LFD_DummyPicture = null;
			}
		}

		private static Bitmap LFD_DummyPicture;

		private Layer LFD_LoadLayer(CsvFileReader reader, string palletFile, string nameListFile)
		{
			int[,] cells = new int[this.W, this.H];

			for (int y = 0; y < this.H; y++)
			{
				string[] row = reader.ReadRow();

				for (int x = 0; x < this.W; x++)
					cells[x, y] = int.Parse(row[x]);
			}
			reader.ReadRow(); // レイヤーの直後の空行

			Tile[] tiles;

			{
				string[] names = File.ReadAllLines(nameListFile, SCommon.ENCODING_SJIS);

				tiles = names.Select(name => new Tile(
					name,
					LFD_DummyPicture // 注意：タイル画像はダミーなので、これを SaveToDir すると、パレットは壊れる。
					))
					.ToArray();
			}

			Layer layer = new Layer(
				this,
				cells,
				tiles
				);

			return layer;
		}

		public void SaveToDir(string dir)
		{
			dir = SCommon.MakeFullPath(dir);

			SCommon.DeletePath(dir);
			SCommon.CreateDir(dir);

			string csvDataFile = Path.Combine(dir, Consts.PL_CSV_DATA_LOCAL_FILE);
			string palletTileFile = Path.Combine(dir, Consts.PL_PALLET_TILE_LOCAL_FILE);
			string palletEnemyFile = Path.Combine(dir, Consts.PL_PALLET_ENEMY_LOCAL_FILE);
			string nameListTileFile = Path.Combine(dir, Consts.PL_NAME_LIST_TILE_LOCAL_FILE);
			string nameListEnemyFile = Path.Combine(dir, Consts.PL_NAME_LIST_ENEMY_LOCAL_FILE);

			using (CsvFileWriter writer = new CsvFileWriter(csvDataFile))
			{
				int colorKey =
					this.TransparentColor.R * 1 +
					this.TransparentColor.G * 256 +
					this.TransparentColor.B * 65536;

				writer.WriteCell("" + this.W); // マップの幅
				writer.WriteCell("" + this.H); // マップの高さ
				writer.WriteCell("" + Consts.TILE_W); // マップチップの幅
				writer.WriteCell("" + Consts.TILE_H); // マップチップの高さ
				writer.WriteCell("" + 2); // レイヤー数
				writer.WriteCell("" + 16); // ビットカウント(8,16)
				writer.WriteCell("" + 1); // パスの格納方法(0=このCSVファイルからの相対パス,1=ファイル名のみを格納)
				writer.WriteCell("" + 1); // パーツアライメント情報(0=パーツアライメント固定(8bit設定で16個折り返し,16bit設定で256個折り返し),1=パーツアライメント非固定)
#if true
				// @ 2021.1.27
				// 何故かカラーキーが効かないので、苦肉の策で透明パーツを有効にする。
				// 最初のパーツ(0)は常に None であるはず。
				//
				writer.WriteCell("" + 1); // 透明パーツ有効フラグ(0=無効,1=有効)
				writer.WriteCell("" + 0); // 透明パーツ番号
#else
				writer.WriteCell("" + 0); // 透明パーツ有効フラグ(0=無効,1=有効)
				writer.WriteCell("" + 0); // 透明パーツ番号
#endif
				writer.WriteCell("" + 1); // カラーキー有効フラグ(0=無効,1=有効)
				writer.WriteCell("" + colorKey); // カラーキー(10進数で出力され,16進数で表すと0x00BBGGRRという形式です)
				writer.EndRow();

				writer.WriteCell(Consts.PL_TILE_LAYER_NAME, true); // レイヤー名
				writer.WriteCell(Consts.PL_PALLET_TILE_LOCAL_FILE, true); // パレットファイル名
				writer.WriteCell("" + 1); // 可視状態(0=不可視,1=可視)
				writer.EndRow();

				writer.WriteCell(Consts.PL_ENEMY_LAYER_NAME, true); // レイヤー名
				writer.WriteCell(Consts.PL_PALLET_ENEMY_LOCAL_FILE, true); // パレットファイル名
				writer.WriteCell("" + 0); // 可視状態(0=不可視,1=可視)
				writer.EndRow();

				this.STD_WriteLayer(writer, this.TileLayer);
				this.STD_WriteLayer(writer, this.EnemyLayer);
			}

			this.STD_SavePalletFile(palletTileFile, nameListTileFile, this.TileLayer);
			this.STD_SavePalletFile(palletEnemyFile, nameListEnemyFile, this.EnemyLayer);
		}

		private void STD_WriteLayer(CsvFileWriter writer, Layer layer)
		{
			for (int y = 0; y < this.H; y++)
			{
				for (int x = 0; x < this.W; x++)
					writer.WriteCell("" + layer.Cells[x, y]);

				writer.EndRow();
			}
			writer.EndRow(); // レイヤーの直後の空行
		}

		private void STD_SavePalletFile(string palletFile, string nameListFile, Layer layer)
		{
			int pallet_w = Enumerable.Range(1, SCommon.IMAX).First(v => layer.Tiles.Length <= v * v);
			int pallet_h = (layer.Tiles.Length + pallet_w - 1) / pallet_w;

			using (Bitmap pallet = new Bitmap(pallet_w * Consts.TILE_W, pallet_h * Consts.TILE_H))
			{
				using (Graphics g = Graphics.FromImage(pallet))
				{
					g.FillRectangle(new SolidBrush(Consts.PL_DUMMY_COLOR), 0, 0, pallet.Width, pallet.Height);

					int tileIndex = 0;

					for (int y = 0; y < pallet_h; y++) // y -> x 注意
					{
						for (int x = 0; x < pallet_w; x++)
						{
							if (tileIndex < layer.Tiles.Length)
							{
								Tile tile = layer.Tiles[tileIndex++];

								g.DrawImage(tile.Picture, x * Consts.TILE_W, y * Consts.TILE_H);
							}
						}
					}
				}
				pallet.Save(palletFile, ImageFormat.Bmp);
			}
			File.WriteAllLines(nameListFile, layer.Tiles.Select(tile => tile.Name), SCommon.ENCODING_SJIS);
		}
	}
}
