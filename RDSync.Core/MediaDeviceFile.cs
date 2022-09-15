using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDSync.Core
{
    public class MediaDeviceFile : File
    {
        private MediaDeviceEndPoint Device { get; }
        public MediaDeviceFile(MediaDeviceEndPoint device, string name): base(name)
        {
            Device = device;
        }

        public override Stream GetStream()
        {
            throw new NotImplementedException();
        }
    }
}
