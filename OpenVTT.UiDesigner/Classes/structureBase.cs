using OpenVTT.UiDesigner.Interfaces;

namespace OpenVTT.UiDesigner.Classes
{
    class structureBase : IStructureBase
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool SingleValue { get; set; }
    }
}
