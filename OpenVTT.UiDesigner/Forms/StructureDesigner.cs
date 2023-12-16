using OpenVTT.UiDesigner.UserControls;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace OpenVTT.UiDesigner.Forms
{
    public partial class StructureDesigner : Form
    {
        public string[] Types { get; set; }

        public OpenVttFileStructure[] Structure
        {
            get => flowLayoutPanel1.Controls.Cast<OpenVttFileStructure>().ToArray();
            set
            {
                foreach (var t in value) { t.RemoveAction += () => flowLayoutPanel1.Controls.Remove(t); flowLayoutPanel1.Controls.Add(t); }
            }
        }


        public StructureDesigner()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var ctrl = new OpenVttFileStructure();
            ctrl.Types = Types;
            ctrl.Name = "";
            ctrl.RemoveAction += () => flowLayoutPanel1.Controls.Remove(ctrl);
            flowLayoutPanel1.Controls.Add(ctrl);
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
