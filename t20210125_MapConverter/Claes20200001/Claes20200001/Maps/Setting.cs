using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using Charlotte.Commons;

namespace Charlotte.Maps
{
	public class Setting
	{
		public I3Color TransparentColor;
		public I3Color SubTransparentColor;
		public string TileEnemyRootDir;
		public List<Tile> Tiles = new List<Tile>();
		public List<Tile> Enemies = new List<Tile>();

		public Setting(string file)
		{
			string[] lines = File.ReadAllLines(file, SCommon.ENCODING_SJIS).Where(line => line != "" && line[0] != ';').ToArray();
			int c = 0;

			{
				int r = int.Parse(lines[c++]);
				int g = int.Parse(lines[c++]);
				int b = int.Parse(lines[c++]);

				this.TransparentColor = new I3Color(r, g, b);
			}

			{
				int r = int.Parse(lines[c++]);
				int g = int.Parse(lines[c++]);
				int b = int.Parse(lines[c++]);

				this.SubTransparentColor = new I3Color(r, g, b);
			}

			{
				file = SCommon.MakeFullPath(file);
				string dir = Path.GetDirectoryName(file);
				dir = Path.Combine(dir, lines[c++]);
				dir = SCommon.MakeFullPath(dir);
				this.TileEnemyRootDir = dir;
			}

			if (lines[c++] != "\\d")
				throw new Exception("Bad Delimiter");

			this.LoadTiles(lines, ref c, this.Tiles);
			this.LoadTiles(lines, ref c, this.Enemies);

			if (lines[c++] != "\\e")
				throw new Exception("Bad Terminator");
		}

		private void LoadTiles(string[] lines, ref int c, List<Tile> dest)
		{
			for (; ; )
			{
				string line = lines[c++];

				if (line == "\\d") // ? Delimiter
					break;

				string[] tokens = SCommon.Tokenize(line, "\t", false, true);
				int d = 0;

				Tile tile = new Tile(
					tokens[d++],
					this.GetPicture(tokens.Skip(d).ToArray())
					);

				dest.Add(tile);
			}
		}

		private Bitmap GetPicture(string[] files)
		{
			Bitmap canvas = new Bitmap(Consts.TILE_W, Consts.TILE_H);
			Bitmap[] images = files.Select(file =>
			{
				Bitmap image;

				if (file[0] == '*') // ? 特殊な図柄
				{
					image = GetSpecialImage(file.Substring(1).Split(':'));
				}
				else // ? 画像ファイルから
				{
					string imageFile = Path.Combine(this.TileEnemyRootDir, file);

					if (!File.Exists(imageFile))
						throw new Exception("Bad imageFile: " + imageFile);

					image = (Bitmap)Bitmap.FromFile(imageFile);
				}
				return image;
			})
			.ToArray();

			for (int x = 0; x < Consts.TILE_W; x++)
			{
				for (int y = 0; y < Consts.TILE_H; y++)
				{
					canvas.SetPixel(x, y, this.TransparentColor.ToColor());

					foreach (Bitmap image in images)
					{
						I4Color pixcel_a = new I4Color(image.GetPixel((x * image.Width) / Consts.TILE_W, (y * image.Height) / Consts.TILE_H));

						if (1 <= pixcel_a.A)
						{
							I3Color pixcel = pixcel_a.WithoutAlpha();

							if (pixcel == this.TransparentColor)
								pixcel = this.SubTransparentColor;

							canvas.SetPixel(x, y, pixcel.ToColor());
						}
					}
				}
			}
			return canvas;
		}

		private static Bitmap GetSpecialImage(string[] arguments)
		{
			int c = 0;
			string command = arguments[c++];

			Bitmap image = new Bitmap(Consts.TILE_W, Consts.TILE_H);

			if (command == "CENTER-BOX")
			{
				int r = int.Parse(arguments[c++]);
				int g = int.Parse(arguments[c++]);
				int b = int.Parse(arguments[c++]);

				for (int x = 0; x < Consts.TILE_W; x++)
				{
					for (int y = 0; y < Consts.TILE_H; y++)
					{
						bool inCenterBox =
							10 <= x && x < 22 &&
							10 <= y && y < 22;

						image.SetPixel(x, y, inCenterBox ? Color.FromArgb(r, g, b) : Color.Transparent);
					}
				}
			}
			// 新しいコマンドをここへ追加..
			else
			{
				throw new Exception("不明なコマンド：" + command);
			}
			return image;
		}
	}
}
