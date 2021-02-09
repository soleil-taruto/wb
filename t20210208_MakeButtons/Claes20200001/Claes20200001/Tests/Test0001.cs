using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Charlotte.Commons;

namespace Charlotte.Tests
{
	public class Test0001
	{
		public void Test01()
		{
			{
				Canvas canvas = Canvas.Load(@"C:\etc\画像\1875384.jpg");
				canvas = canvas.Expand(777, 777);
				canvas.Save(@"C:\temp\1.png");
			}

			{
				Canvas canvas = Canvas.Load(@"C:\etc\画像\80347715_p7.png");
				canvas = canvas.Expand(777, 1111);
				canvas.Save(@"C:\temp\2.png");
			}
		}

		public void Test02()
		{
			{
				Canvas canvas = new Canvas(1200, 800);

				canvas.FillRect(new I4Rect(0, 0, canvas.W, canvas.H), new I4Color(255, 255, 255, 0));

				canvas.DrawCircle(new D2Point(100, 100), 100, new I4Color(0, 0, 255, 255));
				canvas.DrawCircle(new D2Point(1100, 100), 100, new I4Color(0, 0, 255, 255));
				canvas.DrawCircle(new D2Point(1100, 700), 100, new I4Color(0, 0, 255, 255));
				canvas.DrawCircle(new D2Point(100, 700), 100, new I4Color(0, 0, 255, 255));
				canvas.FillRect(new I4Rect(100, 0, 1000, 800), new I4Color(0, 0, 255, 255));
				canvas.FillRect(new I4Rect(0, 100, 1200, 600), new I4Color(0, 0, 255, 255));

				canvas.DrawCircle(new D2Point(100, 100), 50, new I4Color(255, 255, 0, 255));
				canvas.DrawCircle(new D2Point(1100, 100), 50, new I4Color(255, 255, 0, 255));
				canvas.DrawCircle(new D2Point(1100, 700), 50, new I4Color(255, 255, 0, 255));
				canvas.DrawCircle(new D2Point(100, 700), 50, new I4Color(255, 255, 0, 255));
				canvas.FillRect(new I4Rect(100, 50, 1000, 700), new I4Color(255, 255, 0, 255));
				canvas.FillRect(new I4Rect(50, 100, 1100, 600), new I4Color(255, 255, 0, 255));

				canvas = canvas.DrawString("テ1", 400, new I4Color(255, 0, 0, 255), 50, 50);

				canvas = canvas.Expand(300, 200);

				canvas.Save(@"C:\temp\1.png");
			}

			{
				Canvas canvas = new Canvas(1200, 800);

				canvas.FillRect(new I4Rect(0, 0, canvas.W, canvas.H), new I4Color(255, 255, 255, 0));

				canvas.DrawCircle(new D2Point(200, 200), 200, new I4Color(0, 0, 255, 255));
				canvas.DrawCircle(new D2Point(1000, 200), 200, new I4Color(0, 0, 255, 255));
				canvas.DrawCircle(new D2Point(1000, 600), 200, new I4Color(0, 0, 255, 255));
				canvas.DrawCircle(new D2Point(200, 600), 200, new I4Color(0, 0, 255, 255));
				canvas.FillRect(new I4Rect(200, 0, 800, 800), new I4Color(0, 0, 255, 255));
				canvas.FillRect(new I4Rect(0, 200, 1200, 400), new I4Color(0, 0, 255, 255));

				canvas.DrawCircle(new D2Point(200, 200), 100, new I4Color(255, 255, 0, 255));
				canvas.DrawCircle(new D2Point(1000, 200), 100, new I4Color(255, 255, 0, 255));
				canvas.DrawCircle(new D2Point(1000, 600), 100, new I4Color(255, 255, 0, 255));
				canvas.DrawCircle(new D2Point(200, 600), 100, new I4Color(255, 255, 0, 255));
				canvas.FillRect(new I4Rect(200, 100, 800, 600), new I4Color(255, 255, 0, 255));
				canvas.FillRect(new I4Rect(100, 200, 1000, 400), new I4Color(255, 255, 0, 255));

				canvas = canvas.DrawString("テ2", 400, new I4Color(255, 0, 0, 255), 50, 50);

				canvas = canvas.Expand(300, 200);

				canvas.Save(@"C:\temp\2.png");
			}
		}
	}
}
