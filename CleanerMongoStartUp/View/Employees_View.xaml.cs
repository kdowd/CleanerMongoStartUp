using CleanerMongoStartUp.Models;
using MongoConnect.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CleanerMongoStartUp.View
{
    public partial class Employees_View : UserControl
    {
        public Employees_View()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Connector._database != null)
            {
                MongoDatabaseBase db = Connector._database;
                var MyFilterFindAll = new BsonDocument();

                // get results - always returns an IMongoCollection
                IMongoCollection<Employees> results = db.GetCollection<Employees>("employees");

                // convert into a data structure we can loop through
                List<Employees> employeesList = results
                    .Find(MyFilterFindAll)
                    .SortBy(x => x.LastName)
                    .ToList();



                if (employeesList.Count > 0)
                {

                    // and loop
                    foreach (Employees employee in employeesList)
                    {

                        EmployeeCard temp = new EmployeeCard();

                        temp.EmployeeFirstName.Text = employee.FirstName ?? "";
                        temp.EmployeeLastName.Text = employee.LastName ?? "";
                        temp.EmployeeEmail.Text = employee.Email ?? "";
                        temp.EmployeeAge.Text = employee.Age.ToString() ?? "";
                        temp.uid = employee.Id;




                        employees_panel.Children.Add(temp);

                    }

                }
            }



        }

        public BitmapSource BitmapFromBase64(string? b64string)
        {

            var bytes = Convert.FromBase64String(b64string);

            using (var stream = new MemoryStream(bytes))
            {
                return BitmapFrame.Create(stream,
                    BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
        }
    }
}
