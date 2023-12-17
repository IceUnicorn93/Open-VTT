using Open_VTT.Forms.Popups;
using OpenVTT.Controls.Displayer;
using OpenVTT.Logging;
using OpenVTT.Scripting;
using OpenVTT.Session;
using OpenVTT.Settings;
using OpenVTT.StreamDeck;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Open_VTT.Forms
{
    public partial class Start : Form
    {
        public Start()
        {
            Logger.Log("Class: Start | Constructor");

            //Some pre Setup stuff
            //Run a script so subsequent calls run faster
            Task.Run(() =>
            {
                var i = ScriptEngine.RunUiScript<int>("1+1", null).Result;
            });
            //Set Action for ScriptHost to Display Text and Images in Scripts
            ScriptHost.DisplayArtworkText = new Action<string>((text) => WindowInstaces.InformationDisplayPlayer.SetDisplayText(text));
            ScriptHost.DisplayArtworkImage = new Action<Image>((img) => { var pb = WindowInstaces.InformationDisplayPlayer.GetPictureBox(); pb.Image?.Dispose(); pb.Image = null; pb.Image = img; });


            InitializeComponent();

            Settings.Load();

            recentlyOpenedControl1.SessionLoaded += LoadWithHide;
            recentlyOpenedControl1.Init();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: Start | btnClose_Click");

            this.Close();
        }

        private void btnConfigure_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: Start | btnConfigure_Click");

            this.Hide();
            using (var config = new Config())
            {
                config.ShowDialog();
            }
            this.Show();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: Start | btnLoad_Click");

            this.Hide();
            using (var openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    recentlyOpenedControl1.AddPath(openFileDialog.FileName);

                    Load(openFileDialog.FileName);
                }
            }
            this.Show();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: Start | btnCreate_Click");

            this.Hide();
            using (var folderBrowserDiaglog = new FolderBrowserDialog())
            {
                if (folderBrowserDiaglog.ShowDialog() == DialogResult.OK)
                {
                    Session.Values.SessionFolder = folderBrowserDiaglog.SelectedPath;
                    Session.Values.ActiveScene = null;

                    using (var sceneCreator = new SceneControl())
                    {
                        recentlyOpenedControl1.AddPath(Path.Combine(Session.Values.SessionFolder, "Session.xml"));

                        sceneCreator.ShowDialog();
                    }
                }
            }
            this.Show();
        }

        private void Load(string path)
        {
            Logger.Log("Class: Start | Load");

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

            WindowInstaces.Init();
        }

        private void LoadWithHide(string path)
        {
            Logger.Log("Class: Start | LoadWithHide");

            this.Hide();

            Load(path);

            this.Show();
        }
    }
}
