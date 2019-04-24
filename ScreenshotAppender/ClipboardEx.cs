using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenshotAppender
{
	/// <summary>
	/// Wrapper for Clipboard. For handle some events and easy switch between "internal" and standard Clipboard
	/// </summary>
	public class ClipboardEx 
	{
		public event EventHandler OnClear;
		public event EventHandler OnSetImage;

		private Image _internalClipboard;
		private bool _internal;

		public ClipboardEx(bool @internal)
		{
			_internal = @internal;
		}

		public void Clear()
		{
			if (_internal)
			{
				_internalClipboard = null;
			}
			else
			{
				Clipboard.Clear();
			}
			OnClear?.Invoke(this, new EventArgs());
		}

		public bool ContainsImage()
		{
			if (_internal)
			{
				return _internalClipboard != null;
			}
			else
			{
				return Clipboard.ContainsImage();
			}
		}

		public Image GetImage()
		{
			if (_internal)
			{
				return _internalClipboard;
			}
			else
			{
				return Clipboard.GetImage();
			}
		}

		public void SetImage(Image image)
		{
			if (_internal)
			{
				_internalClipboard = new Bitmap(image);
			}
			else
			{
				Clipboard.SetImage(image);
			}
			OnSetImage?.Invoke(this, new EventArgs());
		}
	}
}
