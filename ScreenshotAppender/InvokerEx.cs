using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenshotAppender
{
	/// <summary>
	/// Helper class. Extend MethodInvoker
	/// </summary>
	public static class InvokerEx
	{
		public static void InvokeIfNeed(this Control control, Action action)
		{
			if (control == null || control.Disposing || control.IsDisposed)
			{
				return;
			}

			if (control.InvokeRequired)
			{
				try
				{
					control.Invoke(action);
				}
				catch
				{
					//Do nothing
				}
			}
			else
			{
				action.Invoke();
			}
		}
	}
}
