using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenVTT.Forms
{
    public partial class NewScene : Form
    {
        public string SceneName;

        public bool Create = false;

        public NewScene()
        {
            InitializeComponent();
        }

        private void btnOkay_Click(object sender, System.EventArgs e)
        {
            SceneName = txtSceneName.Text;
            Create = true;
            Close();
        }

        private void btnCancle_Click(object sender, System.EventArgs e)
        {
            Create = false;
            Close();
        }

        private void txtSceneName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOkay_Click(null, null);
            else if (e.KeyCode == Keys.Escape)
                btnCancle_Click(null, null);
        }
    }
}
