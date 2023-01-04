using Open_VTT.Classes;
using Open_VTT.Classes.UI;
using Open_VTT.Controls.Custom;
using Open_VTT.Forms.Popups;
using Open_VTT.Other;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Open_VTT.Controls
{
    public partial class TreeViewDisplay : UserControl
    {
        private DrawingPictureBox ArtworkDisplay;
        private ResizablePictureBox MiniArtworkDisplay;

        internal Editor Editor;

        Image MiniImage;
        Image BigImage;

        internal TreeViewDisplayItem currentInformationItem;

        public TreeViewDisplay()
        {
            InitializeComponent();
        }

        public void Init()
        {
            ArtworkDisplay = WindowInstaces.InformationDisplayPlayer.GetPictureBox();

            if (ArtworkDisplay == null)
                throw new ArgumentNullException(nameof(ArtworkDisplay));
            if (Editor == null)
                throw new ArgumentNullException(nameof(Editor));

            // Building Notes
            Session.InitDisplayItems();

            PopulateTreeView();
        }

        private void PopulateTreeView()
        {
            var treeNodes = new List<TreeNode>();
            var trackNodes = new List<TreeNode>();

            // First create a list of all Nodes, regardless of parent status
            foreach (var item in Session.Values.DisplayItems)
                if (item.ItemType == TreeViewDisplayItemType.Node)
                    trackNodes.Add(new TreeNode() { Text = item.Name, Tag = item });

            // Then create the final List, considering the parents
            foreach (var item in Session.Values.DisplayItems)
                if (item.ItemType == TreeViewDisplayItemType.Node && item.Parent == null)
                    treeNodes.Add(trackNodes.Single(n => n.Tag == item));
                else if (item.ItemType == TreeViewDisplayItemType.Node && item.Parent != null)
                    trackNodes.Single(n => n.Tag == item.Parent).Nodes.Add(trackNodes.Single(n => n.Tag == item));

            // Finally populate the Tree List with Children
            foreach (var item in Session.Values.DisplayItems)
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
            tbSearchItem.Items.AddRange(Session.Values.DisplayItems.Where(n => n.Name.ToLower().Contains(tbSearchItem.Text.ToLower()) && n.ItemType == TreeViewDisplayItemType.Item).Select(n => n.Name).ToArray());
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

                if (Settings.Values.DisplayChangesInstantly)
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

                WindowInstaces.InformationDisplayDM.GetPictureBox().Image?.Dispose();
                WindowInstaces.InformationDisplayDM.GetPictureBox().Image = null;
                WindowInstaces.InformationDisplayDM.GetPictureBox().Image = BigImage;

                WindowInstaces.InformationDisplayPlayer.GetPictureBox().Image?.Dispose();
                WindowInstaces.InformationDisplayPlayer.GetPictureBox().Image = null;
                WindowInstaces.InformationDisplayPlayer.GetPictureBox().Image = BigImage;
            }
        }

        private void btnOpenViewer_Click(object sender, EventArgs e)
        {
            WindowInstaces.InformationDisplayDM.Show();
            WindowInstaces.InformationDisplayPlayer.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var dialog = new DisplayItemGenerator())
                dialog.ShowDialog();

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
                        var list = Session.Values.DisplayItems.Where(n => n.Parent == parent).ToList();
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
                Session.Values.DisplayItems.Remove(item);

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
                currentInformationItem = Session.Values.DisplayItems.Single(n => n.Name == tbSearchItem.Text);

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
            tbSearchItem.Items.AddRange(Session.Values.DisplayItems.Where(n => n.Name.ToLower().Contains(tbSearchItem.Text.ToLower()) && n.ItemType == TreeViewDisplayItemType.Item).Select(n => n.Name).ToArray());
            tbSearchItem.SelectionStart = tbSearchItem.Text.Length;
            tbSearchItem.DroppedDown = true;

            Cursor = Cursors.Default;
        }
    }
}
