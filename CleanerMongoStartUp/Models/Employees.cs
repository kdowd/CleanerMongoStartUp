﻿using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoConnect.Models
{
    public class Employees
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public ObjectId Id { get; set; }

        //data type
        [BsonRepresentation(BsonType.String)]
        [BsonElement("firstname")]
        public string? FirstName { get; set; }


        [BsonRepresentation(BsonType.String)]
        [BsonElement("lastname")]
        public string? LastName { get; set; }

        [BsonRepresentation(BsonType.String)]
        [BsonElement("email")]
        public string? Email { get; set; }

        [BsonRepresentation(BsonType.String)]
        [BsonElement("img")]

        private string? _img;

        public string? Img
        {
            get { return _img; }
            set { _img = value; }
        }

        [BsonRepresentation(BsonType.Int32)]
        [BsonElement("age")]
        public int? Age { get; set; }

        // do we need this ?
        //public Employees(string? firstName, string? lastName, string? email, int? age)
        //{
        //    FirstName = firstName;
        //    LastName = lastName;
        //    Email = email;
        //    Age = age;
        //}
    }
}

