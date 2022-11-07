using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDSync.Core
{
    public class MediaDeviceFile : File
    {
        private MediaDeviceEndPoint Device { get; }
        public MediaDeviceFile(MediaDeviceEndPoint device, string name): base(name)
        {
            Device = device;
        }

        public override Stream GetStream()
        {
            throw new NotImplementedException();
        }

        private bool _InfosLoaded = false;

        private void LoadInfos()
        {
            var infos = Device.Execute(d => d.GetFileInfo(this.Name));
            base.LastWriteTime = infos.LastWriteTime;
            base.Length = ((long)infos.Length);
        }

        public override DateTime? LastWriteTime
        {
            get
            {
                if (!_InfosLoaded)
                {
                    LoadInfos();
                }
                return base.LastWriteTime;
            }
        }

        public override long Length
        {
            get
            {
                if (!_InfosLoaded)
                {
                    LoadInfos();
                }
                return base.Length;
            }
        }
    }
}
