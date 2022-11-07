using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RDSync.Core
{
    public class Rule
    {
        /// <summary>
        /// Path on the device
        /// </summary>
        public string DevicePath { get; set; }

        /// <summary>
        /// File filter
        /// </summary>
        public string FileFilter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LocalPath { get; set; }

        public bool IsRecursive { get; set; }

        public enum TransferOptions
        {
            FromDeviceToLocal = 1,
            FromLocalToDevice = 2,
            ForceOverwriteIfMoreRecent = 4,
            Both = FromDeviceToLocal | FromLocalToDevice
        }

        public TransferOptions Options { get; set; }

        /// <summary>
        /// Execute the rule
        /// </summary>
        /// <param name="device"></param>
        internal virtual void Execute(Syncer syncer)
        {
            if ((Options & TransferOptions.FromDeviceToLocal) == TransferOptions.FromDeviceToLocal)
            {
                CopyDirectoryFromDeviceToLocal(syncer, this, this.DevicePath, this.FileFilter, this.LocalPath);
            }
        }

        public static string FilterToRegex(string filter)
        {
            var result = filter.Replace(".", "\\.").Replace("*", ".*");
            return result;
        }

        private static void CopyDirectoryFromDeviceToLocal(Syncer syncer, Rule rule, string dir, string filter, string localDir)
        {
            var regex = FilterToRegex(filter);
            var root = syncer.Device.GetDirectory(dir);

            foreach (var file in root.GetFiles().Where(f => System.Text.RegularExpressions.Regex.IsMatch(f.Name, regex)))
            {
                // DIRTY
                var localFileName = rule.LocalPath;

                foreach (var p in dir.Split("/"))
                {
                    localFileName = Path.Combine(localFileName, p);
                }
                localFileName = Path.Combine(localFileName, file.Name);

                // Check changes
                if (System.IO.File.Exists(localFileName))
                {
                    var fi = new FileInfo(localFileName);
                    file.WillBeCopied = false;
                    if ((fi.Length != file.Length) || (fi.LastWriteTime <= file.LastWriteTime))
                    {
                        file.WillBeCopied = true;
                    }
                } else
                {
                    file.WillBeCopied = true;
                }

                syncer.RaiseFileDetected(rule, file);



                if (file.WillBeCopied)
                {
                    syncer.RaiseFileCopied(rule, file);

                    using (var s = file.GetStream())
                    {
                    }
                }
            }

            foreach (var d in root.GetDirectories())
            {
                CopyDirectoryFromDeviceToLocal(syncer, rule, d.FullPath, filter, localDir);
            }

        }
    }
}
