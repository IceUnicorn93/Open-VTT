using System;
using System.Drawing;
using System.Windows.Forms;

namespace Open_VTT.Forms.Popups
{
    public partial class ColorSelector : Form
    {
        public ColorSelector(Color clr)
        {
            InitializeComponent();

            SelectedColor = clr;
            panel1.BackColor = clr;

            nudAlpha.Value = clr.A;
            nudBlue.Value = clr.B;
            nudGreen.Value = clr.G;
            nudRed.Value = clr.R;
        }

        public Color SelectedColor;

        private void nudValueChange(object sender, EventArgs e)
        {
            SelectedColor = Color.FromArgb((int)nudAlpha.Value, (int)nudRed.Value, (int)nudGreen.Value, (int)nudBlue.Value);
            panel1.BackColor = SelectedColor;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SelectedColor = Color.FromArgb((int)nudAlpha.Value, (int)nudRed.Value, (int)nudGreen.Value, (int)nudBlue.Value);
            this.Close();
        }
    }
}
