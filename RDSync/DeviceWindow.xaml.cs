using RDSync.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RDSync
{
    /// <summary>
    /// Logique d'interaction pour DeviceWindow.xaml
    /// </summary>
    public partial class DeviceWindow : Window
    {
        /// <summary>
        /// Strongly typed datacontext
        /// </summary>
        internal DeviceWindowDataContext StronglyTypedDataContext => (DeviceWindowDataContext)DataContext;
        internal SyncerDefinition SyncerDefinition => StronglyTypedDataContext.SyncerDefinition;

        public DeviceWindow()
        {
            InitializeComponent();

            this.DataContext = new DeviceWindowDataContext(
                new SyncerDefinition() { Syncer = new Syncer()});
        }

        public DeviceWindow(SyncerDefinition syncerDefinition): this()
        {
            this.DataContext = new DeviceWindowDataContext(syncerDefinition);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (StronglyTypedDataContext?.SelectedDevice != null && !String.IsNullOrWhiteSpace(StronglyTypedDataContext.Name))
            {
                StronglyTypedDataContext.Accept();
                DialogResult = true;
                Close();
            }
        }

        private void btnAddRule_Click(object sender, RoutedEventArgs e)
        {
            StronglyTypedDataContext.Rules.Add(new Core.Rule() { FileFilter = "*.*", Options= Rule.TransferOptions.FromDeviceToLocal});
        }
    }
}
