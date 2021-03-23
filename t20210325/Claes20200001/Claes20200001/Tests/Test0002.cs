using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Commons;

namespace Charlotte.Tests
{
	public class Test0002
	{
		public void Test01()
		{
			SCommon.Batch(new string[] { @"ATTRIB.EXE -R C:\temp\1" }); // 前回の残り
			SCommon.DeletePath(@"C:\temp\1");
			File.WriteAllBytes(@"C:\temp\1", SCommon.EMPTY_BYTES);
			SCommon.Batch(new string[] { @"ATTRIB.EXE +R C:\temp\1" });
			Common.MustThrow(() => SCommon.DeletePath(@"C:\temp\1"));
		}

		public void Test02()
		{
			SCommon.Batch(new string[] { @"ATTRIB.EXE -R C:\temp\1" }); // 前回の残り
			SCommon.DeletePath(@"C:\temp\1");
			SCommon.CreateDir(@"C:\temp\1");
			SCommon.Batch(new string[] { @"ATTRIB.EXE +R C:\temp\1" });
			Common.MustThrow(() => SCommon.DeletePath(@"C:\temp\1"));
		}

		public void Test03()
		{
			SCommon.Batch(new string[] { @"ATTRIB.EXE -R C:\temp\1" }); // 前回の残り
			SCommon.DeletePath(@"C:\temp\1");
			File.WriteAllBytes(@"C:\temp\1", SCommon.EMPTY_BYTES);
			SCommon.Batch(new string[] { @"ATTRIB.EXE +R C:\temp\1" });
			Common.MustThrow(() => SCommon.CreateDir(@"C:\temp\1"));
		}
	}
}
