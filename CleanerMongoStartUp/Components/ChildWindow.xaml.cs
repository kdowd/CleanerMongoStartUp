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

        public async static void FindMoviesAsDocuments(string collName)
        {
            var db = Connector._database as MongoDatabaseBase;

            var collection = db.GetCollection<BsonDocument>(collName);
            var filter = new BsonDocument();
            int count = 0;
            using (var cursor = await collection.FindAsync<BsonDocument>(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (var document in batch)
                    {
                        var movieName = document.GetElement("firstname").Value.ToString();
                        Debug.WriteLine("Movie Name: {0}", movieName);
                        count++;
                    }
                }
            }
        }

        private void onLoaded(object sender, RoutedEventArgs e)
        {

            MyLabel.Content = _objectId.ToString();

            MongoDatabaseBase db;

            // IMongoCollection<Employees> collectionResults;

            if (Connector._database != null)
            {
                db = Connector._database as MongoDatabaseBase;

                //var expresssionFilter = Builders<Employees>.Filter.Lt(x => x.Age, 23);
                //var expresssionFilter = Builders<Employees>.Filter.Eq(x => x.Id, _objectId);

                //var expresssionFilter = new BsonDocument();
                //var expresssionFilter = new BsonDocument("_id", _objectId);
                var expresssionFilter = new BsonDocument("age", 12);


                List<Employees> collection = db.GetCollection<Employees>("employees").Find(expresssionFilter).ToList();

                if (collection?.Count > 1)
                {
                    foreach (var item in collection)
                    {
                        MessageBox.Show(item.Email);
                    }
                }
                else
                {
                    MessageBox.Show("Sorry, nothing found");
                }



                //MessageBox.Show(collection.ToBsonDocument);





                // var movies = collection.AsQueryable().ToList();

            }
        }



    }
}
