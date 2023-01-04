using Open_VTT.Forms;
using Open_VTT.Forms.Popups.Displayer;

namespace Open_VTT.Classes
{
    class WindowInstaces
    {
        internal static InformationDisplayDM InformationDisplayDM;
        internal static InformationDisplayPlayer InformationDisplayPlayer;
        internal static MapPlayer Player;

        static WindowInstaces()
        {
            InformationDisplayDM = InformationDisplayDM.GetOrCreate();
            InformationDisplayPlayer = InformationDisplayPlayer.GetOrCreate();
            Player = MapPlayer.GetOrCreate();
        }
    }
}
