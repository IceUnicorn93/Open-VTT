﻿using OpenVTT.Controls.Forms;
using OpenVTT.Editor;
using OpenVTT.Logging;
using OpenVTT.Scripting;
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
            
            General Structure for Directorys:
            -> _Template-Design.cs          | Contains Design from Design UI
            -> _Template-Implementation.cs  | Contains Implementation for Databinding with JSON-Class
            -> _Template-JSON.json          | Contains Json-Description ala string : name | string : otherText and so on
            -> _Template.cs                 | Is generated Class by description of Template-JSON
            -> _Template.json               | Contains actual Data visible in the UI
            
             */

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
            files.RemoveAll(n => n.Contains("_Template.cs") || n.Contains("_Template-Design.cs") || n.Contains("_Template-Implementation.cs") || n.Contains("_Template-JSON.json") || n.Contains("_Template.json"));
            InitTreeViewFromList(files, true, nodeList);

            //Pre select the Notes-Node
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

        private void tvItems_AfterSelect(object sender, TreeViewEventArgs e)
        {
            btnShowDesigner.Enabled = !(tvItems.SelectedNode.Tag as NodeInformation).isIsLeaf;

            //Clear Panel for new Content
            PageControl.Controls.Clear();
            GC.Collect();
            var path = "";
            if ((tvItems.SelectedNode.Tag as NodeInformation).isIsLeaf)
                path = (tvItems.SelectedNode.Tag as NodeInformation).FilePath;
            else
                path = Path.Combine((tvItems.SelectedNode.Tag as NodeInformation).FilePath, "_Template.json");

            var directory = Directory.GetParent(path).FullName;

            //Check if new Content can be loaded
            if (!(File.Exists(Path.Combine(directory, "_Template-Design.cs")) && File.Exists(Path.Combine(directory, "_Template-Implementation.cs")) && File.Exists(Path.Combine(directory, "_Template.cs")))) return;

            //Read files for Data
            var designFile = File.ReadAllText(Path.Combine(directory, "_Template-Design.cs"));
            var implementationFile = File.ReadAllText(Path.Combine(directory, "_Template-Implementation.cs"));
            var templateFile = File.ReadAllText(Path.Combine(directory, "_Template.cs"));

            var completeFileText = designFile + Environment.NewLine + implementationFile + Environment.NewLine + templateFile + Environment.NewLine;

            

            //Create Dummy init
            var lines = new List<string>();
            lines.Add("var tj = new Template();");
            lines.Add("");
            lines.Add("tj = JsonSerializer.Deserialize<Template>(File.ReadAllBytes(@\"" + path + "\"));");
            lines.Add("var main = new Main(tj);");
            lines.Add("main");

            completeFileText += string.Join(Environment.NewLine, lines);

            //Run Script!
            var ctrl = ScriptEngine.RunUiScript<Control>(completeFileText, null).Result;
            ctrl.Dock = DockStyle.Fill;

            //Add to Panel
            PageControl.Controls.Add(ctrl);
        }
    }
}
