using Open_VTT.Classes;
using Open_VTT.Classes.Scenes;
using Open_VTT.Other;
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
        Image InformationArtwork;

        public SceneControl()
        {
            InitializeComponent();
            var mainScreen = Screen.AllScreens.Single(n => n.Primary);
            this.Location = new Point(mainScreen.Bounds.X, mainScreen.Bounds.Y);
            this.WindowState = FormWindowState.Maximized;

            var drawPbArtwork = WindowInstaces.InformationDisplayDM.GetPictureBox();
            drawPbArtwork.DrawMode = PictureBoxMode.Ping;

            bool canPingArtwork = true;
            drawPbArtwork.PointComplete += (Point p) =>
            {
                if (p.X > -1 && p.Y > -1)
                {
                    if (canPingArtwork)
                    {
                        canPingArtwork = false;
                    }
                    else
                    {
                        return;
                    }

                    var pbPoint = new FogOfWar
                    {
                        Position = new Point(p.X, p.Y),
                        BoxSize = new Size(drawPbArtwork.Width, drawPbArtwork.Height),
                        DrawSize = new Size(100, 100),
                        state = FogState.Add
                    };

                    if (treeViewDisplay1.currentInformationItem == null) return;
                    var path = Path.Combine(treeViewDisplay1.currentInformationItem.GetLocation(".png").ToArray());
                    if (File.Exists(path))
                    {
                        InformationArtwork?.Dispose();
                        InformationArtwork = null;
                        InformationArtwork = Image.FromFile(path);

                        pbPoint.DrawCircle(InformationArtwork);
                        drawPbArtwork.Image?.Dispose();
                        drawPbArtwork.Image = null;
                        drawPbArtwork.Image = InformationArtwork;

                        WindowInstaces.InformationDisplayPlayer.GetPictureBox().Image?.Dispose();
                        WindowInstaces.InformationDisplayPlayer.GetPictureBox().Image = null;
                        WindowInstaces.InformationDisplayPlayer.GetPictureBox().Image = InformationArtwork;
                    }
                }
                else
                {
                    if (treeViewDisplay1.currentInformationItem == null) return;
                    var path = Path.Combine(treeViewDisplay1.currentInformationItem.GetLocation(".png").ToArray());
                    if (File.Exists(path))
                    {
                        InformationArtwork?.Dispose();
                        InformationArtwork = null;
                        

                        drawPbArtwork.Image.Dispose();
                        drawPbArtwork.Image = null;
                        WindowInstaces.InformationDisplayPlayer.GetPictureBox().Image?.Dispose();
                        WindowInstaces.InformationDisplayPlayer.GetPictureBox().Image = null;

                        InformationArtwork = Image.FromFile(path);

                        drawPbArtwork.Image = InformationArtwork;
                        WindowInstaces.InformationDisplayPlayer.GetPictureBox().Image = InformationArtwork;
                    }

                    canPingArtwork = true;
                }
            };

            drawPbMap.RectangleComplete += (Rectangle r) =>
            {
                if (Session.GetLayer(Session.Values.ActiveLayer).FogOfWar.Count == 0)
                    return;

                var fog = new FogOfWar()
                {
                    Position = new Point(r.X, r.Y),
                    BoxSize = new Size(drawPbMap.Width, drawPbMap.Height),
                    DrawSize = new Size(r.Width, r.Height),
                    state = FogState.Remove
                };

                var layer = Session.GetLayer(Session.Values.ActiveLayer);
                layer.FogOfWar.Add(fog);

                new Task(() =>
                {
                    mapControl1.dmImage = fog.DrawFogOfWarComplete(Session.UpdatePath(), layer.FogOfWar, Color.FromArgb(150, 0, 0, 0));
                    mapControl1.playerImage = fog.DrawFogOfWarComplete(Session.UpdatePath(), layer.FogOfWar, Color.FromArgb(255, 0, 0, 0));

                    Thread.Sleep(100);

                    mapControl1.ShowImages(Settings.Values.DisplayChangesInstantly);
                }).Start();

                if (Settings.Values.AutoSaveAction)
                    Session.Save(true);
            };

            Image backupImageForMapDM = null;
            Image backupImageForMapPlayer = null;
            bool canPing = true;
            drawPbMap.PointComplete += (Point p) =>
            {
                if (p.X > -1 && p.Y > -1)
                {
                    if (canPing)
                    {
                        canPing = false;
                    }
                    else
                    {
                        return;
                    }

                    var pbPoint = new FogOfWar
                    {
                        Position = new Point(p.X, p.Y),
                        BoxSize = new Size(drawPbMap.Width, drawPbMap.Height),
                        DrawSize = new Size(100, 100),
                        state = FogState.Add
                    };
                    drawPbMap.Image.Save(Path.Combine(Application.StartupPath,"dm.png"));
                    WindowInstaces.Player.GetPictureBox().Image?.Save(Path.Combine(Application.StartupPath, "player.png"));

                    backupImageForMapDM?.Dispose();
                    backupImageForMapDM = null;
                    backupImageForMapDM = Image.FromFile(Path.Combine(Application.StartupPath, "dm.png"));
                    backupImageForMapPlayer?.Dispose();
                    backupImageForMapPlayer = null;
                    backupImageForMapPlayer = Image.FromFile(Path.Combine(Application.StartupPath, "player.png"));

                    pbPoint.DrawCircle(backupImageForMapDM);
                    pbPoint.DrawCircle(backupImageForMapPlayer);

                    mapControl1.ShowImages(backupImageForMapDM, backupImageForMapPlayer, true);
                }
                else
                {
                    mapControl1.ShowImages(true);

                    backupImageForMapDM?.Dispose();
                    backupImageForMapDM = null;
                    backupImageForMapPlayer?.Dispose();
                    backupImageForMapPlayer = null;

                    if(File.Exists(Path.Combine(Application.StartupPath, "dm.png"))) File.Delete(Path.Combine(Application.StartupPath, "dm.png"));
                    if (File.Exists(Path.Combine(Application.StartupPath, "player.png"))) File.Delete(Path.Combine(Application.StartupPath, "player.png"));

                    canPing = true;
                }
            };

            drawPbMap.PoligonComplete += (Point[] p) =>
            {
                if (Session.GetLayer(Session.Values.ActiveLayer).FogOfWar.Count == 0)
                    return;

                var fog = new FogOfWar()
                {
                    Position = new Point(0, 0),
                    BoxSize = new Size(drawPbMap.Width, drawPbMap.Height),
                    DrawSize = new Size(1, 1),
                    state = FogState.Remove,
                    PoligonData = p.ToList()
                };

                var layer = Session.GetLayer(Session.Values.ActiveLayer);
                layer.FogOfWar.Add(fog);

                new Task(() =>
                {
                    mapControl1.dmImage = fog.DrawFogOfWarComplete(Session.UpdatePath(), layer.FogOfWar, Color.FromArgb(150, 0, 0, 0));
                    mapControl1.playerImage = fog.DrawFogOfWarComplete(Session.UpdatePath(), layer.FogOfWar, Color.FromArgb(255, 0, 0, 0));

                    Thread.Sleep(100);

                    mapControl1.ShowImages(Settings.Values.DisplayChangesInstantly);

                }).Start();

                if (Settings.Values.AutoSaveAction)
                    Session.Save(true);
            };

            Init();
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
            }

            mapControl1.DmPictureBox = drawPbMap;
            mapControl1.Init();

            treeViewDisplay1.Editor = editor1;
            treeViewDisplay1.Init();

            mapControl1.LoadScene(Session.Values.ActiveScene, 0);
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            Session.Save(true);
        }

        private void btnConfig_Click(object sender, System.EventArgs e)
        {
            using (var config = new Config())
            {
                config.ShowDialog();
            }
        }
    }
}
