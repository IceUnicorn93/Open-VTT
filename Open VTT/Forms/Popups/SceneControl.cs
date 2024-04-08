using OpenVTT.Common;
using OpenVTT.Controls.Displayer;
using OpenVTT.FogOfWar;
using OpenVTT.Logging;
using OpenVTT.Scripting;
using OpenVTT.Session;
using OpenVTT.Settings;
using OpenVTT.StreamDeck;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Open_VTT.Forms.Popups
{
    internal partial class SceneControl : Form
    {
        public SceneControl()
        {
            Logger.Log("Class: SceneControl | Constructor");

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
                }
                else
                {
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
                    if(WindowInstaces.AnimatedMapDisplayer.GetImage() != null)
                        pbPoint.DrawCircle(WindowInstaces.AnimatedMapDisplayer.GetImage());

                    mapControl1.ShowImages(drawPbMap.Image, WindowInstaces.AnimatedMapDisplayer.GetImage(), true);
                }
                else
                {

                    if (Session.Values.ActiveLayer.IsImageLayer)
                    {
                        mapControl1.ShowImages(
                                    FogOfWar.DrawFogOfWarComplete(Session.UpdatePath(), Session.Values.ActiveLayer.FogOfWar, Settings.Values.DmColor, false),
                                    FogOfWar.DrawFogOfWarComplete(Session.UpdatePath(), Session.Values.ActiveLayer.FogOfWar, Settings.Values.PlayerColor, true),
                                    true); 
                    }
                    else
                    {
                        mapControl1.ShowImages(
                                    FogOfWar.DrawFogOfWarComplete(Session.UpdateVideoPath(Session.Values.ActiveLayer, true), Session.Values.ActiveLayer.FogOfWar, Settings.Values.DmColor, false),
                                    FogOfWar.DrawFogOfWarComplete(Path.Combine(Application.StartupPath, "Greenscreen.png"), Session.Values.ActiveLayer.FogOfWar, Settings.Values.PlayerColor, true),
                                    true);
                    }

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
                    if(layer.IsImageLayer)
                    {
                        mapControl1.dmImage = FogOfWar.DrawFogOfWarComplete(Session.UpdatePath(), layer.FogOfWar, Settings.Values.DmColor, false);
                        mapControl1.playerImage = FogOfWar.DrawFogOfWarComplete(Session.UpdatePath(), layer.FogOfWar, Settings.Values.PlayerColor, true);
                    }
                    else
                    {
                        mapControl1.dmImage = FogOfWar.DrawFogOfWarComplete(Session.UpdateVideoPath(layer, true), layer.FogOfWar, Settings.Values.DmColor, false);
                        mapControl1.playerImage = FogOfWar.DrawFogOfWarComplete(Path.Combine(Application.StartupPath, "Greenscreen.png"), layer.FogOfWar, Settings.Values.PlayerColor, true);
                    }

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
                    if (layer.IsImageLayer)
                    {
                        mapControl1.dmImage = FogOfWar.DrawFogOfWarComplete(Session.UpdatePath(), layer.FogOfWar, Settings.Values.DmColor, false);
                        mapControl1.playerImage = FogOfWar.DrawFogOfWarComplete(Session.UpdatePath(), layer.FogOfWar, Settings.Values.PlayerColor, true); 
                    }
                    else
                    {
                        mapControl1.dmImage = FogOfWar.DrawFogOfWarComplete(Session.UpdateVideoPath(layer, true), layer.FogOfWar, Settings.Values.DmColor, false);
                        mapControl1.playerImage = FogOfWar.DrawFogOfWarComplete(Path.Combine(Application.StartupPath, "Greenscreen.png"), layer.FogOfWar, Settings.Values.PlayerColor, true);
                    }

                    Thread.Sleep(100);

                    mapControl1.ShowImages(Settings.Values.DisplayChangesInstantly);

                }).Start();

                UpdatePrePlaceFogOfWarList();

                if (Settings.Values.AutoSaveAction)
                    Session.Save(true);
            };

            FormClosed += FormClosedEvent;

            Init();
        }

        private void FormClosedEvent(object sender, FormClosedEventArgs e)
        {
            Logger.Log("Class: SceneControl | FormClosedEvent");

            StreamDeckStatics.Dispose();
        }

        void Init()
        {
            Logger.Log("Class: SceneControl | Init");

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

            mapControl1.LoadScene(Session.Values.ActiveScene, 0);

            treeViewDisplay2.PageControl = panel2;

            UpdatePrePlaceFogOfWarList();
        }

        void ScriptsCalculated()
        {
            Logger.Log("Class: SceneControl | ScriptsCalculated");

            foreach (var item in ScriptEngine.CalculatedHosts)
            {
                if (item.Config.isUI && item.hasSuccessfullyRun && !tabControl1.TabPages.ContainsKey(item.Config.Name))
                    tabControl1.TabPages.Add(item.Page);
            }

            tabControl1.Update();
            this.Update();

            StreamDeckStatics.LoadConfig();

            StreamDeckStatics.SwitchDeckState();
        }

        void UpdatePrePlaceFogOfWarList()
        {
            Logger.Log("Class: SceneControl | UpdatePrePlaceFogOfWarList");

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

                                    if (layer.IsImageLayer)
                                    {
                                        mapControl1.dmImage = FogOfWar.DrawFogOfWarComplete(Session.UpdatePath(), layer.FogOfWar, Settings.Values.DmColor, false);
                                        mapControl1.playerImage = FogOfWar.DrawFogOfWarComplete(Session.UpdatePath(), layer.FogOfWar, Settings.Values.PlayerColor, true); 
                                    }
                                    else
                                    {
                                        mapControl1.dmImage = FogOfWar.DrawFogOfWarComplete(Session.UpdateVideoPath(layer, true), layer.FogOfWar, Settings.Values.DmColor, false);
                                        mapControl1.playerImage = FogOfWar.DrawFogOfWarComplete(Path.Combine(Application.StartupPath, "Greenscreen.png"), layer.FogOfWar, Settings.Values.PlayerColor, true);
                                    }

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

                    if (Session.Values.ActiveLayer.IsImageLayer)
                    {
                        mapControl1.dmImage = FogOfWar.DrawFogOfWarComplete(Session.UpdatePath(), Session.Values.ActiveLayer.FogOfWar, Settings.Values.DmColor, false);
                        mapControl1.playerImage = FogOfWar.DrawFogOfWarComplete(Session.UpdatePath(), Session.Values.ActiveLayer.FogOfWar, Settings.Values.PlayerColor, true); 
                    }
                    else
                    {
                        mapControl1.dmImage = FogOfWar.DrawFogOfWarComplete(Session.UpdateVideoPath(Session.Values.ActiveLayer, true), Session.Values.ActiveLayer.FogOfWar, Settings.Values.DmColor, false);
                        mapControl1.playerImage = FogOfWar.DrawFogOfWarComplete(Path.Combine(Application.StartupPath, "Greenscreen.png"), Session.Values.ActiveLayer.FogOfWar, Settings.Values.PlayerColor, true);
                    }

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
            Logger.Log("Class: SceneControl | btnSave_Click");

            Session.Save(true);
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: SceneControl | btnConfig_Click");

            using (var config = new Config())
            {
                config.ShowDialog();
            }
        }


    }
}
