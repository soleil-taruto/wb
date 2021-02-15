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
			Logger logger = new Logger();

			logger.Clear();

			logger.WriteLog(new string[]
			{
				"1",
			});

			logger.WriteLog(new string[]
			{
				"2",
			});

			logger.WriteLog(new string[]
			{
				"3",
			});
		}

		public void Test02()
		{
			Logger logger = new Logger();

			logger.Clear();

			logger.WriteLog(new string[]
			{
				"1",
				"2",
				"3",
			});

			logger.WriteLog(new string[]
			{
				"4",
				"5",
				"6",
			});

			logger.WriteLog(new string[]
			{
				"7",
				"8",
				"9",
			});
		}
	}
}
