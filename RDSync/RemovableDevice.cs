using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDSync
{

    /// <summary>
    /// Represents a removable device (media device)
    /// </summary>
    internal class RemovableDevice
    {
        /// <summary>
        /// This Id must persist between sessions
        /// </summary>
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        public IEnumerable<string> ListFiles()
        {
            throw new NotImplementedException();
        }
    }

    internal class MediaRemovableDevice : RemovableDevice
    {
        internal MediaRemovableDevice(MediaDevices.MediaDevice device)
        {
            throw new NotImplementedException();
        }
    }
}
