using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDSync
{
    internal class DevicesDataContext : INotifyPropertyChanged
    {
        void RefreshDevices()
        {
            Devices = new ObservableCollection<RemovableDevice>();
            foreach(var md in MediaDevices.MediaDevice.GetDevices())
            {
                Devices.Add(new MediaRemovableDevice(md));
            }
        }
        internal DevicesDataContext()
        {
            RefreshDevices();
        }

        ObservableCollection<RemovableDevice> Devices { get; set; } = new ObservableCollection<RemovableDevice>();

        RemovableDevice _RemovableDevice = null;
        RemovableDevice SelectedDevice
        {
            get { return _RemovableDevice; }
            set { _RemovableDevice = value; OnPropertyChanged(nameof(SelectedDevice)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
