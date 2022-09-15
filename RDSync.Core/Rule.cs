using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDSync.Core
{
    public class Rule
    {
        public string SourcePath { get; set; }
        public bool IsRecursive { get; set; }

        public string DestPath { get; set; }
        public string SourceFilter { get; set; }
    }
}
