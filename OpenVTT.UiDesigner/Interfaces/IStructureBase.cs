namespace OpenVTT.UiDesigner.Interfaces
{
    public interface IStructureBase
    {
        string Name { get; set; }

        string Type { get; set; }

        bool SingleValue { get; set; }
    }
}
