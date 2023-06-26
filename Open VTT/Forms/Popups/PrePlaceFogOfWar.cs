using System;
using System.Windows.Forms;

namespace Open_VTT.Forms.Popups
{
    public partial class PrePlaceFogOfWar : Form
    {
        public PrePlaceFogOfWar()
        {
            InitializeComponent();
        }

        internal string FogName;

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            btnPreview.Text = tbName.Text;
            FogName = tbName.Text;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FogName = tbName.Text;
            this.Close();
        }

        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave_Click(null, null);
        }
    }
}
