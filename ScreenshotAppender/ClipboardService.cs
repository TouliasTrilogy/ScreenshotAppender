using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenshotAppender
{
	public static class ClipboardService
	{
		public static Dictionary<string, object> BackupClipboard()
		{
			Dictionary<string, object> retval = new Dictionary<string, object>();
			var dataObject = Clipboard.GetDataObject();
			foreach (var format in dataObject.GetFormats())
			{
				retval.Add(format, dataObject.GetData(format, false));
			}
			return retval;
		}

		public static void RestoreClipboard(Dictionary<string, object> data)
		{
			Clipboard.Clear();
			DataObject dataObject = new DataObject();
			foreach (var item in data)
			{
				dataObject.SetData(item.Key, true, item.Value);
			}
			Clipboard.SetDataObject(dataObject);
		}
	}
}
