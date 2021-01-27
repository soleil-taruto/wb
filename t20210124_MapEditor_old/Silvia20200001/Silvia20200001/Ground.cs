using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using Charlotte.Commons;
using Charlotte.Maps;

namespace Charlotte
{
	public class Ground
	{
		public static Ground I;

		public Ground()
		{
			// init TilesDir
			{
				string dir = "Tiles";

				if (!Directory.Exists(dir))
				{
					dir = @"..\..\..\..\doc\Tiles";

					if (!Directory.Exists(dir))
						throw new Exception("no Tiles Directory");
				}
				dir = SCommon.MakeFullPath(dir);

				this.TilesDir = dir;
			}

			// init DefaultTile
			{
				Bitmap picture = new Bitmap(Consts.TILE_W, Consts.TILE_H);

				using (Graphics g = Graphics.FromImage(picture))
				{
					g.FillRectangle(Brushes.DarkGreen, new Rectangle(0, 0, Consts.TILE_W, Consts.TILE_H));
				}

				this.DefaultTile = new Tile()
				{
					Name = Consts.DEFAULT_TILE_NAME,
					Picture = picture,
				};
			}
		}

		public TileCatalog TileCatalog = new TileCatalog();
		public Tile DefaultTile;

		// ★設定項目ここから

		public string TilesDir;

		// ★設定項目ここまで

		private static string SettingFile
		{
			get
			{
				return ProcMain.SelfFile + "-setting.dat";
			}
		}

		public void SaveSetting()
		{
			List<string> dest = new List<string>();

			dest.Add(ProcMain.APP_IDENT);

			// ★設定項目ここから

			dest.Add(this.TilesDir);

			// ★設定項目ここまで

			File.WriteAllLines(SettingFile, dest, Encoding.UTF8);
		}

		public void LoadSetting()
		{
			if (!File.Exists(SettingFile))
				return;

			string[] lines = File.ReadAllLines(SettingFile, Encoding.UTF8);
			int c = 0;

			if (lines[c++] != ProcMain.APP_IDENT)
				throw new Exception("Bad Setting File");

			// ★設定項目ここから

			this.TilesDir = lines[c++];

			// ★設定項目ここまで
		}

		public List<EditorWin> Editors = new List<EditorWin>();
	}
}
