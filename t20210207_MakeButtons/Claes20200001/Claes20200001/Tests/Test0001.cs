using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Charlotte.Tests
{
	public class Test0001
	{
		public void Test01()
		{
			Canvas canvas = new Canvas(3000, 500);

			canvas.Fill(Color.Transparent);
			canvas.DrawString("テスト0001", 300, Color.Blue, 0, 0);
			canvas.Bitmap.Save(@"C:\temp\1.png");
		}

		public void Test02()
		{
			Canvas canvas = CanvasUtils.GetText("テスト0001", 100, new Margin(50), Color.Transparent, Color.Blue);

			canvas.Bitmap.Save(@"C:\temp\1.png");
		}
	}
}
