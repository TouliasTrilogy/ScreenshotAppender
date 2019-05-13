using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SA = ScreenshotAppender.Properties.Resources;

namespace ScreenshotAppender
{
	public partial class Form1 : Form
	{
		private KeyboardHookListener _keyboardHookManager;
		private Settings _settings;
		private System.Threading.Timer _clickTimer;
		private NotifyList<Image> _images;
		private Image _lastImage;
		private System.Threading.Timer _cleanTimer;


		public Form1()
		{
			InitializeComponent();
			_images = new NotifyList<Image>();
			_lastImage = null;
			_images.OnChange += _images_OnChange;
			//Load\create settings
			_settings = Settings.Load();
			_settings_EnabledChanged(null, _settings.Enabled);
			//Set icon initial state
			notifyIcon.Icon = _settings.Enabled ? notifyIcon.Icon = SA.std_icon : notifyIcon.Icon = SA.red_icon;
			//Handle settings changed
			_settings.EnabledChanged += _settings_EnabledChanged;
			//Setup global hook
			Hooker hooker = new GlobalHooker();
			_keyboardHookManager = new KeyboardHookListener(hooker)
			{
				Enabled = true
			};
			//Handle keyboard up and down events
			_keyboardHookManager.KeyDown += _keyboardHookManager_KeyDown;
		}

		private void _images_OnChange(object sender, EventArgs e)
		{
			this.InvokeIfNeed(() =>
			{
				if (_settings.Enabled)
				{
					notifyIcon.Text = $"Screenshot Appender\r\nScreens in clipboard: {_images.Count}";
				}
				else
				{
					notifyIcon.Text = $"Screenshot Appender\r\nDisabled";
				}
				SaveClipboard_button.Enabled = ClearClipboard_button.Enabled = _images.Any();
			});
			if (_images.Count == 0)
			{
				GC.Collect();
				GC.WaitForPendingFinalizers();
				GC.Collect();
			}
		}

		private void _settings_EnabledChanged(object sender, bool e)
		{
			//Change tray icon according to enabled\disabled state
			if (e)
			{
				Disable_ToolStripMenuItem.Text = "Disable Screenshot Appender";
				notifyIcon.Icon = SA.std_icon;
			}
			else
			{
				Disable_ToolStripMenuItem.Text = "Enable Screenshot Appender";
				notifyIcon.Icon = SA.red_icon;
			}
		}


		private Point GetExactPosition(int row, int column, Size size)
		{
			return new Point(column * size.Width, row * size.Height);
		}

