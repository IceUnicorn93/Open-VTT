using Open_VTT.Classes;
using Open_VTT.Controls;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Open_VTT.Forms.Popups.Displayer
{
    public partial class MapPlayer : Form
    {
        internal MapPlayer()
        {
            InitializeComponent();

            var screen = Settings.Values.Screens.SingleOrDefault(n => n.Display == Other.DisplayType.Player);
            if (screen == null) return;
            this.Location = new Point(screen.PositionX, screen.PositionY);
            //this.Size = new Size(screen.Width, screen.Height);
        }

        internal DrawingPictureBox GetPictureBox()
        {
            return displayImagePictureBox;
        }
    }
}
