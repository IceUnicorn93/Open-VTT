using OpenVTT.Common;
using OpenVTT.Editor;
using OpenVTT.Editor.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;

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


        public TreeViewDisplay()
        {
            InitializeComponent();

            networkSync1.SyncComplete += Init;
            networkSync1.SERVER_PORT = Settings.Settings.Values.NoteServerPort;
            networkSync1.SERVER_IP = Settings.Settings.Values.NoteServerIP;
        }

        public void Init()
        {
            ArtworkDisplay = GetDmPictureBox();

            if (ArtworkDisplay == null)
                throw new ArgumentNullException(nameof(ArtworkDisplay));
            if (Editor == null)
                throw new ArgumentNullException(nameof(Editor));

            // Building Notes
            Session.Session.InitDisplayItems();

            PopulateTreeView();
        }

        private void PopulateTreeView()
        {
            var treeNodes = new List<TreeNode>();
            var trackNodes = new List<TreeNode>();

            // First create a list of all Nodes, regardless of parent status
            foreach (var item in Session.Session.Values.DisplayItems)
                if (item.ItemType == TreeViewDisplayItemType.Node)
                    trackNodes.Add(new TreeNode() { Text = item.Name, Tag = item });

            // Then create the final List, considering the parents
            foreach (var item in Session.Session.Values.DisplayItems)
                if (item.ItemType == TreeViewDisplayItemType.Node && item.Parent == null)
                    treeNodes.Add(trackNodes.Single(n => n.Tag == item));
                else if (item.ItemType == TreeViewDisplayItemType.Node && item.Parent != null)
                    trackNodes.Single(n => n.Tag == item.Parent).Nodes.Add(trackNodes.Single(n => n.Tag == item));

            // Finally populate the Tree List with Children
            foreach (var item in Session.Session.Values.DisplayItems)
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
            tbSearchItem.Items.AddRange(Session.Session.Values.DisplayItems.Where(n => n.Name.ToLower().Contains(tbSearchItem.Text.ToLower()) && n.ItemType == TreeViewDisplayItemType.Item).Select(n => n.Name).ToArray());
        }

        private void SetCurrentInformationItem()
        {
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
            GetDmDisplay()?.Show();
            GetPlayerDisplay()?.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (GenerateNewDisplayItem != null)
                GenerateNewDisplayItem();

            Init();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
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
                        var list = Session.Session.Values.DisplayItems.Where(n => n.Parent == parent).ToList();
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
                Session.Session.Values.DisplayItems.Remove(item);

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
            currentInformationItem = (TreeViewDisplayItem)e.Node.Tag;

            SetCurrentInformationItem();
        }

        private void tbSearchItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //TODO: What to do if an Item exists multiple times due to same name?
                currentInformationItem = Session.Session.Values.DisplayItems.Single(n => n.Name == tbSearchItem.Text);

                SetCurrentInformationItem();
            }
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            DisplayDmAndPlayer();
        }

        private void tbSearchItem_TextUpdate(object sender, EventArgs e)
        {
            tbSearchItem.Items.Clear();
            tbSearchItem.Items.AddRange(Session.Session.Values.DisplayItems.Where(n => n.Name.ToLower().Contains(tbSearchItem.Text.ToLower()) && n.ItemType == TreeViewDisplayItemType.Item).Select(n => n.Name).ToArray());
            tbSearchItem.SelectionStart = tbSearchItem.Text.Length;
            tbSearchItem.DroppedDown = true;

            Cursor = Cursors.Default;
        }
    }
}
