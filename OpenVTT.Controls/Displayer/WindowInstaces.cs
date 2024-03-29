﻿using OpenVTT.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenVTT.Controls.Displayer
{
    internal class WindowInstaces
    {
        internal static InformationDisplayDM InformationDisplayDM;
        internal static InformationDisplayPlayer InformationDisplayPlayer;
        internal static MapPlayer Player;

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
            Player = new MapPlayer();
        }
    }
}
