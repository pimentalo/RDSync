using RDSync.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDSync
{
    public class DeviceDefinition
    {
        public DeviceDefinition()
        {
        }

        /// <summary>
        /// Creates a new DeviceDefinition from an EndPoint
        /// </summary>
        /// <param name="endPoint"></param>
        public DeviceDefinition(Core.EndPoint endPoint)
        {
            Syncer = new Syncer();
            Syncer.Device = endPoint;
            Identifier = new Guid();
        }

        public Guid Identifier { get; set; }

        public Syncer Syncer { get; set; }
    }
}
