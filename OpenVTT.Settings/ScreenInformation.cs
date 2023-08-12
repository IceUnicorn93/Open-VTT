using OpenVTT.Common;

namespace OpenVTT.Settings
{
    [Documentation("To use this Object use var si = new ScreenInformation();", Name = "ScreenInformation")]
    public class ScreenInformation
    {
        [Documentation("DisplayType, see Enums", Name = "Display", IsField = true, DataType = "DisplayType")]
        public DisplayType Display;
        [Documentation("Where the Monitor is Located (Horizontal)", Name = "PositionX", IsField = true, DataType = "int")]
        public int PositionX;
        [Documentation("Where the Monitor is located (Vertrical)", Name = "PositionY", IsField = true, DataType = "int")]
        public int PositionY;
        [Documentation("Height of the Monitor", Name = "Height", IsField = true, DataType = "int")]
        public int Height;
        [Documentation("Width of the Monitor", Name = "Width", IsField = true, DataType = "int")]
        public int Width;
    }
}
