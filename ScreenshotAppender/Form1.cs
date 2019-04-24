using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
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
		private ClipboardEx _clipboardEx;
		private bool _lastCtrlV;
		private bool _lastShiftInsert;
		private int _currentRow;
		private int _currentColumn;

		private double _imagesCounter = 0;
		public double ClipboardImagesCounter
		{
			get
			{
				return _imagesCounter;
			}
			set
			{
				_imagesCounter = value;
				this.InvokeIfNeed(() =>
				{
					if (_settings.Enabled)
					{
						notifyIcon.Text = $"Screenshot Appender\r\nScreens in clipboard: {_imagesCounter:F0}";
					}
					else
					{
						notifyIcon.Text = $"Screenshot Appender\r\nDisabled";
					}
				});
			}
		}


		public Form1()
		{
			InitializeComponent();
			//Load\create settings
			_settings = Settings.Load();
			_settings_EnabledChanged(null, _settings.Enabled);
			_clipboardEx = new ClipboardEx(_settings.UseInternalClipboard && _settings.PasteKey != Keys.None);
			_clipboardEx.OnClear += _clipboardEx_OnClear;
			_clipboardEx.OnSetImage += _clipboardEx_OnSetImage;
			//Set icon initial state
			notifyIcon.Icon = _settings.Enabled ? notifyIcon.Icon = SA.std_icon : notifyIcon.Icon = SA.red_icon;
			//Handle settings changed
			_settings.EnabledChanged += _settings_EnabledChanged;
			//Setup global hook
			Hooker hooker = new AppHooker();
			hooker = new GlobalHooker();
			_keyboardHookManager = new KeyboardHookListener(hooker)
			{
				Enabled = true
			};
			//Handle keyboard up and down events
			_keyboardHookManager.KeyDown += _keyboardHookManager_KeyDown;
			_keyboardHookManager.KeyUp += _keyboardHookManager_KeyUp;
		}

		private void _clipboardEx_OnSetImage(object sender, EventArgs e)
		{
			ClearClipboard_button.Enabled = true;
			SaveClipboard_button.Enabled = true;
		}

		private void _clipboardEx_OnClear(object sender, EventArgs e)
		{
			_currentRow = _currentColumn = 0;
			ClipboardImagesCounter = 0;
			ClearClipboard_button.Enabled = false;
			SaveClipboard_button.Enabled = false;
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
			using (Bitmap currentScreen = _settings.MultiScreen ? ScreenCapturer.CaptureToBitmapMulti() : ScreenCapturer.CaptureToBitmapSingle())
			{
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
				if (_clipboardEx.ContainsImage())
				{
					Image currentImage = _clipboardEx.GetImage();
					if (_settings.Stack)
					{
						//Composing images in stack
						Rectangle currentBounds = new Rectangle(0, 0, currentImage.Width, currentImage.Height);
						if (_settings.ComposeVertically)
						{
							_currentRow += 1;
							if (_currentRow == _settings.StackSize)
							{
								_currentRow = 0;
								_currentColumn += 1;
							}
						}
						else
						{
							_currentColumn += 1;
							if (_currentColumn == _settings.StackSize)
							{
								_currentRow += 1;
								_currentColumn = 0;
							}
						}
						Point imagePlacement = new Point(_currentColumn * screenSize.Width, _currentRow * screenSize.Height);
						currentBounds = Rectangle.Union(currentBounds, new Rectangle(imagePlacement.X, imagePlacement.Y, screenSize.Width, screenSize.Height));
						using (Image newImage = new Bitmap(currentBounds.Width, currentBounds.Height))
						{
							using (Graphics newGraphics = Graphics.FromImage(newImage))
							{
								newGraphics.DrawImage(currentImage, 0, 0);
								newGraphics.DrawImage(workingImage, imagePlacement);
								_clipboardEx.SetImage(newImage);
								ClipboardImagesCounter++;
							}
						}
					}
					else
					{
						//Compare width of images in clipboard and from screen
						if (_settings.ComposeVertically ? currentImage.Width == screenSize.Width : currentImage.Height == screenSize.Height)
						{
							//If the same - add new image to existing
							using (Image newImage = _settings.ComposeVertically ? new Bitmap(currentImage.Width, currentImage.Height + screenSize.Height) : new Bitmap(currentImage.Width + screenSize.Width, currentImage.Height))
							{
								using (Graphics newGraphics = Graphics.FromImage(newImage))
								{
									newGraphics.DrawImage(currentImage, new Point(0, 0));
									if (_settings.ComposeVertically)
									{
										newGraphics.DrawImage(workingImage, new Point(0, currentImage.Height));
									}
									else
									{
										newGraphics.DrawImage(workingImage, new Point(currentImage.Width, 0));
									}
									_clipboardEx.SetImage(newImage);
								}
								ClipboardImagesCounter++;
							}
						}
						else
						{
							//If screen image and clipboard image have different width, set clipboard to new image
							_clipboardEx.SetImage(workingImage);
							ClipboardImagesCounter = 1;
						}
					}
				}
				else
				{
					_clipboardEx.SetImage(workingImage);
					ClipboardImagesCounter = 1;
					_currentRow = _currentColumn = 0;
				}
			}
			//Blink icon in tray. Just for fun :)
			BlinkTrayIcon();
		}

		private Image ReducePNG(Bitmap image, System.Drawing.Imaging.PixelFormat newFormat)
		{
			return image.Clone(new Rectangle(0, 0, image.Width, image.Height), newFormat);
		}

		private void _keyboardHookManager_KeyUp(object sender, KeyEventArgs e)
		{
			if (_lastCtrlV || _lastShiftInsert)
			{
				if (_clipboardEx.ContainsImage())
				{
					_clipboardEx.Clear();
				}
			}
		}

		private void _keyboardHookManager_KeyDown(object sender, KeyEventArgs e)
		{
			if (!_settings.Enabled)
			{
				//Do nothing if disabled
				e.Handled = false;
				return;
			}
			if (_settings.PasteKey != Keys.None)
			{
				//Process custom paste key
				if (e.KeyCode == _settings.PasteKey)
				{
					if (_settings.PasteAlt && !e.Alt) { return; }
					if (_settings.PasteCtrl && !e.Control) { return; }
					if (_settings.PasteShift && !e.Shift) { return; }
					e.Handled = true;
					if (_settings.UseInternalClipboard)
					{
						//Place to clipboard data from "internal" clipboard
						if (_clipboardEx.GetImage() != null)
						{
							//Preserve old data
							var oldData = ClipboardService.BackupClipboard();
							Clipboard.SetImage(_clipboardEx.GetImage());
							Thread.Sleep(100);
							SendKeys.SendWait("^{v}");
							Thread.Sleep(100);
							_clipboardEx.Clear();
							//Restore old data
							ClipboardService.RestoreClipboard(oldData);
						}
					}
					else
					{
						//Send CTRL+V to active application
						SendKeys.SendWait("^{v}");
						Thread.Sleep(100);
					}
					if (_settings.ClearOnPasteCustom)
					{
						_clipboardEx.Clear();
					}
				}
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
				CaptureRoutine();
			}
			if (e.KeyCode == Keys.PrintScreen && _settings.CaptureKey != Keys.PrintScreen)
			{
				//Set to one image if we use default behavior of Print Screen key
				ClipboardImagesCounter = 1;
			}
			_lastCtrlV = _settings.ClearOnPasteCtrlV && (e.KeyCode == Keys.V && !e.Alt && !e.Shift & e.Control);
			_lastShiftInsert = _settings.ClearOnPasteShiftInsert && (e.KeyCode == Keys.Insert && !e.Alt && e.Shift & !e.Control);
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
			_clipboardEx.Clear();
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
			if (_clipboardEx.ContainsImage() && !_settings.Stack)
			{
				Size screenSize = Screen.PrimaryScreen.Bounds.Size;
				if (_settings.MultiScreen)
				{
					//Process all virtual desktop if allow multi screen
					screenSize = new Size(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
				}
				Image currentImage = _clipboardEx.GetImage();
				if (_settings.ComposeVertically ? currentImage.Width == screenSize.Width : currentImage.Height == screenSize.Height)
				{
					ClipboardImagesCounter = _settings.ComposeVertically ? currentImage.Height / screenSize.Height : currentImage.Width / screenSize.Width;
				}
				else
				{
					ClipboardImagesCounter = 0;
				}
			}
			else
			{
				ClipboardImagesCounter = 0;
			}
			ClearClipboard_button.Enabled = Clipboard.ContainsImage();
			SaveClipboard_button.Enabled = Clipboard.ContainsImage();
		}

		private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			SaveClipboardAs_toolStripMenuItem.Enabled = _clipboardEx.ContainsImage();
			ClearClipboard_ToolStripMenuItem.Enabled = _clipboardEx.ContainsImage();
		}

		private void SaveClipboardAs_toolStripMenuItem_Click(object sender, EventArgs e)
		{
			//Save content to file
			if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				if (_clipboardEx.ContainsImage())
				{
					Image currentImage = _clipboardEx.GetImage();
					try
					{
						currentImage.Save(saveFileDialog.FileName, ImageFormat.Png);
					}
					catch (Exception ex)
					{
						MessageBox.Show(this, $"Unable to save file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
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
			ClearWhenExit_checkBox.Checked = _settings.ClearOnExit;
			CaptureOnTrayClick_checkBox.Checked = _settings.CaptureOnTrayClick;
			ClearWhenPasteCtrlV_checkBox.Checked = _settings.ClearOnPasteCtrlV;
			ClearWhenPasteShiftIns_checkBox.Checked = _settings.ClearOnPasteShiftInsert;
			ClearWhenPasteCustom_checkBox.Checked = _settings.ClearOnPasteCustom;
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
			InternalClipboard_checkBox.Checked = _settings.UseInternalClipboard && _settings.PasteKey != Keys.None;
			Stack_checkBox.Checked = _settings.Stack;
			StackSize_numericUpDown.Value = _settings.StackSize;
			ReduceColors_comboBox.SelectedIndex = _settings.ReduceColors;
			//Set event handler for change settings. We not set in designer for prevent multiple settings write on start application
			Enabled_checkBox.CheckedChanged += SettingsChanged;
			ClearWhenPasteCtrlV_checkBox.CheckedChanged += SettingsChanged;
			ClearWhenPasteShiftIns_checkBox.CheckedChanged += SettingsChanged;
			ClearWhenExit_checkBox.CheckedChanged += SettingsChanged;
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
			InternalClipboard_checkBox.CheckedChanged += SettingsChanged;
			Stack_checkBox.CheckedChanged += SettingsChanged;
			StackSize_numericUpDown.ValueChanged += SettingsChanged;
			ReduceColors_comboBox.SelectedIndexChanged += SettingsChanged;
		}

		private void SettingsChanged(object sender, EventArgs e)
		{
			//Respond to change settings and save settings
			_settings.Enabled = Enabled_checkBox.Checked;
			_settings.ClearOnExit = ClearWhenExit_checkBox.Checked;
			_settings.CaptureOnTrayClick = CaptureOnTrayClick_checkBox.Checked;
			_settings.ClearOnPasteShiftInsert = ClearWhenPasteShiftIns_checkBox.Checked;
			_settings.ClearOnPasteCtrlV = ClearWhenPasteCtrlV_checkBox.Checked;
			_settings.ClearOnPasteCustom = ClearWhenPasteCustom_checkBox.Checked;
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
			if (_settings.UseInternalClipboard != InternalClipboard_checkBox.Checked)
			{
				_clipboardEx.Clear();
			}
			_settings.UseInternalClipboard = InternalClipboard_checkBox.Checked;
			_settings.Stack = Stack_checkBox.Checked;
			_settings.StackSize = (int)StackSize_numericUpDown.Value;
			_settings.ReduceColors = ReduceColors_comboBox.SelectedIndex;
			_clipboardEx = new ClipboardEx(_settings.UseInternalClipboard && _settings.PasteKey != Keys.None);
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
			if (_settings.ClearOnExit)
			{
				//Clean only if image in clipboard with width\height of screen
				Size screenSize = Screen.PrimaryScreen.Bounds.Size;
				if (_settings.MultiScreen)
				{
					//Process all virtual desktop if allow multi screen
					screenSize = new Size(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
				}
				if (_clipboardEx.ContainsImage())
				{
					using (Image currentImage = _clipboardEx.GetImage())
					{
						if (_settings.ComposeVertically ? currentImage.Width == screenSize.Width : currentImage.Height == screenSize.Height)
						{
							_clipboardEx.Clear();
						}
					}
				}
			}
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

		private void PasteKeys_comboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (PasteKeys_comboBox.SelectedIndex == 0)
			{
				InternalClipboard_checkBox.Checked = false;
				InternalClipboard_checkBox.Enabled = false;
			}
			else
			{
				InternalClipboard_checkBox.Enabled = true;
			}
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
