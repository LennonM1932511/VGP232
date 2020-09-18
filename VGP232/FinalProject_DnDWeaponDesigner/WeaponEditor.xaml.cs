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
using System.Windows.Shapes;
using WeaponLib;

namespace FinalProject_DnDWeaponDesigner
{
    /// <summary>
    /// Interaction logic for WeaponEditor.xaml
    /// </summary>
    public partial class WeaponEditor : Window
    {
        public Weapon TempWeapon { get; set; }

        public WeaponEditor()
        {
            InitializeComponent();

            // Populate comboboxes with enumerated lists
            cbCategory.ItemsSource = Enum.GetNames(typeof(Weapon.Category));
            cbType.ItemsSource = Enum.GetNames(typeof(Weapon.DamageType));

            TempWeapon = new Weapon();
            DataContext = TempWeapon;
        }

        public void EditWeapon(Weapon weapon)
        {
            TempWeapon = weapon;
            DataContext = TempWeapon;
            Title = "EDIT WEAPON";
            AddButton.Content = "SAVE";
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            // setup open dialog and append extention if user doesn't
            OpenFileDialog browsePath = new OpenFileDialog
            {
                Title = "Select image file...",
                Filter = "Image Files|*.png;*.jpg",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (browsePath.ShowDialog() == true)
            {
                // split and set the path
                iPath.Source = new BitmapImage(new Uri(browsePath.FileName));
                TempWeapon.sImage = browsePath.FileName;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
