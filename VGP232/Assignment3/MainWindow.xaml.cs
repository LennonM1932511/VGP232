using Microsoft.Win32;
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
using TextureAtlasLib;

namespace Assignment3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Spritesheet sheet;
        List<string> images = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            sheet = new Spritesheet();
            DataContext = sheet;
            ImageListBox.ItemsSource = images;
        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AddButton_Clicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Title = "Add Images",
                Filter = "PNG Files | *.png",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Multiselect = true
            };

            if (openFile.ShowDialog() == true)
            {
                for (int i = 0; i < openFile.FileNames.Length; ++i)
                {
                    images.Add(openFile.FileNames[i]);
                    ImageListBox.Items.Refresh();
                }
            }
        }

        private void RemoveButton_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void GenerateButton_Clicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
