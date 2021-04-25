using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public static class Consts
	{
		/// <summary>
		/// ソースファイル検索対象ルートDIR
		/// </summary>
		public const string ROOT_DIR = @"C:\Dev";

		/// <summary>
		/// ソースファイルのエンコーディング
		/// </summary>
		public static readonly Encoding SOURCE_FILE_ENCODING = Encoding.UTF8;
	}
}
