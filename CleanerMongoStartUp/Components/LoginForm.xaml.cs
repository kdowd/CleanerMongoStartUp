using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
using Microsoft.Win32;
using MongoConnect.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using SharpCompress.Common;

namespace CleanerMongoStartUp.Components
{

    public partial class LoginForm : UserControl
    {
        private string? Base64Image { get; set; } = string.Empty;
        private int UserAge { get; set; } = 0;

        public LoginForm()
        {
            InitializeComponent();

        }

        private bool OnValidate()
        {
            bool IsFormReady = true;

            if (string.IsNullOrEmpty(FirstName.Text.Trim()))
            {
                return false;
            };

            if (string.IsNullOrEmpty(LastName.Text.Trim()))
            {
                return false;
            };

            if (string.IsNullOrEmpty(Email.Text.Trim()))
            {
                return false;
            };


            if (UserAge <= 0)
            {
                return false;
            };
            if (string.IsNullOrEmpty(Description.Text.Trim()))
            {
                return false;
            };

            if (string.IsNullOrEmpty(Base64Image.Trim()))
            {
                return false;
            };




            return IsFormReady;
        }

        


    private bool IsValidEmail(string email)
    {
            // RegEx from https://uibakery.io/regex-library/email-regex-csharp
            Regex validateEmailRegex = new Regex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");
            return validateEmailRegex.IsMatch(email);
}

    private void OnEmailFieldLostFocus(object sender, EventArgs e)
        {
            string UserEmailString = Email.Text.Trim();
            if (!string.IsNullOrEmpty(UserEmailString))
            {
              bool isGood = IsValidEmail(UserEmailString);
            }
        }

        private void OnAgeFieldLostFocus(object sender, EventArgs e)
        {
            int age;

            if (Int32.TryParse(Age.Text.Trim(), out age))
            {
                UserAge = age;
            }
            else
            {
                // fail, handle it
                age = 0;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (OnValidate() == false)
            {
                MessageBox.Show("Please Fillin Form Correctly");
                return;
            }

            string fn = FirstName.Text.Trim() ?? "";
            string ln = LastName.Text.Trim() ?? "";

            string email = Email.Text.Trim() ?? "";
            string desc = Description.Text.Trim() ?? "";

            var temp = new Employees(fn, ln, email, this.UserAge, this.Base64Image, desc);


            MongoDatabaseBase? db = Connector._database as MongoDatabaseBase;

            if (db == null) { return; }

            try
            {
                IMongoCollection<Employees> collectionResults = db.GetCollection<Employees>("employees");

                // check if email already exists using Builders fpr DB query;
                // should be called QueryBuilder really...
                var filter = Builders<Employees>.Filter.Eq("Email", temp.Email);
                //var filter = Builders<Employees>.Filter.Eq("Email", temp.Email) &
                // Builders<Employees>.Filter.Eq("UserName", temp.FirstName);

                // isFoundDoc will be returned as an Employees Class from MongoDB
                Employees? IsFoundDoc = collectionResults.Find(filter).FirstOrDefault();

                if (IsFoundDoc != null)
                {
                    // MessageBox.Show($"Found: {document.ToBsonDocument()}");
                    MessageBox.Show("That Email already exists");
                    return;
                }

                // InsertOne doesn't return anything !!!!
                collectionResults.InsertOne(temp);
                //So, we need to test if really was inserted
                Employees IsInsertedDoc = collectionResults.Find(filter).FirstOrDefault();


                if (IsInsertedDoc != null)
                {
                    // need to update UI sonehow
                    MessageBox.Show("Document Inserted");
                }


            }
            catch (MongoException error)
            {

                MessageBox.Show(error.ToString());
            }



        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // pre-populate form for easy testing
            //FirstName.Text = "john";
            //LastName.Text = "Peel";
            //Age.Text = "66"; ;
            //Email.Text = "john@bbc.com";
            //Description.Text = "Music Man";


        }

        // https://stackoverflow.com/questions/21325661/convert-an-image-selected-by-path-to-base64-string 
        private void OnFileDialogue(object sender, RoutedEventArgs e)
        {

            OpenFileDialog dialogBox = new OpenFileDialog();
            dialogBox.Filter = "JPG Files (*.jpg)|*.jpg| PNG Files (*.png)|*.png| JPEG Files (*.jpeg)|*.jpeg|GIF Files (*.gif)|*.gif";



            bool? IsDialogGood = dialogBox.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (IsDialogGood == true)
            {
                string filepath = dialogBox.FileName;
                string filename = dialogBox.SafeFileName;

                if (string.IsNullOrEmpty(filepath) == false)
                {
                    byte[] imageArray = System.IO.File.ReadAllBytes(filepath);
                    Base64Image = Convert.ToBase64String(imageArray);
                    // finally, update UI, could even show an image thumb in the form. Nice.
                    ImagePath.Text = filename;
                }



            }
        }

    }
}
