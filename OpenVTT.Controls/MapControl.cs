using OpenVTT.Common;
using OpenVTT.Controls.Displayer;
using OpenVTT.FogOfWar;
using OpenVTT.Forms;
using OpenVTT.Session;
using OpenVTT.StreamDeck;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OpenVTT.Controls
{
    internal partial class MapControl : UserControl
    {
        internal Image dmImage;
        internal Image playerImage;

        MapPlayer playerWindow;

        public DrawingPictureBox DmPictureBox;
        DrawingPictureBox PlayerPictureBox;

        internal bool PrePlaceFogOfWar = false;

        internal Action UpdatePrePlaceFogOfWarList;

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
                StreamDeckStatics.ActionList.Add(("Layer  Up", new Action(() => Invoke(new Action(() => btnLayerUp_Click(null, null))))));
                StreamDeckStatics.ActionList.Add(("Layer  Down", new Action(() => Invoke(new Action(() => btnLayerDown_Click(null, null))))));

                StreamDeckStatics.ActionList.Add(("Reveal All", new Action(() => Invoke(new Action(() => btnRevealAll_Click(null, null))))));
                StreamDeckStatics.ActionList.Add(("Cover  All", new Action(() => Invoke(new Action(() => btnCoverAll_Click(null, null))))));
                StreamDeckStatics.ActionList.Add(("Set    Active", new Action(() => Invoke(new Action(() => btnSetActive_Click(null, null))))));

                Session.Session.Values.Scenes.Select(n => (n.Name, n))
                    .ToList()
                    .ForEach(n => StreamDeckStatics.StateDescrptions.Single(m => m.State == "Scene").PageingActions.Add(
                        (
                        n.Name,
                        new Action(
                            () => Invoke(
                                new Action(
                                    ()=>LoadScene(n.n, 0)
                                    )
                                )
                            )
                        )
                        ));
            }
        }

        public void LoadScene(Scene SceneToLoad, int LayerToLoad)
        {
            Session.Session.SetLayer(LayerToLoad);
            Session.Session.SetScene(SceneToLoad);

            if (Session.Session.Values.SessionFolder == string.Empty)
                return;

            try
            {
                Invoke(new Action(
                        () =>
                        {
                            SetLayerDisplay($"Layer: {Session.Session.Values.ActiveLayer} [{SceneToLoad.Layers.Min(n => n.LayerNumber)} to {SceneToLoad.Layers.Max(n => n.LayerNumber)}] | {SceneToLoad.Name}");

                            // Re-Add Scenes
                            cbxScenes.Items.Clear();
                            cbxScenes.Items.AddRange(Session.Session.Values.Scenes.ToArray());
                        }
                    )
                    );
            }
            catch
            {
                SetLayerDisplay($"Layer: {Session.Session.Values.ActiveLayer} [{SceneToLoad.Layers.Min(n => n.LayerNumber)} to {SceneToLoad.Layers.Max(n => n.LayerNumber)}] | {SceneToLoad.Name}");
                // Re-Add Scenes
                cbxScenes.Items.Clear();
                cbxScenes.Items.AddRange(Session.Session.Values.Scenes.ToArray());
            }

            // Load new Background-Image
            if (SceneToLoad.Layers.Count > 0 && Session.Session.GetLayer(Session.Session.Values.ActiveLayer).ImagePath != string.Empty)
            {
                dmImage = Image.FromFile(Session.Session.UpdatePath());
                playerImage = Image.FromFile(Session.Session.UpdatePath());
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
            var layer = Session.Session.GetLayer(Session.Session.Values.ActiveLayer);
            if (layer != null)
            {
                var action = new Action<Image, Color>((Image img, Color color) =>
                {
                    if (layer.FogOfWar.Count == 0) return;

                    if (img == dmImage)
                        dmImage = layer.FogOfWar.FirstOrDefault()?.DrawFogOfWarComplete(Session.Session.UpdatePath(), layer.FogOfWar, color, false);
                    else
                        playerImage = layer.FogOfWar.FirstOrDefault()?.DrawFogOfWarComplete(Session.Session.UpdatePath(), layer.FogOfWar, color, true);
                });

                action(dmImage, Color.FromArgb(150, 0, 0, 0));
                action(playerImage, Color.FromArgb(255, 0, 0, 0));
            }

            GC.Collect();
            ShowImages(false);

            UpdatePrePlaceFogOfWarList?.Invoke();
            StreamDeckStatics.SwitchDeckState(1);
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

            if (showPlayer || Settings.Settings.Values.DisplayChangesInstantly)
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
                    if (!Directory.Exists(Session.Session.GetSubDirectoryPath("Images")))
                        Directory.CreateDirectory(Session.Session.GetSubDirectoryPath("Images"));

                    if (!Directory.Exists(Session.Session.GetSubDirectoryPath("Videos")))
                        Directory.CreateDirectory(Session.Session.GetSubDirectoryPath("Videos"));

                    if (!Directory.Exists(Session.Session.GetSubDirectoryPath("Thumbnails")))
                        Directory.CreateDirectory(Session.Session.GetSubDirectoryPath("Thumbnails"));


                    bool isImage;
                    var ImageExtensions = new List<string>
                    {
                        "JPG",
                        "GIF",
                        "JPEG",
                        "PNG",
                        "BMP",
                        "TIFF",
                        "SVG",
                    };
                    if (ImageExtensions.Contains(Path.GetExtension(openFileDialog.FileName).ToUpperInvariant()))
                        isImage = true;
                    else
                        isImage = false;

                    var newFileName = Session.Session.GetSubDirectoryPathForFile(isImage ? "Images" : "Videos", Path.GetFileName(openFileDialog.FileName));

                    File.Copy(openFileDialog.FileName, newFileName, true);

                    LoadImages(newFileName);
                    ShowImages(false);

                    var layer = Session.Session.GetLayer(Session.Session.Values.ActiveLayer);
                    layer.RootPath = Session.Session.Values.SessionFolder;
                    layer.ImagePath = newFileName;
                    layer.IsImageLayer = isImage;

                    if (Settings.Settings.Values.AutoSaveAction)
                        Session.Session.Save(true);
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

            Session.Session.GetLayer(Session.Session.Values.ActiveLayer).FogOfWar.Clear();

            var fog = new FogOfWar.FogOfWar
            {
                BoxSize = new Size(DmPictureBox.Width, DmPictureBox.Height),
                state = FogState.Add,
                IsToggleFog = false,
            };

            (int pictureWidth, int pictureHeight, int offsetLeftRight, int offsetTopBottom, _) = PictureBoxHelper.GetPictureDimensions(fog, new Size(dmImage.Width, dmImage.Height));

            fog.DrawSize = new Size(pictureWidth, pictureHeight);
            fog.Position = new Point(offsetLeftRight, offsetTopBottom);

            Session.Session.GetLayer(Session.Session.Values.ActiveLayer).FogOfWar.Add(fog);
            var layer = Session.Session.GetLayer(Session.Session.Values.ActiveLayer);

            dmImage = fog.DrawFogOfWarComplete(Session.Session.UpdatePath(), layer.FogOfWar, Settings.Settings.Values.DmColor, false);
            playerImage = fog.DrawFogOfWarComplete(Session.Session.UpdatePath(), layer.FogOfWar, Settings.Settings.Values.PlayerColor, true);

            //fog.DrawFogOfWar(dmImage, Color.FromArgb(150, 0, 0, 0));
            //fog.DrawFogOfWar(playerImage, Color.FromArgb(255, 0, 0, 0));

            ShowImages(false);

            PlayerPictureBox.SetPingPoint(new Point(-1, -1));

            if (Settings.Settings.Values.AutoSaveAction)
                Session.Session.Save(true);
        }

        private void btnRevealAll_Click(object sender, EventArgs e)
        {
            if (dmImage == null)
                return;

            var fog = new FogOfWar.FogOfWar
            {
                BoxSize = new Size(DmPictureBox.Width, DmPictureBox.Height),
                state = FogState.Add,
                IsToggleFog = false,
            };

            (int pictureWidth, int pictureHeight, int offsetLeftRight, int offsetTopBottom, _) = PictureBoxHelper.GetPictureDimensions(fog, new Size(dmImage.Width, dmImage.Height));
            fog.DrawSize = new Size(pictureWidth, pictureHeight);
            fog.Position = new Point(offsetLeftRight, offsetTopBottom);

            Session.Session.GetLayer(Session.Session.Values.ActiveLayer).FogOfWar.Clear();
            var layer = Session.Session.GetLayer(Session.Session.Values.ActiveLayer);

            dmImage = fog.DrawFogOfWarComplete(Session.Session.UpdatePath(), layer.FogOfWar, Settings.Settings.Values.DmColor, false);
            playerImage = fog.DrawFogOfWarComplete(Session.Session.UpdatePath(), layer.FogOfWar, Settings.Settings.Values.PlayerColor, true);

            ShowImages(false);

            PlayerPictureBox.SetPingPoint(new Point(-1, -1));

            if (Settings.Settings.Values.AutoSaveAction)
                Session.Session.Save(true);
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
            var layer = Session.Session.GetLayer(Session.Session.Values.ActiveLayer + 1);
            // Layer Exists
            if (layer != null)
                LoadScene(Session.Session.Values.ActiveScene, Session.Session.Values.ActiveLayer + 1);
            else // layer doesn't exist
            {
                var cLayer = Session.Session.GetLayer(Session.Session.Values.ActiveLayer);
                if (cLayer.ImagePath != string.Empty && cLayer.RootPath != string.Empty) // Only create new Layer if Image is loaded to active layer
                {
                    Session.Session.Values.ActiveScene.Layers.Add(new Layer { LayerNumber = Session.Session.Values.ActiveLayer + 1, DirectorySeperator = Path.DirectorySeparatorChar });

                    if (Settings.Settings.Values.AutoSaveAction)
                        Session.Session.Save(false);

                    LoadScene(Session.Session.Values.ActiveScene, Session.Session.Values.ActiveLayer + 1);
                }
            }
        }

        private void btnLayerDown_Click(object sender, EventArgs e)
        {
            var layer = Session.Session.GetLayer(Session.Session.Values.ActiveLayer - 1);

            if (layer != null)// Layer Exists
                LoadScene(Session.Session.Values.ActiveScene, Session.Session.Values.ActiveLayer - 1);
            else // layer doesn't exist
            {
                var cLayer = Session.Session.GetLayer(Session.Session.Values.ActiveLayer);
                if (cLayer.ImagePath != string.Empty && cLayer.RootPath != string.Empty)
                {
                    var nLayer = new Layer
                    {
                        LayerNumber = Session.Session.Values.ActiveLayer - 1,
                        DirectorySeperator = Path.DirectorySeparatorChar,
                    };
                    Session.Session.Values.ActiveScene.Layers.Add(nLayer);

                    if (Settings.Settings.Values.AutoSaveAction)
                        Session.Session.Save(false);

                    LoadScene(Session.Session.Values.ActiveScene, nLayer.LayerNumber);
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
                    Session.Session.Values.Scenes.Add(sc);

                    if (Settings.Settings.Values.AutoSaveAction)
                        Session.Session.Save(false);
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