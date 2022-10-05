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

        public override string DeviceIdentifier => Root.FullName;

        public DriveEndPoint(DirectoryInfo root, string name): base(name, "","")
        {
            Root = root;
        }

        public override IEnumerable<Directory> GetDirectories(Directory? root = null)
        {
            return Root.GetDirectories().Select(d => new DriveDirectory(d, null));
        }
    }
}
