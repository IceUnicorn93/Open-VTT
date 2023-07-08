using OpenVTT.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace OpenVTT.Controls
{
    public partial class RecentlyOpenedControl : UserControl
    {
        List<string> Paths;

        internal SessionLoad SessionLoaded;

        public RecentlyOpenedControl()
        {
            InitializeComponent();

            Paths = new List<string>();

            Load();

            Init();
        }

        internal void Init()
        {
            Controls.Clear();

            for (int i = 0; i < Paths.Count; i++)
            {
                var ro = new RecentlyOpenedRow(Paths[i]);
                ro.Location = new Point(0, ro.Size.Height * i);
                ro.SessionLoaded += SessionLoaded;

                Controls.Add(ro);
            }
        }

        internal void AddPath(string path)
        {
            Paths.Remove(path);

            Paths.Insert(0, path);

            if (Paths.Count > 8)
                Paths.RemoveAt(9);

            Save();

            Init();
        }

        void Save()
        {
            Paths = Paths.Where(n => File.Exists(n)).ToList();

            var x = new XmlSerializer(typeof(List<string>));
            using (var sw = new StreamWriter(Path.Combine(Application.StartupPath, "RecentlyOpened.xml")))
            {
                x.Serialize(sw, Paths);
            }
        }

        void Load()
        {
            if (!File.Exists(Path.Combine(Application.StartupPath, "RecentlyOpened.xml")))
                return;

            var x = new XmlSerializer(typeof(List<string>));
            using (var sr = new StreamReader(Path.Combine(Application.StartupPath, "RecentlyOpened.xml")))
            {
                Paths = (List<string>)x.Deserialize(sr);
            }
        }
    }
}
