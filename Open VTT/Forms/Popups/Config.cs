using OpenVTT.Settings;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Open_VTT.Forms.Popups
{
    public partial class Config : Form
    {
        public Config()
        {
            InitializeComponent();

            var mainScreen = Screen.AllScreens.Single(n => n.Primary);
            this.Location = new Point(mainScreen.Bounds.X, mainScreen.Bounds.Y);
            this.Location = new Point(
                mainScreen.Bounds.X + mainScreen.Bounds.Width / 2 - this.Size.Width / 2,
                mainScreen.Bounds.Y + mainScreen.Bounds.Height / 2 - this.Size.Height / 2);

            nudPlayerSize.Value = Settings.Values.PlayerScreenSize;
            cbAutoSaveAction.Checked = Settings.Values.AutoSaveAction;
            cbDisplayChangesInstantly.Checked = Settings.Values.DisplayChangesInstantly;
            cbDisplayGrid.Checked = Settings.Values.DisplayGrid;

            tbServerIP.Text = Settings.Values.NoteServerIP;
            nudServerPort.Value = Settings.Values.NoteServerPort;
        }

        public void nudPlayerSize_ValueChanged(object sender, EventArgs e)
        {
            Settings.Values.PlayerScreenSize = (int)nudPlayerSize.Value;
            Settings.Save();
        }

        public void cbAutoSaveAction_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Values.AutoSaveAction = cbAutoSaveAction.Checked;
            Settings.Save();
        }

        public void cbDisplayChangesInstantly_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Values.DisplayChangesInstantly = cbDisplayChangesInstantly.Checked;
            Settings.Save();
        }

        private void tbServerIP_TextChanged(object sender, EventArgs e)
        {
            Settings.Values.NoteServerIP = tbServerIP.Text;
            Settings.Save();
        }

        private void nudServerPort_ValueChanged(object sender, EventArgs e)
        {
            Settings.Values.NoteServerPort = (int)nudServerPort.Value;
            Settings.Save();
        }

        private void cbDisplayGrid_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Values.DisplayGrid = cbDisplayGrid.Checked;
            Settings.Save();
        }
    }
}
