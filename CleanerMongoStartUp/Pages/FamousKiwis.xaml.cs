﻿using CleanerMongoStartUp.Models;
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

namespace CleanerMongoStartUp.Pages
{
    /// <summary>
    /// Interaction logic for FamousKiwis.xaml
    /// </summary>
    public partial class FamousKiwis : Page
    {
        public FamousKiwis()
        {
            InitializeComponent();
        }

        private void onLoaded(object sender, RoutedEventArgs e)
        {
            // make available the Database via static property;
            Connector connector = new Connector();


        }


        private void Employees_View_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            Frame TheFrame = window.MyApp as Frame;
            TheFrame.Navigate("Pages/CreateKiwi.xaml");



        }
    }
}
