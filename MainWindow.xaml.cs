using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public static ObservableCollection<Folder> Items { get; set; }
        string read;

        





        public MainWindow()
        {
            InitializeComponent();
           
            Items = new ObservableCollection<Folder>();
            
            read = File.ReadAllText("ImagePath.json");
            Items.Add( JsonSerializer.Deserialize<Folder>(read));


            FoldersList.ItemsSource = Items; 


        }
        private void Save()
        {
            for (int i = 0; i < Items.Count; i++)
            {

            File.WriteAllText("ImagePath.json", JsonSerializer.Serialize(Items[i]));
            }
        }
        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {

            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            bool flag;


            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Folder folder;
                bool search = false ;
                for (int i = 0; i < Items.Count; i++)
                {
                    if (Items[i].Name==dialog.FileName)
                    {
                        search = true;
                        MessageBox.Show("You have already added this folder");
                    }
                }
                if (search==false)
                {
                    folder = new Folder();
                    folder.Name = dialog.FileName;

                string[] images = Directory.GetFiles(dialog.FileName);
                List<string> imgs = images.ToList();
                flag = false;
                for (int i = 0; i < images.Length; i++)
                {
                    if (imgs[i].Contains("jfif")|| imgs[i].Contains("jpg") || imgs[i].Contains("jpeg") || imgs[i].Contains("png") || imgs[i].Contains("tiff"))
                    {

                        flag = false;
                    }
                    else
                    {
                        imgs.RemoveAt(i);
                        break;
                    }

                }

                if (flag == false)
                {
                    folder.ImagePath = imgs.ToArray();
                }
               
                
                Items.Add(folder);
               
                }

            }
            Save();
        }

        private void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedFolder = FoldersList.SelectedItem as Folder;
            if (selectedFolder!=null)
            {
            Items.Remove(selectedFolder);
            Save();

            }
            //FoldersList.Items.Clear();
        }

        private void FoldersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ImagesList.Items.Clear();
           
            if ((FoldersList.SelectedItem as Folder)!=null&&(FoldersList.SelectedItem as Folder).ImagePath!=null)
            {


            foreach (var item in (FoldersList.SelectedItem as Folder).ImagePath )
            {
           
                    ImagesList.Items.Add(item);

             }
                  
            }
            
               
            }
           
        

        private void ImagesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ImagesList.SelectedItem != null)
            {
                ShowPhoto form2 = new ShowPhoto();
                form2.Index1 = FoldersList.SelectedIndex;
                form2.Index2 = ImagesList.SelectedIndex;
                form2.ShowDialog();

            }
        }

        private void ImagesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
    }
    public class Folder
    {
        public string[] ImagePath { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name};{ImagePath}"; 
        }
    }


}
