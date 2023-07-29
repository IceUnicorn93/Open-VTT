using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using OpenVTT.Common;

namespace OpenVTT.Settings
{
    [Documentation("To use this Object use Settings.Settings.XYZ = ABC;")]
    public class Settings
    {
        [Documentation("Size of the Player Screen, if it's 27\" enter 27")]
        public int PlayerScreenSize; // How many Inches has my Monitor eg. 27
        [Documentation("Calcualted, How many Grid Lines left to right to draw")]
        public double PlayerScreenWidthInches; // How many lines would I need to draw from Left to right
        [Documentation("Calcualted, How many Grid Lines top to bottom to draw")]
        public double PlayerScreenHeightInces; // How many lines would I need to draw from top to bottom

        [Documentation("List of Screen-Information-Objects")]
        public List<ScreenInformation> Screens; // List of configured Screens

        [Documentation("AutoSave-State, true = Automatic Saving")]
        public bool AutoSaveAction = true;
        [Documentation("Display Fog of War changes directly? If yes input true")]
        public bool DisplayChangesInstantly;
        [Documentation("Display Grid?")]
        public bool DisplayGrid;
        [Documentation("Display Grid on DM Side? Only works if DisplayGrid == true")]
        public bool DisplayGridForDM = false;

        [Documentation("ServerIP of the Notes Server")]
        public string NoteServerIP; // IP Adress of Server
        [Documentation("ServerPort of the Notes Server")]
        public int NoteServerPort; // Port of Server

        internal Color DmColor = Color.FromArgb(150, 0, 0, 0);
        internal Color PlayerColor = Color.FromArgb(255, 0, 0, 0);
        internal Color GridColor = Color.FromKnownColor(KnownColor.Gray);
        internal Color TextColor = Color.FromArgb(255, 0, 0, 255);

        private XmlColor _XmlDmColor = new XmlColor { Alpha = 150, Blue = 0, Green = 0, Red = 0 };
        private XmlColor _XmlPlayerColor = new XmlColor { Alpha = 255, Blue = 0, Green = 0, Red = 0 };
        private XmlColor _XmlGridColor = new XmlColor { Alpha = 255, Blue = 128, Green = 128, Red = 128 };
        private XmlColor _XmlTextColor = new XmlColor { Alpha = 255, Blue = 0, Green = 0, Red = 255 };

        [Documentation("XmlColor for the DM Fog of War")]
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

        [Documentation("XmlColor for the Player Fog of War")]
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

        [Documentation("XmlColor for the Grid")]
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

        [Documentation("XmlColor for the Text of pre-placed Fog of War")]
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


        [Documentation("Static Settings Object")]
        public static Settings Values;

        [Documentation("Constructor")]
        static Settings()
        {
            Values = new Settings();
            Values.Screens = new List<ScreenInformation>();

            Load();
        }

        [Documentation("Save Method")]
        public static void Save()
        {
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

        [Documentation("Load Method")]
        public static void Load()
        {
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
