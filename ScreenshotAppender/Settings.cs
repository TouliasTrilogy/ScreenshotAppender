using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenshotAppender
{
	/// <summary>
	/// Application settings
	/// </summary>
	public class Settings
	{
		public event EventHandler<bool> EnabledChanged;
		private bool _enabled;
		public bool Enabled
		{
			get
			{
				return _enabled;
			}
			set
			{
				if (_enabled != value)
				{
					_enabled = value;
					EnabledChanged?.Invoke(this, value);
				}
			}
		}
		public bool FirstRun;
		public bool ClearOnPasteCtrlV;
		public bool ClearOnPasteShiftInsert;
		public bool CaptureOnTrayClick;
		public bool MultiScreen;
		public bool CaptureAlt;
		public bool CaptureShift;
		public bool CaptureCtrl;
		public Keys CaptureKey;
		public bool PasteAlt;
		public bool PasteShift;
		public bool PasteCtrl;
		public Keys PasteKey;
		public bool PreventProcessingCaptureKey;
		public bool PreventProcessingPasteKey;
		public bool ComposeVertically;
		public bool Stack;
		public int StackSize;
		public int ReduceColors;

		public Settings()
		{
			FirstRun = true;
			Enabled = true;
			ClearOnPasteCtrlV = true;
			ClearOnPasteShiftInsert = true;
			CaptureOnTrayClick = true;
			MultiScreen = false;
			CaptureAlt = false;
			CaptureCtrl = false;
			CaptureShift = false;
			CaptureKey = Keys.PrintScreen;
			PasteAlt = false;
			PasteCtrl = false;
			PasteShift = false;
			PasteKey = Keys.None;
			PreventProcessingCaptureKey = true;
			PreventProcessingPasteKey = true;
			ComposeVertically = true;
			Stack = false;
			StackSize = 4;
			ReduceColors = 0;
		}

		private static string UserProfilePath
		{
			get
			{
				return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ScreenshotAppender");
			}
		}

		public static Settings Load(string fileName = "screenshotappender.json")
		{
			Settings retval = new Settings();
			if (String.IsNullOrEmpty(Path.GetDirectoryName(fileName)))
			{
				fileName = Path.Combine(UserProfilePath, fileName);
			}
			if (!Directory.Exists(Path.GetDirectoryName(fileName)))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(fileName));
			}
			if (File.Exists(fileName))
			{
				var settings = new JsonSerializerSettings { Error = (se, ev) => ev.ErrorContext.Handled = true };
				retval = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(fileName), settings);
			}
			else
			{
				File.WriteAllText(fileName, JsonConvert.SerializeObject(retval, Formatting.Indented));
			}
			return retval;
		}

		/// <summary>
		/// Save settings
		/// </summary>
		/// <param name="fileName"></param>
		public bool Save(string fileName = "screenshotappender.json")
		{
			bool retval = false;
			try
			{
				if (String.IsNullOrEmpty(Path.GetDirectoryName(fileName)))
				{
					fileName = Path.Combine(UserProfilePath, fileName);
				}
				if (!Directory.Exists(Path.GetDirectoryName(fileName)))
				{
					Directory.CreateDirectory(Path.GetDirectoryName(fileName));
				}
				File.WriteAllText(fileName, JsonConvert.SerializeObject(this, Formatting.Indented));
				retval = true;
			}
			catch
			{
				//Do nothing 
			}
			return retval;
		}
	}
}
