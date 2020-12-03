#include "C:\Factory\Common\all.h"

int main(int argc, char **argv)
{
	int a;
	int b;
	int c;

	for(a = -20; a < 20; a++)
	for(b = -20; b < 20; b++)
	for(c = -20; c < 20; c++)
	{
		int numer =
			 c      * (b + c) * (c + a) +
			(a + b) *  a      * (c + a) +
			(a + b) * (b + c) *  b;

		int denom = (a + b) * (b + c) * (c + a);

		if(denom && numer == denom * 4)
		{
			cout("%d %d %d\n", a, b, c);
		}
	}
}
