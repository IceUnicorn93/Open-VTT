using System;
using System.Collections.Generic;
using System.Text;

namespace Open_VTT.Classes.Scenes
{
    public class Layer
    {
        public int LayerNumber;
        public string RootPath;
        public string ImagePath;
        public char DirectorySeperator;

        public List<FogOfWar> FogOfWar;

        public Layer()
        {
            LayerNumber = 0;
            ImagePath = string.Empty;
            RootPath = string.Empty;

            FogOfWar = new List<FogOfWar>();
        }
    }
}
