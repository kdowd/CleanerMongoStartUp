using System.ComponentModel;
using System.Windows;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using CleanerMongoStartUp.Models;
using MongoConnect.Models;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;
using System.Globalization;
using System.IO;
using System.Windows.Media.Imaging;



namespace CleanerMongoStartUp.Components
{
    public partial class ChildWindow : Window
    {
        private readonly ObjectId _objectId;
        public string MyProperty { get; set; } = "99";


        public ChildWindow(ObjectId objectId)
        {
            _objectId = objectId;
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

        public BitmapSource BitmapFromBase64(string? b64string)
        {

            var bytes = Convert.FromBase64String(b64string);

            using (var stream = new MemoryStream(bytes))
            {
                return BitmapFrame.Create(stream,
                    BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
        }


        private void onLoaded(object sender, RoutedEventArgs e)
        {

            MyLabel.Content = _objectId.ToString();

            MongoDatabaseBase db;

            if (Connector._database != null)
            {
                db = Connector._database as MongoDatabaseBase;

                // finds all with maths logic
                //var MyFilter = Builders<Employees>.Filter.Lt(x => x.Age, 23);
                //var MyFilter = Builders<Employees>.Filter.Eq(x => x.Id, _objectId);

                // finds all in collection
                //var MyFilter = new BsonDocument();

                //var MyFilter = new BsonDocument("age", 80);

                var MyFilter = new BsonDocument("_id", _objectId);
                //var MyFilter = new BsonDocument();
                //MyFilter.Add("_id", _objectId);


                List<Employees> collection = db.GetCollection<Employees>("employees")
                    .Find(MyFilter)
                    .ToList();


                if (collection?.Count >= 1)
                {
                    foreach (var item in collection)
                    {
                        EmployeeFirstName.Text = item.FirstName;
                        EmployeeLastName.Text = item.LastName;

                        // finally, convert base64 and set Image control source
                        if (string.IsNullOrWhiteSpace(item.Img) == false)
                        {
                            BitmapSource convertedImage = BitmapFromBase64(item.Img);

                            EmployeeImage.Source = convertedImage;
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Sorry, nothing found");
                }







            }
        }



    }
}
