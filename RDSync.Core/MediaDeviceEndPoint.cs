using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDSync.Core
{
    public class MediaDeviceEndPoint : EndPoint
    {
        private MediaDevices.MediaDevice Device { get; }

        internal E Execute<E>(Func<MediaDevices.MediaDevice, E> func)
        {
            lock (Device)
            {
                bool connected = Device.IsConnected;
                try
                {
                    if (!connected)
                        Device.Connect();
                    return func(Device);
                }
                finally
                {
                    if (!connected)
                        Device.Disconnect();
                }
            }
        }

        public string Details { get; set; }

        public override string DeviceIdentifier => Execute(d => Device.PnPDeviceID);

        public MediaDeviceEndPoint(MediaDevices.MediaDevice device) : base(device.FriendlyName, "", "")
        {
            if (device == null) throw new ArgumentNullException("device");
            Device = device;
            Description = Execute((d) => device.Description);
            this.Details = Execute((d) => $"Manufacturer: {d.Manufacturer}:\nModel: {d.Model}\nSN: {d.SerialNumber}\nFW: {d.FirmwareVersion}\nModelUniqueId: {d.ModelUniqueId}\nDeviceId: {d.DeviceId}\nFunctionnalUniqueId: {d.FunctionalUniqueId}\n Protocol: {d.Protocol}");
        }

        public MediaDeviceEndPoint(string deviceIdentifier): this(GetDeviceFromIdentifier(deviceIdentifier))
        {

        }

        private static MediaDevices.MediaDevice? GetDeviceFromIdentifier(string PNPidentifier)
        {
            return MediaDevices.MediaDevice.GetDevices().FirstOrDefault(md => md.PnPDeviceID == PNPidentifier);
        }

        public override IEnumerable<Directory> GetDirectories(Directory? root = null)
        {
            return Execute(d => d.GetDirectories(root?.FullPath).Select(p => new MediaDeviceDirectory(this, p, (MediaDeviceDirectory)root)));
        }
    }
}
