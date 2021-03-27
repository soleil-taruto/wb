using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte
{
	public static class Consts
	{
		/// <summary>
		/// HTTインストール先ディレクトリ
		/// </summary>
		public const string HTT_DIR = @"C:\BlueFish\BlueFish\HTT";

		/// <summary>
		/// HTTアクセスログファイル
		/// 現在の書き出し先
		/// </summary>
		public static readonly string ACCESS_LOG_FILE_01 = Path.Combine(HTT_DIR, "AccessLog.dat");

		/// <summary>
		/// HTTアクセスログファイル
		/// バックアップ(1世代前)
		/// </summary>
		public static readonly string ACCESS_LOG_FILE_02 = Path.Combine(HTT_DIR, "AccessLog0.dat");

		/// <summary>
		/// HTTアクセスログファイルにヒットするフィルタ
		/// </summary>
		public const string ACCESS_LOG_FILE_FILTER = "AccessLog*.dat";

		/// <summary>
		/// ログ出力先ディレクトリ
		/// </summary>
		public const string DEST_LOG_DIR = @"C:\BlueFish\Log\HTTAccessLogLog";

		/// <summary>
		/// HTTアクセスログファイル読み書き用のミューテックス名
		/// </summary>
		public const string ACCESS_LOG_MUTEX_NAME = "{4e08b31a-9280-4ee0-9f9a-2ef462589893}"; // shared_uuid@ign
	}
}
