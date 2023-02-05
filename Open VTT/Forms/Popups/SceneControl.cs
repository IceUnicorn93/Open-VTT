﻿using Open_VTT.Classes;
using Open_VTT.Classes.Scenes;
using Open_VTT.Other;
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
                    pbPoint.DrawCircle(WindowInstaces.Player.GetPictureBox().Image);

                    mapControl1.ShowImages(drawPbMap.Image, WindowInstaces.Player.GetPictureBox().Image, true);
                }
                else
                {
                    var f = new FogOfWar();

                    drawPbMap.BackgroundImageLayout = ImageLayout.Zoom;
                    WindowInstaces.Player.GetPictureBox().BackgroundImageLayout = ImageLayout.Zoom;

                    drawPbMap.BackgroundImage = (Image)drawPbMap.Image.Clone();
                    WindowInstaces.Player.GetPictureBox().BackgroundImage = (Image)WindowInstaces.Player.GetPictureBox().Image.Clone();

                    drawPbMap.Image = null;
                    WindowInstaces.Player.GetPictureBox().Image = null;

                    mapControl1.ShowImages(
                    f.DrawFogOfWarComplete(Session.UpdatePath(), Session.GetLayer(Session.Values.ActiveLayer).FogOfWar, Session.Values.DmColor),
                    f.DrawFogOfWarComplete(Session.UpdatePath(), Session.GetLayer(Session.Values.ActiveLayer).FogOfWar, Session.Values.PlayerColor),
                    true);

                    drawPbMap.BackgroundImage = null;
                    WindowInstaces.Player.GetPictureBox().BackgroundImage = null;

                    GC.Collect();

                    canPing = true;
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
                    mapControl1.dmImage = fog.DrawFogOfWarComplete(Session.UpdatePath(), layer.FogOfWar, Session.Values.DmColor);
                    mapControl1.playerImage = fog.DrawFogOfWarComplete(Session.UpdatePath(), layer.FogOfWar, Session.Values.PlayerColor);

                    Thread.Sleep(100);

                    mapControl1.ShowImages(Settings.Values.DisplayChangesInstantly);
                }).Start();

                if (Settings.Values.AutoSaveAction)
                    Session.Save(true);
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
            }

            try { StreamDeckStatics.InitStreamDeck(); } // If a StreamDeck isn't connected, don't crash
            catch { }

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
