using OpenVTT.Controls.Forms;
using OpenVTT.Editor;
using OpenVTT.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OpenVTT.Controls
{
    public partial class TreeViewDisplay : UserControl
    {
        class NodeInformation
        {
            public string FilePath;
            public string Name;
            public bool isIsLeaf;
        }

        private Control _pageControl;
        public Control PageControl
        {
            get => _pageControl;
            set
            {
                _pageControl = value;
                if (value != null) Init();
            }
        }

        public TreeViewDisplay()
        {
            Logger.Log("Class: TreeViewDisplay | Constructor");

            InitializeComponent();

            networkSync1.SERVER_PORT = Settings.Settings.Values.NoteServerPort;
            networkSync1.SERVER_IP = Settings.Settings.Values.NoteServerIP;
        }

        private void btnOpenViewer_Click(object sender, EventArgs e)
        {
            //Disabled by now!

            /*
             * Uff. Lets think about this later!
             */
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            //Disabled by now!

            /*
             * Idea:
             * Check if PageControl has controls added
             * if yes: have a recursive method that searches for an Picturebox
             *      once found: store image of PictureBox in class field.
             *      
             * From there: More Rework! See ToDo
             */
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addNode = new AddNode();
            addNode.ShowDialog();

            var list = new List<TreeNode>();
            var nodeName = addNode.NodeName;
            var isNode = addNode.IsNode;
            var selNF = tvItems.SelectedNode.Tag as NodeInformation;

            CreateTreeNodeForView(Path.Combine(selNF.FilePath, nodeName), isNode, list);

            var node = list.First();
            tvItems.SelectedNode.Nodes.Add(node);

            if (isNode)
                Directory.CreateDirectory((node.Tag as NodeInformation).FilePath);
            else
                File.Create((node.Tag as NodeInformation).FilePath + ".json");
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //Remove old Item

            var selectedNode = tvItems.SelectedNode;
            var info = selectedNode.Tag as NodeInformation;

            if (info.Name == "Notes") return;

            if(info.isIsLeaf)
                File.Delete(info.FilePath);
            else
                if (MessageBox.Show($"Do you want to delete the Node {info.Name} and all it's children/leafs?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    Directory.Delete(info.FilePath, true);

            Init();
        }

        private void btnShowDesigner_Click(object sender, EventArgs e)
        {
            //Lets design Stuff

            /*
             * check if selected Node.Tag->isLeaf == false
             * if yes: Open Designer as Notes Designer
             *      save design as
             *          selectedNode.Tag->FilePath + _Design-Template.cs +
             *          selectedNode.Tag->FilePath + _Implementation-Template.cs + 
             *              -> Constructor needs the JsonClass-Object as parameter
             *              -> during Constructor Phase for each Textbox:
             *                  -> tb.DataBindings.Add("Text", jsonObj, nameof(jsonObj.PutNameHere));
             *          AppStartupPath + Notes + _Config + selectedNode.Tag->Name.json
             * if  no: Show popup, selection can't be a leaf
             */

            if ((tvItems.SelectedNode.Tag as NodeInformation).isIsLeaf)
            {
                MessageBox.Show("The Designer can only be opened on Nodes");
                return;
            }

            var designer = new ScriptDesigner();
            designer.LoadPath = (tvItems.SelectedNode.Tag as NodeInformation).FilePath;
            designer.IsNotesDesigner = true;
            designer.ShowDialog();

        }

        private void Init()
        {
            if (PageControl == null) return;

            if (!Directory.Exists(Path.Combine(Application.StartupPath, "Notes", "_Config")))
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, "Notes", "_Config"));

            //Clear TreeView and Rebuild it (Start with Notes Node)
            tvItems.Nodes.Clear();
            var nodeList = new List<TreeNode>();
            CreateTreeNodeForView(Path.Combine(Application.StartupPath, "Notes"), false, nodeList);
            tvItems.Nodes.Add(nodeList.First());

            //Get all Direcotrys
            var dirs = Directory.GetDirectories(Path.Combine(Application.StartupPath, "Notes"), "*", SearchOption.AllDirectories).ToList();
            InitTreeViewFromList(dirs, false, nodeList);

            //Get all Files
            var files = Directory.GetFiles(Path.Combine(Application.StartupPath, "Notes"), "*", SearchOption.AllDirectories).ToList();
            InitTreeViewFromList(files, true, nodeList);

            //Pre select the Notes-Node
            tvItems.SelectedNode = nodeList.First();
            nodeList.First().ExpandAll();
        }

        private void InitTreeViewFromList(List<string> list, bool isLeaf, List<TreeNode> nodeList)
        {
            list.RemoveAll(n => n.Contains("_Config"));
            foreach (var dir in list)
                CreateTreeNodeForView(dir, isLeaf, nodeList);
        }

        private void CreateTreeNodeForView(string path, bool isLeaf, List<TreeNode> nodeList)
        {
            var nName = "";
            if(isLeaf) nName = new FileInfo(path).Name;
            else nName = new DirectoryInfo(path).Name;

            var parent = Directory.GetParent(path);
            var node = new TreeNode(nName);
            var infos = new NodeInformation
            {
                Name = nName,
                isIsLeaf = isLeaf,
                FilePath = path
            };
            node.Tag = infos;

            if(nodeList.Count > 0 )
            {
                //Folders first
                nodeList.First(no => (no.Tag as NodeInformation).FilePath == parent.FullName).Nodes.Add(node);

                //Files first
                //var foundNode = nodeList.First(no => (no.Tag as NodeInformation).FilePath == parent.FullName);
                //var nodeNodesCount = foundNode.Nodes.Count;
                //var directoryCount = foundNode.Nodes.Cast<TreeNode>().Count(n => (n.Tag as NodeInformation).isIsLeaf == false);
                //if(nodeNodesCount > 0 && infos.isIsLeaf) foundNode.Nodes.Insert(nodeNodesCount - directoryCount, node);
                //else foundNode.Nodes.Add(node);
            }

            nodeList.Add(node);
        }
    }
}
