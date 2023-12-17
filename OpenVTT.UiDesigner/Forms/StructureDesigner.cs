using OpenVTT.UiDesigner.Interfaces;
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

        private void btnAddArtwork_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Cast<OpenVttFileStructure>().Any(n =>
            {
                var iN = n as IStructureBase;
                return iN.Type == "ArtworkInformation";
            })) return;

            var ctrl = new OpenVttFileStructure();
            ctrl.Types = new[] { "ArtworkInformation" };
            var iCtrl = ctrl as IStructureBase;
            ctrl.RemoveAction += () => flowLayoutPanel1.Controls.Remove(ctrl);
            iCtrl.Name = "Artwork";
            iCtrl.SingleValue = true;
            iCtrl.Type = "ArtworkInformation";
            flowLayoutPanel1.Controls.Add(ctrl);
        }
    }
}
