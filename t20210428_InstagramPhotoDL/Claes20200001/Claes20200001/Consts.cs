using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public static class Consts
	{
		/// <summary>
		/// indexファイルのエンコーディング
		/// </summary>
		public static readonly Encoding INDEX_FILE_ENCODING = Encoding.ASCII;

		/// <summary>
		/// indexファイル内のイメージURLの最大数(個数の最大値)
		/// </summary>
		public const int IMAGE_URL_COUNT_MAX = 30;

		/// <summary>
		/// 直前のイメージURLのリストを保存するファイル
		/// 出力フォルダの直下に置く
		/// </summary>
		public const string KNOWN_IMAGE_URL_LIST_LOCAL_FILE = "KnownImageUrlList.txt";

		/// <summary>
		/// ダミーURL
		/// </summary>
		public const string DUMMY_URL = "http://example.com/dummy.bin";
	}
}
