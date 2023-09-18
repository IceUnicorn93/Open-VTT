using OpenVTT.Common;
using OpenVTT.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OpenVTT.Editor
{
    internal class TreeViewDisplayItem
    {
        public TreeViewDisplayItemType ItemType { get; set; }
        public string Name { get; set; }
        public List<string> ParentDirectories { get; set; }
        public TreeViewDisplayItem Parent { get; set; }

        public override string ToString()
        {
            Logger.Log("Class: TreeViewDisplayItem | ToString");

            return string.Join("=>", ParentDirectories);
        }

        public List<string> GetLocation(string fileextension = "", bool getFileName = true)
        {
            Logger.Log("Class: TreeViewDisplayItem | GetLocation");

            var pathParts = new List<string>();
            pathParts.Add(Application.StartupPath);
            pathParts.Add("Notes");
            pathParts.AddRange(GetParentPath(this).Select(n => n.Name).ToArray());
            if (getFileName)
                pathParts.Add($"{Name}{fileextension}");
            return pathParts;
        }

        public List<TreeViewDisplayItem> GetParentPath(TreeViewDisplayItem Parent)
        {
            Logger.Log("Class: TreeViewDisplayItem | GetParentPath");

            List<TreeViewDisplayItem> m(TreeViewDisplayItem p)
            {
                var r = new List<TreeViewDisplayItem>();
                if (p != null && p.Parent != null)
                {
                    r.Add(p.Parent);
                    r.AddRange(m(p.Parent));
                }
                return r;
            }

            var ret = m(Parent);
            ret.Reverse();
            return ret;
        }
    }
}
