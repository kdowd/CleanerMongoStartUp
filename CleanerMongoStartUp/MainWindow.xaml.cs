using CleanerMongoStartUp.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CleanerMongoStartUp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();

        }

        private void onMainLoaded(object sender, RoutedEventArgs e)
        {
            // make available the Database via static property;
            Connector connector = new Connector();


        }

        private void Employees_View_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}