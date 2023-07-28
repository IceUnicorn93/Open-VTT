using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace OpenVTT.Scripting
{
    public class ScriptConfig
    {
        public bool isUI = false;

        public string Name = "Sample";
        public string Author = "IceUnicorn / Dustin";
        public string Description = "This is a sample Script";
        public string Version = "1.0.0.0";

        public List<string> File_References = new List<string>()
        {
        };
        public List<string> DLL_References = new List<string>()
        {
        };
        public List<string> Using_References = new List<string>()
        {
        };


        public static void Save()
        {
            string path = @".\ScriptConfig.xml";

            var x = new XmlSerializer(typeof(ScriptConfig));
            using (var sw = new StreamWriter(path))
            {
                x.Serialize(sw, new ScriptConfig());
            }
        }

        public static ScriptConfig Load(string path)
        {
            var ret = new ScriptConfig();

            var x = new XmlSerializer(typeof(ScriptConfig));
            using (var sr = new StreamReader(path))
            {
                ret = (ScriptConfig)x.Deserialize(sr);
            }
            return ret;
        }
    }
}
