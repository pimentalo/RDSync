using RDSync.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDSync
{
    public class SyncerDefinition: INotifyPropertyChanged
    {
        public SyncerDefinition()
        {
            Identifier = Guid.NewGuid();
            _Name = "Nouvelle copie de périphérique";
        }


        public Guid Identifier { get; set; }


        public Syncer? Syncer { get; set; } = null;

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged(nameof(Name)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
