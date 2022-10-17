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
    /// Logique d'interaction pour SelectDevice.xaml
    /// </summary>
    public partial class SelectDevice : Window
    {
        public SelectDevice()
        {
            InitializeComponent();

           
            foreach(var s in Core.Helper.GetEndPoints())
            {
                EndPoints.Add(s);
            }

            this.DataContext = this;
        }

        public ObservableCollection<Core.EndPoint> EndPoints { get; set; } = new ObservableCollection<Core.EndPoint>();
    }
}
