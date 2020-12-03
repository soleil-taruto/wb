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
	}
}
