using OpenVTT.Common;
using OpenVTT.Logging;
using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace OpenVTT.AnimatedMap
{
    public partial class AnimatedMapDisplayer : Form
    {
        public bool IsDisplayingImage = true;
        public string DisplayItemURL
        {
            get => _DisplayItemURL;
            set
            {
                _DisplayItemURL = value;
                axWindowsMediaPlayer1.URL = value;
            }
        }
        private string _DisplayItemURL;

        private Form overlay = new Form();
        private Color TransparentKey = Color.FromArgb(0, 255, 66);

        public void SetFogOfWarImage(Image img)
        {
            overlay.BackgroundImage.Dispose();
            overlay.BackgroundImage = null;
            overlay.BackgroundImage = img;

            GC.Collect();
        }

        private void prepareOverlay()
        {
            overlay.TopMost = true;
            overlay.ShowInTaskbar = false;
            overlay.MinimizeBox = false;
            overlay.MaximizeBox = false;
            overlay.ShowIcon = false;
            overlay.ControlBox = false;
            overlay.Text = "";
            overlay.FormBorderStyle = FormBorderStyle.None;
            overlay.Size = Size.Empty;

            overlay.Show();

            overlay.Location = axWindowsMediaPlayer1.PointToScreen(new Point(axWindowsMediaPlayer1.Location.X, axWindowsMediaPlayer1.Location.Y));
            overlay.Size = axWindowsMediaPlayer1.Size;

            //var bmp = new Bitmap(overlay.Width, overlay.Height);
            //using (var g = Graphics.FromImage(bmp))
            //{
            //    //g.FillRectangle(new SolidBrush(Color.FromArgb(0, 255, 66)), new Rectangle(0, 0, overlay.Width, overlay.Height));
            //    g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, overlay.Width, overlay.Height));
            //}

            //overlay.BackgroundImage = bmp;

            overlay.TransparencyKey = TransparentKey;
        }

        public AnimatedMapDisplayer()
        {
            InitializeComponent();

            //Keep the Fog of War Overlay always at the Location and Size of the Windows Media Player
            Move += FitOverlay;
            Resize += FitOverlay;
        }

        private void FitOverlay(object sender, EventArgs e)
        {
            overlay.Size = axWindowsMediaPlayer1.Size;
            overlay.Location = axWindowsMediaPlayer1.PointToScreen(new Point(axWindowsMediaPlayer1.Location.X, axWindowsMediaPlayer1.Location.Y));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.Ctlcontrols.currentItem == null) return;

            if (IsDisplayingImage)
            {
                axWindowsMediaPlayer1.Ctlcontrols.pause();
                return;
            }

            if (axWindowsMediaPlayer1.Ctlcontrols.currentPosition > axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration - 0.02)
            {
                axWindowsMediaPlayer1.Ctlcontrols.currentPosition = 0.0;
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            else
            {
                Thread.Sleep(10);

                GC.Collect();

                //axWindowsMediaPlayer1.settings.mute = true; // only for DM Side, player Side should play the sound!
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }

        private void AnimatedMapDisplayer_Load(object sender, EventArgs e)
        {

            Logger.Log("Class: AnimatedMapDisplayer | Constructor");

            //Player Setup
            var screen = Settings.Settings.Values.Screens.SingleOrDefault(n => n.Display == DisplayType.Player);
            if (screen == null) return;
            this.Location = new Point(screen.PositionX, screen.PositionY);

            this.WindowState = FormWindowState.Maximized;

            // UI Setup

            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.enableContextMenu = false;

            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.None;

            prepareOverlay();
        }
    }
}
