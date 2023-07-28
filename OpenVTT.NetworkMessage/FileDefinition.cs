using System;
using System.Xml.Serialization;

namespace OpenVTT.NetworkMessage
{
    public class FileDefinition
    {
        public string Name;
        public string Type;
        public DateTime LastWriteTime;

        public override string ToString()
        {
            return $"{Type} - {Name}";
        }
    }
}
