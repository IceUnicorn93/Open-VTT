using OpenVTT.Common;
using OpenMacroBoard.SDK;
using StreamDeckSharp;
using System;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;

namespace OpenVTT.StreamDeck
{
    [Documentation("To use this Object use StreamDeckStatics.XYZ = ABC;", Name = "StreamDeckStatics")]
    public static class StreamDeckStatics
    {
        [Documentation("List of Static Button-Actions, used in StateDescription.ActionDescription",
            IsStatic = true, IsField = true, Name = "ActionList",
            DataType = "List<(string Name, Action action)>")]
        public static List<(string Name, Action action)> ActionList = new List<(string Name, Action action)>();

        [Documentation("List of State Descriptions and PageingActions",
            IsStatic = true, IsField = true, Name = "StateDescrptions",
            DataType = "List<(string State, string[,] ActionDescription, List<(string DisplayName, Action action)> PageingActions)>")]
        public static
                List<(string State, string[,] ActionDescription, List<(string DisplayName, Action action)> PageingActions)> StateDescrptions =
            new List<(string State, string[,] ActionDescription, List<(string DisplayName, Action action)> PageingActions)>();

        [Documentation("Number of Buttons left to right",
            IsStatic = true, IsField = true, Name = "MaxX",
            DataType = "int")]
        public static int MaxX { get; internal set; }
        [Documentation("Number of Buttons top to bottom",
            IsStatic = true, IsField = true, Name = "MaxY",
            DataType = "int")]
        public static int MaxY { get; internal set; }

        static string CurrentState;
        static IStreamDeckBoard deck;
        static int Page = 1;
        static int MaxPage = 1;

        static Action[,] actions;

        static internal bool IsInitialized = false;

        static internal void Dispose()
        {
            if (!IsInitialized) return;

            deck.SetBrightness(0);
            IsInitialized = false;
            if (deck != null)
            {
                deck.Dispose();
                deck = null;
            }
        }

        static internal void InitStreamDeck()
        {
            //Open the Stream Deck device
            deck = StreamDeckSharp.StreamDeck.OpenDevice();
            deck.SetBrightness(100);
            deck.KeyStateChanged += Deck_KeyPressed;

            //Setting some Defaults
            CurrentState = "Select";
            Page = 1;
            MaxPage = 1;
            MaxX = deck.Keys.KeyCountX;
            MaxY = deck.Keys.KeyCountY;

            //Initialize requiered Arrays and Lists
            actions = new Action[MaxX, MaxY];
            ActionList.Clear();
            StateDescrptions.Clear();

            //Addding Pageing Functions
            ActionList.Add(("Previous Page", new Action(() =>
            {
                if (Page > 1)
                    Page -= 1;
                SwitchDeckState();
            })));
            ActionList.Add(("Next Page", new Action(() =>
            {
                if (Page < MaxPage)
                    Page += 1;
                SwitchDeckState();
            })));
            ActionList.Add(("Page 1", new Action(() =>
            {
                Page = 1;
                SwitchDeckState();
            })));
            ActionList.Add(("Select", new Action(() =>
            {
                CurrentState = "Select";
                SwitchDeckState(1);
            })));

            //Adding default States
            (string State, string[,] ActionDescription, List<(string Name, Action action)> PageingActions) SelectDescription = CreateDescription("Select");
            SelectDescription.ActionDescription[0, MaxY - 1] = "";
            (string State, string[,] ActionDescription, List<(string Name, Action action)> PageingActions) SceneDescription = CreateDescription("Scene");
            SceneDescription.ActionDescription[0, 0] = "Layer  Up";
            SceneDescription.ActionDescription[1, 0] = "Reveal All";
            SceneDescription.ActionDescription[0, 1] = "Layer  Down";
            SceneDescription.ActionDescription[1, 1] = "Cover  All";
            SceneDescription.ActionDescription[1, 2] = "Set    Active";
            (string State, string[,] ActionDescription, List<(string Name, Action action)> PageingActions) FogDescription = CreateDescription("Fog of War");
            FogDescription.ActionDescription[1, 2] = "Set    Active";

            IsInitialized = true;

            SwitchDeckState();
        }

