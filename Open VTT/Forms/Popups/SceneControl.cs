using OpenVTT.Common;
using OpenVTT.Controls.Displayer;
using OpenVTT.FogOfWar;
using OpenVTT.Scripting;
using OpenVTT.Session;
using OpenVTT.Settings;
using OpenVTT.StreamDeck;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Open_VTT.Forms.Popups
{
    internal partial class SceneControl : Form
    {
        public SceneControl()
        {
            InitializeComponent();

            var mainScreen = Screen.AllScreens.Single(n => n.Primary);
            this.Location = new Point(mainScreen.Bounds.X, mainScreen.Bounds.Y);
            this.WindowState = FormWindowState.Maximized;

            var drawPbArtworkDM = WindowInstaces.InformationDisplayDM.GetPictureBox();
            var drawPbArtworkPlayer = WindowInstaces.InformationDisplayPlayer.GetPictureBox();
            drawPbArtworkDM.DrawMode = PictureBoxMode.Ping;

            bool canPingArtwork = true;
            drawPbArtworkDM.PointComplete += (Point p) =>
            {
                if (p.X > -1 && p.Y > -1)
                {
                    if (canPingArtwork) canPingArtwork = false;
                    else return;

                    var pbPoint = new FogOfWar
                    {
                        Position = new Point(p.X, p.Y),
                        BoxSize = new Size(drawPbArtworkDM.Width, drawPbArtworkDM.Height),
                        DrawSize = new Size(100, 100),
                        state = FogState.Add
                    };

                    if (treeViewDisplay1.currentInformationItem == null) return;
                    var path = Path.Combine(treeViewDisplay1.currentInformationItem.GetLocation(".png").ToArray());
                    if (File.Exists(path))
                    {
                        pbPoint.DrawCircle(drawPbArtworkDM.Image);
                        pbPoint.DrawCircle(drawPbArtworkPlayer.Image);
                        drawPbArtworkPlayer.Image = drawPbArtworkPlayer.Image;
                    }
                }
                else
                {
                    if (treeViewDisplay1.currentInformationItem == null) return;
                    var path = Path.Combine(treeViewDisplay1.currentInformationItem.GetLocation(".png").ToArray());
                    if (File.Exists(path))
                    {
                        drawPbArtworkDM.BackgroundImageLayout = ImageLayout.Zoom;
                        drawPbArtworkPlayer.BackgroundImageLayout = ImageLayout.Zoom;

                        drawPbArtworkDM.BackgroundImage = (Image)drawPbMap.Image.Clone();
                        //drawPbArtworkPlayer.BackgroundImage = (Image)drawPbArtworkPlayer.Image.Clone();

                        drawPbArtworkDM.Image = null;
                        drawPbArtworkPlayer.Image = null;

                        drawPbArtworkDM.Image = Image.FromFile(path);
                        drawPbArtworkPlayer.Image = Image.FromFile(path);

                        drawPbArtworkDM.BackgroundImage = null;
                        drawPbArtworkPlayer.BackgroundImage = null;

                        GC.Collect();
                    }

                    canPingArtwork = true;
                }
            };

            bool canPing = true;
            drawPbMap.PointComplete += (Point p) =>
            {
                if (p.X > -1 && p.Y > -1)
                {
                    if (canPing) canPing = false;
                    else return;

                    var pbPoint = new FogOfWar
                    {
                        Position = new Point(p.X, p.Y),
                        BoxSize = new Size(drawPbMap.Width, drawPbMap.Height),
                        DrawSize = new Size(100, 100),
                        state = FogState.Add
                    };

                    pbPoint.DrawCircle(drawPbMap.Image);
                    if(WindowInstaces.Player.GetPictureBox().Image != null)
                        pbPoint.DrawCircle(WindowInstaces.Player.GetPictureBox().Image);

                    mapControl1.ShowImages(drawPbMap.Image, WindowInstaces.Player.GetPictureBox().Image, true);
                }
                else
                {
                    var f = new FogOfWar();

                    drawPbMap.BackgroundImageLayout = ImageLayout.Zoom;
                    WindowInstaces.Player.GetPictureBox().BackgroundImageLayout = ImageLayout.Zoom;

                    drawPbMap.BackgroundImage = (Image)drawPbMap.Image.Clone();
                    if(WindowInstaces.Player.GetPictureBox().Image != null)
                        WindowInstaces.Player.GetPictureBox().BackgroundImage = (Image)WindowInstaces.Player.GetPictureBox().Image.Clone();

                    drawPbMap.Image = null;
                    WindowInstaces.Player.GetPictureBox().Image = null;

                    mapControl1.ShowImages(
                    f.DrawFogOfWarComplete(Session.UpdatePath(), Session.Values.ActiveLayer.FogOfWar, Settings.Values.DmColor, false),
                    f.DrawFogOfWarComplete(Session.UpdatePath(), Session.Values.ActiveLayer.FogOfWar, Settings.Values.PlayerColor, true),
                    true);

                    drawPbMap.BackgroundImage = null;
                    WindowInstaces.Player.GetPictureBox().BackgroundImage = null;

                    GC.Collect();

                    canPing = true;
                }

            };

            drawPbMap.RectangleComplete += (Rectangle r) =>
            {
                if (Session.Values.ActiveLayer.FogOfWar.Count == 0)
                    return;
                
                var fogName = "";
                if (mapControl1.PrePlaceFogOfWar == true)
                {
                    var fogForm = new PrePlaceFogOfWar();
                    fogForm.ShowDialog();
                    fogName = fogForm.FogName;
                }

                var fog = new FogOfWar
                {
                    Position = new Point(r.X, r.Y),
                    BoxSize = new Size(drawPbMap.Width, drawPbMap.Height),
                    DrawSize = new Size(r.Width, r.Height),
                    state = FogState.Remove,
                    IsToggleFog = mapControl1.PrePlaceFogOfWar,
                    Name = fogName,
                };

                var layer = Session.Values.ActiveLayer;
                layer.FogOfWar.Add(fog);

                new Task(() =>
                {
                    mapControl1.dmImage = fog.DrawFogOfWarComplete(Session.UpdatePath(), layer.FogOfWar, Settings.Values.DmColor, false);
                    mapControl1.playerImage = fog.DrawFogOfWarComplete(Session.UpdatePath(), layer.FogOfWar, Settings.Values.PlayerColor, true);

                    Thread.Sleep(100);

                    mapControl1.ShowImages(Settings.Values.DisplayChangesInstantly);
                }).Start();

                UpdatePrePlaceFogOfWarList();

                if (Settings.Values.AutoSaveAction)
                    Session.Save(true);
            };

            drawPbMap.PoligonComplete += (Point[] p) =>
            {
                if (Session.Values.ActiveLayer.FogOfWar.Count == 0)
                    return;

                var fogName = "";
                if (mapControl1.PrePlaceFogOfWar == true)
                {
                    var fogForm = new PrePlaceFogOfWar();
                    fogForm.ShowDialog();
                    fogName = fogForm.FogName;
                }

                var fog = new FogOfWar
                {
                    Position = new Point(0, 0),
                    BoxSize = new Size(drawPbMap.Width, drawPbMap.Height),
                    DrawSize = new Size(1, 1),
                    state = FogState.Remove,
                    PoligonData = p.ToList(),
                    IsToggleFog = mapControl1.PrePlaceFogOfWar,
                    Name = fogName,
                };

                var layer = Session.Values.ActiveLayer;
                layer.FogOfWar.Add(fog);

                new Task(() =>
                {
                    mapControl1.dmImage = fog.DrawFogOfWarComplete(Session.UpdatePath(), layer.FogOfWar, Settings.Values.DmColor, false);
                    mapControl1.playerImage = fog.DrawFogOfWarComplete(Session.UpdatePath(), layer.FogOfWar, Settings.Values.PlayerColor, true);

                    Thread.Sleep(100);

                    mapControl1.ShowImages(Settings.Values.DisplayChangesInstantly);

                }).Start();

                UpdatePrePlaceFogOfWarList();

                if (Settings.Values.AutoSaveAction)
                    Session.Save(true);
            };

            treeViewDisplay1.GenerateNewDisplayItem += () =>
            {
                using (var dialog = new DisplayItemGenerator())
                    dialog.ShowDialog();
            };
            treeViewDisplay1.GetDmDisplay += () => WindowInstaces.InformationDisplayDM;
            treeViewDisplay1.GetPlayerDisplay += () => WindowInstaces.InformationDisplayPlayer;
            treeViewDisplay1.GetDmPictureBox += () => WindowInstaces.InformationDisplayDM.GetPictureBox();
            treeViewDisplay1.GetPlayerPictureBox += () => WindowInstaces.InformationDisplayPlayer.GetPictureBox();

            FormClosed += FormClosedEvent;

            Init();
        }

        private void FormClosedEvent(object sender, FormClosedEventArgs e)
        {
            StreamDeckStatics.Dispose();
        }

        void Init()
        {
            if (Session.Values.ActiveScene == null)
            {
                Session.Values.Scenes = new List<Scene>
                {
                    new Scene {
                        Name = "Main",
                        Layers = new List<Layer>
                        {
                            new Layer
                            {
                                LayerNumber = 0,
                                DirectorySeperator = Path.DirectorySeparatorChar,
                                RootPath = Session.Values.SessionFolder
                            }
                        }
                    }
                };
                Session.Values.ActiveScene = Session.Values.Scenes.First();
                Session.Save(false);
            }

            try { StreamDeckStatics.InitStreamDeck();} // If a StreamDeck isn't connected, don't crash
            //catch (Exception ex) { throw ex; }
            catch { }

            //don't await this call, this call is supposed to be "fire and forget"
            //Use the HostsCalculated-Method to get notified once compiling is done
            //In combination use Calculated to check if it's already calculated
            ScriptEngine.HostsCalculated += ScriptsCalculated;
            ScriptEngine.RunScripts();

            mapControl1.DmPictureBox = drawPbMap;
            mapControl1.Init();
            mapControl1.UpdatePrePlaceFogOfWarList += UpdatePrePlaceFogOfWarList;

            treeViewDisplay1.Editor = editor1;
            treeViewDisplay1.Init();

            mapControl1.LoadScene(Session.Values.ActiveScene, 0);

            UpdatePrePlaceFogOfWarList();
        }

        void ScriptsCalculated()
        {
            foreach (var item in ScriptEngine.CalculatedHosts)
            {
                if (item.Config.isUI) tabControl1.TabPages.Add(item.Page);
            }

            tabControl1.Update();
            this.Update();

            StreamDeckStatics.SwitchDeckState();
        }

        void UpdatePrePlaceFogOfWarList()
        {
            if (StreamDeckStatics.IsInitialized)
            {
                StreamDeckStatics.StateDescrptions.Single(m => m.State == "Fog of War").PageingActions.Clear();

                Session.Values.ActiveLayer.FogOfWar.Where(n => n.IsToggleFog).Select(f => (f.Name, f))
                        .ToList()
                        .ForEach(n => StreamDeckStatics.StateDescrptions.Single(m => m.State == "Fog of War").PageingActions.Add(
                            (
                            n.Name,
                            new Action(
                                () =>
                                {
                                    n.f.IsHidden = !n.f.IsHidden;

                                    var layer = Session.Values.ActiveLayer;

                                    mapControl1.dmImage = n.f.DrawFogOfWarComplete(Session.UpdatePath(), layer.FogOfWar, Settings.Values.DmColor, false);
                                    mapControl1.playerImage = n.f.DrawFogOfWarComplete(Session.UpdatePath(), layer.FogOfWar, Settings.Values.PlayerColor, true);

                                    Thread.Sleep(100);

                                    mapControl1.ShowImages(Settings.Values.DisplayChangesInstantly);
                                }
                                )
                            )
                            ));
                StreamDeckStatics.SwitchDeckState();
            }

            flowLayoutPanel1.Controls.Clear();

            var fogs = Session.Values.ActiveScene.GetLayer(Session.Values.ActiveLayerNumber).FogOfWar.Where(n => n.IsToggleFog == true).OrderBy(n => n.Name).ToList();

            for (int i = 0; i < fogs.Count; i++)
            {
                var btn = new Button
                {
                    Text = fogs[i].Name,
                    Size = new Size(50, 50),
                    Margin = new Padding(0, 0, 0, 0),
                    Tag = fogs[i],
                };

                btn.Click += (sender, args) =>
                {
                    var fog = ((Button)sender).Tag as FogOfWar;
                    fog.IsHidden = !fog.IsHidden;

                    mapControl1.dmImage = fog.DrawFogOfWarComplete(Session.UpdatePath(), Session.Values.ActiveScene.GetLayer(Session.Values.ActiveLayerNumber).FogOfWar, Settings.Values.DmColor, false);
                    mapControl1.playerImage = fog.DrawFogOfWarComplete(Session.UpdatePath(), Session.Values.ActiveScene.GetLayer(Session.Values.ActiveLayerNumber).FogOfWar, Settings.Values.PlayerColor, true);

                    Thread.Sleep(100);

                    mapControl1.ShowImages(Settings.Values.DisplayChangesInstantly);

                    if (Settings.Values.AutoSaveAction)
                        Session.Save(true);

                    UpdatePrePlaceFogOfWarList();
                };

                flowLayoutPanel1.Controls.Add(btn);
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            Session.Save(true);
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            using (var config = new Config())
            {
                config.ShowDialog();
            }
        }


    }
}
