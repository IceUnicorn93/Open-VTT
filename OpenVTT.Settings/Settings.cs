using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using OpenVTT.Common;

namespace OpenVTT.Settings
{
    public class Settings
    {
        public int PlayerScreenSize; // How many Inches has my Monitor eg. 27
        public double PlayerScreenWidthInches; // How many lines would I need to draw from Left to right
        public double PlayerScreenHeightInces; // How many lines would I need to draw from top to bottom

        public List<ScreenInformation> Screens; // List of configured Screens

        public bool AutoSaveAction = true;
        public bool DisplayChangesInstantly;
        public bool DisplayGrid;

        public string NoteServerIP; // IP Adress of Server
        public int NoteServerPort; // Port of Server

        public static Settings Values;

        static Settings()
        {
            Values = new Settings();
            Values.Screens = new List<ScreenInformation>();

            Load();
        }

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

            var x = new XmlSerializer(typeof(Settings));
            using (var sw = new StreamWriter(Path.Combine(Application.StartupPath, "Settings.xml")))
            {
                x.Serialize(sw, Values);
            }
        }

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
