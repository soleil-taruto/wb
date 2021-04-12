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
			Dictionary<string, int> map = SCommon.CreateDictionary<int>();

			using (CsvFileReader reader = new CsvFileReader(Consts.R_FILE, Encoding.UTF8))
			{
				foreach (string[] row in reader.ReadToEnd())
				{
					string key = int.Parse(row[0]) / 100 + "_" + row[4];

					if (!map.ContainsKey(key))
						map.Add(key, 0);

					map[key] += int.Parse(row[1]);
				}
			}
			using (CsvFileWriter writer = new CsvFileWriter(Consts.W_FILE, false, Encoding.UTF8))
			{
				foreach (string key in map.Keys.Sort(SCommon.Comp))
				{
					writer.WriteCell(key);
					writer.WriteCell("" + map[key]);
					writer.EndRow();
				}
			}
		}
	}
}
