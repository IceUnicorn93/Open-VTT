using Open_VTT.Classes;
using Open_VTT.Forms.Popups;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Open_VTT.Forms
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();

            Settings.Load();

            recentlyOpenedControl1.SessionLoaded += LoadWithHide;
            recentlyOpenedControl1.Init();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfigure_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (var config = new Config())
            {
                config.ShowDialog();
            }
            this.Show();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (var openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Load(openFileDialog.FileName);

                    recentlyOpenedControl1.AddPath(openFileDialog.FileName);
                }
            }
            this.Show();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (var folderBrowserDiaglog = new FolderBrowserDialog())
            {
                if (folderBrowserDiaglog.ShowDialog() == DialogResult.OK)
                {
                    Session.Values.SessionFolder = folderBrowserDiaglog.SelectedPath;
                    Session.Values.ActiveScene = null;

                    using (var sceneCreator = new SceneControl())
                    {
                        sceneCreator.ShowDialog();
                    }
                }
            }
            this.Show();
        }

        private void Load(string path)
        {
            Session.Load(path);
            Session.Values.SessionFolder = Path.GetDirectoryName(path);
            Session.Values.ActiveScene = Session.Values.Scenes.First();

            using (var sceneCreator = new SceneControl())
            {
                sceneCreator.ShowDialog();
            }

            // Close Player Window and Dispose it 
            if (!WindowInstaces.Player.IsDisposed)
                WindowInstaces.Player.Close();

            // Close Information Display for Player Window and Dispose it 
            if (!WindowInstaces.InformationDisplayPlayer.IsDisposed)
                WindowInstaces.InformationDisplayPlayer.Close();

            // Close Information Display for DM Window and Dispose it 
            if (!WindowInstaces.InformationDisplayDM.IsDisposed)
                WindowInstaces.InformationDisplayDM.Close();
        }

        private void LoadWithHide(string path)
        {

            this.Hide();

            Load(path);

            this.Show();
        }
    }
}
