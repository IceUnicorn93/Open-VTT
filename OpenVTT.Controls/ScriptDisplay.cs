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

            Tag = host;

            SetState();
        }

        private void SetState()
        {
            var host = (ScriptHost)Tag;
            if (host.hasSuccessfullyRun)
            {
                pnlState.BackColor = Color.Green;
                btnRerun.Visible = false;
            }
            else
            {
                pnlState.BackColor = Color.Red;
                btnRerun.Visible = true;
            }
        }

        private void pnlState_Click(object sender, System.EventArgs e)
        {
            var host = (ScriptHost)Tag;
            MessageBox.Show(host.exception?.ToString());
        }

        private void btnRerun_Click(object sender, System.EventArgs e)
        {
            var host = (ScriptHost)Tag;
            Tag = ScriptEngine.RunScript(host.path);
            ScriptEngine.HostsCalculated();

            SetState();
        }
    }
}
