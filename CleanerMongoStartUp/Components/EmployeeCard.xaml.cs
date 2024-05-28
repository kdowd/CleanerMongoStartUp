using CleanerMongoStartUp.Models;
using System.Windows;
using System.Windows.Controls;
using CleanerMongoStartUp.Components;
using MongoDB.Bson;



namespace CleanerMongoStartUp
{

    public partial class EmployeeCard : UserControl
    {
        private ObjectId _uid;

        public ObjectId uid
        {
            get { return _uid; }
            set { _uid = value; }
        }


        public EmployeeCard()
        {

            InitializeComponent();
        }

        private void OnCardLoaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            // pass though the _id and when window opens perform a DB search
            ChildWindow handle = new(uid);
            handle.Owner = Application.Current.MainWindow as MainWindow;
            handle.Show();

            // Debug
            // MessageBox.Show($"Employees Unique ID = ${uid}");

        }
    }
}
