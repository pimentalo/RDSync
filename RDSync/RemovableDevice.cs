using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDSync
{

    /// <summary>
    /// Represents a removable device (media device) in the data context
    /// </summary>
    internal abstract class RemovableDevice
    {
        /// <summary>
        /// This Id must persist between sessions
        /// </summary>
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        /// <summary>
        /// Directories: having them in an observable collection make them easy to load asyncchronously
        /// </summary>
        public abstract ReadOnlyObservableCollection<Directory> Directories { get; set; }
       
        public IEnumerable<string> GetFiles(string path)
        {
            return GetFiles(path, null);
        }
        public abstract IEnumerable<string> GetFiles(string path, string? filter);
    }

    internal class Directory
    { 
        public string Name { get; set; }
        public IEnumerable<Directory> Directories { get; set; }
    }

    internal class MediaRemovableDevice : RemovableDevice
    {
        private MediaDevices.MediaDevice Device { get; }
        internal MediaRemovableDevice(MediaDevices.MediaDevice device)
        {
            if (device == null) throw new ArgumentNullException("device");
            Device = device;
            this.Id = device.DeviceId;
            this.Name = device.FriendlyName;
            this.Description = device.ToString();
            try
            {
                device.Connect(MediaDevices.MediaDeviceAccess.GenericRead, MediaDevices.MediaDeviceShare.Read);

                this.Details = $"Manufacturer: {device.Manufacturer}: \tModel: {device.Model}\tSN: {device.SerialNumber}\tFW: {device.FirmwareVersion}\t Protocol: {device.Protocol}";
            }
            finally
            {
                device.Disconnect();
            }
        }

        public override string ToString()
        {
            return Name;
        }

        private IEnumerable<Directory> _Directories;

        public  IEnumerable<string> GetDirectories(string root)
        {
            try
            {
                Device.Connect();
                return Device.GetDirectories(root);
            }
            finally
            {
                Device.Disconnect();
            }
        }

        public override IEnumerable<Directory> Directories { 
            get
            {

                return
            } 
            set => throw new NotImplementedException();
        }

        public override IEnumerable<string> GetFiles(string path, string? filter)
        {
            try
            {
                Device.Connect();
                return Device.GetFiles(path, filter);
            }
            finally
            {
                Device.Disconnect();
            }
        }
    }
}
