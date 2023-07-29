using OpenVTT.Common;
using System.Windows.Forms;

namespace OpenVTT.Scripting
{
    [Documentation("To use this Object just use Page.Controls.Add(ABC); or Config.XYZ = ABC;")]
    public class ScriptHost
    {
        [Documentation("TabPage-Object, this Object will be added to the SceneControl")]
        public TabPage Page = new TabPage();
        [Documentation("ScriptConfig-Object, this is the ScriptConfig.XML in the Script Directory")]
        public ScriptConfig Config;
    }
}
