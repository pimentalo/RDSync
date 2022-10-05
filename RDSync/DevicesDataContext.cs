using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDSync
{
    internal class DevicesDataContext : INotifyPropertyChanged
    {
    
        internal DevicesDataContext()
        {
        }

        public ObservableCollection<DeviceDefinition> Devices { get; set; } = new ObservableCollection<DeviceDefinition>();

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
