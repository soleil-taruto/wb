using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.GameCommons;

namespace Charlotte
{
	public class ResourceMusic
	{
		//public DDMusic Dummy = new DDMusic(@"dat\General\muon.wav");

		public DDMusic MUS_BOSS_01 = new DDMusic(@"dat\Mirror of ES\nc213704.mp3");
		public DDMusic MUS_BOSS_01_v300 = new DDMusic(@"dat\Mirror of ES\nc213704_v300.mp3");
		public DDMusic MUS_BOSS_02 = new DDMusic(@"dat\Reda\nc136551.mp3");

		public ResourceMusic()
		{
			//this.Dummy.Volume = 0.1; // 非推奨
		}
	}
}
