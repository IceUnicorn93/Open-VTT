using OpenVTT.Common;
using OpenVTT.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenVTT.Controls.Displayer
{
    public partial class InformationDisplayDM : Form
    {
        internal InformationDisplayDM()
        {
            Logger.Log("Class: InformationDisplayDM | Constructor");

            InitializeComponent();

            var screen = Settings.Settings.Values.Screens.SingleOrDefault(n => n.Display == DisplayType.InformationDisplayDM);
            if (screen == null) return;
            this.Location = new Point(screen.PositionX, screen.PositionY);
            this.Size = new Size(screen.Width, screen.Height);

            displayImagePictureBox.DrawMode = PictureBoxMode.Ping;
        }

        internal DrawingPictureBox GetPictureBox()
        {
            Logger.Log("Class: InformationDisplayDM | GetPictureBox");

            return displayImagePictureBox;
        }
    }
}
