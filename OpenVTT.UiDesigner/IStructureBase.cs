using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OpenVTT.UiDesigner
{
    public interface IStructureBase
    {
        string Name { get; set; }

        string Type { get; set; }

        bool SingleValue { get; set; }
    }
}
