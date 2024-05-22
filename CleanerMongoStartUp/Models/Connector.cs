using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Diagnostics;
using System.Collections;
using System.Windows;
using MongoConnect.Models;


namespace CleanerMongoStartUp.Models
{
    internal class Connector
    {
        private const string connectionUri = "mongodb+srv://yourHero:Yoobee01@bse2024.jon1dt4.mongodb.net/?retryWrites=true&w=majority&appName=BSE2024";
        private static MongoClient? _client;
        public static MongoDatabaseBase? _database;


        public Connector()
        {

            EstablishConnection();
        }

        public void EstablishConnection()
        {
            try
            {

                _client = new MongoClient(connectionUri);
                _database = _client.GetDatabase("kdowd") as MongoDatabaseBase;


                //debug only, can we successfuly ping our DB, response from Mongo is Bson not Json
                //if (_database != null)
                //{
                //    BsonDocument isOK = _database.RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                //    BsonValue result = isOK.GetElement("ok").Value;
                //    MessageBox.Show(result.ToString());
                //}



            }
            catch (Exception e)
            {
                MessageBox.Show("oh dear.");
                MessageBox.Show(e.Message);
            }

        }
    }
}
