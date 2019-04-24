using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenshotAppender
{
	public static class ScreenCapturer
	{

		[DllImport("gdi32.dll")]
		static extern bool BitBlt(IntPtr hdcDest, int xDest, int yDest, int
		wDest, int hDest, IntPtr hdcSource, int xSrc, int ySrc, CopyPixelOperation rop);
		[DllImport("user32.dll")]
		static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDc);
		[DllImport("gdi32.dll")]
		static extern IntPtr DeleteDC(IntPtr hDc);
		[DllImport("gdi32.dll")]
		static extern IntPtr DeleteObject(IntPtr hDc);
		[DllImport("gdi32.dll")]
		static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);
		[DllImport("gdi32.dll")]
		static extern IntPtr CreateCompatibleDC(IntPtr hdc);
		[DllImport("gdi32.dll")]
		static extern IntPtr SelectObject(IntPtr hdc, IntPtr bmp);
		[DllImport("user32.dll")]
		public static extern IntPtr GetDesktopWindow();
		[DllImport("user32.dll")]
		public static extern IntPtr GetWindowDC(IntPtr ptr);

		/// <summary>
		/// Capture only main screen
		/// </summary>
		/// <returns></returns>
		public static Bitmap CaptureToBitmapSingle()
		{
			Bitmap retval = null;
			Size screenSize = Screen.PrimaryScreen.Bounds.Size;
			IntPtr hDesktop = GetDesktopWindow();
			IntPtr hSource = GetWindowDC(hDesktop);
			IntPtr hDestination = CreateCompatibleDC(hSource);
			IntPtr hBitmap = CreateCompatibleBitmap(hSource, screenSize.Width, screenSize.Height);
			IntPtr hOldBitmap = SelectObject(hDestination, hBitmap);
			_ = BitBlt(hDestination, 0, 0, screenSize.Width, screenSize.Height, hSource, 0, 0, CopyPixelOperation.SourceCopy | CopyPixelOperation.CaptureBlt);
			using (Bitmap bitmap = Bitmap.FromHbitmap(hBitmap))
			{
				SelectObject(hDestination, hOldBitmap);
				DeleteObject(hBitmap);
				DeleteDC(hDestination);
				ReleaseDC(hDesktop, hSource);
				retval = new Bitmap(bitmap);
			}
			return retval;
		}

		/// <summary>
		/// Capture all screens
		/// </summary>
		/// <returns></returns>
		public static Bitmap CaptureToBitmapMulti()
		{
			Bitmap retval = null;
			int screenLeft = SystemInformation.VirtualScreen.Left;
			int screenTop = SystemInformation.VirtualScreen.Top;
			int screenWidth = SystemInformation.VirtualScreen.Width;
			int screenHeight = SystemInformation.VirtualScreen.Height;
			using (Bitmap bitmap = new Bitmap(screenWidth, screenHeight))
			{
				// Draw the screenshot into our bitmap.
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.CopyFromScreen(screenLeft, screenTop, 0, 0, bitmap.Size);
				}
				retval = new Bitmap(bitmap);
			}
			return retval;
		}
	}
}
