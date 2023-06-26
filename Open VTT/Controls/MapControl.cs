﻿using Open_VTT.Classes;
using Open_VTT.Forms.Popups;
using Open_VTT.Forms.Popups.Displayer;
using OpenVTT.Common;
using OpenVTT.FogOfWar;
using OpenVTT.Session;
using OpenVTT.Settings;
using OpenVTT.StreamDeck;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Open_VTT.Controls
{
    internal partial class MapControl : UserControl
    {
        internal Image dmImage;
        internal Image playerImage;

        MapPlayer playerWindow;

        public DrawingPictureBox DmPictureBox;
        DrawingPictureBox PlayerPictureBox;

        internal bool PrePlaceFogOfWar = false;

        public MapControl()
        {
            InitializeComponent();
        }

        public void Init()
        {
            playerWindow = WindowInstaces.Player;
            PlayerPictureBox = playerWindow.GetPictureBox();
            DmPictureBox.DrawMode = PictureBoxMode.Rectangle;

            if (DmPictureBox == null)
                throw new ArgumentNullException(nameof(DmPictureBox));
            if (PlayerPictureBox == null)
                throw new ArgumentNullException(nameof(PlayerPictureBox));

            if (StreamDeckStatics.IsInitialized)
            {
                StreamDeckStatics.SetAction((0, 0), new Action(() => Invoke(new Action(() => btnLayerUp_Click(null, null)))));
                StreamDeckStatics.SetAction((0, 1), new Action(() => Invoke(new Action(() => btnLayerDown_Click(null, null)))));

                StreamDeckStatics.SetAction((1, 0), new Action(() => Invoke(new Action(() => btnRevealAll_Click(null, null)))));
                StreamDeckStatics.SetAction((1, 1), new Action(() => Invoke(new Action(() => btnCoverAll_Click(null, null)))));
                StreamDeckStatics.SetAction((1, 2), new Action(() => Invoke(new Action(() => btnSetActive_Click(null, null)))));

                StreamDeckStatics.LoadScene = new Action<Scene, int>((s, i) => Invoke(new Action(() => LoadScene(s, i))));
            }
        }

        public void LoadScene(Scene SceneToLoad, int LayerToLoad)
        {
            Session.SetLayer(LayerToLoad);
            Session.SetScene(SceneToLoad);

            if (Session.Values.SessionFolder == string.Empty)
                return;

            try
            {
                Invoke(new Action(
                        () =>
                        {
                            SetLayerDisplay($"Layer: {Session.Values.ActiveLayer} [{SceneToLoad.Layers.Min(n => n.LayerNumber)} to {SceneToLoad.Layers.Max(n => n.LayerNumber)}] | {SceneToLoad.Name}");

                            // Re-Add Scenes
                            cbxScenes.Items.Clear();
                            cbxScenes.Items.AddRange(Session.Values.Scenes.ToArray());
                        }
                    )
                    );
            }
            catch
            {
                SetLayerDisplay($"Layer: {Session.Values.ActiveLayer} [{SceneToLoad.Layers.Min(n => n.LayerNumber)} to {SceneToLoad.Layers.Max(n => n.LayerNumber)}] | {SceneToLoad.Name}");
                // Re-Add Scenes
                cbxScenes.Items.Clear();
                cbxScenes.Items.AddRange(Session.Values.Scenes.ToArray());
            }

            // Load new Background-Image
            if (SceneToLoad.Layers.Count > 0 && Session.GetLayer(Session.Values.ActiveLayer).ImagePath != string.Empty)
            {
                dmImage = Image.FromFile(Session.UpdatePath());
                playerImage = Image.FromFile(Session.UpdatePath());
            }
            else
            {
                dmImage?.Dispose();
                dmImage = null;
                playerImage?.Dispose();
                playerImage = null;

                DmPictureBox.Image?.Dispose();
                DmPictureBox.Image = null;
            }


            // Add Fog of War
            var layer = Session.GetLayer(Session.Values.ActiveLayer);
            if (layer != null)
            {
                var action = new Action<Image, Color>((Image img, Color color) =>
                {
                    if (layer.FogOfWar.Count == 0) return;

                    if (img == dmImage)
                        dmImage = layer.FogOfWar.FirstOrDefault()?.DrawFogOfWarComplete(Session.UpdatePath(), layer.FogOfWar, color, false);
                    else
                        playerImage = layer.FogOfWar.FirstOrDefault()?.DrawFogOfWarComplete(Session.UpdatePath(), layer.FogOfWar, color, true);
                });

                action(dmImage, Color.FromArgb(150, 0, 0, 0));
                action(playerImage, Color.FromArgb(255, 0, 0, 0));
            }

            GC.Collect();
            ShowImages(false);
        }

        public void SetLayerDisplay(string text)
        {
            lblLayer.Text = text;
        }

        void LoadImages(string Path)
        {
            dmImage?.Dispose();
            dmImage = null;
            dmImage = Image.FromFile(Path);
            playerImage?.Dispose();
            playerImage = null;
            playerImage = Image.FromFile(Path);
        }

        public void ShowImages(bool showPlayer)
        {
            ShowImages(dmImage, playerImage, showPlayer);
        }

        public void ShowImages(Image dm, Image player, bool showPlayer)
        {
            DmPictureBox.Image = dm;

            if (showPlayer || Settings.Values.DisplayChangesInstantly)
            {
                PlayerPictureBox.Image = player;
            }
        }


        private void btnImportImage_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    var newFileName = Session.GetSubDirectoryPathForFile("Images", Path.GetFileName(openFileDialog.FileName));

                    if (!Directory.Exists(Session.GetSubDirectoryPath("Images")))
                        Directory.CreateDirectory(Session.GetSubDirectoryPath("Images"));

                    File.Copy(openFileDialog.FileName, newFileName, true);

                    LoadImages(newFileName);
                    ShowImages(false);

                    var layer = Session.GetLayer(Session.Values.ActiveLayer);
                    layer.RootPath = Session.Values.SessionFolder;
                    layer.ImagePath = newFileName;

                    if (Settings.Values.AutoSaveAction)
                        Session.Save(true);
                }
            }
        }

        private void btnSetActive_Click(object sender, EventArgs e)
        {
            WindowInstaces.Player.Show();
            ShowImages(true);

            WindowInstaces.Player.Size = new Size(this.ParentForm.Size.Width, this.ParentForm.Size.Height);

            //MessageBox.Show(
            //    $"{WindowInstaces.Player.Size.Width} - {WindowInstaces.Player.Size.Height}{Environment.NewLine}" +
            //    $"{this.ParentForm.Size.Width} - {this.ParentForm.Size.Height}");
        }

        private void btnCoverAll_Click(object sender, EventArgs e)
        {
            if (dmImage == null)
                return;

            var fog = new FogOfWar
            {
                BoxSize = new Size(DmPictureBox.Width, DmPictureBox.Height),
                state = FogState.Add,
                IsToggleFog = false,
            };

            (int pictureWidth, int pictureHeight, int offsetLeftRight, int offsetTopBottom, _) = PictureBoxHelper.GetPictureDimensions(fog, new Size(dmImage.Width, dmImage.Height));

            fog.DrawSize = new Size(pictureWidth, pictureHeight);
            fog.Position = new Point(offsetLeftRight, offsetTopBottom);

            Session.GetLayer(Session.Values.ActiveLayer).FogOfWar.Add(fog);
            var layer = Session.GetLayer(Session.Values.ActiveLayer);

            dmImage = fog.DrawFogOfWarComplete(Session.UpdatePath(), layer.FogOfWar, Session.Values.DmColor, false);
            playerImage = fog.DrawFogOfWarComplete(Session.UpdatePath(), layer.FogOfWar, Session.Values.PlayerColor, true);

            //fog.DrawFogOfWar(dmImage, Color.FromArgb(150, 0, 0, 0));
            //fog.DrawFogOfWar(playerImage, Color.FromArgb(255, 0, 0, 0));

            ShowImages(false);

            PlayerPictureBox.SetPingPoint(new Point(-1, -1));

            if (Settings.Values.AutoSaveAction)
                Session.Save(true);
        }

        private void btnRevealAll_Click(object sender, EventArgs e)
        {
            if (dmImage == null)
                return;

            var fog = new FogOfWar
            {
                BoxSize = new Size(DmPictureBox.Width, DmPictureBox.Height),
                state = FogState.Add,
                IsToggleFog = false,
            };

            (int pictureWidth, int pictureHeight, int offsetLeftRight, int offsetTopBottom, _) = PictureBoxHelper.GetPictureDimensions(fog, new Size(dmImage.Width, dmImage.Height));
            fog.DrawSize = new Size(pictureWidth, pictureHeight);
            fog.Position = new Point(offsetLeftRight, offsetTopBottom);

            Session.GetLayer(Session.Values.ActiveLayer).FogOfWar.Clear();
            var layer = Session.GetLayer(Session.Values.ActiveLayer);

            dmImage = fog.DrawFogOfWarComplete(Session.UpdatePath(), layer.FogOfWar, Session.Values.DmColor, false);
            playerImage = fog.DrawFogOfWarComplete(Session.UpdatePath(), layer.FogOfWar, Session.Values.PlayerColor, true);

            ShowImages(false);

            PlayerPictureBox.SetPingPoint(new Point(-1, -1));

            if (Settings.Values.AutoSaveAction)
                Session.Save(true);
        }

        private void btnRectangleFogOfWar_Click(object sender, EventArgs e)
        {
            DmPictureBox.DrawMode = PictureBoxMode.Rectangle;
            btnPoligonFogOfWar.BackColor = Color.FromKnownColor(KnownColor.Control);
            btnPoligonFogOfWar.UseVisualStyleBackColor = true;
            btnRectangleFogOfWar.BackColor = Color.GreenYellow;
        }

        private void btnPoligonFogOfWar_Click(object sender, EventArgs e)
        {
            DmPictureBox.DrawMode = PictureBoxMode.Poligon;
            btnRectangleFogOfWar.BackColor = Color.FromKnownColor(KnownColor.Control);
            btnRectangleFogOfWar.UseVisualStyleBackColor = true;
            btnPoligonFogOfWar.BackColor = Color.GreenYellow;
        }

        private void btnLayerUp_Click(object sender, EventArgs e)
        {
            var layer = Session.GetLayer(Session.Values.ActiveLayer + 1);
            // Layer Exists
            if (layer != null)
                LoadScene(Session.Values.ActiveScene, Session.Values.ActiveLayer + 1);
            else // layer doesn't exist
            {
                var cLayer = Session.GetLayer(Session.Values.ActiveLayer);
                if (cLayer.ImagePath != string.Empty && cLayer.RootPath != string.Empty) // Only create new Layer if Image is loaded to active layer
                {
                    Session.Values.ActiveScene.Layers.Add(new Layer { LayerNumber = Session.Values.ActiveLayer + 1, DirectorySeperator = Path.DirectorySeparatorChar });

                    if (Settings.Values.AutoSaveAction)
                        Session.Save(false);

                    LoadScene(Session.Values.ActiveScene, Session.Values.ActiveLayer + 1);
                }
            }
        }

        private void btnLayerDown_Click(object sender, EventArgs e)
        {
            var layer = Session.GetLayer(Session.Values.ActiveLayer - 1);

            if (layer != null)// Layer Exists
                LoadScene(Session.Values.ActiveScene, Session.Values.ActiveLayer - 1);
            else // layer doesn't exist
            {
                var cLayer = Session.GetLayer(Session.Values.ActiveLayer);
                if (cLayer.ImagePath != string.Empty && cLayer.RootPath != string.Empty)
                {
                    var nLayer = new Layer
                    {
                        LayerNumber = Session.Values.ActiveLayer - 1,
                        DirectorySeperator = Path.DirectorySeparatorChar,
                    };
                    Session.Values.ActiveScene.Layers.Add(nLayer);

                    if (Settings.Values.AutoSaveAction)
                        Session.Save(false);

                    LoadScene(Session.Values.ActiveScene, nLayer.LayerNumber);
                }
            }
        }

        private void btnNewScene_Click(object sender, EventArgs e)
        {
            using (var form = new NewScene())
            {
                form.ShowDialog();
                if (form.Create)
                {
                    var sc = new Scene
                    {
                        Name = form.SceneName
                    };
                    sc.Layers.Add(new Layer { LayerNumber = 0, DirectorySeperator = Path.DirectorySeparatorChar });

                    cbxScenes.Items.Add(sc);
                    Session.Values.Scenes.Add(sc);

                    if (Settings.Values.AutoSaveAction)
                        Session.Save(false);
                }
            }
        }

        private void cbxScenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadScene((Scene)cbxScenes.SelectedItem, 0);
        }

        private void btnImidiateFogOfWar_Click(object sender, EventArgs e)
        {
            PrePlaceFogOfWar = false;

            btnReSetFogOfWar.BackColor = Color.FromKnownColor(KnownColor.Control);
            btnReSetFogOfWar.UseVisualStyleBackColor = true;
            btnImidiateFogOfWar.BackColor = Color.GreenYellow;
        }

        private void btnReSetFogOfWar_Click(object sender, EventArgs e)
        {
            PrePlaceFogOfWar = true;

            btnImidiateFogOfWar.BackColor = Color.FromKnownColor(KnownColor.Control);
            btnImidiateFogOfWar.UseVisualStyleBackColor = true;
            btnReSetFogOfWar.BackColor = Color.GreenYellow;
        }
    }
}
