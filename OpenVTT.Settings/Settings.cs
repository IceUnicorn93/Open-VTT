using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using OpenVTT.Common;
using OpenVTT.Logging;

namespace OpenVTT.Settings
{
    [Documentation("To use this Object use Settings.Settings.XYZ = ABC;", Name = "Settings")]
    public class Settings
    {
        [Documentation("Size of the Player Screen, if it's 27\" enter 27", Name = "PlayerScreenSize", IsField = true, DataType = "int")]
        public int PlayerScreenSize; // How many Inches has my Monitor eg. 27
        [Documentation("Calcualted, How many Grid Lines left to right to draw", Name = "PlayerScreenWidthInches", IsField = true, DataType = "double")]
        public double PlayerScreenWidthInches; // How many lines would I need to draw from Left to right
        [Documentation("Calcualted, How many Grid Lines top to bottom to draw", Name = "PlayerScreenHeightInces", IsField = true, DataType = "double")]
        public double PlayerScreenHeightInces; // How many lines would I need to draw from top to bottom

        [Documentation("List of Screen-Information-Objects", Name = "Screens", IsField = true, DataType = "List<ScreenInformation>")]
        public List<ScreenInformation> Screens; // List of configured Screens

        [Documentation("AutoSave-State, true = Automatic Saving", Name = "AutoSaveAction", IsField = true, DataType = "bool")]
        public bool AutoSaveAction = true;
        [Documentation("Display Fog of War changes directly? If yes input true", Name = "DisplayChangesInstantly", IsField = true, DataType = "bool")]
        public bool DisplayChangesInstantly;
        [Documentation("Display Grid?", Name = "DisplayGrid", IsField = true, DataType = "bool")]
        public bool DisplayGrid;
        [Documentation("Display Grid on DM Side? Only works if DisplayGrid == true", Name = "DisplayGridForDM", IsField = true, DataType = "bool")]
        public bool DisplayGridForDM = false;

        [Documentation("ServerIP of the Notes Server", Name = "NoteServerIP", IsField = true, DataType = "string")]
        public string NoteServerIP; // IP Adress of Server
        [Documentation("ServerPort of the Notes Server", Name = "NoteServerPort", IsField = true, DataType = "int")]
        public int NoteServerPort; // Port of Server

        internal Color DmColor = Color.FromArgb(150, 0, 0, 0);
        internal Color PlayerColor = Color.FromArgb(255, 0, 0, 0);
        internal Color GridColor = Color.FromKnownColor(KnownColor.Gray);
        internal Color TextColor = Color.FromArgb(255, 0, 0, 255);

        private XmlColor _XmlDmColor = new XmlColor { Alpha = 150, Blue = 0, Green = 0, Red = 0 };
        private XmlColor _XmlPlayerColor = new XmlColor { Alpha = 255, Blue = 0, Green = 0, Red = 0 };
        private XmlColor _XmlGridColor = new XmlColor { Alpha = 255, Blue = 128, Green = 128, Red = 128 };
        private XmlColor _XmlTextColor = new XmlColor { Alpha = 255, Blue = 0, Green = 0, Red = 255 };

        [Documentation("XmlColor for the DM Fog of War", Name = "XmlDmColor", IsProperty = true, DataType = "XmlColor")]
        public XmlColor XmlDmColor
        {
            get
            {
                return _XmlDmColor;
            }
            set
            {
                _XmlDmColor = value;
                DmColor = Color.FromArgb(_XmlDmColor.Alpha, _XmlDmColor.Red, _XmlDmColor.Green, _XmlDmColor.Blue);
            }
        }

        [Documentation("XmlColor for the Player Fog of War", Name = "XmlPlayerColor", IsProperty = true, DataType = "XmlColor")]
        public XmlColor XmlPlayerColor
        {
            get
            {
                return _XmlPlayerColor;
            }
            set
            {
                _XmlPlayerColor = value;
                PlayerColor = Color.FromArgb(_XmlPlayerColor.Alpha, _XmlPlayerColor.Red, _XmlPlayerColor.Green, _XmlPlayerColor.Blue);
            }
        }

