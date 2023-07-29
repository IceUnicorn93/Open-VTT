using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace OpenVTT.Scripting
{
    public class ScriptConfig
    {
        public bool isUI = false;
        public bool isActive = false;

        public string Name = "";
        public string Author = "";
        public string Description = "";
        public string Version = "";

        public List<string> File_References = new List<string>()
        {
        };
        public List<string> DLL_References = new List<string>()
        {
        };
        public List<string> Using_References = new List<string>()
        {
        };


        public static void Save(string path)
        {
            var config = new ScriptConfig();

            config.isUI = false;
            config.isActive = false;

            config.Name = "Sample";
            config.Author = "IceUnicorn / Dustin";
            config.Description = "This is a sample Script. Its deactivated (isActive=false) and woulnd't be shown in the UI (isUI=false)";
            config.Version = "1.0.0.0";

            config.File_References = new List<string>()
            {
                "Main.txt"
            };
            config.DLL_References = new List<string>()
            {
                "PathToSample.DLL"
            };
            config.Using_References = new List<string>()
            {
                "System",
                "System.XML"
            };

            var x = new XmlSerializer(typeof(ScriptConfig));
            using (var sw = new StreamWriter(path))
            {
                x.Serialize(sw, config);
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
