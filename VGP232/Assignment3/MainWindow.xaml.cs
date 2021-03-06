﻿using Microsoft.Win32;
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

// Assignment 3
// NAME: Lennon Marshall
// STUDENT NUMBER: 1932511
// MARKS: 94/100 (bonus +2) Excellent work! Functionally working. However, if you open a file it doesn't clear the image list and then add the elements, instead it appends to it also instead of add range, you should add one at a time while validating the path if the file still exists to generate a list of missing files. Nice use of commands. Missing prompt to save when we exit, also when we open a file and we didn't modify it, it was also prompt to save when we new it. The validate function is redudant, you can catch the exception and get the ex.Message and show it in a MessageBox.

// LC: (bonus) nice attempt to add the Edit Menu, but more works needs to be done to be able to use it with the listbox.

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
        public bool hasChanged = false;
        public string projectPath;


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
            // LC: this is suppose to prompt to save if it's changed as well before we quit.

            // exit the application
            Application.Current.Shutdown();
        }

        // LC: nice use of commands
        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // check if project has changed and ask to save
            if (hasChanged == true)
            {
                if (MessageBoxResult.Yes == MessageBox.Show("You have unsaved changes.\nWould you like to save them now?", "Warning", MessageBoxButton.YesNo))
                {
                    // call the save method depending if it was saved before
                    if (canSave == true)
                    {
                        SaveCommand_Executed(this, null);
                    }
                    else
                    {
                        SaveAsCommand_Executed(this, null);
                    }
                }
            }

            // reset all values to defaults
            ProjectName.Text = "Unsaved Project";
            tbColumns.Text = "0";
            tbOutputDir.Text = "";
            tbOutputFile.Text = "";
            IncludeMetaDataCheck.IsChecked = false;
            images.Clear();
            ImageListBox.Items.Refresh();

            hasChanged = false;
            canSave = false;
        }

        private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // setup the open dialog
            OpenFileDialog openFile = new OpenFileDialog
            {
                Title = "Open ",
                Filter = xmlFilter,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (openFile.ShowDialog() == true)
            {
                // validate the deserialization
                if (sheet.LoadXML(openFile.FileName) != true)
                {
                    MessageBox.Show("Unable to open selected Project file.", "XML Format Error");
                }
                else
                {
                    // load data into main window
                    // LC: you should clear it before you add to it.
                    // LC: also you should add it one by one in a for loop to validate the path and generate the error message.
                    images.AddRange(sheet.InputPaths);
                    ImageListBox.Items.Refresh();
                    tbColumns.Text = sheet.Columns.ToString();
                    tbOutputDir.Text = sheet.OutputDirectory;
                    tbOutputFile.Text = sheet.OutputFile;
                    IncludeMetaDataCheck.IsChecked = sheet.IncludeMetaData;

                    // Set the displayed project save and keep the path for Save
                    ProjectName.Text = openFile.SafeFileName;
                    projectPath = openFile.FileName;
                    canSave = true;
                }
            }
        }

        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // activate once project has been saved once with Save As
            e.CanExecute = canSave;
        }

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // save existing project
            // LC: this returns a bool, should show a message box explaining why it fails.
            sheet.SaveAsXML(projectPath);
        }

        private void SaveAsCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            // activate once data has been entered by user
            e.CanExecute = hasChanged;
        }

        private void SaveAsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // check if textboxes are empty and set the sheet values for saving
            // LC: can't you just assign the textboxes/controls values into the sheet?
            if (tbOutputFile.Text == "")
            {
                sheet.OutputFile = "";
            }
            if (tbOutputDir.Text == "")
            {
                sheet.OutputDirectory = "";
            }
            sheet.InputPaths = images;

            // setup save dialog
            SaveFileDialog saveFile = new SaveFileDialog
            {
                Title = "Save Project As...",
                Filter = xmlFilter,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (saveFile.ShowDialog() == true)
            {
                // LC: this returns a bool, should show a message box explaining why it fails.
                sheet.SaveAsXML(saveFile.FileName);

                // Set the displayed project save and keep the path for Save
                ProjectName.Text = saveFile.SafeFileName;
                projectPath = saveFile.FileName;
                canSave = true;
            }
        }


        // SpriteSheet Controls
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            // setup open dialog and append extention if user doesn't
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
                // split and set the path
                tbOutputDir.Text = System.IO.Path.GetDirectoryName(browsePath.FileName);
                sheet.OutputDirectory = tbOutputDir.Text;

                tbOutputFile.Text = System.IO.Path.GetFileName(browsePath.FileName);
                sheet.OutputFile = tbOutputFile.Text;
            }
        }

        private void AddButton_Clicked(object sender, RoutedEventArgs e)
        {
            // setup open dialog
            OpenFileDialog openFile = new OpenFileDialog
            {
                Title = "Add Images",
                Filter = pngFilter,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Multiselect = true
            };

            if (openFile.ShowDialog() == true)
            {
                // append each selected image to the list
                for (int i = 0; i < openFile.FileNames.Length; ++i)
                {
                    images.Add(openFile.FileNames[i]);
                    ImageListBox.Items.Refresh();
                }
                hasChanged = true;
            }
        }

        private void RemoveButton_Clicked(object sender, RoutedEventArgs e)
        {
            // remove image from the list
            if (ImageListBox.SelectedIndex != -1)
            {
                images.Remove((string)ImageListBox.SelectedItem);
                ImageListBox.Items.Refresh();
            }

        }

        private void GenerateButton_Clicked(object sender, RoutedEventArgs e)
        {
            sheet.InputPaths = images;

            // validate that all required data is set before generating output
            // LC: all you needed to do was to wrap this in a try-catch and use the MessageBox to show the exception.Message
            if (Validate())
            {
                sheet.Generate(true);

                // give user the option to open an explorer window with the output selected
                if (MessageBoxResult.Yes == MessageBox.Show("Sprite sheet generated. Would you like to view?", "Success", MessageBoxButton.YesNo))
                {
                    // Open explorer and select the file
                    // LC: even better to select the file.
                    Process.Start("explorer.exe", "/select," + sheet.OutputDirectory + "\\" + sheet.OutputFile);
                }
            }
        }

        // General Methods
        // LC: this method is redundant because all you need to do it to do a try-catch on Generate and show the ex.Message into a MessageBox
        private bool Validate()
        {
            bool success = false;

            // validate and throw a message for any missing data
            if (images == null || images.Count == 0)
            {
                MessageBox.Show("The input image path cannot be empty");
            }
            else if (string.IsNullOrEmpty(tbOutputFile.Text))
            {
                MessageBox.Show("The output filename cannot be empty");
            }
            else if (string.IsNullOrEmpty(tbOutputDir.Text))
            {
                MessageBox.Show("The output directory cannot be empty");
            }
            else if (int.Parse(tbColumns.Text) < 1)
            {
                MessageBox.Show("Column size must be at least 1");
            }
            else
            {
                success = true;
            }

            return success;
        }

        private void UpdateProject(object sender, RoutedEventArgs e)
        {
            // check if a textbox has a non default value
            if (sender == tbColumns)
            {
                if (tbColumns.Text != "0")
                {
                    hasChanged = true;
                }
            }
            if (sender == tbOutputDir)
            {
                if (tbOutputDir.Text != "")
                {
                    hasChanged = true;
                }
            }
            if (sender == tbOutputFile)
            {
                if (tbOutputFile.Text != "")
                {
                    hasChanged = true;
                }
            }
        }
    }
}