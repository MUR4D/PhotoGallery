using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for ShowPhoto.xaml
    /// </summary>
    public partial class ShowPhoto : Window
    {
        //MainWindow MainWindow = new MainWindow();

        public int Index1 { get; set; } = 0;

        public int Index2 { get; set; } = 0;

        
        
        private void Show()
        {
            ImageSource image = new BitmapImage(new Uri(MainWindow.Items[Index1].ImagePath[Index2], UriKind.RelativeOrAbsolute));
           ImageBox.Source = image;
        }
        public ShowPhoto()
        {
            InitializeComponent();
            

            //BitmapImage logo = new BitmapImage();
            //logo.BeginInit();
            //logo.BaseUri = new Uri(MainWindow.Items[0].ImagePath[0]);
            //this.ImageBox.Source =;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Show();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Index2!=0)
            {
                Index2--;
            }
            else
            {
                Index2 = MainWindow.Items[Index1].ImagePath.Length - 1;
            }
            Show();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (Index2== MainWindow.Items[Index1].ImagePath.Length - 1)
            {
                Index2 = 0;
            }
            else
            {
                Index2++;
            }
            Show();
        }
    }
}
