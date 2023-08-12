using OpenVTT.Common;

namespace OpenVTT.Settings
{
    [Documentation("To use this Object use var xc = new XmlColor();", Name = "XmlColor")]
    public class XmlColor
    {
        [Documentation("Alpha-Channel of the Color", Name = "Alpha", IsField = true, DataType = "int")]
        public int Alpha;
        [Documentation("Red-Channel of the Color", Name = "Red", IsField = true, DataType = "int")]
        public int Red;
        [Documentation("Green-Channel of the Color", Name = "Green", IsField = true, DataType = "int")]
        public int Green;
        [Documentation("Blue-Channel of the Color", Name = "Blue", IsField = true, DataType = "int")]
        public int Blue;
    }
}
