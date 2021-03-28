using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Charlotte.Commons;

namespace Charlotte
{
	public class Aggregate : IDisposable
	{
		private WorkingDir WD;
		private string CounterDir;
		private string EntityDir;

		public Aggregate()
		{
			this.WD = new WorkingDir();
			this.CounterDir = this.WD.MakePath();
			this.EntityDir = this.WD.MakePath();

			SCommon.CreateDir(this.CounterDir);
			SCommon.CreateDir(this.EntityDir);
		}

		public void Add(string line)
		{
			string hash = SCommon.Hex.ToString(SCommon.GetSubBytes(SCommon.GetSHA512(Encoding.UTF8.GetBytes(line)), 0, 24)); // SHA-512_192
			string counterFile = Path.Combine(this.CounterDir, hash);
			string entityFile = Path.Combine(this.EntityDir, hash);

			if (File.Exists(counterFile))
			{
				long counter = long.Parse(File.ReadAllText(counterFile, Encoding.ASCII));
				counter++;
				File.WriteAllText(counterFile, "" + counter);
			}
			else
			{
				File.WriteAllText(counterFile, "1", Encoding.ASCII);
				File.WriteAllText(entityFile, line, Encoding.UTF8);
			}
		}

		public IEnumerable<string> Top(int rankMax)
		{
			string outFile = this.WD.MakePath();
			string countEncHashListFile = this.WD.MakePath();
			string orderedCountEncHashListFile = this.WD.MakePath();

			SCommon.Batch(new string[] { "DIR /B > \"" + outFile + "\"" }, this.CounterDir);

			using (StreamReader reader = new StreamReader(outFile, Encoding.ASCII))
			using (StreamWriter writer = new StreamWriter(countEncHashListFile, false, Encoding.ASCII))
			{
				for (; ; )
				{
					string hash = reader.ReadLine();

					if (hash == null)
						break;

					if (hash == "")
						continue;

					hash = hash.ToLower();

					if (!Regex.IsMatch(hash, "^[0-9a-f]{48}$"))
						throw new Exception("Bad hash");

					string counterFile = Path.Combine(this.CounterDir, hash);
					long count = long.Parse(File.ReadAllText(counterFile, Encoding.ASCII));

					string entityFile = Path.Combine(this.EntityDir, hash);
					string entity = File.ReadAllText(entityFile, Encoding.UTF8);
					string miniEntity = Common.CutTrail(entity, 30);
					string enc = SCommon.Hex.ToString(Encoding.UTF8.GetBytes(miniEntity));

					writer.WriteLine(count.ToString("D19") + ":" + enc + ":" + hash);
				}
			}
			SCommon.Batch(new string[] { "SORT /R \"" + countEncHashListFile + "\" /O \"" + orderedCountEncHashListFile + "\"" });

			using (StreamReader reader = new StreamReader(orderedCountEncHashListFile, Encoding.ASCII))
			{
				long otherCount = 0;

				for (int rank = 1; ; rank++)
				{
					string line = reader.ReadLine();

					if (line == null)
						break;

					if (line == "")
						continue;

					string[] tokens = line.Split(':');

					if (tokens.Length != 3)
						throw null;

					long count = long.Parse(tokens[0]);
					//string enc = tokens[1]; // 不要

					if (rank <= rankMax)
					{
						string hash = tokens[2];
						string entityFile = Path.Combine(this.EntityDir, hash);
						string entity = File.ReadAllText(entityFile, Encoding.UTF8);

						yield return count + " " + entity;
					}
					else
					{
						otherCount += count;
					}
				}
				if (1 <= otherCount)
				{
					yield return otherCount + " (other)";
				}
			}
		}

		public void Dispose()
		{
			if (this.WD != null)
			{
				this.WD.Dispose();
				this.WD = null;
			}
		}
	}
}
