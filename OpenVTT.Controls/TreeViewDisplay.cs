using OpenVTT.Common;
using OpenVTT.Editor;
using OpenVTT.Editor.Controls;
using OpenVTT.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace OpenVTT.Controls
{
    public partial class TreeViewDisplay : UserControl
    {
        private DrawingPictureBox ArtworkDisplay;
        private ResizablePictureBox MiniArtworkDisplay;

        internal Editor.Editor Editor;

        Image MiniImage;
        Image BigImage;

        internal TreeViewDisplayItem currentInformationItem;

        internal Action GenerateNewDisplayItem;
        internal Func<Form> GetDmDisplay;
        internal Func<Form> GetPlayerDisplay;
        internal Func<DrawingPictureBox> GetDmPictureBox;
        internal Func<DrawingPictureBox> GetPlayerPictureBox;

        internal static List<TreeViewDisplayItem> DisplayItems;

        internal static void InitDisplayItems()
        {
            List<string> listDirs = new List<string>();
            List<string> listFile = new List<string>();

            DisplayItems = new List<TreeViewDisplayItem>();

            listDirs.Clear();
            listFile.Clear();

            Session.Session.GetDirectories(Session.Session.GetSubDirectoryApplicationPath("Notes"), true, listDirs, listFile);

            foreach (var item in listDirs)
            {
                var direcotryList = item.Remove(0, Application.StartupPath.Length).Split(Path.DirectorySeparatorChar).Where(n => n != "" && n != "Notes").ToList(); // Monsters\Homebrew
                DisplayItems.Add(new TreeViewDisplayItem
                {
                    Name = Path.GetFileName(item),
                    ItemType = TreeViewDisplayItemType.Node,
                    ParentDirectories = direcotryList
                });

                var parent = DisplayItems.SingleOrDefault(n =>
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

                DisplayItems.Last().Parent = parent;
            }

            foreach (var item in listFile)
            {
                var fi = new FileInfo(item);
                var direcotryList = item.Remove(0, Application.StartupPath.Length).Split(Path.DirectorySeparatorChar).Where(n => n != "" && n != "Notes").ToList(); // Monsters\Homebrew

                if (fi.Extension == ".xml" && fi.Name != "_Template.xml")
                {
                    DisplayItems.Add(new TreeViewDisplayItem
                    {
                        Name = fi.Name.Replace(fi.Extension, ""),
                        ItemType = TreeViewDisplayItemType.Item,
                        ParentDirectories = direcotryList
                    });

                    var parent = DisplayItems.SingleOrDefault(n =>
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

                    DisplayItems.Last().Parent = parent;
                }
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


        public TreeViewDisplay()
        {
            Logger.Log("Class: TreeViewDisplay | Constructor");

            InitializeComponent();

            networkSync1.SyncComplete += Init;
            networkSync1.SERVER_PORT = Settings.Settings.Values.NoteServerPort;
            networkSync1.SERVER_IP = Settings.Settings.Values.NoteServerIP;
        }

        public void Init()
        {
            Logger.Log("Class: TreeViewDisplay | Init");

            ArtworkDisplay = GetDmPictureBox();

            if (ArtworkDisplay == null)
                throw new ArgumentNullException(nameof(ArtworkDisplay));
            if (Editor == null)
                throw new ArgumentNullException(nameof(Editor));

            // Building Notes
            InitDisplayItems();

            PopulateTreeView();
        }

        private void PopulateTreeView()
        {
            Logger.Log("Class: TreeViewDisplay | PopulateTreeView");

            var treeNodes = new List<TreeNode>();
            var trackNodes = new List<TreeNode>();

            // First create a list of all Nodes, regardless of parent status
            foreach (var item in DisplayItems)
                if (item.ItemType == TreeViewDisplayItemType.Node)
                    trackNodes.Add(new TreeNode() { Text = item.Name, Tag = item });

            // Then create the final List, considering the parents
            foreach (var item in DisplayItems)
                if (item.ItemType == TreeViewDisplayItemType.Node && item.Parent == null)
                    treeNodes.Add(trackNodes.Single(n => n.Tag == item));
                else if (item.ItemType == TreeViewDisplayItemType.Node && item.Parent != null)
                    trackNodes.Single(n => n.Tag == item.Parent).Nodes.Add(trackNodes.Single(n => n.Tag == item));

            // Finally populate the Tree List with Children
            foreach (var item in DisplayItems)
                if (item.Parent != null && item.ItemType == TreeViewDisplayItemType.Item)
                {
                    trackNodes.Single(n => n.Tag == item.Parent).Nodes.Add(new TreeNode() { Text = item.Name, Tag = item });
                    tbSearchItem.AutoCompleteCustomSource.Add(item.Name);
                }
                else if (item.Parent == null && item.ItemType == TreeViewDisplayItemType.Item)
                {
                    treeNodes.Add(new TreeNode() { Text = item.Name, Tag = item });
                    tbSearchItem.AutoCompleteCustomSource.Add(item.Name);
                }

            // Populate the Control
            tvItems.Nodes.Clear();
            tvItems.Nodes.AddRange(treeNodes.ToArray());

            tbSearchItem.Items.Clear();
            tbSearchItem.Items.AddRange(DisplayItems.Where(n => n.Name.ToLower().Contains(tbSearchItem.Text.ToLower()) && n.ItemType == TreeViewDisplayItemType.Item).Select(n => n.Name).ToArray());
        }

        private void SetCurrentInformationItem()
        {
            Logger.Log("Class: TreeViewDisplay | SetCurrentInformationItem");

            if (MiniArtworkDisplay != null)
            {
                MiniArtworkDisplay.Image?.Dispose();
                MiniArtworkDisplay.Image = null;
            }

            Editor.Load(currentInformationItem);
            MiniArtworkDisplay = Editor.GetPictureBox();

            var imagePath = Path.Combine(currentInformationItem.GetLocation(".png").ToArray());
            if (File.Exists(imagePath))
            {
                MiniImage?.Dispose();
                MiniImage = null;
                MiniImage = Image.FromFile(imagePath);


                MiniArtworkDisplay.Image = MiniImage;

                if (Settings.Settings.Values.DisplayChangesInstantly)
                {
                    DisplayDmAndPlayer();
                }
            }

            GC.Collect(5, GCCollectionMode.Forced);
        }

        private void DisplayDmAndPlayer()
        {
            Logger.Log("Class: TreeViewDisplay | DisplayDmAndPlayer");

            var imagePath = Path.Combine(currentInformationItem.GetLocation(".png").ToArray());

            BigImage?.Dispose();
            BigImage = null;

            if (File.Exists(imagePath))
            {
                BigImage = Image.FromFile(imagePath);

                GetDmPictureBox().Image?.Dispose();
                GetDmPictureBox().Image = null;
                GetDmPictureBox().Image = BigImage;

                GetPlayerPictureBox().Image?.Dispose();
                GetPlayerPictureBox().Image = null;
                GetPlayerPictureBox().Image = BigImage;
            }
        }

        private void btnOpenViewer_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: TreeViewDisplay | btnOpenViewer_Click");

            GetDmDisplay()?.Show();
            GetPlayerDisplay()?.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: TreeViewDisplay | btnAdd_Click");

            if (GenerateNewDisplayItem != null)
                GenerateNewDisplayItem();

            Init();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: TreeViewDisplay | btnRemove_Click");

            ArtworkDisplay.Image?.Dispose();
            ArtworkDisplay.Image = null;
            MiniArtworkDisplay.Image?.Dispose();
            MiniArtworkDisplay.Image = null;
            MiniImage?.Dispose();
            MiniImage = null;

            var childsToDelete = new List<TreeViewDisplayItem>();
            if (currentInformationItem.ItemType == TreeViewDisplayItemType.Node)
            {
                if (MessageBox.Show("You try to delete a node, deleting the Node will delete all Children, Are you sure you want to continue?", $"Delete: {currentInformationItem.Name}?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    void GetChildsToDelete(TreeViewDisplayItem parent)
                    {
                        var list = DisplayItems.Where(n => n.Parent == parent).ToList();
                        childsToDelete.AddRange(list);

                        if (list.Count > 0)
                            foreach (var item in list)
                                if (item.ItemType == TreeViewDisplayItemType.Node)
                                    GetChildsToDelete(item);
                    }

                    GetChildsToDelete(currentInformationItem);
                }
            }

            childsToDelete.Add(currentInformationItem);

            foreach (var item in childsToDelete)
            {
                DisplayItems.Remove(item);

                var p = new List<string>();

                try
                {
                    if (item.ItemType == TreeViewDisplayItemType.Node)
                    {
                        p = item.GetLocation();

                        Directory.Delete(Path.Combine(p.ToArray()), true);
                    }
                    else
                    {
                        p = item.GetLocation(".xml");

                        if (File.Exists(Path.Combine(p.ToArray())))
                            File.Delete(Path.Combine(p.ToArray()));

                        p = item.GetLocation(".png");

                        if (File.Exists(Path.Combine(p.ToArray())))
                            File.Delete(Path.Combine(p.ToArray()));
                    }
                }
                catch { }
            }

            Init();
        }

        private void tvItems_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Logger.Log("Class: TreeViewDisplay | tvItems_NodeMouseClick");

            currentInformationItem = (TreeViewDisplayItem)e.Node.Tag;

            SetCurrentInformationItem();
        }

        private void tbSearchItem_KeyDown(object sender, KeyEventArgs e)
        {
            Logger.Log("Class: TreeViewDisplay | tbSearchItem_KeyDown");

            if (e.KeyCode == Keys.Enter)
            {
                //TODO: What to do if an Item exists multiple times due to same name?
                currentInformationItem = DisplayItems.Single(n => n.Name == tbSearchItem.Text);

                SetCurrentInformationItem();
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            Logger.Log("Class: TreeViewDisplay | btnDisplay_Click");

            DisplayDmAndPlayer();
        }

        private void tbSearchItem_TextUpdate(object sender, EventArgs e)
        {
            Logger.Log("Class: TreeViewDisplay | tbSearchItem_TextUpdate");

            tbSearchItem.Items.Clear();
            tbSearchItem.Items.AddRange(DisplayItems.Where(n => n.Name.ToLower().Contains(tbSearchItem.Text.ToLower()) && n.ItemType == TreeViewDisplayItemType.Item).Select(n => n.Name).ToArray());
            tbSearchItem.SelectionStart = tbSearchItem.Text.Length;
            tbSearchItem.DroppedDown = true;

            Cursor = Cursors.Default;
        }
    }
}
