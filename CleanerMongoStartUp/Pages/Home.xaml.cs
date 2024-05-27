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
using System.Timers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Threading;



namespace CleanerMongoStartUp.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }


        public void Test()
        {
            MessageBox.Show("WTF");
        }


        private void Home_Page_Loaded(object sender, RoutedEventArgs e)
        {

            new SetTimeout(4);

        }
    }



}



public class SetTimeout : Control
{
    private System.Timers.Timer aTimer;
    private int secs;

    public SetTimeout(int n)
    {
        secs = n * 1000;
        aTimer = new System.Timers.Timer(secs);
        aTimer.Elapsed += OnTimedEvent;
        aTimer.AutoReset = false;
        aTimer.Enabled = true;
    }


    private void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        // aTimer.Stop();
        // aTimer.Dispose();
        //this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { }));
        MessageBox.Show("The Elapsed event was raised");
    }
}



//public class Example
//{
//    private static System.Timers.Timer aTimer;
//    private static int secs;

//    public static void Start(int n)
//    {
//        secs = n * 1000;
//        SetTimer();
//    }

//    private static void SetTimer()
//    {
//        aTimer = new System.Timers.Timer(secs);
//        aTimer.Elapsed += OnTimedEvent;
//        aTimer.AutoReset = false;
//        aTimer.Enabled = true;
//    }

//    private static void OnTimedEvent(Object source, ElapsedEventArgs e)
//    {
//        // aTimer.Stop();
//        // aTimer.Dispose();
//        MessageBox.Show("The Elapsed event was raised");
//    }
//}