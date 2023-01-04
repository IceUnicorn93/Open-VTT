using Open_VTT.Classes.UI;
using Open_VTT.Controls.Custom;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Open_VTT.Controls
{
    public partial class Editor : UserControl
    {
        private bool _isInEditMode = true;
        public bool IsInEditMode
        {
            get => _isInEditMode;
            set
            {
                _isInEditMode = value;

                lblEditName.Visible = value;
                lblEditName2.Visible = value;
                lblEditText.Visible = value;
                tbEditText.Visible = value;
                btnEditNewLabel.Visible = value;
                btnEditNewTextbox.Visible = value;
                btnSave.Visible = !value;

                tbEditText.Text = "";
                lblEditName2.Text = "###";
            }
        }

        private Control activeControl;
        private Point previousPosition;

        private TreeViewDisplayItem currentItem;

        public Editor()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            var lstToRemove = new List<Control>();
            foreach (Control item in Controls)
            {
                if (!(item is ResizableLabel || item is ResizablePictureBox || item is ResizableTextbox))
                    continue;

                lstToRemove.Add(item);
            }

            foreach (var item in lstToRemove)
                Controls.Remove(item);
        }

        private void Save()
        {
            if (currentItem == null)
                return;

            var list = new List<CustomControlData>();

            foreach (Control item in Controls)
            {
                if (!(item is ResizableLabel || item is ResizablePictureBox || item is ResizableTextbox))
                    continue;

                var data = new CustomControlData()
                {
                    Location = item.Location,
                    Size = item.Size,
                    Name = item.Name,
                    Text = item.Text,
                };
                if (item is ResizableLabel)
                    data.ControlType = Other.CustomControlType.Label;
                if (item is ResizablePictureBox)
                    data.ControlType = Other.CustomControlType.Picturebox;
                if (item is ResizableTextbox)
                    data.ControlType = Other.CustomControlType.Textbox;

                list.Add(data);
            }

            var x = new XmlSerializer(typeof(List<CustomControlData>));

            if (currentItem.ItemType == Other.TreeViewDisplayItemType.Item)
            {
                foreach (var item in list)
                {
                    item.Location = default;
                    item.Size = default;
                }

                using (var sw = new StreamWriter(Path.Combine(currentItem.GetLocation(".xml").ToArray())))
                {
                    x.Serialize(sw, list);
                }
            }
            else
            {
                var p = currentItem.GetLocation();
                p.Add("_Template.xml");
                using (var sw = new StreamWriter(Path.Combine(p.ToArray())))
                {
                    x.Serialize(sw, list);
                }
            }

            
        }

        internal void Load(TreeViewDisplayItem item)
        {
            Init();

            currentItem = item;

            var list = new List<CustomControlData>();

            var itemBasePath = item.ItemType == Other.TreeViewDisplayItemType.Node ? item.GetLocation("") : item.GetLocation("", false);
            itemBasePath.Add("_Template.xml");
            var itemXmlPath = item.GetLocation(".xml");

            var TemplatePath = Path.Combine(itemBasePath.ToArray());
            var ItemPath = Path.Combine(itemXmlPath.ToArray());

            IsInEditMode = true;
            if (File.Exists(ItemPath))
                IsInEditMode = false;

            PlaceFromTemplate(list, TemplatePath);
            PopulateWithInstance(ItemPath);
        }

        private void PopulateWithInstance(string ItemPath)
        {
            if (!File.Exists(ItemPath))
                return;

            var list = new List<CustomControlData>();
            var x = new XmlSerializer(typeof(List<CustomControlData>));
            using (var sr = new StreamReader(ItemPath))
            {
                list = (List<CustomControlData>)x.Deserialize(sr);
            }

            foreach (var listItem in list)
            {
                var c = new Control();

                switch (listItem.ControlType)
                {
                    case Other.CustomControlType.Label:
                        c = Controls.OfType<ResizableLabel>().ToList().SingleOrDefault(n => n.Name == listItem.Name);
                        break;
                    case Other.CustomControlType.Textbox:
                        c = Controls.OfType<ResizableTextbox>().ToList().SingleOrDefault(n => n.Name == listItem.Name);
                        break;
                    case Other.CustomControlType.Picturebox:
                        c = Controls.OfType<ResizablePictureBox>().ToList().SingleOrDefault(n => n.Name == listItem.Name);
                        break;
                    default:
                        break;
                }

                if (c == null)
                    continue;

                c.Text = listItem.Text.Replace("\n", Environment.NewLine);
            }
        }

        private void PlaceFromTemplate(List<CustomControlData> list, string TemplatePath)
        {
            if (!File.Exists(TemplatePath))
                return;

            var x = new XmlSerializer(typeof(List<CustomControlData>));
            using (var sr = new StreamReader(TemplatePath))
            {
                list = (List<CustomControlData>)x.Deserialize(sr);
            }

            foreach (var listItem in list)
            {
                var c = new Control();
                if (listItem.ControlType == Other.CustomControlType.Label)
                {
                    c = new ResizableLabel
                    {
                        AutoSize = true,
                        IsEditable = IsInEditMode
                    };
                }
                else if (listItem.ControlType == Other.CustomControlType.Picturebox)
                {
                    c = new ResizablePictureBox
                    {
                        IsEditable = IsInEditMode,
                        BackColor = Color.DimGray,
                        SizeMode = PictureBoxSizeMode.Zoom
                    };
                    if (currentItem.ItemType == Other.TreeViewDisplayItemType.Item)
                        c.DoubleClick += (object sender, EventArgs e) =>
                        {
                            using (var ofd = new OpenFileDialog())
                                if (ofd.ShowDialog() == DialogResult.OK)
                                {
                                    var newPath = Path.Combine(currentItem.GetLocation(".png").ToArray());
                                    File.Copy(ofd.FileName, newPath, true);
                                    ((ResizablePictureBox)c).Image = Image.FromFile(newPath);
                                }
                        };
                }
                else if (listItem.ControlType == Other.CustomControlType.Textbox)
                {
                    c = new ResizableTextbox
                    {
                        Multiline = true,
                        IsEditable = IsInEditMode
                    };
                }

                c.Location = listItem.Location;
                c.Size = listItem.Size;
                c.Name = listItem.Name;
                c.Text = listItem.Text;

                if (currentItem.ItemType == Other.TreeViewDisplayItemType.Node)
                {
                    c.MouseDown += new MouseEventHandler(ControlMouseDown);
                    c.MouseMove += new MouseEventHandler(ControlMouseMove);
                    c.MouseUp += new MouseEventHandler(ControlMouseUp);
                }

                Controls.Add(c);
            }
        }

        internal ResizablePictureBox GetPictureBox()
        {
            return Controls.OfType<ResizablePictureBox>().SingleOrDefault();
        }

        private void ControlMouseUp(object sender, MouseEventArgs e)
        {
            activeControl = null;
            Cursor = Cursors.Default;

            Save();
        }

        private void ControlMouseMove(object sender, MouseEventArgs e)
        {
            if (activeControl == null || activeControl != sender)
                return;
            var location = activeControl.Location;

            location.Offset(e.Location.X - previousPosition.X, e.Location.Y - previousPosition.Y);
            activeControl.Location = location;
        }

        private void ControlMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (!(sender is ResizablePictureBox))
                    Controls.Remove(sender as Control);
            }
            else
            {
                activeControl = sender as Control;

                lblEditName2.Text = activeControl.Name;
                tbEditText.Text = activeControl.Text;
                tbEditText.Tag = activeControl;

                previousPosition = e.Location;
                Cursor = Cursors.Hand;
            }
        }

        private void btnEditNewTextbox_Click(object sender, EventArgs e)
        {
            var tb = new ResizableTextbox();
            tb.Name = $"c{Controls.Count - 6}";
            tb.Location = new Point(20, 20);
            tb.Size = new Size(100, 30);
            tb.Multiline = true;

            tb.MouseDown += new MouseEventHandler(ControlMouseDown);
            tb.MouseMove += new MouseEventHandler(ControlMouseMove);
            tb.MouseUp += new MouseEventHandler(ControlMouseUp);

            this.Controls.Add(tb);
        }

        private void btnEditNewLabel_Click(object sender, EventArgs e)
        {
            var lbl = new ResizableLabel();
            lbl.Name = $"c{Controls.Count - 6}";
            lbl.Location = new Point(20, 20);
            lbl.Size = new Size(100, 30);
            lbl.Text = "###--- Edit Me ---###";
            lbl.AutoSize = true;

            lbl.MouseDown += new MouseEventHandler(ControlMouseDown);
            lbl.MouseMove += new MouseEventHandler(ControlMouseMove);
            lbl.MouseUp += new MouseEventHandler(ControlMouseUp);

            this.Controls.Add(lbl);
        }

        private void tbEditText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var c = (Control)tbEditText.Tag;
                c.Text = tbEditText.Text;

                Save();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
    }
}
