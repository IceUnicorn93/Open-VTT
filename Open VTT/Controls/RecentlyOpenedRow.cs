using Open_VTT.Other;
using System.IO;
using System.Windows.Forms;

namespace Open_VTT.Controls
{
    public partial class RecentlyOpenedRow : UserControl
    {
        private string FilePath;

        internal event SessionLoad SessionLoaded;

        public RecentlyOpenedRow()
        {
            InitializeComponent();
        }

        public RecentlyOpenedRow(string path = "") : this()
        {
            if (path == "") return;

            FilePath = path;
            lblName.Text = new DirectoryInfo(FilePath).Parent.Name;
        }

        private void btnOpen_Click(object sender, System.EventArgs e)
        {
            SessionLoaded?.Invoke(FilePath);
        }
    }
}
