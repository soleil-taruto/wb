﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Charlotte.Commons
{
	/// <summary>
	/// <para>アルファ値の無い色を表す。</para>
	/// <para>各色は 0 ～ 255 を想定する。</para>
	/// <para>R を -1 にすることによって無効な色を示す。</para>
	/// </summary>
	public struct I3Color
	{
		public int R; // -1 == 無効
		public int G;
		public int B;

		public I3Color(int r, int g, int b)
		{
			this.R = r;
			this.G = g;
			this.B = b;
		}

		public I3Color(Color color)
		{
			this.R = (int)color.R;
			this.G = (int)color.G;
			this.B = (int)color.B;
		}

		public override string ToString()
		{
			return string.Format("{0:x2}{1:x2}{2:x2}", this.R, this.G, this.B);
		}

		public I4Color WithAlpha(int a = 255)
		{
			return new I4Color(this.R, this.G, this.B, a);
		}

		public D3Color ToD3Color()
		{
			return new D3Color(
				this.R / 255.0,
				this.G / 255.0,
				this.B / 255.0
				);
		}

		public Color ToColor()
		{
			return Color.FromArgb(this.R, this.G, this.B);
		}

		public static bool operator ==(I3Color a, I3Color b)
		{
			return
				a.R == b.R &&
				a.G == b.G &&
				a.B == b.B;
		}

		public static bool operator !=(I3Color a, I3Color b)
		{
			return !(a == b);
		}

		public override bool Equals(object other)
		{
			return this == (I3Color)other;
		}

		public override int GetHashCode()
		{
			return
				(this.R << 16) |
				(this.G << 8) |
				(this.B << 0);
		}
	}
}
