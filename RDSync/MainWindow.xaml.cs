using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RDSync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Strong typed datacontext
        /// </summary>
        private MainWindowDataContext ThisContext => (MainWindowDataContext)this.DataContext;

        public MainWindow()
        {
            InitializeComponent();

            // Load datacontext
            try
            {
                DataContext = ApplicationData.LoadData();
            } catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError($"Error loading data file: {ex.Message}");
                DataContext = new MainWindowDataContext();
                ApplicationData.SaveData(ThisContext);
            }
        }


        private string GetDevicesFileName { 
            get
            {
                return System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Devices.xml");
            } 
        }

        public void LoadDevices()
        {
            try
            {
                throw new NotImplementedException();

            } catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError("Error reading configuration: {0}", e.ToString());
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var devWindow = new DeviceWindow();
            devWindow.Owner = this;
            if (devWindow.ShowDialog() == true)
            {
                ThisContext.UpsertSyncerDefinition(devWindow.SyncerDefinition);
                ApplicationData.SaveData(this.ThisContext);
            }
        }
    }
}
