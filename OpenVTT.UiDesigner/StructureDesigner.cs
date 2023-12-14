using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace OpenVTT.UiDesigner
{
    public partial class StructureDesigner : Form
    {
        public string[] Types { get; set; }

        public Structure[] Structure
        {
            get => flowLayoutPanel1.Controls.Cast<Structure>().ToArray();
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
            var ctrl = new Structure();
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