		/// <summary>
		/// Capture and place to internal\standard clipboard
		/// </summary>
		private void CaptureRoutine()
		{
			Size screenSize = Screen.PrimaryScreen.Bounds.Size;
			if (_settings.MultiScreen)
			{
				//Process all virtual desktop if allow multi screen
				screenSize = new Size(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
			}
			Bitmap currentScreen = _settings.MultiScreen ? ScreenCapturer.CaptureToBitmapMulti() : ScreenCapturer.CaptureToBitmapSingle();
			Image workingImage = currentScreen;
			switch (_settings.ReduceColors)
			{
				case 1:
					workingImage = ReducePNG(currentScreen, PixelFormat.Format24bppRgb);
					break;
				case 2:
					workingImage = ReducePNG(currentScreen, PixelFormat.Format16bppRgb555);
					break;
				case 3:
					workingImage = ReducePNG(currentScreen, PixelFormat.Format8bppIndexed);
					break;
			}
			_images.Add(workingImage);
			//Blink icon in tray. Just for fun :)
			BlinkTrayIcon();
		}

		private Image ReducePNG(Bitmap image, System.Drawing.Imaging.PixelFormat newFormat)
		{
			return image.Clone(new Rectangle(0, 0, image.Width, image.Height), newFormat);
		}


		private bool MemoryOverload()
		{
			Process process = Process.GetCurrentProcess();
			//Debug.WriteLine($"WorkingSet64: {process.WorkingSet64}\t\t{SizeSuffix(process.WorkingSet64)}");
			//Return true if WorkingSet > 1 GB
			return process.WorkingSet64 > 1073741824;
		}

		public static string SizeSuffix(long value)
		{
			string[] _sizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
			if (value < 0) { return "-" + SizeSuffix(-value); }
			if (value == 0) { return "0 bytes"; }
			int mag = (int)Math.Log(value, 1024);
			decimal adjustedSize = (decimal)value / (1L << (mag * 10));
			return $"{adjustedSize:F2} {_sizeSuffixes[mag]}";
		}

		private Image ComposeImage()
		{
			Bitmap bitmap = null;
			PixelFormat pixelFormat = PixelFormat.Format32bppArgb;
			switch (_settings.ReduceColors)
			{
				case 1:
					pixelFormat = PixelFormat.Format24bppRgb;
					break;
				case 2:
					pixelFormat = PixelFormat.Format16bppRgb555;
					break;
				case 3:
					pixelFormat = PixelFormat.Format8bppIndexed;
					break;
			}

			if (_images.Any())
			{
				int width = 0;
				int height = 0;
				if (!_settings.Stack)
				{
					//Compose images vertically or horizontally
					if (_settings.ComposeVertically)
					{
						width = _images[0].Width;
						height = _images.Sum(item => item.Height);
					}
					else
					{
						width = _images.Sum(item => item.Width);
						height = _images[0].Height;
					}
					try
					{
						bitmap = new Bitmap(width, height, pixelFormat);
					}
					catch (ArgumentException)
					{
						int imagesCount = _images.Count;
						//This make sense for prevent processing images when error message displayed
						_images.Clear();
						this.Invoke((MethodInvoker)(() =>
						{
							MessageBox.Show($"Unable to compose {imagesCount} images to bitmap with size {width}x{height}. You may need to retake last screen capture again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
						}));
						return null;
					}
					Graphics graphics = Graphics.FromImage(bitmap);
					int counter = 0;
					int positionX = 0;
					int positionY = 0;
					foreach (Image image in _images)
					{
						graphics.DrawImage(image, positionX, positionY);
						positionX = _settings.ComposeVertically ? 0 : positionX += image.Width;
						positionY = _settings.ComposeVertically ? positionY + image.Height : 0;
						counter++;
					}
				}
				else
				{
					//Calculate total image size based on first image
					if (_settings.ComposeVertically)
					{
						if (_images.Count <= _settings.StackSize)
						{
							width = _images[0].Width;
							height = _images[0].Height * _images.Count;
						}
						else
						{
							width = _images[0].Width * (_images.Count / _settings.StackSize + (_images.Count % _settings.StackSize > 0 ? 1 : 0));
							height = _images[0].Height * _settings.StackSize;
						}
					}
					else
					{
						if (_images.Count <= _settings.StackSize)
						{
							width = _images[0].Width * _images.Count;
							height = _images[0].Height;
						}
						else
						{
							width = _images[0].Width * _settings.StackSize;
							height = _images[0].Height * (_images.Count / _settings.StackSize + (_images.Count % _settings.StackSize > 0 ? 1 : 0));
						}
					}
					//Compose stack
					try
					{
						bitmap = new Bitmap(width, height, pixelFormat);
					}
					catch (ArgumentException)
					{
						int imagesCount = _images.Count;
						//This make sense for prevent processing images when error message displayed
						_images.Clear();
						this.Invoke((MethodInvoker)(() =>
						{
							MessageBox.Show(this, $"Unable to compose {imagesCount} images to bitmap with size {width}x{height}. You may need to retake last screen capture again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
						}));
						return null;
					}
					Graphics graphics = Graphics.FromImage(bitmap);
					int counter = 0;
					int currentRow = 0;
					int currentColumn = 0;
					foreach (Image image in _images)
					{
						Point imagePlacement = new Point(currentColumn * _images[0].Width, currentRow * _images[0].Height);
						graphics.DrawImage(image, imagePlacement);
						if (_settings.ComposeVertically)
						{
							currentRow += 1;
							if (currentRow == _settings.StackSize)
							{
								currentRow = 0;
								currentColumn += 1;
							}
						}
						else
						{
							currentColumn += 1;
							if (currentColumn == _settings.StackSize)
							{
								currentRow += 1;
								currentColumn = 0;
							}
						}
						counter++;
					}
				}
			}
			bitmap = ResizeImage(bitmap, _settings.ImageSize);
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
			return bitmap;
		}


		private void _keyboardHookManager_KeyDown(object sender, KeyEventArgs e)
		{
			if (!_settings.Enabled)
			{
				//Do nothing if disabled
				e.Handled = false;
				return;
			}
			//Process custom paste key
			if (_settings.PasteKey != Keys.None)
			{
				//Process custom paste key
				if (e.KeyCode == _settings.PasteKey)
				{
					if (_settings.PasteAlt && !e.Alt) { return; }
					if (_settings.PasteCtrl && !e.Control) { return; }
					if (_settings.PasteShift && !e.Shift) { return; }
					if (_lastImage != null)
					{
						e.Handled = true;
						SendKeys.SendWait("^{v}");
					}
				}
			}

			if ((e.KeyCode == Keys.V && !e.Alt && !e.Shift && e.Control) || (e.KeyCode == Keys.Insert && !e.Alt && e.Shift && !e.Control))
			{
				_cleanTimer = new System.Threading.Timer(CleanTimerRoutine, null, 500, Timeout.Infinite);
			}

			//Capture screen
			if (e.KeyCode == _settings.CaptureKey)
			{
				//Check modifiers
				if (_settings.CaptureAlt && !e.Alt) { return; }
				if (_settings.CaptureCtrl && !e.Control) { return; }
				if (_settings.CaptureShift && !e.Shift) { return; }
				//Set handled state according to settings
				e.Handled = _settings.PreventProcessingCaptureKey;
				//For process handled event
				Application.DoEvents();
				if (MemoryOverload())
				{
					//This run in new thread for prevent block current thread
					Task.Factory.StartNew(() =>
					{
						//Make sure all get event about we process capture key
						Thread.Sleep(100);
						//This run in new thread for prevent block current thread
						this.Invoke((MethodInvoker)(() =>
						{
							MessageBox.Show(this, $"Warning! {_images.Count} images captured. Unable to capture more images. Clear clipboard before continue.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
						}));
					});
					return;
				}
				CaptureRoutine();
				_lastImage = ComposeImage();
				this.Invoke((MethodInvoker)(() =>
				{
					try
					{
						if (_lastImage != null)
						{
							Clipboard.SetDataObject(_lastImage, false, 5, 100);
						}
					}
					catch
					{
						_images.Clear();
						//This run in new thread for prevent block current thread
						Task.Factory.StartNew(() =>
						{
							//Make sure all get event about we process capture key
							Thread.Sleep(100);
							this.Invoke((MethodInvoker)(() =>
							{
								MessageBox.Show(this, "Warning! Five attempts to write clipboard failed. You may need to retake last screen capture again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
							}));
						});

					}
				}));
			}
		}




		private void CleanTimerRoutine(object state)
		{
			if (_lastImage != null)
			{
				this.Invoke((MethodInvoker)(() =>
				{
					//Run in STA
					Clipboard.Clear();
				}));
			}
			_lastImage = null;
			_images.Clear();
		}

		/// <summary>
		/// Run only once at first run for show user icon
		/// </summary>
		private void SayHelloTrayIcon()
		{
			int counter = 0;
			this.InvokeIfNeed(() =>
			{
				notifyIcon.Icon = SA.red_icon;
				System.Threading.Timer blinkTimer = null;
				blinkTimer = new System.Threading.Timer(
				(object state) =>
				{
					if (counter % 2 == 0)
					{
						notifyIcon.Icon = SA.red_icon;
					}
					else
					{
						notifyIcon.Icon = SA.green_icon;
					}
					if (counter == 20)
					{
						blinkTimer.Dispose();
						notifyIcon.Icon = SA.std_icon;
					}
					counter++;
				}
				, null
				, 100 //Change icon back in 200 msec
				, 100);
			});

		}

		/// <summary>
		/// Visual indicate Print Screen operation
		/// </summary>
		private void BlinkTrayIcon()
		{
			if (_settings.Enabled)
			{
				this.InvokeIfNeed(() =>
				{
					notifyIcon.Icon = SA.green_icon;
					System.Threading.Timer blinkTimer = null;
					blinkTimer = new System.Threading.Timer(
					(object state) =>
					{
						notifyIcon.Icon = SA.std_icon;
						blinkTimer.Dispose();
					}
					, null
					, 200 //Change icon back in 200 msec
					, Timeout.Infinite);
				});
			}
		}


		private void Exit_ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void ClearClipboard_ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_images.Clear();
		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			if (_settings.FirstRun)
			{
				//Show message about we hide in tray
				notifyIcon.ShowBalloonTip(3000, "Screenshot Appender", "Screenshot Appender run in tray mode.\r\nThis message appear only once, on first run.", ToolTipIcon.Info);
				_settings.FirstRun = false;
				_settings.Save();
				SayHelloTrayIcon();
			}
			//On show check clipboard and setup initial counter.
			//We unable to count number of screen in stack mode

		}

		private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			SaveClipboardAs_toolStripMenuItem.Enabled = _images.Any();
			ClearClipboard_ToolStripMenuItem.Enabled = _images.Any();
		}


		private Bitmap ResizeImage(Bitmap original, int scale)
		{
			if (scale != 100)
			{
				Bitmap resized = new Bitmap((original.Width / 100) * _settings.ImageSize, (original.Height / 100) * scale);
				Graphics graphics = Graphics.FromImage(resized);
				graphics.DrawImage(original, new Rectangle(0, 0, resized.Width, resized.Height));
				return resized;
			}
			else
			{
				return original;
			}

		}

		private void SaveClipboardAs_toolStripMenuItem_Click(object sender, EventArgs e)
		{
			//Save content to file
			if (_images.Any())
			{
				if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
				{
					Image currentImage = ResizeImage((Bitmap)ComposeImage(), _settings.ImageSize);
					try
					{
						currentImage.Save(saveFileDialog.FileName, ImageFormat.Png);
					}
					catch (Exception ex)
					{
						MessageBox.Show(this, $"Unable to save file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
					}
				}
			}
			//Save content to file
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			//Fill hot key selector combo box
			Keys[] keys = (Keys[])Enum.GetValues(typeof(Keys));
			foreach (Keys key in keys.ToList().Distinct().Where(item => (int)item > 7 && (int)item < 256))
			{
				CaptureKeys_comboBox.Items.Add(key.ToString());
				PasteKeys_comboBox.Items.Add(key.ToString());
			}
			//Add none (off) key to custom paste key
			PasteKeys_comboBox.Items.Insert(0, Keys.None);
			//Setup GUI
			StartWithWindows_checkBox.Checked = RegistryHelper.IsAutoRun();
			Enabled_checkBox.Checked = _settings.Enabled;
			CaptureOnTrayClick_checkBox.Checked = _settings.CaptureOnTrayClick;
			ClearWhenPasteCtrlV_checkBox.Checked = _settings.ClearOnPasteCtrlV;
			ClearWhenPasteShiftIns_checkBox.Checked = _settings.ClearOnPasteShiftInsert;
			CaptureCtrl_checkBox.Checked = _settings.CaptureCtrl;
			CaptureAlt_checkBox.Checked = _settings.CaptureAlt;
			CaptureShift_checkBox.Checked = _settings.CaptureShift;
			CaptureKeys_comboBox.SelectedIndex = CaptureKeys_comboBox.FindStringExact(_settings.CaptureKey.ToString());
			PreventProcessingCaptureKey_checkBox.Checked = _settings.PreventProcessingCaptureKey;
			PasteCtrl_checkBox.Checked = _settings.PasteCtrl;
			PasteAlt_checkBox.Checked = _settings.PasteAlt;
			PasteShift_checkBox.Checked = _settings.PasteShift;
			PasteKeys_comboBox.SelectedIndex = PasteKeys_comboBox.FindStringExact(_settings.PasteKey.ToString());
			PreventProcessingPasteKey_checkBox.Checked = _settings.PreventProcessingPasteKey;
			MultiScreen_checkBox.Checked = _settings.MultiScreen;
			ComposeVertically_radioButton.Checked = _settings.ComposeVertically;
			ComposeHorizontally_radioButton.Checked = !_settings.ComposeVertically;
			Stack_checkBox.Checked = _settings.Stack;
			StackSize_numericUpDown.Value = _settings.StackSize;
			if (ReduceColors_comboBox.Items.Count - 1 >= _settings.ReduceColors)
			{
				ReduceColors_comboBox.SelectedIndex = _settings.ReduceColors;
			}
			else
			{
				//Old settings processed
				ReduceColors_comboBox.SelectedIndex = 0;
				_settings.ReduceColors = 0;
			}
			ImageSize_numericUpDown.Value = _settings.ImageSize;
			//Set event handler for change settings. We not set in designer for prevent multiple settings write on start application
			Enabled_checkBox.CheckedChanged += SettingsChanged;
			ClearWhenPasteCtrlV_checkBox.CheckedChanged += SettingsChanged;
			ClearWhenPasteShiftIns_checkBox.CheckedChanged += SettingsChanged;
			CaptureOnTrayClick_checkBox.CheckedChanged += SettingsChanged;
			CaptureCtrl_checkBox.CheckedChanged += SettingsChanged;
			CaptureAlt_checkBox.CheckedChanged += SettingsChanged;
			CaptureShift_checkBox.CheckedChanged += SettingsChanged;
			CaptureKeys_comboBox.SelectedIndexChanged += SettingsChanged;
			PreventProcessingCaptureKey_checkBox.CheckedChanged += SettingsChanged;
			PasteCtrl_checkBox.CheckedChanged += SettingsChanged;
			PasteAlt_checkBox.CheckedChanged += SettingsChanged;
			PasteShift_checkBox.CheckedChanged += SettingsChanged;
			PasteKeys_comboBox.SelectedIndexChanged += SettingsChanged;
			PreventProcessingPasteKey_checkBox.CheckedChanged += SettingsChanged;
			MultiScreen_checkBox.CheckedChanged += SettingsChanged;
			ComposeHorizontally_radioButton.CheckedChanged += SettingsChanged;
			ComposeVertically_radioButton.CheckedChanged += SettingsChanged;
			Stack_checkBox.CheckedChanged += SettingsChanged;
			StackSize_numericUpDown.ValueChanged += SettingsChanged;
			ReduceColors_comboBox.SelectedIndexChanged += SettingsChanged;
			ImageSize_numericUpDown.ValueChanged += SettingsChanged;
		}

		private void SettingsChanged(object sender, EventArgs e)
		{
			//Respond to change settings and save settings
			_settings.Enabled = Enabled_checkBox.Checked;
			_settings.CaptureOnTrayClick = CaptureOnTrayClick_checkBox.Checked;
			_settings.ClearOnPasteShiftInsert = ClearWhenPasteShiftIns_checkBox.Checked;
			_settings.ClearOnPasteCtrlV = ClearWhenPasteCtrlV_checkBox.Checked;
			_settings.PasteCtrl = PasteCtrl_checkBox.Checked;
			_settings.PasteAlt = PasteAlt_checkBox.Checked;
			_settings.PasteShift = PasteShift_checkBox.Checked;
			_settings.PasteKey = (Keys)Enum.Parse(typeof(Keys), PasteKeys_comboBox.SelectedItem.ToString(), false);
			_settings.PreventProcessingPasteKey = PreventProcessingPasteKey_checkBox.Checked;
			_settings.CaptureCtrl = CaptureCtrl_checkBox.Checked;
			_settings.CaptureAlt = CaptureAlt_checkBox.Checked;
			_settings.CaptureShift = CaptureShift_checkBox.Checked;
			_settings.CaptureKey = (Keys)Enum.Parse(typeof(Keys), CaptureKeys_comboBox.SelectedItem.ToString(), false);
			_settings.PreventProcessingCaptureKey = PreventProcessingCaptureKey_checkBox.Checked;
			_settings.MultiScreen = MultiScreen_checkBox.Checked;
			_settings.ComposeVertically = ComposeVertically_radioButton.Checked;
			_settings.Stack = Stack_checkBox.Checked;
			_settings.StackSize = (int)StackSize_numericUpDown.Value;
			_settings.ReduceColors = ReduceColors_comboBox.SelectedIndex;
			_settings.ImageSize = (int)ImageSize_numericUpDown.Value;
			_settings.Save();
		}

		private void Form1_SizeChanged(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized)
			{
				notifyIcon.Visible = true;
				this.ShowInTaskbar = false;
			}
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			//Save setting
			_settings.Save();
			RegistryHelper.SetAutorun(StartWithWindows_checkBox.Checked);
		}

		private void ShowFromTray()
		{
			this.WindowState = FormWindowState.Normal;
			notifyIcon.Visible = false;
			this.ShowInTaskbar = true;
		}

		private void Disable_ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_settings.Enabled = !_settings.Enabled;
		}

		private void ComposeVertically_radioButton_CheckedChanged(object sender, EventArgs e)
		{
			label2.Text = ComposeVertically_radioButton.Checked ? "in column" : "in row";
		}

		private void Show_ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowFromTray();
		}

		private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				_clickTimer.Change(Timeout.Infinite, Timeout.Infinite);
				Thread.Sleep(100);
				ShowFromTray();
				notifyIcon.MouseClick += NotifyIcon_MouseClick;
			}
		}

		private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				notifyIcon.MouseClick -= NotifyIcon_MouseClick;
				_clickTimer = new System.Threading.Timer(ClickTimer, null, SystemInformation.DoubleClickTime + 1, 0);
			}
		}

		private void ClickTimer(object state)
		{
			notifyIcon.MouseClick += NotifyIcon_MouseClick;
			_clickTimer.Change(Timeout.Infinite, Timeout.Infinite);
			if (_settings.CaptureOnTrayClick)
			{
				//Run in main thread
				this.InvokeIfNeed(() =>
				{
					CaptureRoutine();
				});
			}
		}
	}
}
