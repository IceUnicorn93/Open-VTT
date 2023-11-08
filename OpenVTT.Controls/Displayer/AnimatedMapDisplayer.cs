using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpenVTT.Controls.Displayer
{
    public partial class AnimatedMapDisplayer : Form
    {
        private Form frmFogOfWar = new Form();

        public AnimatedMapDisplayer()
        {
            InitializeComponent();

            frmFogOfWar.BackColor = Color.FromArgb(255, 0, 164, 0);
            frmFogOfWar.TransparencyKey = Color.FromArgb(255, 0, 164, 0);
        }

        private void AnimatedMapDisplayer_LocationChanged(object sender, EventArgs e)
        {
            frmFogOfWar.Location = this.Location;
        }

        private void AnimatedMapDisplayer_SizeChanged(object sender, EventArgs e)
        {
            frmFogOfWar.Size = this.Size;
        }
    }
}
