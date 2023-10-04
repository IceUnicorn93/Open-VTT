using OpenVTT.Common;
using System;
using System.Windows.Forms;

namespace OpenVTT.Scripting
{
    [Documentation("To use this Object just use Page.Controls.Add(ABC); or Config.XYZ = ABC;", Name = "ScriptHost")]
    public class ScriptHost
    {
        internal bool hasSuccessfullyRun = false;
        internal Exception exception;

        [Documentation("This Object will be added to the SceneControl", IsField =true, DataType = "TabPage", Name = "Page")]
        public TabPage Page = new TabPage();
        [Documentation("This is the ScriptConfig.XML in the Script Directory", IsField = true, DataType = "ScriptConfig", Name = "Config")]
        public ScriptConfig Config;
    }
}
