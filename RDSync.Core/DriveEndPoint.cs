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
        public DriveEndPoint(string name, DirectoryInfo root): base(name, "","")
        {
            Root = root;
        }

        public override IEnumerable<Directory> GetDirectories(Directory? root = null)
        {
            return Root.GetDirectories().Select(d => new DriveDirectory(d, null));
        }
    }
}
