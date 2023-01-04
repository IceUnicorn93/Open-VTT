using Open_VTT.Other;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Open_VTT.Classes.UI
{
    internal class TreeViewDisplayItem
    {
        public TreeViewDisplayItemType ItemType { get; set; }
        public string Name { get; set; }
        public List<string> ParentDirectories { get; set; }
        public TreeViewDisplayItem Parent { get; set; }

        public override string ToString()
        {
            return string.Join("=>", ParentDirectories);
        }

        public List<string> GetLocation(string fileextension = "", bool getFileName = true)
        {
            var pathParts = new List<string>();
            //pathParts.Add(Session.Values.SessionFolder);
            pathParts.Add(Application.StartupPath);
            pathParts.Add("Notes");
            pathParts.AddRange(GetParentPath(this).Select(n => n.Name).ToArray());
            if(getFileName)
                pathParts.Add($"{Name}{fileextension}");
            return pathParts;
        }

        public List<TreeViewDisplayItem> GetParentPath(TreeViewDisplayItem Parent)
        {
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
