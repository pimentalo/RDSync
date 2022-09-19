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

        public override IEnumerable<Directory> GetDirectories()
        {
            return Device.Execute(d => d.GetDirectories(FullPath).Select(subdir => new MediaDeviceDirectory(Device, subdir, this)));
        }


        public override IEnumerable<File> GetFiles()
        {
            return Device.Execute(d => d.GetFiles(FullPath).Select(f => new MediaDeviceFile(Device, f)));
        }

    }
}
