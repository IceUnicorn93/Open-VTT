using OpenVTT.Logging;
using System.Windows.Forms;

namespace OpenVTT.Forms
{
    public partial class NewScene : Form
    {
        public string SceneName;

        public bool Create = false;

        public NewScene()
        {
            Logger.Log("Class: NewScene | Constructor");

            InitializeComponent();
        }

        private void btnOkay_Click(object sender, System.EventArgs e)
        {
            Logger.Log("Class: NewScene | btnOkay_Click");

            SceneName = txtSceneName.Text;
            Create = true;
            Close();
        }

        private void btnCancle_Click(object sender, System.EventArgs e)
        {
            Logger.Log("Class: NewScene | btnCancle_Click");

            Create = false;
            Close();
        }

        private void txtSceneName_KeyDown(object sender, KeyEventArgs e)
        {
            Logger.Log("Class: NewScene | txtSceneName_KeyDown");

            if (e.KeyCode == Keys.Enter)
                btnOkay_Click(null, null);
            else if (e.KeyCode == Keys.Escape)
                btnCancle_Click(null, null);
        }
    }
}
