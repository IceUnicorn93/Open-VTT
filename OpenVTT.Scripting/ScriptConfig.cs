using OpenVTT.Common;
using OpenVTT.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace OpenVTT.Scripting
{
    [Documentation("To use this Object just use var sc = new ScriptConfig();", Name = "ScriptConfig")]
    public class ScriptConfig
    {
        [Documentation("True, if TabPage should be added to SceneControl", IsField = true, Name = "isUI", DataType = "bool")]
        public bool isUI = false;
        [Documentation("True, if Script should be compiled", IsField = true, Name = "isActive", DataType = "bool")]
        public bool isActive = false;

        [Documentation("Name of Script", IsField = true, Name = "Name", DataType = "string")]
        public string Name = "";
        [Documentation("Name of the Author", IsField = true, Name = "Author", DataType = "string")]
        public string Author = "";
        [Documentation("Description of the Script", IsField = true, Name = "Description", DataType = "string")]
        public string Description = "";
        [Documentation("Version of the Script", IsField = true, Name = "Version", DataType = "string")]
        public string Version = "";

        [Documentation("List of files to load for the Script", IsField = true, Name = "File_References", DataType = "List<string>")]
        public List<string> File_References = new List<string>()
        {
        };
        [Documentation("List of DLL References for the Script", IsField = true, Name = "DLL_References", DataType = "List<string>")]
        public List<string> DLL_References = new List<string>()
        {
        };
        [Documentation("List of Using References for the Script", IsField = true, Name = "Using_References", DataType = "List<string>")]
        public List<string> Using_References = new List<string>()
        {
            "OpenVTT.StreamDeck",
            "OpenVTT.Session",
            "OpenVTT.Settings",
            "OpenVTT.Common",
            "System.Windows.Forms",
            "System.Drawing",
        };


        internal static void SaveDefault(string path)
        {
            Logger.Log("Class: ScriptConfig | Static Save");

            var config = new ScriptConfig();

            config.isUI = false;
            config.isActive = false;

            config.Name = "Sample";
            config.Author = "IceUnicorn / Dustin";
            config.Description = "This is a sample Script. Its deactivated (isActive=false) and woulnd't be shown in the UI (isUI=false)";
            config.Version = "1.0.0.0";

            config.File_References = new List<string>()
            {
                "Main.cs"
            };
            config.DLL_References = new List<string>()
            {
                "SampleDllIn.dll"
            };
            config.Using_References = new List<string>()
            {
                "System"
            };

            var x = new XmlSerializer(typeof(ScriptConfig));
            using (var sw = new StreamWriter(path))
            {
                x.Serialize(sw, config);
            }
        }

        internal void Save(string path)
        {
            Logger.Log("Class: ScriptConfig | Static Save");

            var x = new XmlSerializer(typeof(ScriptConfig));
            using (var sw = new StreamWriter(path))
            {
                x.Serialize(sw, this);
            }
        }

        internal static ScriptConfig Load(string path)
        {
            Logger.Log("Class: ScriptConfig | Load");

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
