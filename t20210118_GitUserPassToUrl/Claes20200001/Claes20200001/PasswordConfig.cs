using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	/// <summary>
	/// 接続情報_設定ファイル
	/// </summary>
	public class PasswordConfig
	{
		public string UserName;
		public string Password;

		public PasswordConfig()
		{
			string[] lines = Common.ReadConfigFile(Consts.PASSWORD_FILE);
			int c = 0;

			this.UserName = lines[c++];
			this.Password = lines[c++];

			if (!IsValidValue(this.UserName))
				throw new Exception("Bad UserName");
		}

		private static bool IsValidValue(string value)
		{
			return !value.Any(chr => !IsValidValueChar(chr));
		}

		private static bool IsValidValueChar(char chr)
		{
			// 使えない文字
			// -- 危なそうで使わなさそうな文字を置いておく
			//
			if (chr == '/') return false;
			if (chr == ':') return false;
			if (chr == '@') return false;

			if (0x21 <= chr && chr <= 0x7e) // ? ASCII 文字
				return true;

			return false;
		}
	}
}
