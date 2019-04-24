using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenshotAppender
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			//Handle about embed dll's
			LoadResolver();
			Application.EnableVisualStyles();
			//Only one copy at one time can run
			_ = new Mutex(true, "Global/thisIsGlobalMutexForScreenshootAppender", out bool IsFirstInstance);
			if (!IsFirstInstance)
			{
				MessageBox.Show("Another copy of \"Screenshoot Appender\" is already running. Please close all \"Screenshoot Appender\" instances before running new copy.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}

		private static void LoadResolver()
		{
			AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolver);
		}

		/// <summary>
		/// Resolve embed DLL's
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		private static Assembly CurrentDomain_AssemblyResolver(object sender, ResolveEventArgs args)
		{
			String resourceName = Assembly.GetExecutingAssembly().FullName.Split(',').First() + "." + new AssemblyName(args.Name).Name + ".dll";
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
			{
				if (stream != null)
				{
					Byte[] assemblyData = new Byte[stream.Length];
					stream.Read(assemblyData, 0, assemblyData.Length);
					return Assembly.Load(assemblyData);
				}
			}
			return null;
		}
	}
}