        //------------------ All for Setting Buttons
        static private KeyBitmap CreateBitmap(string Text, Color color)
        {
            if (!IsInitialized) return null;

            var keyImage = new Bitmap(96, 96);

            if (!Text.Contains(Environment.NewLine))
                Text = string.Join(Environment.NewLine, Text.SplitInParts(7));

            using (Graphics g = Graphics.FromImage(keyImage))
            {
                g.FillRectangle(new SolidBrush(color), new Rectangle(0, 0, keyImage.Width, keyImage.Height));

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
            if (!IsInitialized) return (0, 0);

            int X = ID % (deck.Keys.KeyCountX);
            int Y = (ID - X) / deck.Keys.KeyCountX;

            return (X, Y);
        }

        static int offset = 0;
        static private void SetDeckKey((int x, int y) position, string Text, Color color, Action action = null)
        {
            if (!IsInitialized) return;

            actions[position.x, position.y] = action;

            if(offset == 0)
                offset = deck.Keys.ToList().Select(k => k.X).Distinct().OrderBy(k => k).Skip(1).First();

            var newX = position.x * offset;
            var newY = position.y * offset;

            //deck.Keys.ToList().ForEach(k => Debug.WriteLine($"X: {k.X} | Y: {k.Y} | Dist: "));

            var key = deck.Keys.Single(k => k.X == newX && k.Y == newY);
            deck.SetKeyBitmap((position.y * deck.Keys.KeyCountX) + position.x, CreateBitmap(Text, color));
        }

        static private void Deck_KeyPressed(object sender, KeyEventArgs e)
        {
            if (!IsInitialized) return;

            if (!(sender is IStreamDeckBoard sd)) return;
            if (!e.IsDown) return;

            var data = IdToLocation(e.Key);

            var action = actions[data.X, data.Y];
            if (action != null) action();
        }

        //------------------ All for Selecting the Deck State and displaying Opions
        [Documentation("Reloads the Current Deck-State (If PageNumber != -1 Page will be Set to Parameter)",
            IsStatic = true, IsMethod = true, Name = "SwitchDeckState", Parameters = "int PageNumber = -1",
            ReturnType = "void")]
        static public void SwitchDeckState(int PageNumber = -1)
        {
            if (!IsInitialized) return;

            if (PageNumber != -1)
                Page = PageNumber;

            var description = StateDescrptions.Single(n => n.State == CurrentState);
            var Paginglist = new List<(int x, int y)>();
            actions = new Action[MaxX, MaxY];

            //Static Fields
            for (int x = 0; x < MaxX; x++)
                for (int y = 0; y < MaxY; y++)
                    if (description.ActionDescription[x, y] != "Paging")
                    {
                        var text = "";
                        if (description.ActionDescription[x, y] == "Previous Page")
                            text = "<-";
                        else if (description.ActionDescription[x, y] == "Page 1")
                            text = $"{Page}";
                        else if (description.ActionDescription[x, y] == "Next Page")
                            text = "->";
                        else //Set Text based on description
                            text = description.ActionDescription[x, y];
                        //Set Action based on list and description
                        SetDeckKey((x, y), text, Color.FromArgb(255, 150,150,150), ActionList.SingleOrDefault(n => n.Name == description.ActionDescription[x, y]).action);
                    }
                    else // Clear Paging Pages
                    {
                        SetDeckKey((x, y), "", Color.FromArgb(0, 150, 150, 150));
                        Paginglist.Add((x, y));
                    }

            //Pageing Fields
            Paginglist = Paginglist.OrderBy(n => MaxX * n.y + n.x).ToList();

            var pageingCount = Paginglist.Count;
            var pagingAction = description.PageingActions;

            if (pagingAction == null) return;

            MaxPage = (int)Math.Ceiling((double)pagingAction.Count / pageingCount);

            var pagingAct = pagingAction.OrderBy(n => n.DisplayName).Skip((Page - 1) * pageingCount).Take(pageingCount).ToList();

            for (int i = 0; i < pagingAct.Count; i++)
                SetDeckKey(Paginglist[i], pagingAct[i].DisplayName, Color.FromArgb(0, 150, 150, 150), pagingAct[i].action);
        }
        [Documentation("Retuns an empty Description with Pageing and return to Select (Already Added to the StateDescriptions)",
            IsStatic = true, IsMethod = true, Name = "CreateDescription", Parameters = "string Name",
            ReturnType = "(string State, string[,] ActionDescription, List<(string DisplayName, Action action)> PageingActions)")]
        static public (string State, string[,] ActionDescription, List<(string DisplayName, Action action)> PageingActions) CreateDescription(string Name)
        {
            (string State, string[,] ActionDescription, List<(string DisplayName, Action action)> PageingActions) description =
                (Name, new string[MaxX, MaxY], new List<(string DisplayName, Action action)>());

            for (int x = 0; x < MaxX; x++)
                for (int y = 0; y < MaxY; y++)
                    description.ActionDescription[x, y] = "Paging";


            if (MaxX > 3)
            {
                description.ActionDescription[0, MaxY - 1] = "Select";
                for (int x = 1; x < MaxX - 3; x++)
                    description.ActionDescription[x, MaxY - 1] = "";
                description.ActionDescription[MaxX - 3, MaxY - 1] = "Previous Page";
                description.ActionDescription[MaxX - 2, MaxY - 1] = "Page 1";
                description.ActionDescription[MaxX - 1, MaxY - 1] = "Next Page";
            }
            else // it's a 2*3 StreamDeck
            {
                description.ActionDescription[MaxX - 3, MaxY - 1] = "Select";
                description.ActionDescription[MaxX - 2, MaxY - 1] = "Previous Page";
                description.ActionDescription[MaxX - 1, MaxY - 1] = "Next Page";
            }
            StateDescrptions.Add(description);

            if (StateDescrptions.Any(n => n.State == "Select") && Name != "Select")
                StateDescrptions.Single(n => n.State == "Select").PageingActions.Add(
                    (Name, new Action(() => { CurrentState = Name; SwitchDeckState(); })));

            return description;
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
