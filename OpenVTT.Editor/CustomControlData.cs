using OpenVTT.Common;
using OpenVTT.Logging;
using System.Drawing;

namespace OpenVTT.Editor
{
    public class CustomControlData
    {
        public Point Location;
        public Size Size;
        public string Text;
        public string Name;
        public CustomControlType ControlType;

        public CustomControlData()
        {
            Logger.Log("Class: CustomControlData | Constructor");
        }

        internal void SetDefaultLocationAndSize()
        {
            Logger.Log("Class: CustomControlData | SetDefaultLocationAndSize");

            Location = default;
            Size = default;
        }
    }
}
