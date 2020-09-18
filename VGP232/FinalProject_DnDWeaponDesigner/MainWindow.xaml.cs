using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WeaponLib;

namespace FinalProject_DnDWeaponDesigner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // setup the WeaponList class
        WeaponList weaponlistLoader;

        // define the project files for save/open dialogs
        private const string weaponDesignerFiles = "Weapon Designer Files|*.xml;*.json";

        // flags for controlling the menu commands
        public bool canSave = false;
        public bool hasChanged = false;

        // set project path for Save command
        public string projectPath;
        public string projectFolder;
        public string projectImagesFolder;

        // set initial index count to be displayed
        public int indexCount = 0;

        public MainWindow()
        {
            InitializeComponent();

            // Setup DataGrid Default Source
            weaponlistLoader = new WeaponList();
            dgWeaponList.ItemsSource = weaponlistLoader;
        }

        // update the weapon count, refresh the datagrid, and re-sort
        public void Update()
        {
            indexCount = weaponlistLoader.Count();
            tbWeaponCount.Text = indexCount.ToString();

            dgWeaponList.Items.Refresh();

            // after updating the collection, remove all SortDescription and add'em back.
            // this ensures the datagrid is properly updated when already sorted
            SortDescriptionCollection sortDescriptions = new SortDescriptionCollection();
            foreach (SortDescription sd in dgWeaponList.Items.SortDescriptions)
            {
                sortDescriptions.Add(sd);
            }
            dgWeaponList.Items.SortDescriptions.Clear();

            foreach (SortDescription sd in sortDescriptions)
            {
                dgWeaponList.Items.SortDescriptions.Add(sd);
            }

            // update the group by category
            ICollectionView view = CollectionViewSource.GetDefaultView(dgWeaponList.ItemsSource);
            if (view != null && view.CanGroup == true)
            {
                view.GroupDescriptions.Clear();
                view.GroupDescriptions.Add(new PropertyGroupDescription("eCategory"));
            }
        }

        // used when opening a new
        public void Clear()
        {

        }

        public void SaveChanges()
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
        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            // Ask to save project if changes were made
            SaveChanges();

            Application.Current.Shutdown();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            WeaponEditor editor = new WeaponEditor();

            if (editor.ShowDialog() == true)
            {
                weaponlistLoader.Add(editor.TempWeapon);
                Update();
                hasChanged = true;
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgWeaponList.SelectedIndex != -1)
            {
                WeaponEditor editor = new WeaponEditor();
                editor.EditWeapon((Weapon)dgWeaponList.SelectedItem);
                if (editor.ShowDialog() == true)
                {
                    Update();
                    hasChanged = true;
                }
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgWeaponList.SelectedIndex != -1)
            {
                // Remove selected weapon from list
                weaponlistLoader.Remove((Weapon)dgWeaponList.SelectedItem);
                Update();
                hasChanged = true;
            }
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Ask to save project if changes were made
            SaveChanges();

            // reset all values to defaults
            weaponlistLoader.Clear();

            tbProjectTitle.Text = "Untitled Project";
            tbProjectTitle.FontStyle = FontStyles.Italic;

            projectFolder = "";
            projectImagesFolder = "";
            projectPath = "";

            hasChanged = false;
            canSave = false;

            Update();
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Ask to save project if changes were made
            SaveChanges();

            // setup the open dialog
            OpenFileDialog openFile = new OpenFileDialog
            {
                Title = "Open",
                Filter = weaponDesignerFiles,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (openFile.ShowDialog() == true)
            {
                if (weaponlistLoader.Load(openFile.FileName) != true)
                {
                    MessageBox.Show("Unable to open selected project file.", "Format Error");
                }
                else
                {
                    // Set the displayed project save and keep the path for Save
                    tbProjectTitle.Text = openFile.SafeFileName;
                    tbProjectTitle.FontStyle = FontStyles.Normal;
                    projectPath = openFile.FileName;
                    canSave = true;
                    Update();

                    // Re-sort loaded project contents by category
                    ICollectionView view = CollectionViewSource.GetDefaultView(dgWeaponList.ItemsSource);
                    view.SortDescriptions.Clear();
                    view.SortDescriptions.Add(new SortDescription("eCategory", ListSortDirection.Ascending));
                    view.Refresh();
                }
            }
        }

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            if (weaponlistLoader.Save(projectPath) != true)
            {
                MessageBox.Show("Unable to save project file.", "Format Error");
            }
            else
            {
                hasChanged = false;
            }
        }

        private void SaveAsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // setup save dialog
            SaveFileDialog saveFile = new SaveFileDialog
            {
                Title = "Save Project As...",
                Filter = weaponDesignerFiles,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (saveFile.ShowDialog() == true)
            {
                if (weaponlistLoader.Save(saveFile.FileName) != true)
                {
                    MessageBox.Show("Unable to save project file.", "Format Error");
                }
                else
                {
                    // Set the displayed project project name
                    tbProjectTitle.Text = saveFile.SafeFileName;
                    tbProjectTitle.FontStyle = FontStyles.Normal;

                    // set the project path for saving later
                    projectPath = saveFile.FileName;

                    // flags, can now use Save command, and is unchanged again.
                    canSave = true;
                    hasChanged = false;
                }
            }
        }

        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = canSave && hasChanged;
        }

        private void SaveAsCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = hasChanged || indexCount > 0;
        }

        private void LoadExampleClick(object sender, RoutedEventArgs e)
        {
            // set the path to the included example set and load it
            string basicPath = System.IO.Path.Combine(
                Directory.GetParent(Environment.CurrentDirectory).Parent.FullName,
                "basic_set\\BasicSet.xml");

            if (weaponlistLoader.Load(basicPath) != true)
            {
                MessageBox.Show("Unable to open Example Set.", "Format Error");
            }

            // Set the displayed project save
            tbProjectTitle.Text = "Example Set";
            tbProjectTitle.FontStyle = FontStyles.Normal;
            Update();

            // Re-sort loaded project contents by category
            ICollectionView view = CollectionViewSource.GetDefaultView(dgWeaponList.ItemsSource);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription("eCategory", ListSortDirection.Ascending));
            view.Refresh();
        }
    }
}
