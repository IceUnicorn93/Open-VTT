using OpenVTT.AnimatedMap;
using OpenVTT.Logging;

namespace OpenVTT.Controls.Displayer
{
    internal class WindowInstaces
    {
        internal static InformationDisplayDM InformationDisplayDM;
        internal static InformationDisplayPlayer InformationDisplayPlayer;
        internal static AnimatedMapDisplayer AnimatedMapDisplayer;

        static WindowInstaces()
        {
            Logger.Log("Class: WindowInstaces | Constructor");

            Init();
        }

        internal static void Init()
        {
            Logger.Log("Class: WindowInstaces | Init");

            InformationDisplayDM = new InformationDisplayDM();
            InformationDisplayPlayer = new InformationDisplayPlayer();
            AnimatedMapDisplayer = new AnimatedMapDisplayer();
        }
    }
}
