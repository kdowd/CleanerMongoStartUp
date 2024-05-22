using System;
using System.Collections.Generic;
using System.IO;
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





namespace CleanerMongoStartUp.Components
{

    public partial class Base64ToImage : UserControl
    {
        //public string? ImgSrc { get; set; } = String.Empty;

        private string? imgsrc;

        public string ImgSrc
        {
            get { return imgsrc; }
            set { imgsrc = value; BitmapFromBase64(); }
        }


        public Base64ToImage()
        {
            InitializeComponent();
        }

        private void base64_image_Loaded(object sender, RoutedEventArgs e)
        {

        }



        public BitmapSource BitmapFromBase64()
        {
            var bytes = Convert.FromBase64String(ImgSrc);

            using (var stream = new MemoryStream(bytes))
            {
                return BitmapFrame.Create(stream,
                    BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
        }
    }
}
