using Open_VTT.Classes;
using Open_VTT.Classes.Scenes;
using OpenMacroBoard.SDK;
using StreamDeckSharp;
using System;
using System.Drawing;
using System.Linq;

namespace Open_VTT.Other
{
    static class StreamDeckStatics
    {
        static IStreamDeckBoard deck;
        static int Page = 1;

        static Action[,] actions;

        public static Action<Scene, int> LoadScene;

        public static bool IsInitialized = false;

        static internal void Dispose()
        {
            if (deck != null) deck.Dispose();
        }

        static internal void InitStreamDeck()
        {
            //Open the Stream Deck device
            deck = StreamDeck.OpenDevice();
            deck.SetBrightness(100);
            deck.KeyStateChanged += Deck_KeyPressed;

            // Set Action Array
            actions = new Action[deck.Keys.KeyCountX, deck.Keys.KeyCountY];


            // Set Static Text
            SetDeckKeyText(0, 0, $"Layer{Environment.NewLine}  Up");
            SetDeckKeyText(0, 1, $"Layer{Environment.NewLine} Down");

            SetDeckKeyText(1, 0, $"Reveal{Environment.NewLine}  all");
            SetDeckKeyText(1, 1, $"Cover{Environment.NewLine}  all");
            SetDeckKeyText(1, 2, $" Set {Environment.NewLine}Active");

            SetDeckKeyText(2, deck.Keys.KeyCountY - 1, "<-");
            SetDeckKeyText(deck.Keys.KeyCountX - 1, deck.Keys.KeyCountY - 1, "->");

            var pos =  deck.Keys.KeyCountX - ((deck.Keys.KeyCountX - 1) - 2);
            SetDeckKeyText(pos, deck.Keys.KeyCountY - 1, Page.ToString());

            Page = 1;
            SetMaps();

            // <- (Page Decrement)
            actions[2, deck.Keys.KeyCountY - 1] = new Action(() =>
            {
                if(Page > 1) Page--;
                SetDeckKeyText(pos, deck.Keys.KeyCountY - 1, Page.ToString());
                SetMaps();
            });
            // -> (Page Increment)
            actions[deck.Keys.KeyCountX - 1, deck.Keys.KeyCountY - 1] = new Action(() =>
            {
                var maxPage = Math.Ceiling((decimal)Session.Values.Scenes.Count() / ((deck.Keys.KeyCountX - 2) * (deck.Keys.KeyCountY - 1)));
                
                if(Page < maxPage) Page++;
                SetDeckKeyText(pos, deck.Keys.KeyCountY - 1, Page.ToString());
                SetMaps();
            });
            // -> (Page Reset)
            actions[pos, deck.Keys.KeyCountY - 1] = new Action(() =>
            {
                Page = 1;
                SetDeckKeyText(pos, deck.Keys.KeyCountY - 1, Page.ToString());
                SetMaps();
            });

            IsInitialized = true;
        }

        static internal void SetAction((int X, int Y) position, Action action)
        {
            actions[position.X, position.Y] = action;
        }

        static private void SetDeckKeyText(int X, int Y, string Text)
        {
            var newX = X * 104;
            var newY = Y * 104;

            var key = deck.Keys.Single(k => k.X == newX && k.Y == newY);
            deck.SetKeyBitmap((Y * deck.Keys.KeyCountX) + X, CreateBitmap(Text));
        }

        static private KeyBitmap CreateBitmap(string Text)
        {
            var keyImage = new Bitmap(96, 96);

            if(!Text.Contains(Environment.NewLine))
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

        static private void Deck_KeyPressed(object sender, OpenMacroBoard.SDK.KeyEventArgs e)
        {
            if (!(sender is IStreamDeckBoard sd)) return;
            if (!e.IsDown) return;

            var data = IdToLocation(e.Key);

            var action = actions[data.X, data.Y];
            if (action != null) action();

            //SetDeckKeyText(data.X, data.Y, $"{data.X} - {data.Y}");
        }

        static internal void SetMaps()
        {
            var maxMapCount = (deck.Keys.KeyCountX - 2) * (deck.Keys.KeyCountY - 1);
            var maxX = (deck.Keys.KeyCountX - 2);
            var maxY = (deck.Keys.KeyCountY - 1);

            var mapNames = Session.Values.Scenes.Skip((Page-1) * maxMapCount).Take(maxMapCount).Select(n => n.Name).ToList();

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
                            LoadScene(Session.Values.Scenes.Single(n => n.Name == name), 0);
                        });
                    }
                }
            }
        }

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
