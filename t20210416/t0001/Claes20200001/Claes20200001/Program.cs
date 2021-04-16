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
			CollectMail_File(@"C:\wb\20210416_gmailすべてのメール\すべてのメール.mbox");
		}

		private void CollectMail_File(string file)
		{
			CollectMail(File.ReadAllLines(file, Encoding.ASCII));
		}

		private void CollectMail(string[] lines)
		{
			List<byte[]> messages = new List<byte[]>();

			for (int index = 0; index < lines.Length; index++)
			{
				string line = lines[index];

				if (line == "X-Mailer: stm")
				{
					line = lines[++index];

					if (line != "")
						throw null; // 想定外

					line = lines[++index];

					if (!Regex.IsMatch(line, "^[A-Za-z0-9\\+\\/]*[\\=]{0,3}$")) // ? not BASE-64
						throw null; // 想定外

					messages.Add(SCommon.Base64.I.Decode(line));
					line = lines[++index];

					if (line != "")
						throw null; // 想定外
				}
			}

			foreach (byte[] message in messages)
			{
				File.WriteAllBytes(Common.NextOutputPath() + ".txt", message);
			}
		}
	}
}
