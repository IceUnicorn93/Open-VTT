using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using OpenVTT.Common;
using OpenVTT.Editor;
using OpenVTT.Logging;

namespace OpenVTT.Session
{
    [Documentation("To use this Object, use Session.Session.Values.XYZ = ABC; or Session.Session.Method();", Name = "Session")]
    public class Session
    {
        [XmlIgnore]
        [Documentation("Number of Active Layer", Name = "ActiveLayerNumber", IsField = true, DataType = "int")]
        public int ActiveLayerNumber { get; internal set; }

        [XmlIgnore]
        [Documentation("The current Layer", Name = "ActiveLayer", IsField = true, DataType = "Layer")]
        public Layer ActiveLayer { get => Values.ActiveScene.Layers.SingleOrDefault(n => n.LayerNumber == Values.ActiveLayerNumber); }

        [XmlIgnore]
        [Documentation("Actual Active Scene-Object", Name = "ActiveScene", IsField = true, DataType = "Scene")]
        public Scene ActiveScene { get; internal set; }

        [Documentation("Path where the Session-Folder was created", Name = "SessionFolder", IsField = true, DataType = "string")]
        public string SessionFolder;
        [Documentation("List of Scene-Objects", Name = "Scenes", IsField = true, DataType = "List<Scene>")]
        public List<Scene> Scenes;
        [Documentation("List of CustomSettings-Objects", Name = "CustomData", IsField = true, DataType = "List<CustomSettings>")]
        public List<CustomSettings> CustomData = new List<CustomSettings>();

        public Session()
        {
            Logger.Log("Class: Session | Constructor");
        }

        [Documentation("Static Session-Object", Name = "Values", IsStatic = true, IsField = true, DataType = "Session")]
        public static Session Values;

        [Documentation("Static Constructor", Name = "Session", IsMethod = true, ReturnType = "Session")]
        static Session()
        {
            Logger.Log("Class: Session | static Constructor");

            Values = new Session();
        }

        [Documentation("Saves the Settings", Name = "Save", IsStatic = true, IsMethod = true, ReturnType = "void", Parameters = "bool optimize")]
        public static void Save(bool optimize)
        {
            Logger.Log("Class: Session | Save");

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
            Logger.Log("Class: Session | Load");

            if (path == "")
                path = GetSubDirectoryPathForFile(Values.SessionFolder, "Session.xml");

            var x = new XmlSerializer(typeof(Session));
            using (var sr = new StreamReader(path))
            {
                Values = (Session)x.Deserialize(sr);
            }
        }

        [Documentation("Returns the Layer-Object for the given Layer-Number", Name = "GetLayer", IsStatic = true, IsMethod = true, ReturnType = "Layer", Parameters = "int number")]
        public static Layer GetLayer(int number)
        {
            Logger.Log("Class: Session | GetLayer");

            return Values.ActiveScene.Layers.SingleOrDefault(n => n.LayerNumber == number);
        }
        [Documentation("Sets the Layer-Number")]
        public static void SetLayer(int number)
        {
            Logger.Log("Class: Session | SetLayer");

            Values.ActiveLayerNumber = number;
        }
        [Documentation("Sets the Active-Scene to the given Scene", Name = "SetScene", IsStatic = true, IsMethod = true, ReturnType = "void", Parameters = "Scene scene")]
        public static void SetScene(Scene scene)
        {
            Logger.Log("Class: Session | SetScene");

            Values.ActiveScene = scene;
        }

        [Documentation("Gets the Updated Image Path for the current Layer", Name = "UpdatePath", IsStatic = true, IsMethod = true, ReturnType = "string")]
        public static string UpdatePath()
        {
            Logger.Log("Class: Session | UpdatePath");

            return UpdatePath(Values.ActiveLayer);
        }
        private static string UpdatePath(Layer layer)
        {
            Logger.Log("Class: Session | UpdatePath(Layer layer)");

            if (layer.RootPath == "" || layer.ImagePath == "") return "";

            var oldSessionPath = layer.RootPath;
            var oldImagePath = layer.ImagePath;
            var absolutImagePath = oldImagePath.Replace(oldSessionPath, "");

            if (absolutImagePath[0] != 'I')
                absolutImagePath = absolutImagePath.Remove(0, 1);

            var newPath = Path.Combine(Values.SessionFolder, absolutImagePath);
            return UpdatePath(newPath, layer.DirectorySeperator);
        }

        public static string UpdateVideoPath(Layer layer, bool Thumbnail)
        {
            Logger.Log("Class: Session | UpdatePath(Layer layer)");

            if (layer.RootPath == "" || layer.ImagePath == "") return "";

            var extension = new FileInfo(layer.ImagePath).Extension;

            var oldSessionPath = layer.RootPath;
            var oldImagePath = layer.ImagePath;

            if (Thumbnail)
            {
                oldImagePath = oldImagePath.Replace("Videos", "Thumbnails").Replace(extension, ".png");
            }

            var absolutImagePath = oldImagePath.Replace(oldSessionPath, "");

            if (absolutImagePath[0] == layer.DirectorySeperator)
                absolutImagePath = absolutImagePath.Remove(0, 1);

            var newPath = Path.Combine(Values.SessionFolder, absolutImagePath);
            return UpdatePath(newPath, layer.DirectorySeperator);
        }

        private static string UpdatePath(string path, char replacement)
        {
            Logger.Log("Class: Session | UpdatePath(string path, char replacement)");

            return path.Replace(replacement, Path.DirectorySeparatorChar);
        }

        internal static string GetSubDirectoryPath(string path)
        {
            Logger.Log("Class: Session | GetSubDirectoryPath");

            return UpdatePath(Path.Combine(Values.SessionFolder, path), Values.ActiveLayer.DirectorySeperator);
        }

        internal static string GetSubDirectoryApplicationPath(string path)
        {
            Logger.Log("Class: Session | GetSubDirectoryApplicationPath");

            return UpdatePath(Path.Combine(Application.StartupPath, path), Values.ActiveLayer.DirectorySeperator);
        }

        internal static string GetSubDirectoryPathForFile(string path, string fileName)
        {
            Logger.Log("Class: Session | GetSubDirectoryPathForFile");

            return UpdatePath(Path.Combine(Values.SessionFolder, path, fileName), Values.ActiveLayer.DirectorySeperator);
        }

        internal static void GetDirectories(string path, bool recursive, List<string> listDirs, List<string> listFile, bool isSource = true)
        {
            Logger.Log("Class: Session | GetDirectories");

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
