using System;
using System.Windows.Forms;

namespace OpenVTT.Editor
{
    public partial class AddNode : Form
    {
        public string NodeName;
        public bool IsNode;

        public AddNode()
        {
            InitializeComponent();
        }

        private void btnSafe_Click(object sender, EventArgs e)
        {
            NodeName = tbNodeName.Text;
            IsNode = rbNode.Checked;
            Close();
        }
    }
}
