using OpenVTT.Scripting;
using System.Windows.Forms;

namespace OpenVTT.UiDesigner
{
    public partial class ScriptDescription : UserControl
    {
        public ScriptDescription()
        {
            InitializeComponent();
        }

        public ScriptDescription(ScriptConfig config = null)
        {
            InitializeComponent();

            if (config != null) return;

            lblAuthor.Text = config.Author;
            lblNameAndVersion.Text = $"{config.Name} | {config.Version}";
            tbDescription.Text = config.Description;
        }
    }
}
