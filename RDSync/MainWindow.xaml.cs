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
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DevicesDataContext();

     //       LoadDevices();
        }


        private string GetDevicesFileName { get
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
            devWindow.Show();
        }
    }
}
