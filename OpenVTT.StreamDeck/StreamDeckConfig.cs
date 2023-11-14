using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OpenVTT.StreamDeck
{
    public partial class StreamDeckConfig : UserControl
    {
        class ActionSelector : Form
        {
            public string Selection = "";

            public ActionSelector(List<string> actions)
            {
                StartPosition = FormStartPosition.CenterScreen;

                Size = new Size(200, 70);
                var cbx = new ComboBox();
                cbx.Items.AddRange(actions.ToArray());
                cbx.Location = new Point(5, 5);
                cbx.Size = new Size(175, 30);
                Controls.Add(cbx);

                cbx.SelectedIndexChanged += (s, e) =>
                {
                    Selection = cbx.SelectedItem.ToString();
                    Close();
                };
            }
        }

        public StreamDeckConfig()
        {
            InitializeComponent();
        }

        private void StreamDeckConfig_Load(object sender, EventArgs e)
        {
            if (StreamDeckStatics.IsInitialized == false) return;

            cbxStates.Items.Clear();
            var states = new List<string>();
            states = StreamDeckStatics.StateDescrptions.Select(x => x.State).ToList();
            cbxStates.Items.AddRange(states.ToArray());

            var size = StreamDeckStatics.GetSize();

            for (int x = 0; x < size.Width; x++)
                for (int y = 0; y < size.Height; y++)
                {
                    var btn = new Button();
                    btn.Size = new Size(btn.Size.Width, btn.Size.Width);
                    btn.Location = new Point(x * (btn.Width + 10), y * (btn.Height + 10));
                    btn.Tag = (x, y);
                    btn.Click += (s, ea) =>
                    {
                        var actions = StreamDeckStatics.ActionList.Select(n => n.Name).ToList();
                        actions.Insert(0, "");
                        actions.Insert(1, "Paging");
                        var actssel = new ActionSelector(actions);
                        actssel.ShowDialog();

                        if (actssel.Selection == "") return;

                        var localBtn = (Button)s;
                        var tag = ((int x, int y))localBtn.Tag;
                        localBtn.Text = actssel.Selection;

                        var selectedState = cbxStates.SelectedItem;
                        var desc = StreamDeckStatics.StateDescrptions.Single(n => n.State == selectedState);
                        desc.ActionDescription[tag.x, tag.y] = actssel.Selection;

                        cbxStates_SelectedIndexChanged(null, null);

                        StreamDeckStatics.SwitchDeckState();
                    };
                    pnlButtons.Controls.Add(btn);
                }

            cbxStates.SelectedIndex = 0;
        }

        private void cbxStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StreamDeckStatics.IsInitialized == false) return;

            var selectedState = cbxStates.SelectedItem;
            var desc = StreamDeckStatics.StateDescrptions.Single(n => n.State == selectedState);

            var size = StreamDeckStatics.GetSize();

            for (int x = 0; x < size.Width; x++)
                for (int y = 0; y < size.Height; y++)
                {
                    var btn = pnlButtons.Controls.Cast<Button>().Single(n => ((int, int))n.Tag == (x, y));
                    btn.Text = desc.ActionDescription[x,y].ToString();
                    if (btn.Text == "Paging")
                        btn.BackColor = Color.FromKnownColor(KnownColor.ControlLight);
                    else if (btn.Text == "" || btn.Text.Contains("."))
                        btn.BackColor = Color.FromKnownColor(KnownColor.ControlDark);
                }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (StreamDeckStatics.IsInitialized == false) return;

            StreamDeckStatics.SaveConfig();
        }
    }
}
