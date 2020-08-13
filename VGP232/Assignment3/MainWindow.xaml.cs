using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private const string pngFilter = "PNG Files|*.png";
        private const string xmlFilter = "XML Files|*.xml";
        public bool canSave = false;


        public MainWindow()
        {
            InitializeComponent();
            sheet = new Spritesheet();
            DataContext = sheet;
            ImageListBox.ItemsSource = images;
        }

        // File Menu
        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // SpriteSheet Controls
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog browsePath = new SaveFileDialog
            {
                Title = "Output Path",
                Filter = pngFilter,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                DefaultExt = "png",
                AddExtension = true
            };

            if (browsePath.ShowDialog() == true)
            {
                tbOutputDir.Text = System.IO.Path.GetDirectoryName(browsePath.FileName);
                sheet.OutputDirectory = tbOutputDir.Text;

                tbOutputFile.Text = System.IO.Path.GetFileName(browsePath.FileName);
                sheet.OutputFile = tbOutputFile.Text;
            }
        }

        private void AddButton_Clicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Title = "Add Images",
                Filter = pngFilter,
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
            if (ImageListBox.SelectedIndex != -1)
            {
                images.Remove((string)ImageListBox.SelectedItem);
                ImageListBox.Items.Refresh();
            }

        }

        private void GenerateButton_Clicked(object sender, RoutedEventArgs e)
        {
            //sheet.Validate();
            sheet.InputPaths = images;
            sheet.Generate(true);

            if (MessageBoxResult.Yes == MessageBox.Show("Sprite sheet generated. Would you like to view?", "Success", MessageBoxButton.YesNo))
            {
                // Open explorer and select the file
                Process.Start("explorer.exe", "/select," + sheet.OutputDirectory + "\\" + sheet.OutputFile);
            }

        }

        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProjectName.Text = "Unsaved Project";
            tbColumns.Text = "0";
            tbOutputDir.Text = "";
            tbOutputFile.Text = "";
            IncludeMetaDataCheck.IsChecked = false;
            images.Clear();
            ImageListBox.Items.Refresh();
        }

        private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Title = "Open ",
                Filter = xmlFilter,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (openFile.ShowDialog() == true)
            {
                if (sheet.LoadXML(openFile.FileName) != true)
                {
                    MessageBox.Show("Unable to open selected Project file.", "XML Format Error");
                }
                else
                {
                    ProjectName.Text = openFile.SafeFileName;
                    canSave = true;
                }
            }
        }

        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = canSave;
        }

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void SaveAsCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void SaveAsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        /*
         Remaining TODO:
            Save command
            SaveAs command
            Check if New prompts save
            warning message for missing images on porject load
         
         */
    }
}
