using OpenVTT.Logging;
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
            Logger.Log("Class: FileDefinition | ToString");

            return $"{Type} - {Name}";
        }
    }
}
