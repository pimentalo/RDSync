using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDSync.Core
{
    public class MediaDeviceDirectory : Directory
    {
        private MediaDeviceEndPoint Device { get; }
        public MediaDeviceDirectory(MediaDeviceEndPoint device, string name, MediaDeviceDirectory parent): base(name, parent)
        {
            Device = device;
        }
        public override IEnumerable<File> GetFiles()
        {
            return Device.Execute(d => d.GetFiles(FullPath).Select(f => new MediaDeviceFile(Device, f)));
        }
    }
}
