using OpenVTT.Common;
using OpenMacroBoard.SDK;
using StreamDeckSharp;
using System;
using System.Drawing;
using System.Linq;
using OpenVTT.Session;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace OpenVTT.StreamDeck
{

    internal static class StreamDeckStatics
    {
        public static ObservableCollection<(string State, Action action)> States = new ObservableCollection<(string State, Action action)>();
        static string State;
        static IStreamDeckBoard deck;
        static int Page = 1;
        static int MaxPage = 1;

        static Action[,] actions;

        public static Action<Scene, int> LoadScene;
        public static Action<FogOfWar.FogOfWar> LoadFog;

        public static bool IsInitialized = false;

        static internal void Dispose()
        {
            if (deck != null) deck.Dispose();
        }

        static internal void InitStreamDeck()
        {
            States.Clear();
            States.Add(("SelectControl", () => { SetSelection(); }));
            States.Add(("Scene", () => { SetMaps(); }));
            States.Add(("Fog Of War", () => { SetFog(); }));

            States.CollectionChanged += (sender, e) =>
            {
                SetSelection();
            };

            State = "SelectControl";

            //Open the Stream Deck device

            deck = StreamDeckSharp.StreamDeck.OpenDevice();
            deck.SetBrightness(100);
            deck.KeyStateChanged += Deck_KeyPressed;

            // Set Action Array
            actions = new Action[deck.Keys.KeyCountX, deck.Keys.KeyCountY];

            // Set Fog Buttons
            SetFogButtons();

            // Set Control Button
            SetDeckKeyText(0, deck.Keys.KeyCountY - 1, "Control");

            Page = 1;
            SwitchDeckState();

            //Display <- 1 -> in the Bottom Line
            SetPageButtons();

            // -> Switch to Selection
            SetAction((0, deck.Keys.KeyCountY - 1), () =>
            {
                State = "SelectControl";
                SetSelection();
            });

            IsInitialized = true;
        }

        static internal void SetFogButtons()
        {
            SetDeckKeyText(0, 0, $"Layer{Environment.NewLine}  Up");
            SetDeckKeyText(0, 1, $"Layer{Environment.NewLine}Down");

            SetDeckKeyText(1, 0, $"Reveal{Environment.NewLine}    all");
            SetDeckKeyText(1, 1, $"Cover{Environment.NewLine}   all");
            SetDeckKeyText(1, 2, $" Set {Environment.NewLine}Active");
        }

        static internal void SetPageButtons()
        {
            // -> (Page Reset)
            var pos = deck.Keys.KeyCountX - ((deck.Keys.KeyCountX - 1) - 2);
            SetDeckKeyText(pos, deck.Keys.KeyCountY - 1, Page.ToString());
            actions[pos, deck.Keys.KeyCountY - 1] = new Action(() =>
            {
                Page = 1;
                SetDeckKeyText(pos, deck.Keys.KeyCountY - 1, Page.ToString());
                SwitchDeckState();
            });

            // <- (Page Decrement)
            SetDeckKeyText(2, deck.Keys.KeyCountY - 1, "<-");
            actions[2, deck.Keys.KeyCountY - 1] = new Action(() =>
            {
                if (Page > 1) Page--;
                SetDeckKeyText(pos, deck.Keys.KeyCountY - 1, Page.ToString());
                SwitchDeckState();
            });
            // -> (Page Increment)
            SetDeckKeyText(deck.Keys.KeyCountX - 1, deck.Keys.KeyCountY - 1, "->");
            actions[deck.Keys.KeyCountX - 1, deck.Keys.KeyCountY - 1] = new Action(() =>
            {
                if (Page < MaxPage) Page++;
                SetDeckKeyText(pos, deck.Keys.KeyCountY - 1, Page.ToString());
                SwitchDeckState();
            });
        }

        static internal void SetMaxPage(int max)
        {
            MaxPage = max;
        }

        static internal void SetAction((int X, int Y) position, Action action)
        {
            actions[position.X, position.Y] = action;
        }

        static internal void SetDeckKeyText(int X, int Y, string Text)
        {
            var newX = X * 104;
            var newY = Y * 104;

            var key = deck.Keys.Single(k => k.X == newX && k.Y == newY);
            deck.SetKeyBitmap((Y * deck.Keys.KeyCountX) + X, CreateBitmap(Text));
        }

        static private KeyBitmap CreateBitmap(string Text)
        {
            var keyImage = new Bitmap(96, 96);

            if (!Text.Contains(Environment.NewLine))
                Text = string.Join(Environment.NewLine, Text.SplitInParts(7));

            using (Graphics g = Graphics.FromImage(keyImage))
            {
                var font = new Font("Arial", 20);
                var size = keyImage.Size;
                SizeF textSize = new SizeF();

                textSize = g.MeasureString(Text, font);

                //var fits = false;
                //while (fits == false)
                //{
                //    textSize = g.MeasureString(Text, font);
                //    if (textSize.Width > size.Width)
                //        font = new Font(font.FontFamily, font.Size - 1);
                //    else
                //        fits = true;
                //}


                g.DrawString(Text, font, new SolidBrush(Color.White),
                    keyImage.Width / 2 - textSize.Width / 2,
                    keyImage.Height / 2 - textSize.Height / 2);
            }

            return KeyBitmap.Create.FromBitmap(keyImage);
        }

        static private (int X, int Y) IdToLocation(int ID)
        {
            int X = ID % (deck.Keys.KeyCountX);
            int Y = (ID - X) / deck.Keys.KeyCountX;

            return (X, Y);
        }

        static private void Deck_KeyPressed(object sender, KeyEventArgs e)
        {
            if (!(sender is IStreamDeckBoard sd)) return;
            if (!e.IsDown) return;

            var data = IdToLocation(e.Key);

            var action = actions[data.X, data.Y];
            if (action != null) action();

            //SetDeckKeyText(data.X, data.Y, $"{data.X} - {data.Y}");
        }

        //------------------ All for Selecting the Deck State and displaying Opions

        static internal void SwitchDeckState(int PageNumber = -1)
        {
            if (PageNumber != -1)
            {
                Page = PageNumber;

                var pos = deck.Keys.KeyCountX - ((deck.Keys.KeyCountX - 1) - 2);
                SetDeckKeyText(pos, deck.Keys.KeyCountY - 1, Page.ToString());
            }

            var pair = States.Single(n => n.State == State);
            pair.action();
        }

        static internal void SetSelection()
        {
            var maxMapCount = (deck.Keys.KeyCountX - 2) * (deck.Keys.KeyCountY - 1);
            var maxX = (deck.Keys.KeyCountX - 2);
            var maxY = (deck.Keys.KeyCountY - 1);

            var states = new List<(string State, Action action)>();
            states.AddRange(States);
            states = states
                .Skip((Page - 1) * maxMapCount)
                .Take(maxMapCount)
                .Where(n => n.State != "SelectControl")
                .Select(n => (Regex.Replace(n.State, "(\\B[A-Z])", " $1"), n.action))
                .ToList();

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    var pos = y * maxX + x;
                    if (pos >= states.Count)
                    {
                        SetDeckKeyText(x + 2, y, "");
                        actions[x + 2, y] = null;
                    }
                    else
                    {
                        SetDeckKeyText(x + 2, y, states[pos].State);
                        var name = states[pos];
                        actions[x + 2, y] = new Action(() =>
                        {
                            State = states[pos].State;
                            SwitchDeckState();
                        });
                    }
                }
            }

            MaxPage = (int)Math.Ceiling((decimal)Session.Session.Values.ActiveScene.GetLayer(Session.Session.Values.ActiveLayer).FogOfWar.Count / ((deck.Keys.KeyCountX - 2) * (deck.Keys.KeyCountY - 1)));
        }

        static internal void SetMaps()
        {
            var maxMapCount = (deck.Keys.KeyCountX - 2) * (deck.Keys.KeyCountY - 1);
            var maxX = (deck.Keys.KeyCountX - 2);
            var maxY = (deck.Keys.KeyCountY - 1);

            var mapNames = Session.Session.Values.Scenes.Skip((Page - 1) * maxMapCount).Take(maxMapCount).Select(n => n.Name).ToList();

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    var pos = y * maxX + x;
                    if (pos >= mapNames.Count)
                    {
                        SetDeckKeyText(x + 2, y, "");
                        actions[x + 2, y] = null;
                    }
                    else
                    {
                        SetDeckKeyText(x + 2, y, mapNames[pos]);
                        var name = mapNames[pos];
                        actions[x + 2, y] = new Action(() =>
                        {
                            LoadScene(Session.Session.Values.Scenes.Single(n => n.Name == name), 0);
                        });
                    }
                }
            }

            MaxPage = (int)Math.Ceiling((decimal)Session.Session.Values.Scenes.Count() / ((deck.Keys.KeyCountX - 2) * (deck.Keys.KeyCountY - 1)));
        }

        static internal void SetFog()
        {


            var maxMapCount = (deck.Keys.KeyCountX - 2) * (deck.Keys.KeyCountY - 1);
            var maxX = (deck.Keys.KeyCountX - 2);
            var maxY = (deck.Keys.KeyCountY - 1);

            var fogNames =
                Session.Session.Values.ActiveScene.GetLayer(Session.Session.Values.ActiveLayer).FogOfWar
                .Where(n => n.Name != "")
                .OrderBy(n => n.Name)
                .Skip((Page - 1) * maxMapCount)
                .Take(maxMapCount)
                .Select(n => n.Name)
                .ToList();

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    var pos = y * maxX + x;
                    if (pos >= fogNames.Count)
                    {
                        SetDeckKeyText(x + 2, y, "");
                        actions[x + 2, y] = null;
                    }
                    else
                    {
                        SetDeckKeyText(x + 2, y, fogNames[pos]);
                        var name = fogNames[pos];
                        actions[x + 2, y] = new Action(() =>
                        {
                            LoadFog(Session.Session.Values.ActiveScene.GetLayer(Session.Session.Values.ActiveLayer).FogOfWar.Single(n => n.Name == name));
                        });
                    }
                }
            }

            MaxPage = (int)Math.Ceiling((decimal)Session.Session.Values.ActiveScene.GetLayer(Session.Session.Values.ActiveLayer).FogOfWar.Where(n => n.Name != "").ToList().Count / ((deck.Keys.KeyCountX - 2) * (deck.Keys.KeyCountY - 1)));
        }

        //------------------ Unsued functions, maybe one day ...

        // Maybe we need that?
        static private void CreateBitmapFromImagePath(int X, int Y, string Path)
        {
            var newX = X * 104;
            var newY = Y * 104;

            var key = deck.Keys.Single(k => k.X == newX && k.Y == newY);

            deck.SetKeyBitmap((Y * deck.Keys.KeyCountX) + X, KeyBitmap.Create.FromBitmap(new Bitmap(Path)));
        }
    }
}
