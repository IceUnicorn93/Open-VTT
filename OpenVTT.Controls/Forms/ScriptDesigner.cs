using System.Windows.Forms;

namespace OpenVTT.Controls.Forms
{
    public partial class ScriptDesigner : Form
    {
        public string LoadPath
        {
            get => designer1.LoadPath;
            set => designer1.LoadPath = value;
        }

        public bool IsNotesDesigner
        {
            get => designer1.IsNotesDesigner;
            set => designer1.IsNotesDesigner = value;
        }

        public ScriptDesigner()
        {
            InitializeComponent();
        }
    }
}
