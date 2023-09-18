using OpenVTT.Common;
using OpenVTT.Logging;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OpenVTT.Controls.Displayer
{
    public partial class InformationDisplayPlayer : Form
    {
        public InformationDisplayPlayer()
        {
            Logger.Log("Class: InformationDisplayPlayer | Constructor");

            InitializeComponent();

            var screen = Settings.Settings.Values.Screens.SingleOrDefault(n => n.Display == DisplayType.InformationDisplayPlayer);
            if (screen == null) return;
            this.Location = new Point(screen.PositionX, screen.PositionY);
            this.Size = new Size(screen.Width, screen.Height);

            displayImagePictureBox.DrawMode = PictureBoxMode.Ping;
        }

        internal DrawingPictureBox GetPictureBox()
        {
            Logger.Log("Class: InformationDisplayPlayer | GetPictureBox");

            return displayImagePictureBox;
        }
    }
}
