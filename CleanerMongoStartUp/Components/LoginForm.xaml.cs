using System;
using System.Collections.Generic;
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
using MongoDB.Driver;
using SharpCompress.Common;

namespace CleanerMongoStartUp.Components
{

    public partial class LoginForm : UserControl
    {
        public LoginForm()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string fn = FirstName.Text.Trim() ?? "";
            string ln = LastName.Text.Trim() ?? "";
            int age;

            if (Int32.TryParse(Age.Text.Trim(), out age))
            {
                // success
            }
            else
            {
                // fail, handle it
                age = 0;
            }

            string email = Email.Text.Trim() ?? "";
            string desc = Description.Text.Trim() ?? "";
            var temp = new Employees(fn, ln, email, age, "", desc);

            // https://raw.githubusercontent.com/mongodb/docs-csharp/master/source/includes/code-examples/insert-one/InsertOne.cs
            MongoDatabaseBase? db = Connector._database as MongoDatabaseBase;

            if (db != null)
            {


                try
                {
                    IMongoCollection<Employees> collectionResults = db.GetCollection<Employees>("employees");

                    var filter = Builders<Employees>.Filter.Eq(r => r.Email, temp.Email);
                    var document = collectionResults.Find(filter).FirstOrDefault();

                    if (document != null)
                    {
                        // MessageBox.Show($"Found: {document.ToBsonDocument()}");
                        MessageBox.Show("That Email already exists");
                        return;
                    }

                    // returns void, so test if it inserted, its all synchronous
                    collectionResults.InsertOne(temp);
                    var newDoc = collectionResults.Find(filter).FirstOrDefault();

                    // Prints the document
                    MessageBox.Show($"Document Inserted: {newDoc.ToBsonDocument()}");

                }
                catch (MongoException error)
                {

                    MessageBox.Show(error.ToString());
                }

            }




            //

            // IMongoCollection<Employees> res = collection.InsertOne(temp);
            //collectionResults.InsertOne(temp);

            // MessageBox.Show(res.ToString());
            // collectionResults.InsertOne(new Employees("RICHIE", "SUNAK", "richie@tory.com", 44, "AHHHHH", description: "blah") { });




        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            FirstName.Text = "john";
            LastName.Text = "peel";
            Age.Text = "66"; ;

            Email.Text = "john@bbc.com";
            Description.Text = "Music Man";

        }

    }
}
