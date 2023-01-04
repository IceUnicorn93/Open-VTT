using Open_VTT.Classes;
using Open_VTT.Classes.UI;
using Open_VTT.Other;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Open_VTT.Forms.Popups
{
    public partial class DisplayItemGenerator : Form
    {
        public DisplayItemGenerator()
        {
            InitializeComponent();

            Init();
        }

        public void Init()
        {
            Session.InitDisplayItems();

            tbName.Text = "";
            cbxParent.Text = "";
            rbItem.Checked = true;
            cbxParent.Items.Clear();
            cbxParent.Items.AddRange(Session.Values.DisplayItems.Where(n => n.ItemType == TreeViewDisplayItemType.Node).ToArray());
            cbxParent.AutoCompleteCustomSource.AddRange(Session.Values.DisplayItems.Where(n => n.ItemType == TreeViewDisplayItemType.Node).Select(n => n.Name).ToArray());
            tbName.Focus();
        }

        public void btnCreate_Click(object sender, EventArgs e)
        {
            //If Childs in Root are not allowed
            if (rbNode.Checked == false && cbxParent.SelectedItem == null)
            {
                //Init();
                MessageBox.Show("You try to create a Child without a Parent, please select a Parent!");

                return;
            }

            var item = new TreeViewDisplayItem
            {
                Name = tbName.Text,
                Parent = (TreeViewDisplayItem)cbxParent.SelectedItem,
                ItemType = rbItem.Checked ? TreeViewDisplayItemType.Item : TreeViewDisplayItemType.Node
            };
            Session.Values.DisplayItems.Add(item);
            var p = item.GetLocation();

            if (!rbNode.Checked)
                p[p.Count - 1] += ".xml";

            if (rbNode.Checked)
            { 
                Directory.CreateDirectory(Path.Combine(p.ToArray()));
                Session.CreateBlankTemplate(item);
            }
            else
            { 
                Session.CreateBlankNote(item);
            }

            Init();
        }
    }
}
