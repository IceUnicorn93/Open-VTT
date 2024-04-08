using AxWMPLib;
using OpenVTT.Common;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WMPLib;

namespace OpenVTT.AnimatedMap
{
    public partial class AnimatedMapScreenshotter : Form
    {
        public Action FilePlaying;

        public AnimatedMapScreenshotter(bool hideControls = true)
        {
            InitializeComponent();

            var primary = Screen.PrimaryScreen;

            var playerScreen = Settings.Settings.Values.Screens.SingleOrDefault(n => n.Display == DisplayType.Player);
            if(playerScreen == null)
            {
                playerScreen = new Settings.ScreenInformation()
                {
                    Display = DisplayType.Player,
                    Height = primary.Bounds.Height,
                    Width = primary.Bounds.Width,
                    PositionX = primary.Bounds.X,
                    PositionY = primary.Bounds.Y,
                };
            }

            this.StartPosition = FormStartPosition.Manual;
            this.Size = new Size(playerScreen.Width / 2, playerScreen.Height / 2);
            this.Location = new Point(primary.Bounds.Width / 2 - this.Width / 2, primary.Bounds.Height / 2 - this.Height / 2);

            if (hideControls)
            {
                axWindowsMediaPlayer1.uiMode = "none";
                axWindowsMediaPlayer1.enableContextMenu = false; 
            }
            else
            {
                FormBorderStyle = FormBorderStyle.FixedSingle;
            }

            axWindowsMediaPlayer1.PlayStateChange += PlayStateChange;
        }

        private void PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
            if ((WMPPlayState)e.newState == WMPPlayState.wmppsReady)
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
                axWindowsMediaPlayer1.settings.mute = true;
            }
            else if((WMPPlayState)e.newState == WMPPlayState.wmppsPlaying)
            {
                FilePlaying?.Invoke();
            }
        }

        public void SetPath(string URL)
        {
            axWindowsMediaPlayer1.URL = URL;
        }
    }
}
