using CleanerMongoStartUp.Models;
using System.Windows;
using System.Windows.Controls;
using CleanerMongoStartUp.Components;



namespace CleanerMongoStartUp
{

    public partial class EmployeeCard : UserControl
    {
        private string _uid;

        public string uid
        {
            get { return _uid; }
            set { _uid = value; }
        }


        public EmployeeCard()
        {

            InitializeComponent();
        }

        private void onCardLoaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // MessageBox.Show($"Employees Unique ID = ${uid}");
            new ChildWindow().Show();
            //  TestWriteToDB? temp = new();

        }
    }
}
