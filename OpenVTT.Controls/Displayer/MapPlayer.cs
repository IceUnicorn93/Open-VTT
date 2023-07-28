using OpenVTT.Common;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OpenVTT.Controls.Displayer
{
    public partial class MapPlayer : Form
    {
        public MapPlayer()
        {
            InitializeComponent();

            var screen = Settings.Settings.Values.Screens.SingleOrDefault(n => n.Display == DisplayType.Player);
            if (screen == null) return;
            this.Location = new Point(screen.PositionX, screen.PositionY);
            //this.Size = new Size(screen.Width, screen.Height);

            this.WindowState = FormWindowState.Maximized;
        }

        internal DrawingPictureBox GetPictureBox()
        {
            return displayImagePictureBox;
        }
    }
}
