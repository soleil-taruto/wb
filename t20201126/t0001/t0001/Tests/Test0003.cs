using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Charlotte.Commons;

namespace Charlotte.Tests
{
	public class Test0003
	{
		private static byte[] RBF_FileData;
		private static int RBF_Reader;

		private static uint RBF_ReadValue(int width)
		{
			uint value = 0u;

			for (int index = 0; index < width; index++)
				value |= (uint)RBF_FileData[RBF_Reader++] << (index * 8);

			return value;
		}

		public static I3Color[,] ReadBmpFile(byte[] fileData, out int xSize, out int ySize)
		{
			if (fileData == null) throw new ArgumentException();

			RBF_FileData = fileData;
			RBF_Reader = 0;

			UInt16 bfhType = (UInt16)RBF_ReadValue(2);
			uint bfhSize = RBF_ReadValue(4);
			uint bfhReserved_0102 = RBF_ReadValue(4);
			uint bfhOffBits = RBF_ReadValue(4);

			uint bfiSize = RBF_ReadValue(4);
			uint bfiWidth = RBF_ReadValue(4);
			uint bfiHeight = RBF_ReadValue(4);
			UInt16 bfiPlanes = (UInt16)RBF_ReadValue(2);
			UInt16 bfiBitCount = (UInt16)RBF_ReadValue(2);
			uint bfiCompression = RBF_ReadValue(4);
			uint bfiSizeImage = RBF_ReadValue(4);
			uint bfiXPelsPerMeter = RBF_ReadValue(4);
			uint bfiYPelsPerMeter = RBF_ReadValue(4);
			uint bfiClrUsed = RBF_ReadValue(4);
			uint bfiClrImportant = RBF_ReadValue(4);

			const uint BMP_SIGNATURE = 0x4d42; // 'B' | 'M' << 8

			if (bfhType != BMP_SIGNATURE) throw new Exception("Bad BMP");

			bool hiSign = (bfiHeight & 0x80000000u) != 0u;

			if (hiSign)
				bfiHeight = (bfiHeight ^ 0xffffffffu) + 1u;

			if (bfiWidth == 0) throw new Exception("Bad BMP");
			if (bfiHeight == 0) throw new Exception("Bad BMP");
			if (SCommon.IMAX / bfiWidth < bfiHeight) throw new Exception("Bad BMP");

			int colPalCnt;

			switch (bfiBitCount)
			{
				case 1: colPalCnt = 2; break;
				case 4: colPalCnt = 16; break;
				case 8: colPalCnt = 256; break;

				case 24:
				case 32:
					colPalCnt = 0;
					break;

				default:
					throw new Exception("Bad BMP");
			}

			I3Color[] colorPallet = new I3Color[colPalCnt];

			for (int index = 0; index < colPalCnt; index++)
			{
				uint cR;
				uint cG;
				uint cB;

				// BGR 注意
				cB = RBF_ReadValue(1);
				cG = RBF_ReadValue(1);
				cR = RBF_ReadValue(1);
				RBF_ReadValue(1); // reserved

				colorPallet[index] = new I3Color((int)cR, (int)cG, (int)cB);
			}

			I3Color[,] bmp = new I3Color[bfiWidth, bfiHeight];

			for (int yy = 0; yy < (int)bfiHeight; yy++)
			{
				int y;

				if (hiSign)
					y = yy;
				else
					y = (int)bfiHeight - 1 - yy;

				if (bfiBitCount <= 8)
				{
					int bitMax = 8 / (int)bfiBitCount;
					int x = 0;

					while (x < bfiWidth)
					{
						uint c8 = RBF_ReadValue(1);

						for (int bit = 0; bit < bitMax && x < bfiWidth; bit++, x++)
						{
							bmp[x, y] = colorPallet[(int)(c8 & ((1u << bfiBitCount) - 1u))];
							c8 >>= bfiBitCount;
						}
					}
					RBF_ReadValue((int)((4u - ((bfiWidth + bitMax - 1u) / bitMax) % 4u) % 4u)); // Skip
				}
				else if (bfiBitCount == 24)
				{
					for (int x = 0; x < (int)bfiWidth; x++)
					{
						uint cR;
						uint cG;
						uint cB;

						// BGR 注意
						cB = RBF_ReadValue(1);
						cG = RBF_ReadValue(1);
						cR = RBF_ReadValue(1);

						bmp[x, y] = new I3Color((int)cR, (int)cG, (int)cB);
					}
					RBF_ReadValue((int)(bfiWidth % 4u)); // Skip
				}
				else // ? bfiBitCount == 32
				{
					for (int x = 0; x < (int)bfiWidth; x++)
					{
						uint cR;
						uint cG;
						uint cB;

						// BGR 注意
						cB = RBF_ReadValue(1);
						cG = RBF_ReadValue(1);
						cR = RBF_ReadValue(1);
						RBF_ReadValue(1); // reserved

						bmp[x, y] = new I3Color((int)cR, (int)cG, (int)cB);
					}
				}
			}

			// clear
			RBF_FileData = null;
			RBF_Reader = -1;

			xSize = (int)bfiWidth;
			ySize = (int)bfiHeight;

			return bmp;
		}

