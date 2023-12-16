using System;
using System.Windows.Forms;

namespace OpenVTT.UiDesigner.Classes
{
    internal class listBoxItem
    {
        public string Text;
        public string DefaultName;
        public Func<Control> GetControl;
        public int countPressed = 0;
        public Type ItemType;

        public override string ToString() => Text;
    }
}
