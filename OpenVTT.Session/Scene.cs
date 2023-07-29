using OpenVTT.Common;
using System.Collections.Generic;
using System.Linq;

namespace OpenVTT.Session
{
    [Documentation("to use this Object use var s = new Session();")]
    public class Scene
    {
        [Documentation("Name of the Scene")]
        public string Name;
        [Documentation("List of Layer-Objects")]
        public List<Layer> Layers;

        [Documentation("Constructor")]
        public Scene()
        {
            Name = "Main";

            Layers = new List<Layer>();
        }

        [Documentation("Returns the Layer for the given Parameter")]
        public Layer GetLayer(int number)
        {
            return Layers.SingleOrDefault(n => n.LayerNumber == number);
        }

        [Documentation("Overriden ToString()")]
        public override string ToString()
        {
            return Name;
        }
    }
}
