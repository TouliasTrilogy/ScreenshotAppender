using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotAppender
{
	/// <summary>
	/// Helper class. Check and set\remove application to auto start
	/// </summary>
	public static class RegistryHelper
	{
		public static void SetAutorun(bool autorun)
		{
			using (RegistryKey registryAutorunKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
			{
				if (registryAutorunKey != null)
				{
					if (autorun)
					{
						if (registryAutorunKey.GetValueNames().Any(item => item == "Screenshot Appender"))
						{
							registryAutorunKey.DeleteValue("Screenshot Appender", false);
						}
						registryAutorunKey.SetValue("Screenshot Appender", Assembly.GetExecutingAssembly().Location, RegistryValueKind.String);
					}
					else
					{
						registryAutorunKey.DeleteValue("Screenshot Appender", false);
					}
				}
			}
		}

		public static bool IsAutoRun()
		{
			bool retval = false;
			using (RegistryKey registryAutorunKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", false))
			{
				if (registryAutorunKey != null)
				{
					if (registryAutorunKey.GetValueNames().Any(item => item == "Screenshot Appender"))
					{
						string currentExecutable = registryAutorunKey.GetValue("Screenshot Appender", String.Empty).ToString();
						retval = currentExecutable == Assembly.GetExecutingAssembly().Location;
					}
				}
			}
			return retval;
		}
	}
}
