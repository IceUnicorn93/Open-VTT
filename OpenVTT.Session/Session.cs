using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using OpenVTT.Common;
using OpenVTT.Editor;

namespace OpenVTT.Session
{
    [Documentation("To use this Object, use Session.Session.Values.XYZ = ABC; or Session.Session.Method();")]
    public class Session
    {
        [XmlIgnore]
        [Documentation("Number of Active Layer")]
        public int ActiveLayer { get; internal set; }
        [XmlIgnore]
        [Documentation("Actual Active Scene-Object")]
        public Scene ActiveScene { get; internal set; }

        [Documentation("Path where the Session-Folder was created")]
        public string SessionFolder;
        [Documentation("List of Scene-Objects")]
        public List<Scene> Scenes;

        public Session()
        {

        }

        [Documentation("Static Session-Object")]
        public static Session Values;

        [Documentation("Static Constructor")]
        static Session()
        {
            Values = new Session();
        }



        internal static void Save(bool optimize)
        {
            string path = GetSubDirectoryPathForFile(Values.SessionFolder, "Session.xml");

            //Validate Fog of War
            //foreach (var s in Values.Scenes)
            //    foreach (var l in s.Layers)
            //        foreach (var f in l.FogOfWar)
            //            f.Validate();

            if (optimize)
            {
                //Remove Unused Layers from Scenes
                foreach (var s in Values.Scenes)
                {
                    s.Layers = s.Layers.OrderBy(n => n.LayerNumber).ToList();
                    var rLayers = s.Layers.Where(n => n.ImagePath == string.Empty && n.RootPath == string.Empty && n.LayerNumber != 0).ToList();
                    foreach (var l in rLayers)
                        s.Layers.Remove(l);
                }
            }


            var x = new XmlSerializer(typeof(Session));
            using (var sw = new StreamWriter(path))
            {
                x.Serialize(sw, Values);
            }
        }

        internal static void Load(string path = "")
        {
            if (path == "")
                path = GetSubDirectoryPathForFile(Values.SessionFolder, "Session.xml");

            var x = new XmlSerializer(typeof(Session));
            using (var sr = new StreamReader(path))
            {
                Values = (Session)x.Deserialize(sr);
            }
        }

        [Documentation("Returns the Layer-Object for the given Layer-Number")]
        public static Layer GetLayer(int number)
        {
            return Values.ActiveScene.Layers.SingleOrDefault(n => n.LayerNumber == number);
        }
        [Documentation("Sets the Layer-Number")]
        public static void SetLayer(int number)
        {
            Values.ActiveLayer = number;
        }
        [Documentation("Sets the Active-Scene to the given Scene")]
        public static void SetScene(Scene scene)
        {
            Values.ActiveScene = scene;
        }

        [Documentation("Gets the Updated Image Path for the current Layer")]
        public static string UpdatePath()
        {
            return UpdatePath(GetLayer(Values.ActiveLayer));
        }
        private static string UpdatePath(Layer layer)
        {
            var oldSessionPath = layer.RootPath;
            var oldImagePath = layer.ImagePath;
            var absolutImagePath = oldImagePath.Replace(oldSessionPath, "").Remove(0, 1);
            var newPath = Path.Combine(Values.SessionFolder, absolutImagePath);
            return UpdatePath(newPath, layer.DirectorySeperator);
        }

        private static string UpdatePath(string path, char replacement)
        {
            return path.Replace(replacement, Path.DirectorySeparatorChar);
        }

        internal static string GetSubDirectoryPath(string path)
        {
            return UpdatePath(Path.Combine(Values.SessionFolder, path), GetLayer(Values.ActiveLayer).DirectorySeperator);
        }

        internal static string GetSubDirectoryApplicationPath(string path)
        {
            return UpdatePath(Path.Combine(Application.StartupPath, path), GetLayer(Values.ActiveLayer).DirectorySeperator);
        }

        internal static string GetSubDirectoryPathForFile(string path, string fileName)
        {
            return UpdatePath(Path.Combine(Values.SessionFolder, path, fileName), GetLayer(Values.ActiveLayer).DirectorySeperator);
        }

        internal static void GetDirectories(string path, bool recursive, List<string> listDirs, List<string> listFile, bool isSource = true)
        {
            if (isSource == false)
                listDirs.Add(path);
            if (recursive) // if we want to get subdirectories
            {
                try // getting directories will throw an error if it is a path you don't have access to
                {
                    foreach (var child in Directory.GetDirectories(path)) // get all the subdirectories for the given path
                        GetDirectories(child, recursive, listDirs, listFile, false); // call our function for each sub directory

                    listFile.AddRange(Directory.GetFiles(path));
                }
                catch { }
            }
        }
    }
}
