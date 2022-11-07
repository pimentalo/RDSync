using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDSync
{
    /// <summary>
    /// Syncer Windows Datacontext: syncer name, rules.
    /// Operation to map to a SyncerDefinition back and forth
    /// </summary>
    public class DeviceWindowDataContext : INotifyPropertyChanged
    {
        private SyncerDefinition _syncerDefinition;
        public SyncerDefinition SyncerDefinition => _syncerDefinition;


        public DeviceWindowDataContext(SyncerDefinition syncerDef)
        {
            foreach (var d in Core.Helper.GetEndPoints())
            {
                Devices.Add(d);
            }

            _syncerDefinition = syncerDef;
            _name = syncerDef.Name;
            syncerDef?.Syncer?.Rules.ToList().ForEach(s => Rules.Add(s));

            if (syncerDef.Syncer.EndPointIdentifier != null)
            {
                this._SelectedDevice = Devices.FirstOrDefault(d => d.DeviceIdentifier == syncerDef.Syncer.EndPointIdentifier);
            }
        }

        // Inject modifications in syncer definition
        internal void Accept()
        {
            SyncerDefinition.Name = Name;
            foreach (var r in Rules)
            {
                if (!SyncerDefinition.Syncer.Rules.Contains(r))
                {
                    SyncerDefinition.Syncer.Rules.Add(r);
                }
            }

            foreach (var r in SyncerDefinition.Syncer.Rules.ToList())
            {
                if (!Rules.Contains(r))
                {
                    SyncerDefinition.Syncer.Rules.Remove(r);
                }
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public ObservableCollection<Core.EndPoint> Devices { get; set; } = new ObservableCollection<Core.EndPoint>();
        private Core.EndPoint? _SelectedDevice = null;
        public Core.EndPoint? SelectedDevice
        {
            get { return _SelectedDevice; }
            set { _SelectedDevice = value; OnPropertyChanged(nameof(SelectedDevice)); }
        }



        public ObservableCollection<Core.Rule> Rules { get; set; } = new ObservableCollection<Core.Rule>();
        private Core.Rule? _SelectedRule = null;
        public Core.Rule? SelectedRule
        {
            get { return _SelectedRule; }
            set { _SelectedRule = value; OnPropertyChanged(nameof(SelectedRule)); }
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
