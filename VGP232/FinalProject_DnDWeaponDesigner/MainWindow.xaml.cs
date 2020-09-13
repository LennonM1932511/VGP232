using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        WeaponList weaponlistLoader;
        private const string weaponDesignerFiles = "Weapon Designer Files|*.xml;*.json";
        public bool canSave = false;
        public bool hasChanged = false;
        public string projectPath;

        public MainWindow()
        {
            InitializeComponent();

            // Setup Listbox Default Source
            weaponlistLoader = new WeaponList();
            dgWeaponList.ItemsSource = weaponlistLoader;            
        }

        public void Update()
        {
            tbWeaponCount.Text = weaponlistLoader.Count().ToString();

            dgWeaponList.Items.Refresh();

            // after updating the collection, remove all SortDescription and add'em back.
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
        }

        public void Clear()
        {
            weaponlistLoader.Clear();
            hasChanged = false;
        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
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
            tbProjectTitle.Text = "Untitled Project";
            tbProjectTitle.FontStyle = FontStyles.Italic;
            canSave = false;
            Clear();
            Update();
        }

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
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
                    // Set the displayed project save and keep the path for Save
                    tbProjectTitle.Text = saveFile.SafeFileName;
                    tbProjectTitle.FontStyle = FontStyles.Normal;
                    projectPath = saveFile.FileName;
                    canSave = true;
                    hasChanged = false;
                }
            }
        }

        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = canSave;
        }

        private void SaveAsCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = hasChanged;
        }
    }
}
