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
        public virtual void Execute(EndPoint device)
        {
            if ((Options & TransferOptions.FromDeviceToLocal) == TransferOptions.FromDeviceToLocal)
            {
                CopyDirectoryFromDeviceToLocal(device, this.DevicePath, this.FileFilter, this.LocalPath);
            }
        }

        public static string FilterToRegex(string filter)
        {
            var result = filter.Replace(".", "\\.").Replace("*", ".*");
            return result;
        }

        private static void CopyDirectoryFromDeviceToLocal(EndPoint device, string dir, string filter, string localDir)
        {
            var regex = FilterToRegex(filter);
            var root = device.GetDirectory(dir);

            foreach (var d in root.GetDirectories())
            {
                foreach (var file in d.GetFiles().Where(f => System.Text.RegularExpressions.Regex.IsMatch(f.Name, regex)))
                {
                    using (var s = file.GetStream())
                    {
                      //  var localFilename = 
                    }
                }
            }

        }
    }
}
