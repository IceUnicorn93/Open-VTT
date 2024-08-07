﻿using OpenVTT.AnimatedMap;
using OpenVTT.Common;
using OpenVTT.Controls.Displayer;
using OpenVTT.FogOfWar;
using OpenVTT.Forms;
using OpenVTT.Logging;
using OpenVTT.Session;
using OpenVTT.StreamDeck;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenVTT.Controls
{
    internal partial class MapControl : UserControl
    {
        internal Image dmImage;
        internal Image playerImage;

        AnimatedMapDisplayer playerWindow;

        public DrawingPictureBox DmPictureBox;

        internal bool PrePlaceFogOfWar = false;

        internal Action UpdatePrePlaceFogOfWarList;

        public MapControl()
        {
            Logger.Log("Class: MapControl | Constructor");

            InitializeComponent();
        }

        public void Init()
        {
            Logger.Log("Class: MapControl | Init");

            playerWindow = WindowInstaces.AnimatedMapDisplayer;
            DmPictureBox.DrawMode = PictureBoxMode.Rectangle;

            if (DmPictureBox == null)
                throw new ArgumentNullException(nameof(DmPictureBox));
            if (playerWindow == null)
                throw new ArgumentNullException(nameof(playerWindow));

            if (StreamDeckStatics.IsInitialized)
            {
                StreamDeckStatics.ActionList.Add(("Layer  Up", "Scene.LayerUp", new Action(() => Invoke(new Action(() => btnLayerUp_Click(null, null))))));
                StreamDeckStatics.ActionList.Add(("Layer  Down", "Scene.LayerDown", new Action(() => Invoke(new Action(() => btnLayerDown_Click(null, null))))));

                StreamDeckStatics.ActionList.Add(("Reveal All", "Scene.RevealAll", new Action(() => Invoke(new Action(() => btnRevealAll_Click(null, null))))));
                StreamDeckStatics.ActionList.Add(("Cover  All", "Scene.CoverAll", new Action(() => Invoke(new Action(() => btnCoverAll_Click(null, null))))));
                StreamDeckStatics.ActionList.Add(("Set    Active", "Fog.SetActive", new Action(() => Invoke(new Action(() => btnSetActive_Click(null, null))))));

                if (StreamDeckStatics.IsInitialized)
                {
                    Session.Session.Values.Scenes.Select(n => (n.Name, n))
                                .ToList()
                                .ForEach(n => StreamDeckStatics.StateDescrptions.Single(m => m.State == "Scene").PageingActions.Add(
                                    (
                                    n.Name,
                                    new Action(
                                        () => Invoke(
                                            new Action(
                                                () => LoadScene(n.n, 0)
                                                )
                                            )
                                        )
                                    )
                                    )); 
                }
            }
        }

        public void LoadScene(Scene SceneToLoad, int LayerToLoad)
        {
            Logger.Log("Class: MapControl | LoadScene");

            Session.Session.SetLayer(LayerToLoad);
            Session.Session.SetScene(SceneToLoad);

            if (Session.Session.Values.SessionFolder == string.Empty)
                return;

            try
            {
                Invoke(new Action(
                        () =>
                        {
                            SetLayerDisplay($"Layer: {Session.Session.Values.ActiveLayerNumber} [{SceneToLoad.Layers.Min(n => n.LayerNumber)} to {SceneToLoad.Layers.Max(n => n.LayerNumber)}] | {SceneToLoad.Name}");

                            // Re-Add Scenes
                            cbxScenes.Items.Clear();
                            cbxScenes.Items.AddRange(Session.Session.Values.Scenes.ToArray());
                        }
                    )
                    );
            }
            catch
            {
                SetLayerDisplay($"Layer: {Session.Session.Values.ActiveLayerNumber} [{SceneToLoad.Layers.Min(n => n.LayerNumber)} to {SceneToLoad.Layers.Max(n => n.LayerNumber)}] | {SceneToLoad.Name}");
                // Re-Add Scenes
                cbxScenes.Items.Clear();
                cbxScenes.Items.AddRange(Session.Session.Values.Scenes.ToArray());
            }

            // Load new Background-Image
            if (SceneToLoad.Layers.Count > 0 && Session.Session.Values.ActiveLayer.ImagePath != string.Empty)
            {
                var filePathDM = "";
                var filePathPlayer = "";
                if (Session.Session.Values.ActiveLayer.IsImageLayer)
                { 
                    filePathDM = Session.Session.UpdatePath();
                    filePathPlayer = Session.Session.UpdatePath();
                }
                else
                {
                    filePathDM = Session.Session.UpdateVideoPath(Session.Session.Values.ActiveLayer, true);
                    filePathPlayer = Session.Session.UpdateVideoPath(Session.Session.Values.ActiveLayer, false);
                }

                dmImage = Image.FromFile(filePathDM);
                if (Session.Session.Values.ActiveLayer.IsImageLayer)
                    playerImage = Image.FromFile(filePathPlayer);
                else
                { 
                    //playerWindow.DisplayItemURL = Session.Session.Values.ActiveLayer.ImagePath;
                }
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
            var layer = Session.Session.Values.ActiveLayer;
            if (layer != null)
            {
                var action = new Action<Image, Color>((Image img, Color color) =>
                {
                    if (img == dmImage)
                    {
                        if(layer.IsImageLayer)
                            dmImage = FogOfWar.FogOfWar.DrawFogOfWarComplete(Session.Session.UpdatePath(), layer.FogOfWar, color, false);
                        else
                            dmImage = FogOfWar.FogOfWar.DrawFogOfWarComplete(Session.Session.UpdateVideoPath(layer, true), layer.FogOfWar, color, false);
                    }
                    else
                    {
                        if(layer.IsImageLayer)
                            playerImage = FogOfWar.FogOfWar.DrawFogOfWarComplete(Session.Session.UpdatePath(), layer.FogOfWar, color, true);
                        else
                            playerImage = FogOfWar.FogOfWar.DrawFogOfWarComplete(Path.Combine(Application.StartupPath, "Greenscreen.png"), layer.FogOfWar, color, true);
                    }
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
            Logger.Log("Class: MapControl | SetLayerDisplay");

            lblLayer.Text = text;
        }

        void LoadImages(string Path)
        {
            Logger.Log("Class: MapControl | LoadImages");

            dmImage?.Dispose();
            dmImage = null;

            dmImage = Image.FromFile(Path);
            playerImage?.Dispose();
            playerImage = null;
            if(Session.Session.Values.ActiveLayer.IsImageLayer)
                playerImage = Image.FromFile(Path);
        }

        public void ShowImages(bool showPlayer)
        {
            Logger.Log("Class: MapControl | ShowImages(bool showPlayer)");

            ShowImages(dmImage, playerImage, showPlayer);
        }

        public void ShowImages(Image dm, Image player, bool showPlayer)
        {
            Logger.Log("Class: MapControl | ShowImages(Image dm, Image player, bool showPlayer)");

            try
            { 
                DmPictureBox.Image = dm;

                if (showPlayer || Settings.Settings.Values.DisplayChangesInstantly)
                {
                    playerWindow.IsDisplayingImage = Session.Session.Values.ActiveLayer.IsImageLayer;
                    playerWindow.DisplayItemURL = Session.Session.Values.ActiveLayer.ImagePath;
                    playerWindow.SetFogOfWarImage(player);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
            }
        }


        private void btnImportImage_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: MapControl | btnImportImage_Click");

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
                        ".JPG",
                        ".GIF",
                        ".JPEG",
                        ".PNG",
                        ".BMP",
                        ".TIFF",
                        ".SVG",
                    };
                    if (ImageExtensions.Contains(Path.GetExtension(openFileDialog.FileName).ToUpper()))
                        isImage = true;
                    else
                        isImage = false;

                    var newFileName = Session.Session.GetSubDirectoryPathForFile(isImage ? "Images" : "Videos", Path.GetFileName(openFileDialog.FileName));

                    File.Copy(openFileDialog.FileName, newFileName, true);

                    var layer = Session.Session.Values.ActiveLayer;
                    layer.RootPath = Session.Session.Values.SessionFolder;
                    layer.ImagePath = newFileName;
                    layer.IsImageLayer = isImage;

                    if(isImage == false)
                    {
                        // Load new File
                        var screenshotter = new AnimatedMapScreenshotter();
                        screenshotter.SetPath(layer.ImagePath);
                        screenshotter.FilePlaying += () =>
                        {
                            // AHA!
                            // Take & Save Screenshot

                            Task.Delay(10); // needed so we get the first frames!

                            var fileName = Path.GetFileName(openFileDialog.FileName);

                            Bitmap bmpScreenshot = new Bitmap(screenshotter.Width, screenshotter.Height);
                            using (Graphics g = Graphics.FromImage(bmpScreenshot))
                            { 
                                g.CopyFromScreen(
                                    screenshotter.Location.X, screenshotter.Location.Y,
                                    0, 0,
                                    screenshotter.Size);

                                
                                var fi = new FileInfo(fileName);
                                var extension = fi.Extension;
                                fileName = fileName.Replace(extension, ".png");
                                bmpScreenshot.Save(Session.Session.GetSubDirectoryPathForFile("Thumbnails", fileName));
                            }

                            //Dispose the Window to get Memory back!
                            screenshotter.Close();
                            screenshotter.Dispose();
                            screenshotter = null;
                            GC.Collect();

                            Task.Delay(10);

                            LoadImages(Session.Session.GetSubDirectoryPathForFile("Thumbnails", fileName));
                            ShowImages(false);
                        };
                        screenshotter.Show();
                    }
                    else
                    {
                        LoadImages(newFileName);
                        ShowImages(false); 
                    }

                    if (Settings.Settings.Values.AutoSaveAction)
                        Session.Session.Save(true);
                }
            }
        }

        private void btnSetActive_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: MapControl | btnSetActive_Click");

            if(Application.OpenForms.OfType<AnimatedMapDisplayer>().Count() == 0)
                WindowInstaces.AnimatedMapDisplayer.Show();
            ShowImages(true);


            //MessageBox.Show(
            //    $"{WindowInstaces.Player.Size.Width} - {WindowInstaces.Player.Size.Height}{Environment.NewLine}" +
            //    $"{this.ParentForm.Size.Width} - {this.ParentForm.Size.Height}");
        }

        private void btnCoverAll_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: MapControl | btnCoverAll_Click");

            if (dmImage == null)
                return;

            Session.Session.Values.ActiveLayer.FogOfWar.Clear();

            var fog = new FogOfWar.FogOfWar
            {
                BoxSize = new Size(DmPictureBox.Width, DmPictureBox.Height),
                state = FogState.Add,
                IsToggleFog = false,
            };

            (int pictureWidth, int pictureHeight, int offsetLeftRight, int offsetTopBottom, _) = PictureBoxHelper.GetPictureDimensions(fog, new Size(dmImage.Width, dmImage.Height));

            fog.DrawSize = new Size(pictureWidth, pictureHeight);
            fog.Position = new Point(offsetLeftRight, offsetTopBottom);

            Session.Session.Values.ActiveLayer.FogOfWar.Add(fog);
            var layer = Session.Session.Values.ActiveLayer;

            if(layer.IsImageLayer)
            { 
                dmImage = FogOfWar.FogOfWar.DrawFogOfWarComplete(Session.Session.UpdatePath(), layer.FogOfWar, Settings.Settings.Values.DmColor, false);
                playerImage = FogOfWar.FogOfWar.DrawFogOfWarComplete(Session.Session.UpdatePath(), layer.FogOfWar, Settings.Settings.Values.PlayerColor, true);
            }
            else
            {
                dmImage = FogOfWar.FogOfWar.DrawFogOfWarComplete(Session.Session.UpdateVideoPath(layer, true), layer.FogOfWar, Settings.Settings.Values.DmColor, false);
                playerImage = FogOfWar.FogOfWar.DrawFogOfWarComplete(Path.Combine(Application.StartupPath, "Greenscreen.png"), layer.FogOfWar, Settings.Settings.Values.PlayerColor, true);
            }

            //fog.DrawFogOfWar(dmImage, Color.FromArgb(150, 0, 0, 0));
            //fog.DrawFogOfWar(playerImage, Color.FromArgb(255, 0, 0, 0));

            ShowImages(false);

            //PlayerPictureBox.SetPingPoint(new Point(-1, -1));

            if (Settings.Settings.Values.AutoSaveAction)
                Session.Session.Save(true);
        }

        private void btnRevealAll_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: MapControl | btnRevealAll_Click");

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

            Session.Session.Values.ActiveLayer.FogOfWar.Clear();
            var layer = Session.Session.Values.ActiveLayer;

            if(layer.IsImageLayer)
            {
                dmImage = FogOfWar.FogOfWar.DrawFogOfWarComplete(Session.Session.UpdatePath(), layer.FogOfWar, Settings.Settings.Values.DmColor, false);
                playerImage = FogOfWar.FogOfWar.DrawFogOfWarComplete(Session.Session.UpdatePath(), layer.FogOfWar, Settings.Settings.Values.PlayerColor, true);
            }
            else
            {
                dmImage = FogOfWar.FogOfWar.DrawFogOfWarComplete(Session.Session.UpdateVideoPath(layer, true), layer.FogOfWar, Settings.Settings.Values.DmColor, false);
                playerImage = FogOfWar.FogOfWar.DrawFogOfWarComplete(Path.Combine(Application.StartupPath, "Greenscreen.png"), layer.FogOfWar, Settings.Settings.Values.PlayerColor, true);
            }

            ShowImages(false);

            //PlayerPictureBox.SetPingPoint(new Point(-1, -1));

            if (Settings.Settings.Values.AutoSaveAction)
                Session.Session.Save(true);
        }

        private void btnRectangleFogOfWar_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: MapControl | btnRectangleFogOfWar_Click");

            DmPictureBox.DrawMode = PictureBoxMode.Rectangle;
            btnPoligonFogOfWar.BackColor = Color.FromKnownColor(KnownColor.Control);
            btnPoligonFogOfWar.UseVisualStyleBackColor = true;
            btnRectangleFogOfWar.BackColor = Color.GreenYellow;
        }

        private void btnPoligonFogOfWar_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: MapControl | btnPoligonFogOfWar_Click");

            DmPictureBox.DrawMode = PictureBoxMode.Poligon;
            btnRectangleFogOfWar.BackColor = Color.FromKnownColor(KnownColor.Control);
            btnRectangleFogOfWar.UseVisualStyleBackColor = true;
            btnPoligonFogOfWar.BackColor = Color.GreenYellow;
        }

        private void btnLayerUp_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: MapControl | btnLayerUp_Click");

            var layer = Session.Session.GetLayer(Session.Session.Values.ActiveLayerNumber + 1);
            // Layer Exists
            if (layer != null)
                LoadScene(Session.Session.Values.ActiveScene, Session.Session.Values.ActiveLayerNumber + 1);
            else // layer doesn't exist
            {
                var cLayer = Session.Session.Values.ActiveLayer;
                if (cLayer.ImagePath != string.Empty && cLayer.RootPath != string.Empty) // Only create new Layer if Image is loaded to active layer
                {
                    Session.Session.Values.ActiveScene.Layers.Add(new Layer { LayerNumber = Session.Session.Values.ActiveLayerNumber + 1, DirectorySeperator = Path.DirectorySeparatorChar });

                    if (Settings.Settings.Values.AutoSaveAction)
                        Session.Session.Save(false);

                    LoadScene(Session.Session.Values.ActiveScene, Session.Session.Values.ActiveLayerNumber + 1);
                }
            }
        }

        private void btnLayerDown_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: MapControl | btnLayerDown_Click");

            var layer = Session.Session.GetLayer(Session.Session.Values.ActiveLayerNumber - 1);

            if (layer != null)// Layer Exists
                LoadScene(Session.Session.Values.ActiveScene, Session.Session.Values.ActiveLayerNumber - 1);
            else // layer doesn't exist
            {
                var cLayer = Session.Session.Values.ActiveLayer;
                if (cLayer.ImagePath != string.Empty && cLayer.RootPath != string.Empty)
                {
                    var nLayer = new Layer
                    {
                        LayerNumber = Session.Session.Values.ActiveLayerNumber - 1,
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
            Logger.Log("Class: MapControl | btnNewScene_Click");

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
            Logger.Log("Class: MapControl | cbxScenes_SelectedIndexChanged");

            LoadScene((Scene)cbxScenes.SelectedItem, 0);
        }

        private void btnImidiateFogOfWar_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: MapControl | btnImidiateFogOfWar_Click");

            PrePlaceFogOfWar = false;

            btnReSetFogOfWar.BackColor = Color.FromKnownColor(KnownColor.Control);
            btnReSetFogOfWar.UseVisualStyleBackColor = true;
            btnImidiateFogOfWar.BackColor = Color.GreenYellow;
        }

        private void btnReSetFogOfWar_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: MapControl | btnReSetFogOfWar_Click");

            PrePlaceFogOfWar = true;

            btnImidiateFogOfWar.BackColor = Color.FromKnownColor(KnownColor.Control);
            btnImidiateFogOfWar.UseVisualStyleBackColor = true;
            btnReSetFogOfWar.BackColor = Color.GreenYellow;
        }
    }
}