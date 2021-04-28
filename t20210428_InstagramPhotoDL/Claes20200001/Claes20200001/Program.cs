using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Charlotte.Commons;
using Charlotte.Tests;

namespace Charlotte
{
	class Program
	{
		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2);
		}

		private void Main2(ArgsReader ar)
		{
			if (ProcMain.DEBUG)
			{
				Main3();
			}
			else
			{
				Main4(ar);
			}
			Common.OpenOutputDirIfCreated();
		}

		private void Main3()
		{
			// -- choose one --

			Test01();
			//new Test0001().Test01();
			//new Test0002().Test01();
			//new Test0003().Test01();

			// --

			//Common.Pause();
		}

		private void Test01()
		{
			string mst_indexFile = @"..\..\..\..\dat\barnatsutobi_instagram_index.html";
			string indexFile = @"C:\temp\barnatsutobi_instagram_index.html";
			string outputDir = @"C:\temp\output";

			SCommon.DeletePath(indexFile);
			File.Copy(mst_indexFile, indexFile);

			SCommon.DeletePath(outputDir);
			SCommon.CreateDir(outputDir);

			Main4_Args(indexFile, outputDir, "http://example.com/dummy/", ".jpg");
		}

		private void Main4(ArgsReader ar)
		{
			try
			{
				string indexFile = ar.NextArg();
				string outputDir = ar.NextArg();
				string imageUrlPrefix = ar.NextArg();
				string imageFileSuffix = ar.NextArg();

				Main4_Args(indexFile, outputDir, imageUrlPrefix, imageFileSuffix);
			}
			catch (Exception ex)
			{
				ProcMain.WriteLog(ex);

				// サーバーのバッチ処理なのでエラーでも(キー入力待ち等)止めてはならない。
			}
		}

		private void Main4_Args(string indexFile, string outputDir, string imageUrlPrefix, string imageFileSuffix)
		{
			if (!File.Exists(indexFile))
				throw new Exception("no indexFile");

			if (!Directory.Exists(outputDir))
				throw new Exception("no outputDir");

			if (string.IsNullOrEmpty(imageUrlPrefix))
				throw new Exception("Bad imageUrlPrefix");

			if (string.IsNullOrEmpty(imageFileSuffix))
				throw new Exception("Bad imageFileSuffix");

			List<string> imageUrls = new List<string>();

			{
				string[] lines = File.ReadAllLines(indexFile, Consts.INDEX_FILE_ENCODING);

				for (int index = 0; index < lines.Length; index++)
				{
					Common.Enclosed encl = Common.GetEnclosed(lines[index], "<img src=\"", "\"");

					if (encl != null)
					{
						string imageUrl = encl.Inner;

						if (!Common.LiteValidate(imageUrl, '\u0021', '\u007e', 1, 1000))
							throw new Exception("Bad imageUrl");

						string imageUrlNew = imageUrlPrefix + Common.IndexToImageLocalName(imageUrls.Count) + imageFileSuffix;
						lines[index] = encl.Left + imageUrlNew + encl.Right;
						imageUrls.Add(imageUrl);
					}
				}
				File.WriteAllLines(indexFile, lines, Consts.INDEX_FILE_ENCODING);
			}

			if (Consts.IMAGE_URL_COUNT_MAX < imageUrls.Count)
				throw new Exception("Too many imageUrl");

			string knownImageUrlListFile = Path.Combine(outputDir, Consts.KNOWN_IMAGE_URL_LIST_LOCAL_FILE);
			List<string> knownImageUrls;

			if (File.Exists(knownImageUrlListFile))
				knownImageUrls = File.ReadAllLines(knownImageUrlListFile, Encoding.UTF8).ToList();
			else
				knownImageUrls = new List<string>();

			Common.AdjustCount(knownImageUrls, imageUrls.Count, imageUrls.Count, Consts.DUMMY_URL);

			for (int index = 0; index < imageUrls.Count; index++)
			{
				string imageUrl = imageUrls[index];

				if (imageUrl != knownImageUrls[index])
				{
					byte[] imageFileData = DownloadByHGet.Download(imageUrl);
					string imageFile = Path.Combine(outputDir, Common.IndexToImageLocalName(index) + imageFileSuffix);

					File.WriteAllBytes(imageFile, imageFileData);

					knownImageUrls[index] = imageUrl;

					File.WriteAllLines(knownImageUrlListFile, knownImageUrls, Encoding.UTF8);
				}
			}
		}
	}
}
