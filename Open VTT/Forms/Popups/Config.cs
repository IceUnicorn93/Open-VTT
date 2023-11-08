using OpenVTT.Controls;
using OpenVTT.Controls.Forms;
using OpenVTT.Logging;
using OpenVTT.Scripting;
using OpenVTT.Settings;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Open_VTT.Forms.Popups
{
    public partial class Config : Form
    {
        public Config()
        {
            Logger.Log("Class: Config | Constructor");

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
            cbDisplayGridForDM.Checked = Settings.Values.DisplayGridForDM;

            pnlDmColor.BackColor = Settings.Values.DmColor;
            pnlPlayerColor.BackColor = Settings.Values.PlayerColor;
            pnlGridColor.BackColor = Settings.Values.GridColor;
            pnlTextColor.BackColor = Settings.Values.TextColor;

            tbServerIP.Text = Settings.Values.NoteServerIP;
            nudServerPort.Value = Settings.Values.NoteServerPort;

            for (int i = 0; i < ScriptEngine.CalculatedHosts.Count; i++)
            {
                var ctrl = new ScriptDisplay(ScriptEngine.CalculatedHosts[i]);
                flpScripts.Controls.Add(ctrl);
            }
        }

        public void nudPlayerSize_ValueChanged(object sender, EventArgs e)
        {
            Logger.Log("Class: Config | nudPlayerSize_ValueChanged");

            Settings.Values.PlayerScreenSize = (int)nudPlayerSize.Value;
            Settings.Save();
        }

        public void cbAutoSaveAction_CheckedChanged(object sender, EventArgs e)
        {
            Logger.Log("Class: Config | cbAutoSaveAction_CheckedChanged");

            Settings.Values.AutoSaveAction = cbAutoSaveAction.Checked;
            Settings.Save();
        }

        public void cbDisplayChangesInstantly_CheckedChanged(object sender, EventArgs e)
        {
            Logger.Log("Class: Config | cbDisplayChangesInstantly_CheckedChanged");

            Settings.Values.DisplayChangesInstantly = cbDisplayChangesInstantly.Checked;
            Settings.Save();
        }

        private void tbServerIP_TextChanged(object sender, EventArgs e)
        {
            Logger.Log("Class: Config | tbServerIP_TextChanged");

            Settings.Values.NoteServerIP = tbServerIP.Text;
            Settings.Save();
        }

        private void nudServerPort_ValueChanged(object sender, EventArgs e)
        {
            Logger.Log("Class: Config | nudServerPort_ValueChanged");

            Settings.Values.NoteServerPort = (int)nudServerPort.Value;
            Settings.Save();
        }

        private void cbDisplayGrid_CheckedChanged(object sender, EventArgs e)
        {
            Logger.Log("Class: Config | cbDisplayGrid_CheckedChanged");

            Settings.Values.DisplayGrid = cbDisplayGrid.Checked;
            Settings.Save();
        }

        private void pnlDmColor_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: Config | pnlDmColor_Click");

            var clr = new ColorSelector(pnlDmColor.BackColor);
            clr.ShowDialog();
            pnlDmColor.BackColor = clr.SelectedColor;
            Settings.Values.DmColor = clr.SelectedColor;
            Settings.Save();
        }

        private void pnlPlayerColor_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: Config | pnlPlayerColor_Click");

            var clr = new ColorSelector(pnlPlayerColor.BackColor);
            clr.ShowDialog();
            pnlPlayerColor.BackColor = clr.SelectedColor;
            Settings.Values.PlayerColor = clr.SelectedColor;
            Settings.Save();
        }

        private void pnlGridColor_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: Config | pnlGridColor_Click");

            var clr = new ColorSelector(pnlGridColor.BackColor);
            clr.ShowDialog();
            pnlGridColor.BackColor = clr.SelectedColor;
            Settings.Values.GridColor = clr.SelectedColor;
            Settings.Save();
        }

        private void pnlTextColor_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: Config | pnlTextColor_Click");

            var clr = new ColorSelector(pnlTextColor.BackColor);
            clr.ShowDialog();
            pnlTextColor.BackColor = clr.SelectedColor;
            Settings.Values.TextColor = clr.SelectedColor;
            Settings.Save();
        }

        private void cbDisplayGridForDM_CheckedChanged(object sender, EventArgs e)
        {
            Logger.Log("Class: Config | cbDisplayGridForDM_CheckedChanged");

            Settings.Values.DisplayGridForDM = cbDisplayGridForDM.Checked;
            Settings.Save();
        }

        private void btnOpenDesigner_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: Config | btnOpenDesigner_Click");

            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = Path.Combine(Application.StartupPath, "Scripts");

                if(fbd.ShowDialog() == DialogResult.OK)
                {
                    using(var sd = new ScriptDesigner())
                    {
                        sd.IsNotesDesigner = false;
                        sd.LoadPath = fbd.SelectedPath;
                        sd.ShowDialog();
                    }
                }
            }
        }
    }
}
