using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccessLamp
{
	/// <summary>
	/// パフォーマンスカウンタのインスタンスの情報
	/// </summary>
	public class PCInstanceInfo
	{
		public string Name;
		public string DisplayName;
		public bool DisplayFlag = false; // 設定画面用

		public PCInstanceInfo()
			: this("C:")
		{ }

		public PCInstanceInfo(string name)
		{
			this.Name = name;
			this.DisplayName = name;
		}

		public string[] Serialize()
		{
			return new string[]
			{
				this.Name,
				this.DisplayName,
			};
		}

		public static PCInstanceInfo Deserialize(string[] lines)
		{
			PCInstanceInfo instance = new PCInstanceInfo();
			instance.S_Deserialize(lines);
			return instance;
		}

		private void S_Deserialize(string[] lines)
		{
			int c = 0;

			this.Name = lines[c++];
			this.DisplayName = lines[c++];
		}

		public override string ToString()
		{
			return this.Name + "　(表示名=" + this.DisplayName + ")　" + (this.DisplayFlag ? "[表示]" : "[非表示]");
		}
	}
}
