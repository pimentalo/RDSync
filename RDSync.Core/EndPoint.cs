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

        public virtual Directory GetDirectory(string path)
        {
            var elements = path.Split("/".ToCharArray());
            Directory? dir = null;
            foreach(var element in elements)
            {
                if (dir == null)
                {
                    dir = GetDirectories().FirstOrDefault(d => d.Name == element);
                } else
                {
                    dir = dir.GetDirectories().FirstOrDefault(d => d.Name == element);
                }
            }
            return dir;
        }

        public abstract IEnumerable<Directory> GetDirectories(Directory? root = null);
    }
}
