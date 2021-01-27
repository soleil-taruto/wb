using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Commons;

namespace Charlotte.Maps
{
	public class EnemyCatalog
	{
		public List<Enemy> Enemies = new List<Enemy>();

		public Enemy GetEnemy(string name)
		{
			int index = SCommon.IndexOf(this.Enemies, enemy => enemy.Name == name);

			if (index == -1)
				throw new Exception("no enemy: " + name);

			return this.Enemies[index];
		}
	}
}
