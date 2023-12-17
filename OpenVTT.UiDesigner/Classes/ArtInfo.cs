using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OpenVTT.UiDesigner.Classes
{
    public  class ArtInfo : INotifyPropertyChanged
    {
        public Action pChange;

        public event PropertyChangedEventHandler PropertyChanged;

        private string _name;
        public string Name { get => _name; set { _name = value; NotifyPropertyChanged(); } }

        public string Path { get; set; }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            pChange?.Invoke();
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
