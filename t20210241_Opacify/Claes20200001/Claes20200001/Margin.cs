using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class Margin
	{
		public int L;
		public int T;
		public int R;
		public int B;

		public Margin(int margin)
		{
			this.L = margin;
			this.T = margin;
			this.R = margin;
			this.B = margin;
		}

		public Margin(int l, int t, int r, int b)
		{
			this.L = l;
			this.T = t;
			this.R = r;
			this.B = b;
		}
	}
}
