using CleanerMongoStartUp.Models;
using MongoConnect.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.ComponentModel;
using System.IO;
using System.Windows;
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
            // annoying....
            //var result = MessageBox.Show("Close Window?", "People Window", MessageBoxButton.YesNo,
            //MessageBoxImage.Question);
            //e.Cancel = (result == MessageBoxResult.No);
        }

        private void ChildWindow_Closed(object sender, EventArgs e)
        {

        }

        public BitmapSource BitmapFromBase64(string b64string = "")
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
                //var MyFilter = Builders<Employees>.Filter.Lt(x => x.Age, 80);
                //var MyFilter = Builders<Employees>.Filter.Eq(x => x.Id, _objectId);

                // finds all in current collection
                //var MyFilter = new BsonDocument();

                // finds all whose age is 80
                //var MyFilter = new BsonDocument("age", 80);

                // finds based upon _id - can only return 1 doc
                var MyFilter = new BsonDocument("_id", _objectId);



                List<Employees> collection = db.GetCollection<Employees>("employees")
                    .Find(MyFilter)
                    .ToList();


                if (collection?.Count >= 1)
                {
                    foreach (var item in collection)
                    {
                        EmployeeFirstName.Text = item.FirstName;
                        EmployeeLastName.Text = item.LastName;
                        EmployeeEmail.Text = item.Email;
                        EmployeeExt.Text = item.Age.ToString();
                        EmployeeDescription.Text = item.Description;


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
