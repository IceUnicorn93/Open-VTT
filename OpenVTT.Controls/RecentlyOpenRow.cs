using OpenVTT.Common;
using OpenVTT.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenVTT.Controls
{
    public partial class RecentlyOpenedRow : UserControl
    {
        private string FilePath;

        internal event SessionLoad SessionLoaded;

        public RecentlyOpenedRow()
        {
            Logger.Log("Class: RecentlyOpenedRow | Constructor");

            InitializeComponent();
        }

        public RecentlyOpenedRow(string path = "") : this()
        {
            Logger.Log("Class: RecentlyOpenedRow | Constructor(string path = \"\")");

            if (path == "") return;

            FilePath = path;
            lblName.Text = new DirectoryInfo(FilePath).Parent.Name;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: RecentlyOpenedRow | btnOpen_Click");

            SessionLoaded?.Invoke(FilePath);
        }
    }
}
