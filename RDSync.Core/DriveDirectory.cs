using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDSync.Core
{
    public class DriveDirectory : Directory
    {
        private DirectoryInfo Root { get; }
        public DriveDirectory(DirectoryInfo d, Directory parent): base("", parent)
        {
            Root = d;
        }
        public override IEnumerable<Directory> GetDirectories()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<File> GetFiles()
        {
            throw new NotImplementedException();
        }
    }
}
