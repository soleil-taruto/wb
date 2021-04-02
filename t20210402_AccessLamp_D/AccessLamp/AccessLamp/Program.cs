﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace AccessLamp
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
			SystemEvents.SessionEnding += new SessionEndingEventHandler(SessionEnding);

			Ground.SelfFile = Assembly.GetEntryAssembly().Location;
			Ground.SelfDir = Path.GetDirectoryName(Ground.SelfFile);

			Mutex mtx = new Mutex(false, "{7820a6a9-e65c-4c5b-a0b8-3bf29f191e22}");

			if (mtx.WaitOne(0) == false) // 多重起動防止
			{
				Mutex mtx_2 = new Mutex(false, "{ac989593-8acc-46db-b650-94bf4f08a3cd}");

				if (mtx_2.WaitOne(0) == false)
					return;

				Ground.Ev停止.Set();

				bool mtxOk = mtx.WaitOne(5000);

				mtx_2.ReleaseMutex();
				mtx_2.Close();

				if (mtxOk == false)
					return;
			}

			Ground.Setting.LoadFromFile();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainWin());

			Ground.Setting.SaveToFile();

			mtx.ReleaseMutex();
			mtx.Close();
		}

		public static string APP_TITLE = "AccessLamp";

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			try
			{
				MessageBox.Show(
					"[Application_ThreadException]\n" + e.Exception,
					"AccessLamp Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);
			}
			catch
			{ }

			Environment.Exit(1);
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			try
			{
				MessageBox.Show(
					"[CurrentDomain_UnhandledException]\n" + e.ExceptionObject,
					"AccessLamp Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);
			}
			catch
			{ }

			Environment.Exit(2);
		}

		private static void SessionEnding(object sender, SessionEndingEventArgs e)
		{
			Environment.Exit(3);
		}
	}
}
