﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace AccessLamp
{
	/// <summary>
	/// モニタ中のパフォーマンスカウンタの状態
	/// PerformanceCounter の Wrapper
	/// </summary>
	public class PerfCntrInfo : IDisposable
	{
		private PerformanceCounter Inner;

		public PerfCntrInfo(PerformanceCounter binding_inner)
		{
			this.Inner = binding_inner;
		}

		private bool NextValue()
		{
			return 0f < this.Inner.NextValue();
		}

		private const int BUSY_COUNT_MAX = 3;
		public const int ERROR_COUNT_MAX = 20;

		public int BusyCount = 0;
		public int ErrorCount = 0;

		public void Update()
		{
			if (this.Inner == null)
				return;

			if (this.NextValue())
			{
				if (this.BusyCount < BUSY_COUNT_MAX)
					this.BusyCount++;
			}
			else
				this.BusyCount = 0;

			this.ErrorCount = 0; // 成功したのでエラーカウント_クリア
		}

		public enum Status_e
		{
			DENIED,
			IDLE,
			BUSY,
			VERY_BUSY,
		}

		public Status_e GetStatus()
		{
			if (this.Inner == null)
				return Status_e.DENIED;

			if (this.BusyCount == 0)
				return Status_e.IDLE;

			if (this.BusyCount < BUSY_COUNT_MAX)
				return Status_e.BUSY;

			return Status_e.VERY_BUSY;
		}

		public void Dispose()
		{
			try
			{
				this.Inner.Close();
			}
			catch (Exception ex)
			{
				Ground.Logger.WriteLog(ex);
			}
			this.Inner = null;
		}
	}
}
