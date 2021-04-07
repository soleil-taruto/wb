using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AccessLamp
{
	public class FontSettingInfo
	{
		public string Name;
		public int Size;
		public bool Bold;
		public bool Italic;
		public bool Strikeout;
		public bool Underline;

		public FontSettingInfo()
			: this(new Font("メイリオ", 10))
		{ }

		public FontSettingInfo(Font font)
		{
			this.Name = font.Name;
			this.Size = (int)(font.Size + 0.5f);
			this.Bold = font.Bold;
			this.Italic = font.Italic;
			this.Strikeout = font.Strikeout;
			this.Underline = font.Underline;
		}

		public Font GetFont()
		{
			try
			{
				return new Font(
					this.Name,
					this.Size,
					(this.Bold ? FontStyle.Bold : 0) |
					(this.Italic ? FontStyle.Italic : 0) |
					(this.Strikeout ? FontStyle.Strikeout : 0) |
					(this.Underline ? FontStyle.Underline : 0)
					);
			}
			catch
			{
				return new Font("メイリオ", 10);
			}
		}

		public string[] Serialize()
		{
			return new string[]
			{
				this.Name,
				"" + this.Size,
				"" + (this.Bold ? 1 : 0),
				"" + (this.Italic ? 1 : 0),
				"" + (this.Strikeout ? 1 : 0),
				"" + (this.Underline ? 1 : 0),
			};
		}

		public static FontSettingInfo Deserialize(string[] lines)
		{
			FontSettingInfo info = new FontSettingInfo();
			info.S_Deserialize(lines);
			return info;
		}

		private void S_Deserialize(string[] lines)
		{
			int c = 0;

			this.Name = lines[c++];
			this.Size = int.Parse(lines[c++]);
			this.Bold = int.Parse(lines[c++]) != 0;
			this.Italic = int.Parse(lines[c++]) != 0;
			this.Strikeout = int.Parse(lines[c++]) != 0;
			this.Underline = int.Parse(lines[c++]) != 0;
		}

		public override string ToString()
		{
			return string.Join(":",
				this.Name,
				this.Size,
				this.Bold ? "Bold" : "",
				this.Italic ? "Italic" : "",
				this.Strikeout ? "Strikeout" : "",
				this.Underline ? "Underline" : ""
				);
		}
	}
}
