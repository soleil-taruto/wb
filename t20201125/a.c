#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	int a;
	int b;
	int c;

	// 2 P 31 / 3 * 2 R 3 / 2 == 563.5289171446*

	// 563 * 1126 * 1126 * 3 == 2141442564
	// 563 * 1126 * 1126 * 3 L 2 == 30.*

#define REACH 563

	for(a = -REACH; a <= REACH; a++)
	for(b = -REACH; b <= REACH; b++)
	for(c = -REACH; c <= REACH; c++)
	{
		int numer =
			(  c  ) * (b + c) * (c + a) +
			(a + b) * (  a  ) * (c + a) +
			(a + b) * (b + c) * (  b  );

		int denom =
			(a + b) * (b + c) * (c + a);

		if(denom && numer == denom * 4)
		{
			cout("%d %d %d\n", a, b, c);
		}
	}
}
