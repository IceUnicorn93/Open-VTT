﻿using Newtonsoft.Json;
using OpenVTT.Common;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OpenVTT.Scripting
{
    [Documentation("To use this Object just use Page.Controls.Add(ABC); or Config.XYZ = ABC;", Name = "ScriptHost")]
    public class ScriptHost
    {
        internal bool hasSuccessfullyRun = false;
        internal Exception exception;
        internal string path;

        [Documentation("Action to set the Text in the Artwork Display", IsField = true, DataType = "Action<string>", Name = "DisplayArtworkText", IsStatic =true)]
        public static Action<string> DisplayArtworkText;
        [Documentation("Action to set the Image in the Artwork Display", IsField = true, DataType = "Action<Image>", Name = "DisplayArtworkImage", IsStatic = true)]
        public static Action<Image> DisplayArtworkImage;

        [Documentation("This Object will be added to the SceneControl", IsField =true, DataType = "TabPage", Name = "Page")]
        public TabPage Page = new TabPage();
        [Documentation("This is the ScriptConfig.XML in the Script Directory", IsField = true, DataType = "ScriptConfig", Name = "Config")]
        public ScriptConfig Config;

        [Documentation("Saves an Object in JSON Format (for the Scripts)", IsMethod = true, DataType = "void", Name = "SaveData<T>", Parameters = "string path, T instance", IsStatic = true)]
        public static void SaveData<T>(string path, T instance)
        {
            var jsonText = JsonConvert.SerializeObject(instance);
            File.WriteAllText(path, jsonText);
        }

        [Documentation("Loads an Object in JSON Format (for the Scripts)", IsMethod = true, DataType = "T", Name = "LoadData<T>", Parameters = "string path", IsStatic = true)]
        public static T LoadData<T>(string path)
        {
            var text = File.ReadAllText(path);
            var ret = JsonConvert.DeserializeObject<T>(text);
            return ret;
        }
    }
}
