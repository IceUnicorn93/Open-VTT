using OpenVTT.Common;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace OpenVTT.Scripting
{
    public class ScriptConfig
    {
        [Documentation("True, if TabPage should be added to SceneControl")]
        public bool isUI = false;
        [Documentation("True, if Script should be compiled")]
        public bool isActive = false;

        [Documentation("Name of Script")]
        public string Name = "";
        [Documentation("Name of the Author")]
        public string Author = "";
        [Documentation("Description of the Script")]
        public string Description = "";
        [Documentation("Version of the Script")]
        public string Version = "";

        [Documentation("List of files to load for the Script")]
        public List<string> File_References = new List<string>()
        {
        };
        [Documentation("List of DLL References for the Script")]
        public List<string> DLL_References = new List<string>()
        {
        };
        [Documentation("List of Using References for the Script")]
        public List<string> Using_References = new List<string>()
        {
            "OpenVTT.StreamDeck",
            "OpenVTT.Session",
            "OpenVTT.Settings",
            "OpenVTT.Common",
        };


        internal static void Save(string path)
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

        internal static ScriptConfig Load(string path)
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
