using System.ComponentModel;
using System.Windows;

namespace CleanerMongoStartUp.Components
{
    public partial class ChildWindow : Window
    {
        public ChildWindow()
        {
            InitializeComponent();
        }

        private void ChildWindow_Closing(object sender, CancelEventArgs e)
        {

            //var result = MessageBox.Show("Close Window?", "Application Shutdown Sample", MessageBoxButton.YesNo,
            //    MessageBoxImage.Question);
            //e.Cancel = (result == MessageBoxResult.No);
        }

        private void ChildWindow_Closed(object sender, EventArgs e)
        {

        }
    }
}
