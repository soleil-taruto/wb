﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Charlotte.Commons
{
	/// <summary>
	/// <para>アルファ値を含む色を表す。</para>
	/// <para>各色は 0 ～ 255 を想定する。</para>
	/// <para>R を -1 にすることによって無効な色を示す。</para>
	/// </summary>
	public struct I4Color
	{
		public int R; // -1 == 無効
		public int G;
		public int B;
		public int A;

		public I4Color(int r, int g, int b, int a)
		{
			this.R = r;
			this.G = g;
			this.B = b;
			this.A = a;
		}

		public I4Color(Color color)
		{
			this.R = (int)color.R;
			this.G = (int)color.G;
			this.B = (int)color.B;
			this.A = (int)color.A;
		}

		public override string ToString()
		{
			return string.Format("{0:x2}{1:x2}{2:x2}{3:x2}", this.R, this.G, this.B, this.A);
		}

		public I3Color WithoutAlpha()
		{
			return new I3Color(this.R, this.G, this.B);
		}

		public D4Color ToD4Color()
		{
			return new D4Color(
				this.R / 255.0,
				this.G / 255.0,
				this.B / 255.0,
				this.A / 255.0
				);
		}

		public Color ToColor()
		{
			return Color.FromArgb(this.A, this.R, this.G, this.B); // 引数の並びは ARGB なので注意すること。
		}

		public static bool operator ==(I4Color a, I4Color b)
		{
			return
				a.R == b.R &&
				a.G == b.G &&
				a.B == b.B &&
				a.A == b.A;
		}

		public static bool operator !=(I4Color a, I4Color b)
		{
			return !(a == b);
		}

		public override bool Equals(object other)
		{
			return this == (I4Color)other;
		}

		public override int GetHashCode()
		{
			return
				(this.R << 24) |
				(this.G << 16) |
				(this.B << 8) |
				(this.A << 0);
		}
	}
}
