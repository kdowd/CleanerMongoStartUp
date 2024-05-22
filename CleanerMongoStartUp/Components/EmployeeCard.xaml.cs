﻿using System;
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
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;




namespace CleanerMongoStartUp
{

    public partial class EmployeeCard : UserControl
    {
        private string _uid;

        public string uid
        {
            get { return _uid; }
            set { _uid = value; }
        }


        public EmployeeCard()
        {

            InitializeComponent();
        }

        private void onCardLoaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Employees Unique ID = ${uid}");

        }
    }
}
