using OpenVTT.Common;
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

        }

        internal void SetDefaultLocationAndSize()
        {
            Location = default;
            Size = default;
        }
    }
}
