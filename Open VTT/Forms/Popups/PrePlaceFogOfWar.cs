using OpenVTT.Logging;
using System;
using System.Windows.Forms;

namespace Open_VTT.Forms.Popups
{
    public partial class PrePlaceFogOfWar : Form
    {
        public PrePlaceFogOfWar()
        {
            Logger.Log("Class: PrePlaceFogOfWar | Constructor");

            InitializeComponent();
        }

        internal string FogName;

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            Logger.Log("Class: PrePlaceFogOfWar | tbName_TextChanged");

            btnPreview.Text = tbName.Text;
            FogName = tbName.Text;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: PrePlaceFogOfWar | btnSave_Click");

            FogName = tbName.Text;
            this.Close();
        }

        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            Logger.Log("Class: PrePlaceFogOfWar | tbName_KeyDown");

            if (e.KeyCode == Keys.Enter)
                btnSave_Click(null, null);
        }
    }
}
