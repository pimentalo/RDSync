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

        /// <summary>
        /// Last Write Time of the source
        /// </summary>
        public virtual DateTime? LastWriteTime { get; protected set; }
        /// <summary>
        /// Length of the source file
        /// </summary>
        public virtual long Length { get; protected set; }
        /// <summary>
        /// Triggers the copy:
        /// IsDifferent is initialised during the scanning phase. It can be forced to true (the file will be copied) or false (the file won't be copied) by a FileDetectedEvent handler (see Syncer.FileDetected event)
        /// </summary>
        public bool WillBeCopied { get; set; }

        /// <summary>
        /// Destination Path of the file
        /// </summary>
        public string DestinationPath { get; set; }
    }
}
