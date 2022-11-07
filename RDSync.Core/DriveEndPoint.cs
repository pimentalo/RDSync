using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDSync.Core
{
    public class DriveEndPoint : EndPoint
    {
        private DirectoryInfo Root { get; }

        public override string DeviceIdentifier => "Directory:"+Root.FullName;

        public DriveEndPoint(DirectoryInfo root, string name): base(name, "","")
        {
            Root = root;
        }

        public DriveEndPoint(string directory, string? name = null): this (new DirectoryInfo(directory), name ?? directory)
        {
        }

        public override IEnumerable<Directory> GetDirectories(Directory? root = null)
        {   try
            {
                return Root.GetDirectories().Select(d => new DriveDirectory(d, null));
            }
            catch (System.IO.IOException e)
            {
                throw new DeviceNotReadyException(e);
            }
        }
    }
}
