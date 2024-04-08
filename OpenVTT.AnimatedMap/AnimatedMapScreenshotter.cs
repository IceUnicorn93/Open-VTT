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

        public AnimatedMapScreenshotter()
        {
            InitializeComponent();

            var playerScreen = Settings.Settings.Values.Screens.SingleOrDefault(n => n.Display == DisplayType.Player);
            var primary = Screen.PrimaryScreen;

            this.StartPosition = FormStartPosition.Manual;
            this.Size = new Size(playerScreen.Width / 2, playerScreen.Height / 2);
            this.Location = new Point(primary.Bounds.Width / 2 - this.Width / 2, primary.Bounds.Height / 2 - this.Height / 2);

            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.enableContextMenu = false;

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
                FilePlaying();
            }
        }

        public void SetPath(string URL)
        {
            axWindowsMediaPlayer1.URL = URL;
        }
    }
}
