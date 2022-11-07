using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDSync.Core
{
    public class Helper
    {
        public static IEnumerable<EndPoint> GetEndPoints()
        {
            var result = new List<EndPoint>();

            foreach (var md in MediaDevices.MediaDevice.GetDevices())
            {
                result.Add(new MediaDeviceEndPoint(md));
            }

            foreach (var drive in DriveInfo.GetDrives().Where(d => d.DriveType.HasFlag(DriveType.Removable)))
            {
                result.Add(new Core.DriveEndPoint( drive.RootDirectory, drive.VolumeLabel));
            }
            return result.AsReadOnly();
        }

        /// <summary>
        /// Create an endpoint from its identifier.
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public static EndPoint CreateEndPointFromIdentifier(string identifier)
        {
            EndPoint? result = null;
            if (identifier.StartsWith("Device:"))
            {
                result = new MediaDeviceEndPoint(identifier.Substring("Device:".Length));
            } else if(identifier.StartsWith("Directory:"))
            {
                result = new DriveEndPoint(identifier.Substring("Directory:".Length));
            }
            System.Diagnostics.Debug.Assert(result.DeviceIdentifier == identifier);

            return result;
        }
        
    }
}
