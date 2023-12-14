using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenVTT.UiDesigner
{
    class structureBase : IStructureBase
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool SingleValue { get; set; }
    }
}
