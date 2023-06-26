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
    public class Session
    {
        [XmlIgnore]
        public int ActiveLayer;
        [XmlIgnore]
        public Scene ActiveScene;
        [XmlIgnore]
        internal List<TreeViewDisplayItem> DisplayItems;

        public string SessionFolder;
        public List<Scene> Scenes;

        public Session()
        {

        }
        public static Session Values;

        static Session()
        {
            Values = new Session();
        }

        public static void Save(bool optimize)
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

        public static void Load(string path = "")
        {
            if (path == "")
                path = GetSubDirectoryPathForFile(Values.SessionFolder, "Session.xml");

            var x = new XmlSerializer(typeof(Session));
            using (var sr = new StreamReader(path))
            {
                Values = (Session)x.Deserialize(sr);
            }
        }


        internal static void CreateBlankTemplate(TreeViewDisplayItem item)
        {
            var x = new XmlSerializer(typeof(List<CustomControlData>));
            var p = item.GetLocation("", true);
            p.Add("_Template.xml");
            using (var sw = new StreamWriter(Path.Combine(p.ToArray())))
            {
                var list = new List<CustomControlData>
                {
                    new CustomControlData
                    {
                        ControlType = CustomControlType.Picturebox,
                        Name = "picturebox",
                        Location = new Point(20, 20),
                        Size = new Size(200, 200)
                    }
                };
                x.Serialize(sw, list);
            }
        }
        internal static void CreateBlankNote(TreeViewDisplayItem item)
        {
            var x = new XmlSerializer(typeof(List<CustomControlData>));
            using (var sw = new StreamWriter(Path.Combine(item.GetLocation(".xml").ToArray())))
            {
                x.Serialize(sw, new List<CustomControlData>());
            }
        }



        public static Layer GetLayer(int number)
        {
            return Values.ActiveScene.Layers.SingleOrDefault(n => n.LayerNumber == number);
        }

        public static void SetLayer(int number)
        {
            Values.ActiveLayer = number;
        }
        public static void SetScene(Scene scene)
        {
            Values.ActiveScene = scene;
        }


        public static string UpdatePath()
        {
            return UpdatePath(GetLayer(Values.ActiveLayer));
        }
        public static string UpdatePath(Layer layer)
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

        public static string GetSubDirectoryPath(string path)
        {
            return UpdatePath(Path.Combine(Values.SessionFolder, path), GetLayer(Values.ActiveLayer).DirectorySeperator);
        }

        public static string GetSubDirectoryApplicationPath(string path)
        {
            return UpdatePath(Path.Combine(Application.StartupPath, path), GetLayer(Values.ActiveLayer).DirectorySeperator);
        }

        public static string GetSubDirectoryPathForFile(string path, string fileName)
        {
            return UpdatePath(Path.Combine(Values.SessionFolder, path, fileName), GetLayer(Values.ActiveLayer).DirectorySeperator);
        }

        public static void InitDisplayItems()
        {
            List<string> listDirs = new List<string>();
            List<string> listFile = new List<string>();

            Session.Values.DisplayItems = new List<TreeViewDisplayItem>();

            listDirs.Clear();
            listFile.Clear();

            GetDirectories(Session.GetSubDirectoryApplicationPath("Notes"), true, listDirs, listFile);

            foreach (var item in listDirs)
            {
                var direcotryList = item.Remove(0, Application.StartupPath.Length).Split(Path.DirectorySeparatorChar).Where(n => n != "" && n != "Notes").ToList(); // Monsters\Homebrew
                Session.Values.DisplayItems.Add(new TreeViewDisplayItem
                {
                    Name = Path.GetFileName(item),
                    ItemType = TreeViewDisplayItemType.Node,
                    ParentDirectories = direcotryList
                });

                var parent = Session.Values.DisplayItems.SingleOrDefault(n =>
                {
                    if (n == null) return false;

                    var parentList = n.ParentDirectories;

                    if (parentList.Count == direcotryList.Count - 1)
                    {
                        var isMatch = true;

                        for (int i = 0; i < parentList.Count; i++)
                        {
                            if (parentList[i] != direcotryList[i])
                            {
                                isMatch = false;
                                break;
                            }
                        }
                        if (isMatch == true && n.Name != Path.GetFileName(item)) return true; else return false;
                    }
                    return false;
                });

                Session.Values.DisplayItems.Last().Parent = parent;
            }

            foreach (var item in listFile)
            {
                var fi = new FileInfo(item);
                var direcotryList = item.Remove(0, Application.StartupPath.Length).Split(Path.DirectorySeparatorChar).Where(n => n != "" && n != "Notes").ToList(); // Monsters\Homebrew

                if (fi.Extension == ".xml" && fi.Name != "_Template.xml")
                {
                    Session.Values.DisplayItems.Add(new TreeViewDisplayItem
                    {
                        Name = fi.Name.Replace(fi.Extension, ""),
                        ItemType = TreeViewDisplayItemType.Item,
                        ParentDirectories = direcotryList
                    });

                    var parent = Session.Values.DisplayItems.SingleOrDefault(n =>
                    {
                        if (n == null) return false;

                        var parentList = n.ParentDirectories;

                        if (parentList.Count == direcotryList.Count - 1)
                        {
                            var isMatch = true;

                            for (int i = 0; i < parentList.Count; i++)
                            {
                                if (parentList[i] != direcotryList[i])
                                {
                                    isMatch = false;
                                    break;
                                }
                            }
                            if (isMatch == true && n.Name != Path.GetFileName(item).Replace(".xml", "")) return true; else return false;
                        }
                        return false;
                    });

                    Session.Values.DisplayItems.Last().Parent = parent;
                }
            }
        }

        private static void GetDirectories(string path, bool recursive, List<string> listDirs, List<string> listFile, bool isSource = true)
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