        [Documentation("XmlColor for the Grid", Name = "XmlGridColor", IsProperty = true, DataType = "XmlColor")]
        public XmlColor XmlGridColor
        {
            get
            {
                return _XmlGridColor;
            }
            set
            {
                _XmlGridColor = value;
                GridColor = Color.FromArgb(_XmlGridColor.Alpha, _XmlGridColor.Red, _XmlGridColor.Green, _XmlGridColor.Blue);
            }
        }

        [Documentation("XmlColor for the Text of pre-placed Fog of War", Name = "XmlTextColor", IsProperty = true, DataType = "XmlColor")]
        public XmlColor XmlTextColor
        {
            get
            {
                return _XmlTextColor;
            }
            set
            {
                _XmlTextColor = value;
                TextColor = Color.FromArgb(_XmlTextColor.Alpha, _XmlTextColor.Red, _XmlTextColor.Green, _XmlTextColor.Blue);
            }
        }


        [Documentation("Static Settings Object", Name = "Values", IsField = true, DataType = "Settings")]
        public static Settings Values;

        [Documentation("Constructor", Name = "Settings", IsMethod = true, ReturnType = "Settings")]
        static Settings()
        {
            Logger.Log("Class: Settings | Constructor");

            Values = new Settings();
            Values.Screens = new List<ScreenInformation>();

            Load();
        }

        [Documentation("Save Method", Name = "Save", IsMethod = true, ReturnType = "void")]
        public static void Save()
        {
            Logger.Log("Class: Settings | Save");

            var playerScreen = Values.Screens?.SingleOrDefault(n => n.Display == DisplayType.Player);
            if (playerScreen != null && playerScreen.Height > 0 && playerScreen.Width > 0 && Values.PlayerScreenSize > 0)
            {
                var screenDiagonalPixel = Math.Sqrt(Math.Pow(playerScreen.Height, 2) + Math.Pow(playerScreen.Width, 2));
                var ratio = screenDiagonalPixel / Values.PlayerScreenSize;
                Values.PlayerScreenWidthInches = playerScreen.Width / ratio;
                Values.PlayerScreenHeightInces = playerScreen.Height / ratio;
            }

            Values.XmlDmColor = new XmlColor { Alpha = Values.DmColor.A, Blue = Values.DmColor.B, Green = Values.DmColor.G, Red = Values.DmColor.R };
            Values.XmlPlayerColor = new XmlColor { Alpha = Values.PlayerColor.A, Blue = Values.PlayerColor.B, Green = Values.PlayerColor.G, Red = Values.PlayerColor.R };
            Values.XmlGridColor = new XmlColor { Alpha = Values.GridColor.A, Blue = Values.GridColor.B, Green = Values.GridColor.G, Red = Values.GridColor.R };
            Values.XmlTextColor = new XmlColor { Alpha = Values.TextColor.A, Blue = Values.TextColor.B, Green = Values.TextColor.G, Red = Values.TextColor.R };


            var x = new XmlSerializer(typeof(Settings));
            using (var sw = new StreamWriter(Path.Combine(Application.StartupPath, "Settings.xml")))
            {
                x.Serialize(sw, Values);
            }
        }

        [Documentation("Load Method", Name = "Load", IsMethod = true, ReturnType = "void")]
        public static void Load()
        {
            Logger.Log("Class: Settings | Load");

            if (!File.Exists(Path.Combine(Application.StartupPath, "Settings.xml")))
                return;

            var x = new XmlSerializer(typeof(Settings));
            using (var sr = new StreamReader(Path.Combine(Application.StartupPath, "Settings.xml")))
            {
                Values = (Settings)x.Deserialize(sr);
            }
        }
    }
}
