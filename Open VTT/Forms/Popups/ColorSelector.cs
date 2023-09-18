using OpenVTT.Logging;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Open_VTT.Forms.Popups
{
    public partial class ColorSelector : Form
    {
        public ColorSelector(Color clr)
        {
            Logger.Log("Class: ColorSelector | Constructor");

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
            Logger.Log("Class: ColorSelector | nudValueChange");

            SelectedColor = Color.FromArgb((int)nudAlpha.Value, (int)nudRed.Value, (int)nudGreen.Value, (int)nudBlue.Value);
            panel1.BackColor = SelectedColor;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: ColorSelector | btnSave_Click");

            SelectedColor = Color.FromArgb((int)nudAlpha.Value, (int)nudRed.Value, (int)nudGreen.Value, (int)nudBlue.Value);
            this.Close();
        }
    }
}
