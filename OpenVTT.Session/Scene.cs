using OpenVTT.Common;
using OpenVTT.Logging;
using System.Collections.Generic;
using System.Linq;

namespace OpenVTT.Session
{
    [Documentation("to use this Object use var s = new Session();", Name = "Scene")]
    public class Scene
    {
        [Documentation("Name of the Scene", Name = "Name", IsField = true, DataType = "string")]
        public string Name;
        [Documentation("List of Layer-Objects", Name = "Layers", IsField = true, DataType = "List<Layer>")]
        public List<Layer> Layers;

        [Documentation("Constructor", Name = "Scene", IsMethod = true, ReturnType = "Scene")]
        public Scene()
        {
            Logger.Log("Class: Scene | Constructor");

            Name = "Main";

            Layers = new List<Layer>();
        }

        [Documentation("Returns the Layer for the given Parameter", Name = "GetLayer", IsMethod = true, ReturnType = "Layer", Parameters = "int number")]
        public Layer GetLayer(int number)
        {
            Logger.Log("Class: Scene | GetLayer");

            return Layers.SingleOrDefault(n => n.LayerNumber == number);
        }

        [Documentation("Overriden ToString()", Name = "ToString", IsMethod = true, ReturnType = "string")]
        public override string ToString()
        {
            Logger.Log("Class: Scene | ToString");

            return Name;
        }
    }
}
