using OpenVTT.Common;
using System.Collections.Generic;

namespace OpenVTT.Session
{
    [Documentation("To use this object use var l = new Layer();")]
    public class Layer
    {
        [Documentation("If this layer uses an Image, true, for an animated Map, false")]
        public bool IsImageLayer = true;
        [Documentation("Number of the Layer")]
        public int LayerNumber;
        [Documentation("Root Path of the Session-Folder")]
        public string RootPath;
        [Documentation("Path of Image to use, for Example C:\\SessionFolder\\Imgaes\\test.png")]
        public string ImagePath;
        [Documentation("Character by witch the Folders are Seperated. Either \\ or /")]
        public char DirectorySeperator;

        [Documentation("List of FogOfWar-Objects")]
        public List<FogOfWar.FogOfWar> FogOfWar;

        [Documentation("Constructor")]
        public Layer()
        {
            LayerNumber = 0;
            ImagePath = string.Empty;
            RootPath = string.Empty;

            FogOfWar = new List<FogOfWar.FogOfWar>();
        }
    }
}
