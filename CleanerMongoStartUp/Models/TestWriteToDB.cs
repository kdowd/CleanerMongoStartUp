using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MongoConnect.Models;
using MongoDB.Driver;
namespace CleanerMongoStartUp.Models
{
    internal class TestWriteToDB
    {

        public TestWriteToDB()
        {

            MongoDatabaseBase? db = Connector._database;

            IMongoCollection<Employees> collectionResults = db.GetCollection<Employees>("employees");



            collectionResults.InsertOne(new Employees("RICHIE", "SUNAK", "richie@tory.com", 44, "AHHHHH") { });
        }
    }
}
