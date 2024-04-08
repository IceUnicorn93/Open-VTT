using OpenVTT.UiDesigner.UserControls;
using System;
using System.IO;
using System.Windows.Forms;

namespace OpenVTT.UiDesigner.Forms
{
    public partial class PopupNewStructure : Form
    {
        public Designer designer;

        public PopupNewStructure()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            designer.NoteName = tbName.Text;
            designer.LoadPath = Path.Combine(Application.StartupPath, "Notes", "Configure");
            designer.IsNotesDesigner = true;
            //designer.
        }
    }
}
