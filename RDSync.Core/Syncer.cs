using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDSync.Core
{
    /// <summary>
    /// Syncer is a Device and a list of sync rules + some sync elements
    /// </summary>
    public class Syncer
    {
        public string FriendlyName { get; set; } = "";

        public TimeSpan? Periodicity { get; set; }
        public DateTime? LastExecution { get; set; }
        public bool Automatic { get; set; }

        public EndPoint? Device { get; set; }
        public IList<Rule> Rules { get; set; } = new List<Rule>();

        public Syncer()
        {
        }

        public void Execute()
        {
            foreach(var r in Rules)
            {
                r.Execute(Device!);
            }
        }
      
    }
}
