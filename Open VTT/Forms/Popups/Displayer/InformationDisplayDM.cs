using Open_VTT.Classes;
using Open_VTT.Controls;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Open_VTT.Forms.Popups.Displayer
{
    public partial class InformationDisplayDM : Form
    {
        // Singleton instance
        private static InformationDisplayDM currentPlayerWindow = null;

        private InformationDisplayDM()
        {
            InitializeComponent();

            var screen = Settings.Values.Screens.SingleOrDefault(n => n.Display == Other.DisplayType.InformationDisplayDM);
            if (screen == null) return;
            this.Location = new Point(screen.PositionX, screen.PositionY);
            this.Size = new Size(screen.Width, screen.Height);

            displayImagePictureBox.DrawMode = Other.PictureBoxMode.Ping;
        }

        public static InformationDisplayDM GetOrCreate()
        {
            if (currentPlayerWindow == null || currentPlayerWindow.IsDisposed)
                currentPlayerWindow = new InformationDisplayDM();

            return currentPlayerWindow;
        }

        internal DrawingPictureBox GetPictureBox()
        {
            return displayImagePictureBox;
        }
    }
}
