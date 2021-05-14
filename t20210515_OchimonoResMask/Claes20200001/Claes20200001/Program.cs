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
				Main4();
			}
			Common.OpenOutputDirIfCreated();
		}

		private void Main3()
		{
			// -- choose one --

			Main4();
			//new Test0001().Test01();
			//new Test0002().Test01();
			//new Test0003().Test01();

			// --

			//Common.Pause();
		}

		private void Main4()
		{
			if (!Directory.Exists(Consts.ROOT_DIR))
				throw new Exception("no ROOT_DIR");

			MaskAllPicture();
			MaskAllText();
		}

		private void MaskAllPicture()
		{
			foreach (string file in Directory.GetFiles(Consts.ROOT_DIR, "*.png", SearchOption.AllDirectories))
			{
				Canvas canvas = Canvas.Load(file);
				canvas.Fill(new I4Color(
					SCommon.CRandom.GetInt(256),
					SCommon.CRandom.GetInt(256),
					SCommon.CRandom.GetInt(256), 255)); // ランダムな単色で塗り潰す。
				canvas.Save(file);
			}
		}

		private void MaskAllText()
		{
			foreach (string file in Directory.GetFiles(Consts.ROOT_DIR, "*.txt", SearchOption.AllDirectories))
			{
				File.WriteAllText(file, "テキスト削除済み", Encoding.UTF8);
			}
		}
	}
}
