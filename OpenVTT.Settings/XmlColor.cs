using OpenVTT.Common;

namespace OpenVTT.Settings
{
    [Documentation("To use this Object use var xc = new XmlColor();")]
    public class XmlColor
    {
        [Documentation("Alpha-Channel of the Color")]
        public int Alpha;
        [Documentation("Red-Channel of the Color")]
        public int Red;
        [Documentation("Green-Channel of the Color")]
        public int Green;
        [Documentation("Blue-Channel of the Color")]
        public int Blue;
    }
}
