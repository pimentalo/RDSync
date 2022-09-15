using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDSync.Core
{
    public abstract class File
    {
        public File(string name)
        {
            Name = name;
        }
        public virtual string Name { get; set; }
        public abstract System.IO.Stream GetStream();
    }
}