		private static List<byte> WBF_FileData = new List<byte>();

		private static void WBF_WriteValue(uint value)
		{
			WBF_FileData.Add((byte)(value & 0xff));
			value >>= 8;
			WBF_FileData.Add((byte)(value & 0xff));
			value >>= 8;
			WBF_FileData.Add((byte)(value & 0xff));
			value >>= 8;
			WBF_FileData.Add((byte)(value & 0xff));
		}

		public static byte[] WriteBmpFile(I3Color[,] bmp, int xSize, int ySize)
		{
			WBF_FileData = new List<byte>();

			const int PIXEL_NUM_MAX = 0x2a000000; // (int.MaxValue / 3) あたり -- sizeOfImage のため

			if (bmp == null) throw new ArgumentException();
			if (xSize < 1) throw new ArgumentException();
			if (ySize < 1) throw new ArgumentException();
			if (PIXEL_NUM_MAX / xSize < ySize) throw new ArgumentException();

			int sizeOfImage = ((xSize * 3 + 3) / 4) * 4 * ySize;

			// bfh
			WBF_FileData.Add(0x42); // 'B'
			WBF_FileData.Add(0x4d); // 'M'
			WBF_WriteValue((uint)sizeOfImage + 0x36);
			WBF_WriteValue(0);
			WBF_WriteValue(0x36);

			// bfi
			WBF_WriteValue(0x28);
			WBF_WriteValue((uint)xSize);
			WBF_WriteValue((uint)ySize);
			WBF_WriteValue(0x00180001); // Planes + BitCount
			WBF_WriteValue(0);
			WBF_WriteValue((uint)sizeOfImage);
			WBF_WriteValue(0);
			WBF_WriteValue(0);
			WBF_WriteValue(0);
			WBF_WriteValue(0);

			for (int y = ySize - 1; 0 <= y; y--)
			{
				for (int x = 0; x < xSize; x++)
				{
					I3Color color = bmp[x, y];

					// BGR 注意
					WBF_FileData.Add((byte)color.B);
					WBF_FileData.Add((byte)color.G);
					WBF_FileData.Add((byte)color.R);
				}
				for (int x = xSize % 4; 0 < x; x--)
				{
					WBF_FileData.Add(0);
				}
			}

			byte[] fileData = WBF_FileData.ToArray();
			WBF_FileData = null; // clear
			return fileData;
		}

		// ==== ==== ====
		// ==== ==== ====
		// ==== ==== ====

		public void Test01()
		{
			for (int c = 0; c < 1000; c++)
			{
				Console.WriteLine("1.c: " + c);

				Test01_a(10, 10);
			}
			for (int c = 0; c < 100; c++)
			{
				Console.WriteLine("2.c: " + c);

				Test01_a(100, 100);
			}
			for (int c = 0; c < 30; c++)
			{
				Console.WriteLine("3.c: " + c);

				Test01_a(300, 300);
			}
		}

		private void Test01_a(int w_max, int h_max)
		{
			int w = SCommon.CRandom.GetRange(1, w_max);
			int h = SCommon.CRandom.GetRange(1, h_max);

			Console.WriteLine("w, h: " + w + ", " + h); // test

			I3Color[,] bmp = new I3Color[w, h];

			for (int x = 0; x < w; x++)
				for (int y = 0; y < h; y++)
					bmp[x, y] = new I3Color(SCommon.CRandom.GetInt(256), SCommon.CRandom.GetInt(256), SCommon.CRandom.GetInt(256));

			byte[] bmpFileData = WriteBmpFile(bmp, w, h);

			int w2;
			int h2;

			I3Color[,] bmp2 = ReadBmpFile(bmpFileData, out w2, out h2);

			if (w != w2)
				throw null; // bugged !!!

			if (h != h2)
				throw null; // bugged !!!

			for (int x = 0; x < w; x++)
			{
				for (int y = 0; y < h; y++)
				{
					if (
						bmp[x, y].R != bmp2[x, y].R ||
						bmp[x, y].G != bmp2[x, y].G ||
						bmp[x, y].B != bmp2[x, y].B
						)
						throw null; // bugged !!!
				}
			}

			string bmpFile = @"C:\temp\0001.bmp";

			File.WriteAllBytes(bmpFile, bmpFileData);

			using (Bitmap bmp3 = (Bitmap)Bitmap.FromFile(bmpFile))
			{
				int w3 = bmp3.Width;
				int h3 = bmp3.Height;

				if (w != w3)
					throw null; // bugged !!!

				if (h != h3)
					throw null; // bugged !!!

				for (int x = 0; x < w; x++)
				{
					for (int y = 0; y < h; y++)
					{
						if (
							bmp[x, y].R != bmp3.GetPixel(x, y).R ||
							bmp[x, y].G != bmp3.GetPixel(x, y).G ||
							bmp[x, y].B != bmp3.GetPixel(x, y).B
							)
							throw null; // bugged !!!
					}
				}
			}
		}
	}
}
