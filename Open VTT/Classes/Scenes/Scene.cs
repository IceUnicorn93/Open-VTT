﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Open_VTT.Classes.Scenes
{
    public class Scene
    {
        public string Name;

        public List<Layer> Layers;

        public Scene()
        {
            Name = "Main";

            Layers = new List<Layer>();
        }

        public Layer GetLayer(int number)
        {
            return Layers.SingleOrDefault(n => n.LayerNumber == number);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
