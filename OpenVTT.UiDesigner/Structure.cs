﻿using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace OpenVTT.UiDesigner
{
    public partial class Structure : UserControl, IStructureBase
    {
        public string[] Types
        {
            get => cbType.Items.Cast<string>().ToArray();
            set { cbType.Items.Clear(); cbType.Items.AddRange(value); }
        }

        string IStructureBase.Name { get => tbName.Text; set => tbName.Text = value; }
        string IStructureBase.Type { get => cbType.Text; set => cbType.Text = value; }
        bool IStructureBase.SingleValue { get => !tbMultiValue.Checked; set => tbMultiValue.Checked = !value; }

        public Action RemoveAction;

        public Structure()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RemoveAction?.Invoke();
        }
    }
}
