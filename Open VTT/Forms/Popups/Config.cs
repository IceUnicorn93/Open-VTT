using Open_VTT.Classes;
using System;
using System.Windows.Forms;

namespace Open_VTT.Forms.Popups
{
    public partial class Config : Form
    {
        public Config()
        {
            InitializeComponent();

            nudPlayerSize.Value = Settings.Values.PlayerScreenSize;
            cbAutoSaveAction.Checked = Settings.Values.AutoSaveAction;
            cbDisplayChangesInstantly.Checked = Settings.Values.DisplayChangesInstantly;
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
    }
}
