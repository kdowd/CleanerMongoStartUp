using System;
using System.Collections.Generic;
using System.IO;
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
using CleanerMongoStartUp.Models;
using MongoConnect.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Serilog;

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
                IMongoCollection<Employees> collectionResults = db.GetCollection<Employees>("employees");




                if (collectionResults != null)
                {
                    // put into a dat structire we can loop through
                    List<Employees> employeesList = collectionResults.AsQueryable().ToList<Employees>();


                    // and loop
                    foreach (Employees employee in employeesList)
                    {

                        EmployeeCard temp = new EmployeeCard();

                        temp.EmployeeFirstName.Text = employee.FirstName ?? "";
                        temp.EmployeeLastName.Text = employee.LastName ?? "";
                        temp.EmployeeEmail.Text = employee.Email ?? "";
                        temp.EmployeeAge.Text = employee.Age.ToString() ?? "";

                        // finally, convert base64 and set Image control source
                        if (string.IsNullOrWhiteSpace(employee.Img) == false)
                        {
                            BitmapSource convertedImage = BitmapFromBase64(employee.Img);
                            temp.uid = employee.Id.ToString();
                            temp.EmployeeImage.Source = convertedImage;
                        }



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
