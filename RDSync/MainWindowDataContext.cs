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
    /// Main data context. Contains all SyncerDefinitions
    /// </summary>
    public class MainWindowDataContext : INotifyPropertyChanged
    {
    
        public MainWindowDataContext()
        {
        }

        /// <summary>
        /// Add a definition if not present
        /// </summary>
        /// <param name="sd"></param>
        public void UpsertSyncerDefinition(SyncerDefinition sd)
        {
            if (Syncers.IndexOf(sd)< 0)
            {
                Syncers.Add(sd);
            }
        }

        public ObservableCollection<SyncerDefinition> Syncers { get; set; } = new ObservableCollection<SyncerDefinition>();

        private SyncerDefinition? _SelectedSyncer = null;
        public SyncerDefinition? SelectedSyncer
        {
            get { return _SelectedSyncer; }
            set { _SelectedSyncer = value; OnPropertyChanged(nameof(SelectedSyncer)); }
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
