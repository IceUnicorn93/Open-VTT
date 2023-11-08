using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace OpenVTT.UiDesigner
{
    public partial class Structure : UserControl
    {
        public string Name
        {
            get => tbName.Text;
            set => tbName.Text = value;
        }

        public string Type
        {
            get => cbType.Text;
            set => cbType.Text = value;
        }

        public bool SingleValue
        {
            get => rbSingleValue.Checked;
            set => rbSingleValue.Checked = value;
        }

        public string[] Types
        {
            get => cbType.Items.Cast<string>().ToArray();
            set { cbType.Items.Clear(); cbType.Items.AddRange(value); }
        }

        public Action RemoveAction;

        public Structure()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RemoveAction?.Invoke();
        }
    }
}
