using OpenVTT.Common;
using OpenVTT.Logging;
using System.Collections.Generic;

namespace OpenVTT.Session
{
    [Documentation("To use this object use var l = new Layer();", Name = "Layer")]
    public class Layer
    {
        [Documentation("If this layer uses an Image, true, for an animated Map, false", Name = "IsImageLayer", IsField = true, DataType = "bool")]
        public bool IsImageLayer = true;
        [Documentation("Number of the Layer", Name = "LayerNumber", IsField = true, DataType = "int")]
        public int LayerNumber;
        [Documentation("Root Path of the Session-Folder", Name = "RootPath", IsField = true, DataType = "string")]
        public string RootPath;
        [Documentation("Path of Image to use, for Example C:\\SessionFolder\\Imgaes\\test.png", Name = "ImagePath", IsField = true, DataType = "string")]
        public string ImagePath;
        [Documentation("Character by witch the Folders are Seperated. Either \\ or /", Name = "DirectorySeperator", IsField = true, DataType = "char")]
        public char DirectorySeperator;

        [Documentation("List of FogOfWar-Objects", Name = "FogOfWar", IsField = true, DataType = "List<FogOfWar>")]
        public List<FogOfWar.FogOfWar> FogOfWar = new List<FogOfWar.FogOfWar>();

        [Documentation("Constructor", Name = "Layer", IsMethod = true, ReturnType = "Layer")]
        public Layer()
        {
            Logger.Log("Class: Layer | Constructor");

            LayerNumber = 0;
            ImagePath = string.Empty;
            RootPath = string.Empty;

            FogOfWar = new List<FogOfWar.FogOfWar>();
        }
    }
}
