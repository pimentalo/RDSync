using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDSync.Core
{
    public abstract class EndPoint
    {
        protected EndPoint(string name, string uniqueId, string description)
        {
            Name = name; UniqueId = uniqueId; Description = description;
        }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string UniqueId { get; protected set; }

        public abstract IEnumerable<Directory> GetDirectories(Directory? root = null);
    }
}
