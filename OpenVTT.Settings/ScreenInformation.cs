using OpenVTT.Common;

namespace OpenVTT.Settings
{
    [Documentation("To use this Object use var si = new ScreenInformation();")]
    public class ScreenInformation
    {
        [Documentation("DisplayType, see Enums")]
        public DisplayType Display;
        [Documentation("Where the Monitor is Located (Horizontal)")]
        public int PositionX;
        [Documentation("Where the Monitor is located (Vertrical)")]
        public int PositionY;
        [Documentation("Height of the Monitor")]
        public int Height;
        [Documentation("Width of the Monitor")]
        public int Width;
    }
}
