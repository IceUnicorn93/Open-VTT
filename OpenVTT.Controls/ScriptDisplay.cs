using OpenVTT.Scripting;
using System.Drawing;
using System.Windows.Forms;

namespace OpenVTT.Controls
{
    public partial class ScriptDisplay : UserControl
    {
        public ScriptDisplay(ScriptHost host)
        {
            InitializeComponent();
            lblName.Text = host.Config.Name;

            if (host.hasSuccessfullyRun)
                pnlState.BackColor = Color.Green;
            else
                pnlState.BackColor= Color.Red;

            Tag = host;
        }

        private void pnlState_Click(object sender, System.EventArgs e)
        {
            var host = (ScriptHost)Tag;
            MessageBox.Show(host.exception?.ToString());
        }
    }
}
