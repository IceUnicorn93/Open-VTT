﻿using OpenVTT.Common;
using OpenVTT.Logging;
using OpenVTT.Settings;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace OpenVTT.Controls
{
    public partial class ScreenSelector : UserControl
    {
        public ScreenSelector()
        {
            Logger.Log("Class: ScreenSelector | Constructor");

            InitializeComponent();

            //Fill combo box
            var enumValues = Enum.GetNames(typeof(DisplayType));
            cbType.Items.AddRange(enumValues);
            cbType.Text = cbType.Items[0].ToString();

            // Get Offsets
            var minX = (Screen.AllScreens.Min(n => n.Bounds.X) / 10) * -1;
            var minY = (Screen.AllScreens.Min(n => n.Bounds.Y) / 10) * -1;

            foreach (var screen in Screen.AllScreens)
            {
                var screenHeight = screen.Bounds.Height / 10;
                var screenWidth = screen.Bounds.Width / 10;

                var screenLocationX = (screen.Bounds.X / 10) + minX;
                var screenLocationY = (screen.Bounds.Y / 10) + minY;

                var btn = new Button
                {
                    Height = screenHeight,
                    Width = screenWidth,
                    Location = new Point(screenLocationX, screenLocationY),
                    Text =
                    $"Primary Screen: {screen.Primary}{Environment.NewLine}" +
                    $"{screen.Bounds.Width} * {screen.Bounds.Height}"
                };

                //btn.MouseEnter += (object sender, EventArgs e) =>
                //{
                //    MessageBox.Show(
                //        $"X: {screen.Bounds.X}{Environment.NewLine}" +
                //        $"Y: {screen.Bounds.Y}{Environment.NewLine}" +
                //        $"W: {screen.Bounds.Width}{Environment.NewLine}" +
                //        $"H: {screen.Bounds.Height}{Environment.NewLine}" +
                //        $"L: {screen.Bounds.Left}{Environment.NewLine}" +
                //        $"R: {screen.Bounds.Right}{Environment.NewLine}" +
                //        $"T: {screen.Bounds.Top}{Environment.NewLine}" +
                //        $"B: {screen.Bounds.Bottom}{Environment.NewLine}" +
                //        $"LX: {screen.Bounds.Location.X}{Environment.NewLine}" +
                //        $"LY: {screen.Bounds.Location.Y}{Environment.NewLine}" +
                //        $"SW: {screen.Bounds.Size.Width}{Environment.NewLine}" +
                //        $"SH: {screen.Bounds.Size.Height}{Environment.NewLine}");
                //};

                var si = new ScreenInformation()
                {
                    PositionX = screen.Bounds.X,
                    PositionY = screen.Bounds.Y,
                    Height = screen.Bounds.Height,
                    Width = screen.Bounds.Width,
                };

                btn.Tag = si;

                btn.Click += (object sender, EventArgs e) =>
                {
                    Logger.Log("Class: ScreenSelector | Select Screen Button");

                    try
                    {
                        var clickedButton = (Button)sender;
                        var screenInfo = (ScreenInformation)clickedButton.Tag;

                        var selectedType = (DisplayType)Enum.Parse(typeof(DisplayType), cbType.Text);
                        screenInfo.Display = selectedType;

                        //Create Greenscreen for Working with animated maps
                        if(selectedType == DisplayType.Player)
                        {
                            var bmp = new Bitmap(screenInfo.Width, screenInfo.Height);
                            using(var g = Graphics.FromImage(bmp))
                            {
                                g.FillRectangle(new SolidBrush(Color.FromArgb(0, 255, 66)), new Rectangle(0, 0, bmp.Width, bmp.Height));
                            }
                            var path = Path.Combine(Application.StartupPath, "Greenscreen.png");
                            bmp.Save(path);
                        }

                        var s = Settings.Settings.Values.Screens.SingleOrDefault(n => n.Display == selectedType);
                        if (s == null)
                            Settings.Settings.Values.Screens.Add(screenInfo);
                        else
                        {
                            Settings.Settings.Values.Screens.RemoveAll(n => n.Display == selectedType);
                            Settings.Settings.Values.Screens.Add(screenInfo);
                        }

                        Settings.Settings.Save();

                        // Creating Sample Form to Show Player Location
                        var frm = new Form
                        {
                            StartPosition = FormStartPosition.Manual,
                            Height = 100,
                            Width = 100,
                            BackColor = Color.Navy,
                            Location = new Point(
                                screen.Bounds.X + (screen.Bounds.Width / 2 - 50),
                                screen.Bounds.Y + (screen.Bounds.Height / 2 - 50)),
                            FormBorderStyle = FormBorderStyle.None
                        };
                        // Create Close-Button
                        var b = new Button
                        {
                            Text = $"Okay{Environment.NewLine}Close",
                            BackColor = Color.FromKnownColor(KnownColor.Control),
                        };
                        b.Height *= 2;
                        b.Location = new Point(frm.Width / 2 - b.Width / 2, frm.Height / 2 - b.Height / 2);
                        b.Click += (object o, EventArgs ea) =>
                        {
                            Logger.Log("Class: ScreenSelector | Select Screen Button Pressed");

                            frm.Close();
                        };
                        //Add Close Button and Show Form
                        frm.Controls.Add(b);
                        frm.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                };

                this.Controls.Add(btn);
            }
        }


        // Lets hope to never use this! It's for ... CUSTOM SCALING ... No fun. Please don't use it
        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117,

            // http://pinvoke.net/default.aspx/gdi32/GetDeviceCaps.html
        }


        private float getScalingFactor()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int LogicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
            int PhysicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);

            float ScreenScalingFactor = (float)PhysicalScreenHeight / (float)LogicalScreenHeight;

            return ScreenScalingFactor; // 1.25 = 125%
        }
    }
}
