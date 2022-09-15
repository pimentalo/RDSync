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
            Devices.Clear();
            foreach (var md in MediaDevices.MediaDevice.GetDevices())
            {
                Devices.Add(new Core.MediaDeviceEndPoint(md));
            }
        }
        internal DevicesDataContext()
        {
            RefreshDevices();
        }

        public ObservableCollection<Core.EndPoint> Devices { get; set; } = new ObservableCollection<Core.EndPoint>();

        private Core.EndPoint _RemovableDevice = null;
        public Core.EndPoint SelectedDevice
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
