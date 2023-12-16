using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenVTT.UiDesigner.Forms
{
    public partial class UiDesignStreamDeckConfigurator : Form
    {
        public List<Button> buttons = new List<Button>();
        public List<Button> selectedButtons = new List<Button>();

        public UiDesignStreamDeckConfigurator()
        {
            InitializeComponent();
        }

        private void UiDesignStreamDeckConfigurator_Load(object sender, EventArgs e)
        {
            foreach (Button button in buttons.OrderBy(n => n.Text))
            {
                var cb = new CheckBox();
                cb.Text = $"{button.Text} | {button.Name}";
                cb.Tag = button;
                cb.Size = new Size(200, cb.Height);
                flp.Controls.Add(cb);
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            selectedButtons.AddRange(flp.Controls.Cast<CheckBox>().Where(n => n.Checked).Select(n => n.Tag as Button));
            this.Close();
        }
    }
}
